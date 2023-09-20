using SkyCommNet7MVC.Domain.Models;

namespace SkyCommNet7MVC.Presentation.ViewModels.Units
{
    public class UnitDetailsViewModel
    {
        public Unit SelectedUnit { get; set; }
        public string PageTitle { get; set; }
        public string ImagePath { get; set; }
        public string ReturnUrl { get; set; }
    }
}
