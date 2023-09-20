using SkyCommNet7MVC.Domain.Models;
using SkyCommNet7MVC.Presentation.ViewModels.Personnel;
using System.Linq.Expressions;

namespace SkyCommNet7MVC.Presentation.Interfaces
{
    public interface IPersonnelControllerService
    {
        public IOrderedQueryable<Employee> GetAllEmployees();
        public Employee GetEmployeeById(int id);
        public IQueryable<Employee> GetEmployeesWhere(Expression<Func<Employee, bool>> filter);
        public IEnumerable<Department> GetDepartmentsSelectList();
        public IEnumerable<JobTitle> GetJobTitlesSelectList();
        public IEnumerable<Office> GetOfficesSelectList();
        public Expression<Func<Employee, bool>> BuildFilterExpression
            (bool isJobTitleFiltered, int? filterJobTitle,
            bool isDepartmentFiltered, int? filterDepartment,
            bool isOfficeFiltered, int? filterOffice);
        public Expression<Func<Employee, bool>> BuildSearchExpression
            (string searchString, string searchType, bool isFirstNameSearch, bool isLastNameSearch);
        public string BuildEmailUri
            (string eMail);
        public Task<EmployeesFilterViewModel> BuildEmployeesFilterViewModel
            (int? filterJobTitle, int? filterDepartment, int? filterOffice, int? pageNumber, int? pageSize);
        public Task<EmployeesSearchViewModel> BuildEmployeesSearchViewModel
            (string searchString, string searchName, string? searchType, int? pageNumber, int? pageSize);
        public Task<EmployeeDetailsViewModel> BuildEmployeeDetailsViewModel(int id, string returnUrl);
        public bool EmployeeExists(int id);
    }
}
