using SkyCommNet7MVC.Domain.Models;
using System.Linq.Expressions;

namespace SkyCommNet7MVC.Services.Interfaces
{
    public interface IUnitService
    {
        public IOrderedQueryable<Unit> GetAllUnits();
        public Unit GetUnitById(int id);
        public IQueryable<Unit> GetUnitsWhere(Expression<Func<Unit, bool>> filter);
        public IEnumerable<Unit> GetUnitsSelectList();
        public bool UnitExists(int id);
    }
}
