namespace Ecommerce.Proxy
{
    public class ProxyBookRepository : IBookRepository
    {
        private readonly IBookRepository _bookRepository;
        private readonly Dictionary<int, Book> bookCache = new Dictionary<int, Book>();
        private bool isAuthenticated = false;
        public ProxyBookRepository(IBookRepository bookRepository) 
        {
            _bookRepository= bookRepository;
        }

        public Book GetBook(int bookId)
        {
            if (!isAuthenticated)
            {
                Console.WriteLine("Utente non autenticato");

                return null;
            }
            
            if (bookCache.TryGetValue(bookId, out var cachedBook)) 
            {
                return cachedBook;
            }

            var book = _bookRepository.GetBook(bookId);
            if (book != null) 
            {
                bookCache[bookId] = book;
            }

            return book;
        }

        
    }
}
