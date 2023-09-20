using Microsoft.AspNetCore.Mvc.Rendering;
using SkyCommNet7MVC.Domain.Models;
using SkyCommNet7MVC.Presentation.Interfaces;
using SkyCommNet7MVC.Presentation.ViewModels.Personnel;
using SkyCommNet7MVC.Services.Interfaces;
using SkyCommNet7MVC.Services.SelectLists;
using SkyCommNet7MVC.Services.Services;
using System.Linq.Expressions;
using System.Text;

namespace SkyCommNet7MVC.Presentation.Services
{
    public class PersonnelControllerService : IPersonnelControllerService
    {
        private readonly IDepartmentService _departmentService;
        private readonly IEmployeeService _employeeService;
        private readonly IJobTitleService _jobTitleService;
        private readonly IOfficeService _officeService;

        public PersonnelControllerService
            (IDepartmentService departmentService,
            IEmployeeService employeeService,
            IJobTitleService jobTitleService,
            IOfficeService officeService)
        {
            _departmentService = departmentService;
            _employeeService = employeeService;
            _jobTitleService = jobTitleService;
            _officeService = officeService;
        }

        public IOrderedQueryable<Employee> GetAllEmployees()
        {
            return _employeeService.GetAllEmployees();
        }

        public Employee GetEmployeeById(int id)
        {
            return _employeeService.GetEmployeeById(id);
        }

        public IQueryable<Employee> GetEmployeesWhere(Expression<Func<Employee, bool>> filter)
        {
            return _employeeService.GetEmployeesWhere(filter);
        }

        public IEnumerable<Department> GetDepartmentsSelectList()
        {
            return _departmentService.GetDepartmentsSelectList();
        }

        public IEnumerable<JobTitle> GetJobTitlesSelectList()
        {
            return _jobTitleService.GetJobTitlesSelectList();
        }

        public IEnumerable<Office> GetOfficesSelectList()
        {
            return _officeService.GetOfficesSelectList();
        }

        public async Task<EmployeesFilterViewModel> BuildEmployeesFilterViewModel
            (int? filterJobTitle, int? filterDepartment, int? filterOffice, int? pageNumber, int? pageSize)
        {
            IQueryable<Employee> filteredEmployees;

            string pageAction = "Filter";

            var jobTitlesSelectList = GetJobTitlesSelectList();
            var departmentsSelectList = GetDepartmentsSelectList();
            var officesSelectList = GetOfficesSelectList();

            if (filterJobTitle == null) { filterJobTitle = 0; }
            if (filterDepartment == null) { filterDepartment = 0; }
            if (filterOffice == null) { filterOffice = 0; }

            bool isFilterSet = false;
            bool isJobTitleFiltered = false;
            bool isDepartmentFiltered = false;
            bool isOfficeFiltered = false;
            bool hasRecords = false;

            var jobTitlesSL = new SelectList
                (jobTitlesSelectList, "JobTitleId", "JobTitle1", filterJobTitle);
            var departmentsSL = new SelectList
                (departmentsSelectList, "DepartmentId", "DepartmentName", filterDepartment);
            var officesSL = new SelectList
                (officesSelectList, "OfficeId", "OfficeName", filterOffice);

            Expression<Func<Employee, bool>>? compositeFilter = null;

            string pageTitle = "";

            if (filterJobTitle > 0)
            {
                isJobTitleFiltered = true;
                isFilterSet = true;
            }

            if (filterDepartment > 0)
            {
                isDepartmentFiltered = true;
                isFilterSet = true;
            }

            if (filterOffice > 0)
            {
                isOfficeFiltered = true;
                isFilterSet = true;
            }

            if (isFilterSet)
            {
                compositeFilter = BuildFilterExpression
                    (isJobTitleFiltered, filterJobTitle,
                    isDepartmentFiltered, filterDepartment,
                    isOfficeFiltered, filterOffice);
            }

            if (compositeFilter != null)
            {
                filteredEmployees = GetEmployeesWhere(compositeFilter);
                pageTitle = "Employees Filter - Filtered";
            }
            else
            {
                filteredEmployees = GetAllEmployees();
                pageTitle = "Employees Filter";
            }

            int recordCount = filteredEmployees.Count();

            if (recordCount > 0) { hasRecords = true; }

            var pagedEmployees = await PaginationService<Employee>.CreateAsync
                (filteredEmployees, pageNumber ?? 1, pageSize ?? 12, pageAction);

            EmployeesFilterViewModel vm = new EmployeesFilterViewModel()
            {
                Employees = pagedEmployees,
                RecordCount = recordCount,
                JobTitlesSelectList = jobTitlesSL,
                DepartmentsSelectList = departmentsSL,
                OfficesSelectList = officesSL,
                PageNumber = pageNumber,
                PageSize = pageSize,
                PageAction = pageAction,
                PageTitle = pageTitle,
                FilterJobTitle = filterJobTitle,
                FilterDepartment = filterDepartment,
                FilterOffice = filterOffice,
                HasRecords = hasRecords
            };

            return vm;
        }

