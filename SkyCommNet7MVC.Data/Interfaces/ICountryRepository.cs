using SkyCommNet7MVC.Domain.Models;
using static SkyCommNet7MVC.Data.Interfaces.IRepository;

namespace SkyCommNet7MVC.Data.Interfaces
{
    public interface ICountryRepository : IRepository<Country>
    {
        public IOrderedQueryable<Country> GetAllCountries();
        public Country GetCountryById(int id);
        IEnumerable<Country> GetCountriesSelectList();
    }
}
