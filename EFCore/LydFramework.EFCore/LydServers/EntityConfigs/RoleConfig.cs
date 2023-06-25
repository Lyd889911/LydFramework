namespace LydFramework.EFCore.LydServers.EntityConfigs
{
    internal class RoleConfig : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            //builder.HasMany(x => x.RoleMenus).WithOne(x => x.Role);
            //builder.HasKey(x => x.Id);
            //builder.Property(x => x.Id).ValueGeneratedNever();
        }
    }
}
