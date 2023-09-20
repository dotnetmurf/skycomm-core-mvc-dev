using Microsoft.AspNetCore.Mvc;
using SkyCommNet7MVC.Presentation.Interfaces;
using SkyCommNet7MVC.Presentation.ViewModels.Personnel;

namespace SkyCommCoreMVC.Controllers
{
    public class PersonnelController : Controller
    {
        private readonly IPersonnelControllerService _personnelControllerService;

        public PersonnelController
            (IPersonnelControllerService personnelControllerService)
        {
            _personnelControllerService = personnelControllerService;
        }

        // GET: Personnel/Index
        public IActionResult Index()
        {
            return View();
        }

        // GET: Employees/Filter
        public async Task<IActionResult> Filter
            (int? filterJobTitle, int? filterDepartment, int? filterOffice, int? pageNumber, int? pageSize)
        {
            EmployeesFilterViewModel vm = await _personnelControllerService.BuildEmployeesFilterViewModel
                (filterJobTitle, filterDepartment, filterOffice, pageNumber, pageSize);

            if (vm == null)
            {
                return NotFound();
            }
            else
            {
                return View(vm);
            }
        }

        // GET: Employees/Search
        public async Task<IActionResult> Search
            (string searchString, string searchName, string? searchType, int? pageNumber, int? pageSize)
        {
            EmployeesSearchViewModel vm = await _personnelControllerService.BuildEmployeesSearchViewModel
                (searchString, searchName, searchType, pageNumber, pageSize);

            if (vm == null)
            {
                return NotFound();
            }
            else
            {
                return View(vm);
            }
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (!_personnelControllerService.EmployeeExists((int)id))
            {
                return NotFound();
            }

            string returnUrl = Request.Headers["Referer"].ToString();

            EmployeeDetailsViewModel vm = await _personnelControllerService.BuildEmployeeDetailsViewModel
                ((int)id, returnUrl);

            if (vm == null)
            {
                return NotFound();
            }
            else
            {
                return View(vm);
            }
        }
    }
}
