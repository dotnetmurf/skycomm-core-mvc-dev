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

            string pageAction = "List";

            return View(await PaginatedList<UnitModels>.CreateAsync(UnitModels.AsNoTracking(), pageNumber ?? 1, pageSize ?? 10, pageAction));
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

            string pageAction = "Cards";

            return View(await PaginatedList<UnitModels>.CreateAsync(UnitModels.AsNoTracking(), pageNumber ?? 1, pageSize ?? 12, pageAction));
        }

        //GET: UnitModels/Filter
        public async Task<IActionResult> Filter(int? filterCategory, int? filterFreqBand, int? filterManufacturer, int? pageNumber, int? pageSize)
        {
            var unitModels = from u in _context.UnitModels select u;
            var categorySL = _context.ModelCategories.OrderBy(c => c.ModelCategory);
            var freqBandSL = _context.ModelFreqBands.OrderBy(f => f.ModelFreqBand);
            var manufacturerSL = _context.ModelManufacturers.OrderBy(m => m.ModelManufacturerName);

            if (filterCategory == null)
            {
                ViewData["CategoryId"] = new SelectList(categorySL, "ModelCategoryId", "ModelCategory");
            }
            else
            {
                ViewData["CategoryId"] = new SelectList(categorySL, "ModelCategoryId", "ModelCategory", filterCategory);
                unitModels = unitModels.Where(u => u.ModelCategoryId.Equals(filterCategory));
            }

            if (filterFreqBand == null)
            {
                ViewData["FreqBandId"] = new SelectList(freqBandSL, "ModelFreqBandId", "ModelFreqBand");
            }
            else
            {
                ViewData["FreqBandId"] = new SelectList(freqBandSL, "ModelFreqBandId", "ModelFreqBand", filterFreqBand);
                unitModels = unitModels.Where(u => u.ModelFreqBandId.Equals(filterFreqBand));
            }

            if (filterManufacturer == null)
            {
                ViewData["ManufacturerId"] = new SelectList(manufacturerSL, "ModelManufacturerId", "ModelManufacturerName");
            }
            else
            {
                ViewData["ManufacturerId"] = new SelectList(manufacturerSL, "ModelManufacturerId", "ModelManufacturerName", filterManufacturer);
                unitModels = unitModels.Where(u => u.ModelManufacturerId.Equals(filterManufacturer));
            }

            ViewData["CategoryFilter"] = filterCategory;
            ViewData["FreqBandFilter"] = filterFreqBand;
            ViewData["ManufacturerFilter"] = filterManufacturer;
            ViewData["CurrentPageSize"] = pageSize;

            string pageAction = "Filter";

            var skyCommContext = unitModels
                .Include(u => u.ModelCategory)
                .Include(u => u.ModelFreqBand)
                .Include(u => u.ModelManufacturer)
                .Include(u => u.ModelModType);

            ViewData["RecordCount"] = skyCommContext.Count();

            return View(await PaginatedList<UnitModels>.CreateAsync(skyCommContext.AsNoTracking(), pageNumber ?? 1, pageSize ?? 12, pageAction));
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

            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
            return View(UnitModels);
        }

        // GET: UnitModels/Create
        public IActionResult Create()
        {
            ViewData["ModelCategoryId"] = new SelectList(_context.ModelCategories, "ModelCategoryId", "ModelCategory");
            ViewData["ModelFreqBandId"] = new SelectList(_context.ModelFreqBands, "ModelFreqBandId", "ModelFreqBand");
            ViewData["ModelManufacturerId"] = new SelectList(_context.ModelManufacturers, "ModelManufacturerId", "ModelManufacturerName");
            ViewData["ModelModTypeId"] = new SelectList(_context.ModelModTypes, "ModelModTypeId", "ModelModType");

            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
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

            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
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

            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
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

            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
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

            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
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
