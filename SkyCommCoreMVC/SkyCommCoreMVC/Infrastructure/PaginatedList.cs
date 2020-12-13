using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkyCommCoreMVC.Infrastructure
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int FirstPage { get; private set; }
        public int LastPage { get; private set; }
        public string PageAction { get; private set; }

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize, string pageAction)
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

        //public List<SelectListItem> GetRecordsPerPage()
        //{
        //    List<SelectListItem> recordsPerPage = new List<SelectListItem>()
        //    {
        //        new SelectListItem(){Text = "5", Value = "5"},
        //        new SelectListItem(){Text = "10", Value = "10"},
        //        new SelectListItem(){Text = "25", Value = "25"},
        //        new SelectListItem(){Text = "50", Value = "50"}
        //    };
        //    return recordsPerPage;

        //}

        public List<int> GetListRecordsPerPage()
        {
            List<int> recordsPerPage = new List<int>() { 5, 10, 25, 50 };
            return recordsPerPage;

        }

        public List<int> GetCardsRecordsPerPage()
        {
            List<int> recordsPerPage = new List<int>() { 12, 24, 48, 72 };
            return recordsPerPage;

        }

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize, string pageAction)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, count, pageIndex, pageSize, pageAction);
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

//string firstDisabled = Model.IsFirstPage ? "disabled" : "";
//string prevDisabled = !Model.HasPreviousPage ? "disabled" : "";
//string nextDisabled = !Model.HasNextPage ? "disabled" : "";
//string lastDisabled = Model.IsLastPage ? "disabled" : "";
