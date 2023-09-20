using Microsoft.AspNetCore.Mvc.Rendering;
using SkyCommNet7MVC.Domain.Models;
using SkyCommNet7MVC.Presentation.ViewModels.Units;
using SkyCommNet7MVC.Services.Interfaces;
using SkyCommNet7MVC.Services.SelectLists;
using System.Linq.Expressions;

namespace SkyCommNet7MVC.Services.Services
{
    public class UnitsControllerService : IUnitsControllerService
    {
        private static int currentAirport { get; set; }

        private readonly IUnitService _unitService;
        private readonly IAirportService _airportService;
        private readonly IUnitModelService _unitModelService;

        public UnitsControllerService
            (IUnitService unitService,
            IAirportService airportService,
            IUnitModelService unitModelService)
        {
            _unitService = unitService;
            _airportService = airportService;
            _unitModelService = unitModelService;
        }

        public IOrderedQueryable<Unit> GetAllUnits()
        {
            return _unitService.GetAllUnits();
        }

        public Unit GetUnitById(int id)
        {
            return _unitService.GetUnitById(id);
        }

        public IQueryable<Unit> GetUnitsWhere(Expression<Func<Unit, bool>> filter)
        {
            return _unitService.GetUnitsWhere(filter);
        }

        public IEnumerable<Airport> GetAirportsSelectList()
        {
            var airportList = from airport in _airportService.GetAllAirports()
                              join unit in GetAllUnits()
                              on airport.AirportId equals unit.AirportId
                              select airport;

            var airportSL = airportList.Distinct().OrderBy(airport => airport.AirportName).ToList();

            return airportSL;
        }

        public IEnumerable<UnitModel> GetUnitModelsSelectList(int filterAirport)
        {
            if (filterAirport > 0)
            {
                var unitModelList = from unitModel in _unitModelService.GetAllUnitModels()
                                    join unit in GetAllUnits()
                                    on unitModel.UnitModelId equals unit.UnitModelId
                                    join airport in _airportService.GetAllAirports()
                                    on unit.AirportId equals filterAirport
                                    select unitModel;

                var unitModelSL = unitModelList.Distinct().OrderBy(unitModel => unitModel.ModelName).ToList().AsEnumerable();

                return unitModelSL;
            }
            else
            {
                return _unitModelService.GetUnitModelsSelectList();
            }
        }

        public async Task<UnitsFilterViewModel> BuildUnitsFilterViewModel
            (int? filterAirport, int? filterUnitModel, int? pageNumber, int? pageSize)
        {
            IQueryable<Unit> filteredUnits;

            string pageAction = "Filter";

            if (filterAirport == null) { filterAirport = 0; }
            if ((filterUnitModel == null) || (currentAirport != filterAirport)) { filterUnitModel = 0; }

            currentAirport = (int)filterAirport;

            var airportsSelectList = GetAirportsSelectList();
            var unitModelsSelectList = GetUnitModelsSelectList((int)filterAirport);

            bool isFilterSet = false;
            bool isAirportFiltered = false;
            bool isUnitModelFiltered = false;
            bool hasRecords = false;

            var airportsSL = new SelectList
                (airportsSelectList, "AirportId", "AirportName", filterAirport);
            var unitModelsSL = new SelectList
                (unitModelsSelectList, "UnitModelId", "ModelName", filterUnitModel);

            Expression<Func<Unit, bool>>? compositeFilter = null;

            string pageTitle = "";

            if (filterAirport > 0)
            {
                isAirportFiltered = true;
                isFilterSet = true;
            }

            if (filterUnitModel > 0)
            {
                isUnitModelFiltered = true;
                isFilterSet = true;
            }

            if (isFilterSet)
            {
                compositeFilter = BuildFilterExpression
                    (isAirportFiltered, filterAirport,
                    isUnitModelFiltered, filterUnitModel);
            }

            if (compositeFilter != null)
            {
                filteredUnits = GetUnitsWhere(compositeFilter);
                pageTitle = "Units Filter - Filtered";
            }
            else
            {
                filteredUnits = GetAllUnits();
                pageTitle = "Units Filter";
            }

            int recordCount = filteredUnits.Count();

            if (recordCount > 0) { hasRecords = true; }

            var pagedUnits = await PaginationService<Unit>.CreateAsync
                (filteredUnits, pageNumber ?? 1, pageSize ?? 12, pageAction);

            UnitsFilterViewModel vm = new UnitsFilterViewModel()
            {
                Units = pagedUnits,
                RecordCount = recordCount,
                AirportsSelectList = airportsSL,
                UnitModelsSelectList = unitModelsSL,
                PageNumber = pageNumber,
                PageSize = pageSize,
                PageAction = pageAction,
                PageTitle = pageTitle,
                FilterAirport = filterAirport,
                FilterUnitModel = filterUnitModel,
                HasRecords = hasRecords
            };

            return vm;
        }

        public Expression<Func<Unit, bool>> BuildFilterExpression
            (bool isAirportFiltered, int? filterAirport,
            bool isUnitModelFiltered, int? filterUnitModel)
        {
            Expression<Func<Unit, bool>> filterExpression;

            switch ((isAirportFiltered, isUnitModelFiltered))
            {
                case (true, false):
                    filterExpression = u => u.AirportId == filterAirport;
                    return filterExpression;

                case (false, true):
                    filterExpression = u => u.UnitModelId == filterUnitModel;
                    return filterExpression;

                case (true, true):
                    filterExpression = u => u.AirportId == filterAirport 
                    && u.UnitModelId == filterUnitModel;
                    return filterExpression;

                default:
                    return null;
            }
        }

