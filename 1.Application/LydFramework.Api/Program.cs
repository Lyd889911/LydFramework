
using LydFramework.Common.Utils;
using LydFramework.Common.Expansions;
using LydFramework.Api.Expansions;
using Hangfire;


#region 系统配置
Directory.SetCurrentDirectory(AppContext.BaseDirectory);
WebApplicationOptions options = new()
{
    ContentRootPath = AppContext.BaseDirectory,
    Args = args
};
var builder = WebApplication.CreateBuilder(options);
AppUtils.Init(builder.Services);

//添加日志
builder.Host.UseLydSerilog();
//不是Windows系统不会执行的
await builder.Host.InstallWindowServer(builder.Configuration);
builder.WebHost.UseUrls(builder.Configuration["Application:RunUrl"]);
#endregion

builder.Services.AddLydFrameworkService();

var app = builder.Build();
app.UseLydFrameworkAppcation();

