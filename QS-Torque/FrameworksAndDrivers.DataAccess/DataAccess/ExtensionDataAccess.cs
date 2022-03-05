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
using Extension = Server.Core.Entities.Extension;

namespace FrameworksAndDrivers.DataAccess.DataAccess
{
    public class ExtensionDataAccess : DataAccessBase, IExtensionDataAccess
    {
        private readonly IGlobalHistoryDataAccess _globalHistoryDataAccess;
        private readonly ITimeDataAccess _timeDataAccess;
        private readonly Mapper _mapper = new Mapper();

        public ExtensionDataAccess(ITransactionDbContext transactionContext, SqliteDbContext dbContext,
            IGlobalHistoryDataAccess globalHistoryDataAccess, ITimeDataAccess timeDataAccess)
            : base(transactionContext, dbContext)
        {
            _globalHistoryDataAccess = globalHistoryDataAccess;
            _timeDataAccess = timeDataAccess;
        }

        public List<LocationReferenceLink> GetExtensionLocationLinks(ExtensionId extensionId)
        {
            var condLocIds = _dbContext.CondLocTechs
                            .Where(t => t.EXTENSIONID == extensionId.ToLong() && t.ALIVE == true)
                            .Select(x => x.CONDLOCID)
                            .ToList();
            var locIds = _dbContext.CondLocs
                        .Where(c => condLocIds.Contains(c.SEQID) && c.ALIVE == true)
                        .Select(x => x.LOCID)
                        .ToList();
            var locationLinksDb = _dbContext.Locations
                                .Where(l => locIds.Contains(l.SEQID) &&  l.ALIVE == true)
                                .ToList();

            var locationLinks = new List<LocationReferenceLink>();

            locationLinksDb.ForEach(l =>
                locationLinks.Add(new LocationReferenceLink(new QstIdentifier(l.SEQID), l.USERID, l.NAME)));

            return locationLinks;
        }

        public List<Extension> LoadExtensions()
        {
            var extensions = new List<Extension>();
            
            var dbExtensions = _dbContext.Extensions.Where(x => x.ALIVE == true).ToList();

            dbExtensions.ForEach(db => extensions.Add(_mapper.DirectPropertyMapping(db)));
            
            return extensions;
        }

        public List<Extension> InsertExtensions(List<ExtensionDiff> extensionDiffs, bool returnList)
        {
            var insertedExtensions = new List<Extension>();
            foreach (var extensionDiff in extensionDiffs)
            {
                InsertSingleExtensionWithHistory(extensionDiff, insertedExtensions);
            }

            return returnList ? insertedExtensions : null;
        }

        public bool IsInventoryNumberUnique(ExtensionInventoryNumber inventoryNumber)
        {
            return !_dbContext.Extensions.Any(x => x.ALIVE == true && x.INVENTORY_NUMBER == inventoryNumber.ToDefaultString());
        }

        private void InsertSingleExtensionWithHistory(ExtensionDiff extensionDiff, List<Extension> insertedExtensions)
        {
            var currentTimestamp = _timeDataAccess.UtcNow();
            var globalHistoryId = _globalHistoryDataAccess.CreateGlobalHistory("DATA INSERTED", currentTimestamp);
            AddSpecificExtensionHistoryEntry(globalHistoryId);

            InsertSingleExtension(extensionDiff.NewExtension, currentTimestamp, insertedExtensions);

            var extensionChanges = CreateExtensionChanges(globalHistoryId, extensionDiff);
            AddExtensionChangesForInsert(extensionDiff, extensionChanges);

            _dbContext.SaveChanges();
        }

        private void InsertSingleExtension(Extension extension, DateTime currentTimestamp, List<Extension> insertedExtensions)
        {
            var dbExtension = _mapper.DirectPropertyMapping(extension);
            dbExtension.ALIVE = true;
            dbExtension.TSN = currentTimestamp;
            dbExtension.TSA = currentTimestamp;
            _dbContext.Extensions.Add(dbExtension);
            _dbContext.SaveChanges();

            extension.Id = new ExtensionId(dbExtension.TOOLID);
            insertedExtensions.Add(extension);
        }

        private void AddExtensionChangesForInsert(ExtensionDiff extensionDiff, ExtensionChanges changes)
        {
            var newExtension = extensionDiff.NewExtension;

            changes.ACTION = "INSERT";
            changes.ALIVEOLD = null;
            changes.ALIVENEW = true;
            changes.DESCRIPTIONOLD = null;
            changes.DESCRIPTIONNEW = newExtension.Description;
            changes.INVENTORY_NUMBEROLD = null;
            changes.INVENTORY_NUMBERNEW = newExtension.InventoryNumber?.ToDefaultString();
            changes.FAKTOROLD = null;
            changes.FAKTORNEW = newExtension.FactorTorque;
            changes.TOOLLENOLD = null;
            changes.TOOLLENNEW = newExtension.Length;
            changes.BENDINGOLD = null;
            changes.BENDINGNEW = newExtension.Bending;
            changes.INVENTORY_NUMBERNEW = newExtension.InventoryNumber?.ToDefaultString();

            _dbContext.ExtensionChanges.Add(changes);
        }

        public void UpdateExtensions(List<ExtensionDiff> extensionDiffs)
        {
            foreach (var extensionDiff in extensionDiffs)
            {
                var oldExtension = extensionDiff.OldExtension;
                var newExtension = extensionDiff.NewExtension;

                var itemToUpdate = _dbContext.Extensions.Find(newExtension.Id.ToLong());
                if (!ExtensionUpdateWithHistoryPreconditions(itemToUpdate, oldExtension))
                {
                    AddUseStatistics();
                    ApplyCurrentServerStateToOldExtension(oldExtension, itemToUpdate);
                }
                UpdateSingleExtensionWithHistory(extensionDiff, itemToUpdate);
            }
        }

