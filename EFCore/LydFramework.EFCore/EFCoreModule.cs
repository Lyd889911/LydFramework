using LydFramework.Domain.Shared;
using LydFramework.Module;
using LydFramework.Module.Attributes;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.EFCore
{
    [DependOn(typeof(DomainSharedModule))]
    public class EFCoreModule:LydModule
    {

    }
}
