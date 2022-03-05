using System.Collections.Generic;
using Server.Core.Diffs;
using Server.Core.Entities;
using Server.Core.Entities.ReferenceLink;
using State;

namespace Server.UseCases.UseCases
{
    public interface IToolUseCase
    {
        List<Tool> LoadTools(int index, int size);
        Tool GetToolById(ToolId toolId);
        List<Tool> InsertToolsWithHistory(List<ToolDiff> toolDiff, bool returnList);
        List<Tool> UpdateToolsWithHistory(List<ToolDiff> toolDiff);
        List<LocationToolAssignmentReferenceLink> GetLocationToolAssignmentLinkForTool(ToolId id);
        bool IsSerialNumberUnique(string serialNumber);
        bool IsInventoryNumberUnique(string inventoryNumber);
        string GetToolComment(ToolId toolId);
        Picture LoadPictureForTool(ToolId toolId, int requestFileType);
        List<Tool> LoadToolsForModel(ToolModelId toolModelId);
        List<ToolModel> LoadModelsWithAtLeasOneTool();
    }

    public interface IToolDataAccess
    {
        void Commit();
        List<Tool> LoadTools(int index, int size);
        Tool GetToolById(ToolId toolId);
        List<Tool> InsertToolsWithHistory(List<ToolDiff> toolDiff, bool returnList);
        List<Tool> UpdateToolsWithHistory(List<ToolDiff> toolDiff);
        List<LocationToolAssignmentReferenceLink> GetLocationToolAssignmentLinkForTool(ToolId id);
        bool IsSerialNumberUnique(string serialNumber);
        bool IsInventoryNumberUnique(string inventoryNumber);
        List<Tool> LoadToolsForModel(ToolModelId toolModelId);
        List<ToolModel> LoadModelsWithAtLeasOneTool();
    }

    public class ToolUseCase : IToolUseCase
    {
        private readonly IToolDataAccess _toolDataAccess;
        private readonly IQstCommentDataAccess _qstCommentDataAccess;
        private readonly IPictureDataAccess _pictureDataAccess;

        public ToolUseCase(IToolDataAccess toolDataAccess, IQstCommentDataAccess qstCommentDataAccess,
            IPictureDataAccess pictureDataAccess)
        {
            _toolDataAccess = toolDataAccess;
            _qstCommentDataAccess = qstCommentDataAccess;
            _pictureDataAccess = pictureDataAccess;
        }

        public List<Tool> LoadTools(int index, int size)
        {
            return _toolDataAccess.LoadTools(index, size);
        }

        public Tool GetToolById(ToolId toolId)
        {
            return _toolDataAccess.GetToolById(toolId);
        }

        public List<Tool> InsertToolsWithHistory(List<ToolDiff> toolDiff, bool returnList)
        {
            var tools = _toolDataAccess.InsertToolsWithHistory(toolDiff, returnList);
            _toolDataAccess.Commit();
            return tools;
        }

        public List<Tool> UpdateToolsWithHistory(List<ToolDiff> toolDiff)
        {
            var tools = _toolDataAccess.UpdateToolsWithHistory(toolDiff);
            _qstCommentDataAccess.InsertQstComments(GetQstComments(toolDiff));
            _toolDataAccess.Commit();
            return tools;
        }

        private List<QstComment> GetQstComments(List<ToolDiff> diffs)
        {
            var comments = new List<QstComment>();
            if (diffs == null)
            {
                return comments;
            }

            foreach (var diff in diffs)
            {
                if (diff.GetOldTool().Comment == diff.GetNewTool().Comment)
                {
                    continue;
                }

                var comment = new QstComment()
                {
                    Comment = new HistoryComment(diff.GetNewTool().Comment),
                    UserId = diff.GetUser().UserId,
                    NodeId = (long)NodeId.Tool,
                    NodeSeqid = diff.GetNewTool().Id.ToLong()
                };

                comments.Add(comment);
            }

            return comments;
        }

        public List<LocationToolAssignmentReferenceLink> GetLocationToolAssignmentLinkForTool(ToolId id)
        {
            return _toolDataAccess.GetLocationToolAssignmentLinkForTool(id);
        }

        public bool IsSerialNumberUnique(string serialNumber)
        {
            return _toolDataAccess.IsSerialNumberUnique(serialNumber);
        }

        public bool IsInventoryNumberUnique(string inventoryNumber)
        {
            return _toolDataAccess.IsInventoryNumberUnique(inventoryNumber);
        }

        public string GetToolComment(ToolId toolId)
        {
            var comment = _qstCommentDataAccess.GetQstCommentByNodeIdAndNodeSeqId(NodeId.Tool,
                toolId.ToLong());

            return comment == null ? "" : comment.ToDefaultString();
        }

        public Picture LoadPictureForTool(ToolId toolId, int fileType)
        {
            return _pictureDataAccess.GetQstPicture(fileType, NodeId.Location, toolId.ToLong());
        }

        public List<Tool> LoadToolsForModel(ToolModelId toolModelId)
        {
            return _toolDataAccess.LoadToolsForModel(toolModelId);
        }

        public List<ToolModel> LoadModelsWithAtLeasOneTool()
        {
            return _toolDataAccess.LoadModelsWithAtLeasOneTool();
        }
    }
}
