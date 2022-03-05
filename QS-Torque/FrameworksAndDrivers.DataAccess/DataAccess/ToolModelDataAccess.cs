using System;
using System.Collections.Generic;
using System.Linq;
using Common.Types.Enums;
using Core.Entities;
using Core.Enums;
using Core.UseCases;
using FrameworksAndDrivers.DataAccess.Common;
using FrameworksAndDrivers.DataAccess.DbContext;
using FrameworksAndDrivers.DataAccess.DbEntities;
using FrameworksAndDrivers.DataAccess.T4Mapper;
using Microsoft.EntityFrameworkCore;
using Server.Core.Diffs;
using Server.Core.Entities;
using Server.Core.Entities.ReferenceLink;
using Server.UseCases.UseCases;
using State;
using ToolModel = Server.Core.Entities.ToolModel;

namespace FrameworksAndDrivers.DataAccess.DataAccess
{
    public class ToolModelDataAccess: DataAccessBase, IToolModelDataAccess
    {
        private readonly Mapper _mapper = new Mapper();
        public ToolModelDataAccess(
            ITransactionDbContext transactionContext,
            SqliteDbContext dbContext, ITimeDataAccess time, IGlobalHistoryDataAccess globalHistoryDataAccess)
            : base(transactionContext, dbContext)
        {
            _globalHistory = globalHistoryDataAccess;
            _time = time;
        }

        public List<ToolModel> GetAllToolModels()
        {
            var dbToolModels = _dbContext.ToolModels.Where(toolModel => toolModel.ALIVE == true)
                .Include(x => x.Manufacturer)
                .ToList();

            var qstLists = _dbContext.QstLists
                .Where(
                    x => dbToolModels.Select(m => m.VERSIONID).ToList().Contains(x.LISTID) // ToolType
                        || dbToolModels.Select(m => m.KINDID).ToList().Contains(x.LISTID) // SwitchOff
                        || dbToolModels.Select(m => m.DRIVEID).ToList().Contains(x.LISTID) // DriveType
                        || dbToolModels.Select(m => m.SWITCHID).ToList().Contains(x.LISTID) // ShutOff
                        || dbToolModels.Select(m => m.FORMID).ToList().Contains(x.LISTID) // ConstructionType
                        || dbToolModels.Select(m => m.MEAID).ToList().Contains(x.LISTID) // DriveSize
                        )
                .ToList();

            var mapper = new Mapper();
            var toolModels = new List<ToolModel>();
            foreach (var dbModel in dbToolModels)
            {
                var model = mapper.DirectPropertyMapping(dbModel);

                var toolType = qstLists.SingleOrDefault(x => x.LISTID == dbModel.VERSIONID);
                if (toolType != null)
                {
                    model.ToolType = new ToolType()
                    {
                        ListId = new HelperTableEntityId(toolType.LISTID),
                        Value = new HelperTableEntityValue(toolType.INFO),
                        NodeId = (NodeId)toolType.NODEID,
                        Alive = toolType.ALIVE
                    };
                }

                var switchOff = qstLists.SingleOrDefault(x => x.LISTID == dbModel.KINDID);
                if (switchOff != null)
                {
                    model.SwitchOff = new SwitchOff()
                    {
                        ListId = new HelperTableEntityId(switchOff.LISTID),
                        Value = new HelperTableEntityValue(switchOff.INFO),
                        NodeId = (NodeId)switchOff.NODEID,
                        Alive = switchOff.ALIVE
                    };
                }

                var driveType = qstLists.SingleOrDefault(x => x.LISTID == dbModel.DRIVEID);
                if (driveType != null)
                {
                    model.DriveType = new DriveType()
                    {
                        ListId = new HelperTableEntityId(driveType.LISTID),
                        Value = new HelperTableEntityValue(driveType.INFO),
                        NodeId = (NodeId)driveType.NODEID,
                        Alive = driveType.ALIVE
                    };
                }

                var shutOff = qstLists.SingleOrDefault(x => x.LISTID == dbModel.SWITCHID);
                if (shutOff != null)
                {
                    model.ShutOff = new ShutOff()
                    {
                        ListId = new HelperTableEntityId(shutOff.LISTID),
                        Value = new HelperTableEntityValue(shutOff.INFO),
                        NodeId = (NodeId)shutOff.NODEID,
                        Alive = shutOff.ALIVE
                    };
                }

                var constructionType = qstLists.SingleOrDefault(x => x.LISTID == dbModel.FORMID);
                if (constructionType != null)
                {
                    model.ConstructionType = new ConstructionType()
                    {
                        ListId = new HelperTableEntityId(constructionType.LISTID),
                        Value = new HelperTableEntityValue(constructionType.INFO),
                        NodeId = (NodeId)constructionType.NODEID,
                        Alive = constructionType.ALIVE
                    };
                }

                var driveSize = qstLists.SingleOrDefault(x => x.LISTID == dbModel.MEAID);
                if (driveSize != null)
                {
                    model.DriveSize = new DriveSize()
                    {
                        ListId = new HelperTableEntityId(driveSize.LISTID),
                        Value = new HelperTableEntityValue(driveSize.INFO),
                        NodeId = (NodeId)driveSize.NODEID,
                        Alive = driveSize.ALIVE
                    };
                }

                toolModels.Add(model);
            }

            return toolModels;
        }

