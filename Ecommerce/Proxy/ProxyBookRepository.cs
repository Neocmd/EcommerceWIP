namespace Ecommerce.Proxy
{
    public class ProxyBookRepository : IBookRepository
    {
        private readonly IBookRepository _bookRepository;
        private readonly Dictionary<int, Book> _bookCache = new();
        private readonly bool _isAuthenticated;

        public ProxyBookRepository(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
            _isAuthenticated = false;
        }

        public Book? GetBook(int bookId)
        {
            if (!_isAuthenticated)
            {
                return null;
            }

            if (_bookCache.TryGetValue(bookId, out var cachedBook))
            {
                return cachedBook;
            }

            var book = _bookRepository.GetBook(bookId);
            if (book is not null)
            {
                _bookCache[bookId] = book;
            }

            return book;
        }
    }
}
