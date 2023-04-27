using LydFramework.Application.Dtos;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace LydFramework.Application.Middlewares
{
    public class StatusMiddleware
    {
        private readonly RequestDelegate next;
        public StatusMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        /// <summary>
        /// 参考：https://www.yuque.com/shifeng-wl7di/svid8i/ow0k08#mYGhv
        /// </summary>
        public async Task InvokeAsync(HttpContext context)
        {
            var originalResponseStream = context.Response.Body;
            var ms = new MemoryStream();
            context.Response.Body = ms;
            await next.Invoke(context);
            using var responseReader = new StreamReader(ms);
            ms.Position = 0;
            var responseContent = await responseReader.ReadToEndAsync();


            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            #region 设置响应码，各种asp.net内置的响应码统一为200,统一响应格式
            switch (context.Response.StatusCode)
            {
                case 200:break;
                case 401:
                    responseContent = JsonConvert.SerializeObject(new ResultDto(401, "没有登录"),settings);
                    break;
                case 403:
                    responseContent = JsonConvert.SerializeObject(new ResultDto(403, "没有权限"), settings);
                    break;
                case 400:
                    var result = new ResultDto(400, "请求数据错误", JsonConvert.DeserializeObject(responseContent));
                    responseContent = JsonConvert.SerializeObject(result,settings);
                    break;
                case 405:
                    responseContent = JsonConvert.SerializeObject(new ResultDto(405, "请求方法错误"), settings);
                    break;
                default: 
                    responseContent = JsonConvert.SerializeObject(new ResultDto(context.Response.StatusCode, "详见状态码"), settings);
                    break;
            }
            context.Response.StatusCode = 200;
            #endregion

            #region 写入响应流
            ms.Position = 0;
            await context.Response.WriteAsync(responseContent);
            ms.Position = 0;
            await ms.CopyToAsync(originalResponseStream);
            context.Response.Body = originalResponseStream;
            #endregion
        }
    }
}
