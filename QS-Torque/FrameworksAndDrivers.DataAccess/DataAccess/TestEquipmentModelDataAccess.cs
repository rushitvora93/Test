using System;
using System.Collections.Generic;
using System.Linq;
using Common.Types.Enums;
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
using TestEquipmentModel = Server.Core.Entities.TestEquipmentModel;

namespace FrameworksAndDrivers.DataAccess.DataAccess
{
    public class TestEquipmentModelDataAccess : DataAccessBase, ITestEquipmentModelDataAccess
    {
        private readonly IGlobalHistoryDataAccess _globalHistoryDataAccess;
        private readonly ITimeDataAccess _timeDataAccess;
        private readonly Mapper _mapper = new Mapper();

        public TestEquipmentModelDataAccess(ITransactionDbContext transactionContext, SqliteDbContext dbContext,
            IGlobalHistoryDataAccess globalHistoryDataAccess, ITimeDataAccess timeDataAccess)
            : base(transactionContext, dbContext)
        {
            _globalHistoryDataAccess = globalHistoryDataAccess;
            _timeDataAccess = timeDataAccess;
        }

        private enum AvailableV8TestEquipmentModel
        {
            GwkQuanTec = 10100,
            AltlasCopcoSta6000 = 5400
        }

        public List<TestEquipmentModel> LoadTestEquipmentModels()
        {
            var availableV8TestEquipmentModels = new List<long>
            {
                (long) AvailableV8TestEquipmentModel.GwkQuanTec,
                (long) AvailableV8TestEquipmentModel.AltlasCopcoSta6000
            };

            var dbTestEquipmentModels = _dbContext.TestEquipmentModels
                .Where(x => x.ALIVE == true && availableV8TestEquipmentModels.Contains(x.MODELID))
                .Include(x => x.TestEquipments.Where(t => t.ALIVE == true)).Include(x => x.Manufacturer).ToList();

            var result = new List<TestEquipmentModel>();

            foreach (var testEquipmentModel in dbTestEquipmentModels)
            {
                var model = _mapper.DirectPropertyMapping(testEquipmentModel);
                model.TestEquipments = new List<TestEquipment>();

                foreach (var testEquipment in testEquipmentModel.TestEquipments)
                {
                    var equipment = _mapper.DirectPropertyMapping(testEquipment);
                    model.TestEquipments.Add(equipment);
                }
                result.Add(model);
            }
            
            return result;
        }

        public void UpdateTestEquipmentModelsWithHistory(List<TestEquipmentModelDiff> testEquipmentDiffs)
        {
            foreach (var testEquipmentDiff in testEquipmentDiffs)
            {
                var oldTestEquipmentModel = testEquipmentDiff.GetOldTestEquipmentModel();
                var newTestEquipmentModel = testEquipmentDiff.GetNewTestEquipmentModel();

                var itemToUpdate = _dbContext.TestEquipmentModels.Find(newTestEquipmentModel.Id.ToLong());
                if (!TestEquipmentModelUpdateWithHistoryPreconditions(itemToUpdate, oldTestEquipmentModel))
                {
                    AddUseStatistics();
                    ApplyCurrentServerStateToOldTestEquipmentModel(oldTestEquipmentModel, itemToUpdate);
                }
                UpdateSingleTestEquipmentModelWithHistory(testEquipmentDiff, itemToUpdate);
            }
        }

        public bool IsTestEquipmentModelNameUnique(TestEquipmentModelName name)
        {
            return !_dbContext.TestEquipmentModels.Any(x => x.ALIVE == true && x.NAME == name.ToDefaultString());
        }

