using LydFramework.DbMigration;
using LydFramework.Tools;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(async services =>
    {
        var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
        services.AddEFCoreMySql(configuration);
        //services.AddHostedService<SeedWorker>();
        //services.AddScoped<SeedData>();
        //var sp = services.BuildServiceProvider();
        //var seeddata = sp.GetRequiredService<SeedData>();
        //await seeddata.Seed();
        Dictionary<Guid, string> d = new Dictionary<Guid, string>();
        int c = 1;
        for(int i = 0; i < 10000; i++)
        {
            //long l = IdHelper.GetId();
            //Guid g = Guid.NewGuid();
            Guid g= IdHelper.GenerateSequentialGuid();
            //await Console.Out.WriteLineAsync(g.ToString());
            if (d.ContainsKey(g))
                await Console.Out.WriteLineAsync($"第{c++}次生成了相同的id:{g}");
            else
                d.Add(g, null);

        }
    })
    .Build();


await host.RunAsync();
