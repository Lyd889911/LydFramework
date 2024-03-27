using LydFramework.Common.BaseEntity;
using LydFramework.Common.Utils;
using LydFramework.Domain.InfrastructureContracts;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Domain.Repositorys
{
    public class SqlSugarRepository<T> : SimpleClient<T> where T : class, new()
    {
        public SqlSugarRepository(ISqlSugarClient? context = null) : base(context)
        {
            Context = context;
        }
        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exp"></param>
        public void LogicDelete<T>(Expression<Func<T, bool>> exp) where T : class,IHasDelete, new()
        {
            Context.Updateable<T>()
                .SetColumns(it => new T() { IsDeleted = true,DeleteTime = DateTime.Now,DeleteBy= AppUtils.UserId },
                 true)//true 支持更新数据过滤器赋值字段一起更新
                .Where(exp).ExecuteCommand();
        }
    }
}
