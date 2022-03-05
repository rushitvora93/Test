using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
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
using Tool = Server.Core.Entities.Tool;
using ToolModel = Server.Core.Entities.ToolModel;

namespace FrameworksAndDrivers.DataAccess.DataAccess
{
    public class ToolDataAccess : DataAccessBase, IToolDataAccess
    {
        private readonly IGlobalHistoryDataAccess _globalHistoryDataAccess;
        private readonly ITimeDataAccess _timeDataAccess;
        private readonly Mapper _mapper = new Mapper();

        public ToolDataAccess(SqliteDbContext dbContext, ITransactionDbContext transactionContext,
            IGlobalHistoryDataAccess globalHistoryDataAccess, ITimeDataAccess timeDataAccess)
            : base(transactionContext, dbContext)
        {
            _globalHistoryDataAccess = globalHistoryDataAccess;
            _timeDataAccess = timeDataAccess;
        }

        public List<Tool> LoadTools(int index, int size)
        {
            List<DbEntities.Tool> dbTools;

            if (size > 0)
            {
                dbTools = _dbContext.Tools
                    .Where(x => x.ALIVE == true)
                    .OrderBy(m => m.SEQID)
                    .Skip(index)
                    .Take(size)
                    .ToList();
            }
            else
            {
                dbTools = _dbContext.Tools
                    .Where(x => x.ALIVE == true)
                    .OrderBy(m => m.SEQID)
                    .Skip(index)
                    .ToList();
            }

            var tools = new List<Tool>();

            dbTools.ForEach(db => tools.Add(_mapper.DirectPropertyMapping(db)));

            return tools;
        }

        public Tool GetToolById(ToolId toolId)
        {
            var tool = _dbContext.Tools.Find(toolId.ToLong());
            if (tool == null)
            {
                return null;
            }

            return _mapper.DirectPropertyMapping(tool);
        }

        public List<LocationToolAssignmentReferenceLink> GetLocationToolAssignmentLinkForTool(ToolId id)
        {
            var links = (
                from lp in _dbContext.LocPows
                where lp.PowId == id.ToLong() && lp.Alive == true
                join loc in _dbContext.Locations
                    on lp.LocId equals loc.SEQID
                join t in _dbContext.Tools
                    on lp.PowId equals t.SEQID
                select GetLocationToolAssignment(lp, loc, t)
            ).ToList();

            return links;
        }

        private static LocationToolAssignmentReferenceLink GetLocationToolAssignment(DbEntities.LocPow lp, DbEntities.Location loc, DbEntities.Tool tool)
        {
            return new LocationToolAssignmentReferenceLink(new QstIdentifier(lp.LocPowId),
                new LocationDescription(loc.NAME), new LocationNumber(loc.USERID), tool.SERIALNO, tool.USERNO,
                new LocationId(loc.SEQID), new ToolId(tool.SEQID));
        }


        public bool IsSerialNumberUnique(string serialNumber)
        {
            var count = _dbContext.Tools.Where(x => x.ALIVE == true).Count(x => x.SERIALNO == serialNumber);
            return count == 0;
        }

        public bool IsInventoryNumberUnique(string inventoryNumber)
        {
            var count = _dbContext.Tools.Where(x => x.ALIVE == true).Count(x => x.USERNO == inventoryNumber);
            return count == 0;
        }

        public List<Tool> LoadToolsForModel(ToolModelId toolModelId)
        {
            var entitys = _dbContext.Tools
                .Where(x => x.ALIVE == true && x.MODELID == toolModelId.ToLong())
                .Include(x => x.ToolModel)
                .ThenInclude(x => x.Manufacturer)
                .Include(x => x.Status)
                .ToList();

            var qstLists = _dbContext.QstLists
                .ToList();

            var tools = new List<Tool>();

            entitys.ForEach(x => tools.Add(item: CreateToolFromDb(x, qstLists)));

            return tools;
        }

        public List<ToolModel> LoadModelsWithAtLeasOneTool()
        {
            var entities = _dbContext.ToolModels
                .Include(x => x.Tools.Where(t => t.ALIVE == true))
                .Include(x => x.Manufacturer)
                .Where(x => x.ALIVE == true && x.Tools.Any(t => t.ALIVE == true))
                .ToList();

            var toolModels = new List<ToolModel>();
            entities.ForEach(x => toolModels.Add(_mapper.DirectPropertyMapping(x)));
            return toolModels;
        }

