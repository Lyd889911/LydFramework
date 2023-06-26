using LydFramework.DbMigration;
using LydFramework.EFCore.LydServers.DbContexts;
using LydFramework.Tools;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(async services =>
    {
        var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
        services.AddEFCore<AuthDbContext>(configuration);
        services.AddScoped<SeedData>();
        var sp = services.BuildServiceProvider();
        var context = sp.GetRequiredService<AuthDbContext>();
        if (context.Database.GetPendingMigrations().Any())
        {
            context.Database.Migrate(); //Ö´ÐÐÇ¨ÒÆ
        }
        var seeddata = sp.GetRequiredService<SeedData>();
        await seeddata.Seed();
    })
    .Build();


await host.RunAsync();
