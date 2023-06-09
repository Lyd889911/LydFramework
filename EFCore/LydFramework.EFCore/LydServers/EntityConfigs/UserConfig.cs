﻿

namespace LydFramework.EFCore.LydServers.EntityConfigs
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            //builder.HasKey(x => x.Id);
            //builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Status).HasConversion<string>();
            builder.HasOne(x => x.UserAccessFail).WithOne(x => x.User);
            //builder.HasMany(x => x.UserRoles).WithOne(x => x.User);
        }
    }
}
