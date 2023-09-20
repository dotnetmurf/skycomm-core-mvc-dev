using SkyCommNet7MVC.Data.Interfaces;
using SkyCommNet7MVC.Domain.Models;
using SkyCommNet7MVC.Services.Interfaces;
using System.Linq.Expressions;

namespace SkyCommNet7MVC.Services.Services
{
    public class ModelManufacturerService : IModelManufacturerService
    {
        private readonly IModelManufacturerRepository _modelManufacturerRepository;

        public ModelManufacturerService(IModelManufacturerRepository modelManufacturerRepository)
        {
            _modelManufacturerRepository = modelManufacturerRepository;
        }

        public IOrderedQueryable<ModelManufacturer> GetAllModelManufacturers()
        {
            return _modelManufacturerRepository.GetAllModelManufacturers();
        }

        public ModelManufacturer GetModelManufacturerById(int id)
        {
            return _modelManufacturerRepository.GetModelManufacturerById(id);
        }

        public IQueryable<ModelManufacturer> GetModelManufacturersWhere(Expression<Func<ModelManufacturer, bool>> filter)
        {
            return _modelManufacturerRepository.GetModelManufacturersWhere(filter);
        }

        public IEnumerable<ModelManufacturer> GetModelManufacturersSelectList()
        {
            return _modelManufacturerRepository.GetModelManufacturersSelectList();
        }

        public bool ModelManufacturerExists(int id)
        {
            return _modelManufacturerRepository.ModelManufacturerExists(id);
        }
    }
}
