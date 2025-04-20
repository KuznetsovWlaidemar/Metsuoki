using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Metsuoki.Infrastructure.Persistence.Configurations;

//public class UserActionLogConfiguration : IEntityTypeConfiguration<UserActionLog>
//{
//    public void Configure(EntityTypeBuilder<UserActionLog> builder)
//    {
//        builder.Property(x => x.Timestamp).IsRequired();
//        builder.Property(x => x.UserId).IsRequired();
//        builder.Property(x => x.UserFullName).IsRequired().HasMaxLength(200);
//        builder.Property(x => x.ActionType).IsRequired();
//        builder.Property(x => x.Description).HasMaxLength(500);

//        builder.HasIndex(x => x.UserId);
//        builder.HasIndex(x => x.ActionType);
//        builder.HasIndex(x => x.Timestamp);
//    }
//}