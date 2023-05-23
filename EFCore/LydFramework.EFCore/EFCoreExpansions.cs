using LydFramework.EFCore.DbContexts;
using LydFramework.EFCore.Repositorys;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class EFCoreExpansions
    {
        public static IServiceCollection AddEFCore(this IServiceCollection services,IConfiguration configuration)
        {


            #region 批量注册Repository
            var sp = services.BuildServiceProvider();
            ILoggerFactory factory = sp.GetRequiredService<ILoggerFactory>();
            ILogger logger = factory.CreateLogger("RegisterEFCore");
            StringBuilder sb = new StringBuilder();
            Type[] types = Assembly.GetExecutingAssembly().GetTypes();
            foreach (Type type in types)
            {
                string typeName = type.Name;
                if (typeName.EndsWith("Repository"))
                {
                    Type? interf = type.GetInterface($"I{typeName}");
                    if (interf != null)
                    {
                        services.AddScoped(interf, type);
                        sb.AppendLine($"{interf.Name}:{type.Name}");
                    }
                }
            }
            logger.LogInformation(sb.ToString());
            #endregion

            return services;
        }
    }
}
