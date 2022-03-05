using System;
using System.Collections.Generic;
using System.Linq;
using Core.UseCases;
using FrameworksAndDrivers.DataAccess.Common;
using FrameworksAndDrivers.DataAccess.DbContext;
using FrameworksAndDrivers.DataAccess.DbEntities;
using FrameworksAndDrivers.DataAccess.T4Mapper;
using Microsoft.EntityFrameworkCore;
using Server.Core.Diffs;
using Server.Core.Entities;
using Server.Core.Enums;
using Server.UseCases.UseCases;
using Location = Server.Core.Entities.Location;
using ToleranceClass = Server.Core.Entities.ToleranceClass;

namespace FrameworksAndDrivers.DataAccess.DataAccess
{
    public class LocationDataAccess : DataAccessBase, ILocationDataAccess
    {
        private readonly IGlobalHistoryDataAccess _globalHistoryDataAccess;
        private readonly ITimeDataAccess _timeDataAccess;
        private readonly Mapper _mapper = new Mapper();

        public LocationDataAccess(SqliteDbContext dbContext, ITransactionDbContext transactionContext,
            IGlobalHistoryDataAccess globalHistoryDataAccess, ITimeDataAccess timeDataAccess)
            : base(transactionContext, dbContext)
        {
            _globalHistoryDataAccess = globalHistoryDataAccess;
            _timeDataAccess = timeDataAccess;
        }

        public List<Location> LoadLocations(int index = 0, int size = -1)
        {
            List<DbEntities.Location> dbLocations;

            if (size > 0)
            {
                dbLocations = _dbContext.Locations
                    .Include(l => l.ToleranceClass1)
                    .Include(l => l.ToleranceClass2)
                    .Where(x => x.ALIVE == true)
                    .OrderBy(m => m.SEQID)
                    .Skip(index)
                    .Take(size)
                    .ToList();
            }
            else
            {
                dbLocations = _dbContext.Locations
                    .Include(l => l.ToleranceClass1)
                    .Include(l => l.ToleranceClass2)
                    .Where(x => x.ALIVE == true)
                    .OrderBy(m => m.SEQID)
                    .Skip(index)
                    .ToList();
            }

            var locations = new List<Location>();

            dbLocations.ForEach(db => locations.Add(_mapper.DirectPropertyMapping(db)));

            return locations;
        }

        public List<Location> LoadLocationsByIds(List<LocationId> ids)
        {
            var dbIds = ids.Select(x => x.ToLong()).ToList();
            var dbLocations = _dbContext.Locations
                .Include(l => l.ToleranceClass1)
                .Include(l => l.ToleranceClass2)
                .Where(x => dbIds.Contains(x.SEQID))
                .OrderBy(m => m.SEQID)
                .ToList();

            var locations = new List<Location>();

            dbLocations.ForEach(db => locations.Add(_mapper.DirectPropertyMapping(db)));

            return locations;
        }

        public bool IsUserIdUnique(string userId)
        {
            var count = _dbContext.Locations.Where(x => x.ALIVE == true).Count(x => x.USERID == userId);
            return count == 0;
        }

        public List<long> GetReferencedLocPowIdsForLocationId(LocationId id)
        {
            var locPowIds = _dbContext.LocPows.Where(x => x.LocId == id.ToLong() && x.Alive == true)
                .Select(x => x.LocPowId).ToList();

            return locPowIds;
        }

        public List<Location> InsertLocationsWithHistory(List<LocationDiff> locationDiffs, bool returnList)
        {
            var insertedLocations = new List<Location>();
            foreach (var locationDiff in locationDiffs)
            {
                InsertSingleLocationWithHistory(locationDiff, insertedLocations);
            }

            return returnList ? insertedLocations : null;
        }

        private void InsertSingleLocationWithHistory(LocationDiff locationDiff, List<Location> insertedLocations)
        {
            var currentTimestamp = _timeDataAccess.UtcNow();
            var globalHistoryId = _globalHistoryDataAccess.CreateGlobalHistory(DiffTypeUtil.InsertKey, currentTimestamp);
            AddSpecificLocationHistoryEntry(globalHistoryId);

            InsertSingleLocation(locationDiff.GetNewLocation(), currentTimestamp, insertedLocations);

            var locationChanges = CreateLocationChanges(globalHistoryId, locationDiff);
            AddLocationChangesForInsert(locationDiff, locationChanges);

            _dbContext.SaveChanges();
        }

