using System.Collections.Generic;
using Server.Core.Diffs;
using Server.Core.Entities;
using Server.Core.Entities.ReferenceLink;

namespace Server.UseCases.UseCases
{
    public interface IStatusUseCase
    {
        List<Status> LoadStatus();
        List<ToolReferenceLink> GetStatusToolLinks(StatusId statusId);
        List<Status> InsertStatusWithHistory(List<StatusDiff> statusDiffs, bool returnList);
        List<Status> UpdateStatusWithHistory(List<StatusDiff> statusDiffs);
    }

    public interface IStatusDataAccess
    {
        void Commit();
        List<Status> LoadStatus();
        List<ToolReferenceLink> GetStatusToolLinks(StatusId statusId);
        List<Status> InsertStatusWithHistory(List<StatusDiff> statusDiffs, bool returnList);
        List<Status> UpdateStatusWithHistory(List<StatusDiff> statusDiffs);
    }

    public class StatusUseCase : IStatusUseCase
    {
        private readonly IStatusDataAccess _statusDataAccess;

        public StatusUseCase(IStatusDataAccess statusDataAccess)
        {
            _statusDataAccess = statusDataAccess;
        }

        public List<Status> LoadStatus()
        {
            return _statusDataAccess.LoadStatus();
        }

        public List<ToolReferenceLink> GetStatusToolLinks(StatusId statusId)
        {
            return _statusDataAccess.GetStatusToolLinks(statusId);
        }

        public List<Status> InsertStatusWithHistory(List<StatusDiff> statusDiffs, bool returnList)
        {
            var stateIds = _statusDataAccess.InsertStatusWithHistory(statusDiffs, returnList);
            _statusDataAccess.Commit();
            return stateIds;
        }

        public List<Status> UpdateStatusWithHistory(List<StatusDiff> statusDiffs)
        {
            var stateIds = _statusDataAccess.UpdateStatusWithHistory(statusDiffs);
            _statusDataAccess.Commit();
            return stateIds;
        }
    }
}
