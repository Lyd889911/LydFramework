using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Domain.InfrastructureContracts
{
    /// <summary>
    /// Sql提供者：用来执行sql语句，实现DapperProvider
    /// </summary>
    public interface ISqlProvider
    {
        public Task<List<T>> List<T>(string sql);
        public Task<T> Single<T>(string sql);
        public Task<T> First<T>(string sql);
        public Task<int> Execute(string sql);
        public void InitConnAndTran(string connection, bool enableTran = false);
        public void BeginTransaction();
        public void CommitTransaction();
        public void RollbackTransaction();
    }
}
