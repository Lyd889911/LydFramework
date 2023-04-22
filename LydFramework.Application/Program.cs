using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
#region 添加各个板块的服务
builder.Services.AddApplication();
builder.Services.AddEFCoreMySql(builder.Configuration);
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
