using LydFramework.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace LydFramework.EFCore.MySql.EntityConfigs
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Status).HasConversion<string>();
            builder.HasOne(x => x.UserAccessFail).WithOne(x => x.User);
            //builder.HasMany(x => x.UserRoles).WithOne(x => x.User);
        }
    }
}
