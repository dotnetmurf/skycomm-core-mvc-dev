using Microsoft.AspNetCore.Mvc.Rendering;
using SkyCommNet7MVC.Domain.Models;
using SkyCommNet7MVC.Presentation.ViewModels.Airports;
using SkyCommNet7MVC.Services.Interfaces;
using SkyCommNet7MVC.Services.SelectLists;
using System.Linq.Expressions;
using System.Text;

namespace SkyCommNet7MVC.Services.Services
{
    public class AirportsControllerService : IAirportsControllerService
    {
        private readonly IAirportService _airportService;
        private readonly IAirportTypeService _airportTypeService;
        private readonly ICountryService _countryService;
        private readonly IRegionService _regionService;
        private readonly ISkyCommOpsLevelService _skyCommOpsLevelService;

        public AirportsControllerService
            (IAirportService airportService,
            IAirportTypeService airportTypeService,
            ICountryService countryService,
            IRegionService regionService,
            ISkyCommOpsLevelService skyCommOpsLevelService)
        {
            _airportService = airportService;
            _airportTypeService = airportTypeService;
            _countryService = countryService;
            _regionService = regionService;
            _skyCommOpsLevelService = skyCommOpsLevelService;
        }

        public IOrderedQueryable<Airport> GetAllAirports()
        {
            return _airportService.GetAllAirports();
        }

        public Airport GetAirportById(int id)
        {
            return _airportService.GetAirportById(id);
        }

        public IQueryable<Airport> GetAirportsWhere(Expression<Func<Airport, bool>> filter)
        {
            return _airportService.GetAirportsWhere(filter);
        }

        public IEnumerable<AirportType> GetAirportTypesSelectList()
        {
            return _airportTypeService.GetAirportTypesSelectList();
        }

        public IEnumerable<Country> GetCountriesSelectList()
        {
            return _countryService.GetCountriesSelectList();
        }

        public IEnumerable<Region> GetRegionsSelectList()
        {
            return _regionService.GetRegionsSelectList();
        }

        public IEnumerable<SkyCommOpsLevel> GetSkyCommOpsLevelsSelectList()
        {
            return _skyCommOpsLevelService.GetSkyCommOpsLevelsSelectList();
        }

        public async Task<AirportsFilterViewModel> BuildAirportsFilterViewModel
            (int? filterCountry, int? filterAirportType, int? filterSkyComm, int? pageNumber, int? pageSize)
        {
            IQueryable<Airport> filteredAirports;

            string pageAction = "Filter";

            var countrySelectList = GetCountriesSelectList();
            var airportTypeSelectList = GetAirportTypesSelectList();
            var skyCommSelectList = GetSkyCommOpsLevelsSelectList();

            if (filterCountry == null) { filterCountry = 0; }
            if (filterAirportType == null) { filterAirportType = 0; }
            if (filterSkyComm == null) { filterSkyComm = 0; }

            bool isFilterSet = false;
            bool isCountryFiltered = false;
            bool isAirportTypeFiltered = false;
            bool isSkyCommFiltered = false;
            bool hasRecords = false;

            var airportTypeSL = new SelectList
                (airportTypeSelectList, "AirportTypeId", "AirportType1", filterAirportType);
            var countrySL = new SelectList
                (countrySelectList, "CountryId", "CountryName", filterCountry);
            var skyCommSL = new SelectList
                (skyCommSelectList, "SkyCommOpsLevelId", "SkyCommOpsLevel1", filterSkyComm);

            Expression<Func<Airport, bool>>? compositeFilter = null;

            string pageTitle = "";

            if (filterCountry > 0)
            {
                isCountryFiltered = true;
                isFilterSet = true;
            }

            if (filterAirportType > 0)
            {
                isAirportTypeFiltered = true;
                isFilterSet = true;
            }

            if (filterSkyComm > 0)
            {
                isSkyCommFiltered = true;
                isFilterSet = true;
            }

            if (isFilterSet)
            {
                compositeFilter = BuildFilterExpression
                    (isCountryFiltered, filterCountry,
                    isAirportTypeFiltered, filterAirportType,
                    isSkyCommFiltered, filterSkyComm);
            }

            if (compositeFilter != null)
            {
                filteredAirports = GetAirportsWhere(compositeFilter);
                pageTitle = "Airports Filter - Filtered";
            }
            else
            {
                filteredAirports = GetAllAirports();
                pageTitle = "Airports Filter";
            }

            int recordCount = filteredAirports.Count();

            if (recordCount > 0) { hasRecords = true; }

            var pagedAirports = await PaginationService<Airport>.CreateAsync
                (filteredAirports, pageNumber ?? 1, pageSize ?? 5, pageAction);

            AirportsFilterViewModel vm = new AirportsFilterViewModel()
            {
                Airports = pagedAirports,
                RecordCount = recordCount,
                AirportTypesSelectList = airportTypeSL,
                CountriesSelectList = countrySL,
                SkyCommSelectList = skyCommSL,
                PageNumber = pageNumber,
                PageSize = pageSize,
                PageAction = pageAction,
                PageTitle = pageTitle,
                FilterCountry = filterCountry,
                FilterAirportType = filterAirportType,
                FilterSkyComm = filterSkyComm,
                HasRecords = hasRecords
            };

            return vm;
        }

