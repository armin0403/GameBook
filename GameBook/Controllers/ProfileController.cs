using GameBook.Services.Services;
using Microsoft.AspNetCore.Mvc;
using GameBook.AuthConfig;
using MapsterMapper;
using GameBook.Web.ViewModels;
using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using GameBook.Helpers.SessionHelper;
using GameBook.Core.Models;
using GameBook.Helpers.DropdownHelper;

namespace GameBook.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ISessionService _sessionService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IDropdownService _dropdownService;

        public ProfileController(IUserService userService,
                                 IMapper mapper,
                                 ISessionService sessionService,
                                 IDropdownService dropdownService)
        {
            _sessionService = sessionService;
            _userService = userService;
            _mapper = mapper;
            _dropdownService = dropdownService;
        }

        [AuthentificationFilter]
        public async Task<IActionResult> Index()
        {
            var user = _sessionService.Get<SessionUser>("UserData");
            var userRaw = await _userService.GetUserByIdAsync(user.Id);
            var userVM = _mapper.Map<UserViewModel>(userRaw);

            var countryDropdown = (await _dropdownService.GetCountryDropdownList(null, userVM.CountryId)).Take(5);
            {
                userVM.CountryDropdown = countryDropdown;
            };


            return View(userVM);
        }
    }
}