        public Expression<Func<Employee, bool>> BuildFilterExpression
            (bool isJobTitleFiltered, int? filterJobTitle,
            bool isDepartmentFiltered, int? filterDepartment,
            bool isOfficeFiltered, int? filterOffice)
        {
            Expression<Func<Employee, bool>> filterExpression;

            switch ((isJobTitleFiltered, isDepartmentFiltered, isOfficeFiltered))
            {
                case (true, false, false):
                    filterExpression = e => e.JobTitleId == filterJobTitle;
                    return filterExpression;

                case (false, true, false):
                    filterExpression = e => e.JobTitle.DepartmentId == filterDepartment;
                    return filterExpression;

                case (false, false, true):
                    filterExpression = e => e.OfficeId == filterOffice;
                    return filterExpression;

                case (true, true, false):
                    filterExpression = e => e.JobTitleId == filterJobTitle 
                    && e.JobTitle.DepartmentId == filterDepartment;
                    return filterExpression;

                case (true, false, true):
                    filterExpression = e => e.JobTitleId == filterJobTitle 
                    && e.OfficeId == filterOffice;
                    return filterExpression;

                case (false, true, true):
                    filterExpression = e => e.JobTitle.DepartmentId == filterDepartment 
                    && e.OfficeId == filterOffice;
                    return filterExpression;

                case (true, true, true):
                    filterExpression = e => e.JobTitleId == filterJobTitle 
                    && e.JobTitle.DepartmentId == filterDepartment 
                    && e.OfficeId == filterOffice;
                    return filterExpression;

                default:
                    return null;
            }
        }

        public async Task<EmployeesSearchViewModel> BuildEmployeesSearchViewModel
            (string searchString, string searchName, string? searchType, int? pageNumber, int? pageSize)
        {
            IQueryable<Employee> searchedEmployees;

            string pageAction = "Search";

            string pageTitle = "";

            bool hasRecords = false;

            bool isSearchSet = false;
            bool isFirstNameSearch = false;
            bool isLastNameSearch = false;

            var searchTypeSelectList = SearchTypeSelectList.GetSearchTypeSelectList();
            var searchEmployeeFieldsSelectList = SearchEmployeeFieldsSelectList.GetSearchEmployeeFieldsSelectList();

            if (searchType == null) { searchType = "Contains"; }

            var searchTypeSL = new SelectList
                (searchTypeSelectList, "Id", "TypeName", searchType);
            var searchEmployeeFieldsSL = new SelectList
                (searchEmployeeFieldsSelectList, "Id", "EmployeeFieldName", searchName);

            Expression<Func<Employee, bool>>? compositeFilter = null;

            if (!String.IsNullOrEmpty(searchString))
            {
                isSearchSet = true;
            }

            if (searchName == "First")
            {
                isFirstNameSearch = true;
            }

            if (searchName == "Last")
            {
                isLastNameSearch = true;
            }

            if (isSearchSet)
            {
                compositeFilter = BuildSearchExpression
                    (searchString, searchType, isFirstNameSearch, isLastNameSearch);
            }

            if (compositeFilter != null)
            {
                searchedEmployees = GetEmployeesWhere(compositeFilter);
                pageTitle = "Employees Search - Searched";
            }
            else
            {
                searchedEmployees = GetAllEmployees();
                pageTitle = "Employees Search";
            }

            var pagedEmployees = await PaginationService<Employee>.CreateAsync
                (searchedEmployees, pageNumber ?? 1, pageSize ?? 12, pageAction);

            int recordCount = searchedEmployees.Count();

            if (recordCount > 0) { hasRecords = true; }

            EmployeesSearchViewModel vm = new EmployeesSearchViewModel()
            {
                SearchString = searchString,
                SearchName = searchName,
                SearchType = searchType,
                SearchTypeSelectList = searchTypeSL,
                SearchEmployeeFieldsSelectList = searchEmployeeFieldsSL,
                PageNumber = pageNumber,
                PageSize = pageSize,
                PageAction = pageAction,
                PageTitle = pageTitle,
                HasRecords = hasRecords,
                Employees = pagedEmployees,
                RecordCount = recordCount,
            };

            return vm;
        }

