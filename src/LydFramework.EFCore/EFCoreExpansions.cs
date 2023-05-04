using LydFramework.EFCore.DbContexts;
using LydFramework.EFCore.Repositorys;
using Microsoft.Extensions.Configuration;


namespace Microsoft.Extensions.DependencyInjection
{
    public static class EFCoreExpansions
    {
        public static IServiceCollection AddEFCoreMySql(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<LydDbContext>(opt =>
            {
                //string connStr = "Server=localhost;User ID=root;Password=123456;DataBase=tf;";
                opt.UseMySql(configuration["DbConnection"], new MySqlServerVersion(configuration["DbVersion"]));
            });
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}
