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
using ToleranceClass = Server.Core.Entities.ToleranceClass;

namespace FrameworksAndDrivers.DataAccess.DataAccess
{
    public class ToleranceClassDataAccess :  DataAccessBase, IToleranceClassDataAccess
    {
        private readonly IGlobalHistoryDataAccess _globalHistoryDataAccess;
        private readonly ITimeDataAccess _timeDataAccess;
        private readonly Mapper _mapper = new Mapper();

        public ToleranceClassDataAccess(ITransactionDbContext transactionContext, SqliteDbContext dbContext, 
            IGlobalHistoryDataAccess globalHistoryDataAccess, ITimeDataAccess timeDataAccess) 
            : base(transactionContext, dbContext)
        {
            _globalHistoryDataAccess = globalHistoryDataAccess;
            _timeDataAccess = timeDataAccess;
        }

        public List<ToleranceClass> LoadToleranceClasses()
        {
            var dbToleranceClasses = _dbContext.ToleranceClasses.Where(x => x.ALIVE == true).ToList();
            var toleranceClasses = new List<ToleranceClass>();

            dbToleranceClasses.ForEach(db => toleranceClasses.Add(_mapper.DirectPropertyMapping(db)));

            return toleranceClasses;
        }

        public List<LocationReferenceLink> GetToleranceClassLocationLinks(ToleranceClassId classId)
        {
            var locationLinksDb = _dbContext.Locations.Where(l => (l.CLASSID == classId.ToLong() || l.CLASSID2 == classId.ToLong()) && l.ALIVE == true).ToList();
            var locationLinks = new List<LocationReferenceLink>();

            locationLinksDb.ForEach(l =>
                locationLinks.Add(new LocationReferenceLink(new QstIdentifier(l.SEQID), l.USERID, l.NAME)));

            return locationLinks;
        }

        public List<LocationToolAssignmentId> GetToleranceClassLocationToolAssignmentLinks(ToleranceClassId toleranceClassId)
        {
            var references = _dbContext.CondRots
                .Where(c => (c.CLASSID == toleranceClassId.ToLong() || c.CLASSID2 == toleranceClassId.ToLong()) &&
                            c.ALIVE == true).Select(c => new LocationToolAssignmentId(c.LOCPOWID)).ToList();
            return references;
        }

        public Dictionary<long, ToleranceClass> GetToleranceClassesFromHistoryForIds(List<Tuple<long, long, DateTime>> idsWithClassData)
        {
            var toleranceClassHistory = new Dictionary<long, ToleranceClass>();
            foreach (var data in idsWithClassData)
            {
                var id = data.Item1;
                var classId = data.Item2;
                var historyDate = data.Item3;

                var entity = (
                    from t in _dbContext.ToleranceClassChanges
                    join g in _dbContext.GlobalHistories
                        on t.GLOBALHISTORYID equals g.ID
                    where t.TOLERANCECLASSID == classId && g.TIMESTAMP <= historyDate
                    orderby g.TIMESTAMP descending
                    select t
                ).FirstOrDefault();

                if (entity != null)
                {
                    toleranceClassHistory[id] = GetToleranceClassFromToleranceClassChanges(entity);
                }
            }

            return toleranceClassHistory;
        }

        private ToleranceClass GetToleranceClassFromToleranceClassChanges(ToleranceClassChanges entity)
        {
            var toleranceClass = new ToleranceClass
            {
                Id = new ToleranceClassId(entity.TOLERANCECLASSID),
                Name = entity.DESCRIPTIONNEW,
                LowerLimit = entity.CMINUSNEW.GetValueOrDefault(0),
                UpperLimit = entity.CPLUSNEW.GetValueOrDefault(0),
                Alive = entity.ALIVENEW.GetValueOrDefault(false),
                Relative = entity.CL_RELNEW.GetValueOrDefault(false)
            };
            return toleranceClass;
        }

        public List<ToleranceClass> InsertToleranceClassesWithHistory(List<ToleranceClassDiff> toleranceClassDiffs, bool returnList)
        {
            var insertedToleranceClasses = new List<ToleranceClass>();
            foreach (var toleranceClassDiff in toleranceClassDiffs)
            {
                InsertSingleToleranceClassWithHistory(toleranceClassDiff, insertedToleranceClasses);
            }

            return returnList ? insertedToleranceClasses : null;
        }