        public List<ToolModel> InsertToolModels(List<ToolModelDiff> toolModelDiffs)
        {
            var insertedToolModels = new List<ToolModel>();
            toolModelDiffs.ForEach(toolModelDiff => InsertSingleToolModelWithHistory(toolModelDiff, insertedToolModels));
            return insertedToolModels;
        }

        public List<ToolModel> UpdateToolModels(List<ToolModelDiff> toolModelDiffs)
        {
            var leftOvers = new List<ToolModel>();
            foreach (var toolModelDiff in toolModelDiffs)
            {
                var oldToolModel = toolModelDiff.GetOld();
                var newToolModel = toolModelDiff.GetNew();
                var dbToolModelToUpdate = _dbContext.ToolModels.Find(newToolModel.Id.ToLong());
                if (!ToolModelUpdateWithHistoryPreconditions(dbToolModelToUpdate, newToolModel))
                {
                    AddSaveCollisionToUsageStatistics();
                    ApplyCurrentServerStateToOldToolModel(oldToolModel, dbToolModelToUpdate);
                    leftOvers.Add(new Mapper().DirectPropertyMapping(dbToolModelToUpdate));
                }
                UpdateSingleToolModelWithHistory(toolModelDiff, dbToolModelToUpdate);
            }

            return leftOvers;
        }

        public List<ToolReferenceLink> GetReferencedToolLinks(ToolModelId toolModelId)
        {
            var dbTools = _dbContext.Tools.Where(
                 tool =>
                     tool.MODELID == toolModelId.ToLong()
                     && tool.ALIVE == true).ToList();
            var toolLinks = new List<ToolReferenceLink>();
            foreach (var dbTool in dbTools)
            {
                toolLinks.Add(
                    new ToolReferenceLink(
                        new QstIdentifier(dbTool.SEQID), 
                        dbTool.USERNO, 
                        dbTool.SERIALNO));
            }
            return toolLinks;
        }

        private void InsertSingleToolModelWithHistory(ToolModelDiff toolModelDiff, List<ToolModel> insertedToolModels)
        {
            var currentTImestamp = _time.UtcNow();
            var globalHistoryId = _globalHistory.CreateGlobalHistory("DATA INSERTED", currentTImestamp);
            AddSpecificToolModelHistoryEntry(globalHistoryId);
            InsertSingleToolModel(toolModelDiff.GetNew(), currentTImestamp, insertedToolModels);
            var toolModelChanges = CreateToolModelChanges(globalHistoryId, toolModelDiff);
            AddToolModelChangesForInsert(toolModelDiff, toolModelChanges);
            _dbContext.SaveChanges();
        }

