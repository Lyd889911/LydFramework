using LydFramework.Module;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using LydFramework.Module.Attributes;

namespace LydFramework.EFCore.SqlServer
{
    [DependOn(typeof(EFCoreModule))]
    public class EFCoreSqlServerModule:LydModule
    {

    }
}
