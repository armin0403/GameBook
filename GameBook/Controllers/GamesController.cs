using GameBook.Services.Services;
using GameBook.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using GameBook.Core.Pagination;
using GameBook.Core.ViewModels;
using MapsterMapper;
using GameBook.Core.Models;

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
            var pagedGames = string.IsNullOrEmpty(searchTerm)
                ? await _gameService.GetPagedGames(pageNumber, pageSize, sortBy, ascending)
                : await _gameService.GetSearchedGames(pageNumber, pageSize, sortBy, ascending, searchTerm);

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
    }
}
