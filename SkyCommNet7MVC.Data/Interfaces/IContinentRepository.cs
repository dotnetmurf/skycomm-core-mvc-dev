using SkyCommNet7MVC.Domain.Models;
using static SkyCommNet7MVC.Data.Interfaces.IRepository;

namespace SkyCommNet7MVC.Data.Interfaces
{
    public interface IContinentRepository : IRepository<Continent>
    {
        public IOrderedQueryable<Continent> GetAllContinents();
        public Continent GetContinentById(int id);
    }
}
