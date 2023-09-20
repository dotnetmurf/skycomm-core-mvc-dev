using Microsoft.AspNetCore.Mvc.Rendering;
using SkyCommNet7MVC.Domain.Models;
using SkyCommNet7MVC.Presentation.Interfaces;
using SkyCommNet7MVC.Presentation.ViewModels.Offices;
using SkyCommNet7MVC.Services.Interfaces;
using SkyCommNet7MVC.Services.Services;
using System.Linq.Expressions;

namespace SkyCommNet7MVC.Presentation.Services
{
    public class OfficesControllerService : IOfficesControllerService
    {
        private readonly ICountryService _countryService;
        private readonly IOfficeService _officeService;

        public OfficesControllerService
            (ICountryService countryService,
            IOfficeService officeService)
        {
            _countryService = countryService;
            _officeService = officeService;
        }

        public IOrderedQueryable<Office> GetAllOffices()
        {
            return _officeService.GetAllOffices();
        }

        public Office GetOfficeById(int id)
        {
            return _officeService.GetOfficeById(id);
        }

        public IQueryable<Office> GetOfficesWhere(Expression<Func<Office, bool>> filter)
        {
            return _officeService.GetOfficesWhere(filter);
        }

        public IEnumerable<Country> GetCountriesSelectList()
        {
            var countryList = from country in _countryService.GetAllCountries()
                              join office in _officeService.GetAllOffices()
                              on country.CountryId equals office.CountryId
                              select country;

            var countrySL = countryList.Distinct().OrderBy(country => country.CountryName).ToList();

            return countrySL;
        }

        public async Task<OfficesFilterViewModel> BuildOfficesFilterViewModel
            (int? filterCountry, int? pageNumber, int? pageSize)
        {
            IQueryable<Office> filteredOffices;

            string pageAction = "Filter";

            var countrySelectList = GetCountriesSelectList();

            if (filterCountry == null) { filterCountry = 0; }

            bool hasRecords = false;

            var countrySL = new SelectList
                (countrySelectList, "CountryId", "CountryName", filterCountry);

            string pageTitle = "";

            if (filterCountry != 0)
            {
                filteredOffices = GetOfficesWhere(o => o.CountryId == filterCountry);
                pageTitle = "Offices Filter - Filtered";
            }
            else
            {
                filteredOffices = GetAllOffices();
                pageTitle = "Offices Filter";
            }

            int recordCount = filteredOffices.Count();

            if (recordCount > 0) { hasRecords = true; }

            var pagedOffices = await PaginationService<Office>.CreateAsync
                (filteredOffices, pageNumber ?? 1, pageSize ?? 5, pageAction);

            OfficesFilterViewModel vm = new OfficesFilterViewModel()
            {
                Offices = pagedOffices,
                RecordCount = recordCount,
                CountriesSelectList = countrySL,
                PageNumber = pageNumber,
                PageSize = pageSize,
                PageAction = pageAction,
                PageTitle = pageTitle,
                FilterCountry = filterCountry,
                HasRecords = hasRecords
            };

            return vm;
        }

        public async Task<OfficeDetailsViewModel> BuildOfficeDetailsViewModel(int id, string returnUrl)
        {
            var selectedOffice = GetOfficeById(id);

            if (selectedOffice == null)
            {
                return null;
            }

            OfficeDetailsViewModel vm = new OfficeDetailsViewModel()
            {
                SelectedOffice = selectedOffice,
                PageTitle = "Details",
                ReturnUrl = returnUrl,
            };

            await Task.Delay(1);
            return vm;
        }

        public bool OfficeExists(int id)
        {
            return _officeService.OfficeExists(id);
        }
    }
}
