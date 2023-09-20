using Microsoft.EntityFrameworkCore;
using SkyCommNet7MVC.Domain.Models;

namespace SkyCommNet7MVC.Data.Repositories;

public partial class SkyCommDbContext : DbContext
{
    public SkyCommDbContext()
    {
    }

    public SkyCommDbContext(DbContextOptions<SkyCommDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Airport> Airports { get; set; }
    public virtual DbSet<AirportType> AirportTypes { get; set; }
    public virtual DbSet<Continent> Continents { get; set; }
    public virtual DbSet<Country> Countries { get; set; }
    public virtual DbSet<Department> Departments { get; set; }
    public virtual DbSet<Employee> Employees { get; set; }
    public virtual DbSet<JobTitle> JobTitles { get; set; }
    public virtual DbSet<ModelCategory> ModelCategories { get; set; }
    public virtual DbSet<ModelFreqBand> ModelFreqBands { get; set; }
    public virtual DbSet<ModelManufacturer> ModelManufacturers { get; set; }
    public virtual DbSet<ModelModType> ModelModTypes { get; set; }
    public virtual DbSet<ModelType> ModelTypes { get; set; }
    public virtual DbSet<Office> Offices { get; set; }
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<OrderDetail> OrderDetails { get; set; }
    public virtual DbSet<Region> Regions { get; set; }
    public virtual DbSet<SkyCommOpsLevel> SkyCommOpsLevels { get; set; }
    public virtual DbSet<SkyCommProject> SkyCommProjects { get; set; }
    public virtual DbSet<SkyCommService> SkyCommServices { get; set; }
    public virtual DbSet<Unit> Units { get; set; }
    public virtual DbSet<UnitModel> UnitModels { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SkyCommDB");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Airport>(entity =>
        {
            entity.HasKey(e => e.AirportId).HasName("Airports$PrimaryKey");

            entity.Property(e => e.AirportId).HasColumnName("AirportID");
            entity.Property(e => e.AirportGpscode)
                .HasMaxLength(255)
                .HasColumnName("AirportGPSCode");
            entity.Property(e => e.AirportHomeLink).HasMaxLength(255);
            entity.Property(e => e.AirportIatacode)
                .HasMaxLength(255)
                .HasColumnName("AirportIATACode");
            entity.Property(e => e.AirportIdentifier).HasMaxLength(255);
            entity.Property(e => e.AirportLocalCode).HasMaxLength(255);
            entity.Property(e => e.AirportMunicipality).HasMaxLength(255);
            entity.Property(e => e.AirportName).HasMaxLength(255);
            entity.Property(e => e.AirportScheduledService).HasDefaultValueSql("((0))");
            entity.Property(e => e.AirportTypeId).HasColumnName("AirportTypeID");
            entity.Property(e => e.AirportWikipediaLink).HasMaxLength(255);
            entity.Property(e => e.RegionId).HasColumnName("RegionID");
            entity.Property(e => e.SkyCommOpsLevelId).HasColumnName("SkyCommOpsLevelID");
            entity.Property(e => e.SsmaTimeStamp)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("SSMA_TimeStamp");

            entity.HasOne(d => d.AirportType).WithMany(p => p.Airports)
                .HasForeignKey(d => d.AirportTypeId)
                .HasConstraintName("Airports$AirportTypeAirport");

            entity.HasOne(d => d.Region).WithMany(p => p.Airports)
                .HasForeignKey(d => d.RegionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Airports$RegionAirport");

            entity.HasOne(d => d.SkyCommOpsLevel).WithMany(p => p.Airports)
                .HasForeignKey(d => d.SkyCommOpsLevelId)
                .HasConstraintName("Airports$SkyCommOpsLevelAirport");
        });

        modelBuilder.Entity<AirportType>(entity =>
        {
            entity.HasKey(e => e.AirportTypeId).HasName("AirportTypes$PrimaryKey");

            entity.Property(e => e.AirportTypeId).HasColumnName("AirportTypeID");
            entity.Property(e => e.AirportType1)
                .HasMaxLength(255)
                .HasColumnName("AirportType");
        });

        modelBuilder.Entity<Continent>(entity =>
        {
            entity.HasKey(e => e.ContinentId).HasName("Continents$PrimaryKey");

            entity.Property(e => e.ContinentId).HasColumnName("ContinentID");
            entity.Property(e => e.Continent1)
                .HasMaxLength(255)
                .HasColumnName("Continent");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.CountryId).HasName("Countries$PrimaryKey");

            entity.Property(e => e.CountryId).HasColumnName("CountryID");
            entity.Property(e => e.ContinentId).HasColumnName("ContinentID");
            entity.Property(e => e.CountryName).HasMaxLength(255);
            entity.Property(e => e.SsmaTimeStamp)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("SSMA_TimeStamp");

            entity.HasOne(d => d.Continent).WithMany(p => p.Countries)
                .HasForeignKey(d => d.ContinentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Countries$ContientCountry");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("Departments$PrimaryKey");

            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.DepartmentAccountNbr).HasMaxLength(255);
            entity.Property(e => e.DepartmentCode).HasMaxLength(255);
            entity.Property(e => e.DepartmentName).HasMaxLength(255);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("Employees$PrimaryKey");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.EmailAddress).HasMaxLength(255);
            entity.Property(e => e.FirstName).HasMaxLength(255);
            entity.Property(e => e.ImageFileName).HasMaxLength(255);
            entity.Property(e => e.JobTitleId).HasColumnName("JobTitleID");
            entity.Property(e => e.LastName).HasMaxLength(255);
            entity.Property(e => e.MiddleName).HasMaxLength(255);
            entity.Property(e => e.OfficeId).HasColumnName("OfficeID");
            entity.Property(e => e.PhoneNumber).HasMaxLength(255);

            entity.HasOne(d => d.JobTitle).WithMany(p => p.Employees)
                .HasForeignKey(d => d.JobTitleId)
                .HasConstraintName("Employees$JobTitleEmployee");

            entity.HasOne(d => d.Office).WithMany(p => p.Employees)
                .HasForeignKey(d => d.OfficeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Employees$OfficeEmployee");
        });

        modelBuilder.Entity<JobTitle>(entity =>
        {
            entity.HasKey(e => e.JobTitleId).HasName("JobTitles$PrimaryKey");

            entity.Property(e => e.JobTitleId).HasColumnName("JobTitleID");
            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.JobTitle1)
                .HasMaxLength(255)
                .HasColumnName("JobTitle");

            entity.HasOne(d => d.Department).WithMany(p => p.JobTitles)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("JobTitles$DepartmentJobTitle");
        });

