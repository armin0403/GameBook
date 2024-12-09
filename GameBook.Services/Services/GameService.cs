using GameBook.Core.Models;
using GameBook.Core.Pagination;
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

        public async Task<PagedList<Game>> GetPagedGames(int pageNumber, int pageSize, string sortBy = "Name", bool ascending = true, string searchTerm = "")
        {
            var games = await _unitOfWork.GameRepository.GetAllAsync();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                games = games.Where(g => g.Name.ToLower().Contains(searchTerm));
            }

            games = sortBy.ToLower() switch
            {
                "name" => ascending ? games.OrderBy(g => g.Name) : games.OrderByDescending(g => g.Name),
                "trophies" => ascending ? games.OrderBy(g => g.Trophies) : games.OrderByDescending(g => g.Trophies),
                "maxTrophies" => ascending ? games.OrderBy(g => g.MaxTrophies) : games.OrderByDescending(g => g.MaxTrophies),
                "progression" => ascending ? games.OrderBy(g => g.Progression) : games.OrderByDescending(g => g.Progression),
                _ => games
             };

            var pagedGames = _paginationService.CreatePagedList(games, pageNumber, pageSize);

            return pagedGames;
        }        
    }
}
