using SkyCommNet7MVC.Domain.Models;
using System.Linq.Expressions;

namespace SkyCommNet7MVC.Services.Interfaces
{
    public interface IEmployeeService
    {
        public IOrderedQueryable<Employee> GetAllEmployees();
        public Employee GetEmployeeById(int id);
        public IQueryable<Employee> GetEmployeesWhere(Expression<Func<Employee, bool>> filter);
        public bool EmployeeExists(int id);
    }
}