        private void InsertSingleToolModel(
            ToolModel toolModel,
            DateTime currentTimestamp,
            List<ToolModel> insertedToolModels)
        {
            var dbToolModel = new Mapper().DirectPropertyMapping(toolModel);
            dbToolModel.ALIVE = true;
            dbToolModel.TSN = currentTimestamp;
            dbToolModel.TSA = currentTimestamp;
            _dbContext.ToolModels.Add(dbToolModel);
            _dbContext.SaveChanges();
            toolModel.Id = new ToolModelId(dbToolModel.MODELID);
            insertedToolModels.Add(toolModel);
        }

        private void AddToolModelChangesForInsert(ToolModelDiff toolModelDiff, ToolModelChanges toolModelChanges)
        {
            var newToolModel = toolModelDiff.GetNew();

            toolModelChanges.ACTION = "INSERT";

            toolModelChanges.MODELOLD = null;
            toolModelChanges.MODELNEW = newToolModel.Description;
            toolModelChanges.TYPEIDOLD = null;
            toolModelChanges.TYPEIDNEW = (long)newToolModel.ModelType;
            toolModelChanges.CLASSIDOLD = null;
            toolModelChanges.CLASSIDNEW = (long)newToolModel.Class;
            toolModelChanges.LIMITLOOLD = null;
            toolModelChanges.LIMITLONEW = newToolModel.MinPower;
            toolModelChanges.LIMITHIOLD = null;
            toolModelChanges.LIMITHINEW = newToolModel.MaxPower;
            toolModelChanges.PRESSUREOLD = null;
            toolModelChanges.PRESSURENEW = newToolModel.AirPressure;
            toolModelChanges.WEIGHTOLD = null;
            toolModelChanges.WEIGHTNEW = newToolModel.Weight;
            toolModelChanges.VOLTOLD = null;
            toolModelChanges.VOLTNEW = newToolModel.BatteryVoltage;
            toolModelChanges.TURNOLD = null;
            toolModelChanges.TURNNEW = newToolModel.MaxRotationSpeed;
            toolModelChanges.AIROLD = null;
            toolModelChanges.AIRNEW = newToolModel.AirConsumption;
            toolModelChanges.VERSIONIDOLD = null;
            toolModelChanges.VERSIONIDNEW = newToolModel.ToolType?.ListId?.ToLong();
            toolModelChanges.KINDIDOLD = null;
            toolModelChanges.KINDIDNEW = newToolModel.SwitchOff?.ListId?.ToLong();
            toolModelChanges.DRIVEIDOLD = null;
            toolModelChanges.DRIVEIDNEW = newToolModel.DriveType?.ListId?.ToLong();
            toolModelChanges.SWITCHIDOLD = null;
            toolModelChanges.SWITCHIDNEW = newToolModel.ShutOff?.ListId?.ToLong();
            toolModelChanges.FORMIDOLD = null;
            toolModelChanges.FORMIDNEW = newToolModel.ConstructionType?.ListId?.ToLong();
            toolModelChanges.MEAIDOLD = null;
            toolModelChanges.MEAIDNEW = newToolModel.DriveSize?.ListId?.ToLong();
            toolModelChanges.ALIVEOLD = null;
            toolModelChanges.ALIVENEW = true;

            _dbContext.ToolModelChanges.Add(toolModelChanges);
        }

        private ToolModelChanges CreateToolModelChanges(long globalHistoryId, ToolModelDiff toolModelDiff)
        {
            var newToolModel = toolModelDiff.GetNew();
            var toolModelChanges = new ToolModelChanges
            {
                GLOBALHISTORYID = globalHistoryId,
                TOOLMODELID = newToolModel.Id.ToLong(),
                USERID = toolModelDiff.GetUser().UserId.ToLong(),
                USERCOMMENT = toolModelDiff.GetComment().ToDefaultString()
            };
            return toolModelChanges;
        }

