using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace NewDesignTrial.Models.DB
{
    public partial class DAD_CarSalesdbContext : DbContext
    {
        public DAD_CarSalesdbContext()
        {
        }

        public DAD_CarSalesdbContext(DbContextOptions<DAD_CarSalesdbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Total> Total { get; set; } = null!;
        public virtual DbSet<LeastFiveRentedModels> LeastFiveRentedModels { get; set; } = null!;
        public virtual DbSet<TopFiveCars> TopFiveCars { get; set; } = null!;
        public virtual DbSet<CarRentalsWithCustomerName> CarRentalsWithCustomerNmae { get; set; } = null!;
        public virtual DbSet<CarCustomerWithName> CarCustomerWithNames { get; set; } = null!;
        public virtual DbSet<CarsModelFeatures> CarsModelFeatures { get; set; } = null!;
        public virtual DbSet<CarEmployeeWithName> CarEmployeeWithNames { get; set; } = null!;
        public virtual DbSet<IndividualCar> IndividualCars { get; set; } = null!;
        public virtual DbSet<CarCustomer> CarCustomers { get; set; } = null!;
        public virtual DbSet<CarEmployee> CarEmployees { get; set; } = null!;
        public virtual DbSet<CarFeature> CarFeatures { get; set; } = null!;
        public virtual DbSet<CarModel> CarModels { get; set; } = null!;
        public virtual DbSet<CarPerson> CarPeople { get; set; } = null!;
        public virtual DbSet<CarRental> CarRentals { get; set; } = null!;
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("data source =LAPTOP-LL7MF4G0\\SQLEXPRESS;database = CAR_SALES2; Integrated security =True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IndividualCar>(entity =>
            {
                entity.HasKey(e => e.CarId);

                entity.ToTable("IndividualCar");

                entity.Property(e => e.CarId).HasColumnName("CarID");

                entity.Property(e => e.AdvanceDepositRequired).HasColumnType("money");

                entity.Property(e => e.Colour)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DailyRentalPrice).HasColumnType("money");

                entity.Property(e => e.DateImported).HasColumnType("date");

                entity.Property(e => e.FuelType)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.RegistrationExpiryDate).HasColumnType("date");

                entity.Property(e => e.RegistrationNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Transmission)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CarModelId).HasColumnName("CarModelID");

                entity.Property(e => e.WofexpiryDate)
                    .HasColumnType("date")
                    .HasColumnName("WOFExpiryDate");

                entity.HasOne(d => d.CarModel)
                    .WithMany(p => p.IndividualCars)
                    .HasForeignKey(d => d.CarModelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IndividualCar_CarModel");

                entity.HasMany(d => d.Features)
                    .WithMany(p => p.Cars)
                    .UsingEntity<Dictionary<string, object>>(
                        "CarFeatureAssociation",
                        l => l.HasOne<CarFeature>().WithMany().HasForeignKey("FeatureId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Car_Feature_Association_CarFeature"),
                        r => r.HasOne<IndividualCar>().WithMany().HasForeignKey("CarId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Car_Feature_Association_IndividualCar"),
                        j =>
                        {
                            j.HasKey("CarId", "FeatureId");

                            j.ToTable("Car_Feature_Association");

                            j.IndexerProperty<int>("CarId").HasColumnName("CarID");

                            j.IndexerProperty<int>("FeatureId").HasColumnName("FeatureID");
                        });
            });

            modelBuilder.Entity<CarCustomer>(entity =>
            {
                entity.HasKey(e => e.CustomerId);

                entity.ToTable("CarCustomer");

                entity.Property(e => e.CustomerId)
                    .ValueGeneratedNever()
                    .HasColumnName("CustomerID");

                entity.Property(e => e.LicenseExpiryDate).HasColumnType("date");

                entity.Property(e => e.LicenseNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Customer)
                    .WithOne(p => p.CarCustomer)
                    .HasForeignKey<CarCustomer>(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CarCustomer_CarPerson");
            });

            modelBuilder.Entity<CarEmployee>(entity =>
            {
                entity.HasKey(e => e.EmployeeId);

                entity.ToTable("CarEmployee");

                entity.Property(e => e.EmployeeId)
                    .ValueGeneratedNever()
                    .HasColumnName("EmployeeID");

                entity.Property(e => e.OfficeAddress)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneExtensionNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Role)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Employee)
                    .WithOne(p => p.CarEmployee)
                    .HasForeignKey<CarEmployee>(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CarEmployee_CarPerson");
            });

            modelBuilder.Entity<CarFeature>(entity =>
            {
                entity.HasKey(e => e.FeatureId);

                entity.ToTable("CarFeature");

                entity.Property(e => e.FeatureId).HasColumnName("FeatureID");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CarModel>(entity =>
            {
                entity.HasKey(e => e.ModelId);

                entity.ToTable("CarModel");

                entity.Property(e => e.ModelId).HasColumnName("ModelID");

                entity.Property(e => e.Manufacturer)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Model)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Size)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CarPerson>(entity =>
            {
                entity.HasKey(e => e.PersonId);

                entity.ToTable("CarPerson");

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.Property(e => e.Address)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Telephone)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CarRental>(entity =>
            {
                entity.HasKey(e => e.RentalId);

                entity.ToTable("CarRental");

                entity.Property(e => e.RentalId).HasColumnName("RentalID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.RentDate).HasColumnType("date");

                entity.Property(e => e.ReturnDate).HasColumnType("date");

                entity.Property(e => e.ReturnDueDate).HasColumnType("date");

                entity.Property(e => e.TotalPrice).HasColumnType("money");

                entity.Property(e => e.CarId).HasColumnName("CarID");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CarRentals)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CarRental_CarCustomer");

                entity.HasOne(d => d.Car)
                    .WithMany(p => p.CarRentals)
                    .HasForeignKey(d => d.CarId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CarRental_IndividualCar");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
