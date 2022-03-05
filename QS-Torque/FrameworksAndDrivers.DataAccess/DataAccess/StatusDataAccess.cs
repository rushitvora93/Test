using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.UseCases;
using FrameworksAndDrivers.DataAccess.Common;
using FrameworksAndDrivers.DataAccess.DbContext;
using FrameworksAndDrivers.DataAccess.DbEntities;
using FrameworksAndDrivers.DataAccess.T4Mapper;
using Server.Core.Diffs;
using Server.Core.Entities;
using Server.Core.Entities.ReferenceLink;
using Server.UseCases.UseCases;

namespace FrameworksAndDrivers.DataAccess.DataAccess
{
    public class StatusDataAccess : DataAccessBase, IStatusDataAccess
    {
        private readonly IGlobalHistoryDataAccess _globalHistoryDataAccess;
        private readonly ITimeDataAccess _timeDataAccess;
        private readonly Mapper _mapper = new Mapper();

        public StatusDataAccess(SqliteDbContext dbContext, ITransactionDbContext transactionContext,
            IGlobalHistoryDataAccess globalHistoryDataAccess, ITimeDataAccess timeDataAccess)
            : base(transactionContext, dbContext)
        {
            _globalHistoryDataAccess = globalHistoryDataAccess;
            _timeDataAccess = timeDataAccess;
        }

        public List<Server.Core.Entities.Status> LoadStatus()
        {
            var dbStatus = _dbContext.Status.Where(x => x.ALIVE == true).ToList();
            var status = new List<Server.Core.Entities.Status>();

            dbStatus.ForEach(db => status.Add(_mapper.DirectPropertyMapping(db)));

            return status;
        }

        public List<ToolReferenceLink> GetStatusToolLinks(StatusId statusId)
        {
            var toolLinkDtos = _dbContext.Tools.Where(x => x.STATEID == statusId.ToLong() && x.ALIVE == true).ToList();
            var references = new List<ToolReferenceLink>();
            toolLinkDtos.ForEach(db => references.Add(new ToolReferenceLink(new QstIdentifier(db.SEQID), db.USERNO, db.SERIALNO)));

            return references;
        }

        public List<Server.Core.Entities.Status> InsertStatusWithHistory(List<StatusDiff> statusDiffs, bool returnList)
        {
            var insertedStatus = new List<Server.Core.Entities.Status>();
            foreach (var statusDiff in statusDiffs)
            {
                InsertSingleStatusWithHistory(statusDiff, insertedStatus);
            }

            return returnList ? insertedStatus : null;
        }

        private void InsertSingleStatusWithHistory(StatusDiff statusDiff, List<Server.Core.Entities.Status> insertedStatus)
        {
            var currentTimestamp = _timeDataAccess.UtcNow();
            var globalHistoryId = _globalHistoryDataAccess.CreateGlobalHistory("DATA INSERTED", currentTimestamp);
            AddSpecificStatusHistoryEntry(globalHistoryId);

            InsertSingleStatus(statusDiff.GetNewStatus(), currentTimestamp, insertedStatus);

            var statusChanges = CreateStatusChanges(globalHistoryId, statusDiff);
            AddStatusChangesForInsert(statusDiff, statusChanges);

            _dbContext.SaveChanges();
        }

        private void InsertSingleStatus(Server.Core.Entities.Status status, DateTime currentTimestamp, List<Server.Core.Entities.Status> insertedStatus)
        {
            var dbStatus = _mapper.DirectPropertyMapping(status);
            dbStatus.ALIVE = true;
            dbStatus.TSN = currentTimestamp;
            dbStatus.TSA = currentTimestamp;
            _dbContext.Status.Add(dbStatus);
            _dbContext.SaveChanges();

            status.Id = new StatusId(dbStatus.STATEID);
            insertedStatus.Add(status);
        }

        private void AddStatusChangesForInsert(StatusDiff statusDiff, StatusChanges changes)
        {
            var newStatus = statusDiff.GetNewStatus();

            changes.ACTION = "INSERT";
            changes.ALIVEOLD = null;
            changes.ALIVENEW = true;

            changes.STATEOLD = null;
            changes.STATENEW = newStatus.Value.ToDefaultString();

            _dbContext.StatusChanges.Add(changes);
        }

