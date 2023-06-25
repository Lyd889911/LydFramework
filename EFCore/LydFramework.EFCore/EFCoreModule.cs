using LydFramework.Domain.Shared;
using LydFramework.EFCore.DbContexts;
using LydFramework.Module;
using LydFramework.Module.Attributes;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.EFCore
{
    [DependOn(typeof(DomainSharedModule))]
    public class EFCoreModule:LydModule
    {

        public override void ConfigureServices(IServiceCollection services)
        {
            var sp = services.BuildServiceProvider();
            var config = sp.GetRequiredService<IConfiguration>();
            var dbType = config["EFCore:DbType"].ToLower().Replace(" ", "");
            var dbConnection = config["EFCore:DbConnection"];
            var dbVersion = config["EFCore:DbVersion"];

            services.AddEFCore<AuthDbContext>(builder => ConnectionBuilder(builder, dbConnection, dbVersion, dbType));
        }
        /// <summary>
        /// 构建数据库连接
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="dbConnection"></param>
        /// <param name="dbVersion"></param>
        /// <param name="dbType"></param>
        private void ConnectionBuilder(DbContextOptionsBuilder builder, string dbConnection, string dbVersion, string dbType)
        {
            if (dbType == "sqlserver")
            {
                if (dbVersion == "2008" || dbVersion == "2005")
                {
                    builder.ReplaceService<IQueryTranslationPostprocessorFactory, SqlServer2008QueryTranslationPostprocessorFactory>();
                }
                builder.UseSqlServer(dbConnection, x => x.MigrationsAssembly("CatalogServer.EFCore"));
            }
            else if (dbType == "mysql")
            {
                builder.UseMySql(dbConnection, new MySqlServerVersion(dbVersion), x => x.MigrationsAssembly("CatalogServer.EFCore"));
            }
        }
    }
}
