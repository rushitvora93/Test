using System;
using System.Collections.Generic;
using System.Linq;
using Common.Types.Enums;
using Core.Entities;
using Core.UseCases;
using FrameworksAndDrivers.DataAccess.Common;
using FrameworksAndDrivers.DataAccess.DbContext;
using FrameworksAndDrivers.DataAccess.DbEntities;
using FrameworksAndDrivers.DataAccess.T4Mapper;
using Microsoft.EntityFrameworkCore;
using Server.Core.Diffs;
using Server.Core.Entities;
using Server.UseCases.UseCases;
using TestEquipment = Server.Core.Entities.TestEquipment;

namespace FrameworksAndDrivers.DataAccess.DataAccess
{
    public class TestEquipmentDataAccess : DataAccessBase, ITestEquipmentDataAccess
    {
        private readonly IGlobalHistoryDataAccess _globalHistoryDataAccess;
        private readonly ITimeDataAccess _timeDataAccess;
        private readonly Mapper _mapper = new Mapper();

        public TestEquipmentDataAccess(ITransactionDbContext transactionContext, SqliteDbContext dbContext, 
            IGlobalHistoryDataAccess globalHistoryDataAccess, ITimeDataAccess timeDataAccess)
            : base(transactionContext, dbContext)
        {
            _globalHistoryDataAccess = globalHistoryDataAccess;
            _timeDataAccess = timeDataAccess;
        }

        public List<TestEquipment> GetTestEquipmentsByIds(List<TestEquipmentId> ids)
        {
            var testEquipments = _dbContext.TestEquipments.Where(x => ids.Select(y => y.ToLong()).Contains(x.SEQID))
                .Include(x => x.TestEquipmentModel)
                .ToList();

            var result = new List<TestEquipment>();
            testEquipments.ForEach(x => result.Add(_mapper.DirectPropertyMapping(x)));
            return result;
        }

        public List<TestEquipment> InsertTestEquipmentsWithHistory(List<TestEquipmentDiff> testEquipmentDiffs, bool returnList)
        {
            var insertedTestEquipments = new List<TestEquipment>();
            foreach (var testEquipmentDiff in testEquipmentDiffs)
            {
                InsertSingleTestEquipmentWithHistory(testEquipmentDiff, insertedTestEquipments);
            }

            return returnList ? insertedTestEquipments : null;
        }

        private void InsertSingleTestEquipmentWithHistory(TestEquipmentDiff testEquipmentDiff, List<TestEquipment> insertedTestEquipments)
        {
            var currentTimestamp = _timeDataAccess.UtcNow();
            var globalHistoryId = _globalHistoryDataAccess.CreateGlobalHistory("DATA INSERTED", currentTimestamp);
            AddSpecificTestEquipmentHistoryEntry(globalHistoryId);

            InsertSingleTestEquipment(testEquipmentDiff.GetNewTestEquipment(), currentTimestamp, insertedTestEquipments);

            var testEquipmentChanges = CreateTestEquipmentChanges(globalHistoryId, testEquipmentDiff);
            AddTestEquipmentChangesForInsert(testEquipmentDiff, testEquipmentChanges);

            _dbContext.SaveChanges();
        }

        private void InsertSingleTestEquipment(TestEquipment testEquipment, DateTime currentTimestamp, List<TestEquipment> insertedTestEquipments)
        {
            var dbTestEquipment = _mapper.DirectPropertyMapping(testEquipment);
            dbTestEquipment.ALIVE = true;
            dbTestEquipment.TSN = currentTimestamp;
            dbTestEquipment.TSA = currentTimestamp;
            _dbContext.TestEquipments.Add(dbTestEquipment);
            _dbContext.SaveChanges();

            testEquipment.Id = new TestEquipmentId(dbTestEquipment.SEQID);
            insertedTestEquipments.Add(testEquipment);
        }