        private void InsertSingleLocation(Location location, DateTime currentTimestamp, List<Location> insertedLocations)
        {
            var dbLocation = _mapper.DirectPropertyMapping(location);
            dbLocation.ALIVE = true;
            dbLocation.TSN = currentTimestamp;
            dbLocation.TSA = currentTimestamp;
            _dbContext.Locations.Add(dbLocation);
            _dbContext.SaveChanges();

            location.Id = new LocationId(dbLocation.SEQID);
            insertedLocations.Add(location);
        }

        private void AddLocationChangesForInsert(LocationDiff locationDiff, LocationChanges changes)
        {
            var newLocation = locationDiff.GetNewLocation();

            changes.ACTION = DiffTypeUtil.InsertKey;
            changes.ALIVEOLD = null;
            changes.ALIVENEW = true;
            changes.USERIDNEW = newLocation.Number.ToDefaultString();
            changes.NAMENEW = newLocation.Description.ToDefaultString();
            changes.TREEIDNEW = newLocation.ParentDirectoryId.ToLong();
            changes.TNOMNEW = newLocation.SetPoint1;
            changes.TMINNEW = newLocation.Minimum1;
            changes.TMAXNEW = newLocation.Maximum1;
            changes.TSTARTNEW = newLocation.Threshold1;
            changes.CLASSIDNEW = newLocation.ToleranceClass1?.Id?.ToLong();
            changes.KOSTNEW = newLocation.ConfigurableField1.ToDefaultString();
            changes.DOKUNEW = newLocation.ConfigurableField3;
            changes.KATEGNEW = newLocation.ConfigurableField2.ToDefaultString();
            changes.CONTROLNEW = (long)newLocation.ControlledBy;
            changes.NOM2NEW = newLocation.SetPoint2;
            changes.MIN2NEW = newLocation.Minimum2;
            changes.MAX2NEW = newLocation.Maximum2;
            changes.CLASSID2NEW = newLocation.ToleranceClass2?.Id?.ToLong();

            _dbContext.LocationChanges.Add(changes);
        }

        public List<Location> UpdateLocationsWithHistory(List<LocationDiff> locationDiffs)
        {
            var leftOvers = new List<Location>();
            foreach (var locationDiff in locationDiffs)
            {
                var oldLocation = locationDiff.GetOldLocation();
                var newLocation = locationDiff.GetNewLocation();

                var itemToUpdate = _dbContext.Locations.Find(newLocation.Id.ToLong());
                if (!LocationUpdateWithHistoryPreconditions(itemToUpdate, oldLocation))
                {
                    AddUseStatistics();
                    ApplyCurrentServerStateToOldLocation(oldLocation, itemToUpdate);
                    leftOvers.Add(_mapper.DirectPropertyMapping(itemToUpdate));
                }
                UpdateSingleLocationWithHistory(locationDiff, itemToUpdate);
            }

            return leftOvers;
        }

        private bool LocationUpdateWithHistoryPreconditions(DbEntities.Location itemToUpdate, Location newLocation)
        {
            return
                itemToUpdate.USERID == newLocation.Number.ToDefaultString()
                && itemToUpdate.NAME == newLocation.Description.ToDefaultString()
                && itemToUpdate.TREEID == newLocation.ParentDirectoryId.ToLong()
                && itemToUpdate.TNOM == newLocation.SetPoint1
                && itemToUpdate.TMIN == newLocation.Minimum1
                && itemToUpdate.TMAX == newLocation.Maximum1
                && itemToUpdate.TSTART == newLocation.Threshold1
                && itemToUpdate.CLASSID == newLocation.ToleranceClass1?.Id?.ToLong()
                && itemToUpdate.KOST == newLocation.ConfigurableField1.ToDefaultString()
                && itemToUpdate.DOKU == newLocation.ConfigurableField3
                && itemToUpdate.KATEG == newLocation.ConfigurableField2.ToDefaultString()
                && itemToUpdate.CONTROL == (long)newLocation.ControlledBy
                && itemToUpdate.NOM2 == newLocation.SetPoint2
                && itemToUpdate.MIN2 == newLocation.Minimum2
                && itemToUpdate.MAX2 == newLocation.Maximum2
                && itemToUpdate.CLASSID2 == newLocation.ToleranceClass2?.Id?.ToLong();
        }

        private void AddUseStatistics()
        {
            var usageStatistic = new UsageStatistic
            {
                ACTION = UsageStatisticActions.SaveCollision("Location"),
                TIMESTAMP = _timeDataAccess.UtcNow()
            };

            _dbContext.UsageStatistics.Add(usageStatistic);
            _dbContext.SaveChanges();
        }

