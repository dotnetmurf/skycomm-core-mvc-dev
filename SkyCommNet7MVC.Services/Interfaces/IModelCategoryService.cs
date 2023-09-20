using SkyCommNet7MVC.Domain.Models;
using System.Linq.Expressions;

namespace SkyCommNet7MVC.Services.Interfaces
{
    public interface IModelCategoryService
    {
        public IOrderedQueryable<ModelCategory> GetAllModelCategories();
        public ModelCategory GetModelCategoryById(int id);
        public IQueryable<ModelCategory> GetModelCategoriesWhere(Expression<Func<ModelCategory, bool>> filter);
        public IEnumerable<ModelCategory> GetModelCategoriesSelectList();
        public bool ModelCategoryExists(int id);
    }
}
