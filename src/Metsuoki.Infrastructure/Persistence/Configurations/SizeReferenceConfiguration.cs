using Metsuoki.Domain.Entities.Products.ProductVariants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Metsuoki.Infrastructure.Persistence.Configurations;

public class SizeReferenceConfiguration : IEntityTypeConfiguration<SizeReference>
{
    public void Configure(EntityTypeBuilder<SizeReference> builder)
    {
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(x => x.Description)
            .HasMaxLength(100);

        builder.HasMany(x => x.ProductVariants)
            .WithOne(pv => pv.Size)
            .HasForeignKey(pv => pv.SizeId);
    }
}