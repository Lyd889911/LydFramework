using LydFramework.Domain.Shared.Attributes;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LydFramework.Application.Filters
{
    /// <summary>
    /// 工作单元过滤器
    /// </summary>
    public class UnitOfWorkFilter: IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var result = await next();
            if (result.Exception != null)//只有Action执行成功，才自动调用SaveChanges
                return;
            var actionDesc = context.ActionDescriptor as ControllerActionDescriptor;
            if (actionDesc == null)
                return;
            var unit = actionDesc.MethodInfo.GetCustomAttribute<UnitOfWorkAttribute>();
            if (unit == null)
                return;
            foreach (var dbctxType in unit.DbContextTypes)
            {
                var dbctx = context.HttpContext.RequestServices.GetService(dbctxType) as DbContext;
                if (dbctx != null)
                    await dbctx.SaveChangesAsync();
            }
        }
    }
}
