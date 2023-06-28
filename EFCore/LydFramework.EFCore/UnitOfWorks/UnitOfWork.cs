using Microsoft.EntityFrameworkCore.ChangeTracking;


namespace EntityFrameworkCore.Core
{
    //一个DbContext对应一个UnitOfWork
    public sealed class UnitOfWork<TDbContext> :IUnitOfWork,IDisposable where TDbContext : DbContext
    {
        public bool IsDisposed { get; private set; }
        public bool IsRollback { get; private set; }
        public bool IsCompleted { get; private set; }

        private readonly TDbContext _dbContext;
        private readonly IServiceProvider _sp;
        //private readonly UnitOfWorkOptions _unitOfWorkOptions;

        public UnitOfWork(TDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException($"db context nameof{nameof(dbContext)} is null");
            _sp = serviceProvider;
            //_unitOfWorkOptions = unitOfWorkOptions.Value;
        }

        /// <summary>
        /// 开始事务
        /// </summary>
        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            IsCompleted = false;
            await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (IsCompleted)
                return;

            IsCompleted = true;
            ApplyChangeConventions();
            try
            {
                // 提交事务
                await _dbContext.SaveChangesAsync(cancellationToken);
                await _dbContext.Database.CommitTransactionAsync(cancellationToken);
            }
            catch (Exception)
            {
                // 发生异常回滚事务
                await RollbackTransactionAsync(cancellationToken);//ConfigureAwait(false);
                throw;
            }
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (IsCompleted)
                return;

            IsRollback = true;

            await _dbContext.Database.RollbackTransactionAsync(cancellationToken);
        }

        /// <summary>
        /// 保存改变
        /// </summary>
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ApplyChangeConventions();
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// 实体是否发生改变
        /// 发生改变就做对应的修改
        /// </summary>
        private void ApplyChangeConventions()
        {
            //_dbContext.ChangeTracker.DetectChanges();
            foreach (var entry in _dbContext.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Deleted:
                        SetDelete(entry);
                        break;
                    case EntityState.Modified:
                        SetModified(entry);
                        break;
                    case EntityState.Added:
                        SetCreation(entry);
                        break;
                }
            }
        }

        /// <summary>
        /// 更新实体的变更
        /// </summary>
        /// <param name="entry"></param>
        private void SetModified(EntityEntry entry)
        {
            if(entry.Entity is IHasModify entity)
            {
                entity.ModifyTime = DateTime.Now;
                entity.ModifyBy = GetUserId();
            }
        }

        private void SetCreation(EntityEntry entry)
        {
            if(entry.Entity is IHasCreate entity)
            {
                entity.CreateTime = DateTime.Now;
                entity.CreateBy = GetUserId();
            }

            if (entry.Entity is ITenant tenant)
            {
                tenant.TenantId = GetTenantId();
            }
        }

        private void SetDelete(EntityEntry entry)
        {
            if (entry.Entity is IHasDelete entity)
            {
                entity.IsDeleted = true;
                entity.DeleteBy = GetUserId();
                entity.DeleteTime = DateTime.Now;
            }
        }

        public long? GetUserId()
        {
            try
            {
                var identityProvider = _sp.GetService(typeof(IdentityProvider)) as IdentityProvider;

                var userId = identityProvider?.UserId;
                if (string.IsNullOrEmpty(userId))
                    return null;

                return Convert.ToInt64(userId);
            }
            catch
            {
                return null;
            }
        }

        public long? GetTenantId()
        {
            try
            {

                var identityProvider = _sp.GetService(typeof(IdentityProvider)) as IdentityProvider;

                var tenantId = identityProvider?.TenantId;
                if (string.IsNullOrEmpty(tenantId))
                    return null;

                return Convert.ToInt64(tenantId);
            }
            catch
            {
                return null;
            }
        }

        public void Dispose()
        {
            if (IsDisposed)
            {
                return;
            }

            IsDisposed = true;
        }

    }
}
