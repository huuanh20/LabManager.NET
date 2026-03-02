using LabManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LabManager.Infrastructure.Data.Configurations;

public class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.FullName).IsRequired().HasMaxLength(200);
        builder.Property(p => p.PhoneNumber).HasMaxLength(20);
        builder.Property(p => p.Email).HasMaxLength(150);
    }
}
