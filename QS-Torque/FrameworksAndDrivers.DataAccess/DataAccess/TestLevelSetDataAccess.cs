using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.UseCases;
using FrameworksAndDrivers.DataAccess.DbContext;
using FrameworksAndDrivers.DataAccess.T4Mapper;
using Server.Core.Diffs;
using Server.Core.Entities;
using Server.UseCases.UseCases;

namespace FrameworksAndDrivers.DataAccess.DataAccess
{
    public class TestLevelSetDataAccess : DataAccessBase, ITestLevelSetData
    {
        private readonly ITimeDataAccess _timeDataAccess;
        private readonly IGlobalHistoryDataAccess _globalHistoryDataAccess;

        public TestLevelSetDataAccess(SqliteDbContext dbContext, ITransactionDbContext transactionContext,
            ITimeDataAccess timeDataAccess, IGlobalHistoryDataAccess globalHistoryDataAccess)
            : base(transactionContext, dbContext)
        {
            _timeDataAccess = timeDataAccess;
            _globalHistoryDataAccess = globalHistoryDataAccess;
        }

        public List<TestLevelSet> LoadTestLevelSets()
        {
            var mapper = new Mapper();
            var dbSets = _dbContext.TestLevelSets.ToList();
            var setEntities = dbSets.Select(x => mapper.DirectPropertyMapping(x)).ToList();

            foreach (var setEntity in setEntities)
            {
                var assignedTestLevels = _dbContext.TestLevels
                    .Where(x => x.TestLevelSetId == setEntity.Id.ToLong())
                    .ToList();
                var testLevel1 = assignedTestLevels.FirstOrDefault(x => x.TestLevelNumber == 1);
                var testLevel2 = assignedTestLevels.FirstOrDefault(x => x.TestLevelNumber == 2);
                var testLevel3 = assignedTestLevels.FirstOrDefault(x => x.TestLevelNumber == 3);
                setEntity.TestLevel1 = MapDbTestLevelToTestLevel(testLevel1, mapper);
                setEntity.TestLevel2 = MapDbTestLevelToTestLevel(testLevel2, mapper);
                setEntity.TestLevel3 = MapDbTestLevelToTestLevel(testLevel3, mapper);
            }

            return setEntities;
        }

        public TestLevelSet InsertTestLevelSet(TestLevelSetDiff diff)
        {
            var mapper = new Mapper();

            var dbTestLevelSet = _dbContext.TestLevelSets.Add(mapper.DirectPropertyMapping(diff.New)).Entity;
            _dbContext.SaveChanges();
            diff.New.Id = new TestLevelSetId(dbTestLevelSet.Id);

            var dbTestLevel1 = MapTestLevelToDbTestLevel(diff.New.TestLevel1, mapper);
            dbTestLevel1.TestLevelSetId = diff.New.Id.ToLong();
            dbTestLevel1.TestLevelNumber = 1;
            dbTestLevel1 = _dbContext.TestLevels.Add(dbTestLevel1).Entity;

            var dbTestLevel2 = MapTestLevelToDbTestLevel(diff.New.TestLevel2, mapper);
            dbTestLevel2.TestLevelSetId = diff.New.Id.ToLong();
            dbTestLevel2.TestLevelNumber = 2;
            dbTestLevel2 = _dbContext.TestLevels.Add(dbTestLevel2).Entity;

            var dbTestLevel3 = MapTestLevelToDbTestLevel(diff.New.TestLevel3, mapper);
            dbTestLevel3.TestLevelSetId = diff.New.Id.ToLong();
            dbTestLevel3.TestLevelNumber = 3;
            dbTestLevel3 = _dbContext.TestLevels.Add(dbTestLevel3).Entity;

            _dbContext.SaveChanges();

            diff.New.TestLevel1.Id = new TestLevelId(dbTestLevel1.Id);
            diff.New.TestLevel2.Id = new TestLevelId(dbTestLevel2.Id);
            diff.New.TestLevel3.Id = new TestLevelId(dbTestLevel3.Id);


            if (FeatureToggles.FeatureToggles.SilverTowel_974_TestLevelSetHistory)
            {
                var currentTimestamp = _timeDataAccess.UtcNow();
                var globalHistoryIdTestLevelSet = _globalHistoryDataAccess.CreateGlobalHistory("DATA INSERTED", currentTimestamp);
                var globalHistoryIdTestLevel1 = _globalHistoryDataAccess.CreateGlobalHistory("DATA INSERTED", currentTimestamp);
                var globalHistoryIdTestLevel2 = _globalHistoryDataAccess.CreateGlobalHistory("DATA INSERTED", currentTimestamp);
                var globalHistoryIdTestLevel3 = _globalHistoryDataAccess.CreateGlobalHistory("DATA INSERTED", currentTimestamp);
                var testLevelSetHistory = new DbEntities.TestLevelSetHistory() { GLOBALHISTORYID = globalHistoryIdTestLevelSet };
                var testLevel1History = new DbEntities.TestLevelHistory() { GLOBALHISTORYID = globalHistoryIdTestLevel1 };
                var testLevel2History = new DbEntities.TestLevelHistory() { GLOBALHISTORYID = globalHistoryIdTestLevel2 };
                var testLevel3History = new DbEntities.TestLevelHistory() { GLOBALHISTORYID = globalHistoryIdTestLevel3 };

                _dbContext.TestLevelSetHistory.Add(testLevelSetHistory);
                _dbContext.TestLevelHistory.Add(testLevel1History);
                _dbContext.TestLevelHistory.Add(testLevel2History);
                _dbContext.TestLevelHistory.Add(testLevel3History);
                _dbContext.TestLevelSetChanges.Add(GetTestLevelSetChangesFromEntities(globalHistoryIdTestLevelSet, null, diff.New, diff.User.UserId.ToLong(), "Insert"));
                _dbContext.TestLevelChanges.Add(GetTestLevelChangesFromEntities(globalHistoryIdTestLevel1, null, diff.New.TestLevel1, diff.New.Id.ToLong(), 1, diff.User.UserId.ToLong(), "Insert"));
                _dbContext.TestLevelChanges.Add(GetTestLevelChangesFromEntities(globalHistoryIdTestLevel2, null, diff.New.TestLevel2, diff.New.Id.ToLong(), 2, diff.User.UserId.ToLong(), "Insert"));
                _dbContext.TestLevelChanges.Add(GetTestLevelChangesFromEntities(globalHistoryIdTestLevel3, null, diff.New.TestLevel3, diff.New.Id.ToLong(), 3, diff.User.UserId.ToLong(), "Insert")); 
            }

            _dbContext.SaveChanges();
            return diff.New;
        }

