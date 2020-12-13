using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SkyCommCoreMVC.Infrastructure;
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

        // GET: Employees/Filter
        public async Task<IActionResult> Filter(int? filterJobTitle, int? filterDepartment, int? filterOffice, int? pageNumber, int? pageSize)
        {
            var employees = from e in _context.Employees select e;
            var jobTitleSL = _context.JobTitles.OrderBy(j => j.JobTitle);
            var departmentSL = _context.Departments.OrderBy(d => d.DepartmentName);
            var officeSL = _context.Offices.OrderBy(o => o.OfficeName);

            if (filterJobTitle == null)
            {
                ViewData["JobTitleID"] = new SelectList(jobTitleSL, "JobTitleId", "JobTitle");
            }
            else
            {
                ViewData["JobTitleID"] = new SelectList(jobTitleSL, "JobTitleId", "JobTitle", filterJobTitle);
                employees = employees.Where(e => e.JobTitleId.Equals(filterJobTitle));
            }

            if (filterDepartment == null)
            {
                ViewData["DepartmentID"] = new SelectList(departmentSL, "DepartmentId", "DepartmentName");
            }
            else
            {
                ViewData["DepartmentID"] = new SelectList(departmentSL, "DepartmentId", "DepartmentName", filterDepartment);
                employees = employees.Where(e => e.JobTitle.DepartmentId.Equals(filterDepartment));
            }

            if (filterOffice == null)
            {
                ViewData["OfficeID"] = new SelectList(officeSL, "OfficeId", "OfficeName");
            }
            else
            {
                ViewData["OfficeID"] = new SelectList(officeSL, "OfficeId", "OfficeName", filterOffice);
                employees = employees.Where(e => e.OfficeId.Equals(filterOffice));
            }

            ViewData["JobTitleFilter"] = filterJobTitle;
            ViewData["DepartmentFilter"] = filterDepartment;
            ViewData["OfficeFilter"] = filterOffice;
            ViewData["CurrentPageSize"] = pageSize;

            string pageAction = "Filter";

            var skyCommContext = employees
                .Include(e => e.JobTitle)
                    .ThenInclude(e => e.Department)
                .Include(e => e.Office);

            ViewData["RecordCount"] = skyCommContext.Count();

            return View(await PaginatedList<Employees>.CreateAsync(skyCommContext.AsNoTracking(), pageNumber ?? 1, pageSize ?? 5, pageAction));
        }

        // GET: Employees/Search
        public async Task<IActionResult> Search(string searchString, string searchName, int? pageNumber, int? pageSize)
        {
            ViewData["SearchString"] = searchString;
            ViewData["SearchName"] = searchName;

            ViewData["CurrentPageSize"] = pageSize;

            string pageAction = "Search";

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

            var skyCommContext = employees
                .Include(e => e.JobTitle)
                    .ThenInclude(e => e.Department)
                .Include(e => e.Office);

            ViewData["RecordCount"] = skyCommContext.Count();

            return View(await PaginatedList<Employees>.CreateAsync(skyCommContext.AsNoTracking(), pageNumber ?? 1, pageSize ?? 5, pageAction));
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

            string email = employees.EmailAddress.ToString();

            StringBuilder emailUriBuilder = new StringBuilder("mailto:");
            emailUriBuilder.Append(email);
            ViewData["EMailURI"] = emailUriBuilder;

            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
            return View(employees);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewData["JobTitleId"] = new SelectList(_context.JobTitles, "JobTitleId", "JobTitle");
            ViewData["OfficeId"] = new SelectList(_context.Offices, "OfficeId", "OfficeName");

            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
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
            ViewData["JobTitleId"] = new SelectList(_context.JobTitles, "JobTitleId", "JobTitle", employees.JobTitleId);
            ViewData["OfficeId"] = new SelectList(_context.Offices, "OfficeId", "OfficeName", employees.OfficeId);

            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
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
            ViewData["JobTitleId"] = new SelectList(_context.JobTitles, "JobTitleId", "JobTitle", employees.JobTitleId);
            ViewData["OfficeId"] = new SelectList(_context.Offices, "OfficeId", "OfficeName", employees.OfficeId);

            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
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
            ViewData["JobTitleId"] = new SelectList(_context.JobTitles, "JobTitleId", "JobTitle", employees.JobTitleId);
            ViewData["OfficeId"] = new SelectList(_context.Offices, "OfficeId", "OfficeName", employees.OfficeId);

            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
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

            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
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
