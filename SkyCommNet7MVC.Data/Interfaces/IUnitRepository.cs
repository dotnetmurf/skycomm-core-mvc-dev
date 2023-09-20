using SkyCommNet7MVC.Domain.Models;
using System.Linq.Expressions;
using static SkyCommNet7MVC.Data.Interfaces.IRepository;

namespace SkyCommNet7MVC.Data.Interfaces
{
    public interface IUnitRepository : IRepository<Unit>
    {
        public IOrderedQueryable<Unit> GetAllUnits();
        public Unit GetUnitById(int id);
        public IQueryable<Unit> GetUnitsWhere(Expression<Func<Unit, bool>> filter);
        public IEnumerable<Unit> GetUnitsSelectList();
        public bool UnitExists(int id);
    }
}
