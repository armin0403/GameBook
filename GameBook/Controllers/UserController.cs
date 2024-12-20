using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using GameBook.Web.ViewModels;
using GameBook.Helpers.DropdownHelper;
using FluentValidation;
using FluentValidation.Results;
using MapsterMapper;
using GameBook.Core.Models;
using GameBook.Services.Services;
using GameBook.Helpers.PasswordHelper;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GameBook.Controllers
{
    public class UserController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly IValidator<RegisterViewModel> _validator;
        private readonly IValidator<LogInViewModel> _validator1;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IPhotoService _photoService;

        public UserController(IDropdownService dropdownService,
                              IValidator<RegisterViewModel> validator,
                              IValidator<LogInViewModel> validator1,
                              IMapper mapper,
                              IUserService userService,
                              IPhotoService photoService) 
        {
            _dropdownService = dropdownService;
            _validator = validator;
            _validator1 = validator1;
            _mapper = mapper;
            _userService = userService;
            _photoService = photoService;
        }
        public async Task<IActionResult> LogIn()
        {
            var currentCulture = CultureInfo.CurrentCulture.Name;
            ViewData["SelectedLanguage"] = currentCulture == "en-US" ? "English" : "Bosnian";
            ViewData["SelectedFlag"] = currentCulture == "en-US" ? "us" : "ba";
            return View("Login");            
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LogInViewModel viewModel)
        {
            ModelState.Clear();
            ValidationResult result = await _validator1.ValidateAsync(viewModel);
            if (!result.IsValid)
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(viewModel);
            }

            var user = await _userService.GetByUsernameOrEmail(viewModel.UsernameOrEmail);
            if(user == null)
            {
                ModelState.AddModelError(string.Empty, "Username/email or password incorrect!");
                return View(viewModel);
            }

            var passwordHash = PasswordHelper.CreateHash(viewModel.Password, user.PasswordSalt);
            if(passwordHash != user.PasswordHash)
            {
                ModelState.AddModelError(string.Empty, "Username/email or password incorrect!");
                return View(viewModel);
            }
            
            return RedirectToAction("Home", "Index");
        }

        public async Task<IActionResult> Register() 
        {
            var currentCulture = CultureInfo.CurrentCulture.Name;
            ViewData["SelectedLanguage"] = currentCulture == "en-US" ? "English" : "Bosnian";
            ViewData["SelectedFlag"] = currentCulture == "en-US" ? "us" : "ba";
            
            var countryDropdown = (await _dropdownService.GetCountryDropdownList(null)).Take(5);
            var viewModel = new RegisterViewModel
            {
                CountryDropdown = countryDropdown
            };
            return View("Register", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Register(IFormFile photoUpload, RegisterViewModel viewModel)
        {
            ModelState.Clear();
            ValidationResult result = await _validator.ValidateAsync(viewModel);
            if(!result.IsValid)
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                var countryDropdown = (await _dropdownService.GetCountryDropdownList(null)).Take(5);
                viewModel.CountryDropdown = countryDropdown;
                             
                var currentCulture = CultureInfo.CurrentCulture.Name;
                ViewData["SelectedLanguage"] = currentCulture == "en-US" ? "English" : "Bosnian";
                ViewData["SelectedFlag"] = currentCulture == "en-US" ? "us" : "ba";

                return View(viewModel);
            }

            try
            {
                await _photoService.AddPhotoAsync(photoUpload, viewModel, "uploads/user");
            }
            catch (Exception ex)
            {
                return View(viewModel);
            }

            var passwordSalt = PasswordHelper.CreateSalt();
            var passwordHash = PasswordHelper.CreateHash(viewModel.Password, passwordSalt);
            var newUser = _mapper.Map<User>(viewModel);

            newUser.PasswordSalt = passwordSalt;
            newUser.PasswordHash = passwordHash;
                        
            await _userService.AddNewUserAsync(newUser);
            return RedirectToAction("Index", "Home");
        }

        

        public async Task<IActionResult> GetCountryDropdown(string? searchTerm)
        {
            var countries = await _dropdownService.GetCountryDropdownList(searchTerm);
            var results = countries.Select(countries => new { id = countries.Value, text = countries.Text });
            return Json(new {results});
        }
    }
}
