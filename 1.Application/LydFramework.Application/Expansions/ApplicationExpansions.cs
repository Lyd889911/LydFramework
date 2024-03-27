using Lydong.DynamicApi.Marks;
using Mapster;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ApplicationExpansions
    {
        public static void AddLydApplicationService(this IServiceCollection services)
        {
            //services.AddAutoMapper(Assembly.GetExecutingAssembly());
            //services.AddLydValidators();


            // 获取全局映射配置
            var config = TypeAdapterConfig.GlobalSettings;

            var assemblyService = Assembly.GetExecutingAssembly();
            // 扫描所有继承  IRegister 接口的对象映射配置
            config.Scan(assemblyService);

            // 配置默认全局映射（支持覆盖）
            config.Default
                .NameMatchingStrategy(NameMatchingStrategy.Flexible)
                .PreserveReference(true);

            //注册到容器，方便服务之间互相调用
            var types = assemblyService.GetTypes();
            foreach (var type in types)
            {
                if (type.IsAssignableTo(typeof(IDynamicApi)))
                {
                    services.AddScoped(type);
                }
            }

            services.AddLydongHangfire();
        }

        public static void UseLydApplication(this WebApplication app)
        {
            app.UseLydongHangfire();
        }
    }
}
