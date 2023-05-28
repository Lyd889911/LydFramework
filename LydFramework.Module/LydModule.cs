using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Module
{
    public abstract class LydModule : ILydModule
    {
        private IServiceCollection _serviceCollection;
        public virtual void ConfigureServices(IServiceCollection services)
        {
            _serviceCollection = services ?? throw new ArgumentNullException(nameof(services));
        }

        public virtual void OnApplication(IApplicationBuilder app)
        {

        }
    }
}
