using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkyCommNet7MVC.Data.Repositories;
using SkyCommNet7MVC.Presentation.ViewModels.Continents;
using SkyCommNet7MVC.Services.Interfaces;

namespace SkyCommNet7MVC.Presentation.Controllers
{
    public class ContinentsController : Controller
    {
        private readonly SkyCommDbContext _context;
        private readonly IContinentService _continentService;

        public ContinentsController(SkyCommDbContext context, IContinentService continentService)
        {
            _context = context;
            _continentService = continentService;
        }

        // GET: Continents
        public async Task<IActionResult> Index()
        {
            ContinentsIndexViewModel vm = new ContinentsIndexViewModel()
            {
                Continents = await _continentService.GetAllContinents().ToListAsync()
            };

            return View(vm);
        }

        // GET: Continents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Continents == null)
            {
                return NotFound();
            }

            var continent = await _context.Continents
                .FirstOrDefaultAsync(m => m.ContinentId == id);
            if (continent == null)
            {
                return NotFound();
            }

            return View(continent);
        }
    }
}
