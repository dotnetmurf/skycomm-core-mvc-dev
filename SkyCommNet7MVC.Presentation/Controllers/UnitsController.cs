using Microsoft.AspNetCore.Mvc;
using SkyCommNet7MVC.Presentation.ViewModels.Units;
using SkyCommNet7MVC.Services.Interfaces;

namespace SkyCommNet7MVC.Presentation.Controllers
{
    public class UnitsController : Controller
    {
        private readonly IUnitsControllerService _unitControlService;

        public UnitsController
            (IUnitsControllerService unitControlService)
        {
            _unitControlService = unitControlService;
        }

        public async Task<IActionResult> Filter
            (int? filterAirport, int? filterUnitModel, int? pageNumber, int? pageSize)
        {
            UnitsFilterViewModel vm = await _unitControlService.BuildUnitsFilterViewModel
                (filterAirport, filterUnitModel, pageNumber, pageSize);

            if (vm == null)
            {
                return NotFound();
            }
            else
            {
                return View(vm);
            }
        }

        // GET: Units/Search
        public async Task<IActionResult> Search
            (string searchString, string searchNbr, string? searchType, int? pageNumber, int? pageSize)
        {
            UnitsSearchViewModel vm = await _unitControlService.BuildUnitsSearchViewModel
                (searchString, searchNbr, searchType, pageNumber, pageSize);

            if (vm == null)
            {
                return NotFound();
            }
            else
            {
                return View(vm);
            }
        }

        //GET: Units/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (!_unitControlService.UnitExists((int)id))
            {
                return NotFound();
            }

            string returnUrl = Request.Headers["Referer"].ToString();

            UnitDetailsViewModel vm = await _unitControlService.BuildUnitDetailsViewModel
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
