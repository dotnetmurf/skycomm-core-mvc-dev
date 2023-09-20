using SkyCommNet7MVC.Data.Interfaces;
using SkyCommNet7MVC.Domain.Models;
using SkyCommNet7MVC.Services.Interfaces;
using System.Linq.Expressions;

namespace SkyCommNet7MVC.Services.Services
{
    public class AirportTypeService : IAirportTypeService
    {
        private readonly IAirportTypeRepository _airportTypeRepository;

        public AirportTypeService(IAirportTypeRepository airportTypeRepository)
        {
            _airportTypeRepository = airportTypeRepository;
        }

        public IOrderedQueryable<AirportType> GetAllAirportTypes()
        {
            return _airportTypeRepository.GetAllAirportTypes();
        }

        public AirportType GetAirportTypeById(int id)
        {
            return _airportTypeRepository.GetAirportTypeById(id);
        }

        public IQueryable<AirportType> GetAirportTypesWhere(Expression<Func<AirportType, bool>> filter)
        {
            return _airportTypeRepository.GetAirportTypesWhere(filter);
        }

        public IEnumerable<AirportType> GetAirportTypesSelectList()
        {
            return _airportTypeRepository.GetAirportTypesSelectList();
        }
    }
}
