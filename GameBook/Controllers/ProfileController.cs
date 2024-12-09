using Microsoft.AspNetCore.Mvc;

namespace GameBook.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