        private bool TestEquipmentModelUpdateWithHistoryPreconditions(DbEntities.TestEquipmentModel itemToUpdate, TestEquipmentModel newTestEquipmentModel)
        {
            return
                itemToUpdate.NAME == newTestEquipmentModel.TestEquipmentModelName?.ToDefaultString() &&
                itemToUpdate.TOTESTEQUIPMENT_FILE_PATH == newTestEquipmentModel.CommunicationFilePath?.ToDefaultString() &&
                itemToUpdate.DRIVERPROGRAM_FILE_PATH == newTestEquipmentModel.DriverProgramPath?.ToDefaultString() &&
                itemToUpdate.STATUS_FILE_PATH == newTestEquipmentModel.StatusFilePath?.ToDefaultString() &&
                itemToUpdate.FROMTESTEQUIPMENT_FILE_PATH == newTestEquipmentModel.ResultFilePath?.ToDefaultString() &&
                itemToUpdate.MANUID == newTestEquipmentModel.Manufacturer?.Id?.ToLong() &&
                itemToUpdate.TYPE == newTestEquipmentModel.Type &&
                itemToUpdate.TRANSFERADAPTER == newTestEquipmentModel.TransferAdapter &&
                itemToUpdate.TRANSFERUSER == newTestEquipmentModel.TransferUser &&
                itemToUpdate.TRANSFERATTRIBUTES == newTestEquipmentModel.TransferAttributes &&
                itemToUpdate.ASKFORIDENT == newTestEquipmentModel.AskForIdent &&
                itemToUpdate.ASKFORSIGN == newTestEquipmentModel.AskForSign &&
                itemToUpdate.CONFIRMMEASUREMENT == newTestEquipmentModel.ConfirmMeasurements &&
                itemToUpdate.CANDELETEMEASUREMENTS == newTestEquipmentModel.CanDeleteMeasurements &&
                itemToUpdate.CANUSEQSTSTANDARD == newTestEquipmentModel.CanUseQstStandard &&
                itemToUpdate.DOLOSECHECK == newTestEquipmentModel.DoLoseCheck &&
                itemToUpdate.TRANSFERCURVES == newTestEquipmentModel.TransferCurves &&
                itemToUpdate.TRANSFERNEWLIMITS == newTestEquipmentModel.TransferNewLimits &&
                itemToUpdate.TRANSFERPICTS == newTestEquipmentModel.TransferLocationPictures &&
                itemToUpdate.USEERRORCODES == newTestEquipmentModel.UseErrorCodes &&
                itemToUpdate.TRANSFERTRANSDUCER == newTestEquipmentModel.TransferTransducer &&
                itemToUpdate.USEFORCTL == newTestEquipmentModel.UseForCtl &&
                itemToUpdate.USEFORROT == newTestEquipmentModel.UseForRot &&
                itemToUpdate.DGVERSION.GetValueOrDefault(0) == newTestEquipmentModel.DataGateVersion &&
                itemToUpdate.ALIVE == newTestEquipmentModel.Alive;
        }

        private void AddUseStatistics()
        {
            var usageStatistic = new UsageStatistic()
            {
                ACTION = UsageStatisticActions.SaveCollision("TestEquipmentModel"),
                TIMESTAMP = _timeDataAccess.UtcNow()
            };

            _dbContext.UsageStatistics.Add(usageStatistic);
            _dbContext.SaveChanges();
        }

