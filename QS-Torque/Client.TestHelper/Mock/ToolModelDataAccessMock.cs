using System.Collections.Generic;
using Core.Entities;
using Core.Entities.ReferenceLink;
using Core.UseCases;

namespace TestHelper.Mock
{
    public class ToolModelDataAccessMock : IToolModelData
    {
        public ToolModel AddToolModel(ToolModel toolModel, User byUser)
        {
            return new ToolModel();
        }

        public List<(double cm, double cmk)> LoadCmCmk()
        {
            return new List<(double cm, double cmk)>();
        }

        public Picture LoadPictureForToolModel(long toolModelId)
        {
            return new Picture();
        }

        public List<ToolReferenceLink> LoadReferencedTools(long modelId)
        {
            return new List<ToolReferenceLink>();
        }

        public List<ToolModel> LoadToolModels()
        {
            return new List<ToolModel>();
        }

        public void RemoveToolModels(List<ToolModel> toolModels, User user)
        {

        }

        public ToolModel UpdateToolModel(ToolModelDiff toolModelDiff)
        {
            return new ToolModel();
        }
    }
}