        modelBuilder.Entity<ModelCategory>(entity =>
        {
            entity.HasKey(e => e.ModelCategoryId).HasName("ModelCategories$ID");

            entity.Property(e => e.ModelCategoryId).HasColumnName("ModelCategoryID");
            entity.Property(e => e.ModelCategory1)
                .HasMaxLength(255)
                .HasColumnName("ModelCategory");
            entity.Property(e => e.ModelTypeId).HasColumnName("ModelTypeID");

            entity.HasOne(d => d.ModelType).WithMany(p => p.ModelCategories)
                .HasForeignKey(d => d.ModelTypeId)
                .HasConstraintName("ModelCategories$ModelTypeModelCategory");
        });

        modelBuilder.Entity<ModelFreqBand>(entity =>
        {
            entity.HasKey(e => e.ModelFreqBandId).HasName("ModelFreqBands$PrimaryKey");

            entity.Property(e => e.ModelFreqBandId).HasColumnName("ModelFreqBandID");
            entity.Property(e => e.ModelFreqBand1)
                .HasMaxLength(255)
                .HasColumnName("ModelFreqBand");
        });

        modelBuilder.Entity<ModelManufacturer>(entity =>
        {
            entity.HasKey(e => e.ModelManufacturerId).HasName("ModelManufacturers$ID");

            entity.Property(e => e.ModelManufacturerId).HasColumnName("ModelManufacturerID");
            entity.Property(e => e.ModelManufacturerLink).HasMaxLength(255);
            entity.Property(e => e.ModelManufacturerName).HasMaxLength(255);
        });

