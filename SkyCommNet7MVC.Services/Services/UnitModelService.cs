using SkyCommNet7MVC.Data.Interfaces;
using SkyCommNet7MVC.Domain.Models;
using SkyCommNet7MVC.Services.Interfaces;
using System.Linq.Expressions;

namespace SkyCommNet7MVC.Services.Services
{
    public class UnitModelService : IUnitModelService
    {
        private readonly IUnitModelRepository _unitModelRepository;

        public UnitModelService(IUnitModelRepository unitModelRepository)
        {
            _unitModelRepository = unitModelRepository;
        }

        public IOrderedQueryable<UnitModel> GetAllUnitModels()
        {
            return _unitModelRepository.GetAllUnitModels();
        }

        public UnitModel GetUnitModelById(int id)
        {
            return _unitModelRepository.GetUnitModelById(id);
        }

        public IQueryable<UnitModel> GetUnitModelsWhere(Expression<Func<UnitModel, bool>> filter)
        {
            return _unitModelRepository.GetUnitModelsWhere(filter);
        }

        public IEnumerable<UnitModel> GetUnitModelsSelectList()
        {
            return _unitModelRepository.GetUnitModelsSelectList();
        }

        public bool UnitModelExists(int id)
        {
            return _unitModelRepository.UnitModelExists(id);
        }
    }
}
