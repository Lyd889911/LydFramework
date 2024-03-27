using LydFramework.Common.BaseEntity;
using LydFramework.Common.Utils;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.DbMigration
{
    internal static class Migrator
    {
        public static void CodeFirst(IServiceCollection services)
        {
            Init(services);

            var db = services.BuildServiceProvider().GetRequiredService<ISqlSugarClient>();
            db.DbMaintenance.CreateDatabase();

            Type[] types = Assembly.Load("LydFramework.Domain").GetTypes();
            types = types.Where(t => !t.IsAbstract && !t.IsInterface && t.IsAssignableTo(typeof(IEntity))).ToArray();

            //TODO 批量建立表
            db.CodeFirst.InitTables(types);
            Console.WriteLine("初始化表完成");
        }
        public static void DbFirst(IServiceCollection services)
        {
            Init(services);

            var db = services.BuildServiceProvider().GetRequiredService<ISqlSugarClient>();
            string basePath = Environment.CurrentDirectory.Split("2.Common")[0];
            string entitiesPath = Path.Combine(basePath, "1.Application", "LydFramework.Domain", "Entities");
            db.DbFirst.IsCreateAttribute().StringNullable().CreateClassFile(entitiesPath, "LydFramework.Domain.Entities");
        }
        private static void Init(IServiceCollection services)
        {
            IConfigurationBuilder cb = new ConfigurationBuilder();
            string basePath = Environment.CurrentDirectory.Split("2.Common")[0];
            string appsettingsPath = Path.Combine(basePath, "1.Application", "LydFramework.Api", "appsettings.json");
            cb.AddJsonFile(path: appsettingsPath, optional: false, reloadOnChange: true);
            IConfiguration config = cb.Build();
            services = services.Replace(ServiceDescriptor.Singleton(config));
            AppUtils.Init(services);
            services.AddLydSqlSugar();
        }
    }
}
