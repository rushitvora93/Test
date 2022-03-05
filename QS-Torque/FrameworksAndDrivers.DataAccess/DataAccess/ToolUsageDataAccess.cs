using System;
using System.Collections.Generic;
using System.Linq;
using Core.UseCases;
using FrameworksAndDrivers.DataAccess.Common;
using FrameworksAndDrivers.DataAccess.DbContext;
using FrameworksAndDrivers.DataAccess.DbEntities;
using FrameworksAndDrivers.DataAccess.T4Mapper;
using Server.Core.Diffs;
using Server.Core.Entities;
using Server.UseCases.UseCases;
using ToolUsage = Server.Core.Entities.ToolUsage;

namespace FrameworksAndDrivers.DataAccess.DataAccess
{
    public class ToolUsageDataAccess : DataAccessBase, IToolUsageDataAccess
    {
        private readonly IGlobalHistoryDataAccess _globalHistoryDataAccess;
        private readonly ITimeDataAccess _timeDataAccess;
        private readonly Mapper _mapper = new Mapper();

        public ToolUsageDataAccess(SqliteDbContext dbContext, ITransactionDbContext transactionContext,
            IGlobalHistoryDataAccess globalHistoryDataAccess, ITimeDataAccess timeDataAccess)
            : base(transactionContext, dbContext)
        {
            _globalHistoryDataAccess = globalHistoryDataAccess;
            _timeDataAccess = timeDataAccess;
        }

        public List<ToolUsage> GetAllToolUsages()
        {
            var dbToolUsages = _dbContext.ToolUsages.Where(x => x.ALIVE == true).OrderBy(m => m.POSNAME).ToList();
            var toolUsages = new List<ToolUsage>();

            dbToolUsages.ForEach(db => toolUsages.Add(_mapper.DirectPropertyMapping(db)));

            return toolUsages;
        }

        public List<long> GetToolUsageLocationToolAssignmentReferences(ToolUsageId id)
        {
            var locPowIds = _dbContext.LocPows
                .Where(x => x.PowPosId == id.ToLong() && x.Alive == true)
                .Select(x => x.LocPowId).ToList();
            return locPowIds;
        }

        public List<ToolUsage> InsertToolUsagesWithHistory(List<ToolUsageDiff> toolUsageDiffs, bool returnList)
        {
            var insertedToolUsages = new List<ToolUsage>();
            foreach (var toolUsageDiff in toolUsageDiffs)
            {
                InsertSingleToolUsageWithHistory(toolUsageDiff, insertedToolUsages);
            }

            return returnList ? insertedToolUsages : null;
        }

        private void InsertSingleToolUsageWithHistory(ToolUsageDiff toolUsageDiff, List<ToolUsage> insertedToolUsages)
        {
            var currentTimestamp = _timeDataAccess.UtcNow();
            var globalHistoryId = _globalHistoryDataAccess.CreateGlobalHistory("DATA INSERTED", currentTimestamp);
            AddSpecificToolUsageHistoryEntry(globalHistoryId);

            InsertSingleToolUsage(toolUsageDiff.GetNewToolUsage(), currentTimestamp, insertedToolUsages);

            var toolUsageChanges = CreateToolUsageChanges(globalHistoryId, toolUsageDiff);
            AddToolUsageChangesForInsert(toolUsageDiff, toolUsageChanges);

            _dbContext.SaveChanges();
        }

        private void InsertSingleToolUsage(ToolUsage toolUsage, DateTime currentTimestamp, List<ToolUsage> insertedToolUsages)
        {
            var dbToolUsage = _mapper.DirectPropertyMapping(toolUsage);
            dbToolUsage.ALIVE = true;
            dbToolUsage.TSN = currentTimestamp;
            dbToolUsage.TSA = currentTimestamp;
            _dbContext.ToolUsages.Add(dbToolUsage);
            _dbContext.SaveChanges();

            toolUsage.Id = new ToolUsageId(dbToolUsage.POSID);
            insertedToolUsages.Add(toolUsage);
        }

        private void AddToolUsageChangesForInsert(ToolUsageDiff toolUsageDiff, ToolUsageChanges changes)
        {
            var newToolUsage = toolUsageDiff.GetNewToolUsage();

            changes.ACTION = "INSERT";
            changes.ALIVEOLD = null;
            changes.ALIVENEW = true;

            changes.POSNAMEOLD = null;
            changes.POSNAMENEW = newToolUsage.Description.ToDefaultString();


            _dbContext.ToolUsageChanges.Add(changes);
        }