        modelBuilder.Entity<ModelModType>(entity =>
        {
            entity.HasKey(e => e.ModelModTypeId).HasName("ModelModTypes$PrimaryKey");

            entity.Property(e => e.ModelModTypeId).HasColumnName("ModelModTypeID");
            entity.Property(e => e.ModelModType1)
                .HasMaxLength(255)
                .HasColumnName("ModelModType");
        });

        modelBuilder.Entity<ModelType>(entity =>
        {
            entity.HasKey(e => e.ModelTypeId).HasName("ModelTypes$ID");

            entity.Property(e => e.ModelTypeId).HasColumnName("ModelTypeID");
            entity.Property(e => e.ModelType1)
                .HasMaxLength(255)
                .HasColumnName("ModelType");
        });

        modelBuilder.Entity<Office>(entity =>
        {
            entity.HasKey(e => e.OfficeId).HasName("Offices$PrimaryKey");

            entity.Property(e => e.OfficeId).HasColumnName("OfficeID");
            entity.Property(e => e.CountryId).HasColumnName("CountryID");
            entity.Property(e => e.OfficeAddress1).HasMaxLength(100);
            entity.Property(e => e.OfficeAddress2).HasMaxLength(100);
            entity.Property(e => e.OfficeCity).HasMaxLength(100);
            entity.Property(e => e.OfficeFaxNumber).HasMaxLength(50);
            entity.Property(e => e.OfficeName).HasMaxLength(255);
            entity.Property(e => e.OfficePhoneNumber).HasMaxLength(50);
            entity.Property(e => e.OfficePostalCode).HasMaxLength(20);
            entity.Property(e => e.OfficeState).HasMaxLength(50);

            entity.HasOne(d => d.Country).WithMany(p => p.Offices)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Offices$CountryOffice");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("Orders$PrimaryKey");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.AirportId).HasColumnName("AirportID");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.OrderDate).HasPrecision(0);
            entity.Property(e => e.SsmaTimeStamp)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("SSMA_TimeStamp");

            entity.HasOne(d => d.Airport).WithMany(p => p.Orders)
                .HasForeignKey(d => d.AirportId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Orders$AirportOrder");

            entity.HasOne(d => d.Employee).WithMany(p => p.Orders)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Orders$EmployeeOrder");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailsId).HasName("OrderDetails$PrimaryKey");

            entity.Property(e => e.OrderDetailsId).HasColumnName("OrderDetailsID");
            entity.Property(e => e.ModelPrice).HasColumnType("money");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.UnitModelId).HasColumnName("UnitModelID");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("OrderDetails$OrderOrderDetails");

            entity.HasOne(d => d.UnitModel).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.UnitModelId)
                .HasConstraintName("OrderDetails$ModelOrderDetails");
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.HasKey(e => e.RegionId).HasName("Regions$PrimaryKey");

            entity.Property(e => e.RegionId).HasColumnName("RegionID");
            entity.Property(e => e.CountryId).HasColumnName("CountryID");
            entity.Property(e => e.RegionCode).HasMaxLength(255);

            entity.HasOne(d => d.Country).WithMany(p => p.Regions)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Regions$CountryRegion");
        });

        modelBuilder.Entity<SkyCommOpsLevel>(entity =>
        {
            entity.HasKey(e => e.SkyCommOpsLevelId).HasName("SkyCommOpsLevels$PrimaryKey");

            entity.Property(e => e.SkyCommOpsLevelId).HasColumnName("SkyCommOpsLevelID");
            entity.Property(e => e.SkyCommOpsLevel1)
                .HasMaxLength(255)
                .HasColumnName("SkyCommOpsLevel");
        });

        modelBuilder.Entity<SkyCommProject>(entity =>
        {
            entity.HasKey(e => e.ProjectId).HasName("SkyCommProjects$PrimaryKey");

            entity.Property(e => e.ProjectId).HasColumnName("ProjectID");
            entity.Property(e => e.AirportId).HasColumnName("AirportID");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.ProjectEndDate).HasPrecision(0);
            entity.Property(e => e.ProjectStartDate).HasPrecision(0);
            entity.Property(e => e.SsmaTimeStamp)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("SSMA_TimeStamp");

            entity.HasOne(d => d.Airport).WithMany(p => p.SkyCommProjects)
                .HasForeignKey(d => d.AirportId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SkyCommProjects$AirportProject");

            entity.HasOne(d => d.Employee).WithMany(p => p.SkyCommProjects)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SkyCommProjects$EmployeeProject");
        });

        modelBuilder.Entity<SkyCommService>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("SkyCommServices$PrimaryKey");

            entity.Property(e => e.ServiceId).HasColumnName("ServiceID");
            entity.Property(e => e.ContactEmail)
                .HasMaxLength(255)
                .HasColumnName("ContactEMail");
            entity.Property(e => e.ContactName).HasMaxLength(255);
            entity.Property(e => e.ContactPhone).HasMaxLength(255);
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.Problem).HasMaxLength(255);
            entity.Property(e => e.Repair).HasMaxLength(255);
            entity.Property(e => e.ServiceEndDate).HasPrecision(0);
            entity.Property(e => e.ServiceStartDate).HasPrecision(0);
            entity.Property(e => e.SsmaTimeStamp)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("SSMA_TimeStamp");
            entity.Property(e => e.UnitId).HasColumnName("UnitID");

            entity.HasOne(d => d.Employee).WithMany(p => p.SkyCommServices)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SkyCommServices$EmployeeService");

            entity.HasOne(d => d.Unit).WithMany(p => p.SkyCommServices)
                .HasForeignKey(d => d.UnitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SkyCommServices$UnitService");
        });

        modelBuilder.Entity<Unit>(entity =>
        {
            entity.HasKey(e => e.UnitId).HasName("Units$PrimaryKey");

            entity.Property(e => e.UnitId).HasColumnName("UnitID");
            entity.Property(e => e.AirportId).HasColumnName("AirportID");
            entity.Property(e => e.UnitCost).HasColumnType("money");
            entity.Property(e => e.UnitModelId).HasColumnName("UnitModelID");
            entity.Property(e => e.UnitScnbr)
                .HasMaxLength(255)
                .HasColumnName("UnitSCNbr");
            entity.Property(e => e.UnitSerNbr).HasMaxLength(255);

            entity.HasOne(d => d.Airport).WithMany(p => p.Units)
                .HasForeignKey(d => d.AirportId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Units$AirportUnit");

            entity.HasOne(d => d.UnitModel).WithMany(p => p.Units)
                .HasForeignKey(d => d.UnitModelId)
                .HasConstraintName("Units$ModelUnit");
        });

        modelBuilder.Entity<UnitModel>(entity =>
        {
            entity.HasKey(e => e.UnitModelId).HasName("UnitModels$ID");

            entity.Property(e => e.UnitModelId).HasColumnName("UnitModelID");
            entity.Property(e => e.ModelCategoryId).HasColumnName("ModelCategoryID");
            entity.Property(e => e.ModelCode).HasMaxLength(255);
            entity.Property(e => e.ModelFreqBandId).HasColumnName("ModelFreqBandID");
            entity.Property(e => e.ModelImage).HasMaxLength(255);
            entity.Property(e => e.ModelManufacturerId).HasColumnName("ModelManufacturerID");
            entity.Property(e => e.ModelModTypeId).HasColumnName("ModelModTypeID");
            entity.Property(e => e.ModelMsrp)
                .HasColumnType("money")
                .HasColumnName("ModelMSRP");
            entity.Property(e => e.ModelName).HasMaxLength(255);

            entity.HasOne(d => d.ModelCategory).WithMany(p => p.UnitModels)
                .HasForeignKey(d => d.ModelCategoryId)
                .HasConstraintName("UnitModels$ModelCategoryModel");

            entity.HasOne(d => d.ModelFreqBand).WithMany(p => p.UnitModels)
                .HasForeignKey(d => d.ModelFreqBandId)
                .HasConstraintName("UnitModels$ModelFreqBandModel");

            entity.HasOne(d => d.ModelManufacturer).WithMany(p => p.UnitModels)
                .HasForeignKey(d => d.ModelManufacturerId)
                .HasConstraintName("UnitModels$ManufacturerModel");

            entity.HasOne(d => d.ModelModType).WithMany(p => p.UnitModels)
                .HasForeignKey(d => d.ModelModTypeId)
                .HasConstraintName("UnitModels$ModelModTypeModel");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
