using LydFramework.Module;
using LydFramework.Module.Attributes;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionApplicationExtensions
    {
        /// <summary>
        /// 默认运行顺序
        /// </summary>
        private static int _defaultOrder = 1;


        /// <summary>
        /// 注册模块服务
        /// </summary>
        public static void AddModuleApplication<TModule>(this IServiceCollection services, bool isAutoInject = true)
        where TModule : ILydModule
        {
            var logger = services.BuildServiceProvider().GetRequiredService<ILoggerFactory>().CreateLogger<LydModule>();
            //要执行的模块和执行顺序集合
            var types = new List<Tuple<ILydModule, int>>();
            //根模块
            var type = typeof(TModule);
            //递归获取所有模块
            GetModuleType(type, types);
            //根据执行顺序排序并且去重
            var modules = types.OrderBy(x => x.Item2).Select(x => x.Item1).Distinct();
            //转换成数组
            var lydModules = modules as ILydModule[] ?? modules.ToArray();

            if (isAutoInject)
            {
                // 启动主动注册
                services.AddAutoInject(lydModules);
            }

            services.AddSingleton(types);

            //循环执行注册服务
            foreach (var t in lydModules)
            {
                logger.LogDebug($"Module配置服务：{t.GetType().Name}");
                t.ConfigureServices(services);
            }
        }

        /// <summary>
        /// 初始化Application
        /// </summary>
        public static void InitializeApplication(this IApplicationBuilder app)
        {
            //获取在AddModuleApplication中注册的模块的集合
            var types = app.ApplicationServices.GetService<List<Tuple<ILydModule, int>>>();

            var modules = types?.OrderBy(x => x.Item2).Select(x => x.Item1);

            if (modules == null)
                return;

            foreach (var module in modules)
                module.OnApplication(app);
        }

        /// <summary>
        /// 获取模块类型
        /// </summary>
        private static void GetModuleType(Type type, ICollection<Tuple<ILydModule, int>> types)
        {
            if (!typeof(ILydModule).IsAssignableFrom(type))
            {
                return;
            }

            // 通过反射创建一个对象并且回调方法
            ILydModule typeInstance = Activator.CreateInstance(type) as ILydModule;

            if (typeInstance != null)
            {
                //Console.WriteLine($"{typeInstance.GetType().Name}——{_defaultOrder}");
                types.Add(new Tuple<ILydModule, int>(typeInstance, _defaultOrder++));
                
            }
                

            // 获取DependOn特性注入的模块
            var modules = type.GetCustomAttributes().OfType<DependOnAttribute>()
                .SelectMany(x => x.Type).Where(x => typeof(ILydModule).IsAssignableFrom(x));


            foreach (var t in modules)
            {
                ILydModule? module = Activator.CreateInstance(type) as ILydModule;
                if (module == null)
                    continue;

                // 递归获取依赖的模块类，可能存在循环依赖的问题
                GetModuleType(t, types);
            }
        }
    }
}
