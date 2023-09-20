using SkyCommNet7MVC.Domain.Models;

namespace SkyCommNet7MVC.Services.Interfaces
{
    public interface ICountryService
    {
        public IOrderedQueryable<Country> GetAllCountries();
        public Country GetCountryById(int id);
        IEnumerable<Country> GetCountriesSelectList();
    }
}
