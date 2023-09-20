using SkyCommNet7MVC.Data.Interfaces;
using SkyCommNet7MVC.Domain.Models;
using SkyCommNet7MVC.Services.Interfaces;

namespace SkyCommNet7MVC.Services.Services
{
    public class SkyCommOpsLevelService : ISkyCommOpsLevelService
    {
        private readonly ISkyCommOpsLevelRepository _skyCommOpsLevelRepository;

        public SkyCommOpsLevelService(ISkyCommOpsLevelRepository skyCommOpsLevelRepository)
        {
            _skyCommOpsLevelRepository = skyCommOpsLevelRepository;
        }

        public IOrderedQueryable<SkyCommOpsLevel> GetAllSkyCommOpsLevels()
        {
            return _skyCommOpsLevelRepository.GetAllSkyCommOpsLevels();
        }

        public SkyCommOpsLevel GetAllSkyCommOpsLevelById(int id)
        {
            return _skyCommOpsLevelRepository.GetAllSkyCommOpsLevelById(id);
        }

        public IEnumerable<SkyCommOpsLevel> GetSkyCommOpsLevelsSelectList()
        {
            return _skyCommOpsLevelRepository.GetSkyCommOpsLevelsSelectList();
        }
    }
}
