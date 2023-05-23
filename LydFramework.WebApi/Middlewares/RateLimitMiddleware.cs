using LydFramework.Application.Contracts.Dtos;
using LydFramework.WebApi.Middlewares.Options;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace LydFramework.WebApi.Middlewares
{
    public class RateLimitMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IMemoryCache _memory;
        public RateLimitMiddleware(RequestDelegate next, IMemoryCache memory)
        {
            this.next = next;
            this._memory = memory;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            //读取响应体
            var originalResponseStream = context.Response.Body;
            using var ms = new MemoryStream();
            context.Response.Body = ms;
            string responseContent = null;

            string path = context.Request.Path.Value;
            string method = context.Request.Method;
            string ip = context.Connection.RemoteIpAddress.ToString();
            string id = context.Connection.Id;

            //获取或者设置缓存
            string key = $"{ip}:{id}:{method}:{path}";

            #region 缓存限流存储
            RateLimitOption rlo = null;
            bool exist = _memory.TryGetValue<RateLimitOption>(key, out rlo);
            if (exist)
                rlo.Counter++;
            else
            {
                rlo = new RateLimitOption();
                rlo.Counter = 1;
                rlo.Second = 2;
                rlo.StartTime = DateTime.Now;
                rlo.LimitCount = 1;
            }
            _memory.Set(key, rlo,rlo.Expiration);
            #endregion

            if (rlo.IsLimit)//返回限制
            {
                var settings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };
                var result = new ResultDto(429, "访问频繁", null);
                responseContent = JsonConvert.SerializeObject(result, settings);
                //需要写入到流中
                byte[] bytes = System.Text.Encoding.Default.GetBytes(responseContent);
                ms.Write(bytes, 0, bytes.Length);
            }
            else
            {
                await next.Invoke(context);
            }

            #region 还原流
            ms.Position = 0;
            await ms.CopyToAsync(originalResponseStream);
            context.Response.Body = originalResponseStream;
            #endregion
        }
    }
}
