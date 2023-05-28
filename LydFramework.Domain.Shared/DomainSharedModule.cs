using LydFramework.Module;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Domain.Shared
{
    public class DomainSharedModule : LydModule
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IdentityProvider>();
        }

    }
}
