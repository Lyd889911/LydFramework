using LydFramework.Domain.InfrastructureContracts;
using LydFramework.Module;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Dapper
{
    public class DapperModule:LydModule
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ISqlProvider, DapperClient>();
        }
    }
}