        public async Task<UnitsSearchViewModel> BuildUnitsSearchViewModel
            (string searchString, string searchNbr, string? searchType, int? pageNumber, int? pageSize)
        {
            IQueryable<Unit> searchedUnits;

            string pageAction = "Search";

            string pageTitle = "";

            bool hasRecords = false;

            bool isSearchSet = false;
            bool isSCNbrSearch = false;
            bool isSerNbrSearch = false;

            var searchTypeSelectList = SearchTypeSelectList.GetSearchTypeSelectList();
            var searchUnitFieldsSelectList = SearchUnitFieldsSelectList.GetSearchUnitFieldsSelectList();

            if (searchType == null) { searchType = "Contains"; }

            var searchTypeSL = new SelectList
                (searchTypeSelectList, "Id", "TypeName", searchType);
            var searchUnitFieldsSL = new SelectList
                (searchUnitFieldsSelectList, "Id", "UnitFieldName", searchNbr);

            Expression<Func<Unit, bool>>? compositeFilter = null;

            if (!String.IsNullOrEmpty(searchString))
            {
                isSearchSet = true;
            }

            if (searchNbr == "SCNbr")
            {
                isSCNbrSearch = true;
            }

            if (searchNbr == "SerNbr")
            {
                isSerNbrSearch = true;
            }

            if (isSearchSet)
            {
                compositeFilter = BuildSearchExpression
                    (searchString, searchType, isSCNbrSearch, isSerNbrSearch);
            }

            if (compositeFilter != null)
            {
                searchedUnits = GetUnitsWhere(compositeFilter);
                pageTitle = "Units Search - Searched";
            }
            else
            {
                searchedUnits = GetAllUnits();
                pageTitle = "Units Search";
            }

            var pagedUnits = await PaginationService<Unit>.CreateAsync
                (searchedUnits, pageNumber ?? 1, pageSize ?? 5, pageAction);

            int recordCount = searchedUnits.Count();

            if (recordCount > 0) { hasRecords = true; }

            UnitsSearchViewModel vm = new UnitsSearchViewModel()
            {
                SearchString = searchString,
                SearchNbr = searchNbr,
                SearchType = searchType,
                SearchTypeSelectList = searchTypeSL,
                SearchUnitFieldsSelectList = searchUnitFieldsSL,
                PageNumber = pageNumber,
                PageSize = pageSize,
                PageAction = pageAction,
                PageTitle = pageTitle,
                HasRecords = hasRecords,
                Units = pagedUnits,
                RecordCount = recordCount,
            };

            return vm;
        }

        public Expression<Func<Unit, bool>> BuildSearchExpression
            (string searchString, string searchType, bool isSCNbrSearch, bool isSerNbrSearch)
        {
            Expression<Func<Unit, bool>> searchExpression;

            switch (searchType)
            {
                case ("Contains"):
                    switch ((isSCNbrSearch, isSerNbrSearch))
                    {
                        case (true, false):
                            searchExpression = u => u.UnitScnbr.Contains(searchString);
                            return searchExpression;
                        case (false, true):
                            searchExpression = u => u.UnitSerNbr.Contains(searchString);
                            return searchExpression;
                        case (false, false):
                            searchExpression = u => u.UnitScnbr.Contains(searchString)
                               || u.UnitSerNbr.Contains(searchString);
                            return searchExpression;
                        default:
                            return null;
                    }
                case ("StartsWith"):
                    switch ((isSCNbrSearch, isSerNbrSearch))
                    {
                        case (true, false):
                            searchExpression = u => u.UnitScnbr.StartsWith(searchString);
                            return searchExpression;
                        case (false, true):
                            searchExpression = u => u.UnitSerNbr.StartsWith(searchString);
                            return searchExpression;
                        case (false, false):
                            searchExpression = u => u.UnitScnbr.StartsWith(searchString)
                               || u.UnitSerNbr.StartsWith(searchString);
                            return searchExpression;
                        default:
                            return null;
                    }
                case ("EndsWith"):
                    switch ((isSCNbrSearch, isSerNbrSearch))
                    {
                        case (true, false):
                            searchExpression = u => u.UnitScnbr.EndsWith(searchString);
                            return searchExpression;
                        case (false, true):
                            searchExpression = u => u.UnitSerNbr.EndsWith(searchString);
                            return searchExpression;
                        case (false, false):
                            searchExpression = u => u.UnitScnbr.EndsWith(searchString)
                               || u.UnitSerNbr.EndsWith(searchString);
                            return searchExpression;
                        default:
                            return null;
                    }
                case ("Exact"):
                    switch ((isSCNbrSearch, isSerNbrSearch))
                    {
                        case (true, false):
                            searchExpression = u => u.UnitScnbr.Equals(searchString);
                            return searchExpression;
                        case (false, true):
                            searchExpression = u => u.UnitSerNbr.Equals(searchString);
                            return searchExpression;
                        case (false, false):
                            searchExpression = u => u.UnitScnbr.Equals(searchString)
                               || u.UnitSerNbr.Equals(searchString);
                            return searchExpression;
                        default:
                            return null;
                    }
                default:
                    return null;
            }
        }

        public async Task<UnitDetailsViewModel> BuildUnitDetailsViewModel(int id, string returnUrl)
        {
            var selectedUnit = GetUnitById(id);

            if (selectedUnit == null)
            {
                return null;
            }

            var imagePath = @"/images/images-240px/unitmodels/" + selectedUnit.UnitModel.ModelImage;

            UnitDetailsViewModel vm = new UnitDetailsViewModel()
            {
                SelectedUnit = selectedUnit,
                PageTitle = "Details",
                ImagePath = imagePath,
                ReturnUrl = returnUrl,
            };

            await Task.Delay(1);
            return vm;
        }

        public bool UnitExists(int id)
        {
            return _unitService.UnitExists(id);
        }
    }
}
