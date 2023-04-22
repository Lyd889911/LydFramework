using LydFramework.DbMigration;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
        services.AddEFCoreMySql(configuration);
        //services.AddHostedService<Worker>();
    })
    .Build();


await host.RunAsync();
