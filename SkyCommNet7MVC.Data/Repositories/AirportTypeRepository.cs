using SkyCommCoreMVC.Data.Repositories;
using SkyCommNet7MVC.Data.Interfaces;
using SkyCommNet7MVC.Domain.Models;
using System.Linq.Expressions;

namespace SkyCommNet7MVC.Data.Repositories
{
    public class AirportTypeRepository : Repository<AirportType>, IAirportTypeRepository
    {
        private readonly SkyCommDbContext _context;

        public AirportTypeRepository(SkyCommDbContext context) : base(context)
        {
            _context = context;
        }

        public IOrderedQueryable<AirportType> GetAllAirportTypes()
        {
            var allAirportsTypes =
                from airportType in GetAll()
                orderby airportType.AirportType1
                select airportType;

            return allAirportsTypes;
        }

        public AirportType GetAirportTypeById(int id)
        {
            return GetById(id);
        }

        public IQueryable<AirportType> GetAirportTypesWhere(Expression<Func<AirportType, bool>> filter)
        {
            var filteredAirportTypes =
                from airportType in Where(filter)
                orderby airportType.AirportType1
                select airportType;

            return filteredAirportTypes;
        }

        public IEnumerable<AirportType> GetAirportTypesSelectList()
        {
            return GetAllAirportTypes().AsEnumerable();
        }
    }
}
