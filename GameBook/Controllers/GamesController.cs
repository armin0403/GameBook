using GameBook.Services.Services;
using GameBook.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using GameBook.Web.ViewModels;
using MapsterMapper;
using GameBook.Core.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GameBook.Controllers
{
    public class GamesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaginationService _paginationService;
        private readonly IGameService _gameService;
        private readonly IMapper _mapper;

        public GamesController(IUnitOfWork unitOfWork,
                               IPaginationService paginationService,
                               IGameService gameService,
                               IMapper mapper) 
        {
            _unitOfWork = unitOfWork;
            _paginationService = paginationService;
            _gameService = gameService;
            _mapper = mapper;
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

        public IActionResult AddGame()
        {
            return View("AddGame");
        }

        [HttpPost]
        public async Task<IActionResult> AddGame(GameViewModel model)
        {
            var game = _mapper.Map<Game>(model);

            await _gameService.AddGame(game);
            await _unitOfWork.SaveChangesAsync();

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
            return RedirectToAction("Index");
        }        
    }
}
