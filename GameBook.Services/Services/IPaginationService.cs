using GameBook.Core.Pagination;

namespace GameBook.Services.Services
{
    public interface IPaginationService
    {
        PagedList<T> CreatePagedList<T>(IQueryable<T> source, int pageNumber, int pageSize);
        Pager CalculatePager(int TotalItems, int pageNumber, int pageSize);
    }
}