        public List<Server.Core.Entities.Status> UpdateStatusWithHistory(List<StatusDiff> statusDiffs)
        {
            var leftOvers = new List<Server.Core.Entities.Status>();
            foreach (var statusDiff in statusDiffs)
            {
                var oldStatus = statusDiff.GetOldStatus();
                var newStatus = statusDiff.GetNewStatus();

                var itemToUpdate = _dbContext.Status.Find(newStatus.Id.ToLong());
                if (!StatusUpdateWithHistoryPreconditions(itemToUpdate, oldStatus))
                {
                    AddUseStatistics();
                    ApplyCurrentServerStatusToOldStatus(oldStatus, itemToUpdate);
                    leftOvers.Add(_mapper.DirectPropertyMapping(itemToUpdate));
                }
                UpdateSingleStatusWithHistory(statusDiff, itemToUpdate);
            }

            return leftOvers;
        }

        private bool StatusUpdateWithHistoryPreconditions(DbEntities.Status itemToUpdate, Server.Core.Entities.Status newStatus)
        {
            return itemToUpdate.STATE == newStatus.Value.ToDefaultString()
                && itemToUpdate.ALIVE == newStatus.Alive;
        }

        private void AddUseStatistics()
        {
            var usageStatistic = new UsageStatistic
            {
                ACTION = UsageStatisticActions.SaveCollision("StateId"),
                TIMESTAMP = _timeDataAccess.UtcNow()
            };

            _dbContext.UsageStatistics.Add(usageStatistic);
            _dbContext.SaveChanges();
        }

        private void ApplyCurrentServerStatusToOldStatus(Server.Core.Entities.Status oldStatus, DbEntities.Status itemToUpdate)
        {
            oldStatus.Value = new StatusDescription(itemToUpdate.STATE);
            oldStatus.Alive = itemToUpdate.ALIVE.GetValueOrDefault(false);
        }

        private void UpdateSingleStatusWithHistory(StatusDiff statusDiff, DbEntities.Status statusToUpdate)
        {
            var currentTimestamp = _timeDataAccess.UtcNow();
            var globalHistoryId = _globalHistoryDataAccess.CreateGlobalHistory("DATA UPDATED", currentTimestamp);
            AddSpecificStatusHistoryEntry(globalHistoryId);

            UpdateSingleStatus(statusToUpdate, statusDiff, currentTimestamp);

            var statusChanges = CreateStatusChanges(globalHistoryId, statusDiff);
            AddStatusChangesForUpdate(statusDiff, statusChanges);
            _dbContext.SaveChanges();
        }

        private void UpdateSingleStatus(DbEntities.Status statusToUpdate, StatusDiff statusDiff, DateTime currentTimestamp)
        {
            statusToUpdate.STATE = statusDiff.GetNewStatus().Value.ToDefaultString();
            statusToUpdate.ALIVE = statusDiff.GetNewStatus().Alive;
            statusToUpdate.TSA = currentTimestamp;
        }

        private void AddStatusChangesForUpdate(StatusDiff statusDiff, StatusChanges change)
        {
            var oldStatus = statusDiff.GetOldStatus();
            var newStatus = statusDiff.GetNewStatus();

            change.ACTION = "UPDATE";
            change.STATEOLD = oldStatus.Value.ToDefaultString();
            change.STATENEW = newStatus.Value.ToDefaultString();
            change.ALIVEOLD = oldStatus.Alive;
            change.ALIVENEW = newStatus.Alive;

            _dbContext.StatusChanges.Add(change);
        }

        private StatusChanges CreateStatusChanges(long globalHistoryId, StatusDiff statusDiff)
        {
            var newStatus = statusDiff.GetNewStatus();

            var statusChanges = new StatusChanges()
            {
                GLOBALHISTORYID = globalHistoryId,
                STATEID = newStatus.Id.ToLong(),
                USERID = statusDiff.GetUser().UserId.ToLong(),
                USERCOMMENT = statusDiff.GetComment().ToDefaultString()
            };

            return statusChanges;
        }

        private void AddSpecificStatusHistoryEntry(long globalHistoryId)
        {
            var statusHistory = new StatusHistory() { GLOBALHISTORYID = globalHistoryId };
            _dbContext.StatusHistories.Add(statusHistory);
        }
    }
}