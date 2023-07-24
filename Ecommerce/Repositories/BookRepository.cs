namespace Ecommerce.Repositories
{
    public class BookRepository : IBookRepository
    {
        public Book GetBook(int bookId)
        {
            return new Book
            {
                Id = bookId,
                BookName = "Book" + bookId,
                AuthorName = "Author " + bookId,
                Price = 29.99,
                Image = "book_image_" + bookId + ".jpg",
                GenreId = 1,
                Genre = new Genre { Id = 1, GenreName = "Fiction" }

            };
        }
    }
}
