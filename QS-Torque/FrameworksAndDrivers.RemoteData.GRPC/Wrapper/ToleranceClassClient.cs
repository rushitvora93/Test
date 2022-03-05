using BasicTypes;
using DtoTypes;
using FrameworksAndDrivers.RemoteData.GRPC.DataAccess;
using Grpc.Core;
using ToleranceClassService;

namespace FrameworksAndDrivers.RemoteData.GRPC.Wrapper
{
    class ToleranceClassClient : IToleranceClassClient
    {
        private readonly ToleranceClasses.ToleranceClassesClient _toleranceClassClient;

        public ToleranceClassClient(ToleranceClasses.ToleranceClassesClient toleranceClassClient)
        {
            _toleranceClassClient = toleranceClassClient;
        }

        public ListOfToleranceClasses LoadToleranceClasses()
        {
            return _toleranceClassClient.LoadToleranceClasses(new NoParams(), new CallOptions());
        }

        public ListOfToleranceClasses InsertToleranceClassesWithHistory(InsertToleranceClassesWithHistoryRequest request)
        {
            return _toleranceClassClient.InsertToleranceClassesWithHistory(request, new CallOptions());
        }

        public ListOfToleranceClasses UpdateToleranceClassesWithHistory(UpdateToleranceClassesWithHistoryRequest request)
        {
            return _toleranceClassClient.UpdateToleranceClassesWithHistory(request, new CallOptions());
        }

        public ListOfLocationLink GetToleranceClassLocationLinks(LongRequest toleranceClassId)
        {
            return _toleranceClassClient.GetToleranceClassLocationLinks(toleranceClassId, new CallOptions());
        }

        public ListOfLongs GetToleranceClassLocationToolAssignmentLinks(LongRequest request)
        {
            return _toleranceClassClient.GetToleranceClassLocationToolAssignmentLinks(request, new CallOptions());
        }

        public GetToleranceClassesFromHistoryForIdsResponse GetToleranceClassFromHistoryForIds(
            ListOfToleranceClassFromHistoryParameter datas)
        {
            return _toleranceClassClient.GetToleranceClassesFromHistoryForIds(datas, new CallOptions());
        }
    }
}
