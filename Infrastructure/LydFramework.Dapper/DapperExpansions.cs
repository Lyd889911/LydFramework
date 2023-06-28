using LydFramework.Dapper.UnitOfWorks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DapperExpansions
    {
        public static void UseUnitOfWorkDapperMiddleware(this IApplicationBuilder app, IConfiguration configuration)
        {
            bool isEnabledUnitOfWork = Convert.ToBoolean(configuration["Dapper:IsEnabledAutoUnitOfWork"]);
            if(isEnabledUnitOfWork)
            {
                app.UseMiddleware<DapperUnitOfWorkMiddleware>();
            }
        }
    }
}
