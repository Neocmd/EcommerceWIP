namespace Ecommerce.Repositories
{
    public interface IBookRepository
    {
        Book? GetBook(int bookId);
    }
}
