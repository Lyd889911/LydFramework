using Lydong.DynamicApi.Marks;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Application.Services
{
    public class TestService:IDynamicApi
    {
        public async Task<string> get1(int i, string a)
        {
            return i + a;
        }
        [Route("{id}")]
        public async Task<string> get2([FromRoute]string id)
        {
            return id + id;
        }

        public async Task<string> get3(TDto1 dto)
        {
            return dto.ToString();
        }
        [HttpPost]
        public string create4()
        {
            return "第二个post请求get";
        }

        public string post1([FromQuery] string a, [FromQuery] string b)
        {
            return a + b;
        }

        [NonDynamicApi]
        public string post2()
        {
            return "被屏幕的";
        }
        public void Job1()
        {
            Console.WriteLine("哈哈哈哈"+DateTime.Now);
        }
    }
    public record TDto1
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime DateTime { get; set; }
    }
}
