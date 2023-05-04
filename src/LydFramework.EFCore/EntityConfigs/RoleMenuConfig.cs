

namespace LydFramework.EFCore.EntityConfigs
{
    public class RoleMenuConfig : IEntityTypeConfiguration<RoleMenu>
    {
        public void Configure(EntityTypeBuilder<RoleMenu> builder)
        {
            //builder.HasKey(x => x.Id);
            //builder.Property(x => x.Id).ValueGeneratedNever();
            builder.HasOne(x => x.Role).WithMany(x => x.RoleMenus);
            builder.HasOne(x => x.Menu).WithMany(x => x.RoleMenus);
        }
    }
}
