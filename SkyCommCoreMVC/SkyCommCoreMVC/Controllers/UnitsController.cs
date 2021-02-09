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
    public class UnitsController : Controller
    {
        private readonly SkyCommDBContext _context;

        public UnitsController(SkyCommDBContext context)
        {
            _context = context;
        }

        // GET: Units/Filter
        public async Task<IActionResult> Filter(int? filterAirport, int? filterUnitModel, int? pageNumber, int? pageSize)
        {
            var units = from u in _context.Units select u;
            var unitModelSL = _context.UnitModels.OrderBy(m => m.ModelName);

            if (filterAirport == null)
            {
                ViewData["AirportId"] = new SelectList(GetAirportsSelectList(), "AirportId", "AirportName");
            }
            else
            {
                ViewData["AirportId"] = new SelectList(GetAirportsSelectList(), "AirportId", "AirportName", filterAirport);
                units = units.Where(u => u.AirportId.Equals(filterAirport));
            }

            if (filterUnitModel == null)
            {
                ViewData["UnitModelId"] = new SelectList(unitModelSL, "UnitModelId", "ModelName");
            }
            else
            {
                ViewData["UnitModelId"] = new SelectList(unitModelSL, "UnitModelId", "ModelName", filterUnitModel);
                units = units.Where(u => u.UnitModelId.Equals(filterUnitModel));
            }

            ViewData["AirportFilter"] = filterAirport;
            ViewData["UnitModelFilter"] = filterUnitModel;
            ViewData["CurrentPageSize"] = pageSize;

            string pageAction = "Filter";

            var skyCommContext = units
                .Include(u => u.Airport)
                .Include(u => u.UnitModels)
                .OrderBy(u => u.UnitId);

            ViewData["RecordCount"] = skyCommContext.Count();

            return View(await PaginatedList<Units>.CreateAsync(skyCommContext.AsNoTracking(), pageNumber ?? 1, pageSize ?? 5, pageAction));
        }

        // GET: Units/Search
        public async Task<IActionResult> Search(string searchString, string searchNbr, int? pageNumber, int? pageSize)
        {
            ViewData["SearchString"] = searchString;
            ViewData["SearchNbr"] = searchNbr;

            ViewData["CurrentPageSize"] = pageSize;

            string pageAction = "Search";

            var units = from u in _context.Units select u;

            if (!String.IsNullOrEmpty(searchString))
            {
                if (searchNbr == "SCNbr")
                {
                    units = units.Where(u => u.UnitScnbr.Contains(searchString));
                }
                else if (searchNbr == "SerNbr")
                {
                    units = units.Where(u => u.UnitSerNbr.Contains(searchString));
                }
                else
                {
                    units = units.Where(u => u.UnitScnbr.Contains(searchString)
                   || u.UnitSerNbr.Contains(searchString));
                }
            }

            var skyCommContext = units
                .Include(u => u.Airport)
                .Include(u => u.UnitModels)
                .OrderBy(u => u.UnitId);

            ViewData["RecordCount"] = skyCommContext.Count();

            return View(await PaginatedList<Units>.CreateAsync(skyCommContext.AsNoTracking(), pageNumber ?? 1, pageSize ?? 5, pageAction));
        }

        // GET: Units/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var units = await _context.Units
                .Include(u => u.Airport)
                .Include(u => u.UnitModels)
                .FirstOrDefaultAsync(m => m.UnitId == id);
            if (units == null)
            {
                return NotFound();
            }

            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
            return View(units);
        }

        // GET: Units/Create
        public IActionResult Create()
        {
            ViewData["AirportId"] = new SelectList(_context.Airports, "AirportId", "AirportIatacode");
            ViewData["UnitModelId"] = new SelectList(_context.UnitModels, "UnitModelId", "ModelCode");

            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
            return View();
        }

        // POST: Units/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UnitId,UnitModelId,UnitScnbr,UnitSerNbr,UnitCost,AirportId")] Units units)
        {
            if (ModelState.IsValid)
            {
                _context.Add(units);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AirportId"] = new SelectList(_context.Airports, "AirportId", "AirportIatacode", units.AirportId);
            ViewData["UnitModelId"] = new SelectList(_context.UnitModels, "UnitModelId", "ModelCode", units.UnitModelId);

            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
            return View(units);
        }

        // GET: Units/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var units = await _context.Units.FindAsync(id);
            if (units == null)
            {
                return NotFound();
            }
            ViewData["AirportId"] = new SelectList(_context.Airports, "AirportId", "AirportIatacode", units.AirportId);
            ViewData["UnitModelId"] = new SelectList(_context.UnitModels, "UnitModelId", "ModelCode", units.UnitModelId);

            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
            return View(units);
        }

        // POST: Units/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UnitId,UnitModelId,UnitScnbr,UnitSerNbr,UnitCost,AirportId")] Units units)
        {
            if (id != units.UnitId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(units);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UnitsExists(units.UnitId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AirportId"] = new SelectList(_context.Airports, "AirportId", "AirportIatacode", units.AirportId);
            ViewData["UnitModelId"] = new SelectList(_context.UnitModels, "UnitModelId", "ModelCode", units.UnitModelId);

            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
            return View(units);
        }

        // GET: Units/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var units = await _context.Units
                .Include(u => u.Airport)
                .Include(u => u.UnitModels)
                .FirstOrDefaultAsync(m => m.UnitId == id);
            if (units == null)
            {
                return NotFound();
            }

            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
            return View(units);
        }

        // POST: Units/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var units = await _context.Units.FindAsync(id);
            _context.Units.Remove(units);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UnitsExists(int id)
        {
            return _context.Units.Any(e => e.UnitId == id);
        }

        private List<Airports> GetAirportsSelectList()
        {
            var airportList = from airport in _context.Airports
                              join unit in _context.Units
                              on airport.AirportId equals unit.AirportId
                              select airport;

            var airportSL = airportList.Distinct().OrderBy(airport => airport.AirportName).ToList();

            return airportSL;
        }

    }
}
