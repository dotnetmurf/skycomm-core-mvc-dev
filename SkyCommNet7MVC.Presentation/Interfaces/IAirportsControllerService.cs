using SkyCommNet7MVC.Domain.Models;
using SkyCommNet7MVC.Presentation.ViewModels.Airports;
using System.Linq.Expressions;

namespace SkyCommNet7MVC.Services.Interfaces
{
    public interface IAirportsControllerService
    {
        public IOrderedQueryable<Airport> GetAllAirports();
        public Airport GetAirportById(int id);
        public IQueryable<Airport> GetAirportsWhere(Expression<Func<Airport, bool>> filter);
        public IEnumerable<AirportType> GetAirportTypesSelectList();
        public IEnumerable<Country> GetCountriesSelectList();
        public IEnumerable<Region> GetRegionsSelectList();
        public IEnumerable<SkyCommOpsLevel> GetSkyCommOpsLevelsSelectList();
        public Expression<Func<Airport, bool>> BuildFilterExpression
            (bool isCountryFiltered, int? filterCountry,
            bool isAirportTypeFiltered, int? filterAirportType,
            bool isSkyCommFiltered, int? filterSkyComm);
        public Expression<Func<Airport, bool>> BuildSearchExpression
            (string searchString, string searchType, bool isAirportIatacodeSearch, bool isAirportNameSearch);
        public string BuildMapUri
            (string mapLatitude, string mapLongitude, string mapHSize, string mapVSize, string mapZoom, string mapType);
        public Task<AirportsFilterViewModel> BuildAirportsFilterViewModel
            (int? filterCountry, int? filterAirportType, int? filterSkyComm, int? pageNumber, int? pageSize);
        public Task<AirportsSearchViewModel> BuildAirportsSearchViewModel
            (string searchString, string searchName, string? searchType, int? pageNumber, int? pageSize);
        public Task<AirportDetailsViewModel> BuildAirportDetailsViewModel(int id, string returnUrl);
        public bool AirportExists(int id);
    }
}