        private void AddTestEquipmentChangesForInsert(TestEquipmentDiff testEquipmentDiff, TestEquipmentChanges changes)
        {
            var newTestEquipment = testEquipmentDiff.GetNewTestEquipment();

            changes.ACTION = "INSERT";
            changes.ALIVEOLD = null;
            changes.ALIVENEW = true;
            changes.SERNOOLD = null;
            changes.SERNONEW = newTestEquipment.SerialNumber?.ToDefaultString();
            changes.USRNOOLD = null;
            changes.USRNONEW = newTestEquipment.InventoryNumber?.ToDefaultString();
            changes.MODELIDOLD = null;
            changes.MODELIDNEW = newTestEquipment.TestEquipmentModel?.Id?.ToLong();
            changes.STATEIDOLD = null;
            changes.STATEIDNEW = newTestEquipment.Status?.Id?.ToLong();
            changes.VERSOLD = null;
            changes.VERSNEW = newTestEquipment.Version?.ToDefaultString();
            changes.LASTCERTOLD = null;
            changes.LASTCERTNEW = newTestEquipment.LastCalibration;
            changes.PERIODOLD = null;
            changes.PERIODNEW = newTestEquipment.CalibrationInterval?.IntervalValue;
            changes.TRANSFERUSEROLD = null;
            changes.TRANSFERUSERNEW = newTestEquipment.TransferUser;
            changes.TRANSFERADAPTEROLD = null;
            changes.TRANSFERADAPTERNEW = newTestEquipment.TransferAdapter;
            changes.TRANSFERTRANSDUCEROLD = null;
            changes.TRANSFERTRANSDUCERNEW = newTestEquipment.TransferTransducer;
            changes.ASKFORIDENTOLD = null;
            changes.ASKFORIDENTNEW = newTestEquipment.AskForIdent;
            changes.TRANSFERCURVESOLD = null;
            changes.TRANSFERCURVESNEW = newTestEquipment.TransferCurves;
            changes.ASKFORSIGNOLD = null;
            changes.ASKFORSIGNNEW = newTestEquipment.AskForSign;
            changes.DOLOSECHECKOLD = null;
            changes.DOLOSECHECKNEW = newTestEquipment.DoLoseCheck;
            changes.CANDELETEMEASUREMENTSOLD = null;
            changes.CANDELETEMEASUREMENTSNEW = newTestEquipment.CanDeleteMeasurements;
            changes.CANUSEQSTSTANDARDOLD = null;
            changes.CANUSEQSTSTANDARDNEW = newTestEquipment.CanUseQstStandard;
            changes.CONFIRMMEASUREMENTOLD = null;
            changes.CONFIRMMEASUREMENTNEW = newTestEquipment.ConfirmMeasurements;
            changes.TRANSFERPICTOLD = null;
            changes.TRANSFERPICTNEW = newTestEquipment.TransferLocationPictures;
            changes.TRANSFERNEWLIMITSOLD = null;
            changes.TRANSFERNEWLIMITSNEW = newTestEquipment.TransferNewLimits;
            changes.TRANSFERATTRIBUTESOLD = null;
            changes.TRANSFERATTRIBUTESNEW = newTestEquipment.TransferAttributes;
            changes.USEERRORCODESOLD = null;
            changes.USEERRORCODESNEW = newTestEquipment.UseErrorCodes;
            changes.ISCTLTESTOLD = null;
            changes.ISCTLTESTNEW = newTestEquipment.UseForCtl;
            changes.ISROUTTESTOLD = null;
            changes.ISROUTTESTNEW = newTestEquipment.UseForRot;
            changes.CAPACITYMAXOLD = null;
            changes.CAPACITYMAXNEW = newTestEquipment.CapacityMax;
            changes.CAPACITYMINOLD = null;
            changes.CAPACITYMINNEW = newTestEquipment.CapacityMin;
            changes.CALIBRATION_NORM_TEXTOLD = null;
            changes.CALIBRATION_NORM_TEXTNEW = newTestEquipment.CalibrationNorm?.ToDefaultString();

            _dbContext.TestEquipmentChanges.Add(changes);
        }