        public List<ToolUsage> UpdateToolUsagesWithHistory(List<ToolUsageDiff> toolUsageDiffs)
        {
            var leftOvers = new List<ToolUsage>();
            foreach (var toolUsageDiff in toolUsageDiffs)
            {
                var oldToolUsage = toolUsageDiff.GetOldToolUsage();
                var newToolUsage = toolUsageDiff.GetNewToolUsage();

                var itemToUpdate = _dbContext.ToolUsages.Find(newToolUsage.Id.ToLong());
                if (!ToolUsageUpdateWithHistoryPreconditions(itemToUpdate, oldToolUsage))
                {
                    AddUseStatistics();
                    ApplyCurrentServerStateToOldToolUsage(oldToolUsage, itemToUpdate);
                    leftOvers.Add(_mapper.DirectPropertyMapping(itemToUpdate));
                }
                UpdateSingleToolUsageWithHistory(toolUsageDiff, itemToUpdate);
            }

            return leftOvers;
        }

        private bool ToolUsageUpdateWithHistoryPreconditions(DbEntities.ToolUsage itemToUpdate, ToolUsage newToolUsage)
        {
            return
                itemToUpdate.POSNAME == newToolUsage.Description.ToDefaultString()
                && itemToUpdate.ALIVE == newToolUsage.Alive;
        }

        private void AddUseStatistics()
        {
            var usageStatistic = new UsageStatistic
            {
                ACTION = UsageStatisticActions.SaveCollision("PowPos"),
                TIMESTAMP = _timeDataAccess.UtcNow()
            };

            _dbContext.UsageStatistics.Add(usageStatistic);
            _dbContext.SaveChanges();
        }

        private void ApplyCurrentServerStateToOldToolUsage(ToolUsage oldToolUsage, DbEntities.ToolUsage itemToUpdate)
        {
            oldToolUsage.Description = new ToolUsageDescription(itemToUpdate.POSNAME);
            oldToolUsage.Alive = itemToUpdate.ALIVE.GetValueOrDefault(false);
        }

        private void UpdateSingleToolUsageWithHistory(ToolUsageDiff toolUsageDiff, DbEntities.ToolUsage toolUsageToUpdate)
        {
            var currentTimestamp = _timeDataAccess.UtcNow();
            var globalHistoryId = _globalHistoryDataAccess.CreateGlobalHistory("DATA UPDATED", currentTimestamp);
            AddSpecificToolUsageHistoryEntry(globalHistoryId);

            UpdateSingleToolUsage(toolUsageToUpdate, toolUsageDiff, currentTimestamp);

            var toolUsageChanges = CreateToolUsageChanges(globalHistoryId, toolUsageDiff);
            AddToolUsageChangesForUpdate(toolUsageDiff, toolUsageChanges);
            _dbContext.SaveChanges();
        }

        private void UpdateSingleToolUsage(DbEntities.ToolUsage toolUsageToUpdate, ToolUsageDiff toolUsageDiff, DateTime currentTimestamp)
        {
            UpdateDbToolUsageFromToolUsageEntity(toolUsageToUpdate, toolUsageDiff.GetNewToolUsage());
            toolUsageToUpdate.POSID = toolUsageToUpdate.POSID;
            toolUsageToUpdate.TSA = currentTimestamp;
        }

        private void UpdateDbToolUsageFromToolUsageEntity(DbEntities.ToolUsage dbToolUsage, ToolUsage toolUsageEntity)
        {
            dbToolUsage.POSNAME = toolUsageEntity.Description.ToDefaultString();
            dbToolUsage.ALIVE = toolUsageEntity.Alive;
        }

        private void AddToolUsageChangesForUpdate(ToolUsageDiff toolUsageDiff, ToolUsageChanges change)
        {
            var oldToolUsage = toolUsageDiff.GetOldToolUsage();
            var newToolUsage = toolUsageDiff.GetNewToolUsage();

            change.ACTION = "UPDATE";
            change.POSNAMEOLD = oldToolUsage.Description.ToDefaultString();
            change.POSNAMENEW = newToolUsage.Description.ToDefaultString();
            change.ALIVEOLD = oldToolUsage.Alive;
            change.ALIVENEW = newToolUsage.Alive;

            _dbContext.ToolUsageChanges.Add(change);
        }

        private ToolUsageChanges CreateToolUsageChanges(long globalHistoryId, ToolUsageDiff toolUsageDiff)
        {
            var newToolUsage = toolUsageDiff.GetNewToolUsage();

            var toolUsageChanges = new ToolUsageChanges()
            {
                GLOBALHISTORYID = globalHistoryId,
                TOOLUSAGEID = newToolUsage.Id.ToLong(),
                USERID = toolUsageDiff.GetUser().UserId.ToLong(),
                USERCOMMENT = toolUsageDiff.GetComment().ToDefaultString()
            };

            return toolUsageChanges;
        }

        private void AddSpecificToolUsageHistoryEntry(long globalHistoryId)
        {
            var toolUsageHistory = new ToolUsageHistory() { GLOBALHISTORYID = globalHistoryId };
            _dbContext.ToolUsageHistories.Add(toolUsageHistory);
        }
    }
}