        private void AddSpecificToolModelHistoryEntry(long globalHistoryId)
        {
            var toolModelHistory = new ToolModelHistory {GLOBALHISTORYID = globalHistoryId};
            _dbContext.ToolModelHistories.Add(toolModelHistory);
        }

        private bool ToolModelUpdateWithHistoryPreconditions(
            DbEntities.ToolModel dbToolModelToUpdate,
            ToolModel newToolModel)
        {
            return
                dbToolModelToUpdate.ALIVE == newToolModel.Alive
                && dbToolModelToUpdate.MODEL == newToolModel.Description
                && dbToolModelToUpdate.TYPEID == (long)newToolModel.ModelType
                && dbToolModelToUpdate.CLASSID == (long)newToolModel.Class
                && dbToolModelToUpdate.LIMITLO == newToolModel.MinPower
                && dbToolModelToUpdate.LIMITHI == newToolModel.MaxPower
                && dbToolModelToUpdate.PRESSURE == newToolModel.AirPressure
                && dbToolModelToUpdate.WEIGHT == newToolModel.Weight
                && dbToolModelToUpdate.VOLT == newToolModel.BatteryVoltage
                && dbToolModelToUpdate.TURN == newToolModel.MaxRotationSpeed
                && dbToolModelToUpdate.AIR == newToolModel.AirConsumption
                && dbToolModelToUpdate.VERSIONID == newToolModel.ToolType?.ListId?.ToLong()
                && dbToolModelToUpdate.KINDID == newToolModel.SwitchOff?.ListId?.ToLong()
                && dbToolModelToUpdate.DRIVEID == newToolModel.DriveType?.ListId?.ToLong()
                && dbToolModelToUpdate.SWITCHID == newToolModel.ShutOff?.ListId?.ToLong()
                && dbToolModelToUpdate.FORMID == newToolModel.ConstructionType?.ListId?.ToLong()
                && dbToolModelToUpdate.MEAID == newToolModel.DriveSize?.ListId?.ToLong();
        }

        private void AddSaveCollisionToUsageStatistics()
        {
            var usageStatistic = new UsageStatistic
            {
                ACTION = UsageStatisticActions.SaveCollision("ToolModel"),
                TIMESTAMP = _time.UtcNow()
            };
            _dbContext.UsageStatistics.Add(usageStatistic);
            _dbContext.SaveChanges();
        }

        private void ApplyCurrentServerStateToOldToolModel(
            ToolModel oldToolModel,
            DbEntities.ToolModel dbToolModelToUpdate)
        {
            oldToolModel.Description = dbToolModelToUpdate.MODEL;
            oldToolModel.ModelType = (ToolModelType)dbToolModelToUpdate.TYPEID;
            oldToolModel.Class = (ToolModelClass)dbToolModelToUpdate.CLASSID;
            oldToolModel.MinPower = dbToolModelToUpdate.LIMITLO;
            oldToolModel.MaxPower = dbToolModelToUpdate.LIMITHI;
            oldToolModel.AirPressure = dbToolModelToUpdate.PRESSURE;
            oldToolModel.Weight = dbToolModelToUpdate.WEIGHT;
            oldToolModel.BatteryVoltage = dbToolModelToUpdate.VOLT;
            oldToolModel.MaxRotationSpeed = dbToolModelToUpdate.TURN;
            oldToolModel.AirConsumption = dbToolModelToUpdate.AIR;
            oldToolModel.ToolType = new ToolType(){ListId = new HelperTableEntityId(dbToolModelToUpdate.VERSIONID.GetValueOrDefault(-1)), NodeId = NodeId.ToolType};
            oldToolModel.SwitchOff = new SwitchOff() { ListId = new HelperTableEntityId(dbToolModelToUpdate.KINDID.GetValueOrDefault(-1)), NodeId = NodeId.SwitchOff };
            oldToolModel.DriveType = new DriveType() { ListId = new HelperTableEntityId(dbToolModelToUpdate.DRIVEID.GetValueOrDefault(-1)), NodeId = NodeId.DriveType };
            oldToolModel.ShutOff = new ShutOff() { ListId = new HelperTableEntityId(dbToolModelToUpdate.SWITCHID.GetValueOrDefault(-1)), NodeId = NodeId.ShutOff };
            oldToolModel.ConstructionType = new ConstructionType() { ListId = new HelperTableEntityId(dbToolModelToUpdate.FORMID.GetValueOrDefault(-1)), NodeId = NodeId.ConstructionType };
            oldToolModel.DriveSize = new DriveSize() { ListId = new HelperTableEntityId(dbToolModelToUpdate.MEAID.GetValueOrDefault(-1)), NodeId = NodeId.DriveSize };
            oldToolModel.Alive = dbToolModelToUpdate.ALIVE.GetValueOrDefault(false);
        }

