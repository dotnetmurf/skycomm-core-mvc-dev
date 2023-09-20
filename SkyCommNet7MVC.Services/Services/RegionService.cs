using SkyCommNet7MVC.Data.Interfaces;
using SkyCommNet7MVC.Domain.Models;
using SkyCommNet7MVC.Services.Interfaces;
using System.Linq.Expressions;

namespace SkyCommNet7MVC.Services.Services
{
    public class RegionService : IRegionService
    {
        private readonly IRegionRepository _regionRepository;

        public RegionService(IRegionRepository regionRepository)
        {
            _regionRepository = regionRepository;
        }

        public IOrderedQueryable<Region> GetAllRegions()
        {
            return _regionRepository.GetAllRegions();
        }

        public Region GetRegionById(int id)
        {
            return _regionRepository.GetRegionById(id);
        }

        public IQueryable<Region> GetRegionsWhere(Expression<Func<Region, bool>> filter)
        {
            return _regionRepository.GetRegionsWhere(filter);
        }

        public IEnumerable<Region> GetRegionsSelectList()
        {
            return _regionRepository.GetRegionsSelectList();
        }
    }
}
