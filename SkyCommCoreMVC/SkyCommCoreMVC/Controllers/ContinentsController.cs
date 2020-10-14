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
    public class ContinentsController : Controller
    {
        private readonly SkyCommDBContext _context;

        public ContinentsController(SkyCommDBContext context)
        {
            _context = context;
        }

        // GET: Continents
        public async Task<IActionResult> Index()
        {
            return View(await _context.Continents.ToListAsync());
        }

        // GET: Continents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var continents = await _context.Continents
                .FirstOrDefaultAsync(m => m.ContinentId == id);
            if (continents == null)
            {
                return NotFound();
            }

            return View(continents);
        }

        // GET: Continents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Continents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContinentId,Continent")] Continents continents)
        {
            if (ModelState.IsValid)
            {
                _context.Add(continents);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(continents);
        }

        // GET: Continents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var continents = await _context.Continents.FindAsync(id);
            if (continents == null)
            {
                return NotFound();
            }
            return View(continents);
        }

        // POST: Continents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContinentId,Continent")] Continents continents)
        {
            if (id != continents.ContinentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(continents);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContinentsExists(continents.ContinentId))
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
            return View(continents);
        }

        // GET: Continents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var continents = await _context.Continents
                .FirstOrDefaultAsync(m => m.ContinentId == id);
            if (continents == null)
            {
                return NotFound();
            }

            return View(continents);
        }

        // POST: Continents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var continents = await _context.Continents.FindAsync(id);
            _context.Continents.Remove(continents);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContinentsExists(int id)
        {
            return _context.Continents.Any(e => e.ContinentId == id);
        }
    }
}
