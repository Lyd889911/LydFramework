using LydFramework.Common.Attributes;
using LydFramework.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public class SqlSugarUnitOfWorkMiddleware
    {
        private readonly RequestDelegate _next;
        public SqlSugarUnitOfWorkMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var metadata = context.Features.Get<IEndpointFeature>()?.Endpoint?.Metadata;
            if (metadata == null)
                throw new BusinessException("路由错误");
            var unit = metadata.GetMetadata<UnitOfWorkAttribute>();

            var _sqlSugarClient = context.RequestServices.GetRequiredService<ISqlSugarClient>();
            if(_sqlSugarClient==null)
                throw new BusinessException("ISqlSugarClient未注册");

            if (unit != null)
            {
                await Console.Out.WriteLineAsync("Sugar事务启动");
                _sqlSugarClient.AsTenant().BeginTran();
                try
                {
                    await _next.Invoke(context);
                    _sqlSugarClient.AsTenant().CommitTran();
                }
                catch (Exception ex)
                {
                    await Console.Out.WriteLineAsync("Sugar事务回滚");
                    _sqlSugarClient.AsTenant().RollbackTran();
                    throw ex;
                }
            }
            else
            {
                await Console.Out.WriteLineAsync("不启动Sugar事务");
                await _next.Invoke(context);
            }
        }
    }
}
