namespace Ecommerce.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetUserOrdersAsync();
    }
}
