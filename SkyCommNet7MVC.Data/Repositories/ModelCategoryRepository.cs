using Microsoft.EntityFrameworkCore;
using SkyCommCoreMVC.Data.Repositories;
using SkyCommNet7MVC.Data.Interfaces;
using SkyCommNet7MVC.Domain.Models;
using System.Linq.Expressions;

namespace SkyCommNet7MVC.Data.Repositories
{
    public class ModelCategoryRepository : Repository<ModelCategory>, IModelCategoryRepository
    {
        private readonly SkyCommDbContext _context;

        public ModelCategoryRepository(SkyCommDbContext context) : base(context)
        {
            _context = context;
        }

        public IOrderedQueryable<ModelCategory> GetAllModelCategories()
        {
            var allModelCategories =
                from modelCategory in GetAll().
                    Include(m => m.ModelType)
                orderby modelCategory.ModelCategory1
                select modelCategory;

            return allModelCategories;
        }

        public ModelCategory GetModelCategoryById(int id)
        {
            return GetById(id);
        }

        public IQueryable<ModelCategory> GetModelCategoriesWhere(Expression<Func<ModelCategory, bool>> filter)
        {
            var modelCategoriesWhere =
                from modelCategory in Where(filter).
                    Include(m => m.ModelType)
                orderby modelCategory.ModelCategory1
                select modelCategory;

            return modelCategoriesWhere;
        }

        public IEnumerable<ModelCategory> GetModelCategoriesSelectList()
        {
            var modelCategoriesSL =
                from modelCategory in GetAll()
                orderby modelCategory.ModelCategory1
                select modelCategory;

            return modelCategoriesSL;
        }

        public bool ModelCategoryExists(int id)
        {
            return (_context.ModelCategories?.Any(m => m.ModelCategoryId == id)).GetValueOrDefault();
        }
    }
}
