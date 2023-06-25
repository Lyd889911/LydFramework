using LydFramework.Application.Middlewares;
using LydFramework.Domain;
using LydFramework.EFCore.DbContexts;
using LydFramework.WebApi;
using LydFramework.WebApi.Middlewares;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls(builder.Configuration["RunUrl"]);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddModuleApplication<WebApiModule>();

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
