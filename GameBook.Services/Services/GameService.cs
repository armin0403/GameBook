using GameBook.Core.Models;
using GameBook.Core.Pagination;
using GameBook.Core.ViewModels;
using GameBook.Infrastructure.UnitOfWork;
using MapsterMapper;

namespace GameBook.Services.Services
{
    public class GameService : IGameService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaginationService _paginationService;
        private readonly IMapper _mapper;

        public GameService(IUnitOfWork unitOfWork,
                           IPaginationService paginationService,
                           IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _paginationService = paginationService;
            _mapper = mapper;
        }

        public async Task<Game> AddGame(Game game)
        {
            _mapper.Map<Game>(game);
            await _unitOfWork.GameRepository.AddAsync(game);

            return game;
        }

        public async Task<PagedList<GameViewModel>> GetPagedGames(int pageNumber, int pageSize, string sortBy = "Name", bool ascending = true)
        {
            var games = await _unitOfWork.GameRepository.GetAllAsync();

            games = sortBy.ToLower() switch
            {
                "name" => ascending ? games.OrderBy(g => g.Name) : games.OrderByDescending(g => g.Name),
                "trophies" => ascending ? games.OrderBy(g => g.Trophies) : games.OrderByDescending(g => g.Trophies),
                "maxTrophies" => ascending ? games.OrderBy(g => g.MaxTrophies) : games.OrderByDescending(g => g.MaxTrophies),
                "progression" => ascending ? games.OrderBy(g => g.Progression) : games.OrderByDescending(g => g.Progression),
                _ => games
             };

            var gamesViewModels = games.Select(game => _mapper.Map<GameViewModel>(game)).AsQueryable();

            var pagedGames = _paginationService.CreatePagedList(gamesViewModels, pageNumber, pageSize);

            return pagedGames;
        }

        public async Task<PagedList<GameViewModel>> GetSearchedGames(int pageNumber, int pageSize, string sortBy = null, bool ascending = true, string searchTerm = null)
        {
            var games = await _unitOfWork.GameRepository.GetAllAsync();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                games = games.Where(g => g.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
            }

            games = sortBy.ToLower() switch
            {
                "name" => ascending ? games.OrderBy(g => g.Name) : games.OrderByDescending(g => g.Name),
                "trophies" => ascending ? games.OrderBy(g => g.Trophies) : games.OrderByDescending(g => g.Trophies),
                "maxTrophies" => ascending ? games.OrderBy(g => g.MaxTrophies) : games.OrderByDescending(g => g.MaxTrophies),
                "progression" => ascending ? games.OrderBy(g => g.Progression) : games.OrderByDescending(g => g.Progression),
                _ => games
            };

            var searchedGamesVM = games.Select(game => _mapper.Map<GameViewModel>(game)).AsQueryable();
            var pagedSearchedGames = _paginationService.CreatePagedList(searchedGamesVM, pageNumber, pageSize);

            return pagedSearchedGames;
        }
    }
}
