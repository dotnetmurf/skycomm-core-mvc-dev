using SkyCommNet7MVC.Domain.Models;

namespace SkyCommNet7MVC.Services.Interfaces
{
    public interface ISkyCommOpsLevelService
    {
        public IOrderedQueryable<SkyCommOpsLevel> GetAllSkyCommOpsLevels();
        public SkyCommOpsLevel GetAllSkyCommOpsLevelById(int id);
        public IEnumerable<SkyCommOpsLevel> GetSkyCommOpsLevelsSelectList();
    }
}
