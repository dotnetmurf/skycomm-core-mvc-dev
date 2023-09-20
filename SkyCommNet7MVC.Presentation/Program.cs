using SkyCommCoreMVC.Data.Repositories;
using SkyCommNet7MVC.Data.Interfaces;
using SkyCommNet7MVC.Data.Repositories;
using SkyCommNet7MVC.Presentation.Interfaces;
using SkyCommNet7MVC.Presentation.Services;
using SkyCommNet7MVC.Services.Interfaces;
using SkyCommNet7MVC.Services.Services;
using static SkyCommNet7MVC.Data.Interfaces.IRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<SkyCommDbContext>();

builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddTransient<IAirportRepository, AirportRepository>();
builder.Services.AddTransient<IAirportTypeRepository, AirportTypeRepository>();
builder.Services.AddTransient<IContinentRepository, ContinentRepository>();
builder.Services.AddTransient<ICountryRepository, CountryRepository>();
builder.Services.AddTransient<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddTransient<IJobTitleRepository, JobTitleRepository>();
builder.Services.AddTransient<IModelCategoryRepository, ModelCategoryRepository>();
builder.Services.AddTransient<IModelFreqBandRepository, ModelFreqBandRepository>();
builder.Services.AddTransient<IModelManufacturerRepository, ModelManufacturerRepository>();
builder.Services.AddTransient<IModelModTypeRepository, ModelModTypeRepository>();
builder.Services.AddTransient<IModelTypeRepository, ModelTypeRepository>();
builder.Services.AddTransient<IOfficeRepository, OfficeRepository>();
builder.Services.AddTransient<IRegionRepository, RegionRepository>();
builder.Services.AddTransient<ISkyCommOpsLevelRepository, SkyCommOpsLevelRepository>();
builder.Services.AddTransient<IUnitModelRepository, UnitModelRepository>();
builder.Services.AddTransient<IUnitRepository, UnitRepository>();

builder.Services.AddTransient<IAirportService, AirportService>();
builder.Services.AddTransient<IAirportTypeService, AirportTypeService>();
builder.Services.AddTransient<IContinentService, ContinentService>();
builder.Services.AddTransient<ICountryService, CountryService>();
builder.Services.AddTransient<IDepartmentService, DepartmentService>();
builder.Services.AddTransient<IEmployeeService, EmployeeService>();
builder.Services.AddTransient<IJobTitleService, JobTitleService>();
builder.Services.AddTransient<IModelCategoryService, ModelCategoryService>();
builder.Services.AddTransient<IModelFreqBandService, ModelFreqBandService>();
builder.Services.AddTransient<IModelManufacturerService, ModelManufacturerService>();
builder.Services.AddTransient<IModelModTypeService, ModelModTypeService>();
builder.Services.AddTransient<IModelTypeService, ModelTypeService>();
builder.Services.AddTransient<IOfficeService, OfficeService>();
builder.Services.AddTransient<IRegionService, RegionService>();
builder.Services.AddTransient<ISkyCommOpsLevelService, SkyCommOpsLevelService>();
builder.Services.AddTransient<IUnitModelService, UnitModelService>();
builder.Services.AddTransient<IUnitService, UnitService>();
builder.Services.AddTransient<JsonFileConsolesService, JsonFileConsolesService>();

builder.Services.AddTransient<IAirportsControllerService, AirportsControllerService>();
builder.Services.AddTransient<IOfficesControllerService, OfficesControllerService>();
builder.Services.AddTransient<IPersonnelControllerService, PersonnelControllerService>();
builder.Services.AddTransient<IUnitModelsControllerService, UnitModelsControllerService>();
builder.Services.AddTransient<IUnitsControllerService, UnitsControllerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