        public void UpdateTestEquipmentsWithHistory(List<TestEquipmentDiff> testEquipmentDiffs)
        {
            foreach (var testEquipmentDiff in testEquipmentDiffs)
            {
                var oldTestEquipment = testEquipmentDiff.GetOldTestEquipment();
                var newTestEquipment = testEquipmentDiff.GetNewTestEquipment();

                var itemToUpdate = _dbContext.TestEquipments.Find(newTestEquipment.Id.ToLong());
                if (!TestEquipmentUpdateWithHistoryPreconditions(itemToUpdate, oldTestEquipment))
                {
                    AddUseStatistics();
                    ApplyCurrentServerStateToOldTestEquipment(oldTestEquipment, itemToUpdate);
                }
                UpdateSingleTestEquipmentWithHistory(testEquipmentDiff, itemToUpdate);
            }
        }

        public bool IsInventoryNumberUnique(TestEquipmentInventoryNumber inventoryNumber)
        {
            return !_dbContext.TestEquipments.Any(x => x.ALIVE == true && x.USRNO == inventoryNumber.ToDefaultString());
        }

        public bool IsSerialNumberUnique(TestEquipmentSerialNumber serialNumber)
        {
            return !_dbContext.TestEquipments.Any(x => x.ALIVE == true && x.SERNO == serialNumber.ToDefaultString());
        }

        public List<TestEquipmentType> LoadAvailableTestEquipmentTypes()
        {
            return _dbContext.TestEquipmentModels
                .Where(x => x.ALIVE == true &&
                            (x.TYPE == TestEquipmentType.Analyse || x.TYPE == TestEquipmentType.Wrench))
                .GroupBy(x => x.TYPE).Select(x => x.Key).ToList();
        }

        private bool TestEquipmentUpdateWithHistoryPreconditions(DbEntities.TestEquipment itemToUpdate, TestEquipment newTestEquipment)
        {
            return
                itemToUpdate.USRNO == newTestEquipment.InventoryNumber?.ToDefaultString() &&
                itemToUpdate.SERNO == newTestEquipment.SerialNumber?.ToDefaultString() &&
                itemToUpdate.MODELID == newTestEquipment.TestEquipmentModel?.Id?.ToLong() &&
                itemToUpdate.STATEID == newTestEquipment.Status?.Id?.ToLong() &&
                itemToUpdate.VERS == newTestEquipment.Version?.ToDefaultString() &&
                itemToUpdate.LASTCERT == newTestEquipment.LastCalibration &&
                itemToUpdate.PERIOD == newTestEquipment.CalibrationInterval?.IntervalValue &&
                itemToUpdate.TRANSFERADAPTER == newTestEquipment.TransferAdapter &&
                itemToUpdate.TRANSFERUSER == newTestEquipment.TransferUser &&
                itemToUpdate.TRANSFERATTRIBUTES == newTestEquipment.TransferAttributes &&
                itemToUpdate.ASKFORIDENT.GetValueOrDefault(TestEquipmentBehaviourAskForIdent.Never) == newTestEquipment.AskForIdent &&
                itemToUpdate.ASKFORSIGN == newTestEquipment.AskForSign &&
                itemToUpdate.CONFIRMMEASUREMENT.GetValueOrDefault(TestEquipmentBehaviourConfirmMeasurements.Never) == newTestEquipment.ConfirmMeasurements &&
                itemToUpdate.CANDELETEMEASUREMENTS == newTestEquipment.CanDeleteMeasurements &&
                itemToUpdate.CANUSEQSTSTANDARD == newTestEquipment.CanUseQstStandard &&
                itemToUpdate.DOLOSECHECK == newTestEquipment.DoLoseCheck &&
                itemToUpdate.TRANSFERCURVES.GetValueOrDefault(TestEquipmentBehaviourTransferCurves.Never) == newTestEquipment.TransferCurves &&
                itemToUpdate.TRANSFERNEWLIMITS == newTestEquipment.TransferNewLimits &&
                itemToUpdate.TRANSFERPICT == newTestEquipment.TransferLocationPictures &&
                itemToUpdate.USEERRORCODES == newTestEquipment.UseErrorCodes &&
                itemToUpdate.TRANSFERTRANSDUCER == newTestEquipment.TransferTransducer &&
                itemToUpdate.ISCTLTEST == newTestEquipment.UseForCtl &&
                itemToUpdate.ISROUTTEST == newTestEquipment.UseForRot &&
                itemToUpdate.CAPACITYMAX == newTestEquipment.CapacityMax &&
                itemToUpdate.CAPACITYMIN == newTestEquipment.CapacityMin &&
                itemToUpdate.CALIBRATION_NORM_TEXT == newTestEquipment.CalibrationNorm?.ToDefaultString() &&
                itemToUpdate.ALIVE == newTestEquipment.Alive;
        }

