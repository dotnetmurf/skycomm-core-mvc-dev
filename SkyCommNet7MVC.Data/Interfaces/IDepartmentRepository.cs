using SkyCommNet7MVC.Domain.Models;
using System.Linq.Expressions;
using static SkyCommNet7MVC.Data.Interfaces.IRepository;

namespace SkyCommNet7MVC.Data.Interfaces
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        public IOrderedQueryable<Department> GetAllDepartments();
        public Department GetDepartmentById(int id);
        public IQueryable<Department> GetDepartmentsWhere(Expression<Func<Department, bool>> filter);
        public IEnumerable<Department> GetDepartmentsSelectList();
    }
}
