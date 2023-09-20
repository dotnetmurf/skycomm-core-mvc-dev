using SkyCommNet7MVC.Domain.Models;
using System.Linq.Expressions;
using static SkyCommNet7MVC.Data.Interfaces.IRepository;

namespace SkyCommNet7MVC.Data.Interfaces
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        public IOrderedQueryable<Employee> GetAllEmployees();
        public Employee GetEmployeeById(int id);
        public IQueryable<Employee> GetEmployeesWhere(Expression<Func<Employee, bool>> filter);
        public bool EmployeeExists(int id);
    }
}