        private void AddUseStatistics()
        {
            var usageStatistic = new UsageStatistic()
            {
                ACTION = UsageStatisticActions.SaveCollision("TestEquipment"),
                TIMESTAMP = _timeDataAccess.UtcNow()
            };

            _dbContext.UsageStatistics.Add(usageStatistic);
            _dbContext.SaveChanges();
        }

        private void ApplyCurrentServerStateToOldTestEquipment(TestEquipment oldTestEquipment, DbEntities.TestEquipment itemToUpdate)
        {
            oldTestEquipment.InventoryNumber = new TestEquipmentInventoryNumber(itemToUpdate.USRNO);
            oldTestEquipment.SerialNumber = new TestEquipmentSerialNumber(itemToUpdate.SERNO);
            oldTestEquipment.TestEquipmentModel = new Server.Core.Entities.TestEquipmentModel()
            {
                Id = new TestEquipmentModelId(itemToUpdate.MODELID.GetValueOrDefault())
            };
            oldTestEquipment.Status = new Server.Core.Entities.Status()
            {
                Id = new StatusId(itemToUpdate.STATEID.GetValueOrDefault())
            };
            oldTestEquipment.Alive = itemToUpdate.ALIVE.GetValueOrDefault(false);
            oldTestEquipment.Version = new TestEquipmentVersion(itemToUpdate.VERS);
            oldTestEquipment.LastCalibration = itemToUpdate.LASTCERT;
            oldTestEquipment.CalibrationInterval = new Interval()
            {
                Type = IntervalType.EveryXDays,
                IntervalValue = itemToUpdate.PERIOD.GetValueOrDefault(1)
            };
            oldTestEquipment.TransferUser = itemToUpdate.TRANSFERUSER.GetValueOrDefault(false);
            oldTestEquipment.TransferAdapter = itemToUpdate.TRANSFERADAPTER.GetValueOrDefault(false);
            oldTestEquipment.TransferTransducer = itemToUpdate.TRANSFERTRANSDUCER.GetValueOrDefault(false);
            oldTestEquipment.AskForIdent = itemToUpdate.ASKFORIDENT.GetValueOrDefault(TestEquipmentBehaviourAskForIdent.Never);
            oldTestEquipment.TransferCurves = itemToUpdate.TRANSFERCURVES.GetValueOrDefault(TestEquipmentBehaviourTransferCurves.Never);
            oldTestEquipment.AskForSign = itemToUpdate.ASKFORSIGN.GetValueOrDefault(false);
            oldTestEquipment.DoLoseCheck = itemToUpdate.DOLOSECHECK.GetValueOrDefault(false);
            oldTestEquipment.CanDeleteMeasurements = itemToUpdate.CANDELETEMEASUREMENTS.GetValueOrDefault(false);
            oldTestEquipment.CanUseQstStandard = itemToUpdate.CANUSEQSTSTANDARD.GetValueOrDefault(false);
            oldTestEquipment.ConfirmMeasurements = itemToUpdate.CONFIRMMEASUREMENT.GetValueOrDefault(TestEquipmentBehaviourConfirmMeasurements.Never);
            oldTestEquipment.TransferLocationPictures = itemToUpdate.TRANSFERPICT.GetValueOrDefault(false);
            oldTestEquipment.TransferNewLimits = itemToUpdate.TRANSFERNEWLIMITS.GetValueOrDefault(false);
            oldTestEquipment.TransferAttributes = itemToUpdate.TRANSFERATTRIBUTES.GetValueOrDefault(false);
            oldTestEquipment.UseErrorCodes = itemToUpdate.USEERRORCODES.GetValueOrDefault(false);
            oldTestEquipment.UseForRot = itemToUpdate.ISCTLTEST.GetValueOrDefault(false);
            oldTestEquipment.UseForCtl = itemToUpdate.ISROUTTEST.GetValueOrDefault(false);
            oldTestEquipment.CapacityMax = itemToUpdate.CAPACITYMAX;
            oldTestEquipment.CapacityMin = itemToUpdate.CAPACITYMIN;
            oldTestEquipment.CalibrationNorm = new CalibrationNorm(itemToUpdate.CALIBRATION_NORM_TEXT);
        }

