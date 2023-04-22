

namespace LydFramework.EFCore.MySql.EntityConfigs
{
    internal class RoleConfig : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            //builder.HasMany(x => x.RoleMenus).WithOne(x => x.Role);
        }
    }
}
