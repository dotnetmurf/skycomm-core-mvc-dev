using SkyCommNet7MVC.Domain.Models;
using System.Linq.Expressions;

namespace SkyCommNet7MVC.Services.Interfaces
{
    public interface IDepartmentService
    {
        public IOrderedQueryable<Department> GetAllDepartments();
        public Department GetDepartmentById(int id);
        public IQueryable<Department> GetDepartmentsWhere(Expression<Func<Department, bool>> filter);
        public IEnumerable<Department> GetDepartmentsSelectList();
    }
}
