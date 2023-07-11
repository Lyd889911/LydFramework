using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LydFramework.Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        [HttpGet]
        public string T1()
        {
            return "成功" + DateTime.Now.ToString();
        }
        [HttpGet]
        public string T2([FromQuery] T2Dto dto)
        {
            return "成功" + DateTime.Now.ToString();
        }
        [HttpGet("{Id}/{Age}/{Name}")]
        public string T3([FromRoute]T2Dto dto)
        {
            return "成功" + DateTime.Now.ToString();
        }
        [HttpPost]
        public string T4(T2Dto dto)
        {
            return "成功" + DateTime.Now.ToString();
        }
        [HttpPost]
        public string T5([FromForm]T2Dto dto)
        {
            return "成功" + DateTime.Now.ToString();
        }
    }
    public class T2Dto
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public int Age { get; set; }
    }
}
