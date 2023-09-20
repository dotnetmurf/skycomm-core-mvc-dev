using SkyCommNet7MVC.Domain.Models;
using System.Linq.Expressions;

namespace SkyCommNet7MVC.Services.Interfaces
{
    public interface IAirportService
    {
        public IOrderedQueryable<Airport> GetAllAirports();
        public Airport GetAirportById(int id);
        public IQueryable<Airport> GetAirportsWhere(Expression<Func<Airport, bool>> filter);
        public bool AirportExists(int id);
    }
}
