using System.Collections.Generic;
using Server.Core.Diffs;
using Server.Core.Entities;

namespace Server.UseCases.UseCases
{
    public interface IToolUsageUseCase
    {
        List<ToolUsage> GetAllToolUsages();
        List<ToolUsage> InsertToolUsagesWithHistory(List<ToolUsageDiff> toolUsagesDiff, bool returnList);
        List<ToolUsage> UpdateToolUsagesWithHistory(List<ToolUsageDiff> toolUsagesDiff);
        List<long> GetToolUsageLocationToolAssignmentReferences(ToolUsageId id);
    }

    public interface IToolUsageDataAccess
    {
        void Commit();
        List<ToolUsage> GetAllToolUsages();
        List<ToolUsage> InsertToolUsagesWithHistory(List<ToolUsageDiff> toolUsagesDiff, bool returnList);
        List<ToolUsage> UpdateToolUsagesWithHistory(List<ToolUsageDiff> toolUsagesDiff);
        List<long> GetToolUsageLocationToolAssignmentReferences(ToolUsageId id);
    }

    public class ToolUsageUseCase : IToolUsageUseCase
    {
        private readonly IToolUsageDataAccess _toolUsageDataAccess;

        public ToolUsageUseCase(IToolUsageDataAccess toolUsageDataAccess)
        {
            _toolUsageDataAccess = toolUsageDataAccess;
        }

        public List<ToolUsage> GetAllToolUsages()
        {
            return _toolUsageDataAccess.GetAllToolUsages();
        }

        public List<ToolUsage> InsertToolUsagesWithHistory(List<ToolUsageDiff> toolUsagesDiff, bool returnList)
        {
            var entities = _toolUsageDataAccess.InsertToolUsagesWithHistory(toolUsagesDiff, returnList);
            _toolUsageDataAccess.Commit();
            return entities;
        }

        public List<ToolUsage> UpdateToolUsagesWithHistory(List<ToolUsageDiff> toolUsagesDiff)
        {
            var entities = _toolUsageDataAccess.UpdateToolUsagesWithHistory(toolUsagesDiff);
            _toolUsageDataAccess.Commit();
            return entities;
        }

        public List<long> GetToolUsageLocationToolAssignmentReferences(ToolUsageId id)
        {
            return _toolUsageDataAccess.GetToolUsageLocationToolAssignmentReferences(id);
        }
    }
}
