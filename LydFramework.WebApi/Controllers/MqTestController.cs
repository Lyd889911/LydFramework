using LydFramework.Application.Contracts.MqTest;
using LydFramework.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LydFramework.WebApi.Controllers
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

        [HttpGet("{msg}")]
        public string Get([FromRoute]string msg)
        {
            return _mqTestService.Publish(msg);
        }
    }
}