        private bool ExtensionUpdateWithHistoryPreconditions(DbEntities.Extension itemToUpdate, Extension newExtension)
        {
            return
                itemToUpdate.INVENTORY_NUMBER == newExtension.InventoryNumber?.ToDefaultString() &&
                itemToUpdate.TOOL == newExtension.Description &&
                itemToUpdate.BENDING == newExtension.Bending &&
                itemToUpdate.FAKTOR == newExtension.FactorTorque &&
                itemToUpdate.TOOLLEN == newExtension.Length &&
                itemToUpdate.ALIVE == newExtension.Alive;
        }

        private void AddUseStatistics()
        {
            var usageStatistic = new UsageStatistic()
            {
                ACTION = UsageStatisticActions.SaveCollision("Extension"),
                TIMESTAMP = _timeDataAccess.UtcNow()
            };

            _dbContext.UsageStatistics.Add(usageStatistic);
            _dbContext.SaveChanges();
        }

        private void ApplyCurrentServerStateToOldExtension(Extension oldExtension, DbEntities.Extension itemToUpdate)
        {
            oldExtension.InventoryNumber = new ExtensionInventoryNumber(itemToUpdate.INVENTORY_NUMBER);
            oldExtension.Description = itemToUpdate.TOOL;
            oldExtension.FactorTorque = itemToUpdate.FAKTOR.GetValueOrDefault(0);
            oldExtension.Length = itemToUpdate.TOOLLEN.GetValueOrDefault(0);
            oldExtension.Bending = itemToUpdate.BENDING.GetValueOrDefault(0);
            oldExtension.Alive = itemToUpdate.ALIVE.GetValueOrDefault(false);
        }

        private void UpdateSingleExtensionWithHistory(ExtensionDiff extensionDiff, DbEntities.Extension extensionToUpdate)
        {
            var currentTimestamp = _timeDataAccess.UtcNow();
            var globalHistoryId = _globalHistoryDataAccess.CreateGlobalHistory("DATA UPDATED", currentTimestamp);
            AddSpecificExtensionHistoryEntry(globalHistoryId);

            UpdateSingleExtension(extensionToUpdate, extensionDiff, currentTimestamp);

            var extensionChanges = CreateExtensionChanges(globalHistoryId, extensionDiff);
            AddExtensionChangesForUpdate(extensionDiff, extensionChanges);
            _dbContext.SaveChanges();
        }

        private void UpdateSingleExtension(DbEntities.Extension extensionToUpdate, ExtensionDiff extensionDiff, DateTime currentTimestamp)
        {
            UpdateDbExtensionFromExtensionEntity(extensionToUpdate, extensionDiff.NewExtension);
            extensionToUpdate.TOOLID = extensionToUpdate.TOOLID;
            extensionToUpdate.TSA = currentTimestamp;
        }

        private void UpdateDbExtensionFromExtensionEntity(DbEntities.Extension dbExtension, Extension extensionEntity)
        {
            dbExtension.INVENTORY_NUMBER = extensionEntity.InventoryNumber?.ToDefaultString();
            dbExtension.TOOL = extensionEntity.Description;
            dbExtension.FAKTOR = extensionEntity.FactorTorque;
            dbExtension.BENDING = extensionEntity.Bending;
            dbExtension.TOOLLEN = extensionEntity.Length;
            dbExtension.ALIVE = extensionEntity.Alive;
           
        }

        private void AddExtensionChangesForUpdate(ExtensionDiff extensionDiff, ExtensionChanges change)
        {
            var oldExtension = extensionDiff.OldExtension;
            var newExtension = extensionDiff.NewExtension;

            change.ACTION = "UPDATE";

            change.INVENTORY_NUMBEROLD = oldExtension.InventoryNumber?.ToDefaultString();
            change.INVENTORY_NUMBERNEW = newExtension.InventoryNumber?.ToDefaultString();
            change.DESCRIPTIONOLD = oldExtension.Description;
            change.DESCRIPTIONNEW = newExtension.Description;
            change.TOOLLENOLD = oldExtension.Length;
            change.TOOLLENNEW = newExtension.Length;
            change.BENDINGOLD = oldExtension.Bending;
            change.BENDINGNEW = newExtension.Bending;
            change.FAKTOROLD = oldExtension.FactorTorque;
            change.FAKTORNEW = newExtension.FactorTorque;
            change.ALIVEOLD = oldExtension.Alive;
            change.ALIVENEW = newExtension.Alive;

            _dbContext.ExtensionChanges.Add(change);
        }

        private ExtensionChanges CreateExtensionChanges(long globalHistoryId, ExtensionDiff diff)
        {
            var newExtension = diff.NewExtension;

            var testEquipmentChanges = new ExtensionChanges()
            {
                GLOBALHISTORYID = globalHistoryId,
                EXTENSIONID = newExtension.Id.ToLong(),
                USERID = diff.User.UserId.ToLong(),
                USERCOMMENT = diff.Comment.ToDefaultString()
            };

            return testEquipmentChanges;
        }

        private void AddSpecificExtensionHistoryEntry(long globalHistoryId)
        {
            var extensionHistory = new ExtensionHistory() { GLOBALHISTORYID = globalHistoryId };
            _dbContext.ExtensionHistories.Add(extensionHistory);
        }

        public List<Extension> LoadDeletedExtensions()
        {
            var extensions = new List<Extension>();

            var dbExtensions = _dbContext.Extensions.Where(x => x.ALIVE == false).ToList();

            dbExtensions.ForEach(db => extensions.Add(_mapper.DirectPropertyMapping(db)));

            return extensions;
        }
    }
}
