using SetupService;

namespace FrameworksAndDrivers.RemoteData.GRPC
{
    public interface ISetupClient
    {
        ListOfQstSetup GetQstSetupsByUserIdAndName(GetQstSetupsByUserIdAndNameRequest request);
        ListOfQstSetup GetColumnWidthsForGrid(GetColumnWidthsForGridRequest request);
        ListOfQstSetup InsertOrUpdateQstSetups(InsertOrUpdateQstSetupsRequest request);
    }
}
