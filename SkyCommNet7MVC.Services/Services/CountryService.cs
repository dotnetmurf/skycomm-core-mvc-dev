using SkyCommNet7MVC.Data.Interfaces;
using SkyCommNet7MVC.Domain.Models;
using SkyCommNet7MVC.Services.Interfaces;

namespace SkyCommNet7MVC.Services.Services
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;

        public CountryService(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public IOrderedQueryable<Country> GetAllCountries()
        {
            return _countryRepository.GetAllCountries();
        }

        public Country GetCountryById(int id)
        {
            return _countryRepository.GetCountryById(id);
        }

        public IEnumerable<Country> GetCountriesSelectList()
        {
            return _countryRepository.GetCountriesSelectList();
        }
    }
}
