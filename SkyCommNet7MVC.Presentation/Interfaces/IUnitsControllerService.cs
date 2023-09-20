using SkyCommNet7MVC.Domain.Models;
using SkyCommNet7MVC.Presentation.ViewModels.Units;
using System.Linq.Expressions;

namespace SkyCommNet7MVC.Services.Interfaces
{
    public interface IUnitsControllerService
    {
        public IOrderedQueryable<Unit> GetAllUnits();
        public Unit GetUnitById(int id);
        public IQueryable<Unit> GetUnitsWhere(Expression<Func<Unit, bool>> filter);
        public IEnumerable<Airport> GetAirportsSelectList();
        public IEnumerable<UnitModel> GetUnitModelsSelectList(int filterAirport);
        public Expression<Func<Unit, bool>> BuildFilterExpression
            (bool isAirportFiltered, int? filterAirport,
            bool isUnitModelFiltered, int? filterUnitModel);
        public Task<UnitsFilterViewModel> BuildUnitsFilterViewModel
            (int? filterAirport, int? filterUnitModel, int? pageNumber, int? pageSize);
        public Task<UnitsSearchViewModel> BuildUnitsSearchViewModel
            (string searchString, string searchNbr, string? searchType, int? pageNumber, int? pageSize);
        public Expression<Func<Unit, bool>> BuildSearchExpression
            (string searchString, string searchType, bool isAirportIatacodeSearch, bool isAirportNameSearch);
        public Task<UnitDetailsViewModel> BuildUnitDetailsViewModel(int id, string returnUrl);
        public bool UnitExists(int id);
    }
}
