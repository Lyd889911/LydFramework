using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.EFCore.Custom.DbContexts
{
    public class CustomDbContext:DbContext
    {
        public CustomDbContext(DbContextOptions<CustomDbContext> options) : base(options)
        {

        }
        public CustomDbContext()
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //从当前程序集加载数据库字段配置
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
