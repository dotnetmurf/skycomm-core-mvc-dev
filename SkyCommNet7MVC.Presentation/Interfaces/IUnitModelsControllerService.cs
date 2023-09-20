using SkyCommNet7MVC.Domain.Models;
using SkyCommNet7MVC.Presentation.ViewModels.UnitModels;
using System.Linq.Expressions;

namespace SkyCommNet7MVC.Services.Interfaces
{
    public interface IUnitModelsControllerService
    {
        public IOrderedQueryable<UnitModel> GetAllUnitModels();
        public UnitModel GetUnitModelById(int id);
        public IQueryable<UnitModel> GetUnitModelsWhere(Expression<Func<UnitModel, bool>> filter);
        public IEnumerable<ModelCategory> GetModelCategoriesSelectList();
        public IEnumerable<ModelFreqBand> GetModelFreqBandsSelectList();
        public IEnumerable<ModelManufacturer> GetModelManufacturersSelectList();
        public Expression<Func<UnitModel, bool>> BuildFilterExpression
            (bool isCategoryFiltered, int? filterCategory,
            bool isFreqBandFiltered, int? filterFreqBand,
            bool isManufacturerFiltered, int? filterManufacturer);
        public Task<UnitModelsFilterViewModel> BuildUnitModelsFilterViewModel
            (int? filterCategory, int? filterFreqBand, int? filterManufacturer, int? pageNumber, int? pageSize);
        public Task<UnitModelDetailsViewModel> BuildUnitModelDetailsViewModel(int id, string returnUrl);
        public bool UnitModelExists(int id);
    }
}
