

namespace LydFramework.EFCore.MySql.EntityConfigs
{
    public class UserRoleConfig : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            //builder.HasKey(x => x.Id);
            //builder.Property(x => x.Id).ValueGeneratedNever();
            builder.HasOne(x => x.User).WithMany(x => x.UserRoles);
            builder.HasOne(x => x.Role).WithMany(x => x.UserRoles);
        }
    }
}
