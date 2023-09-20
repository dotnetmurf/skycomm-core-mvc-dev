using Microsoft.EntityFrameworkCore;
using SkyCommCoreMVC.Data.Repositories;
using SkyCommNet7MVC.Data.Interfaces;
using SkyCommNet7MVC.Domain.Models;
using System.Linq.Expressions;

namespace SkyCommNet7MVC.Data.Repositories
{
    public class UnitModelRepository : Repository<UnitModel>, IUnitModelRepository
    {
        private readonly SkyCommDbContext _context;

        public UnitModelRepository(SkyCommDbContext context) : base(context)
        {
            _context = context;
        }

        public IOrderedQueryable<UnitModel> GetAllUnitModels()
        {
            var allUnitModels =
                from unitModel in GetAll().AsQueryable().
                    Include(u => u.ModelCategory).
                    Include(u => u.ModelFreqBand).
                    Include(u => u.ModelManufacturer).
                    Include(u => u.ModelModType)
                orderby unitModel.UnitModelId
                select unitModel;

            return allUnitModels;
        }

        public UnitModel GetUnitModelById(int id)
        {
            return GetAllUnitModels().FirstOrDefault(u => u.UnitModelId == id);
        }

        public IQueryable<UnitModel> GetUnitModelsWhere(Expression<Func<UnitModel, bool>> filter)
        {
            var unitsWhere =
                from unitModel in Where(filter).
                    Include(u => u.ModelCategory).
                    Include(u => u.ModelFreqBand).
                    Include(u => u.ModelManufacturer).
                    Include(u => u.ModelModType)
                orderby unitModel.ModelName
                select unitModel;

            return unitsWhere;
        }

        public IEnumerable<UnitModel> GetUnitModelsSelectList()
        {
            var unitModelsSL =
                from unitModel in GetAll()
                orderby unitModel.ModelName
                select unitModel;

            return unitModelsSL.AsEnumerable();
        }

        public bool UnitModelExists(int id)
        {
            return (_context.UnitModels?.Any(u => u.UnitModelId == id)).GetValueOrDefault();
        }
    }
}
