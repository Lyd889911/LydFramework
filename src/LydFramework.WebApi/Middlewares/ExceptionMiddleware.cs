using LydFramework.Application.Contracts.Dtos;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace LydFramework.Application.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //读取响应体
            var originalResponseStream = context.Response.Body;
            using var ms = new MemoryStream();
            context.Response.Body = ms;
            string responseContent = null;
            StreamReader responseReader = null;//手动释放

            try
            {
                //这里只捕获异常，响应的内容应该在后面的中间件就写入了流中
                await next.Invoke(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                var settings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var result = new ResultDto(500,"异常",ex);
                responseContent = JsonConvert.SerializeObject(result, settings);

                //需要写入到流中
                byte[] bytes = System.Text.Encoding.Default.GetBytes(responseContent);
                ms.Write(bytes, 0, bytes.Length);
            }

            #region 还原流
            ms.Position = 0;
            await ms.CopyToAsync(originalResponseStream);
            context.Response.Body = originalResponseStream;
            #endregion
            if(responseReader != null)
                responseReader.Dispose();

        }
    }
}
