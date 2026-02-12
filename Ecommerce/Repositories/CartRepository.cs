using Ecommerce.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartRepository(
            ApplicationDbContext db,
            IHttpContextAccessor httpContextAccessor,
            UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<int> AddItem(int bookId, int qty)
        {
            var userId = GetUserId();

            await using var transaction = await _db.Database.BeginTransactionAsync();

            var cart = await GetCart(userId);
            if (cart is null)
            {
                cart = new ShoppingCart
                {
                    UserId = userId
                };
                await _db.ShoppingCarts.AddAsync(cart);
                await _db.SaveChangesAsync();
            }

            var cartItem = await _db.CartDetails
                .FirstOrDefaultAsync(a => a.ShoppingCartId == cart.Id && a.BookId == bookId);

            if (cartItem is not null)
            {
                cartItem.Quantity += qty;
            }
            else
            {
                var book = await _db.Books.FindAsync(bookId);
                if (book is null)
                {
                    throw new NotFoundException("Book not found.");
                }

                cartItem = new CartDetail
                {
                    BookId = bookId,
                    ShoppingCartId = cart.Id,
                    Quantity = qty,
                    UnitPrice = book.Price
                };

                await _db.CartDetails.AddAsync(cartItem);
            }

            await _db.SaveChangesAsync();
            await transaction.CommitAsync();

            return await GetCartItemCount(userId);
        }

        public async Task<int> RemoveItem(int bookId)
        {
            var userId = GetUserId();

            var cart = await GetCart(userId);
            if (cart is null)
            {
                throw new ValidationException("Invalid cart.");
            }

            var cartItem = await _db.CartDetails
                .FirstOrDefaultAsync(a => a.ShoppingCartId == cart.Id && a.BookId == bookId);

            if (cartItem is null)
            {
                throw new NotFoundException("Cart item not found.");
            }

            if (cartItem.Quantity <= 1)
            {
                _db.CartDetails.Remove(cartItem);
            }
            else
            {
                cartItem.Quantity -= 1;
            }

            await _db.SaveChangesAsync();
            return await GetCartItemCount(userId);
        }

        public async Task<ShoppingCart> GetUserCart()
        {
            var userId = GetUserId();

            var shoppingCart = await _db.ShoppingCarts
                .Include(a => a.CartDetails)
                .ThenInclude(a => a.Book)
                .ThenInclude(a => a.Genre)
                .FirstOrDefaultAsync(a => a.UserId == userId);

            return shoppingCart ?? new ShoppingCart { UserId = userId };
        }

        public async Task<ShoppingCart?> GetCart(string userId)
        {
            return await _db.ShoppingCarts.FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task<int> GetCartItemCount(string userId = "")
        {
            var effectiveUserId = string.IsNullOrWhiteSpace(userId) ? GetUserId() : userId;

            return await (from cart in _db.ShoppingCarts
                          join cartDetail in _db.CartDetails on cart.Id equals cartDetail.ShoppingCartId
                          where cart.UserId == effectiveUserId
                          select cartDetail.Id).CountAsync();
        }

        public async Task Checkout()
        {
            await using var transaction = await _db.Database.BeginTransactionAsync();

            var userId = GetUserId();
            var cart = await GetCart(userId);
            if (cart is null)
            {
                throw new ValidationException("Invalid cart.");
            }

            var cartDetails = await _db.CartDetails
                .Where(a => a.ShoppingCartId == cart.Id)
                .ToListAsync();

            if (cartDetails.Count == 0)
            {
                throw new ValidationException("Cart is empty.");
            }

            var order = new Order
            {
                UserId = userId,
                CreateDate = DateTime.UtcNow,
                OrderStatusId = 1
            };

            await _db.Orders.AddAsync(order);
            await _db.SaveChangesAsync();

            foreach (var item in cartDetails)
            {
                var orderDetail = new OrderDetail
                {
                    BookId = item.BookId,
                    OrderId = order.Id,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                };

                await _db.OrderDetails.AddAsync(orderDetail);
            }

            _db.CartDetails.RemoveRange(cartDetails);

            await _db.SaveChangesAsync();
            await transaction.CommitAsync();
        }

        private string GetUserId()
        {
            var principal = _httpContextAccessor.HttpContext?.User
                ?? throw new AppException("User context is not available.", 401);

            var userId = _userManager.GetUserId(principal);
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new AppException("User is not logged in.", 401);
            }

            return userId;
        }
    }
}
