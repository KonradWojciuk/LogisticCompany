using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<Truck> Trucks { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Shipment>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Truck)
                    .WithMany(t => t.Shipment)
                    .HasForeignKey(e => e.TruckId)
                    .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.Warehouse)
                    .WithMany(t => t.Shipment)
                    .HasForeignKey(e => e.WarehouseId)
                    .OnDelete(DeleteBehavior.Restrict);
                entity.Property(e => e.Status).IsRequired();
            });

            modelBuilder.Entity<Truck>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.DriverFirstName).HasMaxLength(50).IsRequired();
                entity.Property(e => e.DriverLastName).HasMaxLength(50).IsRequired();
                entity.Property(e => e.DriverPhoneNumber).HasMaxLength(15).IsRequired();
                entity.Property(e => e.LicensePlate).HasMaxLength(20).IsRequired();
                entity.Property(e => e.DriverCardNumber).HasMaxLength(20).IsRequired();
            });

            modelBuilder.Entity<Warehouse>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Description).HasMaxLength(200);
                entity.Property(e => e.Country).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Region).HasMaxLength(50).IsRequired();
                entity.Property(e => e.City).HasMaxLength(50).IsRequired();
                entity.Property(e => e.PostalCode).HasMaxLength(10).IsRequired();
                entity.Property(e => e.Street).HasMaxLength(100).IsRequired();
                entity.Property(e => e.StreetNumber).HasMaxLength(10).IsRequired();
            });
        }
    }
}
