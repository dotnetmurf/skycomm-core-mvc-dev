using SkyCommNet7MVC.Domain.Models;
using System.Linq.Expressions;

namespace SkyCommNet7MVC.Services.Interfaces
{
    public interface IUnitModelService
    {
        public IOrderedQueryable<UnitModel> GetAllUnitModels();
        public UnitModel GetUnitModelById(int id);
        public IQueryable<UnitModel> GetUnitModelsWhere(Expression<Func<UnitModel, bool>> filter);
        public IEnumerable<UnitModel> GetUnitModelsSelectList();
        public bool UnitModelExists(int id);
    }
}
