using SkyCommNet7MVC.Domain.Models;
using static SkyCommNet7MVC.Data.Interfaces.IRepository;

namespace SkyCommNet7MVC.Data.Interfaces
{
    public interface ISkyCommOpsLevelRepository : IRepository<SkyCommOpsLevel>
    {
        public IOrderedQueryable<SkyCommOpsLevel> GetAllSkyCommOpsLevels();
        public SkyCommOpsLevel GetAllSkyCommOpsLevelById(int id);
        public IEnumerable<SkyCommOpsLevel> GetSkyCommOpsLevelsSelectList();
    }
}
