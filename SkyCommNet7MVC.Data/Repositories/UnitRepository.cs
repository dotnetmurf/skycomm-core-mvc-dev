using Microsoft.EntityFrameworkCore;
using SkyCommCoreMVC.Data.Repositories;
using SkyCommNet7MVC.Data.Interfaces;
using SkyCommNet7MVC.Domain.Models;
using System.Linq.Expressions;

namespace SkyCommNet7MVC.Data.Repositories
{
    public class UnitRepository : Repository<Unit>, IUnitRepository
    {
        private readonly SkyCommDbContext _context;

        public UnitRepository(SkyCommDbContext context) : base(context)
        {
            _context = context;
        }

        public IOrderedQueryable<Unit> GetAllUnits()
        {
            var allUnits =
                from unit in GetAll().AsQueryable().
                    Include(u => u.Airport).
                    Include(u => u.UnitModel)
                orderby unit.UnitId
                select unit;

            return allUnits;
        }

        public Unit GetUnitById(int id)
        {
            return GetAllUnits().FirstOrDefault(u => u.UnitId == id);
        }

        public IQueryable<Unit> GetUnitsWhere(Expression<Func<Unit, bool>> filter)
        {
            var unitsWhere =
                from unit in Where(filter).
                    Include(u => u.Airport).
                    Include(u => u.UnitModel)
                orderby unit.UnitId
                select unit;

            return unitsWhere;
        }

        public IEnumerable<Unit> GetUnitsSelectList()
        {
            var unitsSL =
                from unit in GetAll()
                orderby unit.UnitId
                select unit;

            return unitsSL.AsEnumerable();
        }

        public bool UnitExists(int id)
        {
            return (_context.Units?.Any(u => u.UnitId == id)).GetValueOrDefault();
        }
    }
}
