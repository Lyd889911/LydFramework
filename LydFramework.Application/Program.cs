using LydFramework.Application.Middlewares;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
#region 添加各个板块的服务
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddEFCoreMySql(builder.Configuration);
builder.Services.AddDomainShared();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<StatusMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
