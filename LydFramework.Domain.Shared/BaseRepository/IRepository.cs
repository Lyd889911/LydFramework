using LydFramework.Domain.Shared.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Domain.Shared.BaseRepository
{
    public interface IRepository<TEntity, TKey> : IRepository<TEntity> where TEntity : Entity
    {
        /// <summary>
        /// 删除
        /// </summary>
        Task DeleteAsync(TKey id, CancellationToken cancellationToken = default);

        /// <summary>
        /// 批量删除
        /// </summary>
        Task DeleteManyAsync(IEnumerable<TKey> ids, CancellationToken cancellationToken = default);
    }

    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// 返回满足条件的第一个元素
        /// </summary>
        Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 返回满足条件的第一个元素不存在抛出异常
        /// </summary>
        Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 获取列表
        /// </summary>
        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 是否存在
        /// </summary>
        Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 获取到IQueryable
        /// </summary>
        Task<IQueryable<TEntity>> GetQueryAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 获取到IQueryable
        /// </summary>
        Task<IQueryable<TResult>> GetQueryAsync<TResult>(Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TResult>> selector);

        /// <summary>
        /// 添加
        /// </summary>
        Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// 批量添加
        /// </summary>
        Task InsertManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// 删除
        /// </summary>
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// 批量删除
        /// </summary>
        Task DeleteManyAsync(IEnumerable<TEntity> entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// 更新
        /// </summary>
        Task<TEntity> UpdateAsync(TEntity entity);

        /// <summary>
        /// 批量更新
        /// </summary>
        Task UpdateManyAsync(IEnumerable<TEntity> entities);
    }
}