        private void ApplyCurrentServerStateToOldLocation(Location oldLocation, DbEntities.Location itemToUpdate)
        {
            oldLocation.Number = new LocationNumber(itemToUpdate.USERID);
            oldLocation.Description = new LocationDescription(itemToUpdate.NAME);
            oldLocation.ParentDirectoryId = itemToUpdate.TREEID == null ? null : new LocationDirectoryId(itemToUpdate.TREEID.Value);
            oldLocation.ControlledBy = (LocationControlledBy)itemToUpdate.CONTROL;
            oldLocation.SetPoint1 = itemToUpdate.TNOM.GetValueOrDefault();
            oldLocation.Minimum1 = itemToUpdate.TMIN.GetValueOrDefault();
            oldLocation.Maximum1 = itemToUpdate.TMAX.GetValueOrDefault();
            oldLocation.Threshold1 = itemToUpdate.TSTART.GetValueOrDefault();
            oldLocation.SetPoint2 = itemToUpdate.NOM2.GetValueOrDefault();
            oldLocation.Minimum2 = itemToUpdate.MIN2.GetValueOrDefault();
            oldLocation.Maximum2 = itemToUpdate.MAX2.GetValueOrDefault();
            oldLocation.ConfigurableField1 = new LocationConfigurableField1(itemToUpdate.KOST); ;
            oldLocation.ConfigurableField2 = new LocationConfigurableField2(itemToUpdate.KATEG);
            oldLocation.ConfigurableField3 = itemToUpdate.DOKU.GetValueOrDefault(false);
            oldLocation.Alive = itemToUpdate.ALIVE.GetValueOrDefault(false);
            oldLocation.ToleranceClass1 = GetToleranceClassFromId(itemToUpdate.CLASSID);
            oldLocation.ToleranceClass2 = GetToleranceClassFromId(itemToUpdate.CLASSID2);
        }

        private ToleranceClass GetToleranceClassFromId(long? id)
        {
            if (id == null)
            {
                return null;
            }

            return new ToleranceClass()
            {
                Id = new ToleranceClassId(id.Value)
            };
        }

        private void UpdateSingleLocationWithHistory(LocationDiff locationDiff, DbEntities.Location locationToUpdate)
        {
            var currentTimestamp = _timeDataAccess.UtcNow();
            var globalHistoryId = _globalHistoryDataAccess.CreateGlobalHistory(DiffTypeUtil.UpdateKey, currentTimestamp);
            AddSpecificLocationHistoryEntry(globalHistoryId);

            UpdateSingleLocation(locationToUpdate, locationDiff, currentTimestamp);

            var locationChanges = CreateLocationChanges(globalHistoryId, locationDiff);
            AddLocationChangesForUpdate(locationDiff, locationChanges);
            _dbContext.SaveChanges();
        }

        private void UpdateSingleLocation(DbEntities.Location locationToUpdate, LocationDiff locationDiff, DateTime currentTimestamp)
        {
            UpdateDbLocationFromLocationEntity(locationToUpdate, locationDiff.GetNewLocation());
            locationToUpdate.SEQID = locationToUpdate.SEQID;
            locationToUpdate.TSA = currentTimestamp;
        }

        private void UpdateDbLocationFromLocationEntity(DbEntities.Location dbLocation, Location locationEntity)
        {
            dbLocation.USERID = locationEntity.Number.ToDefaultString();
            dbLocation.NAME = locationEntity.Description.ToDefaultString();
            dbLocation.TREEID = locationEntity.ParentDirectoryId.ToLong();
            dbLocation.CONTROL = (long)locationEntity.ControlledBy;
            dbLocation.CLASSID = locationEntity.ToleranceClass1?.Id?.ToLong();
            dbLocation.TNOM = locationEntity.SetPoint1;
            dbLocation.TMIN = locationEntity.Minimum1;
            dbLocation.TMAX = locationEntity.Maximum1;
            dbLocation.TSTART = locationEntity.Threshold1;
            dbLocation.CLASSID2 = locationEntity.ToleranceClass2?.Id?.ToLong();
            dbLocation.NOM2 = locationEntity.SetPoint2;
            dbLocation.MIN2 = locationEntity.Minimum2;
            dbLocation.MAX2 = locationEntity.Maximum2;
            dbLocation.KOST = locationEntity.ConfigurableField1.ToDefaultString();
            dbLocation.KATEG = locationEntity.ConfigurableField2.ToDefaultString();
            dbLocation.DOKU = locationEntity.ConfigurableField3;
            dbLocation.ALIVE = locationEntity.Alive;
        }

