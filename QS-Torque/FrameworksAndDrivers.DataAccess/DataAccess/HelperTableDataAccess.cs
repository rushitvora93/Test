using System;
using System.Collections.Generic;
using System.Linq;
using Common.Types.Exceptions;
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
using State;
using Tool = FrameworksAndDrivers.DataAccess.DbEntities.Tool;
using ToolModel = FrameworksAndDrivers.DataAccess.DbEntities.ToolModel;

namespace FrameworksAndDrivers.DataAccess.DataAccess
{
    public class HelperTableDataAccess : DataAccessBase, IHelperTableDataAccess
    {
        private readonly IGlobalHistoryDataAccess _globalHistoryDataAccess;
        private readonly ITimeDataAccess _timeDataAccess;
        private readonly Mapper _mapper = new Mapper();

        public HelperTableDataAccess(SqliteDbContext dbContext, ITransactionDbContext transactionContext, 
            IGlobalHistoryDataAccess globalHistoryDataAccess, ITimeDataAccess timeDataAccess)
            : base(transactionContext, dbContext)
        {
            _globalHistoryDataAccess = globalHistoryDataAccess;
            _timeDataAccess = timeDataAccess;
        }

        public List<HelperTableEntity> GetHelperTableByNodeId(NodeId nodeId)
        {
            var dbQstList = _dbContext.QstLists.Where(x => x.NODEID == (long)nodeId && x.ALIVE).ToList();
            var entities = new List<HelperTableEntity>();

            dbQstList.ForEach(db => entities.Add(_mapper.DirectPropertyMapping(db)));

            return entities;
        }

        public List<HelperTableEntity> GetAllHelperTableEntities()
        {
            var dbQstList = _dbContext.QstLists.Where(x => x.ALIVE).ToList();
            var entities = new List<HelperTableEntity>();

            dbQstList.ForEach(db => entities.Add(_mapper.DirectPropertyMapping(db)));

            return entities;
        }

        public List<ToolModelReferenceLink> GetHelperTableEntityModelLinks(long id, NodeId nodeId)
        {
            List<ToolModel> models = new List<ToolModel>();
            switch (nodeId)
            {
                case NodeId.ToolType:
                    models = _dbContext.ToolModels.Where(x => x.VERSIONID == id && x.ALIVE == true).ToList();
                    break;
                case NodeId.SwitchOff:
                    models = _dbContext.ToolModels.Where(x => x.KINDID == id && x.ALIVE == true).ToList();
                    break;
                case NodeId.ShutOff:
                    models = _dbContext.ToolModels.Where(x => x.SWITCHID == id && x.ALIVE == true).ToList();
                    break;
                case NodeId.DriveSize:
                    models = _dbContext.ToolModels.Where(x => x.MEAID == id && x.ALIVE == true).ToList();
                    break;
                case NodeId.DriveType:
                    models = _dbContext.ToolModels.Where(x => x.DRIVEID == id && x.ALIVE == true).ToList();
                    break;
                case NodeId.ConstructionType:
                    models = _dbContext.ToolModels.Where(x => x.FORMID == id && x.ALIVE == true).ToList();
                    break;
            }

            var links = models.Select(x => new ToolModelReferenceLink
            {
                Id = new QstIdentifier(x.MODELID),
                DisplayName = x.MODEL
            }).ToList();

            return links;
        }

        public List<ToolReferenceLink> GetHelperTableEntityToolLinks(long id, NodeId nodeId)
        {
            var tools = new List<Tool>();

            switch (nodeId)
            {
                case NodeId.ConfigurableField:
                    tools = _dbContext.Tools.Where(x => x.ORDERID == id && x.ALIVE == true).ToList();
                    break;
                case NodeId.CostCenter:
                    tools = _dbContext.Tools.Where(x => x.KOSTID == id && x.ALIVE == true).ToList();
                    break;
            }

            var links = tools
                .Select(x => new ToolReferenceLink(new QstIdentifier(x.SEQID), x.USERNO, x.SERIALNO)).ToList();

            return links;
        }

        public List<HelperTableEntity> InsertHelperTableEntityWithHistory(List<HelperTableEntityDiff> helperTableEntityDiffs, bool returnList)
        {
            var insertedHelperTableEntity = new List<HelperTableEntity>();
            foreach (var helperTableEntityDiff in helperTableEntityDiffs)
            {
                QstListInsertPreconditions(helperTableEntityDiff.GetNewHelperTableEntity());
                InsertSingleHelperTableEntityWithHistory(helperTableEntityDiff, insertedHelperTableEntity);
            }

            return returnList ? insertedHelperTableEntity : null;
        }

        private void QstListInsertPreconditions(HelperTableEntity helperTableEntity)
        {
            var existingQstListCount = _dbContext.QstLists.Count(x => x.NODEID == (long)helperTableEntity.NodeId
                                             && x.INFO == helperTableEntity.Value.ToDefaultString()
                                             && x.ALIVE);
            if (existingQstListCount > 0)
            {
                throw new EntryAlreadyExistsException("Entry already existing!");
            }
        }