        private void ApplyCurrentServerStateToOldTestEquipmentModel(TestEquipmentModel oldTestEquipmentModel, DbEntities.TestEquipmentModel itemToUpdate)
        {
            oldTestEquipmentModel.TestEquipmentModelName = new TestEquipmentModelName(itemToUpdate.NAME);
            oldTestEquipmentModel.CommunicationFilePath = new TestEquipmentSetupPath(itemToUpdate.TOTESTEQUIPMENT_FILE_PATH);
            oldTestEquipmentModel.DriverProgramPath = new TestEquipmentSetupPath(itemToUpdate.DRIVERPROGRAM_FILE_PATH);
            oldTestEquipmentModel.StatusFilePath = new TestEquipmentSetupPath(itemToUpdate.STATUS_FILE_PATH);
            oldTestEquipmentModel.ResultFilePath = new TestEquipmentSetupPath(itemToUpdate.FROMTESTEQUIPMENT_FILE_PATH);
            oldTestEquipmentModel.DataGateVersion = itemToUpdate.DGVERSION.GetValueOrDefault(0);
            oldTestEquipmentModel.Manufacturer = new Server.Core.Entities.Manufacturer()
            {
                Id = new ManufacturerId(itemToUpdate.MANUID.GetValueOrDefault())
            };
            oldTestEquipmentModel.Alive = itemToUpdate.ALIVE.GetValueOrDefault(false);
            oldTestEquipmentModel.Type = (TestEquipmentType)itemToUpdate.TYPE;
            oldTestEquipmentModel.TransferUser = itemToUpdate.TRANSFERUSER.GetValueOrDefault(false);
            oldTestEquipmentModel.TransferAdapter = itemToUpdate.TRANSFERADAPTER.GetValueOrDefault(false);
            oldTestEquipmentModel.TransferTransducer = itemToUpdate.TRANSFERTRANSDUCER.GetValueOrDefault(false);
            oldTestEquipmentModel.AskForIdent = itemToUpdate.ASKFORIDENT.GetValueOrDefault(false);
            oldTestEquipmentModel.TransferCurves = itemToUpdate.TRANSFERCURVES.GetValueOrDefault(false);
            oldTestEquipmentModel.AskForSign = itemToUpdate.ASKFORSIGN.GetValueOrDefault(false);
            oldTestEquipmentModel.DoLoseCheck = itemToUpdate.DOLOSECHECK.GetValueOrDefault(false);
            oldTestEquipmentModel.CanDeleteMeasurements = itemToUpdate.CANDELETEMEASUREMENTS.GetValueOrDefault(false);
            oldTestEquipmentModel.CanUseQstStandard = itemToUpdate.CANUSEQSTSTANDARD.GetValueOrDefault(false);
            oldTestEquipmentModel.ConfirmMeasurements = itemToUpdate.CONFIRMMEASUREMENT.GetValueOrDefault(false);
            oldTestEquipmentModel.TransferLocationPictures = itemToUpdate.TRANSFERPICTS.GetValueOrDefault(false);
            oldTestEquipmentModel.TransferNewLimits = itemToUpdate.TRANSFERNEWLIMITS.GetValueOrDefault(false);
            oldTestEquipmentModel.TransferAttributes = itemToUpdate.TRANSFERATTRIBUTES.GetValueOrDefault(false);
            oldTestEquipmentModel.UseErrorCodes = itemToUpdate.USEERRORCODES.GetValueOrDefault(false);
            oldTestEquipmentModel.UseForRot = itemToUpdate.USEFORROT.GetValueOrDefault(false);
            oldTestEquipmentModel.UseForCtl = itemToUpdate.USEFORCTL.GetValueOrDefault(false);
        }

        private void UpdateSingleTestEquipmentModelWithHistory(TestEquipmentModelDiff testEquipmentModelDiff, DbEntities.TestEquipmentModel testEquipmentToUpdate)
        {
            var currentTimestamp = _timeDataAccess.UtcNow();
            var globalHistoryId = _globalHistoryDataAccess.CreateGlobalHistory("DATA UPDATED", currentTimestamp);
            AddSpecificTestEquipmentModelHistoryEntry(globalHistoryId);

            UpdateSingleTestEquipmentModel(testEquipmentToUpdate, testEquipmentModelDiff, currentTimestamp);

            var testEquipmentChanges = CreateTestEquipmentModelChanges(globalHistoryId, testEquipmentModelDiff);
            AddTestEquipmentModelChangesForUpdate(testEquipmentModelDiff, testEquipmentChanges);
            _dbContext.SaveChanges();
        }

        private void UpdateSingleTestEquipmentModel(DbEntities.TestEquipmentModel testEquipmentToUpdate, TestEquipmentModelDiff testEquipmentDiff, DateTime currentTimestamp)
        {
            UpdateDbTestEquipmentModelFromTestEquipmentModelEntity(testEquipmentToUpdate, testEquipmentDiff.GetNewTestEquipmentModel());
            testEquipmentToUpdate.MODELID = testEquipmentToUpdate.MODELID;
            testEquipmentToUpdate.TSA = currentTimestamp;
        }

