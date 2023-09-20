using SkyCommNet7MVC.Data.Interfaces;
using SkyCommNet7MVC.Domain.Models;
using SkyCommNet7MVC.Services.Interfaces;
using System.Linq.Expressions;

namespace SkyCommNet7MVC.Services.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public IOrderedQueryable<Employee> GetAllEmployees()
        {
            return _employeeRepository.GetAllEmployees();
        }

        public Employee GetEmployeeById(int id)
        {
            return _employeeRepository.GetEmployeeById(id);
        }

        public IQueryable<Employee> GetEmployeesWhere(Expression<Func<Employee, bool>> filter)
        {
            return _employeeRepository.GetEmployeesWhere(filter);
        }

        public bool EmployeeExists(int id)
        {
            return _employeeRepository.EmployeeExists(id);
        }
    }
}
