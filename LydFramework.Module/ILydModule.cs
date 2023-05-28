using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace LydFramework.Module
{
    public interface ILydModule
    {
        void ConfigureServices(IServiceCollection services);

        void OnApplication(IApplicationBuilder app);
    }
}