        public static Tool CreateToolFromDb(DbEntities.Tool toolDb, List<QstList> lists)
        {
            var tool = new Tool();
            tool.Id = new ToolId(toolDb.SEQID);
            tool.SerialNumber = toolDb.SERIALNO;
            tool.InventoryNumber = toolDb.USERNO;
            tool.Accessory = toolDb.PTACCESS;
            tool.AdditionalConfigurableField1 = new ConfigurableFieldString40(toolDb.FREE_STR1);
            tool.AdditionalConfigurableField2 = new ConfigurableFieldString80(toolDb.FREE_STR2);
            tool.AdditionalConfigurableField3 = new ConfigurableFieldString250(toolDb.FREE_STR3);
            tool.Alive = toolDb.ALIVE.GetValueOrDefault(false);

            var configurableField = lists.SingleOrDefault(x => x.LISTID == toolDb.ORDERID);
            if (configurableField != null)
            {
                tool.ConfigurableField = new ConfigurableField()
                {
                    ListId = new HelperTableEntityId(configurableField.LISTID),
                    Value = new HelperTableEntityValue(configurableField.INFO),
                    NodeId = (NodeId) configurableField.NODEID,
                    Alive = configurableField.ALIVE
                };
            }

            var costCenter = lists.SingleOrDefault(x => x.LISTID == toolDb.KOSTID);
            if (costCenter != null)
            {
                tool.CostCenter = new CostCenter()
                {
                    ListId = new HelperTableEntityId(costCenter.LISTID),
                    Value = new HelperTableEntityValue(costCenter.INFO),
                    NodeId = (NodeId)costCenter.NODEID,
                    Alive = costCenter.ALIVE
                };
            }

            var mapper = new Mapper();
            if (toolDb.Status != null)
            {
                tool.Status = mapper.DirectPropertyMapping(toolDb.Status);
            }

            if (toolDb.ToolModel != null)
            {
                tool.ToolModel = CreateToolModelFromDb(toolDb.ToolModel, lists);
            }

            return tool;
        }

        public List<Tool> InsertToolsWithHistory(List<ToolDiff> toolDiffs, bool returnList)
        {
            var insertedTools = new List<Tool>();
            foreach (var toolDiff in toolDiffs)
            {
                InsertSingleToolWithHistory(toolDiff, insertedTools);
            }

            return returnList ? insertedTools : null;
        }

        private void InsertSingleToolWithHistory(ToolDiff toolDiff, List<Tool> insertedTools)
        {
            var currentTimestamp = _timeDataAccess.UtcNow();
            var globalHistoryId = _globalHistoryDataAccess.CreateGlobalHistory("DATA INSERTED", currentTimestamp);
            AddSpecificToolHistoryEntry(globalHistoryId);

            InsertSingleTool(toolDiff.GetNewTool(), currentTimestamp, insertedTools);

            var toolChanges = CreateToolChanges(globalHistoryId, toolDiff);
            AddToolChangesForInsert(toolDiff, toolChanges);

            _dbContext.SaveChanges();
        }

        private void InsertSingleTool(Tool tool, DateTime currentTimestamp, List<Tool> insertedTools)
        {
            var dbTool = _mapper.DirectPropertyMapping(tool);
            dbTool.ALIVE = true;
            dbTool.TSN = currentTimestamp;
            dbTool.TSA = currentTimestamp;
            _dbContext.Tools.Add(dbTool);
            _dbContext.SaveChanges();

            tool.Id = new ToolId(dbTool.SEQID);
            insertedTools.Add(tool);
        }

