using System.Collections.Generic;
using System.Linq;
using BasicTypes;
using Core;
using Core.UseCases;
using DtoTypes;
using FrameworksAndDrivers.RemoteData.GRPC.T4Mapper;
using ToolModelService;
using Picture = Core.Entities.Picture;
using ToolModel = Core.Entities.ToolModel;
using ToolModelDiff = Core.UseCases.ToolModelDiff;
using ToolReferenceLink = Core.Entities.ReferenceLink.ToolReferenceLink;
using User = Core.Entities.User;

namespace FrameworksAndDrivers.RemoteData.GRPC.DataAccess
{
    public interface IToolModelClient
    {
        ListOfToolModel GetAllToolModels();
        ListOfToolModel UpdateToolModels(ListOfToolModelDiff toolModelDiffs);
        ListOfToolModel AddToolModel(ListOfToolModelDiff toolModelDiffs);
        ListOfToolReferenceLink GetReferencedToolLinks(Long toolModelId);
        ListOfToolModel LoadDeletedToolModels();
        ListOfToolModel GetAllDeletedToolModels();
    }

    public class ToolModelDataAccess: IToolModelData, IToolModelPictureData
    {
        public ToolModelDataAccess(IClientFactory clientFactory, IToolDisplayFormatter toolDisplayFormatter)
        {
            _clientFactory = clientFactory;
            _toolDisplayFormatter = toolDisplayFormatter;
        }
        private IToolModelClient GetClient()
        {
            return _clientFactory.AuthenticationChannel.GetToolModelClient();
        }

        public List<ToolModel> LoadToolModels()
        {
            var mapper = new Mapper();
            var result = GetClient().GetAllToolModels();
            return result.ToolModels.Select(toolModelDto => mapper.DirectPropertyMapping(toolModelDto)).ToList();
        }

        public List<ToolModel> LoadDeletedToolModels()
        {
            var mapper = new Mapper();
            var result = GetClient().LoadDeletedToolModels();
            return result.ToolModels.Select(toolModelDto => mapper.DirectPropertyMapping(toolModelDto)).ToList();
        }

        public Picture LoadPictureForToolModel(long toolModelId)
        {
            return null; // TODO: Pictures MUST be implemented when hash-based-lookup is implemented
        }

        public void RemoveToolModels(List<ToolModel> toolModels, User user)
        {
            var mapper = new Mapper();
            var toolModelDiffs = new ListOfToolModelDiff();
            toolModels.ForEach(toolModel =>
            {
                var oldModel = mapper.DirectPropertyMapping(toolModel);
                oldModel.Alive = true;
                var newModel = mapper.DirectPropertyMapping(toolModel);
                newModel.Alive = false;
                toolModelDiffs.ToolModelDiffs.Add(
                    new DtoTypes.ToolModelDiff
                    {
                        UserId = user.UserId.ToLong(),
                        OldToolModel = oldModel,
                        NewToolModel = newModel
                    });
            });
            GetClient().UpdateToolModels(toolModelDiffs);
        }

        public ToolModel AddToolModel(ToolModel toolModel, User byUser)
        {
            var mapper = new Mapper();
            var toolModelDIffs = new ListOfToolModelDiff();
            toolModelDIffs.ToolModelDiffs.Add(
                new DtoTypes.ToolModelDiff
                {
                    UserId = byUser.UserId.ToLong(),
                    NewToolModel = mapper.DirectPropertyMapping(toolModel)
                });
            var result = GetClient().AddToolModel(toolModelDIffs);
            return
                result.ToolModels.Count > 0
                    ? mapper.DirectPropertyMapping(result.ToolModels[0])
                    : null;
        }

        public ToolModel UpdateToolModel(ToolModelDiff toolModelDiff)
        {
            var mapper = new Mapper();
            var toolModelDiffs = new ListOfToolModelDiff();
            var oldToolModel = mapper.DirectPropertyMapping(toolModelDiff.OldToolModel);
            oldToolModel.Alive = true;
            var newToolModel = mapper.DirectPropertyMapping(toolModelDiff.NewToolModel);
            newToolModel.Alive = true;
            toolModelDiffs.ToolModelDiffs.Add(
                new DtoTypes.ToolModelDiff
                {
                    UserId = toolModelDiff.User.UserId.ToLong(),
                    Comment = toolModelDiff.Comment.ToDefaultString(),
                    OldToolModel = oldToolModel,
                    NewToolModel = newToolModel
                });
            GetClient().UpdateToolModels(toolModelDiffs);
            return toolModelDiff.NewToolModel;
        }

        public List<ToolReferenceLink> LoadReferencedTools(long toolModelId)
        {
            var result = GetClient().GetReferencedToolLinks(new Long {Value = toolModelId});
            return result.ToolReferenceLinks.Select(toolReferenceLink => new ToolReferenceLink(
                new Core.Entities.QstIdentifier(toolReferenceLink.Id),
                toolReferenceLink.InventoryNumber,
                toolReferenceLink.SerialNumber,
                _toolDisplayFormatter)).ToList();
        }

        private IClientFactory _clientFactory;
        private IToolDisplayFormatter _toolDisplayFormatter;
    }
}
