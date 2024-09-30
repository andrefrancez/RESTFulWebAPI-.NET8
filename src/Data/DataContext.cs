using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VehiclesAPI.Models;

namespace VehiclesAPI.Data;

public class DataContext : IdentityDbContext<ApplicationUser>
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    public DbSet<CarMake> CarMakes { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CarMake>()
            .Property(cm => cm.Description)
            .HasMaxLength(300)
            .IsRequired(true);

        modelBuilder.Entity<Category>()
            .Property(c => c.Description)
            .HasMaxLength(250)
            .IsRequired(true);

        modelBuilder.Entity<Vehicle>()
            .Property(v => v.Description)
            .HasMaxLength(400)
            .IsRequired(true);

        modelBuilder.Entity<Vehicle>()
        .Property(v => v.Price)
        .HasColumnType("decimal(18, 2)");

        modelBuilder.Entity<Vehicle>()
            .HasOne(v => v.CarMake)
            .WithMany(cm => cm.Vehicles)
            .HasForeignKey(v => v.CarMakeId);

        modelBuilder.Entity<Vehicle>()
            .HasOne(v => v.Category)
            .WithMany(c => c.Vehicles)
            .HasForeignKey(v => v.CategoryId);

    }
}
