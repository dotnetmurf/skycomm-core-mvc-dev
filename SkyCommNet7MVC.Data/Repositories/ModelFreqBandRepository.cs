using SkyCommCoreMVC.Data.Repositories;
using SkyCommNet7MVC.Data.Interfaces;
using SkyCommNet7MVC.Domain.Models;
using System.Linq.Expressions;

namespace SkyCommNet7MVC.Data.Repositories
{
    public class ModelFreqBandRepository : Repository<ModelFreqBand>, IModelFreqBandRepository
    {
        private readonly SkyCommDbContext _context;

        public ModelFreqBandRepository(SkyCommDbContext context) : base(context)
        {
            _context = context;
        }

        public IOrderedQueryable<ModelFreqBand> GetAllModelFreqBands()
        {
            var allModelFreqBands =
                from modelFreqBand in GetAll()
                orderby modelFreqBand.ModelFreqBand1
                select modelFreqBand;

            return allModelFreqBands;
        }

        public ModelFreqBand GetModelFreqBandById(int id)
        {
            return GetById(id);
        }

        public IQueryable<ModelFreqBand> GetModelFreqBandsWhere(Expression<Func<ModelFreqBand, bool>> filter)
        {
            var modelFreqBandsWhere =
                from modelFreqBand in Where(filter)
                orderby modelFreqBand.ModelFreqBand1
                select modelFreqBand;

            return modelFreqBandsWhere;
        }

        public IEnumerable<ModelFreqBand> GetModelFreqBandsSelectList()
        {
            return GetAllModelFreqBands().AsEnumerable();
        }

        public bool ModelFreqBandExists(int id)
        {
            return (_context.ModelFreqBands?.Any(m => m.ModelFreqBandId == id)).GetValueOrDefault();
        }
    }
}
