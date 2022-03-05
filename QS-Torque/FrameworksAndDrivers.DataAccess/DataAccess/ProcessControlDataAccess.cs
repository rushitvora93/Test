using Core.UseCases;
using FrameworksAndDrivers.DataAccess.Common;
using FrameworksAndDrivers.DataAccess.DbContext;
using FrameworksAndDrivers.DataAccess.DbEntities;
using FrameworksAndDrivers.DataAccess.T4Mapper;
using Server.Core.Diffs;
using Server.Core.Entities;
using Server.UseCases.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using Common.Types.Enums;
using Microsoft.EntityFrameworkCore;
using Server.Core.Enums;

namespace FrameworksAndDrivers.DataAccess.DataAccess
{
    public class ProcessControlDataAccess : DataAccessBase, IProcessControlDataAccess
    {
        private readonly IGlobalHistoryDataAccess _globalHistoryDataAccess;
        private readonly ILocationDataAccess _locationDataAccess;
        private readonly ITimeDataAccess _timeDataAccess;
        private readonly Mapper _mapper = new Mapper();

        public ProcessControlDataAccess(SqliteDbContext dbContext, 
            ITransactionDbContext transactionContext,
            IGlobalHistoryDataAccess globalHistoryDataAccess, 
            ILocationDataAccess locationDataAccess,
            ITimeDataAccess timeDataAccess)
            : base(transactionContext, dbContext)
        {
            _globalHistoryDataAccess = globalHistoryDataAccess;
            _timeDataAccess = timeDataAccess;
            _locationDataAccess = locationDataAccess;
        }

        public List<ProcessControlCondition> UpdateProcessControlConditionsWithHistory(List<ProcessControlConditionDiff> processControlConditionDiffs)
        {
            var leftOvers = new List<ProcessControlCondition>();
            foreach (var processControlConditionDiff in processControlConditionDiffs)
            {
                var oldCondition = processControlConditionDiff.GetOldProcessControlCondition();
                var newCondition = processControlConditionDiff.GetNewProcessControlCondition();

                var itemToUpdate = _dbContext.CondLocs.Find(newCondition.Id.ToLong());
                if (!CondLocUpdateWithHistoryPreconditions(itemToUpdate, oldCondition))
                {
                    AddUseStatistics();
                    ApplyCurrentServerStateToOldCondLoc(oldCondition, itemToUpdate);
                    var leftOver = _mapper.DirectPropertyMapping(itemToUpdate);
                    leftOver.Location = _locationDataAccess.LoadLocationsByIds(new List<LocationId>() { new LocationId(itemToUpdate.LOCID) }).First();
                    leftOvers.Add(leftOver);
                }
                UpdateSingleCondLocWithHistory(processControlConditionDiff, itemToUpdate);
            }

            return leftOvers;
        }

        public ProcessControlCondition LoadProcessControlConditionForLocation(LocationId locationId)
        {
            var processControlDb =_dbContext.CondLocs
                .SingleOrDefault(x => x.LOCID == locationId.ToLong() && x.TYPEID == 0 && x.ALIVE);

            if (processControlDb == null)
            {
                return null;
            }

            var processControl = _mapper.DirectPropertyMapping(processControlDb);
            processControl.Location = _locationDataAccess.LoadLocationsByIds(new List<LocationId>() { locationId }).First();

            var testLevelSet = _dbContext.TestLevelSets.FirstOrDefault(x => x.Id == processControlDb.TESTLEVELSETID);
            if (testLevelSet != null)
            {
                processControl.TestLevelSet = _mapper.DirectPropertyMapping(testLevelSet);
                var testLevel1 = _dbContext.TestLevels.FirstOrDefault(x => x.TestLevelSetId == testLevelSet.Id && x.TestLevelNumber == 1);
                var testLevel2 = _dbContext.TestLevels.FirstOrDefault(x => x.TestLevelSetId == testLevelSet.Id && x.TestLevelNumber == 2);
                var testLevel3 = _dbContext.TestLevels.FirstOrDefault(x => x.TestLevelSetId == testLevelSet.Id && x.TestLevelNumber == 3);
                processControl.TestLevelSet.TestLevel1 = testLevel1 != null ? _mapper.DirectPropertyMapping(testLevel1) : null;
                processControl.TestLevelSet.TestLevel2 = testLevel1 != null ? _mapper.DirectPropertyMapping(testLevel2) : null;
                processControl.TestLevelSet.TestLevel3 = testLevel1 != null ? _mapper.DirectPropertyMapping(testLevel3) : null;
            }

            var condLocTech = _dbContext.CondLocTechs.Include(x => x.Extension).SingleOrDefault(x =>
                x.CONDLOCID == processControl.Id.ToLong() && x.HERSTELLERID == ManufacturerIds.ID_QST && x.ALIVE);

            processControl.ProcessControlTech = CondLocTechConverter.ConvertCondLocTechToEntity(condLocTech);

            return processControl;
        }

