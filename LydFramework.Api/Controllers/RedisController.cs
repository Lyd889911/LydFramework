using LydFramework.Application.Contracts.Dtos;
using LydFramework.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LydFramework.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RedisController : ControllerBase
    {
        private readonly ICacheClient _cache;
        public RedisController(ICacheClient cache)
        {
            _cache = cache;
        }

        public async Task<object> Get()
        {
            ResultDto dto = new ResultDto(500,"测试一下");
            //await _cache.Set("test:string", dto, new TimeSpan(0, 1, 0));
            //await _cache.PushList("new:list", new List<object>{ dto,dto,dto });
            //await _cache.SetHashValue("hhh:hash", "A", dto);
            //await _cache.SetHashValue("hhh:hash", "B", dto);
            //return await _cache.Get<ResultDto>("test:string");
            //return await _cache.GetList<ResultDto>("new:list");
            //return await _cache.ListLength("new:list");
            //return await _cache.PopList<ResultDto>("new:list");
            var hash = await _cache.GetHash<string,ResultDto>("hhh:hash");
            //var hash = await _cache.GetHashValue<string>("hhh:hash", "A");
            return "ok";
        }
    }
}
