using Microsoft.AspNetCore.Mvc.Rendering;
using SkyCommNet7MVC.Domain.Models;
using SkyCommNet7MVC.Services.Services;

namespace SkyCommNet7MVC.Presentation.ViewModels.Personnel
{
    public class EmployeesFilterViewModel
    {
        public PaginationService<Employee> Employees { get; set; }
        public SelectList JobTitlesSelectList { get; set; }
        public SelectList DepartmentsSelectList { get; set; }
        public SelectList OfficesSelectList { get; set; }
        public int RecordCount { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string PageAction { get; set; }
        public int? FilterJobTitle { get; set; }
        public int? FilterDepartment { get; set; }
        public int? FilterOffice { get; set; }
        public string PageTitle { get; set; }
        public bool HasRecords { get; set; }
    }
}
