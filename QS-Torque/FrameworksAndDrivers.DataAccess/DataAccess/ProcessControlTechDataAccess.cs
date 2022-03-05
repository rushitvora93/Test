using System;
using System.Collections.Generic;
using Core.UseCases;
using FrameworksAndDrivers.DataAccess.Common;
using FrameworksAndDrivers.DataAccess.DbContext;
using FrameworksAndDrivers.DataAccess.DbEntities;
using Server.Core.Diffs;
using Server.Core.Entities;
using Server.UseCases.UseCases;

namespace FrameworksAndDrivers.DataAccess.DataAccess
{
    public class ProcessControlTechDataAccess : DataAccessBase, IProcessControlTechDataAccess
    {
        private readonly IGlobalHistoryDataAccess _globalHistoryDataAccess;
        private readonly ITimeDataAccess _timeDataAccess;

        public ProcessControlTechDataAccess(SqliteDbContext dbContext,
            ITransactionDbContext transactionContext,
            IGlobalHistoryDataAccess globalHistoryDataAccess,
            ITimeDataAccess timeDataAccess)
            : base(transactionContext, dbContext)
        {
            _globalHistoryDataAccess = globalHistoryDataAccess;
            _timeDataAccess = timeDataAccess;
        }

        public void UpdateProcessControlTechsWithHistory(List<ProcessControlTechDiff> processControlTechDiffs)
        {
            foreach (var processControlTechDiff in processControlTechDiffs)
            {
                var oldTech = CondLocTechConverter.ConvertEntityToCondLocTech(processControlTechDiff.GetOldProcessControlTech());
                var newTech = CondLocTechConverter.ConvertEntityToCondLocTech(processControlTechDiff.GetNewProcessControlTech());

                var itemToUpdate = _dbContext.CondLocTechs.Find(newTech.SEQID);
                if (!CondLocTechUpdateWithHistoryPreconditions(itemToUpdate, oldTech))
                {
                    AddUseStatistics();
                    oldTech = itemToUpdate;
                }
                UpdateSingleCondLocTechWithHistory(oldTech, newTech, processControlTechDiff, itemToUpdate);
            }
        }

        private void UpdateSingleCondLocTechWithHistory(CondLocTech oldTech, CondLocTech newTech, ProcessControlTechDiff processControlTechDiff, CondLocTech itemToUpdate)
        {
            var currentTimestamp = _timeDataAccess.UtcNow();
            var globalHistoryId = _globalHistoryDataAccess.CreateGlobalHistory("DATA UPDATED", currentTimestamp);
            AddSpecificCondLocTechHistoryEntry(globalHistoryId);

            UpdateSingleCondLocTech(itemToUpdate, newTech);

            var processControlConditionChanges = CreateCondLocTechChanges(globalHistoryId, processControlTechDiff);
            AddCondLocTechChangesForUpdate(oldTech, newTech, processControlConditionChanges);
            _dbContext.SaveChanges();
        }

