using Microsoft.AspNetCore.Mvc.Rendering;
using SkyCommNet7MVC.Domain.Models;
using SkyCommNet7MVC.Services.Services;

namespace SkyCommNet7MVC.Presentation.ViewModels.Units
{
    public class UnitsFilterViewModel
    {
        public PaginationService<Unit> Units { get; set; }
        public SelectList AirportsSelectList { get; set; }
        public SelectList UnitModelsSelectList { get; set; }
        public int RecordCount { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string PageAction { get; set; }
        public int? FilterAirport { get; set; }
        public int? FilterUnitModel { get; set; }
        public string PageTitle { get; set; }
        public bool HasRecords { get; set; }
    }
}
