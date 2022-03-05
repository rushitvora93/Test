using BasicTypes;
using DtoTypes;
using FrameworksAndDrivers.RemoteData.GRPC.DataAccess;
using Grpc.Core;
using HelperTableService;

namespace FrameworksAndDrivers.RemoteData.GRPC.Wrapper
{
    public class HelperTableClient : IHelperTableClient
    {
        private readonly HelperTableService.HelperTableService.HelperTableServiceClient _helperTableClient;

        public HelperTableClient(HelperTableService.HelperTableService.HelperTableServiceClient helperTableClient)
        {
            _helperTableClient = helperTableClient;
        }

        public ListOfHelperTableEntity GetHelperTableByNodeId(Long request)
        {
            return _helperTableClient.GetHelperTableByNodeId(request, new CallOptions());
        }

        public ListOfHelperTableEntity GetAllHelperTableEntities()
        {
            return _helperTableClient.GetAllHelperTableEntities(new NoParams(), new CallOptions());
        }

        public ListOfModelLink GetHelperTableEntityModelLinks(HelperTableEntityLinkRequest request)
        {
            return _helperTableClient.GetHelperTableEntityModelLinks(request, new CallOptions());
        }

        public ListOfToolReferenceLink GetHelperTableEntityToolLinks(HelperTableEntityLinkRequest request)
        {
            return _helperTableClient.GetHelperTableEntityToolLinks(request, new CallOptions());
        }

        public ListOfHelperTableEntity InsertHelperTableEntityWithHistory(InsertHelperTableEntityWithHistoryRequest request)
        {
            return _helperTableClient.InsertHelperTableEntityWithHistory(request, new CallOptions());
        }

        public ListOfHelperTableEntity UpdateHelperTableEntityWithHistory(UpdateHelperTableEntityWithHistoryRequest request)
        {
            return _helperTableClient.UpdateHelperTableEntityWithHistory(request, new CallOptions());
        }
    }
}
