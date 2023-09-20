using Microsoft.AspNetCore.Mvc;
using SkyCommNet7MVC.Presentation.ViewModels.UnitModels;
using SkyCommNet7MVC.Services.Interfaces;

namespace SkyCommNet7MVC.Presentation.Controllers
{
    public class UnitModelsController : Controller
    {
        private readonly IUnitModelsControllerService _unitModelControlService;

        public UnitModelsController
            (IUnitModelsControllerService unitModelControlService)
        {
            _unitModelControlService = unitModelControlService;
        }

        public async Task<IActionResult> Filter
            (int? filterCategory, int? filterFreqBand, int? filterManufacturer, int? pageNumber, int? pageSize)
        {
            UnitModelsFilterViewModel vm = await _unitModelControlService.BuildUnitModelsFilterViewModel
                (filterCategory, filterFreqBand, filterManufacturer, pageNumber, pageSize);

            if (vm == null)
            {
                return NotFound();
            }
            else
            {
                return View(vm);
            }
        }

        //GET: UnitModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (!_unitModelControlService.UnitModelExists((int)id))
            {
                return NotFound();
            }

            string returnUrl = Request.Headers["Referer"].ToString();

            UnitModelDetailsViewModel vm = await _unitModelControlService.BuildUnitModelDetailsViewModel
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
