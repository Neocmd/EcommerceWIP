namespace Ecommerce.Services.Interfaces
{
    public interface ICartService
    {
        Task<int> AddItemAsync(int bookId, int qty = 1);
        Task<int> RemoveItemAsync(int bookId);
        Task<ShoppingCart> GetUserCartAsync();
        Task<int> GetCartItemCountAsync();
        Task CheckoutAsync();
    }
}
