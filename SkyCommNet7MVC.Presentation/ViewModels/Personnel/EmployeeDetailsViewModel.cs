using SkyCommNet7MVC.Domain.Models;

namespace SkyCommNet7MVC.Presentation.ViewModels.Personnel
{
    public class EmployeeDetailsViewModel
    {
        public Employee SelectedEmployee { get; set; }
        public string PageTitle { get; set; }
        public string EmailUri { get; set; }
        public string ReturnUrl { get; set; }
    }
}
