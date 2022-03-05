using BasicTypes;
using DtoTypes;
using FrameworksAndDrivers.RemoteData.GRPC.DataAccess;
using Grpc.Core;
using StatusService;

namespace FrameworksAndDrivers.RemoteData.GRPC.Wrapper
{
    public class StatusClient : IStatusClient
    {
        private readonly StatusService.StatusService.StatusServiceClient _statusClient;

        public StatusClient(StatusService.StatusService.StatusServiceClient statusClient)
        {
            _statusClient = statusClient;
        }

        public ListOfStatus LoadStatus()
        {
            return _statusClient.LoadStatus(new NoParams(), new CallOptions());
        }

        public ListOfToolReferenceLink GetStatusToolLinks(LongRequest status)
        {
            return _statusClient.GetStatusToolLinks(status, new CallOptions());
        }

        public ListOfStatus InsertStatusWithHistory(InsertStatusWithHistoryRequest request)
        {
            return _statusClient.InsertStatusWithHistory(request, new CallOptions());
        }

        public ListOfStatus UpdateStatusWithHistory(UpdateStatusWithHistoryRequest request)
        {
            return _statusClient.UpdateStatusWithHistory(request, new CallOptions());
        }
    }
}
