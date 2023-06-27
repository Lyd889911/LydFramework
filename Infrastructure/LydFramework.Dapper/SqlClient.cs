using Dapper;
using LydFramework.Domain;
using LydFramework.Domain.Shared.Expections;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Data;

namespace LydFramework.Dapper
{
    public class SqlClient : ISqlClient
    {
        private IDbTransaction? _tran;
        private IDbConnection? _connection;

        public SqlClient(IConfiguration configuration)
        {
            InitConnection(configuration["Dapper:DbConnection"]);
        }

        public async Task<List<T>> List<T>(string sql)
        {
            CheckConnection();

            var ienumerable = await _connection.QueryAsync<T>(sql);
            return ienumerable.ToList();
        }
        public Task<T> Single<T>(string sql)
        {
            CheckConnection();

            return _connection.QuerySingleOrDefaultAsync<T>(sql);
        }
        public Task<T> First<T>(string sql)
        {
            CheckConnection();

            return _connection.QueryFirstOrDefaultAsync<T>(sql);
        }
        public async Task<int> Execute(string sql)
        {
            CheckConnection();

            int i = 0;
            if (_tran == null)
                i = await _connection.ExecuteAsync(sql);
            else
                i = await _connection.ExecuteAsync(sql, transaction: _tran);
            return i;
        }

        #region 连接
        public void InitConnection(string connection)
        {
            _connection = new SqlConnection(connection);
            if (_connection == null)
            {
                Console.WriteLine($"创建数据库连接失败：{connection}");
                return;
            }
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Close();
                _connection.Open();
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

            _tran = _connection.BeginTransaction();
            if (_tran == null)
                Console.WriteLine("开启事务失败");
        }
        public void CommitTransaction()
        {
            if (_tran == null)
            {
                Console.WriteLine("事务未开启，不能提交");
                return;
            }
            _tran.Commit();
            TranDispose();
        }
        public void RollbackTransaction()
        {
            if (_tran == null)
            {
                Console.WriteLine("事务未开启，不能回滚");
                return;
            }
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