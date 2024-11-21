using GameBook.Core.Models;
using GameBook.Core.Pagination;
using GameBook.Core.ViewModels;

namespace GameBook.Services.Services
{
    public interface IGameService
    {
        Task<PagedList<GameViewModel>> GetPagedGames (int pageNumber, int  pageSize, string sortBy = null, bool ascending = true);
        Task<PagedList<GameViewModel>> GetSearchedGames(int pageNumber, int pageSize, string sortBy = null, bool ascending = true, string searchTerm = "");
        Task<Game> AddGame (Game game);
    }
}
