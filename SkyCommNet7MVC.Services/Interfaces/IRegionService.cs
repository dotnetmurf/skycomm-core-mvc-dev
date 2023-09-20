using SkyCommNet7MVC.Domain.Models;
using System.Linq.Expressions;

namespace SkyCommNet7MVC.Services.Interfaces
{
    public interface IRegionService
    {
        public IOrderedQueryable<Region> GetAllRegions();
        public Region GetRegionById(int id);
        public IQueryable<Region> GetRegionsWhere(Expression<Func<Region, bool>> filter);
        public IEnumerable<Region> GetRegionsSelectList();
    }
}
