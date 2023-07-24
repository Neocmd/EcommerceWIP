using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    public class BookController : Controller
    {
        private IBookRepository _bookRepository;

        public IActionResult Index()
        {
            return View();
        }
    }
}
