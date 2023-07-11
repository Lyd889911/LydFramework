using LydFramework.Application.Middlewares;
using LydFramework.WebApi;
using LydFramework.WebApi.Middlewares;

#region 系统配置
Directory.SetCurrentDirectory(AppContext.BaseDirectory);
WebApplicationOptions options = new()
{
    ContentRootPath = AppContext.BaseDirectory,
    Args = args
};
var builder = WebApplication.CreateBuilder(options);
//添加日志
builder.Host.UseLydSerilog(builder.Configuration);
//不是Windows系统不会执行的
await builder.Host.InstallWindowServer(builder.Configuration);
builder.WebHost.UseUrls(builder.Configuration["Application:RunUrl"]);
#endregion

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddModuleApplication<ApiModule>();

var app = builder.Build();

app.UseMiddleware<RequestSerilogMiddleware>();

app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<StatusMiddleware>();
app.UseUnitOfWorkMiddleware(builder.Configuration);
app.UseUnitOfWorkDapperMiddleware(builder.Configuration);
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
