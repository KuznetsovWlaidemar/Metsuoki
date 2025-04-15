using Metsuoki.Domain.Entities.Products.ProductVariants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Metsuoki.Infrastructure.Persistence.Configurations;

public class ColorReferenceConfiguration : IEntityTypeConfiguration<ColorReference>
{
    public void Configure(EntityTypeBuilder<ColorReference> builder)
    {
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.HexCode)
            .HasMaxLength(7);

        builder.HasMany(x => x.ProductVariants)
            .WithOne(pv => pv.Color)
            .HasForeignKey(pv => pv.ColorId);
    }
}