        private void AddCondLocTechChangesForUpdate(CondLocTech oldTech, CondLocTech newTech, CondLocTechChanges changes)
        {
            changes.ACTION = "UPDATE";
            changes.ALIVEOLD = oldTech.ALIVE;
            changes.ALIVENEW = newTech.ALIVE;
            changes.HERSTELLERIDOLD = oldTech.HERSTELLERID;
            changes.HERSTELLERIDNEW = newTech.HERSTELLERID;
            changes.METHODEOLD = oldTech.METHODE;
            changes.METHODENEW = newTech.METHODE;
            changes.EXTENSIONIDOLD = oldTech.EXTENSIONID;
            changes.EXTENSIONIDNEW = newTech.EXTENSIONID;
            changes.F0OLD = oldTech.F0;
            changes.F0NEW = newTech.F0;
            changes.F1OLD = oldTech.F1;
            changes.F1NEW = newTech.F1;
            changes.F2OLD = oldTech.F2;
            changes.F2NEW = newTech.F2;
            changes.F3OLD = oldTech.F3;
            changes.F3NEW = newTech.F3;
            changes.F4OLD = oldTech.F4;
            changes.F4NEW = newTech.F4;
            changes.F5OLD = oldTech.F5;
            changes.F5NEW = newTech.F5;
            changes.F6OLD = oldTech.F6;
            changes.F6NEW = newTech.F6;
            changes.F7OLD = oldTech.F7;
            changes.F7NEW = newTech.F7;
            changes.F8OLD = oldTech.F8;
            changes.F8NEW = newTech.F8;
            changes.F9OLD = oldTech.F9;
            changes.F9NEW = newTech.F9;
            changes.F10OLD = oldTech.F10;
            changes.F10NEW = newTech.F10;
            changes.F11OLD = oldTech.F1;
            changes.F11NEW = newTech.F1;
            changes.F12OLD = oldTech.F12;
            changes.F12NEW = newTech.F12;
            changes.F13OLD = oldTech.F13;
            changes.F13NEW = newTech.F13;
            changes.F14OLD = oldTech.F14;
            changes.F14NEW = newTech.F14;
            changes.F15OLD = oldTech.F15;
            changes.F15NEW = newTech.F15;
            changes.F16OLD = oldTech.F16;
            changes.F16NEW = newTech.F16;
            changes.F17OLD = oldTech.F17;
            changes.F17NEW = newTech.F17;
            changes.F18OLD = oldTech.F18;
            changes.F18NEW = newTech.F18;
            changes.F19OLD = oldTech.F19;
            changes.F19NEW = newTech.F19;
            changes.F20OLD = oldTech.F20;
            changes.F20NEW = newTech.F20;
            changes.F21OLD = oldTech.F21;
            changes.F21NEW = newTech.F21;
            changes.F22OLD = oldTech.F22;
            changes.F22NEW = newTech.F22;
            changes.F23OLD = oldTech.F23;
            changes.F23NEW = newTech.F23;
            changes.F24OLD = oldTech.F24;
            changes.F24NEW = newTech.F24;
            changes.F25OLD = oldTech.F25;
            changes.F25NEW = newTech.F25;
            changes.F26OLD = oldTech.F26;
            changes.F26NEW = newTech.F26;
            changes.F27OLD = oldTech.F27;
            changes.F27NEW = newTech.F27;
            changes.F28OLD = oldTech.F28;
            changes.F28NEW = newTech.F28;
            changes.F29OLD = oldTech.F29;
            changes.F29NEW = newTech.F29;
            changes.I0OLD = oldTech.I0;
            changes.I0NEW = newTech.I0;
            changes.I1OLD = oldTech.I1;
            changes.I1NEW = newTech.I1;
            changes.I2OLD = oldTech.I2;
            changes.I2NEW = newTech.I2;
            changes.I3OLD = oldTech.I3;
            changes.I3NEW = newTech.I3;
            changes.I4OLD = oldTech.I4;
            changes.I4NEW = newTech.I4;
            changes.I5OLD = oldTech.I5;
            changes.I5NEW = newTech.I5;
            changes.I6OLD = oldTech.I6;
            changes.I6NEW = newTech.I6;
            changes.I7OLD = oldTech.I7;
            changes.I7NEW = newTech.I7;
            changes.I8OLD = oldTech.I8;
            changes.I8NEW = newTech.I8;
            changes.I9OLD = oldTech.I9;
            changes.I9NEW = newTech.I9;
            changes.I10OLD = oldTech.I10;
            changes.I10NEW = newTech.I10;
            changes.I11OLD = oldTech.I11;
            changes.I11NEW = newTech.I11;
            changes.I12OLD = oldTech.I12;
            changes.I12NEW = newTech.I12;
            changes.I13OLD = oldTech.I13;
            changes.I13NEW = newTech.I13;
            changes.I14OLD = oldTech.I14;
            changes.I14NEW = newTech.I14;
            changes.S0OLD = oldTech.S0;
            changes.S0NEW = newTech.S0;
            changes.S1OLD = oldTech.S1;
            changes.S1NEW = newTech.S1;
            changes.S2OLD = oldTech.S2;
            changes.S2NEW = newTech.S2;
            changes.S3OLD = oldTech.S3;
            changes.S3NEW = newTech.S3;
            changes.S4OLD = oldTech.S4;
            changes.S4NEW = newTech.S4;
            _dbContext.CondLocTechChanges.Add(changes);
        }

