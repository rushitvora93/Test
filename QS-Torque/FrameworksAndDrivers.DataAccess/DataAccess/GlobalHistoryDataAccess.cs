using System;
using FrameworksAndDrivers.DataAccess.DbContext;
using FrameworksAndDrivers.DataAccess.DbEntities;
using Server.UseCases.UseCases;

namespace FrameworksAndDrivers.DataAccess.DataAccess
{
    public class GlobalHistoryDataAccess : DataAccessBase, IGlobalHistoryDataAccess
    {
        public GlobalHistoryDataAccess(SqliteDbContext dbContext, ITransactionDbContext transactionContext) 
            : base(transactionContext, dbContext)
        { }

        public long CreateGlobalHistory(string type, DateTime currentTimestamp)
        {
            var globalHistory = new GlobalHistory()
            {
                TYPE = type,
                TIMESTAMP = currentTimestamp
            };
            _dbContext.GlobalHistories.Add(globalHistory);
            _dbContext.SaveChanges();
            return globalHistory.ID;
        }
    }
}
