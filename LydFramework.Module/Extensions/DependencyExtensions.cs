using LydFramework.Module;
using LydFramework.Module.Attributes;
using LydFramework.Module.Dependencys;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyExtensions
    {
        public static void AddAutoInject(this IServiceCollection services, IEnumerable<ILydModule> lydModules)
        {
            // 加载所有需要注入的程序集（只有引用的模块）
            var allTypes = lydModules.Select(x => x.GetType().Assembly).Distinct()
                .SelectMany(x => x.GetTypes());

            // 过滤程序集，剩下可以注册的类
            var types = allTypes
                .Where(type => type.IsAssignableModule());

            // 根据实现的接口注册相对应的生命周期
            foreach (var t in types)
            {
                var interfaces = t.GetDependencyType();

                Console.WriteLine($"注册：【{interfaces?.Name}:{t.Name}】");

                if (interfaces != null)
                {
                    if (t.IsAssignableTo(typeof(ITransientDependency)))
                        services.AddTransient(interfaces, t);

                    else if (t.IsAssignableTo(typeof(IScopedDependency)))
                        services.AddScoped(interfaces, t);

                    else if (t.IsAssignableTo(typeof(ISingletonDependency)))
                        services.AddSingleton(interfaces, t);
                }
                else
                {
                    if (t.IsAssignableTo(typeof(ITransientDependency)))
                        services.AddTransient(t);

                    else if (t.IsAssignableTo(typeof(IScopedDependency)))
                        services.AddScoped(t);

                    else if (t.IsAssignableTo(typeof(ISingletonDependency)))
                        services.AddSingleton(t);
                }
            }

            //指针为空，方便垃圾回收
            types = null;
            allTypes = null;
        }

        /// <summary>
        /// 是否支持注入
        /// </summary>
        public static bool IsAssignableModule(this Type type)
        {
            //如果标记了DisabledInject特性，禁止注册
            var disabledInjectAttribute = type.GetCustomAttribute<DisabledInjectAttribute>();
            if (disabledInjectAttribute?.Disabled == true)
                return false;


            //如果该类型是抽象的或者接口，不能注册
            if (type.Attributes.HasFlag(TypeAttributes.Abstract) || type.Attributes.HasFlag(TypeAttributes.Interface))
                return false;

            //标记了这些接口才能注册
            if (typeof(ISingletonDependency).IsAssignableFrom(type) ||
                typeof(IScopedDependency).IsAssignableFrom(type) ||
                typeof(ITransientDependency).IsAssignableFrom(type))
                return true;

            return false;
        }

        /// <summary>
        /// 获取注册类型
        /// </summary>
        private static Type? GetDependencyType(this Type type)
        {
            var exposeServices = type.GetCustomAttribute<ExposeServicesAttribute>();

            if (exposeServices == null)
            {
                //return type.GetInterfaces().Where(x => x.Name.EndsWith(type.Name))?.FirstOrDefault();
                return type.GetInterfaces().Where(x => type.Name.EndsWith(x.Name.Substring(1)))?.FirstOrDefault();
            }

            return type.GetInterfaces().Where(x => x == exposeServices.Type)?.FirstOrDefault();
        }
    }
}
