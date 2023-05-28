using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Module.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DisabledInjectAttribute : Attribute
    {
        public readonly bool Disabled;

        public DisabledInjectAttribute(bool disabled = true)
        {
            Disabled = disabled;
        }
    }
}
