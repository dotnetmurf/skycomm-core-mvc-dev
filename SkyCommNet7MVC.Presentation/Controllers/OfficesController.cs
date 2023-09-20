using Microsoft.AspNetCore.Mvc;
using SkyCommNet7MVC.Presentation.Interfaces;
using SkyCommNet7MVC.Presentation.ViewModels.Offices;

namespace SkyCommNet7MVC.Presentation.Controllers
{
    public class OfficesController : Controller
    {
        private readonly IOfficesControllerService _officesControllerService;

        public OfficesController
            (IOfficesControllerService officesControllerService)
        {
            _officesControllerService = officesControllerService;
        }

        // GET: Offices/Filter
        public async Task<IActionResult> Filter(int? filterCountry, int? pageNumber, int? pageSize)
        {
            OfficesFilterViewModel vm = await _officesControllerService.BuildOfficesFilterViewModel
            (filterCountry, pageNumber, pageSize);

            if (vm == null)
            {
                return NotFound();
            }
            else
            {
                return View(vm);
            }
        }

        // GET: Offices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (!_officesControllerService.OfficeExists((int)id))
            {
                return NotFound();
            }

            string returnUrl = Request.Headers["Referer"].ToString();

            OfficeDetailsViewModel vm = await _officesControllerService.BuildOfficeDetailsViewModel
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
