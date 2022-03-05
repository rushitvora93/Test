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
using Manufacturer = Server.Core.Entities.Manufacturer;

namespace FrameworksAndDrivers.DataAccess.DataAccess
{
    public class ManufacturerDataAccess : DataAccessBase, IManufacturerDataAccess
    {
        private readonly IGlobalHistoryDataAccess _globalHistoryDataAccess;
        private readonly ITimeDataAccess _timeDataAccess;
        private readonly Mapper _mapper = new Mapper();

        public ManufacturerDataAccess(SqliteDbContext dbContext, ITransactionDbContext transactionContext, 
            IGlobalHistoryDataAccess globalHistoryDataAccess, ITimeDataAccess timeDataAccess)
            : base(transactionContext, dbContext)
        {
            _globalHistoryDataAccess = globalHistoryDataAccess;
            _timeDataAccess = timeDataAccess;
        }

        public List<Manufacturer> LoadManufacturers()
        {
            var dbManufacturers = _dbContext.Manufacturers.Where(x => x.ALIVE == true).OrderBy(m => m.NAME).ToList();
            var manufacturers = new List<Manufacturer>();

            dbManufacturers.ForEach(db => manufacturers.Add(_mapper.DirectPropertyMapping(db)));

            return manufacturers;            
        }

        public List<ToolModelReferenceLink> GetManufacturerModelLinks(ManufacturerId manufacturerId)
        {
            var dbToolModels = _dbContext.ToolModels.Where(x => x.MANUID == manufacturerId.ToLong() && x.ALIVE == true).ToList();
            var modelLinks = new List<ToolModelReferenceLink>();
            foreach (var model in dbToolModels)
            {
                modelLinks.Add(new ToolModelReferenceLink()
                {
                    Id = new QstIdentifier(model.MODELID),
                    DisplayName = model.MODEL
                });
            }

            return modelLinks;
        }

        public List<Manufacturer> InsertManufacturersWithHistory(List<ManufacturerDiff> manufacturerDiffs, bool returnList)
        {
            var insertedManufacturers = new List<Manufacturer>();
            foreach (var manufacturerDiff in manufacturerDiffs)
            {
                InsertSingleManufacturerWithHistory(manufacturerDiff, insertedManufacturers);
            }

            return returnList ? insertedManufacturers : null;
        }

        private void InsertSingleManufacturerWithHistory(ManufacturerDiff manufacturerDiff, List<Manufacturer> insertedManufacturers)
        {
            var currentTimestamp = _timeDataAccess.UtcNow();
            var globalHistoryId = _globalHistoryDataAccess.CreateGlobalHistory("DATA INSERTED", currentTimestamp);
            AddSpecificManufacturerHistoryEntry(globalHistoryId);

            InsertSingleManufacturer(manufacturerDiff.GetNewManufacturer(), currentTimestamp, insertedManufacturers);

            var manufacturerChanges = CreateManufacturerChanges(globalHistoryId, manufacturerDiff);
            AddManufacturerChangesForInsert(manufacturerDiff, manufacturerChanges);

            _dbContext.SaveChanges();
        }

        private void InsertSingleManufacturer(Manufacturer manufacturer, DateTime currentTimestamp, List<Manufacturer> insertedManufacturers)
        {
            var dbManufacturer = _mapper.DirectPropertyMapping(manufacturer);
            dbManufacturer.ALIVE = true;
            dbManufacturer.TSN = currentTimestamp;
            dbManufacturer.TSA = currentTimestamp;
            _dbContext.Manufacturers.Add(dbManufacturer);
            _dbContext.SaveChanges();

            manufacturer.Id = new ManufacturerId(dbManufacturer.MANUID);
            insertedManufacturers.Add(manufacturer);
        }

