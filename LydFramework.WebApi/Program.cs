using LydFramework.Application.Middlewares;
using LydFramework.EFCore.DbContexts;
using LydFramework.WebApi.Middlewares;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
#region 添加各个板块的服务
builder.Services.AddDomainShared();
builder.Services.AddEFCoreCustom<AuthDbContext>(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddMQ(builder.Configuration);
builder.Services.AddWebApi(builder.Configuration);
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<RateLimitMiddleware>();
app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<StatusMiddleware>();
app.UseUnitOfWorkMiddleware();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
