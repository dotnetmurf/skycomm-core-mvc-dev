using SkyCommNet7MVC.Domain.Models;
using System.Linq.Expressions;

namespace SkyCommNet7MVC.Services.Interfaces
{
    public interface IModelModTypeService
    {
        public IOrderedQueryable<ModelModType> GetAllModelModTypes();
        public ModelModType GetModelModTypeById(int id);
        public IQueryable<ModelModType> GetModelModTypesWhere(Expression<Func<ModelModType, bool>> filter);
        public IEnumerable<ModelModType> GetModelModTypesSelectList();
        public bool ModelModTypeExists(int id);
    }
}
