using Microsoft.AspNetCore.Mvc.Rendering;
using SkyCommNet7MVC.Domain.Models;
using SkyCommNet7MVC.Presentation.ViewModels.UnitModels;
using SkyCommNet7MVC.Services.Interfaces;
using System.Linq.Expressions;

namespace SkyCommNet7MVC.Services.Services
{
    public class UnitModelsControllerService : IUnitModelsControllerService
    {
        private readonly IUnitModelService _unitModelService;
        private readonly IModelCategoryService _modelCategoryService;
        private readonly IModelFreqBandService _modelFreqBandService;
        private readonly IModelManufacturerService _modelManufacturerService;

        public UnitModelsControllerService
            (IUnitModelService unitModelService,
            IModelCategoryService modelCategoryService,
            IModelFreqBandService modelFreqBandService,
            IModelManufacturerService modelManufacturerService)
        {
            _unitModelService = unitModelService;
            _modelCategoryService = modelCategoryService;
            _modelFreqBandService = modelFreqBandService;
            _modelManufacturerService = modelManufacturerService;
        }

        public IOrderedQueryable<UnitModel> GetAllUnitModels()
        {
            return _unitModelService.GetAllUnitModels();
        }

        public UnitModel GetUnitModelById(int id)
        {
            return _unitModelService.GetUnitModelById(id);
        }

        public IQueryable<UnitModel> GetUnitModelsWhere(Expression<Func<UnitModel, bool>> filter)
        {
            return _unitModelService.GetUnitModelsWhere(filter);
        }

        public IEnumerable<ModelCategory> GetModelCategoriesSelectList()
        {
            return _modelCategoryService.GetModelCategoriesSelectList();
        }

        public IEnumerable<ModelFreqBand> GetModelFreqBandsSelectList()
        {
            return _modelFreqBandService.GetModelFreqBandsSelectList();
        }

        public IEnumerable<ModelManufacturer> GetModelManufacturersSelectList()
        {
            return _modelManufacturerService.GetModelManufacturersSelectList();
        }

        public async Task<UnitModelsFilterViewModel> BuildUnitModelsFilterViewModel
            (int? filterCategory, int? filterFreqBand, int? filterManufacturer, int? pageNumber, int? pageSize)
        {
            IQueryable<UnitModel> filteredUnitModels;

            string pageAction = "Filter";

            var modelCategoriesSelectList = GetModelCategoriesSelectList();
            var modelFreqBandsSelectList = GetModelFreqBandsSelectList();
            var modelManufacturersSelectList = GetModelManufacturersSelectList();

            if (filterCategory == null) { filterCategory = 0; }
            if (filterFreqBand == null) { filterFreqBand = 0; }
            if (filterManufacturer == null) { filterManufacturer = 0; }

            bool isFilterSet = false;
            bool isCategoryFiltered = false;
            bool isFreqBandFiltered = false;
            bool isManufacturerFiltered = false;
            bool hasRecords = false;

            var modelCategoriesSL = new SelectList
                (modelCategoriesSelectList, "ModelCategoryId", "ModelCategory1", filterCategory);
            var modelFreqBandsSL = new SelectList
                (modelFreqBandsSelectList, "ModelFreqBandId", "ModelFreqBand1", filterFreqBand);
            var modelManufacturersSL = new SelectList
                (modelManufacturersSelectList, "ModelManufacturerId", "ModelManufacturerName", filterManufacturer);

            Expression<Func<UnitModel, bool>>? compositeFilter = null;

            string pageTitle = "";

            if (filterCategory > 0)
            {
                isCategoryFiltered = true;
                isFilterSet = true;
            }

            if (filterFreqBand > 0)
            {
                isFreqBandFiltered = true;
                isFilterSet = true;
            }

            if (filterManufacturer > 0)
            {
                isManufacturerFiltered = true;
                isFilterSet = true;
            }

            if (isFilterSet)
            {
                compositeFilter = BuildFilterExpression
                    (isCategoryFiltered, filterCategory,
                    isFreqBandFiltered, filterFreqBand,
                    isManufacturerFiltered, filterManufacturer);
            }

            if (compositeFilter != null)
            {
                filteredUnitModels = GetUnitModelsWhere(compositeFilter);
                pageTitle = "Unit Models Filter - Filtered";
            }
            else
            {
                filteredUnitModels = GetAllUnitModels();
                pageTitle = "Unit Models Filter";
            }

            int recordCount = filteredUnitModels.Count();

            if (recordCount > 0) { hasRecords = true; }

            var pagedUnitModels = await PaginationService<UnitModel>.CreateAsync
                (filteredUnitModels, pageNumber ?? 1, pageSize ?? 12, pageAction);

            UnitModelsFilterViewModel vm = new UnitModelsFilterViewModel()
            {
                UnitModels = pagedUnitModels,
                RecordCount = recordCount,
                ModelCategoriesSelectList = modelCategoriesSL,
                ModelFreqBandsSelectList = modelFreqBandsSL,
                ModelManufacturersSelectList = modelManufacturersSL,
                PageNumber = pageNumber,
                PageSize = pageSize,
                PageAction = pageAction,
                PageTitle = pageTitle,
                FilterCategory = filterCategory,
                FilterFreqBand = filterFreqBand,
                FilterManufacturer = filterManufacturer,
                HasRecords = hasRecords
            };

            return vm;
        }

