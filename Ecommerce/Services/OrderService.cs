using Ecommerce.Services.Interfaces;

namespace Ecommerce.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUserOrderRepository _userOrderRepository;

        public OrderService(IUserOrderRepository userOrderRepository)
        {
            _userOrderRepository = userOrderRepository;
        }

        public async Task<IEnumerable<Order>> GetUserOrdersAsync()
        {
            return await _userOrderRepository.UserOrders();
        }
    }
}
