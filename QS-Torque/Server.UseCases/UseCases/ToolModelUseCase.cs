using System.Collections.Generic;
using Server.Core.Diffs;
using Server.Core.Entities;
using Server.Core.Entities.ReferenceLink;

namespace Server.UseCases.UseCases
{
    public interface IToolModelUseCase
    {
        List<ToolModel> GetAllToolModels();
        List<ToolModel> InsertToolModels(List<ToolModelDiff> toolModelDiffs);
        List<ToolModel> UpdateToolModels(List<ToolModelDiff> toolModelDiffs);
        List<ToolReferenceLink> GetReferencedToolLinks(ToolModelId toolModelId);
        List<ToolModel> LoadDeletedToolModels();
    }

    public interface IToolModelDataAccess
    {
        void Commit();
        List<ToolModel> GetAllToolModels();
        List<ToolModel> InsertToolModels(List<ToolModelDiff> toolModelDiffs);
        List<ToolModel> UpdateToolModels(List<ToolModelDiff> toolModelDiffs);
        List<ToolReferenceLink> GetReferencedToolLinks(ToolModelId toolModelId);
        List<ToolModel> LoadDeletedToolModels();
    }

    public class ToolModelUseCase: IToolModelUseCase
    {
        public ToolModelUseCase(IToolModelDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public List<ToolModel> GetAllToolModels()
        {
            return _dataAccess.GetAllToolModels();
        }

        public List<ToolModel> InsertToolModels(List<ToolModelDiff> toolModelDiffs)
        {
            var result = _dataAccess.InsertToolModels(toolModelDiffs);
            _dataAccess.Commit();
            return result;
        }

        public List<ToolModel> UpdateToolModels(List<ToolModelDiff> toolModelDiffs)
        {
            var result = _dataAccess.UpdateToolModels(toolModelDiffs);
            _dataAccess.Commit();
            return result;
        }

        public List<ToolReferenceLink> GetReferencedToolLinks(ToolModelId toolModelId)
        {
            return _dataAccess.GetReferencedToolLinks(toolModelId);
        }
        public List<ToolModel> LoadDeletedToolModels()
        {
            return _dataAccess.LoadDeletedToolModels();
        }
        private readonly IToolModelDataAccess _dataAccess;
    }
}
