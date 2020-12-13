using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SkyCommCoreMVC.Models
{
    public partial class SkyCommDBContext : DbContext
    {
        public SkyCommDBContext()
        {
        }

        public SkyCommDBContext(DbContextOptions<SkyCommDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AirportTypes> AirportTypes { get; set; }
        public virtual DbSet<Airports> Airports { get; set; }
        public virtual DbSet<Continents> Continents { get; set; }
        public virtual DbSet<Countries> Countries { get; set; }
        public virtual DbSet<Departments> Departments { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<JobTitles> JobTitles { get; set; }
        public virtual DbSet<ModelCategories> ModelCategories { get; set; }
        public virtual DbSet<ModelFreqBands> ModelFreqBands { get; set; }
        public virtual DbSet<ModelManufacturers> ModelManufacturers { get; set; }
        public virtual DbSet<ModelModTypes> ModelModTypes { get; set; }
        public virtual DbSet<ModelTypes> ModelTypes { get; set; }
        public virtual DbSet<Offices> Offices { get; set; }
        public virtual DbSet<OrderDetails> OrderDetails { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Regions> Regions { get; set; }
        public virtual DbSet<SkyCommOpsLevels> SkyCommOpsLevels { get; set; }
        public virtual DbSet<SkyCommProjects> SkyCommProjects { get; set; }
        public virtual DbSet<SkyCommServices> SkyCommServices { get; set; }
        public virtual DbSet<UnitModels> UnitModels { get; set; }
        public virtual DbSet<Units> Units { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SkyCommDB;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AirportTypes>(entity =>
            {
                entity.HasKey(e => e.AirportTypeId)
                    .HasName("AirportTypes$PrimaryKey");

                entity.HasIndex(e => e.AirportTypeId)
                    .HasName("AirportTypes$tblAirportTypeID");

                entity.Property(e => e.AirportTypeId).HasColumnName("AirportTypeID");

                entity.Property(e => e.AirportType)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Airports>(entity =>
            {
                entity.HasKey(e => e.AirportId)
                    .HasName("Airports$PrimaryKey");

                entity.HasIndex(e => e.AirportId)
                    .HasName("Airports$tblAirportID");

                entity.HasIndex(e => e.AirportTypeId)
                    .HasName("Airports$AirportTypeID");

                entity.HasIndex(e => e.RegionId)
                    .HasName("Airports$RegionID");

                entity.HasIndex(e => e.SkyCommOpsLevelId)
                    .HasName("Airports$tblSkyCommOpsLevelID");

                entity.Property(e => e.AirportId).HasColumnName("AirportID");

                entity.Property(e => e.AirportGpscode)
                    .HasColumnName("AirportGPSCode")
                    .HasMaxLength(255);

                entity.Property(e => e.AirportHomeLink).HasMaxLength(255);

                entity.Property(e => e.AirportIatacode)
                    .IsRequired()
                    .HasColumnName("AirportIATACode")
                    .HasMaxLength(255);

                entity.Property(e => e.AirportIdentifier)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.AirportLocalCode).HasMaxLength(255);

                entity.Property(e => e.AirportMunicipality).HasMaxLength(255);

                entity.Property(e => e.AirportName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.AirportScheduledService).HasDefaultValueSql("((0))");

                entity.Property(e => e.AirportTypeId).HasColumnName("AirportTypeID");

                entity.Property(e => e.AirportWikipediaLink).HasMaxLength(255);

                entity.Property(e => e.RegionId).HasColumnName("RegionID");

                entity.Property(e => e.SkyCommOpsLevelId).HasColumnName("SkyCommOpsLevelID");

                entity.Property(e => e.SsmaTimeStamp)
                    .IsRequired()
                    .HasColumnName("SSMA_TimeStamp")
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.AirportType)
                    .WithMany(p => p.Airports)
                    .HasForeignKey(d => d.AirportTypeId)
                    .HasConstraintName("Airports$AirportTypeAirport");

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.Airports)
                    .HasForeignKey(d => d.RegionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Airports$RegionAirport");

                entity.HasOne(d => d.SkyCommOpsLevel)
                    .WithMany(p => p.Airports)
                    .HasForeignKey(d => d.SkyCommOpsLevelId)
                    .HasConstraintName("Airports$SkyCommOpsLevelAirport");
            });

            modelBuilder.Entity<Continents>(entity =>
            {
                entity.HasKey(e => e.ContinentId)
                    .HasName("Continents$PrimaryKey");

                entity.Property(e => e.ContinentId).HasColumnName("ContinentID");

                entity.Property(e => e.Continent)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Countries>(entity =>
            {
                entity.HasKey(e => e.CountryId)
                    .HasName("Countries$PrimaryKey");

                entity.HasIndex(e => e.ContinentId)
                    .HasName("Countries$ContinentID");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.ContinentId).HasColumnName("ContinentID");

                entity.Property(e => e.CountryName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.SsmaTimeStamp)
                    .IsRequired()
                    .HasColumnName("SSMA_TimeStamp")
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.Continent)
                    .WithMany(p => p.Countries)
                    .HasForeignKey(d => d.ContinentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Countries$ContientCountry");
            });

            modelBuilder.Entity<Departments>(entity =>
            {
                entity.HasKey(e => e.DepartmentId)
                    .HasName("Departments$PrimaryKey");

                entity.HasIndex(e => e.DepartmentAccountNbr)
                    .HasName("Departments$DepartmentAccountNbr1")
                    .IsUnique();

                entity.HasIndex(e => e.DepartmentCode)
                    .HasName("Departments$DepartmentAccountNbr")
                    .IsUnique();

                entity.HasIndex(e => e.DepartmentId)
                    .HasName("Departments$DepartmentID");

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.DepartmentAccountNbr)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.DepartmentCode)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.DepartmentName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Employees>(entity =>
            {
                entity.HasKey(e => e.EmployeeId)
                    .HasName("Employees$PrimaryKey");

                entity.HasIndex(e => e.EmployeeId)
                    .HasName("Employees$EmployeeID");

                entity.HasIndex(e => e.JobTitleId)
                    .HasName("Employees$JobTitleID");

                entity.HasIndex(e => e.OfficeId)
                    .HasName("Employees$OfficeID");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.EmailAddress).HasMaxLength(255);

                entity.Property(e => e.FirstName).HasMaxLength(255);

                entity.Property(e => e.ImageFileName).HasMaxLength(255);

                entity.Property(e => e.JobTitleId).HasColumnName("JobTitleID");

                entity.Property(e => e.LastName).HasMaxLength(255);

                entity.Property(e => e.MiddleName).HasMaxLength(255);

                entity.Property(e => e.OfficeId).HasColumnName("OfficeID");

                entity.Property(e => e.PhoneNumber).HasMaxLength(255);

                entity.HasOne(d => d.JobTitle)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.JobTitleId)
                    .HasConstraintName("Employees$JobTitleEmployee");

                entity.HasOne(d => d.Office)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.OfficeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Employees$OfficeEmployee");
            });

            modelBuilder.Entity<JobTitles>(entity =>
            {
                entity.HasKey(e => e.JobTitleId)
                    .HasName("JobTitles$PrimaryKey");

                entity.HasIndex(e => e.DepartmentId)
                    .HasName("JobTitles$DepartmentJobTitle");

                entity.HasIndex(e => e.JobTitleId)
                    .HasName("JobTitles$JobTitleID");

                entity.Property(e => e.JobTitleId).HasColumnName("JobTitleID");

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.JobTitle).HasMaxLength(255);

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.JobTitles)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("JobTitles$DepartmentJobTitle");
            });

            modelBuilder.Entity<ModelCategories>(entity =>
            {
                entity.HasKey(e => e.ModelCategoryId)
                    .HasName("ModelCategories$ID");

                entity.HasIndex(e => e.ModelTypeId)
                    .HasName("ModelCategories$tblEquipmentTypeID");

                entity.Property(e => e.ModelCategoryId).HasColumnName("ModelCategoryID");

                entity.Property(e => e.ModelCategory)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.ModelTypeId).HasColumnName("ModelTypeID");

                entity.HasOne(d => d.ModelType)
                    .WithMany(p => p.ModelCategories)
                    .HasForeignKey(d => d.ModelTypeId)
                    .HasConstraintName("ModelCategories$ModelTypeModelCategory");
            });

            modelBuilder.Entity<ModelFreqBands>(entity =>
            {
                entity.HasKey(e => e.ModelFreqBandId)
                    .HasName("ModelFreqBands$PrimaryKey");

                entity.Property(e => e.ModelFreqBandId).HasColumnName("ModelFreqBandID");

                entity.Property(e => e.ModelFreqBand)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<ModelManufacturers>(entity =>
            {
                entity.HasKey(e => e.ModelManufacturerId)
                    .HasName("ModelManufacturers$ID");

                entity.Property(e => e.ModelManufacturerId).HasColumnName("ModelManufacturerID");

                entity.Property(e => e.ModelManufacturerLink).HasMaxLength(255);

                entity.Property(e => e.ModelManufacturerName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<ModelModTypes>(entity =>
            {
                entity.HasKey(e => e.ModelModTypeId)
                    .HasName("ModelModTypes$PrimaryKey");

                entity.Property(e => e.ModelModTypeId).HasColumnName("ModelModTypeID");

                entity.Property(e => e.ModelModType)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<ModelTypes>(entity =>
            {
                entity.HasKey(e => e.ModelTypeId)
                    .HasName("ModelTypes$ID");

                entity.Property(e => e.ModelTypeId).HasColumnName("ModelTypeID");

                entity.Property(e => e.ModelType)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Offices>(entity =>
            {
                entity.HasKey(e => e.OfficeId)
                    .HasName("Offices$PrimaryKey");

                entity.HasIndex(e => e.CountryId)
                    .HasName("Offices$CountryOffice");

                entity.Property(e => e.OfficeId).HasColumnName("OfficeID");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.OfficeAddress1)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.OfficeAddress2)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.OfficeCity)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.OfficeFaxNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.OfficeName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.OfficePhoneNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.OfficePostalCode).HasMaxLength(20);

                entity.Property(e => e.OfficeState).HasMaxLength(50);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Offices)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Offices$CountryOffice");
            });

            modelBuilder.Entity<OrderDetails>(entity =>
            {
                entity.HasIndex(e => e.OrderId)
                    .HasName("OrderDetails$OrderOrderDetails");

                entity.HasIndex(e => e.UnitModelId)
                    .HasName("OrderDetails$ModelOrderDetails");

                entity.Property(e => e.OrderDetailsId).HasColumnName("OrderDetailsID");

                entity.Property(e => e.ModelPrice).HasColumnType("money");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.UnitModelId).HasColumnName("UnitModelId");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("OrderDetails$OrderOrderDetails");

                entity.HasOne(d => d.UnitModels)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.UnitModelId)
                    .HasConstraintName("OrderDetails$ModelOrderDetails");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("Orders$PrimaryKey");

                entity.HasIndex(e => e.AirportId)
                    .HasName("Orders$AirportOrder");

                entity.HasIndex(e => e.EmployeeId)
                    .HasName("Orders$EmployeeOrder");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.AirportId).HasColumnName("AirportID");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.OrderDate).HasColumnType("datetime2(0)");

                entity.Property(e => e.SsmaTimeStamp)
                    .IsRequired()
                    .HasColumnName("SSMA_TimeStamp")
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.Airport)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.AirportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Orders$AirportOrder");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Orders$EmployeeOrder");
            });

            modelBuilder.Entity<Regions>(entity =>
            {
                entity.HasKey(e => e.RegionId)
                    .HasName("Regions$PrimaryKey");

                entity.HasIndex(e => e.CountryId)
                    .HasName("Regions$tblISOCountryID");

                entity.Property(e => e.RegionId).HasColumnName("RegionID");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.RegionCode)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Regions)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Regions$CountryRegion");
            });

            modelBuilder.Entity<SkyCommOpsLevels>(entity =>
            {
                entity.HasKey(e => e.SkyCommOpsLevelId)
                    .HasName("SkyCommOpsLevels$PrimaryKey");

                entity.HasIndex(e => e.SkyCommOpsLevelId)
                    .HasName("SkyCommOpsLevels$tblSkyCommOpsLevelID");

                entity.Property(e => e.SkyCommOpsLevelId).HasColumnName("SkyCommOpsLevelID");

                entity.Property(e => e.SkyCommOpsLevel)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<SkyCommProjects>(entity =>
            {
                entity.HasKey(e => e.SkyCommProjectId)
                    .HasName("SkyCommProjects$PrimaryKey");

                entity.HasIndex(e => e.AirportId)
                    .HasName("SkyCommProjects$AirportProject");

                entity.HasIndex(e => e.EmployeeId)
                    .HasName("SkyCommProjects$EmployeeProject");

                entity.HasIndex(e => e.SkyCommProjectId)
                    .HasName("SkyCommProjects$ProjectID");

                entity.Property(e => e.SkyCommProjectId).HasColumnName("SkyCommProjectID");

                entity.Property(e => e.AirportId).HasColumnName("AirportID");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.ProjectEndDate).HasColumnType("datetime2(0)");

                entity.Property(e => e.ProjectStartDate).HasColumnType("datetime2(0)");

                entity.Property(e => e.SsmaTimeStamp)
                    .IsRequired()
                    .HasColumnName("SSMA_TimeStamp")
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.Airport)
                    .WithMany(p => p.SkyCommProjects)
                    .HasForeignKey(d => d.AirportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SkyCommProjects$AirportProject");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.SkyCommProjects)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SkyCommProjects$EmployeeProject");
            });

            modelBuilder.Entity<SkyCommServices>(entity =>
            {
                entity.HasKey(e => e.SkyCommServiceId)
                    .HasName("SkyCommServices$PrimaryKey");

                entity.HasIndex(e => e.EmployeeId)
                    .HasName("SkyCommServices$EmployeeService");

                entity.HasIndex(e => e.SkyCommServiceId)
                    .HasName("SkyCommServices$ProjectID")
                    .IsUnique();

                entity.HasIndex(e => e.UnitId)
                    .HasName("SkyCommServices$UnitService");

                entity.Property(e => e.SkyCommServiceId).HasColumnName("SkyCommServiceID");

                entity.Property(e => e.ContactEmail)
                    .IsRequired()
                    .HasColumnName("ContactEMail")
                    .HasMaxLength(255);

                entity.Property(e => e.ContactName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.ContactPhone)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.Problem)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Repair).HasMaxLength(255);

                entity.Property(e => e.ServiceEndDate).HasColumnType("datetime2(0)");

                entity.Property(e => e.ServiceStartDate).HasColumnType("datetime2(0)");

                entity.Property(e => e.SsmaTimeStamp)
                    .IsRequired()
                    .HasColumnName("SSMA_TimeStamp")
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.UnitId).HasColumnName("UnitID");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.SkyCommServices)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SkyCommServices$EmployeeService");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.SkyCommServices)
                    .HasForeignKey(d => d.UnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SkyCommServices$UnitService");
            });

            modelBuilder.Entity<UnitModels>(entity =>
            {
                entity.HasKey(e => e.UnitModelId)
                    .HasName("UnitModels$ID");

                entity.HasIndex(e => e.ModelCategoryId)
                    .HasName("UnitModels$ModelCategoryModel");

                entity.HasIndex(e => e.ModelFreqBandId)
                    .HasName("UnitModels$ModelFreqBandModel");

                entity.HasIndex(e => e.ModelManufacturerId)
                    .HasName("UnitModels$ManufacturerModel");

                entity.HasIndex(e => e.ModelModTypeId)
                    .HasName("UnitModels$ModelModTypeModel");

                entity.Property(e => e.UnitModelId).HasColumnName("UnitModelId");

                entity.Property(e => e.ModelCategoryId).HasColumnName("ModelCategoryID");

                entity.Property(e => e.ModelCode)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.ModelFreqBandId).HasColumnName("ModelFreqBandID");

                entity.Property(e => e.ModelImage)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.ModelManufacturerId).HasColumnName("ModelManufacturerID");

                entity.Property(e => e.ModelModTypeId).HasColumnName("ModelModTypeID");

                entity.Property(e => e.ModelMsrp)
                    .HasColumnName("ModelMSRP")
                    .HasColumnType("money");

                entity.Property(e => e.ModelName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.ModelCategory)
                    .WithMany(p => p.UnitModels)
                    .HasForeignKey(d => d.ModelCategoryId)
                    .HasConstraintName("UnitModels$ModelCategoryModel");

                entity.HasOne(d => d.ModelFreqBand)
                    .WithMany(p => p.UnitModels)
                    .HasForeignKey(d => d.ModelFreqBandId)
                    .HasConstraintName("UnitModels$ModelFreqBandModel");

                entity.HasOne(d => d.ModelManufacturer)
                    .WithMany(p => p.UnitModels)
                    .HasForeignKey(d => d.ModelManufacturerId)
                    .HasConstraintName("UnitModels$ManufacturerModel");

                entity.HasOne(d => d.ModelModType)
                    .WithMany(p => p.UnitModels)
                    .HasForeignKey(d => d.ModelModTypeId)
                    .HasConstraintName("UnitModels$ModelModTypeModel");
            });

            modelBuilder.Entity<Units>(entity =>
            {
                entity.HasKey(e => e.UnitId)
                    .HasName("Units$PrimaryKey");

                entity.HasIndex(e => e.AirportId)
                    .HasName("Units$tblAirportID");

                entity.HasIndex(e => e.UnitModelId)
                    .HasName("Units$tblModelID");

                entity.Property(e => e.UnitId).HasColumnName("UnitID");

                entity.Property(e => e.AirportId).HasColumnName("AirportID");

                entity.Property(e => e.UnitCost).HasColumnType("money");

                entity.Property(e => e.UnitModelId).HasColumnName("UnitModelId");

                entity.Property(e => e.UnitScnbr)
                    .IsRequired()
                    .HasColumnName("UnitSCNbr")
                    .HasMaxLength(255);

                entity.Property(e => e.UnitSerNbr)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.Airport)
                    .WithMany(p => p.Units)
                    .HasForeignKey(d => d.AirportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Units$AirportUnit");

                entity.HasOne(d => d.UnitModels)
                    .WithMany(p => p.Units)
                    .HasForeignKey(d => d.UnitModelId)
                    .HasConstraintName("Units$ModelUnit");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
