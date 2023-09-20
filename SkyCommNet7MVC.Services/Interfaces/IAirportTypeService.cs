using SkyCommNet7MVC.Domain.Models;
using System.Linq.Expressions;

namespace SkyCommNet7MVC.Services.Interfaces
{
    public interface IAirportTypeService
    {
        public IOrderedQueryable<AirportType> GetAllAirportTypes();
        public AirportType GetAirportTypeById(int id);
        public IQueryable<AirportType> GetAirportTypesWhere(Expression<Func<AirportType, bool>> filter);
        public IEnumerable<AirportType> GetAirportTypesSelectList();
    }
}
