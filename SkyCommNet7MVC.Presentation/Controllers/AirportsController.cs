using Microsoft.AspNetCore.Mvc;
using SkyCommNet7MVC.Presentation.ViewModels.Airports;
using SkyCommNet7MVC.Services.Interfaces;

namespace SkyCommNet7MVC.Presentation.Controllers
{
    public class AirportsController : Controller
    {
        private readonly IAirportsControllerService _airportControlService;

        public AirportsController
            (IAirportsControllerService airportControlService)
        {
            _airportControlService = airportControlService;
        }

        // GET: Airports/Index
        public IActionResult Index()
        {
            return View();
        }

        // GET: Airports/Filter
        public async Task<IActionResult> Filter
            (int? filterCountry, int? filterAirportType, int? filterSkyComm, int? pageNumber, int? pageSize)
        {
            AirportsFilterViewModel vm = await _airportControlService.BuildAirportsFilterViewModel
                (filterCountry, filterAirportType, filterSkyComm, pageNumber, pageSize);

            if (vm == null)
            {
                return NotFound();
            }
            else
            {
                return View(vm);
            }
        }

        // GET: Airports/Search
        public async Task<IActionResult> Search
            (string searchString, string searchName, string? searchType, int? pageNumber, int? pageSize)
        {
            AirportsSearchViewModel vm = await _airportControlService.BuildAirportsSearchViewModel
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

        // GET: Airports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (!_airportControlService.AirportExists((int)id))
            {
                return NotFound();
            }

            string returnUrl = Request.Headers["Referer"].ToString();

            AirportDetailsViewModel vm = await _airportControlService.BuildAirportDetailsViewModel
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