        public Expression<Func<Airport, bool>> BuildFilterExpression
            (bool isCountryFiltered, int? filterCountry,
            bool isAirportTypeFiltered, int? filterAirportType,
            bool isSkyCommFiltered, int? filterSkyComm)
        {
            Expression<Func<Airport, bool>> filterExpression;

            switch ((isCountryFiltered, isAirportTypeFiltered, isSkyCommFiltered))
            {
                case (true, false, false):
                    filterExpression = a => a.Region.CountryId == filterCountry;
                    return filterExpression;

                case (false, true, false):
                    filterExpression = a => a.AirportTypeId == filterAirportType;
                    return filterExpression;

                case (false, false, true):
                    filterExpression = a => a.SkyCommOpsLevelId == filterSkyComm;
                    return filterExpression;

                case (true, true, false):
                    filterExpression = a => a.Region.CountryId == filterCountry 
                    && a.AirportTypeId == filterAirportType;
                    return filterExpression;

                case (true, false, true):
                    filterExpression = a => a.Region.CountryId == filterCountry 
                    && a.SkyCommOpsLevelId == filterSkyComm;
                    return filterExpression;

                case (false, true, true):
                    filterExpression = a => a.AirportTypeId == filterAirportType 
                    && a.SkyCommOpsLevelId == filterSkyComm;
                    return filterExpression;

                case (true, true, true):
                    filterExpression = a => a.Region.CountryId == filterCountry 
                    && a.AirportTypeId == filterAirportType 
                    && a.SkyCommOpsLevelId == filterSkyComm;
                    return filterExpression;

                default:
                    return null;
            }
        }

        public async Task<AirportsSearchViewModel> BuildAirportsSearchViewModel
            (string searchString, string searchName, string? searchType, int? pageNumber, int? pageSize)
        {
            IQueryable<Airport> searchedAirports;

            string pageAction = "Search";

            string pageTitle = "";

            bool hasRecords = false;

            bool isSearchSet = false;
            bool isAirportNameSearch = false;
            bool isAirportIatacodeSearch = false;

            var searchTypeSelectList = SearchTypeSelectList.GetSearchTypeSelectList();
            var searchAirportFieldsSelectList = SearchAirportFieldsSelectList.GetSearchAirportFieldsSelectList();

            if (searchType == null) { searchType = "Contains"; }

            var searchTypeSL = new SelectList
                (searchTypeSelectList, "Id", "TypeName", searchType);
            var searchAirportFieldsSL = new SelectList
                (searchAirportFieldsSelectList, "Id", "AirportFieldName", searchName);

            Expression<Func<Airport, bool>>? compositeFilter = null;

            if (!String.IsNullOrEmpty(searchString))
            {
                isSearchSet = true;
            }

            if (searchName == "Code")
            {
                isAirportIatacodeSearch = true;
            }

            if (searchName == "Name")
            {
                isAirportNameSearch = true;
            }

            if (isSearchSet)
            {
                compositeFilter = BuildSearchExpression
                    (searchString, searchType, isAirportIatacodeSearch, isAirportNameSearch);
            }

            if (compositeFilter != null)
            {
                searchedAirports = GetAirportsWhere(compositeFilter);
                pageTitle = "Airports Search - Searched";
            }
            else
            {
                searchedAirports = GetAllAirports();
                pageTitle = "Airports Search";
            }

            var pagedAirports = await PaginationService<Airport>.CreateAsync
                (searchedAirports, pageNumber ?? 1, pageSize ?? 5, pageAction);

            int recordCount = searchedAirports.Count();

            if (recordCount > 0) { hasRecords = true; }

            AirportsSearchViewModel vm = new AirportsSearchViewModel()
            {
                SearchString = searchString,
                SearchName = searchName,
                SearchType = searchType,
                SearchTypeSelectList = searchTypeSL,
                SearchAirportFieldsSelectList = searchAirportFieldsSL,
                PageNumber = pageNumber,
                PageSize = pageSize,
                PageAction = pageAction,
                PageTitle = pageTitle,
                HasRecords = hasRecords,
                Airports = pagedAirports,
                RecordCount = recordCount,
            };

            return vm;
        }

