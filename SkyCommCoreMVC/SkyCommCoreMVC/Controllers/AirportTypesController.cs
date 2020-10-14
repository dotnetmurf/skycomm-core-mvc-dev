using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SkyCommCoreMVC.Infrastructure;
using SkyCommCoreMVC.Models;

namespace SkyCommCoreMVC.Controllers
{
    public class AirportTypesController : Controller
    {
        private readonly SkyCommDBContext _context;

        public AirportTypesController(SkyCommDBContext context)
        {
            _context = context;
        }

        // GET: AirportTypes
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["AirportTypeSortParm"] = String.IsNullOrEmpty(sortOrder) ? "airportType_desc" : "";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var airportTypes = from a in _context.AirportTypes
                           select a;
            if (!String.IsNullOrEmpty(searchString))
            {
                airportTypes = airportTypes.Where(a => a.AirportType.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "airportType_desc":
                    airportTypes = airportTypes.OrderByDescending(a => a.AirportType);
                    break;
                default:
                    airportTypes = airportTypes.OrderBy(a => a.AirportType);
                    break;
            }

            int pageSize = 3;

            return View(await PaginatedList<AirportTypes>.CreateAsync(airportTypes.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: AirportTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airportTypes = await _context.AirportTypes
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.AirportTypeId == id);
            if (airportTypes == null)
            {
                return NotFound();
            }

            return View(airportTypes);
        }

        // GET: AirportTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AirportTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AirportType")] AirportTypes airportTypes)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(airportTypes);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View(airportTypes);
        }        

        // GET: AirportTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airportTypes = await _context.AirportTypes.FindAsync(id);
            if (airportTypes == null)
            {
                return NotFound();
            }
            return View(airportTypes);
        }

        // POST: AirportTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var airportTypesToUpdate = await _context.AirportTypes.FirstOrDefaultAsync(s => s.AirportTypeId == id);
            if (await TryUpdateModelAsync<AirportTypes>(
                airportTypesToUpdate,
                "",
                s => s.AirportType))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            return View(airportTypesToUpdate);
        }

        // GET: AirportTypes/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airportTypes = await _context.AirportTypes
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.AirportTypeId == id);
            if (airportTypes == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(airportTypes);
        }

        // POST: AirportTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var airportTypes = await _context.AirportTypes.FindAsync(id);

            if (airportTypes == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.AirportTypes.Remove(airportTypes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }

        }

        private bool AirportTypesExists(int id)
        {
            return _context.AirportTypes.Any(e => e.AirportTypeId == id);
        }
    }
}
