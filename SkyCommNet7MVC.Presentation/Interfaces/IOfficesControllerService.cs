using SkyCommNet7MVC.Domain.Models;
using SkyCommNet7MVC.Presentation.ViewModels.Offices;
using System.Linq.Expressions;

namespace SkyCommNet7MVC.Presentation.Interfaces
{
    public interface IOfficesControllerService
    {
        public IOrderedQueryable<Office> GetAllOffices();
        public Office GetOfficeById(int id);
        public IQueryable<Office> GetOfficesWhere(Expression<Func<Office, bool>> filter);
        public IEnumerable<Country> GetCountriesSelectList();
        public Task<OfficesFilterViewModel> BuildOfficesFilterViewModel
            (int? filterCountry, int? pageNumber, int? pageSize);
        public Task<OfficeDetailsViewModel> BuildOfficeDetailsViewModel(int id, string returnUrl);
        public bool OfficeExists(int id);
    }
}
