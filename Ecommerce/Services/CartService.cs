using Ecommerce.Exceptions;
using Ecommerce.Services.Interfaces;

namespace Ecommerce.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<int> AddItemAsync(int bookId, int qty = 1)
        {
            if (bookId <= 0)
            {
                throw new ValidationException("Book id is invalid.");
            }

            if (qty <= 0)
            {
                throw new ValidationException("Quantity must be greater than zero.");
            }

            return await _cartRepository.AddItem(bookId, qty);
        }

        public async Task<int> RemoveItemAsync(int bookId)
        {
            if (bookId <= 0)
            {
                throw new ValidationException("Book id is invalid.");
            }

            return await _cartRepository.RemoveItem(bookId);
        }

        public async Task<ShoppingCart> GetUserCartAsync()
        {
            return await _cartRepository.GetUserCart();
        }

        public async Task<int> GetCartItemCountAsync()
        {
            return await _cartRepository.GetCartItemCount();
        }

        public async Task CheckoutAsync()
        {
            await _cartRepository.Checkout();
        }
    }
}