        public Expression<Func<UnitModel, bool>> BuildFilterExpression
            (bool isCategoryFiltered, int? filterCategory,
            bool isFreqBandFiltered, int? filterFreqBand,
            bool isManufacturerFiltered, int? filterManufacturer)
        {
            Expression<Func<UnitModel, bool>> filterExpression;

            switch ((isCategoryFiltered, isFreqBandFiltered, isManufacturerFiltered))
            {
                case (true, false, false):
                    filterExpression = u => u.ModelCategoryId == filterCategory;
                    return filterExpression;

                case (false, true, false):
                    filterExpression = u => u.ModelFreqBandId == filterFreqBand;
                    return filterExpression;

                case (false, false, true):
                    filterExpression = u => u.ModelManufacturerId == filterManufacturer;
                    return filterExpression;

                case (true, true, false):
                    filterExpression = u => u.ModelCategoryId == filterCategory 
                    && u.ModelFreqBandId == filterFreqBand;
                    return filterExpression;

                case (true, false, true):
                    filterExpression = u => u.ModelCategoryId == filterCategory 
                    && u.ModelManufacturerId == filterManufacturer;
                    return filterExpression;

                case (false, true, true):
                    filterExpression = u => u.ModelFreqBandId == filterFreqBand 
                    && u.ModelManufacturerId == filterManufacturer;
                    return filterExpression;

                case (true, true, true):
                    filterExpression = u => u.ModelCategoryId == filterCategory 
                    && u.ModelFreqBandId == filterFreqBand 
                    && u.ModelManufacturerId == filterManufacturer;
                    return filterExpression;

                default:
                    return null;
            }
        }

        public async Task<UnitModelDetailsViewModel> BuildUnitModelDetailsViewModel(int id, string returnUrl)
        {
            var selectedUnitModel = GetUnitModelById(id);

            if (selectedUnitModel == null)
            {
                return null;
            }

            var imagePath = @"/images/images-240px/unitmodels/" + selectedUnitModel.ModelImage;
            var logoPath = @"/images/logo/" + selectedUnitModel.ModelManufacturer.ModelManufacturerImage;

            UnitModelDetailsViewModel vm = new UnitModelDetailsViewModel()
            {
                SelectedUnitModel = selectedUnitModel,
                PageTitle = "Details",
                ImagePath = imagePath,
                LogoPath = logoPath,
                ReturnUrl = returnUrl,
            };

            await Task.Delay(1);
            return vm;
        }

        public bool UnitModelExists(int id)
        {
            return _unitModelService.UnitModelExists(id);
        }
    }
}
