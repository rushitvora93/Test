using System;
using Microsoft.EntityFrameworkCore.Storage;

namespace FrameworksAndDrivers.DataAccess.DbContext
{
    public class EfTransactionDbContext : ITransactionDbContext, IDisposable
    {
        private readonly IDbContextTransaction _transaction;

        public EfTransactionDbContext(SqliteDbContext dbContext)
        {
            _transaction = dbContext.Database.BeginTransaction();
        }

        public void CommitChanges()
        {
            _transaction.Commit();
        }

        public void RollbackChanges()
        {
            _transaction.Rollback();
        }

        public void Dispose()
        {
            _transaction.Dispose();
        }
    }
}
