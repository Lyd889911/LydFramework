using LydFramework.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Dapper.UnitOfWorks
{
    public class DapperUnitOfWorkMiddleware
    {
        private readonly RequestDelegate _next;
        public DapperUnitOfWorkMiddleware(RequestDelegate next)
        {
            this._next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var metadata = context.Features.Get<IEndpointFeature>()?.Endpoint?.Metadata;
            var unit = metadata.GetMetadata<DapperUnitOfWorkAttribute>();

            #region 不需要工作单元
            if (unit == null)
            {
                await Console.Out.WriteLineAsync("不需要Dapper工作单元");
                await _next.Invoke(context);
            }
            #endregion

            #region 需要工作单元
            else
            {
                await Console.Out.WriteLineAsync("需要Dapper工作单元");
                var sqlClient = context.RequestServices.GetRequiredService<IDapperClient>();
                sqlClient.BeginTransaction();
                try
                {
                    await _next.Invoke(context);
                    sqlClient.CommitTransaction();
                }
                catch (Exception ex)
                {
                    sqlClient.RollbackTransaction();
                    throw ex;
                }
            }
            #endregion
        }
    }
}
