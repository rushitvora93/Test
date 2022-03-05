namespace Server.UseCases
{
    public interface IDataAccessBase
    {
        void Commit();
        void Rollback();
    }
}
