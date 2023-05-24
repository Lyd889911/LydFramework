using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore.Core
{
    public interface IUnitOfWork
    {
        public Task BeginTransactionAsync(CancellationToken cancellationToken = default);
        public Task CommitTransactionAsync(CancellationToken cancellationToken = default);
        public Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
    }
}
