using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repositories
{
    public class HomeRepository : IHomeRepository
    {
        private readonly ApplicationDbContext _db;

        public HomeRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Genre>> Genres()
        {
            return await _db.Genres.OrderBy(x => x.GenreName).ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBooks(string sTerm = "", int genreId = 0)
        {
            var query = _db.Books
                .Include(x => x.Genre)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(sTerm))
            {
                var loweredTerm = sTerm.Trim().ToLower();
                query = query.Where(book => book.BookName.ToLower().StartsWith(loweredTerm));
            }

            if (genreId > 0)
            {
                query = query.Where(book => book.GenreId == genreId);
            }

            var books = await query
                .Select(book => new Book
                {
                    Id = book.Id,
                    Image = book.Image,
                    AuthorName = book.AuthorName,
                    BookName = book.BookName,
                    GenreId = book.GenreId,
                    Price = book.Price,
                    GenreName = book.Genre.GenreName
                })
                .ToListAsync();

            return books;
        }
    }
}
