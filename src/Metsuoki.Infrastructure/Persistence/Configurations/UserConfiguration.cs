using Metsuoki.Domain.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Metsuoki.Infrastructure.Persistence.Configurations;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.FirstName).IsRequired().HasMaxLength(100);
        builder.Property(u => u.LastName).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Patronymic).HasMaxLength(100);
        builder.Property(u => u.ShippingAddress).HasMaxLength(500);
        builder.Property(u => u.ContactInfo).HasMaxLength(500);
    }
}