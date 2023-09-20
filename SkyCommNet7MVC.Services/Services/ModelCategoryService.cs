using SkyCommNet7MVC.Data.Interfaces;
using SkyCommNet7MVC.Domain.Models;
using SkyCommNet7MVC.Services.Interfaces;
using System.Linq.Expressions;

namespace SkyCommNet7MVC.Services.Services
{
    public class ModelCategoryService : IModelCategoryService
    {
        private readonly IModelCategoryRepository _modelCategoryRepository;

        public ModelCategoryService(IModelCategoryRepository modelCategoryRepository)
        {
            _modelCategoryRepository = modelCategoryRepository;
        }

        public IOrderedQueryable<ModelCategory> GetAllModelCategories()
        {
            return _modelCategoryRepository.GetAllModelCategories();
        }

        public ModelCategory GetModelCategoryById(int id)
        {
            return _modelCategoryRepository.GetModelCategoryById(id);
        }

        public IQueryable<ModelCategory> GetModelCategoriesWhere(Expression<Func<ModelCategory, bool>> filter)
        {
            return _modelCategoryRepository.GetModelCategoriesWhere(filter);
        }

        public IEnumerable<ModelCategory> GetModelCategoriesSelectList()
        {
            return _modelCategoryRepository.GetModelCategoriesSelectList();
        }

        public bool ModelCategoryExists(int id)
        {
            return _modelCategoryRepository.ModelCategoryExists(id);
        }
    }
}
