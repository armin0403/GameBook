using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using GameBook.Web.ViewModels;

namespace GameBook.Controllers
{
    public class UserController : Controller
    {
        public IActionResult LogIn()
        {
            var currentCulture = CultureInfo.CurrentCulture.Name;
            ViewData["SelectedLanguage"] = currentCulture == "en-US" ? "English" : "Bosnian";
            ViewData["SelectedFlag"] = currentCulture == "en-US" ? "us" : "ba";
            return View("LogIn");
        }

        public IActionResult Register() 
        {
            var currentCulture = CultureInfo.CurrentCulture.Name;
            ViewData["SelectedLanguage"] = currentCulture == "en-US" ? "English" : "Bosnian";
            ViewData["SelectedFlag"] = currentCulture == "en-US" ? "us" : "ba";
            return View("Register");
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
