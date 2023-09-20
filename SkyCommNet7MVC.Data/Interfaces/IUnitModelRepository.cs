using SkyCommNet7MVC.Domain.Models;
using System.Linq.Expressions;
using static SkyCommNet7MVC.Data.Interfaces.IRepository;

namespace SkyCommNet7MVC.Data.Interfaces
{
    public interface IUnitModelRepository : IRepository<UnitModel>
    {
        public IOrderedQueryable<UnitModel> GetAllUnitModels();
        public UnitModel GetUnitModelById(int id);
        public IQueryable<UnitModel> GetUnitModelsWhere(Expression<Func<UnitModel, bool>> filter);
        public IEnumerable<UnitModel> GetUnitModelsSelectList();
        public bool UnitModelExists(int id);
    }
}
