using Microsoft.AspNetCore.Mvc;

namespace GameBook.Controllers
{
    public class PostController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddPost ()
        {
            return View();
        }

        public IActionResult EditPost ()
        {
            return View();
        }

        public IActionResult DeletePost ()
        {
            return View();
        }
    }
}
    