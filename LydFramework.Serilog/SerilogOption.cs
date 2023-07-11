using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Serilog
{
    public class SerilogOption
    {
        public bool WriteToFile { get; set; }
        public bool WriteToConsole { get; set; }
        public string FilePath { get; set; }
        public RollingInterval RollingInterval { get; set; }
        public int FileCountLimit { get; set; }
        public string SerilogOutputTemplate { get; set; }
    }
}
