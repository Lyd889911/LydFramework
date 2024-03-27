using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Domain.Shared.BaseRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDapperRepository
    {
        public Task<IEnumerable<T>> GetListAsync<T>(string sql, object? p = null);
        public Task<T?> FirstAsync<T>(string sql, object? p = null);
        public Task<int> ExecuteAsync(string sql, object? p = null);
        public Task<IDataReader> GetDataReader(string sql, object? p = null);
        public void InitConnAndTran(string connection, string dbtype = "sqlserver", bool enableTran = false);
        public void BeginTransaction();
        public void CommitTransaction();
        public void RollbackTransaction();
    }
}