        private void UpdateSingleTestEquipmentWithHistory(TestEquipmentDiff testEquipmentDiff, DbEntities.TestEquipment testEquipmentToUpdate)
        {
            var currentTimestamp = _timeDataAccess.UtcNow();
            var globalHistoryId = _globalHistoryDataAccess.CreateGlobalHistory("DATA UPDATED", currentTimestamp);
            AddSpecificTestEquipmentHistoryEntry(globalHistoryId);

            UpdateSingleTestEquipment(testEquipmentToUpdate, testEquipmentDiff, currentTimestamp);

            var testEquipmentChanges = CreateTestEquipmentChanges(globalHistoryId, testEquipmentDiff);
            AddTestEquipmentChangesForUpdate(testEquipmentDiff, testEquipmentChanges);
            _dbContext.SaveChanges();
        }

        private void UpdateSingleTestEquipment(DbEntities.TestEquipment testEquipmentToUpdate, TestEquipmentDiff testEquipmentDiff, DateTime currentTimestamp)
        {
            UpdateDbTestEquipmentFromTestEquipmentEntity(testEquipmentToUpdate, testEquipmentDiff.GetNewTestEquipment());
            testEquipmentToUpdate.SEQID = testEquipmentToUpdate.SEQID;
            testEquipmentToUpdate.TSA = currentTimestamp;
        }

