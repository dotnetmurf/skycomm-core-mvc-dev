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

        // GET: Offices
        public async Task<IActionResult> Index()
        {
            var skyCommContext = _context.Offices.Include(o => o.Country);
            return View(await skyCommContext.ToListAsync());
        }

        // GET: Offices/List
        public async Task<IActionResult> List(int? pageNumber, int? pageSize)
        {
            var offices = _context.Offices.Include(o => o.Country);
            //return View(await offices.ToListAsync());

            return View(await PaginatedList<Offices>.CreateAsync(offices.AsNoTracking(), pageNumber ?? 1, pageSize ?? 10));
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

            return View(offices);
        }

        // GET: Offices/Create
        public IActionResult Create()
        {
            ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "CountryName");
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
    }
}
