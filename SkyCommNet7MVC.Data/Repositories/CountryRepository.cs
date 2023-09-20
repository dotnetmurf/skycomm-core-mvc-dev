using Microsoft.EntityFrameworkCore;
using SkyCommCoreMVC.Data.Repositories;
using SkyCommNet7MVC.Data.Interfaces;
using SkyCommNet7MVC.Domain.Models;

namespace SkyCommNet7MVC.Data.Repositories
{
    public class CountryRepository : Repository<Country>, ICountryRepository
    {
        private readonly SkyCommDbContext _context;

        public CountryRepository(SkyCommDbContext context) : base(context)
        {
            _context = context;
        }

        public IOrderedQueryable<Country> GetAllCountries()
        {
            var allCountries =
                from country in GetAll().AsQueryable().
                    Include(c => c.Continent)
                orderby country.CountryName
                select country;

            return allCountries;
        }

        public Country GetCountryById(int id)
        {
            return GetAllCountries().FirstOrDefault(c => c.CountryId == id);
        }

        public IEnumerable<Country> GetCountriesSelectList()
        {
            var countrySelectList =
                from country in GetAll()
                orderby country.CountryName
                select country;

            return countrySelectList;
        }
    }
}