        private void UpdateDbTestEquipmentFromTestEquipmentEntity(DbEntities.TestEquipment dbTestEquipment, TestEquipment testEquipmentEntity)
        {
            dbTestEquipment.SERNO = testEquipmentEntity.SerialNumber?.ToDefaultString();
            dbTestEquipment.USRNO = testEquipmentEntity.InventoryNumber?.ToDefaultString();
            dbTestEquipment.MODELID = testEquipmentEntity.TestEquipmentModel?.Id?.ToLong();
            dbTestEquipment.ALIVE = testEquipmentEntity.Alive;
            dbTestEquipment.VERS = testEquipmentEntity.Version?.ToDefaultString();
            dbTestEquipment.LASTCERT = testEquipmentEntity.LastCalibration;
            dbTestEquipment.PERIOD = testEquipmentEntity.CalibrationInterval?.IntervalValue;
            dbTestEquipment.STATEID = testEquipmentEntity.Status?.Id?.ToLong();
            dbTestEquipment.TRANSFERUSER = testEquipmentEntity.TransferUser;
            dbTestEquipment.TRANSFERADAPTER = testEquipmentEntity.TransferAdapter;
            dbTestEquipment.TRANSFERTRANSDUCER = testEquipmentEntity.TransferTransducer;
            dbTestEquipment.ASKFORIDENT = testEquipmentEntity.AskForIdent;
            dbTestEquipment.TRANSFERCURVES = testEquipmentEntity.TransferCurves;
            dbTestEquipment.ASKFORSIGN = testEquipmentEntity.AskForSign;
            dbTestEquipment.DOLOSECHECK = testEquipmentEntity.DoLoseCheck;
            dbTestEquipment.CANDELETEMEASUREMENTS = testEquipmentEntity.CanDeleteMeasurements;
            dbTestEquipment.CANUSEQSTSTANDARD = testEquipmentEntity.CanUseQstStandard;
            dbTestEquipment.CONFIRMMEASUREMENT = testEquipmentEntity.ConfirmMeasurements;
            dbTestEquipment.TRANSFERPICT = testEquipmentEntity.TransferLocationPictures;
            dbTestEquipment.TRANSFERNEWLIMITS = testEquipmentEntity.TransferNewLimits;
            dbTestEquipment.TRANSFERATTRIBUTES = testEquipmentEntity.TransferAttributes;
            dbTestEquipment.USEERRORCODES = testEquipmentEntity.UseErrorCodes;
            dbTestEquipment.ISCTLTEST = testEquipmentEntity.UseForCtl;
            dbTestEquipment.ISROUTTEST = testEquipmentEntity.UseForRot;
            dbTestEquipment.CAPACITYMIN = testEquipmentEntity.CapacityMin;
            dbTestEquipment.CAPACITYMAX = testEquipmentEntity.CapacityMax;
            dbTestEquipment.CALIBRATION_NORM_TEXT = testEquipmentEntity.CalibrationNorm?.ToDefaultString();
        }

