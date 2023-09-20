using SkyCommNet7MVC.Data.Interfaces;
using SkyCommNet7MVC.Domain.Models;
using SkyCommNet7MVC.Services.Interfaces;
using System.Linq.Expressions;

namespace SkyCommNet7MVC.Services.Services
{
    public class ModelFreqBandService : IModelFreqBandService
    {
        private readonly IModelFreqBandRepository _modelFreqBandRepository;

        public ModelFreqBandService(IModelFreqBandRepository modelFreqBandRepository)
        {
            _modelFreqBandRepository = modelFreqBandRepository;
        }

        public IOrderedQueryable<ModelFreqBand> GetAllModelFreqBands()
        {
            return _modelFreqBandRepository.GetAllModelFreqBands();
        }

        public ModelFreqBand GetModelFreqBandById(int id)
        {
            return _modelFreqBandRepository.GetModelFreqBandById(id);
        }

        public IQueryable<ModelFreqBand> GetModelFreqBandsWhere(Expression<Func<ModelFreqBand, bool>> filter)
        {
            return _modelFreqBandRepository.GetModelFreqBandsWhere(filter);
        }

        public IEnumerable<ModelFreqBand> GetModelFreqBandsSelectList()
        {
            return _modelFreqBandRepository.GetModelFreqBandsSelectList();
        }

        public bool ModelFreqBandExists(int id)
        {
            return _modelFreqBandRepository.ModelFreqBandExists(id);
        }
    }
}
