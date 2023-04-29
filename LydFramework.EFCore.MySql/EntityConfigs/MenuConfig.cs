

namespace LydFramework.EFCore.MySql.EntityConfigs
{
    public class MenuConfig : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            //builder.HasMany(x => x.RoleMenus).WithOne(x => x.Menu);
            //builder.HasKey(x => x.Id);
            //builder.Property(x => x.Id).ValueGeneratedNever();
        }
    }
}
