namespace Ecommerce.Models.BDM
{
    public class BookDisplayModel
    {
        public IEnumerable<Book> Books { get; set; } = Enumerable.Empty<Book>();
        public IEnumerable<Genre> Genres { get; set; } = Enumerable.Empty<Genre>();
        public string STerm { get; set; } = string.Empty;
        public int GenreId { get; set; }
    }
}
