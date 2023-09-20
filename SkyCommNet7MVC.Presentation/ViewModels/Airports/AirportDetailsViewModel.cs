using SkyCommNet7MVC.Domain.Models;

namespace SkyCommNet7MVC.Presentation.ViewModels.Airports
{
    public class AirportDetailsViewModel
    {
        public Airport SelectedAirport { get; set; }
        public string PageTitle { get; set; }
        public string SatUri { get; set; }
        public string MapUri { get; set; }
        public string ReturnUrl { get; set; }
    }
}
