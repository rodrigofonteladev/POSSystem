using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POSSystem.Domain.Entities;

namespace POSSystem.Persistence.Configurations
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.Property(col => col.Total)
                .HasPrecision(10, 2);
            builder
                .HasOne(s => s.User)
                .WithMany(u => u.Sales)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            builder
                .HasOne(s => s.CashBox)
                .WithMany(cb => cb.Sales)
                .HasForeignKey(s => s.CashBoxId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}