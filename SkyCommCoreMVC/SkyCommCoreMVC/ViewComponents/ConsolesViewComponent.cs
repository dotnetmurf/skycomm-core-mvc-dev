using Microsoft.AspNetCore.Mvc;
using SkyCommCoreMVC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkyCommCoreMVC.ViewComponents
{
    public class ConsolesViewComponent : ViewComponent
    {
        private JsonFileConsolesService _consolesService;

        public ConsolesViewComponent(JsonFileConsolesService consolesService)
        {
            _consolesService = consolesService;
        }

        public IViewComponentResult Invoke(string consoleID)
        {
            var skyCommConsole = _consolesService.GetConsoleByID(consoleID);
            return View(skyCommConsole);
        }

    }
}
