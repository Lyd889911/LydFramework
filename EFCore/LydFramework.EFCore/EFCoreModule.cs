﻿using LydFramework.Module;
using LydFramework.Module.Attributes;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Configuration;


namespace LydFramework.EFCore
{
    [DependOn(typeof(DomainSharedModule))]
    public class EFCoreModule:LydModule
    {

        public override void ConfigureServices(IServiceCollection services)
        {
            var sp = services.BuildServiceProvider();
            var config = sp.GetRequiredService<IConfiguration>();
            bool isEnabledDefault = Convert.ToBoolean(config["EFCore:IsEnabledDefault"]);
            if (isEnabledDefault)
            {
                services.AddEFCore<AuthDbContext>(config);
            }                                 
        }

    }
}