        public List<ProcessControlCondition> InsertProcessControlConditionsWithHistory(List<ProcessControlConditionDiff> diffs, bool returnList)
        {
            var insertedProcessControlConditions = new List<ProcessControlCondition>();
            foreach (var processControlConditionDiff in diffs)
            {
                InsertSingleProcessControlConditionWithHistory(processControlConditionDiff, insertedProcessControlConditions);
            }

            return returnList ? insertedProcessControlConditions : null;
        }

 
        private void InsertSingleProcessControlConditionWithHistory(ProcessControlConditionDiff processControlConditionDiff, List<ProcessControlCondition> insertedProcessControlConditions)
        {
            var currentTimestamp = _timeDataAccess.UtcNow();
            var globalHistoryId = _globalHistoryDataAccess.CreateGlobalHistory("DATA INSERTED", currentTimestamp);
            AddSpecificCondLocHistoryEntry(globalHistoryId);

            InsertSingleProcessControlCondition(processControlConditionDiff.GetNewProcessControlCondition(), currentTimestamp, insertedProcessControlConditions);

            var processControlConditionChanges = CreateCondLocChanges(globalHistoryId, processControlConditionDiff);
            AddProcessControlConditionChangesForInsert(processControlConditionDiff, processControlConditionChanges);

            _dbContext.SaveChanges();
        }

        private void InsertSingleProcessControlCondition(ProcessControlCondition processControlCondition, DateTime currentTimestamp, List<ProcessControlCondition> insertedProcessControlConditions)
        {
            var dbProcessControlCondition = _mapper.DirectPropertyMapping(processControlCondition);
            dbProcessControlCondition.ALIVE = true;
            dbProcessControlCondition.TSN = currentTimestamp;
            dbProcessControlCondition.TSA = currentTimestamp;
            _dbContext.CondLocs.Add(dbProcessControlCondition);
            _dbContext.SaveChanges();

            processControlCondition.Id = new ProcessControlConditionId(dbProcessControlCondition.SEQID);
            if (processControlCondition.ProcessControlTech != null)
            {
                processControlCondition.ProcessControlTech.ProcessControlConditionId = new ProcessControlConditionId(processControlCondition.Id.ToLong());
            }
            insertedProcessControlConditions.Add(processControlCondition);
        }

        private void AddProcessControlConditionChangesForInsert(ProcessControlConditionDiff processControlConditionDiff, CondLocChanges changes)
        {
            var newProcessControlCondition = processControlConditionDiff.GetNewProcessControlCondition();

            changes.ACTION = "INSERT";
            changes.ALIVEOLD = null;
            changes.ALIVENEW = true;
            changes.LOCIDNEW = newProcessControlCondition.Location.Id.ToLong();
            changes.MDMINNEW = newProcessControlCondition.LowerMeasuringLimit;
            changes.MDMAXNEW = newProcessControlCondition.UpperMeasuringLimit;
            changes.OEGNNEW = newProcessControlCondition.UpperInterventionLimit;
            changes.UEGNNEW = newProcessControlCondition.LowerInterventionLimit;
            changes.TESTLEVELNUMBERNEW = newProcessControlCondition.TestLevelNumber;
            changes.TESTLEVELSETIDNEW = newProcessControlCondition.TestLevelSet?.Id?.ToLong();
            changes.TESTSTARTNEW = newProcessControlCondition.StartDate;
            changes.PLANOKNEW = newProcessControlCondition.TestOperationActive;

            _dbContext.CondLocChanges.Add(changes);
        }

