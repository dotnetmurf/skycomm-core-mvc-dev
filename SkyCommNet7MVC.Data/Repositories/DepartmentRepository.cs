using SkyCommCoreMVC.Data.Repositories;
using SkyCommNet7MVC.Data.Interfaces;
using SkyCommNet7MVC.Domain.Models;
using System.Linq.Expressions;

namespace SkyCommNet7MVC.Data.Repositories
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        private readonly SkyCommDbContext _context;

        public DepartmentRepository(SkyCommDbContext context) : base(context)
        {
            _context = context;
        }

        public IOrderedQueryable<Department> GetAllDepartments()
        {
            var allDepartments =
                from department in GetAll()
                orderby department.DepartmentName
                select department;

            return allDepartments;
        }

        public Department GetDepartmentById(int id)
        {
            return GetById(id);
        }

        public IQueryable<Department> GetDepartmentsWhere(Expression<Func<Department, bool>> filter)
        {
            var departmentsWhere =
                from department in Where(filter)
                orderby department.DepartmentName
                select department;

            return departmentsWhere;
        }

        public IEnumerable<Department> GetDepartmentsSelectList()
        {
            return GetAllDepartments().AsEnumerable();
        }
    }
}
