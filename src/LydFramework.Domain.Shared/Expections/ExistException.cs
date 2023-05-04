using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Domain.Shared.Expections
{
    public class ExistException:Exception
    {
        public int Code { get; set; }
        public ExistException(string message, int code = 502) : base(message)
        {
            Code = code;
        }
    }
}