        private void InsertSingleToleranceClassWithHistory(ToleranceClassDiff toleranceClassDiff, List<ToleranceClass> insertedToleranceClasses)
        {
            var currentTimestamp = _timeDataAccess.UtcNow();
            var globalHistoryId = _globalHistoryDataAccess.CreateGlobalHistory("DATA INSERTED", currentTimestamp);
            AddSpecificToleranceClassHistoryEntry(globalHistoryId);

            InsertSingleToleranceClass(toleranceClassDiff.NewToleranceClass, currentTimestamp, insertedToleranceClasses);

            var toleranceClassChanges = CreateToleranceClassChanges(globalHistoryId, toleranceClassDiff);
            AddToleranceClassChangesForInsert(toleranceClassDiff, toleranceClassChanges);

            _dbContext.SaveChanges();
        }

        private void InsertSingleToleranceClass(ToleranceClass toleranceClass, DateTime currentTimestamp, List<ToleranceClass> insertedToleranceClasses)
        {
            var dbToleranceClass = _mapper.DirectPropertyMapping(toleranceClass);
            dbToleranceClass.ALIVE = true;
            dbToleranceClass.TSN = currentTimestamp;
            dbToleranceClass.TSA = currentTimestamp;
            _dbContext.ToleranceClasses.Add(dbToleranceClass);
            _dbContext.SaveChanges();

            toleranceClass.Id = new ToleranceClassId(dbToleranceClass.CLASSID);
            insertedToleranceClasses.Add(toleranceClass);
        }

        private void AddToleranceClassChangesForInsert(ToleranceClassDiff toleranceClassDiff, ToleranceClassChanges changes)
        {
            var newToleranceClass = toleranceClassDiff.NewToleranceClass;

            changes.ACTION = "INSERT";
            changes.ALIVEOLD = null;
            changes.ALIVENEW = true;

            changes.DESCRIPTIONOLD = null;
            changes.DESCRIPTIONNEW = newToleranceClass.Name;
            changes.CMINUSOLD = null;
            changes.CMINUSNEW = newToleranceClass.LowerLimit;
            changes.CPLUSOLD = null;
            changes.CPLUSNEW = newToleranceClass.UpperLimit;
            changes.CL_RELOLD = null;
            changes.CL_RELNEW = newToleranceClass.Relative;
            

            _dbContext.ToleranceClassChanges.Add(changes);
        }

        public List<ToleranceClass> UpdateToleranceClassesWithHistory(List<ToleranceClassDiff> toleranceClassDiffs)
        {
            var leftOvers = new List<ToleranceClass>();
            foreach (var toleranceClassDiff in toleranceClassDiffs)
            {
                var oldToleranceClass = toleranceClassDiff.OldToleranceClass;
                var newToleranceClass = toleranceClassDiff.NewToleranceClass;

                var itemToUpdate = _dbContext.ToleranceClasses.Find(newToleranceClass.Id.ToLong());
                if (!ToleranceClassUpdateWithHistoryPreconditions(itemToUpdate, oldToleranceClass))
                {
                    AddUseStatistics();
                    ApplyCurrentServerToleranceClassToOldToleranceClass(oldToleranceClass, itemToUpdate);
                    leftOvers.Add(_mapper.DirectPropertyMapping(itemToUpdate));
                }
                UpdateSingleToleranceClassWithHistory(toleranceClassDiff, itemToUpdate);
            }

            return leftOvers;
        }

        private bool ToleranceClassUpdateWithHistoryPreconditions(DbEntities.ToleranceClass itemToUpdate, ToleranceClass oldToleranceClass)
        {
            return itemToUpdate.DESCRIPTION == oldToleranceClass.Name
                   && itemToUpdate.CMINUS == oldToleranceClass.LowerLimit
                   && itemToUpdate.CPLUS == oldToleranceClass.UpperLimit
                   && itemToUpdate.CL_REL == oldToleranceClass.Relative
                   && itemToUpdate.ALIVE == oldToleranceClass.Alive;
        }

        private void AddUseStatistics()
        {
            var usageStatistic = new UsageStatistic
            {
                ACTION = UsageStatisticActions.SaveCollision("ToleranceClass"),
                TIMESTAMP = _timeDataAccess.UtcNow()
            };

            _dbContext.UsageStatistics.Add(usageStatistic);
            _dbContext.SaveChanges();
        }