        public void DeleteTestLevelSet(TestLevelSetDiff diff)
        {
            var testLevelSetsToDelete = _dbContext.TestLevelSets.Where(x => x.Id == diff.Old.Id.ToLong());
            _dbContext.RemoveRange(testLevelSetsToDelete);
            var testLevelsToDelete = _dbContext.TestLevels.Where(x => x.TestLevelSetId == diff.Old.Id.ToLong()).ToList();
            _dbContext.RemoveRange(testLevelsToDelete);

            if (FeatureToggles.FeatureToggles.SilverTowel_974_TestLevelSetHistory)
            {
                var currentTimestamp = _timeDataAccess.UtcNow();

                foreach(var set in testLevelSetsToDelete)
                {
                    var globalHistoryId = _globalHistoryDataAccess.CreateGlobalHistory("DATA DELETED", currentTimestamp);
                    var historyDto = new DbEntities.TestLevelSetHistory() { GLOBALHISTORYID = globalHistoryId };
                    _dbContext.TestLevelSetHistory.Add(historyDto);
                    _dbContext.TestLevelSetChanges.Add(GetTestLevelSetChangesFromEntities(globalHistoryId, new TestLevelSet()
                    {
                        Id = new TestLevelSetId(set.Id),
                        Name = new TestLevelSetName(set.Name)
                    }, 
                    null, 
                    diff.User.UserId.ToLong(),
                    "Delete"));
                }

                foreach (var level in testLevelsToDelete)
                {
                    var globalHistoryId = _globalHistoryDataAccess.CreateGlobalHistory("DATA DELETED", currentTimestamp);
                    var historyDto = new DbEntities.TestLevelHistory() { GLOBALHISTORYID = globalHistoryId };
                    _dbContext.TestLevelHistory.Add(historyDto);
                    _dbContext.TestLevelChanges.Add(GetTestLevelChangesFromEntities(
                        globalHistoryId, 
                        MapDbTestLevelToTestLevel(level, new Mapper()),
                        null, 
                        level.TestLevelSetId.GetValueOrDefault(0),
                        level.TestLevelNumber.GetValueOrDefault(0),
                        diff.User.UserId.ToLong(),
                        "Delete"));
                }
            }

            _dbContext.SaveChanges();
        }

