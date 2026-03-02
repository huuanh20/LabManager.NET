using LabManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LabManager.Infrastructure.Data.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(r => r.Id);
        
        builder.Property(r => r.Name).IsRequired().HasMaxLength(100);
        builder.Property(r => r.Code).IsRequired().HasMaxLength(50);
        
        builder.HasIndex(r => r.Code).IsUnique();
    }
}
