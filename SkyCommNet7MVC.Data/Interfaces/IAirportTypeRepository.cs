using SkyCommNet7MVC.Domain.Models;
using System.Linq.Expressions;
using static SkyCommNet7MVC.Data.Interfaces.IRepository;

namespace SkyCommNet7MVC.Data.Interfaces
{
    public interface IAirportTypeRepository : IRepository<AirportType>
    {
        public IOrderedQueryable<AirportType> GetAllAirportTypes();
        public AirportType GetAirportTypeById(int id);
        public IQueryable<AirportType> GetAirportTypesWhere(Expression<Func<AirportType, bool>> filter);
        public IEnumerable<AirportType> GetAirportTypesSelectList();
    }
}
