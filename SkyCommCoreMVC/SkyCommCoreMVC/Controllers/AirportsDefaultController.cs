using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SkyCommCoreMVC.Models;

namespace SkyCommCoreMVC.Controllers
{
    public class AirportsDefaultController : Controller
    {
        private readonly SkyCommDBContext _context;

        public AirportsDefaultController(SkyCommDBContext context)
        {
            _context = context;
        }

        // GET: AirportsDefault
        public async Task<IActionResult> Index()
        {
            var skyCommContext = _context.Airports.Include(a => a.AirportType).Include(a => a.Region).Include(a => a.SkyCommOpsLevel);
            return View(await skyCommContext.ToListAsync());
        }

        // GET: AirportsDefault/Details/5
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

        // GET: AirportsDefault/Create
        public IActionResult Create()
        {
            ViewData["AirportTypeId"] = new SelectList(_context.AirportTypes, "AirportTypeId", "AirportType");
            ViewData["RegionId"] = new SelectList(_context.Regions, "RegionId", "RegionCode");
            ViewData["SkyCommOpsLevelId"] = new SelectList(_context.SkyCommOpsLevels, "SkyCommOpsLevelId", "SkyCommOpsLevel");
            return View();
        }

        // POST: AirportsDefault/Create
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

        // GET: AirportsDefault/Edit/5
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

        // POST: AirportsDefault/Edit/5
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

        // GET: AirportsDefault/Delete/5
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

        // POST: AirportsDefault/Delete/5
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
