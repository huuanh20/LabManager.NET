using LabManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LabManager.Infrastructure.Data.Configurations;

public class TestOrderConfiguration : IEntityTypeConfiguration<TestOrder>
{
    public void Configure(EntityTypeBuilder<TestOrder> builder)
    {
        builder.HasKey(t => t.Id);
        
        builder.HasOne(t => t.Patient)
            .WithMany(p => p.TestOrders)
            .HasForeignKey(t => t.PatientId)
            .OnDelete(DeleteBehavior.Restrict);
            
        builder.HasOne(t => t.RunByUser)
            .WithMany()
            .HasForeignKey(t => t.RunByUserId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}

public class TestOrderCommentConfiguration : IEntityTypeConfiguration<TestOrderComment>
{
    public void Configure(EntityTypeBuilder<TestOrderComment> builder)
    {
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.Content).IsRequired();
        
        builder.HasOne(c => c.TestOrder)
            .WithMany(t => t.Comments)
            .HasForeignKey(c => c.TestOrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
