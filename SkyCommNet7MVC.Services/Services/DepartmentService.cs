using SkyCommNet7MVC.Data.Interfaces;
using SkyCommNet7MVC.Domain.Models;
using SkyCommNet7MVC.Services.Interfaces;
using System.Linq.Expressions;

namespace SkyCommNet7MVC.Services.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public IOrderedQueryable<Department> GetAllDepartments()
        {
            return _departmentRepository.GetAllDepartments();
        }

        public Department GetDepartmentById(int id)
        {
            return _departmentRepository.GetDepartmentById(id);
        }

        public IQueryable<Department> GetDepartmentsWhere(Expression<Func<Department, bool>> filter)
        {
            return _departmentRepository.GetDepartmentsWhere(filter);
        }

        public IEnumerable<Department> GetDepartmentsSelectList()
        {
            return GetAllDepartments().AsEnumerable();
        }
    }
}
