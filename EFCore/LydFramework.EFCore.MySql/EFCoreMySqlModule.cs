using LydFramework.Module;
using LydFramework.Module.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.EFCore.MySql
{
    [DependOn(typeof(EFCoreModule))]
    public class EFCoreMySqlModule:LydModule
    {
    }
}
