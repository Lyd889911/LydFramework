using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Hangfire
{
    public interface IWorker
    {
        string WorkerName
        {
            get
            {
                return this.GetType().Name.Replace("Worker", "");
            }
        }
        string Cron { get;}
        void Work();
    }
}
