using LydFramework.Domain.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using Newtonsoft.Json;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;

namespace LydFramework.Application.Filters
{
    public class IdentityUserFilter : IAsyncActionFilter
    {
        private readonly Domain.Shared.IdentityUser<Guid> _identityUser;
        public IdentityUserFilter(Domain.Shared.IdentityUser<Guid> identityUser)
        {
            _identityUser = identityUser;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var controllerType = context.Controller.GetType();
            var actionDesc = context.ActionDescriptor as ControllerActionDescriptor;
            var controllerAuthorize = controllerType.GetCustomAttribute<AuthorizeAttribute>();
            var actionAuthorize = actionDesc.MethodInfo.GetCustomAttribute<AuthorizeAttribute>();

            //控制器和方法上都没有Authorize特性就是不需要认证
            if (controllerAuthorize == null && actionAuthorize==null)
            {
                await next();
                await Console.Out.WriteLineAsync("不认证");
            }

            else
            {
                await Console.Out.WriteLineAsync("认证");
                var controllerBase = context.Controller as ControllerBase;
                var user = controllerBase.User;

                //获取登录用户id
                Claim? Id = user.FindFirst(ClaimTypes.NameIdentifier);
                _identityUser.Id.Value = Id == null ? null : new Guid(Id.Value);

                //获取登录用户角色
                var roles = user.FindAll(ClaimTypes.Role);
                List<string> Roles = new List<string>();
                foreach (var role in roles)
                {
                    Roles.Add(role.Value);
                }
                _identityUser.Roles.Value = Roles;

                //获取登录用户名
                Claim? Name = user.FindFirst(ClaimTypes.Name);
                _identityUser.Name.Value = Name == null ? null : Name.Value;


                await next();
            }
        }
    }
}