        private void InsertSingleHelperTableEntityWithHistory(HelperTableEntityDiff helperTableEntityDiff, List<HelperTableEntity> insertedHelperTableEntity)
        {
            var currentTimestamp = _timeDataAccess.UtcNow();
            var globalHistoryId = _globalHistoryDataAccess.CreateGlobalHistory("DATA INSERTED", currentTimestamp);
            AddSpecificHelperTableHistoryEntry(helperTableEntityDiff.GetNewHelperTableEntity().NodeId, globalHistoryId);

            InsertSingleHelperTableEntity(helperTableEntityDiff.GetNewHelperTableEntity(), currentTimestamp, insertedHelperTableEntity);

            var helperTableEntityChanges = CreateQstListChanges(globalHistoryId, helperTableEntityDiff);
            AddHelperTableEntityChangesForInsert(helperTableEntityDiff, helperTableEntityChanges);

            _dbContext.SaveChanges();
        }

        private void InsertSingleHelperTableEntity(HelperTableEntity helperTableEntity, DateTime currentTimestamp, List<HelperTableEntity> insertedHelperTableEntity)
        {
            var dbQstList = _mapper.DirectPropertyMapping(helperTableEntity);
            dbQstList.ALIVE = true;
            dbQstList.TSN = currentTimestamp;
            dbQstList.TSA = currentTimestamp;
            _dbContext.QstLists.Add(dbQstList);
            _dbContext.SaveChanges();

            helperTableEntity.ListId = new HelperTableEntityId(dbQstList.LISTID);
            insertedHelperTableEntity.Add(helperTableEntity);
        }

        private void AddHelperTableEntityChangesForInsert(HelperTableEntityDiff helperTableEntityDiff, QstListChanges changes)
        {
            var newHelperTableEntity = helperTableEntityDiff.GetNewHelperTableEntity();

            changes.ACTION = "INSERT";
            changes.ALIVEOLD = null;
            changes.ALIVENEW = true;

            changes.INFOOLD = null;
            changes.INFONEW = newHelperTableEntity.Value.ToDefaultString();

            _dbContext.QstListChanges.Add(changes);
        }

        public List<HelperTableEntity> UpdateHelperTableEntityWithHistory(List<HelperTableEntityDiff> helperTableEntityDiffs)
        {
            var leftOvers = new List<HelperTableEntity>();
            foreach (var helperTableEntityDiff in helperTableEntityDiffs)
            {
                var oldHelperTableEntity = helperTableEntityDiff.GetOldHelperTableEntity();
                var newHelperTableEntity = helperTableEntityDiff.GetNewHelperTableEntity();

                QstListUpdatePreconditions(newHelperTableEntity);

                var itemToUpdate = _dbContext.QstLists.Find(newHelperTableEntity.ListId.ToLong());
                if (!HelperTableEntityUpdateWithHistoryPreconditions(itemToUpdate, oldHelperTableEntity))
                {
                    AddUseStatistics(itemToUpdate);
                    ApplyCurrentServerHelperTableEntityToOldHelperTableEntity(oldHelperTableEntity, itemToUpdate);
                    leftOvers.Add(_mapper.DirectPropertyMapping(itemToUpdate));
                }
                UpdateSingleHelperTableEntityWithHistory(helperTableEntityDiff, itemToUpdate);
            }

            return leftOvers;
        }

        private void QstListUpdatePreconditions(HelperTableEntity newHelperTableEntity)
        {
            if (newHelperTableEntity.Value.ToDefaultString() == "")
            {
                throw new Exception("Empty Value could not inserted");
            }
        }

        private bool HelperTableEntityUpdateWithHistoryPreconditions(QstList itemToUpdate, HelperTableEntity newHelperTableEntity)
        {
            return itemToUpdate.INFO == newHelperTableEntity.Value.ToDefaultString();
        }

        private void AddUseStatistics(QstList itemToUpdate)
        {
            var usageStatistic = new UsageStatistic
            {
                ACTION = UsageStatisticActions.SaveCollision("QstList" + itemToUpdate.NODEID),
                TIMESTAMP = _timeDataAccess.UtcNow()
            };

            _dbContext.UsageStatistics.Add(usageStatistic);
            _dbContext.SaveChanges();
        }

        private void ApplyCurrentServerHelperTableEntityToOldHelperTableEntity(HelperTableEntity oldHelperTableEntity, QstList itemToUpdate)
        {
            oldHelperTableEntity.Value = new HelperTableEntityValue(itemToUpdate.INFO);
            oldHelperTableEntity.Alive = itemToUpdate.ALIVE;
        }

