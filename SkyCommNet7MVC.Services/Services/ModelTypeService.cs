using SkyCommNet7MVC.Data.Interfaces;
using SkyCommNet7MVC.Domain.Models;
using SkyCommNet7MVC.Services.Interfaces;
using System.Linq.Expressions;

namespace SkyCommNet7MVC.Services.Services
{
    public class ModelModTypeService : IModelModTypeService
    {
        private readonly IModelModTypeRepository _modelModTypeRepository;

        public ModelModTypeService(IModelModTypeRepository modelModTypeRepository)
        {
            _modelModTypeRepository = modelModTypeRepository;
        }

        public IOrderedQueryable<ModelModType> GetAllModelModTypes()
        {
            return _modelModTypeRepository.GetAllModelModTypes();
        }

        public ModelModType GetModelModTypeById(int id)
        {
            return _modelModTypeRepository.GetModelModTypeById(id);
        }

        public IQueryable<ModelModType> GetModelModTypesWhere(Expression<Func<ModelModType, bool>> filter)
        {
            return _modelModTypeRepository.GetModelModTypesWhere(filter);
        }

        public IEnumerable<ModelModType> GetModelModTypesSelectList()
        {
            return _modelModTypeRepository.GetModelModTypesSelectList();
        }

        public bool ModelModTypeExists(int id)
        {
            return _modelModTypeRepository.ModelModTypeExists(id);
        }
    }
}
