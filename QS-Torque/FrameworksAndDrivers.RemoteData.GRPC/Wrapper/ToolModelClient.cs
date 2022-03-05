using BasicTypes;
using DtoTypes;
using FrameworksAndDrivers.RemoteData.GRPC.DataAccess;
using Grpc.Core;
using ToolModelService;

namespace FrameworksAndDrivers.RemoteData.GRPC.Wrapper
{
    class ToolModelClient: IToolModelClient
    {
        public ToolModelClient(ToolModels.ToolModelsClient toolModelClient)
        {
            _toolModelClient = toolModelClient;
        }

        public ListOfToolModel GetAllToolModels()
        {
            return _toolModelClient.GetAllToolModels(new NoParams());
        }

        public ListOfToolModel UpdateToolModels(ListOfToolModelDiff toolModelDiffs)
        {
            return _toolModelClient.UpdateToolModel(toolModelDiffs);
        }

        public ListOfToolModel AddToolModel(ListOfToolModelDiff toolModelDiffs)
        {
            return _toolModelClient.InsertToolModel(toolModelDiffs);
        }

        public ListOfToolReferenceLink GetReferencedToolLinks(Long toolModelId)
        {
            return _toolModelClient.GetReferencedToolLinks(toolModelId);
        }

        public ListOfToolModel GetAllDeletedToolModels()
        {
            return _toolModelClient.GetAllDeletedToolModels(new NoParams());
        }

        public ListOfToolModel LoadDeletedToolModels()
        {
            return _toolModelClient.LoadDeletedToolModels(new NoParams(), new CallOptions());
        }

        private readonly ToolModelService.ToolModels.ToolModelsClient _toolModelClient;
    }
}