        private void UpdateDbTestEquipmentModelFromTestEquipmentModelEntity(DbEntities.TestEquipmentModel dbTestEquipmentModel, TestEquipmentModel testEquipmentModelEntity)
        {
            dbTestEquipmentModel.NAME = testEquipmentModelEntity.TestEquipmentModelName.ToDefaultString();
            dbTestEquipmentModel.DRIVERPROGRAM_FILE_PATH = testEquipmentModelEntity.DriverProgramPath?.ToDefaultString();
            dbTestEquipmentModel.TOTESTEQUIPMENT_FILE_PATH = testEquipmentModelEntity.CommunicationFilePath?.ToDefaultString();
            dbTestEquipmentModel.FROMTESTEQUIPMENT_FILE_PATH = testEquipmentModelEntity.ResultFilePath?.ToDefaultString();
            dbTestEquipmentModel.STATUS_FILE_PATH = testEquipmentModelEntity.StatusFilePath?.ToDefaultString();
            dbTestEquipmentModel.MANUID = testEquipmentModelEntity.Manufacturer?.Id?.ToLong();
            dbTestEquipmentModel.ALIVE = testEquipmentModelEntity.Alive;
            dbTestEquipmentModel.TYPE = testEquipmentModelEntity.Type;
            dbTestEquipmentModel.TRANSFERUSER = testEquipmentModelEntity.TransferUser;
            dbTestEquipmentModel.TRANSFERADAPTER = testEquipmentModelEntity.TransferAdapter;
            dbTestEquipmentModel.TRANSFERTRANSDUCER = testEquipmentModelEntity.TransferTransducer;
            dbTestEquipmentModel.ASKFORIDENT = testEquipmentModelEntity.AskForIdent;
            dbTestEquipmentModel.TRANSFERCURVES = testEquipmentModelEntity.TransferCurves;
            dbTestEquipmentModel.ASKFORSIGN = testEquipmentModelEntity.AskForSign;
            dbTestEquipmentModel.DOLOSECHECK = testEquipmentModelEntity.DoLoseCheck;
            dbTestEquipmentModel.CANDELETEMEASUREMENTS = testEquipmentModelEntity.CanDeleteMeasurements;
            dbTestEquipmentModel.CANUSEQSTSTANDARD = testEquipmentModelEntity.CanUseQstStandard;
            dbTestEquipmentModel.CONFIRMMEASUREMENT = testEquipmentModelEntity.ConfirmMeasurements;
            dbTestEquipmentModel.TRANSFERPICTS = testEquipmentModelEntity.TransferLocationPictures;
            dbTestEquipmentModel.TRANSFERNEWLIMITS = testEquipmentModelEntity.TransferNewLimits;
            dbTestEquipmentModel.TRANSFERATTRIBUTES = testEquipmentModelEntity.TransferAttributes;
            dbTestEquipmentModel.USEERRORCODES = testEquipmentModelEntity.UseErrorCodes;
            dbTestEquipmentModel.DGVERSION = testEquipmentModelEntity.DataGateVersion;
            dbTestEquipmentModel.USEFORCTL = testEquipmentModelEntity.UseForCtl;
            dbTestEquipmentModel.USEFORROT = testEquipmentModelEntity.UseForRot;
        }

