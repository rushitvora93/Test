using System.Collections.Generic;
using System.Linq;
using Core.UseCases;
using FrameworksAndDrivers.DataAccess.DbContext;
using FrameworksAndDrivers.DataAccess.DbEntities;
using FrameworksAndDrivers.DataAccess.T4Mapper;
using Microsoft.EntityFrameworkCore;
using Server.Core.Entities;
using Server.Core.Enums;
using Server.UseCases.UseCases;

namespace FrameworksAndDrivers.DataAccess.DataAccess
{
    public class TestLevelSetAssignmentDataAccess : DataAccessBase, ITestLevelSetAssignmentData
    {
        private readonly ITimeDataAccess _timeDataAccess;
        private readonly IGlobalHistoryDataAccess _globalHistoryDataAccess;

        public TestLevelSetAssignmentDataAccess(SqliteDbContext dbContext, ITransactionDbContext transactionContext, ITimeDataAccess timeDataAccess, IGlobalHistoryDataAccess globalHistoryDataAccess)
            : base(transactionContext, dbContext)
        {
            _timeDataAccess = timeDataAccess;
            _globalHistoryDataAccess = globalHistoryDataAccess;
        }


        public void RemoveTestLevelSetAssignmentFor(List<(LocationToolAssignmentId, TestType)> ids, User user)
        {
            foreach (var item in ids)
            {
                var entry = _dbContext.CondRots.FirstOrDefault(x => x.LOCPOWID == item.Item1.ToLong());
                
                if(entry != null)
                {
                    var currentTimestamp = _timeDataAccess.UtcNow();
                    var globalHistoryId = _globalHistoryDataAccess.CreateGlobalHistory("TESTLEVELSET ASSIGNMENT REMOVED", currentTimestamp);
                    var condRotHistory = new CondRotHistory() { GLOBALHISTORYID = globalHistoryId };
                    _dbContext.CondRotHistories.Add(condRotHistory);

                    entry.TSA = currentTimestamp;
                    CondRotChanges condRotChanges = new CondRotChanges()
                    {
                        GlobalHistoryId = globalHistoryId,
                        CondRotId = entry.LOCPOWID,
                        UserId = user.UserId.ToLong(),
                        Action = "TESTLEVELSET ASSIGNMENT REMOVED"
                    };

                    if(item.Item2 == TestType.Mfu)
                    {
                        condRotChanges.TestLevelSetIdMfuOld = entry.TestLevelSetIdMfu;
                        condRotChanges.TestLevelSetIdMfuNew = null;
                        entry.TestLevelSetIdMfu = null;
                    }
                    else if (item.Item2 == TestType.Chk)
                    {
                        condRotChanges.TestLevelSetIdChkOld = entry.TestLevelSetIdChk;
                        condRotChanges.TestLevelSetIdChkNew = null;
                        entry.TestLevelSetIdChk = null;
                    }

                    _dbContext.CondRots.Update(entry);
                    _dbContext.CondRotChanges.Add(condRotChanges); 
                }
            }
            _dbContext.SaveChanges();
        }

        public void AssignTestLevelSetToLocationToolAssignments(TestLevelSetId testLevelSetId, List<(LocationToolAssignmentId, TestType)> locationToolAssignmentIds, User user)
        {
            foreach (var item in locationToolAssignmentIds)
            {
                var entry = _dbContext.CondRots.FirstOrDefault(x => x.LOCPOWID == item.Item1.ToLong());

                if (entry != null)
                {
                    var currentTimestamp = _timeDataAccess.UtcNow();
                    var globalHistoryId = _globalHistoryDataAccess.CreateGlobalHistory("TESTLEVELSET ASSIGNED", currentTimestamp);
                    var condRotHistory = new CondRotHistory() { GLOBALHISTORYID = globalHistoryId };
                    _dbContext.CondRotHistories.Add(condRotHistory);

                    entry.TSA = currentTimestamp;
                    CondRotChanges condRotChanges = new CondRotChanges()
                    {
                        GlobalHistoryId = globalHistoryId,
                        CondRotId = entry.LOCPOWID,
                        UserId = user.UserId.ToLong(),
                        Action = "TESTLEVELSET ASSIGNED"
                    };

                    if (item.Item2 == TestType.Mfu)
                    {
                        condRotChanges.TestLevelSetIdMfuOld = entry.TestLevelSetIdMfu;
                        condRotChanges.TestLevelSetIdMfuNew = testLevelSetId.ToLong();
                        condRotChanges.TestLevelNumberMfuOld = entry.TestLevelNumberMfu;
                        condRotChanges.TestLevelNumberMfuNew = 1;
                        entry.TestLevelSetIdMfu = testLevelSetId.ToLong();
                        entry.TestLevelNumberMfu = 1;
                    }
                    else if (item.Item2 == TestType.Chk)
                    {
                        condRotChanges.TestLevelSetIdChkOld = entry.TestLevelSetIdChk;
                        condRotChanges.TestLevelSetIdChkNew = testLevelSetId.ToLong();
                        condRotChanges.TestLevelNumberChkOld = entry.TestLevelNumberChk;
                        condRotChanges.TestLevelNumberChkNew = 1;
                        entry.TestLevelSetIdChk = testLevelSetId.ToLong();
                        entry.TestLevelNumberChk = 1;
                    }

                    _dbContext.CondRots.Update(entry);
                    _dbContext.CondRotChanges.Add(condRotChanges);
                }
            }
            _dbContext.SaveChanges();
        }

