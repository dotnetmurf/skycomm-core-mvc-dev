using Microsoft.EntityFrameworkCore;
using SkyCommCoreMVC.Data.Repositories;
using SkyCommNet7MVC.Data.Interfaces;
using SkyCommNet7MVC.Domain.Models;
using System.Linq.Expressions;

namespace SkyCommNet7MVC.Data.Repositories
{
    public class AirportRepository : Repository<Airport>, IAirportRepository
    {
        private readonly SkyCommDbContext _context;

        public AirportRepository(SkyCommDbContext context) : base(context)
        {
            _context = context;
        }

        public IOrderedQueryable<Airport> GetAllAirports()
        {
            var allAirports =
                from airport in GetAll().AsQueryable().
                    Include(a => a.AirportType).
                    Include(a => a.Region).
                    Include(a => a.Region.Country).
                    Include(a => a.SkyCommOpsLevel)
                orderby airport.AirportIatacode
                select airport;

            return allAirports;
        }

        public Airport GetAirportById(int id)
        {
            return GetAllAirports().FirstOrDefault(a => a.AirportId == id);
        }

        public IQueryable<Airport> GetAirportsWhere(Expression<Func<Airport, bool>> filter)
        {
            var airportsWhere =
                from airport in Where(filter).
                    Include(a => a.AirportType).
                    Include(a => a.Region).
                    Include(a => a.Region.Country).
                    Include(a => a.SkyCommOpsLevel)
                orderby airport.AirportIatacode
                select airport;

            return airportsWhere;
        }

        public IEnumerable<Airport> GetAirportsSelectList()
        {
            var airportsSL =
                from airport in GetAll()
                orderby airport.AirportIatacode
                select airport;

            return airportsSL.AsEnumerable();
        }

        public bool AirportExists(int id)
        {
            return (_context.Airports?.Any(a => a.AirportId == id)).GetValueOrDefault();
        }
    }
}