        private void UpdateSingleCondLocTech(CondLocTech itemToUpdate, CondLocTech newTech)
        {
            itemToUpdate.HERSTELLERID = newTech.HERSTELLERID;
            itemToUpdate.ALIVE = newTech.ALIVE;
            itemToUpdate.CONDLOCID = newTech.CONDLOCID;
            itemToUpdate.METHODE = newTech.METHODE;
            itemToUpdate.EXTENSIONID = newTech.EXTENSIONID;
            itemToUpdate.F0 = newTech.F0;
            itemToUpdate.F1 = newTech.F1;
            itemToUpdate.F2 = newTech.F2;
            itemToUpdate.F3 = newTech.F3;
            itemToUpdate.F4 = newTech.F4;
            itemToUpdate.F5 = newTech.F5;
            itemToUpdate.F6 = newTech.F6;
            itemToUpdate.F7 = newTech.F7;
            itemToUpdate.F8 = newTech.F8;
            itemToUpdate.F9 = newTech.F9;
            itemToUpdate.F10 = newTech.F10;
            itemToUpdate.F11 = newTech.F11;
            itemToUpdate.F12 = newTech.F12;
            itemToUpdate.F13 = newTech.F13;
            itemToUpdate.F14 = newTech.F14;
            itemToUpdate.F15 = newTech.F15;
            itemToUpdate.F16 = newTech.F16;
            itemToUpdate.F17 = newTech.F17;
            itemToUpdate.F18 = newTech.F18;
            itemToUpdate.F19 = newTech.F19;
            itemToUpdate.F20 = newTech.F20;
            itemToUpdate.F21 = newTech.F21;
            itemToUpdate.F22 = newTech.F22;
            itemToUpdate.F23 = newTech.F23;
            itemToUpdate.F24 = newTech.F24;
            itemToUpdate.F25 = newTech.F25;
            itemToUpdate.F26 = newTech.F26;
            itemToUpdate.F27 = newTech.F27;
            itemToUpdate.F28 = newTech.F28;
            itemToUpdate.F29 = newTech.F29;
            itemToUpdate.I0 = newTech.I0;
            itemToUpdate.I1 = newTech.I1;
            itemToUpdate.I2 = newTech.I2;
            itemToUpdate.I3 = newTech.I3;
            itemToUpdate.I4 = newTech.I4;
            itemToUpdate.I5 = newTech.I5;
            itemToUpdate.I6 = newTech.I6;
            itemToUpdate.I7 = newTech.I7;
            itemToUpdate.I8 = newTech.I8;
            itemToUpdate.I9 = newTech.I9;
            itemToUpdate.I10 = newTech.I10;
            itemToUpdate.I11 = newTech.I11;
            itemToUpdate.I12 = newTech.I12;
            itemToUpdate.I13 = newTech.I13;
            itemToUpdate.I14 = newTech.I14;
            itemToUpdate.S0 = newTech.S0;
            itemToUpdate.S1 = newTech.S1;
            itemToUpdate.S2 = newTech.S2;
            itemToUpdate.S3 = newTech.S3;
            itemToUpdate.S4 = newTech.S4;
        }

        private void AddUseStatistics()
        {
            var usageStatistic = new UsageStatistic
            {
                ACTION = UsageStatisticActions.SaveCollision("CondLocTech"),
                TIMESTAMP = _timeDataAccess.UtcNow()
            };

            _dbContext.UsageStatistics.Add(usageStatistic);
            _dbContext.SaveChanges();
        }

