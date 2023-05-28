using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Module.Attributes
{
    /// <summary>
    /// 指定要把该类注册成什么类型
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ExposeServicesAttribute : Attribute
    {
        public readonly Type? Type;

        public ExposeServicesAttribute(Type? type)
        {
            Type = type;
        }
    }
}
