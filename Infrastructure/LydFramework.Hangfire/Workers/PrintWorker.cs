using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Hangfire.Workers
{
    public class PrintWorker:IWorker
    {
        public string Cron => "0/10 * * * * ?";

        public void Work()
        {
            Console.WriteLine($"现在时间：{DateTime.Now}");
        }
    }
}
