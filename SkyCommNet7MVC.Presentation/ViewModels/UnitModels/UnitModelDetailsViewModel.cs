using SkyCommNet7MVC.Domain.Models;

namespace SkyCommNet7MVC.Presentation.ViewModels.UnitModels
{
    public class UnitModelDetailsViewModel
    {
        public UnitModel SelectedUnitModel { get; set; }
        public string PageTitle { get; set; }
        public string ImagePath { get; set; }
        public string LogoPath { get; set; }
        public string ReturnUrl { get; set; }
    }
}
