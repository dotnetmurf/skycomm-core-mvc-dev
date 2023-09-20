using SkyCommNet7MVC.Data.Interfaces;
using SkyCommNet7MVC.Domain.Models;
using SkyCommNet7MVC.Services.Interfaces;

namespace SkyCommNet7MVC.Services.Services
{
    public class ContinentService : IContinentService
    {
        private readonly IContinentRepository _continentRepository;

        public ContinentService(IContinentRepository continentRepository)
        {
            _continentRepository = continentRepository;
        }

        public IOrderedQueryable<Continent> GetAllContinents()
        {
            return _continentRepository.GetAllContinents();
        }

        public Continent GetContinentById(int id)
        {
            return _continentRepository.GetContinentById(id);
        }
    }
}