        private void AddTestEquipmentModelChangesForUpdate(TestEquipmentModelDiff testEquipmentModelDiff, TestEquipmentModelChanges change)
        {
            var oldTestEquipmentModel = testEquipmentModelDiff.GetOldTestEquipmentModel();
            var newTestEquipmentModel = testEquipmentModelDiff.GetNewTestEquipmentModel();

            change.ACTION = "UPDATE";
            change.NAMEOLD = oldTestEquipmentModel.TestEquipmentModelName?.ToDefaultString();
            change.NAMENEW = newTestEquipmentModel.TestEquipmentModelName?.ToDefaultString();
            change.DRIVERPROGRAM_FILE_PATHOLD = oldTestEquipmentModel.DriverProgramPath?.ToDefaultString();
            change.DRIVERPROGRAM_FILE_PATHNEW = newTestEquipmentModel.DriverProgramPath?.ToDefaultString();
            change.TOTESTEQUIPMENT_FILE_PATHOLD = oldTestEquipmentModel.CommunicationFilePath?.ToDefaultString();
            change.TOTESTEQUIPMENT_FILE_PATHNEW = newTestEquipmentModel.CommunicationFilePath?.ToDefaultString();
            change.FROMTESTEQUIPMENT_FILE_PATHOLD = oldTestEquipmentModel.ResultFilePath?.ToDefaultString();
            change.FROMTESTEQUIPMENT_FILE_PATHNEW = newTestEquipmentModel.ResultFilePath?.ToDefaultString();
            change.STATUS_FILE_PATHOLD = oldTestEquipmentModel.StatusFilePath?.ToDefaultString();
            change.STATUS_FILE_PATHNEW = newTestEquipmentModel.StatusFilePath?.ToDefaultString();
            change.MANUIDOLD = oldTestEquipmentModel.Manufacturer?.Id?.ToLong();
            change.MANUIDNEW = newTestEquipmentModel.Manufacturer?.Id?.ToLong();
            change.ALIVEOLD = oldTestEquipmentModel.Alive;
            change.ALIVENEW = newTestEquipmentModel.Alive;
            change.TYPEOLD = oldTestEquipmentModel.Type;
            change.TYPENEW = newTestEquipmentModel.Type;
            change.TRANSFERUSEROLD = oldTestEquipmentModel.TransferUser;
            change.TRANSFERUSERNEW = newTestEquipmentModel.TransferUser;
            change.TRANSFERADAPTEROLD = oldTestEquipmentModel.TransferAdapter;
            change.TRANSFERADAPTERNEW = newTestEquipmentModel.TransferAdapter;
            change.TRANSFERTRANSDUCEROLD = oldTestEquipmentModel.TransferTransducer;
            change.TRANSFERTRANSDUCERNEW = newTestEquipmentModel.TransferTransducer;
            change.ASKFORIDENTOLD = oldTestEquipmentModel.AskForIdent;
            change.ASKFORIDENTNEW = newTestEquipmentModel.AskForIdent;
            change.TRANSFERCURVESOLD = oldTestEquipmentModel.TransferCurves;
            change.TRANSFERCURVESNEW = newTestEquipmentModel.TransferCurves;
            change.ASKFORSIGNOLD = oldTestEquipmentModel.AskForSign;
            change.ASKFORSIGNNEW = newTestEquipmentModel.AskForSign;
            change.DOLOSECHECKOLD = oldTestEquipmentModel.DoLoseCheck;
            change.DOLOSECHECKNEW = newTestEquipmentModel.DoLoseCheck;
            change.CANDELETEMEASUREMENTSOLD = oldTestEquipmentModel.CanDeleteMeasurements;
            change.CANDELETEMEASUREMENTSNEW = newTestEquipmentModel.CanDeleteMeasurements;
            change.CANUSEQSTSTANDARDOLD = oldTestEquipmentModel.CanUseQstStandard;
            change.CANUSEQSTSTANDARDNEW = newTestEquipmentModel.CanUseQstStandard;
            change.CONFIRMMEASUREMENTOLD = oldTestEquipmentModel.ConfirmMeasurements;
            change.CONFIRMMEASUREMENTNEW = newTestEquipmentModel.ConfirmMeasurements;
            change.TRANSFERPICTSOLD = oldTestEquipmentModel.TransferLocationPictures;
            change.TRANSFERPICTSNEW = newTestEquipmentModel.TransferLocationPictures;
            change.TRANSFERNEWLIMITSOLD = oldTestEquipmentModel.TransferNewLimits;
            change.TRANSFERNEWLIMITSNEW = newTestEquipmentModel.TransferNewLimits;
            change.TRANSFERATTRIBUTESOLD = oldTestEquipmentModel.TransferAttributes;
            change.TRANSFERATTRIBUTESNEW = newTestEquipmentModel.TransferAttributes;
            change.USEERRORCODESOLD = oldTestEquipmentModel.UseErrorCodes;
            change.USEERRORCODESNEW = newTestEquipmentModel.UseErrorCodes;
            change.DGVERSIONOLD = oldTestEquipmentModel.DataGateVersion;
            change.DGVERSIONNEW = newTestEquipmentModel.DataGateVersion;
            change.USEFORCTLOLD = oldTestEquipmentModel.UseForCtl;
            change.USEFORCTLNEW = newTestEquipmentModel.UseForCtl;
            change.USEFORROTOLD = oldTestEquipmentModel.UseForRot;
            change.USEFORROTNEW = newTestEquipmentModel.UseForRot;

            _dbContext.TestEquipmentModelChanges.Add(change);
        }

        private TestEquipmentModelChanges CreateTestEquipmentModelChanges(long globalHistoryId, TestEquipmentModelDiff testEquipmentModelDiff)
        {
            var newTestEquipmentModel = testEquipmentModelDiff.GetNewTestEquipmentModel();

            var testEquipmentChanges = new TestEquipmentModelChanges()
            {
                GLOBALHISTORYID = globalHistoryId,
                TESTEQUIPMENTMODELID = newTestEquipmentModel.Id.ToLong(),
                USERID = testEquipmentModelDiff.GetUser().UserId.ToLong(),
                USERCOMMENT = testEquipmentModelDiff.GetComment().ToDefaultString()
            };

            return testEquipmentChanges;
        }

        private void AddSpecificTestEquipmentModelHistoryEntry(long globalHistoryId)
        {
            var testEquipmentHistory = new TestEquipmentModelHistory() { GLOBALHISTORYID = globalHistoryId };
            _dbContext.TestEquipmentModelHistories.Add(testEquipmentHistory);
        }
    }
}
