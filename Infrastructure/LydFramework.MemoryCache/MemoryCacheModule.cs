using LydFramework.Domain.InfrastructureContracts;
using LydFramework.Module;
using Microsoft.Extensions.DependencyInjection;

namespace LydFramework.MemoryCache
{
    public class MemoryCacheModule:LydModule
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddScoped<IMemoryCacheProvider, MemoryCacheProvider>();
        }
    }
}