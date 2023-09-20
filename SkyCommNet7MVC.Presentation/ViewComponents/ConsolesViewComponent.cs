using Microsoft.AspNetCore.Mvc;
using SkyCommNet7MVC.Services.Services;

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
