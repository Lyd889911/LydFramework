using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Common.Attributes
{
    /// <summary>
    /// API返回不包裹ResultDto
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class NonApiResultWrapperAttribute: Attribute
    {
    }
}