        private void AddLocationChangesForUpdate(LocationDiff locationDiff, LocationChanges change)
        {
            var oldLocation = locationDiff.GetOldLocation();
            var newLocation = locationDiff.GetNewLocation();

            change.ACTION = DiffTypeUtil.UpdateKey;
            change.USERIDOLD = oldLocation.Number.ToDefaultString();
            change.USERIDNEW = newLocation.Number.ToDefaultString();
            change.ALIVEOLD = oldLocation.Alive;
            change.ALIVENEW = newLocation.Alive;
            change.NAMEOLD = oldLocation.Description.ToDefaultString();
            change.NAMENEW = newLocation.Description.ToDefaultString();
            change.TREEIDOLD = oldLocation.ParentDirectoryId.ToLong();
            change.TREEIDNEW = newLocation.ParentDirectoryId.ToLong();
            change.CONTROLOLD = (long)oldLocation.ControlledBy;
            change.CONTROLNEW = (long)newLocation.ControlledBy;
            change.TNOMOLD = oldLocation.SetPoint1;
            change.TNOMNEW = newLocation.SetPoint1;
            change.TMINOLD = oldLocation.Minimum1;
            change.TMINNEW = newLocation.Minimum1;
            change.TMAXOLD = oldLocation.Maximum1;
            change.TMAXNEW = newLocation.Maximum1;
            change.TSTARTOLD = oldLocation.Threshold1;
            change.TSTARTNEW = newLocation.Threshold1;
            change.NOM2OLD = oldLocation.SetPoint2;
            change.NOM2NEW = newLocation.SetPoint2;
            change.MIN2OLD = oldLocation.Minimum2;
            change.MIN2NEW = newLocation.Minimum2;
            change.MAX2OLD = oldLocation.Maximum2;
            change.MAX2NEW = newLocation.Maximum2;
            change.KOSTOLD = oldLocation.ConfigurableField1.ToDefaultString();
            change.KOSTNEW = newLocation.ConfigurableField1.ToDefaultString();
            change.KATEGOLD = oldLocation.ConfigurableField2.ToDefaultString();
            change.KATEGNEW = newLocation.ConfigurableField2.ToDefaultString();
            change.DOKUOLD = oldLocation.ConfigurableField3;
            change.DOKUNEW = newLocation.ConfigurableField3;
            change.CLASSIDOLD = oldLocation.ToleranceClass1?.Id?.ToLong();
            change.CLASSIDNEW = newLocation.ToleranceClass1?.Id?.ToLong();
            change.CLASSID2OLD = oldLocation.ToleranceClass2?.Id?.ToLong();
            change.CLASSID2NEW = newLocation.ToleranceClass2?.Id?.ToLong();

            _dbContext.LocationChanges.Add(change);
        }

        private LocationChanges CreateLocationChanges(long globalHistoryId, LocationDiff locationDiff)
        {
            var newLocation = locationDiff.GetNewLocation();

            var locationChanges = new LocationChanges()
            {
                GLOBALHISTORYID = globalHistoryId,
                LOCATIONID = newLocation.Id.ToLong(),
                USERID = locationDiff.GetUser().UserId.ToLong(),
                USERCOMMENT = locationDiff.GetComment().ToDefaultString()
            };

            return locationChanges;
        }

        private void AddSpecificLocationHistoryEntry(long globalHistoryId)
        {
            var locationHistory = new LocationHistory() { GLOBALHISTORYID = globalHistoryId };
            _dbContext.LocationHistories.Add(locationHistory);
        }

        public List<Location> LoadDeletedLocations(int index, int size)
        {
            List<DbEntities.Location> dbLocations;

            if (size > 0)
            {
                dbLocations = _dbContext.Locations
                    .Include(l => l.ToleranceClass1)
                    .Include(l => l.ToleranceClass2)
                    .Where(x => x.ALIVE == false)
                    .OrderBy(m => m.SEQID)
                    .Skip(index)
                    .Take(size)
                    .ToList();
            }
            else
            {
                dbLocations = _dbContext.Locations
                    .Include(l => l.ToleranceClass1)
                    .Include(l => l.ToleranceClass2)
                    .Where(x => x.ALIVE == false)
                    .OrderBy(m => m.SEQID)
                    .Skip(index)
                    .ToList();
            }

            var locations = new List<Location>();

            dbLocations.ForEach(db => locations.Add(_mapper.DirectPropertyMapping(db)));

            return locations;
        }
    }
}
