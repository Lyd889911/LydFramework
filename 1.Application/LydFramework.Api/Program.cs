
using LydFramework.Common.Utils;
using LydFramework.Common.Expansions;
using LydFramework.Api.Expansions;
using Hangfire;


#region ϵͳ����
Directory.SetCurrentDirectory(AppContext.BaseDirectory);
WebApplicationOptions options = new()
{
    ContentRootPath = AppContext.BaseDirectory,
    Args = args
};
var builder = WebApplication.CreateBuilder(options);
AppUtils.Init(builder.Services);

//�����־
builder.Host.UseLydSerilog();
//����Windowsϵͳ����ִ�е�
await builder.Host.InstallWindowServer(builder.Configuration);
builder.WebHost.UseUrls(builder.Configuration["Application:RunUrl"]);
#endregion

builder.Services.AddLydFrameworkService();

var app = builder.Build();
app.UseLydFrameworkAppcation();