        private void UpdateSingleToolModelWithHistory(
            ToolModelDiff toolModelDiff,
            DbEntities.ToolModel dbToolModelToUpdate)
        {
            var currentTimestamp = _time.UtcNow();
            var globalHistoryId = _globalHistory.CreateGlobalHistory("DATA UPDATED", currentTimestamp);
            AddSpecificToolModelHistoryEntry(globalHistoryId);
            UpdateSingleToolModel(dbToolModelToUpdate, toolModelDiff, currentTimestamp);
            var toolModelChanges = CreateToolModelChanges(globalHistoryId, toolModelDiff);
            AddToolModelChangesForUpdate(toolModelDiff, toolModelChanges);
            _dbContext.SaveChanges();
        }

        private void UpdateSingleToolModel(
            DbEntities.ToolModel dbToolModelToUpdate,
            ToolModelDiff toolModelDiff,
            DateTime currentTimestamp)
        {
            UpdateDbToolModelFromToolModelEntity(dbToolModelToUpdate, toolModelDiff.GetNew());
            dbToolModelToUpdate.TSA = currentTimestamp;
        }

        private void UpdateDbToolModelFromToolModelEntity(DbEntities.ToolModel dbToolModel, ToolModel toolModelEntity)
        {
            dbToolModel.MODEL = toolModelEntity.Description;
            dbToolModel.TYPEID = (long)toolModelEntity.ModelType;
            dbToolModel.CLASSID = (long)toolModelEntity.Class;
            dbToolModel.LIMITLO = toolModelEntity.MinPower;
            dbToolModel.LIMITHI = toolModelEntity.MaxPower;
            dbToolModel.PRESSURE = toolModelEntity.AirPressure;
            dbToolModel.WEIGHT = toolModelEntity.Weight;
            dbToolModel.VOLT = toolModelEntity.BatteryVoltage;
            dbToolModel.TURN = toolModelEntity.MaxRotationSpeed;
            dbToolModel.AIR = toolModelEntity.AirConsumption;
            dbToolModel.ALIVE = toolModelEntity.Alive;
            dbToolModel.VERSIONID = toolModelEntity.ToolType?.ListId?.ToLong();
            dbToolModel.KINDID = toolModelEntity.SwitchOff?.ListId?.ToLong();
            dbToolModel.DRIVEID = toolModelEntity.DriveType?.ListId?.ToLong();
            dbToolModel.SWITCHID = toolModelEntity.ShutOff?.ListId?.ToLong();
            dbToolModel.FORMID = toolModelEntity.ConstructionType?.ListId?.ToLong();
            dbToolModel.MEAID = toolModelEntity.DriveSize?.ListId?.ToLong();
            dbToolModel.Manufacturer = _dbContext.Find<DbEntities.Manufacturer>(toolModelEntity.Manufacturer.Id.ToLong());
        }

