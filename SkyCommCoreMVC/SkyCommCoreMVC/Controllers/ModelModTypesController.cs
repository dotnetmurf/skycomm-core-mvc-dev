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
    public class ModelModTypesController : Controller
    {
        private readonly SkyCommDBContext _context;

        public ModelModTypesController(SkyCommDBContext context)
        {
            _context = context;
        }

        // GET: ModelModTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.ModelModTypes.ToListAsync());
        }

        // GET: ModelModTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modelModTypes = await _context.ModelModTypes
                .FirstOrDefaultAsync(m => m.ModelModTypeId == id);
            if (modelModTypes == null)
            {
                return NotFound();
            }

            return View(modelModTypes);
        }

        // GET: ModelModTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ModelModTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ModelModTypeId,ModelModType")] ModelModTypes modelModTypes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(modelModTypes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(modelModTypes);
        }

        // GET: ModelModTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modelModTypes = await _context.ModelModTypes.FindAsync(id);
            if (modelModTypes == null)
            {
                return NotFound();
            }
            return View(modelModTypes);
        }

        // POST: ModelModTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ModelModTypeId,ModelModType")] ModelModTypes modelModTypes)
        {
            if (id != modelModTypes.ModelModTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(modelModTypes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModelModTypesExists(modelModTypes.ModelModTypeId))
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
            return View(modelModTypes);
        }

        // GET: ModelModTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modelModTypes = await _context.ModelModTypes
                .FirstOrDefaultAsync(m => m.ModelModTypeId == id);
            if (modelModTypes == null)
            {
                return NotFound();
            }

            return View(modelModTypes);
        }

        // POST: ModelModTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var modelModTypes = await _context.ModelModTypes.FindAsync(id);
            _context.ModelModTypes.Remove(modelModTypes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModelModTypesExists(int id)
        {
            return _context.ModelModTypes.Any(e => e.ModelModTypeId == id);
        }
    }
}