        private void UpdateSingleCondLocWithHistory(ProcessControlConditionDiff processControlConditionDiff, CondLoc condLocToUpdate)
        {
            var currentTimestamp = _timeDataAccess.UtcNow();
            var globalHistoryId = _globalHistoryDataAccess.CreateGlobalHistory("DATA UPDATED", currentTimestamp);
            AddSpecificCondLocHistoryEntry(globalHistoryId);

            UpdateSingleCondLoc(condLocToUpdate, processControlConditionDiff, currentTimestamp);

            var processControlConditionChanges = CreateCondLocChanges(globalHistoryId, processControlConditionDiff);
            AddCondLocChangesForUpdate(processControlConditionDiff, processControlConditionChanges);
            _dbContext.SaveChanges();
        }

        private void AddCondLocChangesForUpdate(ProcessControlConditionDiff processControlConditionDiff, CondLocChanges change)
        {
            var oldCondition = processControlConditionDiff.GetOldProcessControlCondition();
            var newCondition = processControlConditionDiff.GetNewProcessControlCondition();

            change.ACTION = "UPDATE";
            change.LOCIDOLD = oldCondition.Location.Id.ToLong();
            change.LOCIDNEW = newCondition.Location.Id.ToLong();
            change.MDMINOLD = oldCondition.LowerMeasuringLimit;
            change.MDMINNEW = newCondition.LowerMeasuringLimit;
            change.MDMAXOLD = oldCondition.UpperMeasuringLimit;
            change.MDMAXNEW = newCondition.UpperMeasuringLimit;
            change.UEGNOLD = oldCondition.LowerInterventionLimit;
            change.UEGNNEW = newCondition.LowerInterventionLimit;
            change.OEGNOLD = oldCondition.UpperInterventionLimit;
            change.OEGNNEW = newCondition.UpperInterventionLimit;
            change.TESTLEVELSETIDOLD = oldCondition.TestLevelSet?.Id?.ToLong();
            change.TESTLEVELSETIDNEW = newCondition.TestLevelSet?.Id?.ToLong();
            change.TESTLEVELNUMBEROLD = oldCondition.TestLevelNumber;
            change.TESTLEVELNUMBERNEW = newCondition.TestLevelNumber;
            change.TESTSTARTOLD = oldCondition.StartDate;
            change.TESTSTARTNEW = newCondition.StartDate;
            change.PLANOKOLD = oldCondition.TestOperationActive;
            change.PLANOKNEW = newCondition.TestOperationActive;
            change.ALIVEOLD = oldCondition.Alive;
            change.ALIVENEW = newCondition.Alive;

            _dbContext.CondLocChanges.Add(change);
        }

        private CondLocChanges CreateCondLocChanges(long globalHistoryId, ProcessControlConditionDiff processControlConditionDiff)
        {
            var newProcessControlCondition = processControlConditionDiff.GetNewProcessControlCondition();

            var condLocChanges = new CondLocChanges()
            {
                GLOBALHISTORYID = globalHistoryId,
                CONDLOCID = newProcessControlCondition.Id.ToLong(),
                USERID = processControlConditionDiff.GetUser().UserId.ToLong(),
                USERCOMMENT = processControlConditionDiff.GetComment().ToDefaultString()
            };

            return condLocChanges;
        }

        private void UpdateSingleCondLoc(CondLoc condLocToUpdate, ProcessControlConditionDiff processControlConditionDiff, DateTime currentTimestamp)
        {
            UpdateDbCondLocFromCondLocEntity(condLocToUpdate, processControlConditionDiff.GetNewProcessControlCondition());
            condLocToUpdate.LOCID = condLocToUpdate.LOCID;
            condLocToUpdate.TSA = currentTimestamp;
        }

