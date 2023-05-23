using LydFramework.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LydFramework.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MqController : ControllerBase
    {
        private readonly IMqClient _mqClient;
        public MqController(IMqClient mqClient)
        {
            _mqClient = mqClient;
        }

        [HttpGet]
        public string Get()
        {
            string str = DateTime.Now.ToString() + "呵呵";
            _mqClient.Publish("Default",str);
            return str;
        }
    }
}