        public Expression<Func<Employee, bool>> BuildSearchExpression
            (string searchString, string searchType, bool isFirstNameSearch, bool isLastNameSearch)
        {
            Expression<Func<Employee, bool>> searchExpression;

            switch (searchType)
            {
                case ("Contains"):
                    switch ((isFirstNameSearch, isLastNameSearch))
                    {
                        case (true, false):
                            searchExpression = e => e.FirstName.Contains(searchString);
                            return searchExpression;
                        case (false, true):
                            searchExpression = e => e.LastName.Contains(searchString);
                            return searchExpression;
                        case (false, false):
                            searchExpression = e => e.FirstName.Contains(searchString)
                               || e.LastName.Contains(searchString);
                            return searchExpression;
                        default:
                            return null;
                    }
                case ("StartsWith"):
                    switch ((isFirstNameSearch, isLastNameSearch))
                    {
                        case (true, false):
                            searchExpression = e => e.FirstName.StartsWith(searchString);
                            return searchExpression;
                        case (false, true):
                            searchExpression = e => e.LastName.StartsWith(searchString);
                            return searchExpression;
                        case (false, false):
                            searchExpression = e => e.FirstName.StartsWith(searchString)
                               || e.LastName.StartsWith(searchString);
                            return searchExpression;
                        default:
                            return null;
                    }
                case ("EndsWith"):
                    switch ((isFirstNameSearch, isLastNameSearch))
                    {
                        case (true, false):
                            searchExpression = e => e.FirstName.EndsWith(searchString);
                            return searchExpression;
                        case (false, true):
                            searchExpression = e => e.LastName.EndsWith(searchString);
                            return searchExpression;
                        case (false, false):
                            searchExpression = e => e.FirstName.EndsWith(searchString)
                               || e.LastName.EndsWith(searchString);
                            return searchExpression;
                        default:
                            return null;
                    }
                case ("Exact"):
                    switch ((isFirstNameSearch, isLastNameSearch))
                    {
                        case (true, false):
                            searchExpression = e => e.FirstName.Equals(searchString);
                            return searchExpression;
                        case (false, true):
                            searchExpression = e => e.LastName.Equals(searchString);
                            return searchExpression;
                        case (false, false):
                            searchExpression = e => e.FirstName.Equals(searchString)
                               || e.LastName.Equals(searchString);
                            return searchExpression;
                        default:
                            return null;
                    }
                default:
                    return null;
            }
        }

        public async Task<EmployeeDetailsViewModel> BuildEmployeeDetailsViewModel(int id, string returnUrl)
        {
            var selectedEmployee = GetEmployeeById(id);

            if (selectedEmployee == null)
            {
                return null;
            }

            string eMail = selectedEmployee.EmailAddress.ToString();
            string eMailUri = "n/a";

            if (!string.IsNullOrEmpty(eMail))
            {
                eMailUri = BuildEmailUri(eMail);
            }

            EmployeeDetailsViewModel vm = new EmployeeDetailsViewModel()
            {
                SelectedEmployee = selectedEmployee,
                PageTitle = "Details",
                EmailUri = eMailUri,
                ReturnUrl = returnUrl,
            };

            await Task.Delay(1);
            return vm;
        }

        public string BuildEmailUri
            (string eMail)
        {
            StringBuilder emailUriBuilder = new StringBuilder("mailto:");
            emailUriBuilder.Append(eMail);
            return emailUriBuilder.ToString();
        }

        public bool EmployeeExists(int id)
        {
            return _employeeService.EmployeeExists(id);
        }
    }
}
