using LydFramework.Common.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Reflection;

namespace LydFramework.Api.JwtPolicy
{
    public class ApisRequirement : AuthorizationHandler<ApisRequirement>, IAuthorizationRequirement
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ApisRequirement requirement)
        {
            string? apiJson = context.User?.Claims?.FirstOrDefault(c => c.Type == "Apis")?.Value;
            if (string.IsNullOrEmpty(apiJson))
            {
                context.Fail();
                return Task.CompletedTask;
            }


            var apis = JsonConvert.DeserializeObject<HashSet<string>>(apiJson);
            if(apis==null)
                context.Fail();

            //获取目标接口的路由
            HttpContext httpContext = (context.Resource as HttpContext)!;

            var metadata = httpContext.Features.Get<IEndpointFeature>()?.Endpoint?.Metadata;
            if (metadata == null)
                throw new BusinessException("路由错误");
            string routeString = "";
            string methodString = "";
            var controllerActionDescriptor = metadata.GetMetadata<ControllerActionDescriptor>();
            var controllerType = controllerActionDescriptor!.ControllerTypeInfo;
            var actionType = controllerActionDescriptor!.MethodInfo;

            var controllerRouteAttribute = controllerType.GetCustomAttribute<RouteAttribute>();
            if (controllerRouteAttribute != null)
                routeString = "/"+controllerRouteAttribute.Template;

            routeString = routeString.Replace("[controller]", controllerActionDescriptor.ControllerName).Replace("[action]", actionType.Name);

            var httpAttribute = actionType.GetCustomAttribute<HttpMethodAttribute>();
            if (httpAttribute != null)
                routeString += string.IsNullOrEmpty(httpAttribute.Template)?"":"/" + httpAttribute.Template;

            routeString = routeString.ToLower();
            methodString = metadata.GetMetadata<HttpMethodAttribute>()?.HttpMethods.FirstOrDefault();


            string api = $"{methodString} {routeString}";
            if (apis.Contains(api))
                context.Succeed(requirement);
            else
                context.Fail();

            return Task.CompletedTask;
        }
    }
}
