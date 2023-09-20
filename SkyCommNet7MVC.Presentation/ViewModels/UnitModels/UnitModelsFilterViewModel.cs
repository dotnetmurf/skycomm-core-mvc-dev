using Microsoft.AspNetCore.Mvc.Rendering;
using SkyCommNet7MVC.Domain.Models;
using SkyCommNet7MVC.Services.Services;

namespace SkyCommNet7MVC.Presentation.ViewModels.UnitModels
{
    public class UnitModelsFilterViewModel
    {
        public PaginationService<UnitModel> UnitModels { get; set; }
        public SelectList ModelCategoriesSelectList { get; set; }
        public SelectList ModelFreqBandsSelectList { get; set; }
        public SelectList ModelManufacturersSelectList { get; set; }
        public int RecordCount { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string PageAction { get; set; }
        public int? FilterCategory { get; set; }
        public int? FilterFreqBand { get; set; }
        public int? FilterManufacturer { get; set; }
        public string PageTitle { get; set; }
        public bool HasRecords { get; set; }
    }
}
