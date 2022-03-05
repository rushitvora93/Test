namespace FrameworksAndDrivers.DataAccess.DbContext
{
    public interface ITransactionDbContext
    {
        void CommitChanges();
        void RollbackChanges();
    }
}
