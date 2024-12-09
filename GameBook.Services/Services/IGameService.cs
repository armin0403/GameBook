using GameBook.Core.Models;
using GameBook.Core.Pagination;

namespace GameBook.Services.Services
{
    public interface IGameService
    {
        Task<PagedList<Game>> GetPagedGames (int pageNumber, int  pageSize, string sortBy = null, bool ascending = true, string searchTerm = "");
        Task<Game> AddGame (Game game);
    }
}