        public void UpdateTestLevelSet(TestLevelSetDiff diff)
        {
            var currentTimestamp = _timeDataAccess.UtcNow();
            var dbTestLevelSetToUpdate = _dbContext.TestLevelSets.FirstOrDefault(x => x.Id == diff.New.Id.ToLong());

            if(dbTestLevelSetToUpdate != null)
            {
                dbTestLevelSetToUpdate.Name = diff.New.Name.ToDefaultString();
                _dbContext.TestLevelSets.Update(dbTestLevelSetToUpdate);

                if(FeatureToggles.FeatureToggles.SilverTowel_974_TestLevelSetHistory && !diff.New.EqualsByContent(diff.Old) && !diff.New.EqualsByContent(diff.Old))
                {
                    var globalHistoryId = _globalHistoryDataAccess.CreateGlobalHistory("DATA UPDATED", currentTimestamp);
                    var historyDto = new DbEntities.TestLevelSetHistory() { GLOBALHISTORYID = globalHistoryId };
                    _dbContext.TestLevelSetHistory.Add(historyDto);
                    _dbContext.TestLevelSetChanges.Add(GetTestLevelSetChangesFromEntities(
                        globalHistoryId,
                        diff.Old,
                        diff.New,
                        diff.User.UserId.ToLong(),
                        "Update", 
                        diff.Comment.ToDefaultString()));
                }
            }

            var dbTestLevel1ToUpdate = _dbContext.TestLevels.FirstOrDefault(x => x.TestLevelSetId == diff.New.Id.ToLong() && x.TestLevelNumber == 1);
            if (dbTestLevel1ToUpdate != null)
            {
                MapTestLevelToExistingDbTestLevelWithoutId(diff.New.TestLevel1, dbTestLevel1ToUpdate, 1, diff.New.Id.ToLong());
                _dbContext.TestLevels.Update(dbTestLevel1ToUpdate);

                if (FeatureToggles.FeatureToggles.SilverTowel_974_TestLevelSetHistory && !diff.New.TestLevel1.EqualsByContent(diff.Old.TestLevel1))
                {
                    var globalHistoryId = _globalHistoryDataAccess.CreateGlobalHistory("DATA UPDATED", currentTimestamp);
                    var historyDto = new DbEntities.TestLevelHistory() { GLOBALHISTORYID = globalHistoryId };
                    _dbContext.TestLevelHistory.Add(historyDto);
                    _dbContext.TestLevelChanges.Add(GetTestLevelChangesFromEntities(
                        globalHistoryId,
                        diff.Old.TestLevel1, 
                        diff.New.TestLevel1, 
                        diff.New.Id.ToLong(),
                        1,
                        diff.User.UserId.ToLong(), 
                        "Update", 
                        diff.Comment.ToDefaultString()));
                }
            }

            var dbTestLevel2ToUpdate = _dbContext.TestLevels.FirstOrDefault(x => x.TestLevelSetId == diff.New.Id.ToLong() && x.TestLevelNumber == 2);
            if (dbTestLevel2ToUpdate != null)
            {
                MapTestLevelToExistingDbTestLevelWithoutId(diff.New.TestLevel2, dbTestLevel2ToUpdate, 2, diff.New.Id.ToLong());
                _dbContext.TestLevels.Update(dbTestLevel2ToUpdate);

                if (FeatureToggles.FeatureToggles.SilverTowel_974_TestLevelSetHistory && !diff.New.TestLevel2.EqualsByContent(diff.Old.TestLevel2))
                {
                    var globalHistoryId = _globalHistoryDataAccess.CreateGlobalHistory("DATA UPDATED", currentTimestamp);
                    var historyDto = new DbEntities.TestLevelHistory() { GLOBALHISTORYID = globalHistoryId };
                    _dbContext.TestLevelHistory.Add(historyDto);
                    _dbContext.TestLevelChanges.Add(GetTestLevelChangesFromEntities(
                        globalHistoryId, 
                        diff.Old.TestLevel2, 
                        diff.New.TestLevel2, 
                        diff.New.Id.ToLong(),
                        2, 
                        diff.User.UserId.ToLong(), 
                        "Update", 
                        diff.Comment.ToDefaultString()));
                }
            }

            var dbTestLevel3ToUpdate = _dbContext.TestLevels.FirstOrDefault(x => x.TestLevelSetId == diff.New.Id.ToLong() && x.TestLevelNumber == 3);
            if (dbTestLevel3ToUpdate != null)
            {
                MapTestLevelToExistingDbTestLevelWithoutId(diff.New.TestLevel3, dbTestLevel3ToUpdate, 3, diff.New.Id.ToLong());
                _dbContext.TestLevels.Update(dbTestLevel3ToUpdate);

                if (FeatureToggles.FeatureToggles.SilverTowel_974_TestLevelSetHistory && !diff.New.TestLevel3.EqualsByContent(diff.Old.TestLevel3))
                {
                    var globalHistoryId = _globalHistoryDataAccess.CreateGlobalHistory("DATA UPDATED", currentTimestamp);
                    var historyDto = new DbEntities.TestLevelHistory() { GLOBALHISTORYID = globalHistoryId };
                    _dbContext.TestLevelHistory.Add(historyDto);
                    _dbContext.TestLevelChanges.Add(GetTestLevelChangesFromEntities(
                        globalHistoryId, 
                        diff.Old.TestLevel3, 
                        diff.New.TestLevel3,
                        diff.New.Id.ToLong(), 
                        3, 
                        diff.User.UserId.ToLong(), 
                        "Update", 
                        diff.Comment.ToDefaultString()));
                }
            }

            _dbContext.SaveChanges();
        }

