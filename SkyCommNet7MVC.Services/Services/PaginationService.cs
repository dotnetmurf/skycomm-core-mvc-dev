using Microsoft.EntityFrameworkCore;

namespace SkyCommNet7MVC.Services.Services
{
    public class PaginationService<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int FirstPage { get; private set; }
        public int LastPage { get; private set; }
        public string PageAction { get; private set; }

        public PaginationService(List<T> items, int count, int pageIndex, int pageSize, string pageAction)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            FirstPage = 1;
            LastPage = TotalPages;
            PageAction = pageAction;

            this.AddRange(items);
        }

        public bool IsFirstPage
        {
            get
            {
                return (PageIndex == 1);
            }
        }

        public bool IsLastPage
        {
            get
            {
                return (PageIndex == TotalPages);
            }
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }

        public List<int> Get5To50RecordsPerPageList()
        {
            List<int> recordsPerPage = new List<int>() { 5, 10, 25, 50 };
            return recordsPerPage;
        }

        public List<int> Get12To72RecordsPerPageList()
        {
            List<int> recordsPerPage = new List<int>() { 12, 24, 48, 72 };
            return recordsPerPage;
        }

        public static async Task<PaginationService<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize, string pageAction)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginationService<T>(items, count, pageIndex, pageSize, pageAction);
        }

        public string FirstDisabled()
        {
            return IsFirstPage ? "disabled" : "";
        }

        public string PreviousDisabled()
        {
            return !HasPreviousPage ? "disabled" : "";
        }

        public string NextDisabled()
        {
            return !HasNextPage ? "disabled" : "";
        }

        public string LastDisabled()
        {
            return IsLastPage ? "disabled" : "";
        }
    }
}
