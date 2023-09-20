using Microsoft.AspNetCore.Mvc;
using SkyCommNet7MVC.Services.Services;

namespace SkyCommNet7MVC.Presentation.Controllers
{
    public class EquipmentController : Controller
    {
        public EquipmentController()
        {
        }

        // GET: Equipment Index Home Page
        public ActionResult Index()
        {
            return View();
        }
    }
}
