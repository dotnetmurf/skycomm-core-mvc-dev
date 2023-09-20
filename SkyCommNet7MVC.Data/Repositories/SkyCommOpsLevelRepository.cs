using SkyCommCoreMVC.Data.Repositories;
using SkyCommNet7MVC.Data.Interfaces;
using SkyCommNet7MVC.Domain.Models;

namespace SkyCommNet7MVC.Data.Repositories
{
    public class SkyCommOpsLevelRepository : Repository<SkyCommOpsLevel>, ISkyCommOpsLevelRepository
    {
        private readonly SkyCommDbContext _context;

        public SkyCommOpsLevelRepository(SkyCommDbContext context) : base(context)
        {
            _context = context;
        }

        public IOrderedQueryable<SkyCommOpsLevel> GetAllSkyCommOpsLevels()
        {
            var allSkyCommOpsLevels =
                from skyCommOpsLevel in GetAll()
                orderby skyCommOpsLevel.SkyCommOpsLevel1
                select skyCommOpsLevel;

            return allSkyCommOpsLevels;
        }

        public SkyCommOpsLevel GetAllSkyCommOpsLevelById(int id)
        {
            return GetById(id);
        }

        public IEnumerable<SkyCommOpsLevel> GetSkyCommOpsLevelsSelectList()
        {
            return GetAllSkyCommOpsLevels().AsEnumerable();
        }
    }
}
