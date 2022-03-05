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
using LocationDirectory = Server.Core.Entities.LocationDirectory;

namespace FrameworksAndDrivers.DataAccess.DataAccess
{
    public class LocationDirectoryDataAccess : DataAccessBase, ILocationDirectoryDataAccess
    {
        private readonly IGlobalHistoryDataAccess _globalHistoryDataAccess;
        private readonly ITimeDataAccess _timeDataAccess;
        private readonly Mapper _mapper = new Mapper();

        public LocationDirectoryDataAccess(SqliteDbContext dbContext, ITransactionDbContext transactionContext,
            IGlobalHistoryDataAccess globalHistoryDataAccess, ITimeDataAccess timeDataAccess)
            : base(transactionContext, dbContext)
        {
            _globalHistoryDataAccess = globalHistoryDataAccess;
            _timeDataAccess = timeDataAccess;
        }

        public List<LocationDirectory> LoadLocationDirectories()
        {
            var dbLocationDirectories = _dbContext.LocationDirectories.Where(x => x.STATUS == true)
                .ToList();

            var locationDirectories = new List<LocationDirectory>();

            dbLocationDirectories.ForEach(db => locationDirectories.Add(_mapper.DirectPropertyMapping(db)));

            return locationDirectories;
        }

        public List<LocationDirectory> LoadAllLocationDirectories()
        {
            var dbLocationDirectories = _dbContext.LocationDirectories.ToList();

            var locationDirectories = new List<LocationDirectory>();

            dbLocationDirectories.ForEach(db => locationDirectories.Add(_mapper.DirectPropertyMapping(db)));

            return locationDirectories;
        }

        public List<LocationDirectory> InsertLocationDirectoriesWithHistory(List<LocationDirectoryDiff> locationDirectoryDiffs, bool returnList)
        {
            var insertedLocationDirectories = new List<LocationDirectory>();
            foreach (var locationDirectoryDiff in locationDirectoryDiffs)
            {
                InsertSingleLocationDirectoryWithHistory(locationDirectoryDiff, insertedLocationDirectories);
            }

            return returnList ? insertedLocationDirectories : null;
        }

        private void InsertSingleLocationDirectoryWithHistory(LocationDirectoryDiff locationDirectoryDiff, List<LocationDirectory> insertedLocationDirectories)
        {
            var currentTimestamp = _timeDataAccess.UtcNow();
            var globalHistoryId = _globalHistoryDataAccess.CreateGlobalHistory("DATA INSERTED", currentTimestamp);
            AddSpecificLocationDirectoryHistoryEntry(globalHistoryId);

            InsertSingleLocationDirectory(locationDirectoryDiff.GetNewLocationDirectory(), currentTimestamp, insertedLocationDirectories);

            var locationDirectoryChanges = CreateLocationDirectoryChanges(globalHistoryId, locationDirectoryDiff);
            AddLocationDirectoryChangesForInsert(locationDirectoryDiff, locationDirectoryChanges);

            _dbContext.SaveChanges();
        }

        private void InsertSingleLocationDirectory(LocationDirectory locationDirectory, DateTime currentTimestamp, List<LocationDirectory> insertedLocationDirectorys)
        {
            var dbLocationDirectory = _mapper.DirectPropertyMapping(locationDirectory);
            dbLocationDirectory.STATUS = true;
            dbLocationDirectory.TSN = currentTimestamp;
            dbLocationDirectory.TSA = currentTimestamp;
            _dbContext.LocationDirectories.Add(dbLocationDirectory);
            _dbContext.SaveChanges();

            locationDirectory.Id = new LocationDirectoryId(dbLocationDirectory.LOCTREEID);
            insertedLocationDirectorys.Add(locationDirectory);
        }

        private void AddLocationDirectoryChangesForInsert(LocationDirectoryDiff locationDirectoryDiff, LocationDirectoryChanges changes)
        {
            var newLocationDirectory = locationDirectoryDiff.GetNewLocationDirectory();

            changes.ACTION = "INSERT";
            changes.STATUSOLD = null;
            changes.STATUSNEW = true;
            changes.NAMENEW = newLocationDirectory.Name.ToDefaultString();
            changes.PARENTNEW = newLocationDirectory.ParentId.ToLong();

            _dbContext.LocationDirectoryChanges.Add(changes);
        }

        public List<LocationDirectory> UpdateLocationDirectoriesWithHistory(List<LocationDirectoryDiff> locationDirectoryDiffs)
        {
            var leftOvers = new List<LocationDirectory>();
            foreach (var locationDirectoryDiff in locationDirectoryDiffs)
            {
                var oldLocationDirectory = locationDirectoryDiff.GetOldLocationDirectory();
                var newLocationDirectory = locationDirectoryDiff.GetNewLocationDirectory();

                var itemToUpdate = _dbContext.LocationDirectories.Find(newLocationDirectory.Id.ToLong());
                if (!LocationDirectoryUpdateWithHistoryPreconditions(itemToUpdate, oldLocationDirectory))
                {
                    AddUseStatistics();
                    ApplyCurrentServerStateToOldLocationDirectory(oldLocationDirectory, itemToUpdate);
                    leftOvers.Add(_mapper.DirectPropertyMapping(itemToUpdate));
                }
                UpdateSingleLocationDirectoryWithHistory(locationDirectoryDiff, itemToUpdate);
            }

            return leftOvers;
        }

