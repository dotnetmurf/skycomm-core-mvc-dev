using SkyCommCoreMVC.Data.Repositories;
using SkyCommNet7MVC.Data.Interfaces;
using SkyCommNet7MVC.Domain.Models;
using System.Linq.Expressions;

namespace SkyCommNet7MVC.Data.Repositories
{
    public class ModelModTypeRepository : Repository<ModelModType>, IModelModTypeRepository
    {
        private readonly SkyCommDbContext _context;

        public ModelModTypeRepository(SkyCommDbContext context) : base(context)
        {
            _context = context;
        }

        public IOrderedQueryable<ModelModType> GetAllModelModTypes()
        {
            var allModelModTypes =
                from modelType in GetAll()
                orderby modelType.ModelModType1
                select modelType;

            return allModelModTypes;
        }

        public ModelModType GetModelModTypeById(int id)
        {
            return GetById(id);
        }

        public IQueryable<ModelModType> GetModelModTypesWhere(Expression<Func<ModelModType, bool>> filter)
        {
            var modelTypesWhere =
                from modelType in Where(filter)
                orderby modelType.ModelModType1
                select modelType;

            return modelTypesWhere;
        }

        public IEnumerable<ModelModType> GetModelModTypesSelectList()
        {
            return GetAllModelModTypes().AsEnumerable();
        }

        public bool ModelModTypeExists(int id)
        {
            return (_context.ModelModTypes?.Any(m => m.ModelModTypeId == id)).GetValueOrDefault();
        }
    }
}
