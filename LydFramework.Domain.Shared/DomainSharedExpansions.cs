using LydFramework.Domain.Shared;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DomainSharedExpansions
    {
        public static IServiceCollection AddDomainShared(this IServiceCollection services)
        {
            services.AddScoped<IdentityProvider>();
            return services;
        }
    }
}
