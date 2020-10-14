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
    public class AirportsController : Controller
    {
        private readonly SkyCommDBContext _context;

        public AirportsController(SkyCommDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var SkyCommDBContext = _context.Airports.Include(a => a.AirportType).Include(a => a.Region).Include(a => a.SkyCommOpsLevel);
            return View(await SkyCommDBContext.ToListAsync());
        }

        //// GET: Airports
        //public async Task<IActionResult> Index(int? pageNumber, int recordsPerPage = 5)
        //{

        //    ViewData["CurrentFilter"] = recordsPerPage;


        //    var airports = _context.Airports
        //        .Include(a => a.AirportType)
        //        .Include(a => a.Region)
        //            .ThenInclude(a => a.Country)
        //        .Include(a => a.SkyCommOpsLevel);

        //    int pageSize = recordsPerPage;
        //    //int pageSize = 10;

        //    return View(await PaginatedList<Airports>.CreateAsync(airports.AsNoTracking(), pageNumber ?? 1, pageSize));
        //}

        // GET: Airports/List
        public async Task<IActionResult> List(int? pageNumber, int? pageSize)
        {

            var airports = _context.Airports
                .Include(a => a.AirportType)
                .Include(a => a.Region)
                    .ThenInclude(a => a.Country)
                .Include(a => a.SkyCommOpsLevel);

            return View(await PaginatedList<Airports>.CreateAsync(airports.AsNoTracking(), pageNumber ?? 1, pageSize ?? 10));
        }

        // GET: Airports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airports = await _context.Airports
                .Include(a => a.AirportType)
                .Include(a => a.Region)
                .Include(a => a.SkyCommOpsLevel)
                .FirstOrDefaultAsync(m => m.AirportId == id);
            if (airports == null)
            {
                return NotFound();
            }

            return View(airports);
        }

        // GET: Airports/Create
        public IActionResult Create()
        {
            ViewData["AirportTypeId"] = new SelectList(_context.AirportTypes, "AirportTypeId", "AirportType");
            ViewData["RegionId"] = new SelectList(_context.Regions, "RegionId", "RegionCode");
            ViewData["SkyCommOpsLevelId"] = new SelectList(_context.SkyCommOpsLevels, "SkyCommOpsLevelId", "SkyCommOpsLevel");
            return View();
        }

        // POST: Airports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AirportId,AirportIatacode,AirportIdentifier,AirportTypeId,AirportName,AirportLatitudeDegrees,AirportLongitudeDegrees,AirportElevationFeet,RegionId,AirportMunicipality,AirportScheduledService,AirportGpscode,AirportLocalCode,AirportHomeLink,AirportWikipediaLink,SkyCommOpsLevelId")] Airports airports)
        {
            if (ModelState.IsValid)
            {
                _context.Add(airports);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AirportTypeId"] = new SelectList(_context.AirportTypes, "AirportTypeId", "AirportType", airports.AirportTypeId);
            ViewData["RegionId"] = new SelectList(_context.Regions, "RegionId", "RegionCode", airports.RegionId);
            ViewData["SkyCommOpsLevelId"] = new SelectList(_context.SkyCommOpsLevels, "SkyCommOpsLevelId", "SkyCommOpsLevel", airports.SkyCommOpsLevelId);
            return View(airports);
        }

        // GET: Airports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airports = await _context.Airports.FindAsync(id);
            if (airports == null)
            {
                return NotFound();
            }
            ViewData["AirportTypeId"] = new SelectList(_context.AirportTypes, "AirportTypeId", "AirportType", airports.AirportTypeId);
            ViewData["RegionId"] = new SelectList(_context.Regions, "RegionId", "RegionCode", airports.RegionId);
            ViewData["SkyCommOpsLevelId"] = new SelectList(_context.SkyCommOpsLevels, "SkyCommOpsLevelId", "SkyCommOpsLevel", airports.SkyCommOpsLevelId);
            return View(airports);
        }

        // POST: Airports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AirportId,AirportIatacode,AirportIdentifier,AirportTypeId,AirportName,AirportLatitudeDegrees,AirportLongitudeDegrees,AirportElevationFeet,RegionId,AirportMunicipality,AirportScheduledService,AirportGpscode,AirportLocalCode,AirportHomeLink,AirportWikipediaLink,SkyCommOpsLevelId")] Airports airports)
        {
            if (id != airports.AirportId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(airports);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AirportsExists(airports.AirportId))
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
            ViewData["AirportTypeId"] = new SelectList(_context.AirportTypes, "AirportTypeId", "AirportType", airports.AirportTypeId);
            ViewData["RegionId"] = new SelectList(_context.Regions, "RegionId", "RegionCode", airports.RegionId);
            ViewData["SkyCommOpsLevelId"] = new SelectList(_context.SkyCommOpsLevels, "SkyCommOpsLevelId", "SkyCommOpsLevel", airports.SkyCommOpsLevelId);
            return View(airports);
        }

        // GET: Airports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airports = await _context.Airports
                .Include(a => a.AirportType)
                .Include(a => a.Region)
                .Include(a => a.SkyCommOpsLevel)
                .FirstOrDefaultAsync(m => m.AirportId == id);
            if (airports == null)
            {
                return NotFound();
            }

            return View(airports);
        }

        // POST: Airports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var airports = await _context.Airports.FindAsync(id);
            _context.Airports.Remove(airports);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AirportsExists(int id)
        {
            return _context.Airports.Any(e => e.AirportId == id);
        }
    }
}
