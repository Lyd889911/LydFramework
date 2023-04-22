

namespace LydFramework.EFCore.MySql.EntityConfigs
{
    public class RoleMenuConfig : IEntityTypeConfiguration<RoleMenu>
    {
        public void Configure(EntityTypeBuilder<RoleMenu> builder)
        {
            builder.HasOne(x => x.Role).WithMany(x => x.RoleMenus);
            builder.HasOne(x => x.Menu).WithMany(x => x.RoleMenus);
        }
    }
}
