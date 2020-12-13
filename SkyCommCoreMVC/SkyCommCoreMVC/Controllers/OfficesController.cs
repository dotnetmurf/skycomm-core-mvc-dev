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
    public class OfficesController : Controller
    {
        private readonly SkyCommDBContext _context;

        public OfficesController(SkyCommDBContext context)
        {
            _context = context;
        }

        // GET: Offices/Filter
        public async Task<IActionResult> Filter(int? filterCountry, int? pageNumber, int? pageSize)
        {
            var offices = from o in _context.Offices select o;

            if (filterCountry == null)
            {
                ViewData["CountryId"] = new SelectList(GetCountriesSelectList(), "CountryId", "CountryName");
            }
            else
            {
                ViewData["CountryId"] = new SelectList(GetCountriesSelectList(), "CountryId", "CountryName", filterCountry);
                offices = offices.Where(o => o.CountryId.Equals(filterCountry));
            }

            ViewData["CountryFilter"] = filterCountry;
            ViewData["CurrentPageSize"] = pageSize;

            var skyCommContext = offices
                .Include(o => o.Country)
                .OrderBy(o => o.OfficeName);

            ViewData["RecordCount"] = skyCommContext.Count();

            string pageAction = "Filter";

            return View(await PaginatedList<Offices>.CreateAsync(skyCommContext.AsNoTracking(), pageNumber ?? 1, pageSize ?? 5, pageAction));
        }

        // GET: Offices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offices = await _context.Offices
                .Include(o => o.Country)
                .FirstOrDefaultAsync(m => m.OfficeId == id);
            if (offices == null)
            {
                return NotFound();
            }

            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
            return View(offices);
        }

        // GET: Offices/Create
        public IActionResult Create()
        {
            ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "CountryName");

            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
            return View();
        }

        // POST: Offices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OfficeId,OfficeName,OfficeAddress1,OfficeAddress2,OfficeCity,OfficeState,CountryId,OfficePostalCode,OfficePhoneNumber,OfficeFaxNumber")] Offices offices)
        {
            if (ModelState.IsValid)
            {
                _context.Add(offices);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "CountryName", offices.CountryId);

            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
            return View(offices);
        }

        // GET: Offices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offices = await _context.Offices.FindAsync(id);
            if (offices == null)
            {
                return NotFound();
            }
            ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "CountryName", offices.CountryId);

            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
            return View(offices);
        }

        // POST: Offices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OfficeId,OfficeName,OfficeAddress1,OfficeAddress2,OfficeCity,OfficeState,CountryId,OfficePostalCode,OfficePhoneNumber,OfficeFaxNumber")] Offices offices)
        {
            if (id != offices.OfficeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(offices);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OfficesExists(offices.OfficeId))
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
            ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "CountryName", offices.CountryId);

            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
            return View(offices);
        }

        // GET: Offices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offices = await _context.Offices
                .Include(o => o.Country)
                .FirstOrDefaultAsync(m => m.OfficeId == id);
            if (offices == null)
            {
                return NotFound();
            }

            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
            return View(offices);
        }

        // POST: Offices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var offices = await _context.Offices.FindAsync(id);
            _context.Offices.Remove(offices);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OfficesExists(int id)
        {
            return _context.Offices.Any(e => e.OfficeId == id);
        }

        private List<Countries> GetCountriesSelectList()
        {
            var countryList = from country in _context.Countries
                              join office in _context.Offices
                              on country.CountryId equals office.CountryId
                              select country;

            var countrySL = countryList.Distinct().OrderBy(country => country.CountryName).ToList();

            return countrySL;
        }
    }
}
