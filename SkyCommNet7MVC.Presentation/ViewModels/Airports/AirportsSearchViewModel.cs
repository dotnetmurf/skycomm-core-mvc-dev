using Microsoft.AspNetCore.Mvc.Rendering;
using SkyCommNet7MVC.Domain.Models;
using SkyCommNet7MVC.Services.Services;

namespace SkyCommNet7MVC.Presentation.ViewModels.Airports
{
    public class AirportsSearchViewModel
    {
        public PaginationService<Airport> Airports { get; set; }
        public string SearchString { get; set; }
        public string SearchName { get; set; }
        public string SearchType { get; set; }
        public SelectList SearchTypeSelectList { get; set; }
        public SelectList SearchAirportFieldsSelectList { get; set; }
        public int RecordCount { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string PageAction { get; set; }
        public string PageTitle { get; set; }
        public bool HasRecords { get; set; }
    }
}