        public bool IsTestLevelSetNameUnique(string name)
        {
            return !_dbContext.TestLevelSets.Any(x => x.Name == name);
        }

        public bool DoesTestLevelSetHaveReferences(TestLevelSet set)
        {
            return _dbContext.CondRots.Where(x => x.ALIVE == true && (x.TestLevelSetIdMfu == set.Id.ToLong() || x.TestLevelSetIdChk == set.Id.ToLong())).Any();
        }


        private TestLevel MapDbTestLevelToTestLevel(DbEntities.TestLevel dbEntity, Mapper mapper)
        {
            if(dbEntity == null)
            {
                return null;
            }

            var entity = mapper.DirectPropertyMapping(dbEntity);
            entity.TestInterval = new Interval()
            {
                IntervalValue = (int)(dbEntity.IntervalValue ?? 0),
                Type = (IntervalType)(dbEntity.IntervalType ?? 0)
            };
            return entity;
        }

        private DbEntities.TestLevel MapTestLevelToDbTestLevel(TestLevel entity, Mapper mapper)
        {
            if (entity == null)
            {
                return null;
            }

            var dbEntity = mapper.DirectPropertyMapping(entity);
            dbEntity.IntervalValue = entity.TestInterval?.IntervalValue ?? 0;
            dbEntity.IntervalType = (long?)entity.TestInterval?.Type;
            return dbEntity;
        }

        private void MapTestLevelToExistingDbTestLevelWithoutId(TestLevel entity, DbEntities.TestLevel existingDbEntity, long testLevelNumber, long testSetId)
        {
            if (entity == null)
            {
                return;
            }

            existingDbEntity.IntervalValue = entity.TestInterval?.IntervalValue ?? 0;
            existingDbEntity.IntervalType = (long?)entity.TestInterval?.Type;
            existingDbEntity.SampleNumber = entity.SampleNumber;
            existingDbEntity.ConsiderWorkingCalendar = entity.ConsiderWorkingCalendar ? 1 : 0;
            existingDbEntity.IsActive = entity.IsActive ? 1 : 0;
            existingDbEntity.TestLevelNumber = testLevelNumber;
            existingDbEntity.TestLevelSetId = testSetId;
        }

        private DbEntities.TestLevelSetChanges GetTestLevelSetChangesFromEntities(long id, TestLevelSet oldSet, TestLevelSet newSet, long userId, string action, string comment = null)
        {
            if(oldSet != null && newSet != null && !oldSet.EqualsById(newSet))
            {
                throw new ArgumentException("The id of old and new test level set must be equal");
            }

            return new DbEntities.TestLevelSetChanges()
            {
                GlobalHistoryId = id,
                Id = newSet?.Id.ToLong() ?? oldSet.Id.ToLong(),
                UserId = userId,
                UserComment = comment,
                Action = action,
                NameOld = oldSet?.Name?.ToDefaultString(),
                NameNew = newSet?.Name?.ToDefaultString()
            };
        }

        private DbEntities.TestLevelChanges GetTestLevelChangesFromEntities(long id, TestLevel oldLevel, TestLevel newLevel, long testLevelSetId, long testLevelNumber, long userId, string action, string comment = null)
        {
            if (oldLevel != null && newLevel != null && !oldLevel.EqualsById(newLevel))
            {
                throw new ArgumentException("The id of old and new test level must be equal");
            }

            return new DbEntities.TestLevelChanges()
            {
                GlobalHistoryId = id,
                Id = newLevel?.Id.ToLong() ?? oldLevel.Id.ToLong(),
                UserId = userId,
                UserComment = comment,
                Action = action,
                TestLevelSetId = testLevelSetId,
                TestLevelNumber = testLevelNumber,
                IntervalValueOld = oldLevel?.TestInterval?.IntervalValue,
                IntervalValueNew = newLevel?.TestInterval?.IntervalValue,
                IntervalTypeOld = (long?)oldLevel?.TestInterval?.Type,
                IntervalTypeNew = (long?)newLevel?.TestInterval?.Type,
                SampleNumberOld = oldLevel?.SampleNumber,
                SampleNumberNew = newLevel?.SampleNumber,
                ConsiderWorkingCalendarOld = oldLevel?.ConsiderWorkingCalendar == true ? 1 : 0,
                ConsiderWorkingCalendarNew = newLevel?.ConsiderWorkingCalendar == true ? 1 : 0,
                IsActiveOld = oldLevel?.IsActive == true ? 1 : 0,
                IsActiveNew = newLevel?.IsActive == true ? 1 : 0
            };
        }
    }
}
