using SkyCommNet7MVC.Domain.Models;
using System.Linq.Expressions;
using static SkyCommNet7MVC.Data.Interfaces.IRepository;

namespace SkyCommNet7MVC.Data.Interfaces
{
    public interface IRegionRepository : IRepository<Region>
    {
        public IOrderedQueryable<Region> GetAllRegions();
        public Region GetRegionById(int id);
        public IQueryable<Region> GetRegionsWhere(Expression<Func<Region, bool>> filter);
        public IEnumerable<Region> GetRegionsSelectList();
    }
}
