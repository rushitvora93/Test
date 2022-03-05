using SetupService;

namespace FrameworksAndDrivers.RemoteData.GRPC.Wrapper
{
    public class SetupClient : ISetupClient
    {
        private readonly SetupService.SetupService.SetupServiceClient _setupClient;

        public SetupClient(SetupService.SetupService.SetupServiceClient setupClient)
        {
            _setupClient = setupClient;
        }
        public ListOfQstSetup GetQstSetupsByUserIdAndName(GetQstSetupsByUserIdAndNameRequest request)
        {
            return _setupClient.GetQstSetupsByUserIdAndName(request);
        }

        public ListOfQstSetup GetColumnWidthsForGrid(GetColumnWidthsForGridRequest request)
        {
            return _setupClient.GetColumnWidthsForGrid(request);
        }

        public ListOfQstSetup InsertOrUpdateQstSetups(InsertOrUpdateQstSetupsRequest request)
        {
            return _setupClient.InsertOrUpdateQstSetups(request);
        }
    }
}
