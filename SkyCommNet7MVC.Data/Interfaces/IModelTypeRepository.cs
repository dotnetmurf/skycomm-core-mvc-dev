using SkyCommNet7MVC.Domain.Models;
using System.Linq.Expressions;

namespace SkyCommNet7MVC.Data.Interfaces
{
    public interface IModelTypeRepository
    {
        public IOrderedQueryable<ModelType> GetAllModelTypes();
        public ModelType GetModelTypeById(int id);
        public IQueryable<ModelType> GetModelTypesWhere(Expression<Func<ModelType, bool>> filter);
        public IEnumerable<ModelType> GetModelTypesSelectList();
        public bool ModelTypeExists(int id);
    }
}