        private void AddToolChangesForInsert(ToolDiff toolDiff, ToolChanges changes)
        {
            var newTool = toolDiff.GetNewTool();

            changes.ACTION = "INSERT";
            changes.ALIVENEW = true;
            changes.SERIALNONEW = newTool.SerialNumber;
            changes.USERNONEW = newTool.InventoryNumber;
            changes.MODELIDNEW = newTool.ToolModel?.Id?.ToLong();
            changes.STATEIDNEW = newTool.Status?.Id?.ToLong();
            changes.PTACCESSNEW = newTool.Accessory;
            changes.ORDERIDNEW = newTool.ConfigurableField?.ListId?.ToLong();
            changes.KOSTIDNEW = newTool.CostCenter?.ListId?.ToLong();
            changes.FREE_STR1NEW = newTool.AdditionalConfigurableField1?.ToDefaultString();
            changes.FREE_STR2NEW = newTool.AdditionalConfigurableField2?.ToDefaultString();
            changes.FREE_STR3NEW = newTool.AdditionalConfigurableField3?.ToDefaultString();

            _dbContext.ToolChanges.Add(changes);
        }

        public List<Tool> UpdateToolsWithHistory(List<ToolDiff> toolDiffs)
        {
            var leftOvers = new List<Tool>();
            foreach (var toolDiff in toolDiffs)
            {
                var oldTool = toolDiff.GetOldTool();
                var newTool = toolDiff.GetNewTool();

                var itemToUpdate = _dbContext.Tools.Find(newTool.Id.ToLong());
                if (!ToolUpdateWithHistoryPreconditions(itemToUpdate, oldTool))
                {
                    AddUseStatistics();
                    ApplyCurrentServerStateToOldTool(oldTool, itemToUpdate);
                    leftOvers.Add(_mapper.DirectPropertyMapping(itemToUpdate));
                }
                UpdateSingleToolWithHistory(toolDiff, itemToUpdate);
            }

            return leftOvers;
        }

        private static ToolModel CreateToolModelFromDb(DbEntities.ToolModel toolModelDb, List<QstList> qstLists)
        {
            var mapper = new Mapper();
            var toolModel = mapper.DirectPropertyMapping(toolModelDb);

            var toolType = qstLists.SingleOrDefault(x => x.LISTID == toolModelDb.VERSIONID);
            if (toolType != null)
            {
                toolModel.ToolType = new ToolType()
                {
                    ListId = new HelperTableEntityId(toolType.LISTID),
                    Value = new HelperTableEntityValue(toolType.INFO),
                    NodeId = (NodeId)toolType.NODEID,
                    Alive = toolType.ALIVE
                };
            }

            var switchOff = qstLists.SingleOrDefault(x => x.LISTID == toolModelDb.KINDID);
            if (switchOff != null)
            {
                toolModel.SwitchOff = new SwitchOff()
                {
                    ListId = new HelperTableEntityId(switchOff.LISTID),
                    Value = new HelperTableEntityValue(switchOff.INFO),
                    NodeId = (NodeId)switchOff.NODEID,
                    Alive = switchOff.ALIVE
                };
            }

            var driveType = qstLists.SingleOrDefault(x => x.LISTID == toolModelDb.DRIVEID);
            if (driveType != null)
            {
                toolModel.DriveType = new DriveType()
                {
                    ListId = new HelperTableEntityId(driveType.LISTID),
                    Value = new HelperTableEntityValue(driveType.INFO),
                    NodeId = (NodeId)driveType.NODEID,
                    Alive = driveType.ALIVE
                };
            }

            var shutOff = qstLists.SingleOrDefault(x => x.LISTID == toolModelDb.SWITCHID);
            if (shutOff != null)
            {
                toolModel.ShutOff = new ShutOff()
                {
                    ListId = new HelperTableEntityId(shutOff.LISTID),
                    Value = new HelperTableEntityValue(shutOff.INFO),
                    NodeId = (NodeId)shutOff.NODEID,
                    Alive = shutOff.ALIVE
                };
            }

            var constructionType = qstLists.SingleOrDefault(x => x.LISTID == toolModelDb.FORMID);
            if (constructionType != null)
            {
                toolModel.ConstructionType = new ConstructionType()
                {
                    ListId = new HelperTableEntityId(constructionType.LISTID),
                    Value = new HelperTableEntityValue(constructionType.INFO),
                    NodeId = (NodeId)constructionType.NODEID,
                    Alive = constructionType.ALIVE
                };
            }

            var driveSize = qstLists.SingleOrDefault(x => x.LISTID == toolModelDb.MEAID);
            if (driveSize != null)
            {
                toolModel.DriveSize = new DriveSize()
                {
                    ListId = new HelperTableEntityId(driveSize.LISTID),
                    Value = new HelperTableEntityValue(driveSize.INFO),
                    NodeId = (NodeId)driveSize.NODEID,
                    Alive = driveSize.ALIVE
                };
            }

            return toolModel;
        }

