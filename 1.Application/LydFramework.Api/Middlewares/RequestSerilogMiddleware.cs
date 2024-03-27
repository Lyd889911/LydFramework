using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Components;
using Serilog;
using LydFramework.Common.Utils;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 请求日志中间件
    /// </summary>
    public class RequestSerilogMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestSerilogMiddleware> _logger;
        public RequestSerilogMiddleware(RequestDelegate next, ILogger<RequestSerilogMiddleware> logger)
        {
            this._next = next;
            this._logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            string method = context.Request.Method;
            string path = context.Request.Path.Value;
            string query = GetRequestQuery(context);
            string ip = context.Connection.RemoteIpAddress.ToString();
            string body = await GetRequestBody(context);
            string form = GetRequestForm(context);
            string route = GetRequestRoute(context);

            await _next.Invoke(context);

            stopwatch.Stop();

            StringBuilder sb = new StringBuilder($"{method} {path} {ip} Route:{route} ");
            if (!string.IsNullOrEmpty(query))
                sb.Append($"Query:{query} ");
            if (!string.IsNullOrEmpty(body))
                sb.Append($"Body:{body} ");
            if (!string.IsNullOrEmpty(form))
                sb.Append($"Form:{form}");
            sb.Append($" Time:{stopwatch.ElapsedMilliseconds}");
            //Log.Information(sb.ToString());
            LogUtils.TenantLogger("tenantid").Information(sb.ToString());

        }
        private async Task<string> GetRequestBody(HttpContext context)
        {
            if (!string.IsNullOrEmpty(context.Request.ContentType) && context.Request.ContentType.Contains("application/json"))
            {
                //读取请求体
                context.Request.EnableBuffering();
                var request = context.Request;
                request.Body.Position = 0;
                StreamReader sr = new StreamReader(request.Body);
                string body = await sr.ReadToEndAsync();
                request.Body.Position = 0;
                return body.Replace("\r", "").Replace("\n", "");
            }
            else
                return "";

        }
        private string GetRequestForm(HttpContext context)
        {
            if (!string.IsNullOrEmpty(context.Request.ContentType) && context.Request.ContentType.Contains("multipart/form-data"))
            {
                var form = context.Request.Form;
                Dictionary<string, string> kv = new Dictionary<string, string>();
                foreach (var key in form.Keys)
                {
                    kv.Add(key, form[key]);
                }
                string json = JsonConvert.SerializeObject(kv);
                return json;
            }
            else
                return "";

        }
        private string GetRequestRoute(HttpContext context)
        {
            string json = JsonConvert.SerializeObject(context.Request.RouteValues);
            return json;
        }
        private string GetRequestQuery(HttpContext context)
        {
            var query = context.Request.Query;
            Dictionary<string, string> kv = new Dictionary<string, string>();
            foreach (var key in query.Keys)
            {
                kv.Add(key, query[key]);
            }
            string json = JsonConvert.SerializeObject(kv);
            return json;
        }
    }
}
