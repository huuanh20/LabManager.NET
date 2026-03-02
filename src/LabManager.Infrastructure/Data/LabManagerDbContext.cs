using LabManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LabManager.Infrastructure.Data;

public class LabManagerDbContext : DbContext
{
    public LabManagerDbContext(DbContextOptions<LabManagerDbContext> options)
        : base(options)
    {
    }

    public DbSet<Lab> Labs { get; set; }
    public DbSet<Equipment> Equipment { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<TestOrder> TestOrders { get; set; }
    public DbSet<Sample> Samples { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Lab>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Location).IsRequired().HasMaxLength(300);
            entity.Property(e => e.ContactEmail).IsRequired().HasMaxLength(200);
        });

        modelBuilder.Entity<Equipment>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Model).IsRequired().HasMaxLength(200);
            entity.Property(e => e.SerialNumber).IsRequired().HasMaxLength(100);
            entity.HasOne(e => e.Lab)
                  .WithMany(l => l.Equipment)
                  .HasForeignKey(e => e.LabId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Gender).IsRequired().HasMaxLength(20);
        });

        modelBuilder.Entity<TestOrder>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.OrderNumber).IsRequired().HasMaxLength(50);
            entity.Property(e => e.TestName).IsRequired().HasMaxLength(200);
            entity.HasOne(e => e.Patient)
                  .WithMany(p => p.TestOrders)
                  .HasForeignKey(e => e.PatientId)
                  .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.Lab)
                  .WithMany(l => l.TestOrders)
                  .HasForeignKey(e => e.LabId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Sample>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Barcode).IsRequired().HasMaxLength(100);
            entity.HasOne(e => e.TestOrder)
                  .WithMany(o => o.Samples)
                  .HasForeignKey(e => e.TestOrderId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
