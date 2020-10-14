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
    public class EmployeesController : Controller
    {
        private readonly SkyCommDBContext _context;

        public EmployeesController(SkyCommDBContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index(string searchString, string searchName)
        {
            ViewData["CurrentFilter"] = searchString;

            var employees = from e in _context.Employees select e;

            if (!String.IsNullOrEmpty(searchString))
            {
                if (searchName == "First")
                {
                    employees = employees.Where(e => e.FirstName.Contains(searchString));
                }
                else if (searchName == "Last")
                {
                    employees = employees.Where(e => e.LastName.Contains(searchString));

                }
                else 
                {
                    employees = employees.Where(e => e.LastName.Contains(searchString)
                   || e.FirstName.Contains(searchString));

                }
            }

            employees = employees
                .Include(e => e.JobTitle)
                .Include(e => e.Office);


            return View(await employees.AsNoTracking().ToListAsync());
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employees = await _context.Employees
                .Include(e => e.JobTitle)
                .Include(e => e.Office)
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employees == null)
            {
                return NotFound();
            }

            return View(employees);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewData["JobTitleId"] = new SelectList(_context.JobTitles, "JobTitleId", "JobTitleId");
            ViewData["OfficeId"] = new SelectList(_context.Offices, "OfficeId", "OfficeAddress1");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,FirstName,MiddleName,LastName,JobTitleId,PhoneNumber,EmailAddress,OfficeId,ImageFileName")] Employees employees)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employees);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["JobTitleId"] = new SelectList(_context.JobTitles, "JobTitleId", "JobTitleId", employees.JobTitleId);
            ViewData["OfficeId"] = new SelectList(_context.Offices, "OfficeId", "OfficeAddress1", employees.OfficeId);
            return View(employees);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employees = await _context.Employees.FindAsync(id);
            if (employees == null)
            {
                return NotFound();
            }
            ViewData["JobTitleId"] = new SelectList(_context.JobTitles, "JobTitleId", "JobTitleId", employees.JobTitleId);
            ViewData["OfficeId"] = new SelectList(_context.Offices, "OfficeId", "OfficeAddress1", employees.OfficeId);
            return View(employees);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeId,FirstName,MiddleName,LastName,JobTitleId,PhoneNumber,EmailAddress,OfficeId,ImageFileName")] Employees employees)
        {
            if (id != employees.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employees);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeesExists(employees.EmployeeId))
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
            ViewData["JobTitleId"] = new SelectList(_context.JobTitles, "JobTitleId", "JobTitleId", employees.JobTitleId);
            ViewData["OfficeId"] = new SelectList(_context.Offices, "OfficeId", "OfficeAddress1", employees.OfficeId);
            return View(employees);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employees = await _context.Employees
                .Include(e => e.JobTitle)
                .Include(e => e.Office)
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employees == null)
            {
                return NotFound();
            }

            return View(employees);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employees = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(employees);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeesExists(int id)
        {
            return _context.Employees.Any(e => e.EmployeeId == id);
        }
    }
}
