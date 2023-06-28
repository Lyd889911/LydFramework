using LydFramework.Dapper.UnitOfWorks;
using LydFramework.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LydFramework.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DapperController : ControllerBase
    {
        private readonly IDapperClient _sqlClient;
        public DapperController(IDapperClient sqlClient)
        {
            _sqlClient = sqlClient;
        }
        [HttpGet]
        [DapperUnitOfWork]
        public async Task<object> Get()
        {
            _sqlClient.InitConnAndTran("Data Source=.;Initial Catalog=Catalog;Integrated Security=True;Encrypt=False;",true);
            await _sqlClient.Single<dynamic>("update dbset set yhm='asdfd' where id=1");
            //throw new Exception("发生错误");
            await _sqlClient.Single<dynamic>("update dbset set yhm='14312' where id=2");
            _sqlClient.CommitTransaction();
            return "ok";
        }
    }
}
