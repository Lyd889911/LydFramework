using Microsoft.OpenApi.Models;

namespace LydFramework.Api.Expansions
{
    public static class SwaggerExpansions
    {
        public static void AddLydSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var apiInfo = new OpenApiInfo { Title = "LydFramework", Version = "v1", Description = "LydFramework接口文档" };
                options.SwaggerDoc("LydFramework", apiInfo);
                options.IncludeXmlComments("LydFramework.Api.xml");
            });
        }
        public static void UseLydSwagger(this IApplicationBuilder app)
        {
            //启用中间件服务生成Swagger作为JSON终结点
            app.UseSwagger();
            //启用中间件服务对swagger-ui，指定Swagger JSON终结点
            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/LydFramework/swagger.json", "LydFramework");
            //});
        }
    }
}
