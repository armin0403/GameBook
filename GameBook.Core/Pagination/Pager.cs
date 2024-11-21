namespace GameBook.Core.Pagination
{
    public class Pager
    {
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }

        public Pager(int totalItems, int pageNumber = 1, int pageSize = 10)
        {
            TotalItems = totalItems;
            TotalPages = (int)Math.Ceiling(totalItems / (decimal)pageSize);
            CurrentPage = pageNumber;
            StartPage = CurrentPage - 5;
            EndPage = CurrentPage + 4;
            PageSize = pageSize;

            if (StartPage <= 0)
            {
                EndPage -= StartPage - 1;
                StartPage = 1;
            }
            if (EndPage > TotalPages)
            {
                EndPage = TotalPages;
                if (EndPage > 10)
                {
                    StartPage = TotalPages - 9;
                }
            }
        }


    }
}
