using GameBook.Services.Services;
using GameBook.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using GameBook.Web.ViewModels;
using MapsterMapper;
using GameBook.Core.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using GameBook.Helpers.ToastHelper;
using GameBook.Helpers.DropdownHelper;
using Microsoft.AspNetCore.Mvc.Diagnostics;

namespace GameBook.Controllers
{
    public class GamesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaginationService _paginationService;
        private readonly IGameService _gameService;
        private readonly IMapper _mapper;
        private readonly IToastService _toast;
        private readonly IDropdownService _dropdownService;
        private readonly IPhotoService _photoService;

        public GamesController(IUnitOfWork unitOfWork,
                               IPaginationService paginationService,
                               IGameService gameService,
                               IMapper mapper,
                               IToastService toastService,
                               IDropdownService dropdownService,
                               IPhotoService photoService) 
        {
            _unitOfWork = unitOfWork;
            _paginationService = paginationService;
            _gameService = gameService;
            _mapper = mapper;
            _toast = toastService;
            _dropdownService = dropdownService;
            _photoService = photoService;
        }
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 5, string sortBy = "Name", bool ascending = true, string searchTerm ="")
        {
            var pagedGames = await _gameService.GetPagedGames(pageNumber, pageSize, sortBy, ascending, searchTerm);

            ViewData["SortBy"] = sortBy;
            ViewData["Ascending"] = ascending;
            ViewData["SearchTerm"] = searchTerm;
            return View(pagedGames);
        }

        public async Task<IActionResult> Search (int pageNumber = 1, int pageSize = 5, string sortBy = "Name", bool ascending = true, string searchTerm = "")
        {
            return RedirectToAction("Index", new {pageNumber, pageSize, sortBy, ascending, searchTerm});
        }

        public async Task<IActionResult> AddGame()
        {
            var gameGenreDropdown = (await _dropdownService.GetGenreDropdownList(null, null)).Take(5);
            var gamePlatformDropdown = await _dropdownService.GetPlatformSelectList();
            var viewModel = new GameViewModel
            {
                GameGenres = gameGenreDropdown,
                GamePlatforms = gamePlatformDropdown
            };
            return View("AddGame", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddGame(IFormFile photoUpload, GameViewModel model)
        {
            ModelState.Clear();

            try
            {
                await _photoService.AddPhotoAsync(photoUpload, model, "uploads/games");
            }
            catch
            {
                return View(model);
            }

            var game = _mapper.Map<Game>(model);


            game.GameGenres = model.SelectedGameGenres.Select(genreId => new GameGenre { GenreId = genreId }).ToList();
            game.GamePlatforms = model.SelectedGamePlatforms.Select(platformId => new GamePlatform { PlatformId = platformId }).ToList();
            try
            {
                await _gameService.AddGame(game);
            }
            catch
            {
                return View(model);
            }

            _toast.Success("Uspješno dodano!");
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var game = await _unitOfWork.GameRepository.FindByIdAsync(id);
            if(game == null) return View("Error");

            var gameVM = _mapper.Map<GameViewModel>(game);

            return View("EditGame", gameVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id,  GameViewModel gameVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit a game");
                return View(gameVM);
            }
            var editGame = await _unitOfWork.GameRepository.FindByIdAsync(id);
            _mapper.Map(gameVM, editGame);

            await _unitOfWork.GameRepository.UpdateAsync(editGame);

            _toast.Warning("Uspješno editovano!");
            return RedirectToAction("Index");
        }       

        public async Task<IActionResult> Info(int id)
        {
            var game = await _unitOfWork.GameRepository.FindByIdAsync(id);
            if (game == null) return View("Error");

            var gameVM = _mapper.Map<GameViewModel>(game);
            if (gameVM == null) return View("Error");
            return View("InfoGame", gameVM);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var game = await _unitOfWork.GameRepository.FindByIdAsync(id);
            if (game == null) return View("Error");

            _unitOfWork.GameRepository.Delete(game);

            _toast.Danger("Uspješno izbrisano!");
            return RedirectToAction("Index");
        }      
        
        public async Task<IActionResult> GetGenreDropdown(string? searchTerm)
        {
            var genres = await _dropdownService.GetGenreDropdownList(searchTerm, null);
            var results = genres.Select(genres => new { id = genres.Value, text = genres.Text });
            return Json(new {results});
        }

        public async Task<IActionResult> GetPlatformDropdown()
        {
            var platforms = await _dropdownService.GetPlatformSelectList();
            var results = platforms.Select(platforms => new {id = platforms.Value, text = platforms.Text});
            return Json(new {results});
        }
    }
}
