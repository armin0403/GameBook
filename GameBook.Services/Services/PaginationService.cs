using GameBook.Core.Pagination;

namespace GameBook.Services.Services
{
    public class PaginationService : IPaginationService
    {
        public Pager CalculatePager(int totalItems, int pageNumber, int pageSize)
        {
            return new Pager(totalItems, pageNumber, pageSize); 
        }

        public PagedList<T> CreatePagedList<T>(IQueryable<T> source, int pageNumber, int pageSize)
        {
            int totalCount = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            var pager = CalculatePager(totalCount, pageNumber, pageSize);

            return new PagedList<T>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                PageCount = pager.TotalPages,
                TotalCount = totalCount,
                HasPreviousPage = pageNumber > 1,
                HasNextPage = pageNumber < pager.TotalPages,
                IsFirstPage = pageNumber == 1,
                IsLastPage = pageNumber == pager.TotalPages,
                Pager = pager,
                Items = items
            };
        }
    }
}
