using SkyCommCoreMVC.Data.Repositories;
using SkyCommNet7MVC.Data.Interfaces;
using SkyCommNet7MVC.Domain.Models;
using System.Linq.Expressions;

namespace SkyCommNet7MVC.Data.Repositories
{
    public class ModelManufacturerRepository : Repository<ModelManufacturer>, IModelManufacturerRepository
    {
        private readonly SkyCommDbContext _context;

        public ModelManufacturerRepository(SkyCommDbContext context) : base(context)
        {
            _context = context;
        }

        public IOrderedQueryable<ModelManufacturer> GetAllModelManufacturers()
        {
            var allModelManufacturers =
                from modelManufacturer in GetAll()
                orderby modelManufacturer.ModelManufacturerName
                select modelManufacturer;

            return allModelManufacturers;
        }

        public ModelManufacturer GetModelManufacturerById(int id)
        {
            return GetById(id);
        }

        public IQueryable<ModelManufacturer> GetModelManufacturersWhere(Expression<Func<ModelManufacturer, bool>> filter)
        {
            var modelManufacturersWhere =
                from modelManufacturer in Where(filter)
                orderby modelManufacturer.ModelManufacturerName
                select modelManufacturer;

            return modelManufacturersWhere;
        }

        public IEnumerable<ModelManufacturer> GetModelManufacturersSelectList()
        {
            return GetAllModelManufacturers().AsEnumerable();
        }

        public bool ModelManufacturerExists(int id)
        {
            return (_context.ModelManufacturers?.Any(m => m.ModelManufacturerId == id)).GetValueOrDefault();
        }
    }
}
