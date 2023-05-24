using EntityFrameworkCore.Core;
using LydFramework.EFCore.Cores;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.EFCore.Middlewares
{
    public class UnitOfWorkMiddleware
    {
        private readonly RequestDelegate _next;
        public UnitOfWorkMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var routeData = context.GetRouteData();

            var controllerDuow = routeData.Values["controller"].GetType().GetTypeInfo().GetCustomAttribute<DisabledUnitOfWorkAttribute>();
            var actionDuow = routeData.Values["action"].GetType().GetTypeInfo().GetCustomAttribute<DisabledUnitOfWorkAttribute>();

            #region 不需要工作单元
            if(controllerDuow!=null||actionDuow!=null)
            {
                await _next.Invoke(context);
            }
            #endregion
            #region 需要工作单元
            else
            {
                //获取服务中多个DbContext
                var unitOfWorks = context.RequestServices.GetServices<IUnitOfWork>();
                foreach (var unitOfWork in unitOfWorks)
                {
                    // 开启事务
                    await unitOfWork.BeginTransactionAsync();
                }
                try
                {
                    await _next.Invoke(context);

                    foreach (var unitOfWork in unitOfWorks)
                    {
                        // 提交事务
                        await unitOfWork.CommitTransactionAsync();
                    }
                }
                catch (Exception)
                {
                    foreach (var unitOfWork in unitOfWorks)
                    {
                        await unitOfWork.RollbackTransactionAsync();
                    }
                }
            }
            #endregion
        }
    }
}
