using BasicTypes;
using DtoTypes;
using FrameworksAndDrivers.RemoteData.GRPC.DataAccess;
using Grpc.Core;
using ToolUsageService;

namespace FrameworksAndDrivers.RemoteData.GRPC.Wrapper
{
    public class ToolUsageClient : IToolUsageClient
    {
        private readonly ToolUsageService.ToolUsageService.ToolUsageServiceClient _toolUsageClient;

        public ToolUsageClient(ToolUsageService.ToolUsageService.ToolUsageServiceClient toolUsageClient)
        {
            _toolUsageClient = toolUsageClient;
        }

        public ListOfToolUsage GetAllToolUsages()
        {
            return _toolUsageClient.GetAllToolUsages(new NoParams(), new CallOptions());
        }

        public ListOfLongs GetToolUsageLocationToolAssignmentReferences(Long id)
        {
            return _toolUsageClient.GetToolUsageLocationToolAssignmentReferences(id, new CallOptions());
        }

        public ListOfToolUsage InsertToolUsagesWithHistory(InsertToolUsagesWithHistoryRequest request)
        {
            return _toolUsageClient.InsertToolUsagesWithHistory(request, new CallOptions());
        }

        public ListOfToolUsage UpdateToolUsagesWithHistory(UpdateToolUsagesWithHistoryRequest request)
        {
            return _toolUsageClient.UpdateToolUsagesWithHistory(request, new CallOptions());
        }
    }
}
