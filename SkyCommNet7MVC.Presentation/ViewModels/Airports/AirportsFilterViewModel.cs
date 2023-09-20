using Microsoft.AspNetCore.Mvc.Rendering;
using SkyCommNet7MVC.Domain.Models;
using SkyCommNet7MVC.Services.Services;

namespace SkyCommNet7MVC.Presentation.ViewModels.Airports
{
    public class AirportsFilterViewModel
    {
        public PaginationService<Airport> Airports { get; set; }
        public SelectList CountriesSelectList { get; set; }
        public SelectList AirportTypesSelectList { get; set; }
        public SelectList SkyCommSelectList { get; set; }
        public int RecordCount { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string PageAction { get; set; }
        public int? FilterCountry { get; set; }
        public int? FilterAirportType { get; set; }
        public int? FilterSkyComm { get; set; }
        public string PageTitle { get; set; }
        public bool HasRecords { get; set; }
    }
}
