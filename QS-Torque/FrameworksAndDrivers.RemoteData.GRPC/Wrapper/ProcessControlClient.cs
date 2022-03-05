using BasicTypes;
using DtoTypes;
using FrameworksAndDrivers.RemoteData.GRPC.DataAccess;
using ProcessControlService;

namespace FrameworksAndDrivers.RemoteData.GRPC.Wrapper
{
    public class ProcessControlClient : IProcessControlClient
    {
        private readonly ProcessControlService.ProcessControlService.ProcessControlServiceClient _processControlClient;

        public ProcessControlClient(ProcessControlService.ProcessControlService.ProcessControlServiceClient processControlClient)
        {
            _processControlClient = processControlClient;
        }

        public void UpdateProcessControlConditionsWithHistory(UpdateProcessControlConditionsWithHistoryRequest updateRequest)
        {
            _processControlClient.UpdateProcessControlConditionsWithHistory(updateRequest);
        }

        public LoadProcessControlConditionForLocationResponse LoadProcessControlConditionForLocation(Long locationId)
        {
            return _processControlClient.LoadProcessControlConditionForLocation(locationId);
        }

        public ListOfProcessControlCondition InsertProcessControlConditionsWithHistory(
            InsertProcessControlConditionsWithHistoryRequest request)
        {
            return _processControlClient.InsertProcessControlConditionsWithHistory(request);
        }

        public ListOfProcessControlConditions LoadProcessControlConditions(NoParams request)
        {
            return _processControlClient.LoadProcessControlConditions(new NoParams());
        }
    }
}