        private void AddManufacturerChangesForInsert(ManufacturerDiff manufacturerDiff, ManufacturerChanges changes)
        {
            var newManufacturer = manufacturerDiff.GetNewManufacturer();

            changes.ACTION = "INSERT";
            changes.ALIVEOLD = null;
            changes.ALIVENEW = true;

            changes.NAMEOLD = null;
            changes.NAMENEW = newManufacturer.Name.ToDefaultString();
            changes.STREETOLD = null;
            changes.STREETNEW = newManufacturer.Street;
            changes.CITYOLD = null;
            changes.CITYNEW = newManufacturer.City;
            changes.TELOLD = null;
            changes.TELNEW = newManufacturer.PhoneNumber;
            changes.FAXOLD = null;
            changes.FAXNEW = newManufacturer.Fax;
            changes.PERSONOLD = null;
            changes.PERSONNEW = newManufacturer.Person;
            changes.COUNTRYOLD = null;
            changes.COUNTRYNEW = newManufacturer.Country;
            changes.HOUSENUMBERSTROLD = null;
            changes.HOUSENUMBERSTRNEW = newManufacturer.HouseNumber;
            changes.ZIPCODESTROLD = null;
            changes.ZIPCODESTRNEW = newManufacturer.Plz;

            _dbContext.ManufacturerChanges.Add(changes);
        }

        public List<Manufacturer> UpdateManufacturersWithHistory(List<ManufacturerDiff> manufacturerDiffs)
        {
            var leftOvers = new List<Manufacturer>();
            foreach (var manufacturerDiff in manufacturerDiffs)
            {
                var oldManufacturer = manufacturerDiff.GetOldManufacturer();
                var newManufacturer = manufacturerDiff.GetNewManufacturer();

                var itemToUpdate = _dbContext.Manufacturers.Find(newManufacturer.Id.ToLong());
                if (!ManufacturerUpdateWithHistoryPreconditions(itemToUpdate, oldManufacturer))
                {
                    AddUseStatistics();
                    ApplyCurrentServerStateToOldManufacturer(oldManufacturer, itemToUpdate);
                    leftOvers.Add(_mapper.DirectPropertyMapping(itemToUpdate));
                }
                UpdateSingleManufacturerWithHistory(manufacturerDiff, itemToUpdate);
            }

            return leftOvers;
        }

       private bool ManufacturerUpdateWithHistoryPreconditions(DbEntities.Manufacturer itemToUpdate, Manufacturer newManufacturer)
        {
            return
                itemToUpdate.CITY == newManufacturer.City
                && itemToUpdate.COUNTRY == newManufacturer.Country
                && itemToUpdate.FAX == newManufacturer.Fax
                && itemToUpdate.NAME == newManufacturer.Name.ToDefaultString()
                && itemToUpdate.PERSON == newManufacturer.Person
                && itemToUpdate.STREET == newManufacturer.Street
                && itemToUpdate.TEL == newManufacturer.PhoneNumber
                && itemToUpdate.HOUSENUMBERSTR == newManufacturer.HouseNumber
                && itemToUpdate.ZIPCODESTR == newManufacturer.Plz
                && itemToUpdate.ALIVE == newManufacturer.Alive;
        }

       private void AddUseStatistics()
       {
           var usageStatistic = new UsageStatistic
           {
               ACTION = UsageStatisticActions.SaveCollision("Manufacturer"),
               TIMESTAMP = _timeDataAccess.UtcNow()
            };

           _dbContext.UsageStatistics.Add(usageStatistic);
           _dbContext.SaveChanges();
       }

        private void ApplyCurrentServerStateToOldManufacturer(Manufacturer oldManufacturer, DbEntities.Manufacturer itemToUpdate)
        {
            oldManufacturer.City = itemToUpdate.CITY;
            oldManufacturer.Country = itemToUpdate.COUNTRY;
            oldManufacturer.Fax = itemToUpdate.FAX;
            oldManufacturer.Name = new ManufacturerName(itemToUpdate.NAME);
            oldManufacturer.Person = itemToUpdate.PERSON;
            oldManufacturer.Street = itemToUpdate.STREET;
            oldManufacturer.PhoneNumber = itemToUpdate.TEL;
            oldManufacturer.HouseNumber = itemToUpdate.HOUSENUMBERSTR;
            oldManufacturer.Plz = itemToUpdate.ZIPCODESTR;
            oldManufacturer.Alive = itemToUpdate.ALIVE.GetValueOrDefault(false);
        }

        private void UpdateSingleManufacturerWithHistory(ManufacturerDiff manufacturerDiff, DbEntities.Manufacturer manufacturerToUpdate)
        {
            var currentTimestamp = _timeDataAccess.UtcNow();
            var globalHistoryId = _globalHistoryDataAccess.CreateGlobalHistory("DATA UPDATED", currentTimestamp);
            AddSpecificManufacturerHistoryEntry(globalHistoryId);

            UpdateSingleManufacturer(manufacturerToUpdate, manufacturerDiff, currentTimestamp);

            var manufacturerChanges = CreateManufacturerChanges(globalHistoryId, manufacturerDiff);
            AddManufacturerChangesForUpdate(manufacturerDiff, manufacturerChanges);
            _dbContext.SaveChanges();
        }

