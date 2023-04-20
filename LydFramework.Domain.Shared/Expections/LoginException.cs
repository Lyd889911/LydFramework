using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Domain.Shared.Expections
{
    public class LoginException:Exception
    {
        public int Code { get; set; }
        public LoginException(string message,int code=501):base(message)
        {
            Code = code;
        }
    }
}
