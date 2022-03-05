using System.Collections.Generic;
using Server.Core.Diffs;
using Server.Core.Entities;
using Server.Core.Entities.ReferenceLink;
using State;

namespace Server.UseCases.UseCases
{
    public interface IHelperTableUseCase
    {
        List<HelperTableEntity> GetHelperTableByNodeId(NodeId nodeId);
        List<HelperTableEntity> GetAllHelperTableEntities();
        List<ToolModelReferenceLink> GetHelperTableEntityModelLinks(long id, NodeId nodeId);
        List<ToolReferenceLink> GetHelperTableEntityToolLinks(long id, NodeId nodeId);
        List<HelperTableEntity> InsertHelperTableEntityWithHistory(List<HelperTableEntityDiff> helperTableEntityDiffs, bool returnList);
        List<HelperTableEntity> UpdateHelperTableEntityWithHistory(List<HelperTableEntityDiff> helperTableEntityDiffs);
    }

    public interface IHelperTableDataAccess
    {
        void Commit();
        List<HelperTableEntity> GetHelperTableByNodeId(NodeId nodeId);
        List<HelperTableEntity> GetAllHelperTableEntities();
        List<ToolModelReferenceLink> GetHelperTableEntityModelLinks(long id, NodeId nodeId);
        List<ToolReferenceLink> GetHelperTableEntityToolLinks(long id, NodeId nodeId);
        List<HelperTableEntity> InsertHelperTableEntityWithHistory(List<HelperTableEntityDiff> helperTableEntityDiffs, bool returnList);
        List<HelperTableEntity> UpdateHelperTableEntityWithHistory(List<HelperTableEntityDiff> helperTableEntityDiffs);
    }

    public class HelperTableUseCase : IHelperTableUseCase
    {
        private readonly IHelperTableDataAccess _helperTableDataAccess;

        public HelperTableUseCase(IHelperTableDataAccess helperTableDataAccess)
        {
            _helperTableDataAccess = helperTableDataAccess;
        }

        public List<HelperTableEntity> GetHelperTableByNodeId(NodeId nodeId)
        {
            return _helperTableDataAccess.GetHelperTableByNodeId(nodeId);
        }

        public List<HelperTableEntity> GetAllHelperTableEntities()
        {
            return _helperTableDataAccess.GetAllHelperTableEntities();
        }

        public List<ToolModelReferenceLink> GetHelperTableEntityModelLinks(long id, NodeId nodeId)
        {
            return _helperTableDataAccess.GetHelperTableEntityModelLinks(id, nodeId);
        }

        public List<ToolReferenceLink> GetHelperTableEntityToolLinks(long id, NodeId nodeId)
        {
            return _helperTableDataAccess.GetHelperTableEntityToolLinks(id, nodeId);
        }

        public List<HelperTableEntity> InsertHelperTableEntityWithHistory(List<HelperTableEntityDiff> helperTableEntityDiffs, bool returnList)
        {
            var result = _helperTableDataAccess.InsertHelperTableEntityWithHistory(helperTableEntityDiffs, returnList);
            _helperTableDataAccess.Commit();
            return result;
        }

        public List<HelperTableEntity> UpdateHelperTableEntityWithHistory(List<HelperTableEntityDiff> helperTableEntityDiffs)
        {
            var result = _helperTableDataAccess.UpdateHelperTableEntityWithHistory(helperTableEntityDiffs);
            _helperTableDataAccess.Commit();
            return result;
        }
    }
}
