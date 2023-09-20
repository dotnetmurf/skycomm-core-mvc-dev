using SkyCommNet7MVC.Domain.Models;

namespace SkyCommNet7MVC.Presentation.ViewModels.Offices
{
    public class OfficeDetailsViewModel
    {
        public Office SelectedOffice { get; set; }
        public string PageTitle { get; set; }
        public string ReturnUrl { get; set; }
    }
}