        private void AddTestEquipmentChangesForUpdate(TestEquipmentDiff testEquipmentDiff, TestEquipmentChanges change)
        {
            var oldTestEquipment = testEquipmentDiff.GetOldTestEquipment();
            var newTestEquipment = testEquipmentDiff.GetNewTestEquipment();

            change.ACTION = "UPDATE";
            change.SERNOOLD = oldTestEquipment.SerialNumber?.ToDefaultString();
            change.SERNONEW = newTestEquipment.SerialNumber?.ToDefaultString();
            change.USRNOOLD = oldTestEquipment.InventoryNumber?.ToDefaultString();
            change.USRNONEW = newTestEquipment.InventoryNumber?.ToDefaultString();
            change.MODELIDOLD = oldTestEquipment.TestEquipmentModel?.Id?.ToLong();
            change.MODELIDNEW = newTestEquipment.TestEquipmentModel?.Id?.ToLong();
            change.ALIVEOLD = oldTestEquipment.Alive;
            change.ALIVENEW = newTestEquipment.Alive;
            change.STATEIDOLD = oldTestEquipment.Status?.Id?.ToLong();
            change.STATEIDNEW = newTestEquipment.Status?.Id?.ToLong();
            change.VERSOLD = oldTestEquipment.Version?.ToDefaultString();
            change.VERSNEW = newTestEquipment.Version?.ToDefaultString();
            change.LASTCERTOLD = oldTestEquipment.LastCalibration;
            change.LASTCERTNEW = newTestEquipment.LastCalibration;
            change.PERIODOLD = oldTestEquipment.CalibrationInterval?.IntervalValue;
            change.PERIODNEW = newTestEquipment.CalibrationInterval?.IntervalValue;
            change.TRANSFERUSEROLD = oldTestEquipment.TransferUser;
            change.TRANSFERUSERNEW = newTestEquipment.TransferUser;
            change.TRANSFERADAPTEROLD = oldTestEquipment.TransferAdapter;
            change.TRANSFERADAPTERNEW = newTestEquipment.TransferAdapter;
            change.TRANSFERTRANSDUCEROLD = oldTestEquipment.TransferTransducer;
            change.TRANSFERTRANSDUCERNEW = newTestEquipment.TransferTransducer;
            change.ASKFORIDENTOLD = oldTestEquipment.AskForIdent;
            change.ASKFORIDENTNEW = newTestEquipment.AskForIdent;
            change.TRANSFERCURVESOLD = oldTestEquipment.TransferCurves;
            change.TRANSFERCURVESNEW = newTestEquipment.TransferCurves;
            change.ASKFORSIGNOLD = oldTestEquipment.AskForSign;
            change.ASKFORSIGNNEW = newTestEquipment.AskForSign;
            change.DOLOSECHECKOLD = oldTestEquipment.DoLoseCheck;
            change.DOLOSECHECKNEW = newTestEquipment.DoLoseCheck;
            change.CANDELETEMEASUREMENTSOLD = oldTestEquipment.CanDeleteMeasurements;
            change.CANDELETEMEASUREMENTSNEW = newTestEquipment.CanDeleteMeasurements;
            change.CANUSEQSTSTANDARDOLD = oldTestEquipment.CanUseQstStandard;
            change.CANUSEQSTSTANDARDNEW = newTestEquipment.CanUseQstStandard;
            change.CONFIRMMEASUREMENTOLD = oldTestEquipment.ConfirmMeasurements;
            change.CONFIRMMEASUREMENTNEW = newTestEquipment.ConfirmMeasurements;
            change.TRANSFERPICTOLD = oldTestEquipment.TransferLocationPictures;
            change.TRANSFERPICTNEW = newTestEquipment.TransferLocationPictures;
            change.TRANSFERNEWLIMITSOLD = oldTestEquipment.TransferNewLimits;
            change.TRANSFERNEWLIMITSNEW = newTestEquipment.TransferNewLimits;
            change.TRANSFERATTRIBUTESOLD = oldTestEquipment.TransferAttributes;
            change.TRANSFERATTRIBUTESNEW = newTestEquipment.TransferAttributes;
            change.USEERRORCODESOLD = oldTestEquipment.UseErrorCodes;
            change.USEERRORCODESNEW = newTestEquipment.UseErrorCodes;
            change.ISCTLTESTOLD = oldTestEquipment.UseForCtl;
            change.ISCTLTESTNEW = newTestEquipment.UseForCtl;
            change.ISROUTTESTOLD = oldTestEquipment.UseForRot;
            change.ISROUTTESTNEW = newTestEquipment.UseForRot;
            change.CAPACITYMAXOLD = oldTestEquipment.CapacityMax;
            change.CAPACITYMAXNEW = newTestEquipment.CapacityMax;
            change.CAPACITYMINOLD = oldTestEquipment.CapacityMin;
            change.CAPACITYMINNEW = newTestEquipment.CapacityMin;
            change.CALIBRATION_NORM_TEXTOLD = oldTestEquipment.CalibrationNorm?.ToDefaultString();
            change.CALIBRATION_NORM_TEXTNEW = newTestEquipment.CalibrationNorm?.ToDefaultString();

            _dbContext.TestEquipmentChanges.Add(change);
        }

        private TestEquipmentChanges CreateTestEquipmentChanges(long globalHistoryId, TestEquipmentDiff testEquipmentDiff)
        {
            var newTestEquipment = testEquipmentDiff.GetNewTestEquipment();

            var testEquipmentChanges = new TestEquipmentChanges()
            {
                GLOBALHISTORYID = globalHistoryId,
                TESTEQUIPMENTID = newTestEquipment.Id.ToLong(),
                USERID = testEquipmentDiff.GetUser().UserId.ToLong(),
                USERCOMMENT = testEquipmentDiff.GetComment().ToDefaultString()
            };

            return testEquipmentChanges;
        }

        private void AddSpecificTestEquipmentHistoryEntry(long globalHistoryId)
        {
            var testEquipmentHistory = new TestEquipmentHistory() { GLOBALHISTORYID = globalHistoryId };
            _dbContext.TestEquipmentHistories.Add(testEquipmentHistory);
        }
    }
}
