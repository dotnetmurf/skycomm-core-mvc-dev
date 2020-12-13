using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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

        public IActionResult Index()
        {
            return View();
        }

        // GET: Airports/Filter
        public async Task<IActionResult> Filter(int? filterSkyComm, int? filterCountry, int? filterAirportType, int? pageNumber, int? pageSize)
        {
            var airports = from a in _context.Airports select a;
            var skyCommL = _context.SkyCommOpsLevels.ToList();
            skyCommL.Add(new SkyCommOpsLevels { SkyCommOpsLevelId = 0, SkyCommOpsLevel = "All SkyComm" });
            var skyCommSL = skyCommL.OrderBy(s => s.SkyCommOpsLevelId);
            var countrySL = _context.Countries.OrderBy(c => c.CountryName);
            var airportTypeSL = _context.AirportTypes.OrderBy(a => a.AirportType);

            if (filterSkyComm == null)
            {
                ViewData["SkyCommOpsLevelId"] = new SelectList(skyCommSL, "SkyCommOpsLevelId", "SkyCommOpsLevel");
            }
            else if (filterSkyComm.Equals(0))
            {
                ViewData["SkyCommOpsLevelId"] = new SelectList(skyCommSL, "SkyCommOpsLevelId", "SkyCommOpsLevel", filterSkyComm);
                airports = airports.Where(a => a.SkyCommOpsLevelId != 4);
            }
            else
            {
                ViewData["SkyCommOpsLevelId"] = new SelectList(skyCommSL, "SkyCommOpsLevelId", "SkyCommOpsLevel", filterSkyComm);
                airports = airports.Where(a => a.SkyCommOpsLevelId.Equals(filterSkyComm));
            }

            if (filterCountry == null)
            {
                ViewData["CountryId"] = new SelectList(countrySL, "CountryId", "CountryName");
            }
            else
            {
                ViewData["CountryId"] = new SelectList(countrySL, "CountryId", "CountryName", filterCountry);
                airports = airports.Where(a => a.Region.CountryId.Equals(filterCountry));
            }

            if (filterAirportType == null)
            {
                ViewData["AirportTypeId"] = new SelectList(airportTypeSL, "AirportTypeId", "AirportType");
            }
            else
            {
                ViewData["AirportTypeId"] = new SelectList(airportTypeSL, "AirportTypeId", "AirportType", filterAirportType);
                airports = airports.Where(a => a.AirportTypeId.Equals(filterAirportType));
            }

            ViewData["SkyCommFilter"] = filterSkyComm;
            ViewData["CountryFilter"] = filterCountry;
            ViewData["AirportTypeFilter"] = filterAirportType;
            ViewData["CurrentPageSize"] = pageSize;

            string pageAction = "Filter";

            var skyCommContext = airports
                .Include(a => a.AirportType)
                .Include(a => a.Region)
                    .ThenInclude(a => a.Country)
                .Include(a => a.SkyCommOpsLevel)
                .OrderBy(a => a.AirportIatacode);

            ViewData["RecordCount"] = skyCommContext.Count();

            return View(await PaginatedList<Airports>.CreateAsync(skyCommContext.AsNoTracking(), pageNumber ?? 1, pageSize ?? 5, pageAction));
        }

        // GET: Airports/Search
        public async Task<IActionResult> Search(string searchString, string searchName, int? pageNumber, int? pageSize)
        {
            ViewData["SearchString"] = searchString;
            ViewData["SearchName"] = searchName;

            ViewData["CurrentPageSize"] = pageSize;

            string pageAction = "Search";

            var airports = from a in _context.Airports select a;

            if (!String.IsNullOrEmpty(searchString))
            {
                if (searchName == "Code")
                {
                    airports = airports.Where(a => a.AirportIatacode.Contains(searchString));
                }
                else if (searchName == "Name")
                {
                    airports = airports.Where(a => a.AirportName.Contains(searchString));
                }
                else
                {
                    airports = airports.Where(a => a.AirportIatacode.Contains(searchString)
                   || a.AirportName.Contains(searchString));
                }
            }

            var skyCommContext = airports
                .Include(a => a.AirportType)
                .Include(a => a.Region)
                    .ThenInclude(a => a.Country)
                .Include(a => a.SkyCommOpsLevel)
                .OrderBy(a => a.AirportIatacode);

            ViewData["RecordCount"] = skyCommContext.Count();

            return View(await PaginatedList<Airports>.CreateAsync(skyCommContext.AsNoTracking(), pageNumber ?? 1, pageSize ?? 5, pageAction));
        }

        //// GET: Airports/Details/5
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

            string mapLatitude = airports.AirportLatitudeDegrees.ToString();
            string mapLongitude = airports.AirportLongitudeDegrees.ToString();

            StringBuilder mapUriBuilder = new StringBuilder("https://");
            mapUriBuilder.Append("www.mapquestapi.com/staticmap/v4/getmap?size=500,400&type=map&zoom=11&center=");
            mapUriBuilder.Append(mapLatitude);
            mapUriBuilder.Append(",");
            mapUriBuilder.Append(mapLongitude);
            mapUriBuilder.Append("&imagetype=JPEG&key=FnEceCKCZhq1u4OZnAQIWUQzAvAdLEny");
            ViewData["MapURI"] = mapUriBuilder;

            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
            return View(airports);
        }

        // GET: Airports/Create
        public IActionResult Create()
        {
            ViewData["AirportTypeId"] = new SelectList(_context.AirportTypes, "AirportTypeId", "AirportType");
            ViewData["RegionId"] = new SelectList(_context.Regions, "RegionId", "RegionCode");
            ViewData["SkyCommOpsLevelId"] = new SelectList(_context.SkyCommOpsLevels, "SkyCommOpsLevelId", "SkyCommOpsLevel");

            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
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

            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
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

            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
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

            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
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

            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
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
