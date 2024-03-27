using LydFramework.Application.DtoParams;
using LydFramework.Common.Attributes;
using LydFramework.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SqlSugar.Extensions;
using System.Drawing.Imaging;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LydFramework.Api.Middlewares
{
    public class ApiResultWrapperMiddleware
    {
        private readonly RequestDelegate _next;
        public ApiResultWrapperMiddleware(RequestDelegate next)
        {
            this._next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            string json = "";
            try
            {
                await _next.Invoke(context);

                var noWrapper = context.Features.Get<IEndpointFeature>()?.Endpoint?.Metadata?.GetMetadata<NonApiResultWrapperAttribute>();
                if (noWrapper != null)
                    return;

                //只针对application/json结果进行处理
                if (!context.Response.ContentType.ToLower().Contains("application/json"))
                    return;

                //获取Action的返回类型
                var returnType = context.GetEndpoint()?.Metadata.GetMetadata<ControllerActionDescriptor>()?.MethodInfo.ReturnType;
                if (returnType == null)
                    return;

                //如果终结点已经是ResultDto则不进行包装处理
                if ((returnType.IsGenericType && returnType.GetGenericTypeDefinition() == typeof(ResultDto<>)) || returnType == typeof(ResultDto))
                    return;

                //泛型的特殊处理
                if (returnType.IsGenericType && (returnType.GetGenericTypeDefinition() == typeof(Task<>) || returnType.GetGenericTypeDefinition() == typeof(ValueTask<>)))
                {
                    returnType = returnType.GetGenericArguments()[0];
                }

                context.Response.Body.Seek(0, SeekOrigin.Begin);
                json = Encoding.UTF8.GetString(((MemoryStream)(context.Response.Body)).ToArray());
                var result = JsonConvert.DeserializeObject(json, returnType);
                json = JsonConvert.SerializeObject(new ResultDto(context.Response.StatusCode, GetHttpStatusDescription(context.Response.StatusCode), result));
            }
            catch (Exception ex)
            {
                json = JsonConvert.SerializeObject(new ResultDto(500, GetHttpStatusDescription(500), ex.Message));
            }
            finally
            {
                //清除响应体的主体内容，再写入新的（不清除的话，比如原本有500b，新写入有100b，100-500部分是旧的，0-100是新的）
                context.Response.Body.Seek(0, SeekOrigin.Begin);
                context.Response.Body.SetLength(0);
                await context.Response.WriteAsync(json);
            }
        }
        public string GetHttpStatusDescription(int statusCode)
        {
            switch (statusCode)
            {
                case 100: return "Continue|继续";
                case 101: return "Switching Protocols|切换协议";
                case 200: return "OK|成功";
                case 201: return "Created|已创建";
                case 202: return "Accepted|已接受";
                case 203: return "Non-Authoritative Information|非授权信息";
                case 204: return "No Content|无内容";
                case 205: return "Reset Content|重置内容";
                case 206: return "Partial Content|部分内容";
                case 300: return "Multiple Choices|多种选择";
                case 301: return "Moved Permanently|永久移动";
                case 302: return "Found|找到";
                case 303: return "See Other|参见其他";
                case 304: return "Not Modified|未修改";
                case 305: return "Use Proxy|使用代理";
                case 307: return "Temporary Redirect|临时重定向";
                case 400: return "Bad Request|错误请求";
                case 401: return "Unauthorized|未授权";
                case 402: return "Payment Required|需要付款";
                case 403: return "Forbidden|禁止";
                case 404: return "Not Found|未找到";
                case 405: return "Method Not Allowed|不允许使用的方法";
                case 406: return "Not Acceptable|不可接受";
                case 407: return "Proxy Authentication Required|要求代理身份验证";
                case 408: return "Request Timeout|请求超时";
                case 409: return "Conflict|冲突";
                case 410: return "Gone|已删除";
                case 411: return "Length Required|需要有效长度";
                case 412: return "Precondition Failed|前提条件失败";
                case 413: return "Payload Too Large|请求实体过大";
                case 414: return "URI Too Long|请求的 URI 过长";
                case 415: return "Unsupported Media Type|不支持的媒体类型";
                case 416: return "Range Not Satisfiable|范围无法满足";
                case 417: return "Expectation Failed|预期失败";
                case 426: return "Upgrade Required|需要升级";
                case 500: return "Internal Server Error|服务器内部错误";
                case 501: return "Not Implemented|尚未实施";
                case 502: return "Bad Gateway|错误的网关";
                case 503: return "Service Unavailable|服务不可用";
                case 504: return "Gateway Timeout|网关超时";
                case 505: return "HTTP Version Not Supported|不支持的 HTTP 版本";
                default: return "Unknown|未知";
            }
        }
    }
}