        private bool ToolUpdateWithHistoryPreconditions(DbEntities.Tool itemToUpdate, Tool newTool)
        {
            return
                itemToUpdate.SERIALNO == newTool.SerialNumber
                && itemToUpdate.USERNO == newTool.InventoryNumber
                && itemToUpdate.MODELID == newTool.ToolModel?.Id?.ToLong()
                && itemToUpdate.STATEID == newTool.Status?.Id?.ToLong()
                && itemToUpdate.PTACCESS == newTool.Accessory
                && itemToUpdate.ORDERID == newTool.ConfigurableField?.ListId?.ToLong()
                && itemToUpdate.KOSTID == newTool.CostCenter?.ListId?.ToLong()
                && itemToUpdate.FREE_STR1 == newTool.AdditionalConfigurableField1?.ToDefaultString()
                && itemToUpdate.FREE_STR2 == newTool.AdditionalConfigurableField2?.ToDefaultString()
                && itemToUpdate.FREE_STR3 == newTool.AdditionalConfigurableField3?.ToDefaultString()
                && itemToUpdate.ALIVE == newTool.Alive;
        }

        private void AddUseStatistics()
        {
            var usageStatistic = new UsageStatistic
            {
                ACTION = UsageStatisticActions.SaveCollision("Pow_Tool"),
                TIMESTAMP = _timeDataAccess.UtcNow()
            };

            _dbContext.UsageStatistics.Add(usageStatistic);
            _dbContext.SaveChanges();
        }

        private void ApplyCurrentServerStateToOldTool(Tool oldTool, DbEntities.Tool itemToUpdate)
        {
            oldTool.SerialNumber = itemToUpdate.SERIALNO;
            oldTool.InventoryNumber = itemToUpdate.USERNO;
            oldTool.ToolModel = new ToolModel() {Id = new ToolModelId(itemToUpdate.MODELID.GetValueOrDefault(-1))};
            oldTool.Status = new Server.Core.Entities.Status(){ Id = new StatusId(itemToUpdate.STATEID.GetValueOrDefault(-1))};
            oldTool.Accessory = itemToUpdate.PTACCESS;
            oldTool.ConfigurableField = new ConfigurableField() {ListId = new HelperTableEntityId(itemToUpdate.ORDERID.GetValueOrDefault(-1))};
            oldTool.CostCenter = new CostCenter() { ListId = new HelperTableEntityId(itemToUpdate.KOSTID.GetValueOrDefault(-1)) };
            oldTool.AdditionalConfigurableField1 = new ConfigurableFieldString40(itemToUpdate.FREE_STR1);
            oldTool.AdditionalConfigurableField2 = new ConfigurableFieldString80(itemToUpdate.FREE_STR2);
            oldTool.AdditionalConfigurableField3 = new ConfigurableFieldString250(itemToUpdate.FREE_STR3);
            oldTool.Alive = itemToUpdate.ALIVE.GetValueOrDefault(false);
        }

        private void UpdateSingleToolWithHistory(ToolDiff toolDiff, DbEntities.Tool toolToUpdate)
        {
            var currentTimestamp = _timeDataAccess.UtcNow();
            var globalHistoryId = _globalHistoryDataAccess.CreateGlobalHistory("DATA UPDATED", currentTimestamp);
            AddSpecificToolHistoryEntry(globalHistoryId);

            UpdateSingleTool(toolToUpdate, toolDiff, currentTimestamp);

            var toolChanges = CreateToolChanges(globalHistoryId, toolDiff);
            AddToolChangesForUpdate(toolDiff, toolChanges);
            _dbContext.SaveChanges();
        }

