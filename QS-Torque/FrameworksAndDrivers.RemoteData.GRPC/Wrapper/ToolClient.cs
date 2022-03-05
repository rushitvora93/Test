using BasicTypes;
using DtoTypes;
using FrameworksAndDrivers.RemoteData.GRPC.DataAccess;
using ToolService;

namespace FrameworksAndDrivers.RemoteData.GRPC.Wrapper
{
    public class ToolClient : IToolClient
    {
        private readonly ToolService.ToolService.ToolServiceClient _toolClient;

        public ToolClient(ToolService.ToolService.ToolServiceClient toolClient)
        {
            _toolClient = toolClient;
        }

        public ListOfTools LoadTools(LoadToolsRequest request)
        {
            return _toolClient.LoadTools(request);
        }

        public Tool GetToolById(Long toolId)
        {
            return _toolClient.GetToolById(toolId);
        }

        public ListOfTools InsertToolsWithHistory(InsertToolsWithHistoryRequest request)
        {
            return _toolClient.InsertToolsWithHistory(request);
        }

        public ListOfTools UpdateToolsWithHistory(UpdateToolsWithHistoryRequest request)
        {
            return _toolClient.UpdateToolsWithHistory(request);
        }

        public ListOfLocationToolAssignmentReferenceLink GetLocationToolAssignmentLinkForTool(Long locationToolAssignmentId)
        {
            return _toolClient.GetLocationToolAssignmentLinkForTool(locationToolAssignmentId);
        }

        public Bool IsSerialNumberUnique(String serialNumber)
        {
            return _toolClient.IsSerialNumberUnique(serialNumber);
        }

        public Bool IsInventoryNumberUnique(String inventoryNumber)
        {
            return _toolClient.IsInventoryNumberUnique(inventoryNumber);
        }

        public String GetToolComment(Long toolId)
        {
            return _toolClient.GetToolComment(toolId);
        }

        public LoadPictureForToolResponse LoadPictureForTool(LoadPictureForToolRequest request)
        {
            return _toolClient.LoadPictureForTool(request);
        }

        public ListOfTools LoadToolsForModel(Long modelId)
        {
            return _toolClient.LoadToolsForModel(modelId);
        }

        public ListOfToolModel LoadModelsWithAtLeasOneTool()
        {
            return _toolClient.LoadModelsWithAtLeasOneTool(new NoParams());
        }

        public ListOfToolModel LoadDeletedModelsWithAtLeastOneTool()
        {
            return _toolClient.LoadDeletedModelsWithAtLeasOneTool(new NoParams());
        }
    }
}