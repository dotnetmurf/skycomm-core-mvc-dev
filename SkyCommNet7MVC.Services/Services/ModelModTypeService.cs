using SkyCommNet7MVC.Data.Interfaces;
using SkyCommNet7MVC.Domain.Models;
using SkyCommNet7MVC.Services.Interfaces;
using System.Linq.Expressions;

namespace SkyCommNet7MVC.Services.Services
{
    public class ModelTypeService : IModelTypeService
    {
        private readonly IModelTypeRepository _modelTypeRepository;

        public ModelTypeService(IModelTypeRepository modelTypeRepository)
        {
            _modelTypeRepository = modelTypeRepository;
        }

        public IOrderedQueryable<ModelType> GetAllModelTypes()
        {
            return _modelTypeRepository.GetAllModelTypes();
        }

        public ModelType GetModelTypeById(int id)
        {
            return _modelTypeRepository.GetModelTypeById(id);
        }

        public IQueryable<ModelType> GetModelTypesWhere(Expression<Func<ModelType, bool>> filter)
        {
            return _modelTypeRepository.GetModelTypesWhere(filter);
        }

        public IEnumerable<ModelType> GetModelTypesSelectList()
        {
            return _modelTypeRepository.GetModelTypesSelectList();
        }

        public bool ModelTypeExists(int id)
        {
            return _modelTypeRepository.ModelTypeExists(id);
        }
    }
}