        private void UpdateDbCondLocFromCondLocEntity(CondLoc dbCondLoc, ProcessControlCondition processControlConditionEntity)
        {
            dbCondLoc.LOCID = processControlConditionEntity.Location.Id.ToLong();
            dbCondLoc.MDMIN = processControlConditionEntity.LowerMeasuringLimit;
            dbCondLoc.MDMAX = processControlConditionEntity.UpperMeasuringLimit;
            dbCondLoc.UEGN = processControlConditionEntity.LowerInterventionLimit;
            dbCondLoc.OEGN = processControlConditionEntity.UpperInterventionLimit;
            dbCondLoc.TESTLEVELSETID = processControlConditionEntity.TestLevelSet?.Id?.ToLong();
            dbCondLoc.TESTLEVELNUMBER = processControlConditionEntity.TestLevelNumber;
            dbCondLoc.PLANOK = processControlConditionEntity.TestOperationActive;
            dbCondLoc.TESTSTART = processControlConditionEntity.StartDate;
            dbCondLoc.ALIVE = processControlConditionEntity.Alive;
        }

        private void AddSpecificCondLocHistoryEntry(long globalHistoryId)
        {
            var condLocHistory = new CondLocHistory { GLOBALHISTORYID = globalHistoryId };
            _dbContext.CondLocHistories.Add(condLocHistory);
        }

        private void ApplyCurrentServerStateToOldCondLoc(ProcessControlCondition oldCondition, CondLoc itemToUpdate)
        {
            oldCondition.Location = _locationDataAccess.LoadLocationsByIds(new List<LocationId>() { new LocationId(itemToUpdate.LOCID) }).First();
            oldCondition.LowerMeasuringLimit = itemToUpdate.MDMIN;
            oldCondition.UpperMeasuringLimit = itemToUpdate.MDMAX;
            oldCondition.LowerInterventionLimit = itemToUpdate.UEGN;
            oldCondition.UpperInterventionLimit = itemToUpdate.OEGN;
            oldCondition.TestLevelSet = itemToUpdate.TESTLEVELSETID == null
                ? null
                : new Server.Core.Entities.TestLevelSet()
                {
                    Id = new TestLevelSetId(itemToUpdate.TESTLEVELSETID.Value)
                };
            oldCondition.TestLevelNumber = itemToUpdate.TESTLEVELNUMBER;
            oldCondition.Alive = itemToUpdate.ALIVE;
            oldCondition.StartDate = itemToUpdate.TESTSTART;
            oldCondition.TestOperationActive = itemToUpdate.PLANOK.GetValueOrDefault(false);
        }

        private void AddUseStatistics()
        {
            var usageStatistic = new UsageStatistic
            {
                ACTION = UsageStatisticActions.SaveCollision("CondLoc"),
                TIMESTAMP = _timeDataAccess.UtcNow()
            };

            _dbContext.UsageStatistics.Add(usageStatistic);
            _dbContext.SaveChanges();
        }

        private bool CondLocUpdateWithHistoryPreconditions(CondLoc itemToUpdate, ProcessControlCondition oldCondition)
        {
            return
                itemToUpdate.LOCID == oldCondition.Location.Id.ToLong()
                && itemToUpdate.MDMIN == oldCondition.LowerMeasuringLimit
                && itemToUpdate.MDMAX == oldCondition.UpperMeasuringLimit
                && itemToUpdate.UEGN == oldCondition.LowerInterventionLimit
                && itemToUpdate.OEGN == oldCondition.UpperInterventionLimit
                && itemToUpdate.TESTLEVELSETID == oldCondition.TestLevelSet?.Id?.ToLong()
                && itemToUpdate.TESTLEVELNUMBER == oldCondition.TestLevelNumber
                && itemToUpdate.PLANOK == oldCondition.TestOperationActive
                && itemToUpdate.TESTSTART == oldCondition.StartDate
                && itemToUpdate.ALIVE == oldCondition.Alive;
        }

