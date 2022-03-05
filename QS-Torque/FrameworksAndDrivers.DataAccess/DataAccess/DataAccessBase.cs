using FrameworksAndDrivers.DataAccess.DbContext;
using Server.UseCases;

namespace FrameworksAndDrivers.DataAccess.DataAccess
{
    public class DataAccessBase : IDataAccessBase
    {
        private readonly ITransactionDbContext _transactionContext;
        protected readonly SqliteDbContext _dbContext;

        public DataAccessBase(ITransactionDbContext transactionContext, SqliteDbContext dbContext)
        {
            _transactionContext = transactionContext;
            _dbContext = dbContext;
        }

        public void Commit()
        {
            _transactionContext.CommitChanges();
        }

        public void Rollback()
        {
            _transactionContext.RollbackChanges();
        }
    }
}