        private void ApplyCurrentServerToleranceClassToOldToleranceClass(ToleranceClass oldToleranceClass, DbEntities.ToleranceClass itemToUpdate)
        {
            oldToleranceClass.Name = itemToUpdate.DESCRIPTION;
            oldToleranceClass.LowerLimit = itemToUpdate.CMINUS.GetValueOrDefault(0);
            oldToleranceClass.UpperLimit = itemToUpdate.CPLUS.GetValueOrDefault(0);
            oldToleranceClass.Relative = itemToUpdate.CL_REL.GetValueOrDefault(false);
            oldToleranceClass.Alive = itemToUpdate.ALIVE.GetValueOrDefault(false);
        }

        private void UpdateSingleToleranceClassWithHistory(ToleranceClassDiff toleranceClassDiff, DbEntities.ToleranceClass toleranceClassToUpdate)
        {
            var currentTimestamp = _timeDataAccess.UtcNow();
            var globalHistoryId = _globalHistoryDataAccess.CreateGlobalHistory("DATA UPDATED", currentTimestamp);
            AddSpecificToleranceClassHistoryEntry(globalHistoryId);

            UpdateSingleToleranceClass(toleranceClassToUpdate, toleranceClassDiff, currentTimestamp);

            var toleranceClassChanges = CreateToleranceClassChanges(globalHistoryId, toleranceClassDiff);
            AddToleranceClassChangesForUpdate(toleranceClassDiff, toleranceClassChanges);
            _dbContext.SaveChanges();
        }

        private void UpdateSingleToleranceClass(DbEntities.ToleranceClass toleranceClassToUpdate, ToleranceClassDiff toleranceClassDiff, DateTime currentTimestamp)
        {
            toleranceClassToUpdate.DESCRIPTION = toleranceClassDiff.NewToleranceClass.Name;
            toleranceClassToUpdate.ALIVE = toleranceClassDiff.NewToleranceClass.Alive;
            toleranceClassToUpdate.CL_REL = toleranceClassDiff.NewToleranceClass.Relative;
            toleranceClassToUpdate.CMINUS = toleranceClassDiff.NewToleranceClass.LowerLimit;
            toleranceClassToUpdate.CPLUS = toleranceClassDiff.NewToleranceClass.UpperLimit;
            toleranceClassToUpdate.TSA = currentTimestamp;
        }

        private void AddToleranceClassChangesForUpdate(ToleranceClassDiff toleranceClassDiff, ToleranceClassChanges change)
        {
            var oldToleranceClass = toleranceClassDiff.OldToleranceClass;
            var newToleranceClass = toleranceClassDiff.NewToleranceClass;

            change.ACTION = "UPDATE";
            change.ALIVEOLD = oldToleranceClass.Alive;
            change.ALIVENEW = newToleranceClass.Alive;
            change.DESCRIPTIONOLD = oldToleranceClass.Name;
            change.DESCRIPTIONNEW = newToleranceClass.Name;
            change.CL_RELOLD = oldToleranceClass.Relative;
            change.CL_RELNEW = newToleranceClass.Relative;
            change.CMINUSOLD = oldToleranceClass.LowerLimit;
            change.CMINUSNEW = newToleranceClass.LowerLimit;
            change.CPLUSOLD = oldToleranceClass.UpperLimit;
            change.CPLUSNEW = newToleranceClass.UpperLimit;

            _dbContext.ToleranceClassChanges.Add(change);
        }

        private ToleranceClassChanges CreateToleranceClassChanges(long globalHistoryId, ToleranceClassDiff toleranceClassDiff)
        {
            var newToleranceClass = toleranceClassDiff.NewToleranceClass;

            var toleranceClassChanges = new ToleranceClassChanges()
            {
                GLOBALHISTORYID = globalHistoryId,
                TOLERANCECLASSID = newToleranceClass.Id.ToLong(),
                USERID = toleranceClassDiff.User.UserId.ToLong(),
                USERCOMMENT = toleranceClassDiff.Comment.ToDefaultString()
            };

            return toleranceClassChanges;
        }

        private void AddSpecificToleranceClassHistoryEntry(long globalHistoryId)
        {
            var toleranceClassHistory = new ToleranceClassHistory() { GLOBALHISTORYID = globalHistoryId };
            _dbContext.ToleranceClassHistories.Add(toleranceClassHistory);
        }
    }
}
