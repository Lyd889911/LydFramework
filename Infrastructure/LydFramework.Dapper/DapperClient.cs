using Dapper;
using LydFramework.Domain;
using LydFramework.Domain.Shared.Expections;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Data;

namespace LydFramework.Dapper
{
    public class DapperClient : IDapperClient, IDisposable
    {
        public IDbTransaction? _tran;
        public IDbConnection? _connection;

        public DapperClient(IConfiguration configuration)
        {
            InitConnAndTran(configuration["Dapper:DbConnection"]);
        }

        public async Task<List<T>> List<T>(string sql)
        {
            CheckConnection();
            var ienumerable = await _connection.QueryAsync<T>(sql,transaction:_tran);
            return ienumerable.ToList();
        }
        public Task<T> Single<T>(string sql)
        {
            CheckConnection();
            return _connection.QuerySingleOrDefaultAsync<T>(sql, transaction: _tran);
        }
        public Task<T> First<T>(string sql)
        {
            CheckConnection();
            return _connection.QueryFirstOrDefaultAsync<T>(sql, transaction: _tran);
        }
        public async Task<int> Execute(string sql)
        {
            CheckConnection();
            int i = await _connection.ExecuteAsync(sql, transaction: _tran);
            return i;
        }

        #region 连接
        public void InitConnAndTran(string connection,bool enableTran=false)
        {
            Console.WriteLine($"切换数据库连接：{connection}");
            try
            {
                _connection = new SqlConnection(connection);
            }
            catch(Exception ex)
            {

            }

            if (_connection == null)
            {
                Console.WriteLine($"切换数据库连接失败：{connection}");
                return;
            }
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Close();
                _connection.Open();
            }
            TranDispose();
            if (enableTran)
            {
                Console.WriteLine("切换工作单元");
                BeginTransaction();
            }
        }

        private void CheckConnection()
        {
            if (_connection == null || _connection.State != ConnectionState.Open)
            {
                throw new BusinessException("连接不可用");
            }
        }
        #endregion

        #region 事务
        public void BeginTransaction()
        {
            
            CheckConnection();
            Console.WriteLine("开启dapper工作单元");
            _tran = _connection.BeginTransaction();
            if (_tran == null)
                Console.WriteLine("开启事务失败");
        }
        public void CommitTransaction()
        {
            if (_tran == null)
            {
                //Console.WriteLine("事务未开启，不能提交");
                return;
            }
            Console.WriteLine("提交dapper工作单元");
            _tran.Commit();
            TranDispose();
        }
        public void RollbackTransaction()
        {
            
            if (_tran == null)
            {
                //Console.WriteLine("事务未开启，不能回滚");
                return;
            }
            Console.WriteLine("回滚dapper工作单元");
            _tran.Rollback();
            TranDispose();
        }
        #endregion

        public void Dispose()
        {
            ConnDispose();
            TranDispose();
        }
        private void TranDispose()
        {
            _tran?.Dispose();
            _tran = null;
        }
        private void ConnDispose()
        {
            _connection?.Close();
            _connection?.Dispose();
            _connection = null;
        }
    }
}