        private bool CondLocTechUpdateWithHistoryPreconditions(CondLocTech itemToUpdate, CondLocTech oldTech)
        {
            return
                itemToUpdate.SEQID == oldTech.SEQID &&
                itemToUpdate.HERSTELLERID == oldTech.HERSTELLERID &&
                itemToUpdate.CONDLOCID == oldTech.CONDLOCID &&
                itemToUpdate.ALIVE == oldTech.ALIVE &&
                itemToUpdate.METHODE == oldTech.METHODE &&
                itemToUpdate.EXTENSIONID == oldTech.EXTENSIONID &&
                itemToUpdate.F0 == oldTech.F0 &&
                itemToUpdate.F1 == oldTech.F1 &&
                itemToUpdate.F2 == oldTech.F2 &&
                itemToUpdate.F3 == oldTech.F3 &&
                itemToUpdate.F4 == oldTech.F4 &&
                itemToUpdate.F5 == oldTech.F5 &&
                itemToUpdate.F6 == oldTech.F6 &&
                itemToUpdate.F7 == oldTech.F7 &&
                itemToUpdate.F8 == oldTech.F8 &&
                itemToUpdate.F9 == oldTech.F9 &&
                itemToUpdate.F10 == oldTech.F10 &&
                itemToUpdate.F11 == oldTech.F11 &&
                itemToUpdate.F12 == oldTech.F12 &&
                itemToUpdate.F13 == oldTech.F13 &&
                itemToUpdate.F14 == oldTech.F14 &&
                itemToUpdate.F15 == oldTech.F15 &&
                itemToUpdate.F16 == oldTech.F16 &&
                itemToUpdate.F17 == oldTech.F17 &&
                itemToUpdate.F18 == oldTech.F18 &&
                itemToUpdate.F19 == oldTech.F19 &&
                itemToUpdate.F20 == oldTech.F20 &&
                itemToUpdate.F21 == oldTech.F21 &&
                itemToUpdate.F22 == oldTech.F22 &&
                itemToUpdate.F23 == oldTech.F23 &&
                itemToUpdate.F24 == oldTech.F24 &&
                itemToUpdate.F25 == oldTech.F25 &&
                itemToUpdate.F26 == oldTech.F26 &&
                itemToUpdate.F27 == oldTech.F27 &&
                itemToUpdate.F28 == oldTech.F28 &&
                itemToUpdate.F29 == oldTech.F29 &&
                itemToUpdate.I0 == oldTech.I0 &&
                itemToUpdate.I1 == oldTech.I1 &&
                itemToUpdate.I2 == oldTech.I2 &&
                itemToUpdate.I3 == oldTech.I3 &&
                itemToUpdate.I4 == oldTech.I4 &&
                itemToUpdate.I5 == oldTech.I5 &&
                itemToUpdate.I6 == oldTech.I6 &&
                itemToUpdate.I7 == oldTech.I7 &&
                itemToUpdate.I8 == oldTech.I8 &&
                itemToUpdate.I9 == oldTech.I9 &&
                itemToUpdate.I10 == oldTech.I10 &&
                itemToUpdate.I11 == oldTech.I11 &&
                itemToUpdate.I12 == oldTech.I12 &&
                itemToUpdate.I13 == oldTech.I13 &&
                itemToUpdate.I14 == oldTech.I14 &&
                itemToUpdate.S0 == oldTech.S0 &&
                itemToUpdate.S1 == oldTech.S1 &&
                itemToUpdate.S2 == oldTech.S2 &&
                itemToUpdate.S3 == oldTech.S3 &&
                itemToUpdate.S4 == oldTech.S4;
        }

        public List<ProcessControlTech> InsertProcessControlTechsWithHistory(List<ProcessControlTechDiff> diffs, bool returnList)
        {
            var insertedProcessControlTechs = new List<ProcessControlTech>();
            foreach (var processControlConditionDiff in diffs)
            {
                InsertSingleProcessControlTechWithHistory(processControlConditionDiff, insertedProcessControlTechs);
            }

            return returnList ? insertedProcessControlTechs : null;
        }

        private void InsertSingleProcessControlTechWithHistory(ProcessControlTechDiff processControlTechDiff, List<ProcessControlTech> insertedProcessControlTechs)
        {
            var currentTimestamp = _timeDataAccess.UtcNow();
            var globalHistoryId = _globalHistoryDataAccess.CreateGlobalHistory("DATA INSERTED", currentTimestamp);
            AddSpecificCondLocTechHistoryEntry(globalHistoryId);

            InsertSingleProcessControlTech(processControlTechDiff.GetNewProcessControlTech(), currentTimestamp, insertedProcessControlTechs);

            var processControlTechChanges = CreateCondLocTechChanges(globalHistoryId, processControlTechDiff);
            AddProcessControlTechChangesForInsert(processControlTechDiff, processControlTechChanges);

            _dbContext.SaveChanges();
        }

        private void InsertSingleProcessControlTech(ProcessControlTech processControlTech, DateTime currentTimestamp, List<ProcessControlTech> insertedProcessControlTechs)
        {
            var dbProcessControlTech = CondLocTechConverter.ConvertEntityToCondLocTech(processControlTech);
            dbProcessControlTech.ALIVE = true;
            _dbContext.CondLocTechs.Add(dbProcessControlTech);
            _dbContext.SaveChanges();

            processControlTech.Id = new ProcessControlTechId(dbProcessControlTech.SEQID);
            insertedProcessControlTechs.Add(processControlTech);
        }

