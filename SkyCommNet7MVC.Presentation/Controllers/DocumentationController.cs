using Microsoft.AspNetCore.Mvc;
using SkyCommNet7MVC.Services.Services;

namespace SkyCommNet7MVC.Presentation.Controllers
{
    public class DocumentationController : Controller
    {
        private JsonFileConsolesService _consolesService;

        public DocumentationController(JsonFileConsolesService consolesService)
        {
            _consolesService = consolesService;
        }

        // GET: Documentation Index Home Page
        public ActionResult Index()
        {
            return View();
        }

        // GET: SkyComm Quick Start Guide
        public IActionResult QuickStartGuide()
        {
            return View();
        }

        // GET: Consoles
        public ActionResult ConsolesList()
        {
            var skyCommConsoles = _consolesService.GetConsoles();
            return View(skyCommConsoles.ToList());
        }

        //GET: Console by ID
        public ActionResult GetConsoleByID(string consoleID)
        {
            var skyCommConsoles = _consolesService.GetConsoles();

            var selectedConsole = skyCommConsoles.First(x => x.ConsoleId == consoleID);
            return View(selectedConsole);
        }
    }
}
