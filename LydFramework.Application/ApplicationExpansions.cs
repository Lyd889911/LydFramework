using LydFramework.Application.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ApplicationExpansions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.Configure<MvcOptions>(x =>
            {
                x.Filters.Add<UnitOfWorkFilter>();
            });

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
