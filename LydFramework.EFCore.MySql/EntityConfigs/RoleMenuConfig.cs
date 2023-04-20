using LydFramework.Domain.Roles;
using LydFramework.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