        private bool LocationDirectoryUpdateWithHistoryPreconditions(DbEntities.LocationDirectory itemToUpdate, LocationDirectory newLocationDirectory)
        {
            return
                itemToUpdate.NAME == newLocationDirectory.Name.ToDefaultString()
                && itemToUpdate.PARENTID == newLocationDirectory.ParentId.ToLong()
                && itemToUpdate.STATUS == newLocationDirectory.Alive;
        }

        private void AddUseStatistics()
        {
            var usageStatistic = new UsageStatistic
            {
                ACTION = UsageStatisticActions.SaveCollision("LocTree"),
                TIMESTAMP = _timeDataAccess.UtcNow()
            };

            _dbContext.UsageStatistics.Add(usageStatistic);
            _dbContext.SaveChanges();
        }

        private void ApplyCurrentServerStateToOldLocationDirectory(LocationDirectory oldLocationDirectory, DbEntities.LocationDirectory itemToUpdate)
        {
            oldLocationDirectory.Name = new LocationDirectoryName(itemToUpdate.NAME);
            oldLocationDirectory.ParentId = itemToUpdate.PARENTID == null ? null : new LocationDirectoryId(itemToUpdate.PARENTID.Value);
            oldLocationDirectory.Alive = itemToUpdate.STATUS.GetValueOrDefault(false);
        }

        private void UpdateSingleLocationDirectoryWithHistory(LocationDirectoryDiff locationDirectoryDiff, DbEntities.LocationDirectory locationDirectoryToUpdate)
        {
            var currentTimestamp = _timeDataAccess.UtcNow();
            var globalHistoryId = _globalHistoryDataAccess.CreateGlobalHistory("DATA UPDATED", currentTimestamp);
            AddSpecificLocationDirectoryHistoryEntry(globalHistoryId);

            UpdateSingleLocationDirectory(locationDirectoryToUpdate, locationDirectoryDiff, currentTimestamp);

            var locationDirectoryChanges = CreateLocationDirectoryChanges(globalHistoryId, locationDirectoryDiff);
            AddLocationDirectoryChangesForUpdate(locationDirectoryDiff, locationDirectoryChanges);
            _dbContext.SaveChanges();
        }

        private void UpdateSingleLocationDirectory(DbEntities.LocationDirectory locationDirectoryToUpdate, LocationDirectoryDiff locationDirectoryDiff, DateTime currentTimestamp)
        {
            var locationDirectoryEntity = locationDirectoryDiff.GetNewLocationDirectory();
            locationDirectoryToUpdate.NAME = locationDirectoryEntity.Name.ToDefaultString();
            locationDirectoryToUpdate.PARENTID = locationDirectoryEntity.ParentId.ToLong();
            locationDirectoryToUpdate.STATUS = locationDirectoryEntity.Alive;
            locationDirectoryToUpdate.LOCTREEID = locationDirectoryToUpdate.LOCTREEID;
            locationDirectoryToUpdate.TSA = currentTimestamp;
        }

        private void AddLocationDirectoryChangesForUpdate(LocationDirectoryDiff locationDirectoryDiff, LocationDirectoryChanges change)
        {
            var oldLocationDirectory = locationDirectoryDiff.GetOldLocationDirectory();
            var newLocationDirectory = locationDirectoryDiff.GetNewLocationDirectory();

            change.ACTION = "UPDATE";
            change.NAMEOLD = oldLocationDirectory.Name.ToDefaultString();
            change.NAMENEW = newLocationDirectory.Name.ToDefaultString();
            change.PARENTOLD = oldLocationDirectory.ParentId.ToLong();
            change.PARENTNEW = newLocationDirectory.ParentId.ToLong();
            change.STATUSOLD = oldLocationDirectory.Alive;
            change.STATUSNEW = newLocationDirectory.Alive;

            _dbContext.LocationDirectoryChanges.Add(change);
        }

        private LocationDirectoryChanges CreateLocationDirectoryChanges(long globalHistoryId, LocationDirectoryDiff locationDirectoryDiff)
        {
            var newLocationDirectory = locationDirectoryDiff.GetNewLocationDirectory();

            var locationDirectoryChanges = new LocationDirectoryChanges()
            {
                GLOBALHISTORYID = globalHistoryId,
                LOCATIONDIRECTORYID = newLocationDirectory.Id.ToLong(),
                USERID = locationDirectoryDiff.GetUser().UserId.ToLong(),
                USERCOMMENT = locationDirectoryDiff.GetComment().ToDefaultString()
            };

            return locationDirectoryChanges;
        }

        private void AddSpecificLocationDirectoryHistoryEntry(long globalHistoryId)
        {
            var locationDirectoryHistory = new LocationDirectoryHistory() { GLOBALHISTORYID = globalHistoryId };
            _dbContext.LocationDirectoryHistories.Add(locationDirectoryHistory);
        }
    }
}