        private void UpdateSingleTool(DbEntities.Tool toolToUpdate, ToolDiff toolDiff, DateTime currentTimestamp)
        {
            UpdateDbToolFromToolEntity(toolToUpdate, toolDiff.GetNewTool());
            toolToUpdate.SEQID = toolToUpdate.SEQID;
            toolToUpdate.TSA = currentTimestamp;
        }

        private void UpdateDbToolFromToolEntity(DbEntities.Tool dbTool, Tool toolEntity)
        {
            dbTool.SERIALNO = toolEntity.SerialNumber;
            dbTool.USERNO = toolEntity.InventoryNumber;
            dbTool.MODELID = toolEntity.ToolModel?.Id?.ToLong();
            dbTool.STATEID = toolEntity.Status?.Id?.ToLong();
            dbTool.PTACCESS = toolEntity.Accessory;
            dbTool.ORDERID = toolEntity.ConfigurableField?.ListId?.ToLong();
            dbTool.KOSTID = toolEntity.CostCenter?.ListId?.ToLong();
            dbTool.FREE_STR1 = toolEntity.AdditionalConfigurableField1?.ToDefaultString();
            dbTool.FREE_STR2 = toolEntity.AdditionalConfigurableField2?.ToDefaultString();
            dbTool.FREE_STR3 = toolEntity.AdditionalConfigurableField3?.ToDefaultString();
            dbTool.ALIVE = toolEntity.Alive;
        }

        private void AddToolChangesForUpdate(ToolDiff toolDiff, ToolChanges change)
        {
            var oldTool = toolDiff.GetOldTool();
            var newTool = toolDiff.GetNewTool();

            change.ACTION = "UPDATE";
            change.SERIALNOOLD = oldTool.SerialNumber;
            change.SERIALNONEW = newTool.SerialNumber;
            change.USERNOOLD = oldTool.InventoryNumber;
            change.USERNONEW = newTool.InventoryNumber;
            change.MODELIDOLD = oldTool.ToolModel?.Id?.ToLong();
            change.MODELIDNEW = newTool.ToolModel?.Id?.ToLong();
            change.STATEIDOLD = oldTool.Status?.Id?.ToLong();
            change.STATEIDNEW = newTool.Status?.Id?.ToLong(); 
            change.PTACCESSOLD = oldTool.Accessory;
            change.PTACCESSNEW = newTool.Accessory;
            change.ORDERIDOLD = oldTool.ConfigurableField?.ListId?.ToLong();
            change.ORDERIDNEW = newTool.ConfigurableField?.ListId?.ToLong(); 
            change.KOSTIDOLD = oldTool.CostCenter?.ListId?.ToLong();
            change.KOSTIDNEW = newTool.CostCenter?.ListId?.ToLong(); 
            change.FREE_STR1OLD = oldTool.AdditionalConfigurableField1?.ToDefaultString();
            change.FREE_STR1NEW = newTool.AdditionalConfigurableField1?.ToDefaultString();
            change.FREE_STR2OLD = oldTool.AdditionalConfigurableField2?.ToDefaultString();
            change.FREE_STR2NEW = newTool.AdditionalConfigurableField2?.ToDefaultString();
            change.FREE_STR3OLD = oldTool.AdditionalConfigurableField3?.ToDefaultString();
            change.FREE_STR3NEW = newTool.AdditionalConfigurableField3?.ToDefaultString();
            change.ALIVEOLD = oldTool.Alive;
            change.ALIVENEW = newTool.Alive;

            _dbContext.ToolChanges.Add(change);
        }

        private ToolChanges CreateToolChanges(long globalHistoryId, ToolDiff toolDiff)
        {
            var newTool = toolDiff.GetNewTool();

            var toolChanges = new ToolChanges()
            {
                GLOBALHISTORYID = globalHistoryId,
                ToolId = newTool.Id.ToLong(),
                USERID = toolDiff.GetUser().UserId.ToLong(),
                USERCOMMENT = toolDiff.GetComment().ToDefaultString()
            };

            return toolChanges;
        }

        private void AddSpecificToolHistoryEntry(long globalHistoryId)
        {
            var toolHistory = new ToolHistory() { GLOBALHISTORYID = globalHistoryId };
            _dbContext.ToolHistories.Add(toolHistory);
        }
    }
}
