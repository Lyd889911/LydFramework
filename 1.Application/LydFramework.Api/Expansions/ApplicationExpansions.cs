using LydFramework.Api.Middlewares;
using LydFramework.Common.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;

namespace LydFramework.Api.Expansions
{
    public static class ApplicationExpansions
    {
        public static void UseLydFrameworkAppcation(this WebApplication app)
        {

            app.UseLydSwagger();//使用swgger
            app.UseLydongHangfire();//定时任务
            app.UseCors(); //跨域        
            app.UseStaticFiles();//静态文件
            app.UseMiddleware<ResponseStreamMiddleware>();
            app.UseMiddleware<ApiResultWrapperMiddleware>();
            app.UseMiddleware<RequestSerilogMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<SqlSugarUnitOfWorkMiddleware>();
            app.MapControllers();
            app.Run();
        }
    }
}
