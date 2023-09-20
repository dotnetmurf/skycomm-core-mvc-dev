using SkyCommNet7MVC.Data.Interfaces;
using SkyCommNet7MVC.Domain.Models;
using SkyCommNet7MVC.Services.Interfaces;
using System.Linq.Expressions;

namespace SkyCommNet7MVC.Services.Services
{
    public class AirportService : IAirportService
    {
        private readonly IAirportRepository _airportRepository;

        public AirportService(IAirportRepository airportRepository)
        {
            _airportRepository = airportRepository;
        }

        public IOrderedQueryable<Airport> GetAllAirports()
        {
            return _airportRepository.GetAllAirports();
        }

        public Airport GetAirportById(int id)
        {
            return _airportRepository.GetAirportById(id);
        }

        public IQueryable<Airport> GetAirportsWhere(Expression<Func<Airport, bool>> filter)
        {
            return _airportRepository.GetAirportsWhere(filter);
        }

        public bool AirportExists(int id)
        {
            return _airportRepository.AirportExists(id);
        }
    }
}
