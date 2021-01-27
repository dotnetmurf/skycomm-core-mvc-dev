using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkyCommCoreMVC.Services;

namespace SkyCommCoreMVC.Controllers
{
    public class EquipmentController : Controller
    {
        private JsonFileConsolesService _consolesService;

        public EquipmentController(JsonFileConsolesService consolesService)
        {
            _consolesService = consolesService;
        }

        // GET: Equipment Index Home Page
        public ActionResult Index()
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
        public ActionResult ShowConsolePartial(string consoleID)
        {
            var skyCommConsoles = _consolesService.GetConsoles();

            var selectedConsole = skyCommConsoles.First(x => x.ConsoleId == consoleID);

            ViewBag.SelectedConsole = selectedConsole;

            return PartialView("ConsolePartial");
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