        public void AssignTestLevelSetToProcessControlConditions(TestLevelSetId testLevelSetId, List<ProcessControlConditionId> processControlConditionIds, User user)
        {
            foreach (var processControlCondidtionId in processControlConditionIds)
            {
                var entry = _dbContext.CondLocs.FirstOrDefault(x => x.SEQID == processControlCondidtionId.ToLong());

                if (entry != null)
                {
                    var currentTimestamp = _timeDataAccess.UtcNow();
                    var globalHistoryId = _globalHistoryDataAccess.CreateGlobalHistory("TESTLEVELSET ASSIGNED", currentTimestamp);
                    var condLocHistory = new CondLocHistory() { GLOBALHISTORYID = globalHistoryId };
                    _dbContext.CondLocHistories.Add(condLocHistory);

                    entry.TSA = currentTimestamp;
                    var condLocChanges = new CondLocChanges()
                    {
                        GLOBALHISTORYID = globalHistoryId,
                        CONDLOCID = entry.SEQID,
                        USERID = user.UserId.ToLong(),
                        ACTION = "TESTLEVELSET ASSIGNED",
                        TESTLEVELSETIDOLD = entry.TESTLEVELSETID,
                        TESTLEVELSETIDNEW = testLevelSetId.ToLong(),
                        TESTLEVELNUMBEROLD = entry.TESTLEVELNUMBER,
                        TESTLEVELNUMBERNEW = 1
                    };

                    entry.TESTLEVELSETID = testLevelSetId.ToLong();
                    entry.TESTLEVELNUMBER = 1;

                    _dbContext.CondLocs.Update(entry);
                    _dbContext.CondLocChanges.Add(condLocChanges);
                }
            }
            _dbContext.SaveChanges();
        }

        public void RemoveTestLevelSetAssignmentFor(List<ProcessControlConditionId> ids, User user)
        {
            foreach (var processControlId in ids)
            {
                var entry = _dbContext.CondLocs.FirstOrDefault(x => x.SEQID == processControlId.ToLong());

                if (entry != null)
                {
                    var currentTimestamp = _timeDataAccess.UtcNow();
                    var globalHistoryId = _globalHistoryDataAccess.CreateGlobalHistory("TESTLEVELSET ASSIGNMENT REMOVED", currentTimestamp);
                    var condLocHistory = new CondLocHistory() { GLOBALHISTORYID = globalHistoryId };
                    _dbContext.CondLocHistories.Add(condLocHistory);

                    entry.TSA = currentTimestamp;
                    var condLocChanges = new CondLocChanges()
                    {
                        GLOBALHISTORYID = globalHistoryId,
                        CONDLOCID = entry.SEQID,
                        USERID = user.UserId.ToLong(),
                        ACTION = "TESTLEVELSET ASSIGNMENT REMOVED",
                        TESTLEVELSETIDOLD = entry.TESTLEVELSETID,
                        TESTLEVELSETIDNEW = null
                    };
                    entry.TESTLEVELSETID = null;

                    _dbContext.CondLocs.Update(entry);
                    _dbContext.CondLocChanges.Add(condLocChanges);
                }
            }
            _dbContext.SaveChanges();
        }
    }
}