        private void UpdateSingleHelperTableEntityWithHistory(HelperTableEntityDiff helperTableEntityDiff, QstList itemToUpdate)
        {
            var currentTimestamp = _timeDataAccess.UtcNow();
            var globalHistoryId = _globalHistoryDataAccess.CreateGlobalHistory("DATA UPDATED", currentTimestamp);
            AddSpecificHelperTableHistoryEntry(helperTableEntityDiff.GetNewHelperTableEntity().NodeId, globalHistoryId);

            UpdateSingleHelperTableEntity(itemToUpdate, helperTableEntityDiff, currentTimestamp);

            var helperTableEntityChanges = CreateQstListChanges(globalHistoryId, helperTableEntityDiff);
            AddHelperTableEntityChangesForUpdate(helperTableEntityDiff, helperTableEntityChanges);
            _dbContext.SaveChanges();
        }

        private void UpdateSingleHelperTableEntity(QstList itemToUpdate, HelperTableEntityDiff helperTableEntityDiff, DateTime currentTimestamp)
        {
            itemToUpdate.NODEID = (long)helperTableEntityDiff.GetNewHelperTableEntity().NodeId;
            itemToUpdate.INFO = helperTableEntityDiff.GetNewHelperTableEntity().Value.ToDefaultString();
            itemToUpdate.ALIVE = helperTableEntityDiff.GetNewHelperTableEntity().Alive;
            itemToUpdate.TSA = currentTimestamp;
        }
        private void AddHelperTableEntityChangesForUpdate(HelperTableEntityDiff helperTableEntityDiff, QstListChanges change)
        {
            var oldHelperTableEntity = helperTableEntityDiff.GetOldHelperTableEntity();
            var newHelperTableEntity = helperTableEntityDiff.GetNewHelperTableEntity();

            change.ACTION = "UPDATE";
            change.ALIVEOLD = oldHelperTableEntity.Alive;
            change.ALIVENEW = newHelperTableEntity.Alive;
            change.INFOOLD = oldHelperTableEntity.Value.ToDefaultString();
            change.INFONEW = newHelperTableEntity.Value.ToDefaultString();

            _dbContext.QstListChanges.Add(change);
        }

        private QstListChanges CreateQstListChanges(long globalHistoryId, HelperTableEntityDiff helperTableEntityDiff)
        {
            var newHelperTableEntity = helperTableEntityDiff.GetNewHelperTableEntity();

            var qstListChanges = new QstListChanges()
            {
                GLOBALHISTORYID = globalHistoryId,
                LISTID = newHelperTableEntity.ListId.ToLong(),
                NODEID = (long)newHelperTableEntity.NodeId,
                USERID = helperTableEntityDiff.GetUser().UserId.ToLong(),
                USERCOMMENT = helperTableEntityDiff.GetComment().ToDefaultString()
            };

            return qstListChanges;
        }

        private void AddSpecificHelperTableHistoryEntry(NodeId nodeId, long globalHistoryId)
        {
            switch (nodeId)
            {
                case NodeId.ToolType:
                    var toolType = new ToolTypeHistory { GLOBALHISTORYID = globalHistoryId };
                    _dbContext.ToolTypeHistories.Add(toolType);
                    break;
                case NodeId.DriveType:
                    var driveType = new DriveTypeHistory() { GLOBALHISTORYID = globalHistoryId };
                    _dbContext.DriveTypeHistories.Add(driveType);
                    break;
                case NodeId.ConstructionType:
                    var constType = new ConstructionTypeHistory() { GLOBALHISTORYID = globalHistoryId };
                    _dbContext.ConstructionTypeHistories.Add(constType);
                    break;
                case NodeId.SwitchOff:
                    var switchOff = new SwitchoffHistory() { GLOBALHISTORYID = globalHistoryId };
                    _dbContext.SwitchoffHistories.Add(switchOff);
                    break;
                case NodeId.DriveSize:
                    var driveSize = new DriveSizeHistory() { GLOBALHISTORYID = globalHistoryId };
                    _dbContext.DriveSizeHistories.Add(driveSize);
                    break;
                case NodeId.ShutOff:
                    var shutOff = new ShutoffHistory() { GLOBALHISTORYID = globalHistoryId };
                    _dbContext.ShutoffHistories.Add(shutOff);
                    break;
                case NodeId.ReasonForToolChange:
                    var reasonForToolChange = new ReasonForToolChangeHistory() { GLOBALHISTORYID = globalHistoryId };
                    _dbContext.ReasonForToolChangeHistories.Add(reasonForToolChange);
                    break;
                case NodeId.ConfigurableField:
                    var configurableField = new ConfigurableFieldHistory() { GLOBALHISTORYID = globalHistoryId };
                    _dbContext.ConfigurableFieldHistories.Add(configurableField);
                    break;
                case NodeId.CostCenter:
                    var costCenter = new CostCenterHistory() { GLOBALHISTORYID = globalHistoryId };
                    _dbContext.CostCenterHistories.Add(costCenter);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(nodeId), nodeId, null);
            }
            _dbContext.SaveChanges();
        }
    }
}