        private void UpdateSingleManufacturer(DbEntities.Manufacturer manufacturerToUpdate, ManufacturerDiff manufacturerDiff, DateTime currentTimestamp)
        {
            UpdateDbManufacturerFromManufacturerEntity(manufacturerToUpdate, manufacturerDiff.GetNewManufacturer());
            manufacturerToUpdate.MANUID = manufacturerToUpdate.MANUID;
            manufacturerToUpdate.TSA = currentTimestamp;
        }

        private void UpdateDbManufacturerFromManufacturerEntity(DbEntities.Manufacturer dbManufacturer, Manufacturer manufacturerEntity)
        {
            dbManufacturer.NAME = manufacturerEntity.Name.ToDefaultString();
            dbManufacturer.CITY = manufacturerEntity.City;
            dbManufacturer.COUNTRY = manufacturerEntity.Country;
            dbManufacturer.FAX = manufacturerEntity.Fax;
            dbManufacturer.HOUSENUMBERSTR = manufacturerEntity.HouseNumber;
            dbManufacturer.PERSON = manufacturerEntity.Person;
            dbManufacturer.ZIPCODESTR = manufacturerEntity.Plz;
            dbManufacturer.STREET = manufacturerEntity.Street;
            dbManufacturer.TEL = manufacturerEntity.PhoneNumber;
            dbManufacturer.ALIVE = manufacturerEntity.Alive;
        }

        private void AddManufacturerChangesForUpdate(ManufacturerDiff manufacturerDiff, ManufacturerChanges change)
        {
            var oldManufacturer = manufacturerDiff.GetOldManufacturer();
            var newManufacturer = manufacturerDiff.GetNewManufacturer();

            change.ACTION = "UPDATE";
            change.CITYOLD = oldManufacturer.City;
            change.CITYNEW = newManufacturer.City;
            change.COUNTRYOLD = oldManufacturer.Country;
            change.COUNTRYNEW = newManufacturer.Country;
            change.FAXOLD = oldManufacturer.Fax;
            change.FAXNEW = newManufacturer.Fax;
            change.NAMEOLD = oldManufacturer.Name.ToDefaultString();
            change.NAMENEW = newManufacturer.Name.ToDefaultString();
            change.PERSONOLD = oldManufacturer.Person;
            change.PERSONNEW = newManufacturer.Person;
            change.STREETOLD = oldManufacturer.Street;
            change.STREETNEW = newManufacturer.Street;
            change.TELOLD = oldManufacturer.PhoneNumber;
            change.TELNEW = newManufacturer.PhoneNumber;
            change.HOUSENUMBERSTROLD = oldManufacturer.HouseNumber;
            change.HOUSENUMBERSTRNEW = newManufacturer.HouseNumber;
            change.ZIPCODESTROLD = oldManufacturer.Plz;
            change.ZIPCODESTRNEW = newManufacturer.Plz;
            change.ALIVEOLD = oldManufacturer.Alive;
            change.ALIVENEW = newManufacturer.Alive;

            _dbContext.ManufacturerChanges.Add(change);
        }

        private ManufacturerChanges CreateManufacturerChanges(long globalHistoryId, ManufacturerDiff manufacturerDiff)
        {
            var newManufacturer = manufacturerDiff.GetNewManufacturer();

            var manufacturerChanges = new ManufacturerChanges()
            {
                GLOBALHISTORYID = globalHistoryId,
                MANUFACTURERID = newManufacturer.Id.ToLong(),
                USERID = manufacturerDiff.GetUser().UserId.ToLong(),
                USERCOMMENT = manufacturerDiff.GetComment().ToDefaultString()
            };

            return manufacturerChanges;
        }

        private void AddSpecificManufacturerHistoryEntry(long globalHistoryId)
        {
            var manufacturerHistory = new ManufacturerHistory() { GLOBALHISTORYID = globalHistoryId };
            _dbContext.ManufacturerHistories.Add(manufacturerHistory);
        }
    }
}
