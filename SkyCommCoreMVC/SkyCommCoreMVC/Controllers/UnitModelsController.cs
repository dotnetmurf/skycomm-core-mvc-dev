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
    public class UnitModelsController : Controller
    {
        private readonly SkyCommDBContext _context;

        public UnitModelsController(SkyCommDBContext context)
        {
            _context = context;
        }

        // GET: UnitModels
        public async Task<IActionResult> Index()
        {
            var SkyCommDBContext = _context.UnitModels
                .Include(u => u.ModelCategory)
                .Include(u => u.ModelFreqBand)
                .Include(u => u.ModelManufacturer)
                .Include(u => u.ModelModType);
            return View(await SkyCommDBContext.ToListAsync());
        }

        // GET: UnitModels/List
        public async Task<IActionResult> List(int? pageNumber, int? pageSize)
        {
            var UnitModels = _context.UnitModels
                .Include(u => u.ModelCategory)
                .Include(u => u.ModelFreqBand)
                .Include(u => u.ModelManufacturer)
                .Include(u => u.ModelModType);
            //return View(await SkyCommDBContext.ToListAsync());

            return View(await PaginatedList<UnitModels>.CreateAsync(UnitModels.AsNoTracking(), pageNumber ?? 1, pageSize ?? 10));
        }

        // GET: UnitModels/Cards
        public async Task<IActionResult> Cards(int? pageNumber, int? pageSize)
        {
            var UnitModels = _context.UnitModels
                .Include(u => u.ModelCategory)
                .Include(u => u.ModelFreqBand)
                .Include(u => u.ModelManufacturer)
                .Include(u => u.ModelModType);
            //return View(await SkyCommDBContext.ToListAsync());

            return View(await PaginatedList<UnitModels>.CreateAsync(UnitModels.AsNoTracking(), pageNumber ?? 1, pageSize ?? 10));
        }

        // GET: UnitModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var UnitModels = await _context.UnitModels
                .Include(u => u.ModelCategory)
                .Include(u => u.ModelFreqBand)
                .Include(u => u.ModelManufacturer)
                .Include(u => u.ModelModType)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.UnitModelId == id);
            if (UnitModels == null)
            {
                return NotFound();
            }

            return View(UnitModels);
        }

        // GET: UnitModels/Create
        public IActionResult Create()
        {
            ViewData["ModelCategoryId"] = new SelectList(_context.ModelCategories, "ModelCategoryId", "ModelCategory");
            ViewData["ModelFreqBandId"] = new SelectList(_context.ModelFreqBands, "ModelFreqBandId", "ModelFreqBand");
            ViewData["ModelManufacturerId"] = new SelectList(_context.ModelManufacturers, "ModelManufacturerId", "ModelManufacturerName");
            ViewData["ModelModTypeId"] = new SelectList(_context.ModelModTypes, "ModelModTypeId", "ModelModType");
            return View();
        }

        // POST: UnitModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UnitModelId,ModelCode,ModelName,ModelImage,ModelMsrp,ModelManufacturerId,ModelCategoryId,ModelFreqBandId,ModelModTypeId")] UnitModels UnitModels)
        {
            if (ModelState.IsValid)
            {
                _context.Add(UnitModels);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ModelCategoryId"] = new SelectList(_context.ModelCategories, "ModelCategoryId", "ModelCategory", UnitModels.ModelCategoryId);
            ViewData["ModelFreqBandId"] = new SelectList(_context.ModelFreqBands, "ModelFreqBandId", "ModelFreqBand", UnitModels.ModelFreqBandId);
            ViewData["ModelManufacturerId"] = new SelectList(_context.ModelManufacturers, "ModelManufacturerId", "ModelManufacturerName", UnitModels.ModelManufacturerId);
            ViewData["ModelModTypeId"] = new SelectList(_context.ModelModTypes, "ModelModTypeId", "ModelModType", UnitModels.ModelModTypeId);
            return View(UnitModels);
        }

        // GET: UnitModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var UnitModels = await _context.UnitModels.FindAsync(id);
            if (UnitModels == null)
            {
                return NotFound();
            }
            ViewData["ModelCategoryId"] = new SelectList(_context.ModelCategories, "ModelCategoryId", "ModelCategory", UnitModels.ModelCategoryId);
            ViewData["ModelFreqBandId"] = new SelectList(_context.ModelFreqBands, "ModelFreqBandId", "ModelFreqBand", UnitModels.ModelFreqBandId);
            ViewData["ModelManufacturerId"] = new SelectList(_context.ModelManufacturers, "ModelManufacturerId", "ModelManufacturerName", UnitModels.ModelManufacturerId);
            ViewData["ModelModTypeId"] = new SelectList(_context.ModelModTypes, "ModelModTypeId", "ModelModType", UnitModels.ModelModTypeId);
            return View(UnitModels);
        }

        // POST: UnitModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UnitModelId,ModelCode,ModelName,ModelImage,ModelMsrp,ModelManufacturerId,ModelCategoryId,ModelFreqBandId,ModelModTypeId")] UnitModels UnitModels)
        {
            if (id != UnitModels.UnitModelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(UnitModels);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UnitModelsExists(UnitModels.UnitModelId))
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
            ViewData["ModelCategoryId"] = new SelectList(_context.ModelCategories, "ModelCategoryId", "ModelCategory", UnitModels.ModelCategoryId);
            ViewData["ModelFreqBandId"] = new SelectList(_context.ModelFreqBands, "ModelFreqBandId", "ModelFreqBand", UnitModels.ModelFreqBandId);
            ViewData["ModelManufacturerId"] = new SelectList(_context.ModelManufacturers, "ModelManufacturerId", "ModelManufacturerName", UnitModels.ModelManufacturerId);
            ViewData["ModelModTypeId"] = new SelectList(_context.ModelModTypes, "ModelModTypeId", "ModelModType", UnitModels.ModelModTypeId);
            return View(UnitModels);
        }

        // GET: UnitModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var UnitModels = await _context.UnitModels
                .Include(u => u.ModelCategory)
                .Include(u => u.ModelFreqBand)
                .Include(u => u.ModelManufacturer)
                .Include(u => u.ModelModType)
                .FirstOrDefaultAsync(m => m.UnitModelId == id);
            if (UnitModels == null)
            {
                return NotFound();
            }

            return View(UnitModels);
        }

        // POST: UnitModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var UnitModels = await _context.UnitModels.FindAsync(id);
            _context.UnitModels.Remove(UnitModels);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UnitModelsExists(int id)
        {
            return _context.UnitModels.Any(e => e.UnitModelId == id);
        }
    }
}
