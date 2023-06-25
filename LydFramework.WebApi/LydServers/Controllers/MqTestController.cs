
using LydFramework.Application.Contracts.LydServers.MqTest;
using Microsoft.AspNetCore.Mvc;

namespace LydFramework.WebApi.LydServers.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MqTestController : ControllerBase
    {
        private readonly IMqTestService _mqTestService;
        public MqTestController(IMqTestService mqTestService)
        {
            _mqTestService = mqTestService;
        }

        [HttpGet("{eventName}/{msg}")]
        public string Get([FromRoute] string eventName, [FromRoute] string msg)
        {
            return _mqTestService.Publish(eventName, msg);
        }
    }
}
