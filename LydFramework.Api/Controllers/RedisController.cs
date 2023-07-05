using LydFramework.Application.Contracts.Dtos;
using LydFramework.Domain;
using LydFramework.Domain.InfrastructureContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LydFramework.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RedisController : ControllerBase
    {
        private readonly IRedisProvider _redis;
        public RedisController(IRedisProvider redis)
        {
            _redis = redis;
        }

        public async Task<object> Get()
        {
            ResultDto dto = new ResultDto(500,"测试一下");
            //await _redis.Set("test:string", dto, new TimeSpan(0, 1, 0));
            //await _redis.PushList("new:list", new List<object>{ dto,dto,dto });
            //await _redis.SetHashValue("hhh:hash", "A", dto);
            //await _redis.SetHashValue("hhh:hash", "B", dto);
            //return await _redis.Get<ResultDto>("test:string");
            //return await _redis.GetList<ResultDto>("new:list");
            //return await _redis.ListLength("new:list");
            //return await _redis.PopList<ResultDto>("new:list");
            var hash = await _redis.GetHash<string,ResultDto>("hhh:hash");
            //var hash = await _redis.GetHashValue<string>("hhh:hash", "A");
            return "ok";
        }
    }
}
