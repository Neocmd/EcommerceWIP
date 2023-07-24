using Ecommerce.Models;

namespace Ecommerce.Repositories
{
    public interface IUserOrderRepository
    {
        Task<IEnumerable<Order>> UserOrders();
    }
}