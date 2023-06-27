using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Domain
{
    public interface ISqlClient
    {
        public Task<List<T>> List<T>(string sql);
        public Task<T> Single<T>(string sql);
        public Task<T> First<T>(string sql);
        public Task<int> Execute(string sql);
        public void InitConnection(string connection);
        public void BeginTransaction();
        public void CommitTransaction();
        public void RollbackTransaction();
        public void Dispose();
    }
}
