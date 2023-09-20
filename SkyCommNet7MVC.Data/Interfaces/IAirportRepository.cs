using SkyCommNet7MVC.Domain.Models;
using System.Linq.Expressions;
using static SkyCommNet7MVC.Data.Interfaces.IRepository;

namespace SkyCommNet7MVC.Data.Interfaces
{
    public interface IAirportRepository : IRepository<Airport>
    {
        public IOrderedQueryable<Airport> GetAllAirports();
        public Airport GetAirportById(int id);
        public IQueryable<Airport> GetAirportsWhere(Expression<Func<Airport, bool>> filter);
        public IEnumerable<Airport> GetAirportsSelectList();
        public bool AirportExists(int id);
    }
}
