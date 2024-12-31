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
using GameBook.Helpers.SessionHelper;
using GameBook.Helpers.ToastHelper;
using Microsoft.Extensions.Localization;
using GameBook.Resources;
using GameBook.Infrastructure;

namespace GameBook.Controllers
{
    public class UserController : Controller
    {
        private readonly GameBookDbContext _dbContext;
        private readonly IDropdownService _dropdownService;
        private readonly IValidator<RegisterViewModel> _validator;
        private readonly IValidator<LogInViewModel> _validator1;
        private readonly IValidator<UserViewModel> _validator2;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly IUserService _userService;
        private readonly IPhotoService _photoService;
        private readonly IToastService _toastService;
        private readonly IStringLocalizer<Resource> _localizer;

        public UserController(GameBookDbContext dbContext,
                              IDropdownService dropdownService,
                              IValidator<RegisterViewModel> validator,
                              IValidator<LogInViewModel> validator1,
                              IValidator<UserViewModel> validator2,
                              IMapper mapper,
                              ISessionService sessionService,
                              IUserService userService,
                              IPhotoService photoService,
                              IToastService toastService,
                              IStringLocalizer<Resources.Resource> localizer) 
        {
            _dbContext = dbContext;
            _dropdownService = dropdownService;
            _validator = validator;
            _validator1 = validator1;
            _validator2 = validator2;
            _mapper = mapper;
            _sessionService = sessionService;
            _userService = userService;
            _photoService = photoService;
            _toastService = toastService;
            _localizer = localizer;
        }
        public async Task<IActionResult> LogIn()
        {
            var currentCulture = CultureInfo.CurrentCulture.Name;
            ViewData["SelectedLanguage"] = currentCulture == "en-US" ? "English" : "Bosnian";
            ViewData["SelectedFlag"] = currentCulture == "en-US" ? "us" : "ba";
            return View("Login");            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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

            var sessionData = new SessionUser
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email
            };

            _sessionService.Set("UserData", sessionData);
            
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Register() 
        {
            var currentCulture = CultureInfo.CurrentCulture.Name;
            ViewData["SelectedLanguage"] = currentCulture == "en-US" ? "English" : "Bosnian";
            ViewData["SelectedFlag"] = currentCulture == "en-US" ? "us" : "ba";
            
            var countryDropdown = (await _dropdownService.GetCountryDropdownList(null, null)).Take(5);
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
                var countryDropdown = (await _dropdownService.GetCountryDropdownList(null, null)).Take(5);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("LogIn");
        }
        

        public async Task<IActionResult> GetCountryDropdown(string? searchTerm)
        {
            var countries = await _dropdownService.GetCountryDropdownList(searchTerm, null);
            var results = countries.Select(countries => new { id = countries.Value, text = countries.Text });
            return Json(new {results});
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, UserViewModel model)
        {
            ModelState.Clear();
            ValidationResult result = await _validator2.ValidateAsync(model);
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                _toastService.Danger(_localizer["NotSaved"]);
                return RedirectToAction("Index", "Profile");
            }

            var editUser = _mapper.Map<User>(model);
            await _userService.UpdateUserAsync(editUser);

            _toastService.Success(_localizer["ChangesSaved"]);
            return RedirectToAction("Index", "Profile");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if(user == null)
            {
                _toastService.Danger("User not found!");
                return RedirectToAction("Index", "Profile");
            }

            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    await _userService.DeleteUserAsync(user);
                    await transaction.CommitAsync();

                    return RedirectToAction("Login");
                }
                catch
                {
                    await transaction.RollbackAsync();

                    _toastService.Danger("User not deleted!");
                    return RedirectToAction("Index", "Profile");
                }
            }
        }
    }
}
