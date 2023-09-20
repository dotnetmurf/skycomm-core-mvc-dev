using SkyCommNet7MVC.Domain.Models;
using System.Linq.Expressions;

namespace SkyCommNet7MVC.Services.Interfaces
{
    public interface IModelFreqBandService
    {
        public IOrderedQueryable<ModelFreqBand> GetAllModelFreqBands();
        public ModelFreqBand GetModelFreqBandById(int id);
        public IQueryable<ModelFreqBand> GetModelFreqBandsWhere(Expression<Func<ModelFreqBand, bool>> filter);
        public IEnumerable<ModelFreqBand> GetModelFreqBandsSelectList();
        public bool ModelFreqBandExists(int id);
    }
}