        public Expression<Func<Airport, bool>> BuildSearchExpression
            (string searchString, string searchType, bool isAirportIatacodeSearch, bool isAirportNameSearch)
        {
            Expression<Func<Airport, bool>> searchExpression;

            switch (searchType)
            {
                case ("Contains"):
                    switch ((isAirportIatacodeSearch, isAirportNameSearch))
                    {
                        case (true, false):
                            searchExpression = a => a.AirportIatacode.Contains(searchString);
                            return searchExpression;
                        case (false, true):
                            searchExpression = a => a.AirportName.Contains(searchString);
                            return searchExpression;
                        case (false, false):
                            searchExpression = a => a.AirportIatacode.Contains(searchString)
                               || a.AirportName.Contains(searchString);
                            return searchExpression;
                        default:
                            return null;
                    }
                case ("StartsWith"):
                    switch ((isAirportIatacodeSearch, isAirportNameSearch))
                    {
                        case (true, false):
                            searchExpression = a => a.AirportIatacode.StartsWith(searchString);
                            return searchExpression;
                        case (false, true):
                            searchExpression = a => a.AirportName.StartsWith(searchString);
                            return searchExpression;
                        case (false, false):
                            searchExpression = a => a.AirportIatacode.StartsWith(searchString)
                               || a.AirportName.StartsWith(searchString);
                            return searchExpression;
                        default:
                            return null;
                    }
                case ("EndsWith"):
                    switch ((isAirportIatacodeSearch, isAirportNameSearch))
                    {
                        case (true, false):
                            searchExpression = a => a.AirportIatacode.EndsWith(searchString);
                            return searchExpression;
                        case (false, true):
                            searchExpression = a => a.AirportName.EndsWith(searchString);
                            return searchExpression;
                        case (false, false):
                            searchExpression = a => a.AirportIatacode.EndsWith(searchString)
                               || a.AirportName.EndsWith(searchString);
                            return searchExpression;
                        default:
                            return null;
                    }
                case ("Exact"):
                    switch ((isAirportIatacodeSearch, isAirportNameSearch))
                    {
                        case (true, false):
                            searchExpression = a => a.AirportIatacode.Equals(searchString);
                            return searchExpression;
                        case (false, true):
                            searchExpression = a => a.AirportName.Equals(searchString);
                            return searchExpression;
                        case (false, false):
                            searchExpression = a => a.AirportIatacode.Equals(searchString)
                               || a.AirportName.Equals(searchString);
                            return searchExpression;
                        default:
                            return null;
                    }
                default:
                    return null;
            }
        }

        public async Task<AirportDetailsViewModel> BuildAirportDetailsViewModel(int id, string returnUrl)
        {
            var selectedAirport = GetAirportById(id);

            if (selectedAirport == null)
            {
                return null;
            }

            string mapLatitude = selectedAirport.AirportLatitudeDegrees.ToString();
            string mapLongitude = selectedAirport.AirportLongitudeDegrees.ToString();
            string satZoom = "13";
            string satType = "sat";
            string mapZoom = "11";
            string mapType = "map";
            string mapHSize = "500";
            string mapVSize = "500";

            string satUri = BuildMapUri
                (mapLatitude, mapLongitude, mapHSize, mapVSize, satZoom, satType);

            string mapUri = BuildMapUri
                (mapLatitude, mapLongitude, mapHSize, mapVSize, mapZoom, mapType);

            AirportDetailsViewModel vm = new AirportDetailsViewModel()
            {
                SelectedAirport = selectedAirport,
                PageTitle = "Details",
                SatUri = satUri,
                MapUri = mapUri,
                ReturnUrl = returnUrl,
            };

            await Task.Delay(1);
            return vm;
        }

        public string BuildMapUri
            (string mapLatitude, string mapLongitude, string mapHSize, string mapVSize, string mapZoom, string mapType)
        {
            StringBuilder mapUriBuilder = new StringBuilder("https://");
            mapUriBuilder.Append("www.mapquestapi.com/staticmap/v5/map?");
            mapUriBuilder.Append("key=FnEceCKCZhq1u4OZnAQIWUQzAvAdLEny");
            mapUriBuilder.Append("&center=");
            mapUriBuilder.Append(mapLatitude);
            mapUriBuilder.Append(",");
            mapUriBuilder.Append(mapLongitude);
            mapUriBuilder.Append("&size=");
            mapUriBuilder.Append(mapHSize);
            mapUriBuilder.Append(",");
            mapUriBuilder.Append(mapVSize);
            mapUriBuilder.Append("&zoom=");
            mapUriBuilder.Append(mapZoom);
            mapUriBuilder.Append("&type=");
            mapUriBuilder.Append(mapType);

            return mapUriBuilder.ToString();
        }

        public bool AirportExists(int id)
        {
            return _airportService.AirportExists(id);
        }
    }
}
