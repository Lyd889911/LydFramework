using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Redis
{
    public enum CacheDataType
    {
        String,
        List,
        Hash,
        Set,
        ZSet
    }
}
