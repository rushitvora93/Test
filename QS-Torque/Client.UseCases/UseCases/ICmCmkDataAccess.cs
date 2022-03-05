namespace Core.UseCases
{
    //TODO: should this have a usecase?
    public interface ICmCmkDataAccess
    {
        (double cm, double cmk) LoadCmCmk();
    }
}