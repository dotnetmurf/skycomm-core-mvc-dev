using Microsoft.EntityFrameworkCore;
using SkyCommCoreMVC.Data.Repositories;
using SkyCommNet7MVC.Data.Interfaces;
using SkyCommNet7MVC.Domain.Models;
using System.Linq.Expressions;

namespace SkyCommNet7MVC.Data.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        private readonly SkyCommDbContext _context;

        public EmployeeRepository(SkyCommDbContext context) : base(context)
        {
            _context = context;
        }

        public IOrderedQueryable<Employee> GetAllEmployees()
        {
            var allEmployees =
                from employee in GetAll().AsQueryable().
                    Include(e => e.JobTitle).
                        ThenInclude(e => e.Department).
                    Include(e => e.Office)
                orderby employee.LastName, employee.FirstName
                select employee;

            return allEmployees;
        }

        public Employee GetEmployeeById(int id)
        {
            return GetAllEmployees().FirstOrDefault(e => e.EmployeeId == id);
        }

        public IQueryable<Employee> GetEmployeesWhere(Expression<Func<Employee, bool>> filter)
        {
            var employeesWhere =
                from employee in Where(filter).
                    Include(e => e.JobTitle).
                        ThenInclude(e => e.Department).
                    Include(e => e.Office)
                orderby employee.LastName, employee.FirstName
                select employee;

            return employeesWhere;
        }

        public bool EmployeeExists(int id)
        {
            return (_context.Employees?.Any(e => e.EmployeeId == id)).GetValueOrDefault();
        }
    }
}
