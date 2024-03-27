using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using LydFramework.Common.BaseEntity;
using LydFramework.Common.Utils;
using LydFramework.DbMigration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SqlSugar;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        Migrator.DbFirst(services);
    })
    .Build();



await host.RunAsync();


