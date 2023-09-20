using SkyCommNet7MVC.Domain.Models;

namespace SkyCommNet7MVC.Services.Interfaces
{
    public interface IContinentService
    {
        public IOrderedQueryable<Continent> GetAllContinents();
        public Continent GetContinentById(int id);
    }
}
