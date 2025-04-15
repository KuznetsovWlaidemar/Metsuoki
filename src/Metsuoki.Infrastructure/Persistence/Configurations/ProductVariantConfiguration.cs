using Metsuoki.Domain.Entities.Products.ProductVariants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Metsuoki.Infrastructure.Persistence.Configurations;
public class ProductVariantConfiguration : IEntityTypeConfiguration<ProductVariant>
{
    public void Configure(EntityTypeBuilder<ProductVariant> builder)
    {
        builder.Property(v => v.Stock)
            .IsRequired();

        builder.Property(v => v.PriceOverride)
            .HasColumnType("decimal(18,2)");

        builder.HasOne(v => v.Product)
            .WithMany(p => p.Variants)
            .HasForeignKey(v => v.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(v => v.Color)
            .WithMany(c => c.ProductVariants)
            .HasForeignKey(v => v.ColorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(v => v.Size)
            .WithMany(s => s.ProductVariants)
            .HasForeignKey(v => v.SizeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(v => new { v.ProductId, v.ColorId, v.SizeId }).IsUnique(); // уникальная комбинация
    }
}