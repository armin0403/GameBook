using System.Diagnostics;
using GameBook.AuthConfig;
using GameBook.Models;
using GameBook.Services.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace GameBook.Controllers
{
    [AuthentificationFilter]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;
        private readonly IGameService _gamesService;

        public HomeController(ILogger<HomeController> logger,
                              IUserService userService,
                              IGameService gameService)
        {
            _logger = logger;
            _userService = userService;
            _gamesService = gameService;
        }

        public IActionResult Index()
        {
            //await _userService.GetLastFive();
            //await _gamesService.GetLastFive();
            return View();
        }

        public IActionResult ChangeLanguage(string culture)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions() { Expires = DateTimeOffset.UtcNow.AddYears(1) });
                
            return Redirect(Request.Headers["Referer"].ToString());
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
