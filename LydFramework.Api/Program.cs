using LydFramework.Application.Middlewares;
using LydFramework.WebApi;
using LydFramework.WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls(builder.Configuration["RunUrl"]);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddModuleApplication<ApiModule>();

var app = builder.Build();

// Configure the HTTP request pipeline.
//app.UseMiddleware<RateLimitMiddleware>();
app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<StatusMiddleware>();
app.UseUnitOfWorkMiddleware();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
