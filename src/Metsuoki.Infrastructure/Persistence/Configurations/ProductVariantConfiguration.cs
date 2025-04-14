using Metsuoki.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Metsuoki.Infrastructure.Persistence.Configurations;
public class ProductVariantConfiguration : IEntityTypeConfiguration<ProductVariant>
{
    public void Configure(EntityTypeBuilder<ProductVariant> builder)
    {
        builder.Property(v => v.Color).IsRequired().HasMaxLength(50);
        builder.Property(v => v.Size).IsRequired().HasMaxLength(20);

        builder.Property(v => v.Stock).IsRequired();

        builder.Property(v => v.PriceOverride).HasColumnType("decimal(18,2)");

        builder.HasOne(v => v.Product)
            .WithMany(p => p.Variants)
            .HasForeignKey(v => v.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(v => new { v.ProductId, v.Color, v.Size }).IsUnique(); // уникальность комбинации
    }
}