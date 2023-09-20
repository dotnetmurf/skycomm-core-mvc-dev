using Microsoft.AspNetCore.Mvc.Rendering;
using SkyCommNet7MVC.Domain.Models;

namespace SkyCommNet7MVC.Presentation.ViewModels.Airports
{
    public class AirportCreateViewModel
    {
        public Airport Airport { get; set; }
        public string PageTitle { get; set; }
        public SelectList AirportTypesSelectList { get; set; }
        public SelectList RegionsSelectList { get; set; }
        public SelectList SkyCommSelectList { get; set; }
        public string ReturnUrl { get; set; }
    }
}