        private CondLocTechChanges CreateCondLocTechChanges(long globalHistoryId, ProcessControlTechDiff processControlTechDiff)
        {
            var newProcessControlTech = processControlTechDiff.GetNewProcessControlTech();

            var condLocChanges = new CondLocTechChanges()
            {
                GLOBALHISTORYID = globalHistoryId,
                CONDLOCTECHID = newProcessControlTech.Id.ToLong(),
                USERID = processControlTechDiff.GetUser().UserId.ToLong(),
                USERCOMMENT = processControlTechDiff.GetComment().ToDefaultString()
            };

            return condLocChanges;
        }


        private void AddProcessControlTechChangesForInsert(ProcessControlTechDiff processControlTechDiff, CondLocTechChanges changes)
        {
            var dbProcessControlTech = CondLocTechConverter.ConvertEntityToCondLocTech(processControlTechDiff.GetNewProcessControlTech());

            changes.ACTION = "INSERT";
            changes.ALIVEOLD = null;
            changes.ALIVENEW = true;
            changes.HERSTELLERIDNEW = dbProcessControlTech.HERSTELLERID;
            changes.METHODENEW = dbProcessControlTech.METHODE;
            changes.EXTENSIONIDNEW = dbProcessControlTech.EXTENSIONID;
            changes.F0NEW = dbProcessControlTech.F0;
            changes.F1NEW = dbProcessControlTech.F1;
            changes.F2NEW = dbProcessControlTech.F2;
            changes.F3NEW = dbProcessControlTech.F3;
            changes.F4NEW = dbProcessControlTech.F4;
            changes.F5NEW = dbProcessControlTech.F5;
            changes.F6NEW = dbProcessControlTech.F6;
            changes.F7NEW = dbProcessControlTech.F7;
            changes.F8NEW = dbProcessControlTech.F8;
            changes.F9NEW = dbProcessControlTech.F9;
            changes.F10NEW = dbProcessControlTech.F10;
            changes.F11NEW = dbProcessControlTech.F1;
            changes.F12NEW = dbProcessControlTech.F12;
            changes.F13NEW = dbProcessControlTech.F13;
            changes.F14NEW = dbProcessControlTech.F14;
            changes.F15NEW = dbProcessControlTech.F15;
            changes.F16NEW = dbProcessControlTech.F16;
            changes.F17NEW = dbProcessControlTech.F17;
            changes.F18NEW = dbProcessControlTech.F18;
            changes.F19NEW = dbProcessControlTech.F19;
            changes.F20NEW = dbProcessControlTech.F20;
            changes.F21NEW = dbProcessControlTech.F21;
            changes.F22NEW = dbProcessControlTech.F22;
            changes.F23NEW = dbProcessControlTech.F23;
            changes.F24NEW = dbProcessControlTech.F24;
            changes.F25NEW = dbProcessControlTech.F25;
            changes.F26NEW = dbProcessControlTech.F26;
            changes.F27NEW = dbProcessControlTech.F27;
            changes.F28NEW = dbProcessControlTech.F28;
            changes.F29NEW = dbProcessControlTech.F29;
            changes.I0NEW = dbProcessControlTech.I0;
            changes.I1NEW = dbProcessControlTech.I1;
            changes.I2NEW = dbProcessControlTech.I2;
            changes.I3NEW = dbProcessControlTech.I3;
            changes.I4NEW = dbProcessControlTech.I4;
            changes.I5NEW = dbProcessControlTech.I5;
            changes.I6NEW = dbProcessControlTech.I6;
            changes.I7NEW = dbProcessControlTech.I7;
            changes.I8NEW = dbProcessControlTech.I8;
            changes.I9NEW = dbProcessControlTech.I9;
            changes.I10NEW = dbProcessControlTech.I10;
            changes.I11NEW = dbProcessControlTech.I11;
            changes.I12NEW = dbProcessControlTech.I12;
            changes.I13NEW = dbProcessControlTech.I13;
            changes.I14NEW = dbProcessControlTech.I14;
            changes.S0NEW = dbProcessControlTech.S0;
            changes.S1NEW = dbProcessControlTech.S1;
            changes.S2NEW = dbProcessControlTech.S2;
            changes.S3NEW = dbProcessControlTech.S3;
            changes.S4NEW = dbProcessControlTech.S4;

            _dbContext.CondLocTechChanges.Add(changes);
        }

        private void AddSpecificCondLocTechHistoryEntry(long globalHistoryId)
        {
            var condLocTechHistory = new CondLocTechHistory { GLOBALHISTORYID = globalHistoryId };
            _dbContext.CondLocTechHistories.Add(condLocTechHistory);
        }
    }
}