        public List<ProcessControlCondition> LoadProcessControlConditions()
        {
            var dbEntites = _dbContext.CondLocs.Where(x => x.ALIVE);

            if (dbEntites == null || !dbEntites.Any())
            {
                return new List<ProcessControlCondition>();
            }

            var entities = new List<ProcessControlCondition>();

            foreach (var dbEntity in dbEntites)
            {
                entities.Add(MapDbToEntity(dbEntity));
            }

            return entities;
        }

        public List<ProcessControlConditionId> GetProcessControlConditionIdsForTestLevelSet(TestLevelSetId id)
        {
            return _dbContext.CondLocs.Where(x => x.ALIVE == true && x.TESTLEVELSETID == id.ToLong())
                .Select(x => new ProcessControlConditionId(x.SEQID)).ToList();
        }

        public ProcessControlCondition GetProcessControlConditionById(ProcessControlConditionId id)
        {
            var condLoc = _dbContext.CondLocs.FirstOrDefault(x => x.ALIVE == true && x.SEQID == id.ToLong());
            return MapDbToEntity(condLoc);
        }

        private ProcessControlCondition MapDbToEntity(DbEntities.CondLoc dbEntity)
        {
            var processControl = _mapper.DirectPropertyMapping(dbEntity);
            processControl.Location = _locationDataAccess.LoadLocationsByIds(new List<LocationId>() { new LocationId(dbEntity.LOCID) }).First();

            var testLevelSet = _dbContext.TestLevelSets.FirstOrDefault(x => x.Id == dbEntity.TESTLEVELSETID);
            if (testLevelSet != null)
            {
                processControl.TestLevelSet = _mapper.DirectPropertyMapping(testLevelSet);
                var testLevel1 = _dbContext.TestLevels.FirstOrDefault(x => x.TestLevelSetId == testLevelSet.Id && x.TestLevelNumber == 1);
                var testLevel2 = _dbContext.TestLevels.FirstOrDefault(x => x.TestLevelSetId == testLevelSet.Id && x.TestLevelNumber == 2);
                var testLevel3 = _dbContext.TestLevels.FirstOrDefault(x => x.TestLevelSetId == testLevelSet.Id && x.TestLevelNumber == 3);
                processControl.TestLevelSet.TestLevel1 = testLevel1 != null ? _mapper.DirectPropertyMapping(testLevel1) : null;
                processControl.TestLevelSet.TestLevel2 = testLevel1 != null ? _mapper.DirectPropertyMapping(testLevel2) : null;
                processControl.TestLevelSet.TestLevel3 = testLevel1 != null ? _mapper.DirectPropertyMapping(testLevel3) : null;
            }

            var condLocTech = _dbContext.CondLocTechs.Include(x => x.Extension).SingleOrDefault(x =>
                x.CONDLOCID == processControl.Id.ToLong() && x.HERSTELLERID == ManufacturerIds.ID_QST && x.ALIVE);

            processControl.ProcessControlTech = CondLocTechConverter.ConvertCondLocTechToEntity(condLocTech);
            return processControl;
        }

        public void SaveNextTestDatesFor(ProcessControlConditionId id, DateTime? nextTestDate, Shift? nextTestShift,
            DateTime? endOfLastTestPeriod, Shift? endOfLastTestPeriodShift)
        {
            var condLoc = _dbContext.CondLocs.FirstOrDefault(x => x.SEQID == id.ToLong());

            if (condLoc != null)
            {
                condLoc.NEXT_CTL = nextTestDate;
                condLoc.NEXTSHIFT = (long?)nextTestShift;
                condLoc.ENDOFLASTTESTPERIOD = endOfLastTestPeriod;
                condLoc.ENDOFLASTTESTPERIODSHIFT = (long?)endOfLastTestPeriodShift;
                _dbContext.CondLocs.Update(condLoc);
                _dbContext.SaveChanges();
            }
        }
    }
}