        private void AddToolModelChangesForUpdate(ToolModelDiff toolModelDiff, ToolModelChanges toolModelChanges)
        {
            var oldToolModel = toolModelDiff.GetOld();
            var newToolModel = toolModelDiff.GetNew();

            toolModelChanges.ACTION = "UPDATE";

            toolModelChanges.MODELOLD = oldToolModel.Description;
            toolModelChanges.MODELNEW = newToolModel.Description;
            toolModelChanges.TYPEIDOLD = (long)oldToolModel.ModelType;
            toolModelChanges.TYPEIDNEW = (long)newToolModel.ModelType;
            toolModelChanges.CLASSIDOLD = (long)oldToolModel.Class;
            toolModelChanges.CLASSIDNEW = (long)newToolModel.Class;
            toolModelChanges.LIMITLOOLD = oldToolModel.MinPower;
            toolModelChanges.LIMITLONEW = newToolModel.MinPower;
            toolModelChanges.LIMITHIOLD = oldToolModel.MaxPower;
            toolModelChanges.LIMITHINEW = newToolModel.MaxPower;
            toolModelChanges.PRESSUREOLD = oldToolModel.AirPressure;
            toolModelChanges.PRESSURENEW = newToolModel.AirPressure;
            toolModelChanges.WEIGHTOLD = oldToolModel.Weight;
            toolModelChanges.WEIGHTNEW = newToolModel.Weight;
            toolModelChanges.VOLTOLD = oldToolModel.BatteryVoltage;
            toolModelChanges.VOLTNEW = newToolModel.BatteryVoltage;
            toolModelChanges.TURNOLD = oldToolModel.MaxRotationSpeed;
            toolModelChanges.TURNNEW = newToolModel.MaxRotationSpeed;
            toolModelChanges.AIROLD = oldToolModel.AirConsumption;
            toolModelChanges.AIRNEW = newToolModel.AirConsumption;
            toolModelChanges.ALIVEOLD = oldToolModel.Alive;
            toolModelChanges.ALIVENEW = newToolModel.Alive;
            toolModelChanges.MANUIDOLD = oldToolModel.Manufacturer.Id.ToLong();
            toolModelChanges.MANUIDNEW = newToolModel.Manufacturer.Id.ToLong();
            toolModelChanges.VERSIONIDOLD = oldToolModel.ToolType?.ListId?.ToLong(); 
            toolModelChanges.VERSIONIDNEW = newToolModel.ToolType?.ListId?.ToLong();
            toolModelChanges.KINDIDOLD = oldToolModel.SwitchOff?.ListId?.ToLong(); 
            toolModelChanges.KINDIDNEW = newToolModel.SwitchOff?.ListId?.ToLong();
            toolModelChanges.DRIVEIDOLD = oldToolModel.DriveType?.ListId?.ToLong(); 
            toolModelChanges.DRIVEIDNEW = newToolModel.DriveType?.ListId?.ToLong();
            toolModelChanges.SWITCHIDOLD = oldToolModel.ShutOff?.ListId?.ToLong(); 
            toolModelChanges.SWITCHIDNEW = newToolModel.ShutOff?.ListId?.ToLong();
            toolModelChanges.FORMIDOLD = oldToolModel.ConstructionType?.ListId?.ToLong(); 
            toolModelChanges.FORMIDNEW = newToolModel.ConstructionType?.ListId?.ToLong();
            toolModelChanges.MEAIDOLD = oldToolModel.DriveSize?.ListId?.ToLong(); 
            toolModelChanges.MEAIDNEW = newToolModel.DriveSize?.ListId?.ToLong();

            _dbContext.ToolModelChanges.Add(toolModelChanges);
        }

        private readonly ITimeDataAccess _time;
        private readonly IGlobalHistoryDataAccess _globalHistory;

        public List<ToolModel> LoadDeletedToolModels()
        {
            var toolModels = new List<ToolModel>();

            var dbExtensions = _dbContext.ToolModels.Where(x => x.ALIVE == false).ToList();

            dbExtensions.ForEach(db => toolModels.Add(_mapper.DirectPropertyMapping(db)));

            return toolModels;
        }
    }
}
