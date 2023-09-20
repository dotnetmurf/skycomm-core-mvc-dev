using Microsoft.AspNetCore.Mvc.Rendering;
using SkyCommNet7MVC.Domain.Models;
using SkyCommNet7MVC.Services.Services;

namespace SkyCommNet7MVC.Presentation.ViewModels.Units
{
    public class UnitsSearchViewModel
    {
        public PaginationService<Unit> Units { get; set; }
        public string SearchString { get; set; }
        public string SearchNbr { get; set; }
        public string SearchType { get; set; }
        public SelectList SearchTypeSelectList { get; set; }
        public SelectList SearchUnitFieldsSelectList { get; set; }
        public int RecordCount { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string PageAction { get; set; }
        public string PageTitle { get; set; }
        public bool HasRecords { get; set; }
    }
}
