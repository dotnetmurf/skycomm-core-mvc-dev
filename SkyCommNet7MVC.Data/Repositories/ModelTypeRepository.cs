using SkyCommCoreMVC.Data.Repositories;
using SkyCommNet7MVC.Data.Interfaces;
using SkyCommNet7MVC.Domain.Models;
using System.Linq.Expressions;

namespace SkyCommNet7MVC.Data.Repositories
{
    public class ModelTypeRepository : Repository<ModelType>, IModelTypeRepository
    {
        private readonly SkyCommDbContext _context;

        public ModelTypeRepository(SkyCommDbContext context) : base(context)
        {
            _context = context;
        }

        public IOrderedQueryable<ModelType> GetAllModelTypes()
        {
            var allModelTypes =
                from modelType in GetAll()
                orderby modelType.ModelType1
                select modelType;

            return allModelTypes;
        }

        public ModelType GetModelTypeById(int id)
        {
            return GetById(id);
        }

        public IQueryable<ModelType> GetModelTypesWhere(Expression<Func<ModelType, bool>> filter)
        {
            var modelTypesWhere =
                from modelType in Where(filter)
                orderby modelType.ModelType1
                select modelType;

            return modelTypesWhere;
        }

        public IEnumerable<ModelType> GetModelTypesSelectList()
        {
            return GetAllModelTypes().AsEnumerable();
        }

        public bool ModelTypeExists(int id)
        {
            return (_context.ModelTypes?.Any(m => m.ModelTypeId == id)).GetValueOrDefault();
        }
    }
}
