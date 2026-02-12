using Ecommerce.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repositories
{
    public class UserOrderRepository : IUserOrderRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<IdentityUser> _userManager;

        public UserOrderRepository(
            ApplicationDbContext db,
            UserManager<IdentityUser> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<IEnumerable<Order>> UserOrders()
        {
            var userId = GetUserId();

            return await _db.Orders
                .Include(x => x.OrderStatus)
                .Include(x => x.OrderDetail)
                .ThenInclude(x => x.Book)
                .ThenInclude(x => x.Genre)
                .Where(a => a.UserId == userId)
                .OrderByDescending(x => x.CreateDate)
                .ToListAsync();
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
