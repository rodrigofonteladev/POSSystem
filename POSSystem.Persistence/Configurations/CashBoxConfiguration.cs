using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POSSystem.Domain.Entities;

namespace POSSystem.Persistence.Configurations
{
    public class CashBoxConfiguration : IEntityTypeConfiguration<CashBox>
    {
        public void Configure(EntityTypeBuilder<CashBox> builder)
        {
            builder.Property(col => col.StartTime)
                .IsRequired();
            builder.Property(col => col.InitialAmount)
                .HasPrecision(10, 2)
                .IsRequired();
            builder.Property(col => col.TotalSales)
                .HasPrecision(10, 2);
            builder.Property(col => col.FinalAmount)
                .HasPrecision(10, 2);
            builder.Property(col => col.Difference)
                .HasPrecision(10, 2);
            builder.Property(col => col.Status)
                .HasConversion<string>()
                .HasMaxLength(20);
            builder
                .HasOne(cb => cb.ApplicationUser)
                .WithMany(u => u.CashBoxes)
                .HasForeignKey(cb => cb.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}