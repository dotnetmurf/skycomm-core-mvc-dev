using Microsoft.EntityFrameworkCore;
using SkyCommCoreMVC.Data.Repositories;
using SkyCommNet7MVC.Data.Interfaces;
using SkyCommNet7MVC.Domain.Models;
using System.Linq.Expressions;

namespace SkyCommNet7MVC.Data.Repositories
{
    public class RegionRepository : Repository<Region>, IRegionRepository
    {
        private readonly SkyCommDbContext _context;

        public RegionRepository(SkyCommDbContext context) : base(context)
        {
            _context = context;
        }

        public IOrderedQueryable<Region> GetAllRegions()
        {
            var allRegions =
                from Region in GetAll().AsQueryable().
                    Include(r => r.Country).
                    Include(r => r.Country.Continent)
                orderby Region.RegionCode
                select Region;

            return allRegions;
        }

        public Region GetRegionById(int id)
        {
            return GetAllRegions().FirstOrDefault(r => r.RegionId == id);
        }

        public IQueryable<Region> GetRegionsWhere(Expression<Func<Region, bool>> filter)
        {
            var filteredRegions =
                from Region in Where(filter).
                    Include(r => r.Country).
                    Include(r => r.Country.Continent)
                orderby Region.RegionCode
                select Region;

            return filteredRegions;
        }

        public IEnumerable<Region> GetRegionsSelectList()
        {
            var regionSelectList =
                from region in GetAll()
                orderby region.RegionCode
                select region;

            return regionSelectList;
        }
    }
}
