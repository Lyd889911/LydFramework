


using LydFramework.Application.DtoParams;
using LydFramework.Domain.Books;
using LydFramework.Domain.Repositorys;
using Lydong.DynamicApi.Marks;
using Lydong.Hangfire.Symbols;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Application.Services
{
    public class LIBService:IDynamicApi,IJob
    {
        private readonly SqlSugarRepository<BookList> _bookRepo;
        public LIBService(SqlSugarRepository<BookList> bookRepo)
        {
            _bookRepo = bookRepo;
        }

        public async Task<List<BookList>> GetList()
        {
            throw new NotImplementedException();
            var page = new PageModel() { PageIndex = 1,PageSize=10 };
            return await _bookRepo.GetPageListAsync(x => true, page);
        }
        public async Task<PageDto<List<BookList>>> GetJoinBook()
        {
            int total = 0;
            var list = _bookRepo.Context.Queryable<BookList>()
                .IncludesAllFirstLayer()
                .ToPageList(1, 10, ref total);
            return new PageDto<List<BookList>>(total, list);
        }

        [Job(Cron = "0/10 * * * * ?")]
        public void T1()
        {
            Console.WriteLine("t1:"+DateTime.Now);
        }

        [Job(Cron = "0/13 * * * * ?")]
        public void T2()
        {
            Console.WriteLine("t2:"+DateTime.Now);
        }
    }
}
