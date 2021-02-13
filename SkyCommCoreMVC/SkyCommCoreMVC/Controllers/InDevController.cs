using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SkyCommCoreMVC.Infrastructure;
using SkyCommCoreMVC.Models;
using SkyCommCoreMVC.Services;

namespace SkyCommCoreMVC.Controllers
{
    public class InDevController : Controller
    {
        private readonly SkyCommDBContext _context;
        private JsonFileConsolesService _consolesService;

        public InDevController(SkyCommDBContext context, JsonFileConsolesService consolesService)
        {
            _context = context;
            _consolesService = consolesService;
        }

        // GET: QuickStartGuideVideoPlayer
        public IActionResult QuickStartGuideVideoPlayer()
        {
            return View();
        }

        // GET: Consoles
        public ActionResult ConsolesListWithPartial()
        {
            var skyCommConsoles = _consolesService.GetConsoles();
            return View(skyCommConsoles.ToList());
        }

        //GET: Console by ID
        public ActionResult ShowConsolePartial(string consoleID)
        {
            var skyCommConsoles = _consolesService.GetConsoles();

            var selectedConsole = skyCommConsoles.First(x => x.ConsoleId == consoleID);

            ViewBag.SelectedConsole = selectedConsole;

            return PartialView("ConsolePartial");
        }

        // GET: UnitModelsList
        public async Task<IActionResult> UnitModelsList(int? pageNumber, int? pageSize)
        {
            var UnitModels = _context.UnitModels
                .Include(u => u.ModelCategory)
                .Include(u => u.ModelFreqBand)
                .Include(u => u.ModelManufacturer)
                .Include(u => u.ModelModType);
            //return View(await SkyCommDBContext.ToListAsync());

            string pageAction = "List";

            return View(await PaginatedList<UnitModels>.CreateAsync(UnitModels.AsNoTracking(), pageNumber ?? 1, pageSize ?? 10, pageAction));
        }

        // GET: UnitModelsCards
        public async Task<IActionResult> UnitModelsCards(int? pageNumber, int? pageSize)
        {
            var UnitModels = _context.UnitModels
                .Include(u => u.ModelCategory)
                .Include(u => u.ModelFreqBand)
                .Include(u => u.ModelManufacturer)
                .Include(u => u.ModelModType);
            //return View(await SkyCommDBContext.ToListAsync());

            string pageAction = "Cards";

            return View(await PaginatedList<UnitModels>.CreateAsync(UnitModels.AsNoTracking(), pageNumber ?? 1, pageSize ?? 12, pageAction));
        }

        // GET: UnitsIndex
        public async Task<IActionResult> UnitsIndex()
        {
            var skyCommDBContext = _context.Units.Include(u => u.Airport).Include(u => u.UnitModels);
            return View(await skyCommDBContext.ToListAsync());
        }

        // GET: Index
        public IActionResult Index()
        {
            return View();
        }


    }
}
