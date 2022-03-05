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
using Server.Core.Enums;
using Server.UseCases.UseCases;
using State;
using Location = FrameworksAndDrivers.DataAccess.DbEntities.Location;
using ToolUsage = Server.Core.Entities.ToolUsage;

namespace FrameworksAndDrivers.DataAccess.DataAccess
{
    public class LocationToolAssignmentDataAccess : DataAccessBase, ILocationToolAssignmentData
    {
        private readonly ITimeDataAccess _timeDataAccess;
        private readonly IGlobalHistoryDataAccess _globalHistoryDataAccess;
        private readonly IQstCommentDataAccess _commentDataAccess;

        public LocationToolAssignmentDataAccess(SqliteDbContext dbContext, ITransactionDbContext transactionContext, 
            ITimeDataAccess timeDataAccess, IGlobalHistoryDataAccess globalHistoryDataAccess, IQstCommentDataAccess commentDataAccess)
            : base(transactionContext, dbContext)
        {
            _timeDataAccess = timeDataAccess;
            _globalHistoryDataAccess = globalHistoryDataAccess;
            _commentDataAccess = commentDataAccess;
        }

        private readonly Mapper _mapper = new Mapper();

        public List<LocationToolAssignment> LoadLocationToolAssignments()
        {
            var locPows = _dbContext.LocPows.Where(x => x.Alive == true).ToList();
            var locToolAssignments = new List<LocationToolAssignment>();

            var qstLists = _dbContext.QstLists.ToList();

            foreach (var condRot in locPows)
            {
                var locToolAssignment = LoadLocationToolAssignmentFromLocPow(condRot, qstLists);
                if (locToolAssignment?.TestParameters != null)
                {
                    locToolAssignments.Add(locToolAssignment); 
                }
            }

            return locToolAssignments;
        }

        public LocationToolAssignment GetLocationToolAssignmentById(LocationToolAssignmentId id)
        {
            var locPows = _dbContext.LocPows.FirstOrDefault(x => x.Alive == true && x.LocPowId == id.ToLong());
            return LoadLocationToolAssignmentFromLocPow(locPows, _dbContext.QstLists.ToList());
        }

        public List<LocationToolAssignmentId> GetLocationToolAssignmentIdsForTestLevelSet(TestLevelSetId id)
        {
            return _dbContext.CondRots.Where(x => x.TestLevelSetIdMfu == id.ToLong() || x.TestLevelSetIdChk == id.ToLong())
                .Select(x => new LocationToolAssignmentId(x.LOCPOWID)).ToList();
        }

        public void SaveNextTestDatesFor(LocationToolAssignmentId id, DateTime? nextTestDateMfu, Shift? nextTestShiftMfu,
            DateTime? nextTestDateChk, Shift? nextTestShiftChk,
            DateTime? endOfLastTestPeriodMfu, Shift? endOfLastTestPeriodShiftMfu, DateTime? endOfLastTestPeriodChk, Shift? endOfLastTestPeriodShiftChk)
        {
            var condRot = _dbContext.CondRots.FirstOrDefault(x => x.LOCPOWID == id.ToLong());
            
            if(condRot != null)
            {
                condRot.NEXT_MFU = nextTestDateMfu;
                condRot.NEXT_CHK = nextTestDateChk;
                condRot.NextMfuShift = (long?)nextTestShiftMfu;
                condRot.NextChkShift = (long?)nextTestShiftChk;
                condRot.EndOfLastTestPeriodMfu = endOfLastTestPeriodMfu;
                condRot.EndOfLastTestPeriodShiftMfu = (long?)endOfLastTestPeriodShiftMfu;
                condRot.EndOfLastTestPeriodChk = endOfLastTestPeriodChk;
                condRot.EndOfLastTestPeriodShiftChk = (long?)endOfLastTestPeriodShiftChk;
                _dbContext.CondRots.Update(condRot);
                _dbContext.SaveChanges();
            }
        }

        public List<LocationReferenceLink> LoadLocationReferenceLinksForTool(ToolId toolId)
        {
            var links = (
                from lp in _dbContext.LocPows
                where lp.Alive == true && lp.PowId == toolId.ToLong()
                join loc in _dbContext.Locations
                    on lp.LocId equals loc.SEQID
                select GetLocationReferenceLink(loc)
            ).ToList();

            return links;
        }

        public List<ToolUsage> LoadUnusedToolUsagesForLocation(LocationId locationId)
        {
            var powPosIds = _dbContext.LocPows.Where(x => x.LocId == locationId.ToLong() && x.Alive == true)
                .Select(x => x.PowPosId).ToList();

            var powPos = _dbContext.ToolUsages
                .Where(x => !powPosIds.Contains(x.POSID) && x.ALIVE == true).ToList();

            var result = new List<ToolUsage>();
            powPos.ForEach(x => result.Add(_mapper.DirectPropertyMapping(x)));

            return result;
        }

        public List<LocationToolAssignment> GetLocationToolAssignmentsByLocationId(LocationId locationId)
        {
            var locPows = _dbContext.LocPows.Where(x => x.LocId == locationId.ToLong() && x.Alive == true).ToList();
            var result = new List<LocationToolAssignment>();

            var qstLists = _dbContext.QstLists.ToList();

            foreach (var locPow in locPows)
            {
                result.Add(LoadLocationToolAssignmentFromLocPow(locPow, qstLists, true, true));
            }

            return result;
        }

        public List<LocationToolAssignment> GetLocationToolAssignmentsByIds(List<LocationToolAssignmentId> ids)
        {
            var longIds = ids.Select(x => x.ToLong()).ToList();
            var locPows = _dbContext.LocPows.Where(x => longIds.Contains(x.LocPowId)).ToList();
            var result = new List<LocationToolAssignment>();

            var qstLists = _dbContext.QstLists.ToList();

            foreach (var locPow in locPows)
            {
                result.Add(LoadLocationToolAssignmentFromLocPow(locPow, qstLists));
            }

            return result;
        }

        private static LocationReferenceLink GetLocationReferenceLink(Location location)
        {
            return new LocationReferenceLink(new QstIdentifier(location.SEQID), location.USERID, location.NAME);
        }


        private void LoadLocationToolAssignmentTestLevelSet(LocationToolAssignment locationToolAssignment, CondRot condRot)
        {
            _mapper.DirectPropertyMapping(condRot, locationToolAssignment);

            locationToolAssignment.NextTestShiftMfu = (Shift?)condRot.NextMfuShift;
            locationToolAssignment.NextTestShiftChk = (Shift?)condRot.NextChkShift;

            var testLevelSetMfu = _dbContext.TestLevelSets.FirstOrDefault(x => x.Id == condRot.TestLevelSetIdMfu);
            var testLevelSetChk = _dbContext.TestLevelSets.FirstOrDefault(x => x.Id == condRot.TestLevelSetIdChk);

            if (testLevelSetMfu != null)
            {
                locationToolAssignment.TestLevelSetMfu = _mapper.DirectPropertyMapping(testLevelSetMfu);
                var testLevel1 = _dbContext.TestLevels.FirstOrDefault(x => x.TestLevelSetId == testLevelSetMfu.Id && x.TestLevelNumber == 1);
                var testLevel2 = _dbContext.TestLevels.FirstOrDefault(x => x.TestLevelSetId == testLevelSetMfu.Id && x.TestLevelNumber == 2);
                var testLevel3 = _dbContext.TestLevels.FirstOrDefault(x => x.TestLevelSetId == testLevelSetMfu.Id && x.TestLevelNumber == 3);
                locationToolAssignment.TestLevelSetMfu.TestLevel1 = testLevel1 != null ? _mapper.DirectPropertyMapping(testLevel1) : null;
                locationToolAssignment.TestLevelSetMfu.TestLevel2 = testLevel1 != null ? _mapper.DirectPropertyMapping(testLevel2) : null;
                locationToolAssignment.TestLevelSetMfu.TestLevel3 = testLevel1 != null ? _mapper.DirectPropertyMapping(testLevel3) : null;
            }
            if (testLevelSetChk != null)
            {
                locationToolAssignment.TestLevelSetChk = _mapper.DirectPropertyMapping(testLevelSetChk);
                var testLevel1 = _dbContext.TestLevels.FirstOrDefault(x => x.TestLevelSetId == testLevelSetChk.Id && x.TestLevelNumber == 1);
                var testLevel2 = _dbContext.TestLevels.FirstOrDefault(x => x.TestLevelSetId == testLevelSetChk.Id && x.TestLevelNumber == 2);
                var testLevel3 = _dbContext.TestLevels.FirstOrDefault(x => x.TestLevelSetId == testLevelSetChk.Id && x.TestLevelNumber == 3);
                locationToolAssignment.TestLevelSetChk.TestLevel1 = testLevel1 != null ? _mapper.DirectPropertyMapping(testLevel1) : null;
                locationToolAssignment.TestLevelSetChk.TestLevel2 = testLevel1 != null ? _mapper.DirectPropertyMapping(testLevel2) : null;
                locationToolAssignment.TestLevelSetChk.TestLevel3 = testLevel1 != null ? _mapper.DirectPropertyMapping(testLevel3) : null;
            }
        }

        private LocationToolAssignment LoadLocationToolAssignmentFromLocPow(LocPow locPow, List<QstList> qstLists, bool withLocationComment = false, bool withToolComment = false)
        {
            if (locPow == null)
            {
                return null;
            }

            var locationToolAssignment = new LocationToolAssignment
            {
                Id = new LocationToolAssignmentId(locPow.LocPowId)
            };

            LoadLocationToolAssignmentLocation(locationToolAssignment, locPow, withLocationComment);
            LoadLocationToolAssignmentTool(locationToolAssignment, locPow, qstLists, withToolComment);
            LoadLocationToolAssignmentToolUsage(locationToolAssignment, locPow);

            var condRot = _dbContext.CondRots.FirstOrDefault(x => x.LOCPOWID == locPow.LocPowId && x.ALIVE == true);

            if (condRot != null)
            {
                LoadLocationToolAssignmentTestParameters(locationToolAssignment, condRot);
                LoadLocationToolAssignmentTestTechnique(locationToolAssignment, condRot);
                LoadLocationToolAssignmentTestLevelSet(locationToolAssignment, condRot);
            }

            return locationToolAssignment;
        }

        private void LoadLocationToolAssignmentTool(LocationToolAssignment locationToolAssignment, LocPow locPow, List<QstList> qstLists, bool withToolComment)
        {
            var powTool = _dbContext.Tools
                .Include(x => x.ToolModel)
                .ThenInclude(x => x.Manufacturer)
                .Include(x => x.Status)
                .SingleOrDefault(x => x.SEQID == locPow.PowId);

            if (powTool == null)
            {
                return;
            }

            locationToolAssignment.AssignedTool = ToolDataAccess.CreateToolFromDb(powTool, qstLists);

            if (locationToolAssignment.AssignedTool == null || !withToolComment)
                return;

            var comment = _commentDataAccess.GetQstCommentByNodeIdAndNodeSeqId(NodeId.Tool,
                locationToolAssignment.AssignedTool.Id.ToLong());

            locationToolAssignment.AssignedTool.Comment = comment?.ToDefaultString();
        }

        private void LoadLocationToolAssignmentLocation(LocationToolAssignment locationToolAssignment, LocPow locPow, bool withLocationComment)
        {
            var location = _dbContext.Locations
                .Include(x => x.ToleranceClass1)
                .Include(x => x.ToleranceClass2)
                .SingleOrDefault(x => x.SEQID == locPow.LocId);

            if (location == null) 
                return;

            locationToolAssignment.AssignedLocation = _mapper.DirectPropertyMapping(location);

            if (locationToolAssignment.AssignedLocation == null || !withLocationComment) 
                return;

            var comment = _commentDataAccess.GetQstCommentByNodeIdAndNodeSeqId(NodeId.Location,
                locationToolAssignment.AssignedLocation.Id.ToLong());
            locationToolAssignment.AssignedLocation.Comment = comment?.ToDefaultString();
        }

        private void LoadLocationToolAssignmentTestTechnique(LocationToolAssignment locationToolAssignment, CondRot condRot)
        {
            locationToolAssignment.TestTechnique = new TestTechnique
            {
                EndCycleTime = condRot.AC_ENDTIME.GetValueOrDefault(0),
                FilterFrequency = condRot.AC_FILTERFREQ.GetValueOrDefault(0),
                CycleComplete = condRot.AC_CYCLECOMPLETE.GetValueOrDefault(0),
                MeasureDelayTime = condRot.AC_MEASUREDELAYTIME.GetValueOrDefault(0),
                ResetTime = condRot.AC_RESETTIME.GetValueOrDefault(0),
                MustTorqueAndAngleBeInLimits = condRot.AC_CMCMKSPCTESTTYPE.GetValueOrDefault(false),
                CycleStart = condRot.AC_CYCLESTARTROT.GetValueOrDefault(0),
                StartFinalAngle = condRot.AC_STARTFINALANGLEROT.GetValueOrDefault(0),
                SlipTorque = condRot.AC_SLIPTORQUE.GetValueOrDefault(0),
                TorqueCoefficient = condRot.AC_TORQUECOEFFICIENT.GetValueOrDefault(0),
                MinimumPulse = (int) condRot.AC_MINIMUMPULSE.GetValueOrDefault(0),
                MaximumPulse = (int) condRot.AC_MAXIMUMPULSE.GetValueOrDefault(0),
                Threshold = (int) condRot.AC_THRESHOLD.GetValueOrDefault(0)
            };
        }

        private void LoadLocationToolAssignmentTestParameters(LocationToolAssignment locationToolAssignment, CondRot condRot)
        {
            locationToolAssignment.TestParameters = new TestParameters
            {
                SetPoint1 = condRot.NOM1.GetValueOrDefault(0),
                Minimum1 = condRot.MIN1.GetValueOrDefault(0),
                Maximum1 = condRot.MAX1.GetValueOrDefault(0),
                Threshold1 = condRot.MS.GetValueOrDefault(0),
                SetPoint2 = condRot.NOM2.GetValueOrDefault(0),
                Minimum2 = condRot.MIN2.GetValueOrDefault(0),
                Maximum2 = condRot.MAX2.GetValueOrDefault(0),
                ControlledBy = (LocationControlledBy) condRot.I_CTRL.GetValueOrDefault(0)
            };

            if (condRot.CLASSID != null)
            {
                var tol1 = _dbContext.ToleranceClasses.SingleOrDefault(x => x.CLASSID == condRot.CLASSID.Value);

                if (tol1 != null)
                {
                    locationToolAssignment.TestParameters.ToleranceClass1 = _mapper.DirectPropertyMapping(tol1);
                }
            }

            if (condRot.CLASSID2 != null)
            {
                var tol2 = _dbContext.ToleranceClasses.SingleOrDefault(x =>x.CLASSID == condRot.CLASSID2);

                if (tol2 != null)
                {
                    locationToolAssignment.TestParameters.ToleranceClass2 = _mapper.DirectPropertyMapping(tol2);
                }
            }
        }

        private void LoadLocationToolAssignmentToolUsage(LocationToolAssignment locationToolAssignment, LocPow locPow)
        {
            var toolUsage = _dbContext.ToolUsages.SingleOrDefault(x => x.POSID == locPow.PowPosId);
            if (toolUsage != null)
            {
                locationToolAssignment.ToolUsage = _mapper.DirectPropertyMapping(toolUsage);
            }
        }

        public List<LocationToolAssignment> InsertLocPowsWithHistory(List<LocationToolAssignmentDiff> locationToolAssignmentDiffs, bool returnList)
        {
            var insertedLocationToolAssignments = new List<LocationToolAssignment>();
            foreach (var locationToolAssignmentDiff in locationToolAssignmentDiffs)
            {
                InsertSingleLocPowWithHistory(locationToolAssignmentDiff, insertedLocationToolAssignments);
            }

            return returnList ? insertedLocationToolAssignments : null;
        }

        private void InsertSingleLocPowWithHistory(LocationToolAssignmentDiff locationToolAssignmentDiff, List<LocationToolAssignment> insertedLocationToolAssignments)
        {
            var currentTimestamp = _timeDataAccess.UtcNow();
            var globalHistoryId = _globalHistoryDataAccess.CreateGlobalHistory("DATA INSERTED", currentTimestamp);
            AddSpecificLocPowHistoryEntry(globalHistoryId);

            InsertSingleLocPow(locationToolAssignmentDiff.GetNewLocationToolAssignment(), currentTimestamp, insertedLocationToolAssignments);

            var locPowChanges = CreateLocPowChanges(globalHistoryId, locationToolAssignmentDiff);
            AddLocPowChangesForInsert(locationToolAssignmentDiff, locPowChanges);

            _dbContext.SaveChanges();
        }

        private void InsertSingleLocPow(LocationToolAssignment locationToolAssignment, DateTime currentTimestamp, List<LocationToolAssignment> insertedLocationToolAssignments)
        {
            var dbLocationToolAssignment = new LocPow
            {
                LocPowId = locationToolAssignment.Id.ToLong(),
                PowPosId = locationToolAssignment.ToolUsage?.Id?.ToLong(),
                LocId = locationToolAssignment.AssignedLocation?.Id?.ToLong(),
                PowId = locationToolAssignment.AssignedTool?.Id?.ToLong(),
                Alive = true,
                TSN = currentTimestamp,
                TSA = currentTimestamp
            };
            _dbContext.LocPows.Add(dbLocationToolAssignment);
            _dbContext.SaveChanges();

            locationToolAssignment.Id = new LocationToolAssignmentId(dbLocationToolAssignment.LocPowId);
            insertedLocationToolAssignments.Add(locationToolAssignment);
        }

        private void AddLocPowChangesForInsert(LocationToolAssignmentDiff locationToolAssignmentDiff, LocPowChanges changes)
        {
            var newLocationToolAssignment = locationToolAssignmentDiff.GetNewLocationToolAssignment();

            changes.Action = "INSERT";
            changes.ALIVENEW = true;
            changes.LocIdNew = newLocationToolAssignment.AssignedLocation?.Id?.ToLong();
            changes.PowIdNew = newLocationToolAssignment.AssignedTool?.Id?.ToLong();
            changes.ORDNEW = newLocationToolAssignment.ToolUsage?.Id?.ToLong();
            _dbContext.LocPowChanges.Add(changes);
        }

        public List<LocationToolAssignment> UpdateLocPowsWithHistory(List<LocationToolAssignmentDiff> locationToolAssignmentDiffs)
        {
            var leftOvers = new List<LocationToolAssignment>();
            foreach (var locationToolAssignmentDiff in locationToolAssignmentDiffs)
            {
                var oldLocationToolAssignment = locationToolAssignmentDiff.GetOldLocationToolAssignment();
                var newLocationToolAssignment = locationToolAssignmentDiff.GetNewLocationToolAssignment();

                var itemToUpdate = _dbContext.LocPows.Find(newLocationToolAssignment.Id.ToLong());
                if (!LocPowUpdateWithHistoryPreconditions(itemToUpdate, oldLocationToolAssignment))
                {
                    AddUseStatistics("LocPow");
                    ApplyCurrentServerStateToOldLocationToolAssignment(oldLocationToolAssignment, itemToUpdate);

                    var locToolAssignment = new LocationToolAssignment()
                    {
                        Id = new LocationToolAssignmentId(itemToUpdate.LocPowId),
                        AssignedLocation = new Server.Core.Entities.Location(){Id = new LocationId(itemToUpdate.LocId.Value)},
                        AssignedTool = new Server.Core.Entities.Tool() {Id = new ToolId(itemToUpdate.PowId.Value)},
                        ToolUsage = new Server.Core.Entities.ToolUsage(){Id = new ToolUsageId(itemToUpdate.PowId.Value)},
                        Alive = itemToUpdate.Alive
                    };
                    leftOvers.Add(locToolAssignment);
                }
                UpdateSingleLocPowWithHistory(locationToolAssignmentDiff, itemToUpdate);
            }

            return leftOvers;
        }

        private bool LocPowUpdateWithHistoryPreconditions(DbEntities.LocPow itemToUpdate, LocationToolAssignment newLocationToolAssignment)
        {
            return
                itemToUpdate.LocPowId == newLocationToolAssignment.Id?.ToLong() &&
                itemToUpdate.LocId == newLocationToolAssignment.AssignedLocation?.Id?.ToLong() &&
                itemToUpdate.PowPosId == newLocationToolAssignment.ToolUsage?.Id?.ToLong() &&
                itemToUpdate.PowId == newLocationToolAssignment.AssignedTool?.Id?.ToLong() &&
                itemToUpdate.Alive == newLocationToolAssignment.Alive;
        }

        private void AddUseStatistics(string text)
        {
            var usageStatistic = new UsageStatistic
            {
                ACTION = UsageStatisticActions.SaveCollision(text),
                TIMESTAMP = _timeDataAccess.UtcNow()
            };

            _dbContext.UsageStatistics.Add(usageStatistic);
            _dbContext.SaveChanges();
        }

        private void ApplyCurrentServerStateToOldLocationToolAssignment(LocationToolAssignment oldLocationToolAssignment, DbEntities.LocPow itemToUpdate)
        {
            oldLocationToolAssignment.AssignedLocation = new Server.Core.Entities.Location()
            {
                Id = new LocationId(itemToUpdate.LocId.Value)
            };
            oldLocationToolAssignment.AssignedTool = new Server.Core.Entities.Tool()
            {
                Id = new ToolId(itemToUpdate.PowId.Value)
            };
            oldLocationToolAssignment.ToolUsage = new Server.Core.Entities.ToolUsage()
            {
                Id = new ToolUsageId(itemToUpdate.LocId.Value)
            };

            oldLocationToolAssignment.Alive = itemToUpdate.Alive.GetValueOrDefault(false);
        }

        private void UpdateSingleLocPowWithHistory(LocationToolAssignmentDiff locationToolAssignmentDiff, DbEntities.LocPow locationToolAssignmentToUpdate)
        {
            var currentTimestamp = _timeDataAccess.UtcNow();
            var globalHistoryId = _globalHistoryDataAccess.CreateGlobalHistory("DATA UPDATED", currentTimestamp);
            AddSpecificLocPowHistoryEntry(globalHistoryId);

            UpdateSingleLocPow(locationToolAssignmentToUpdate, locationToolAssignmentDiff, currentTimestamp);

            var locPowChanges = CreateLocPowChanges(globalHistoryId, locationToolAssignmentDiff);
            AddLocPowChangesForUpdate(locationToolAssignmentDiff, locPowChanges);
            _dbContext.SaveChanges();
        }

        private void UpdateSingleLocPow(DbEntities.LocPow locPowToUpdate, LocationToolAssignmentDiff locationToolAssignmentDiff, DateTime currentTimestamp)
        {
            UpdateDbLocPowFromLocationToolAssignmentEntity(locPowToUpdate, locationToolAssignmentDiff.GetNewLocationToolAssignment());
            locPowToUpdate.LocPowId = locPowToUpdate.LocPowId;
            locPowToUpdate.TSA = currentTimestamp;
        }

        private void UpdateDbLocPowFromLocationToolAssignmentEntity(DbEntities.LocPow dbLocPow, LocationToolAssignment locationToolAssignmentEntity)
        {
            dbLocPow.LocId = locationToolAssignmentEntity.AssignedLocation?.Id?.ToLong();
            dbLocPow.PowId = locationToolAssignmentEntity.AssignedTool?.Id?.ToLong();
            dbLocPow.PowPosId = locationToolAssignmentEntity.ToolUsage?.Id?.ToLong();
            dbLocPow.Alive = locationToolAssignmentEntity.Alive;
        }

        private void AddLocPowChangesForUpdate(LocationToolAssignmentDiff locationToolAssignmentDiff, LocPowChanges change)
        {
            var oldLocationToolAssignment = locationToolAssignmentDiff.GetOldLocationToolAssignment();
            var newLocationToolAssignment = locationToolAssignmentDiff.GetNewLocationToolAssignment();

            change.Action = "UPDATE";

            change.ALIVEOLD = oldLocationToolAssignment.Alive;
            change.ALIVENEW = newLocationToolAssignment.Alive;
            change.LocIdOld = oldLocationToolAssignment.AssignedLocation?.Id?.ToLong();
            change.LocIdNew = newLocationToolAssignment.AssignedLocation?.Id?.ToLong();
            change.PowIdOld = oldLocationToolAssignment.AssignedTool?.Id?.ToLong();
            change.PowIdNew = newLocationToolAssignment.AssignedTool?.Id?.ToLong();
            change.ORDOLD = oldLocationToolAssignment.ToolUsage?.Id?.ToLong();
            change.ORDNEW = newLocationToolAssignment.ToolUsage?.Id?.ToLong();

            _dbContext.LocPowChanges.Add(change);
        }

        private LocPowChanges CreateLocPowChanges(long globalHistoryId, LocationToolAssignmentDiff locationToolAssignmentDiff)
        {
            var newLocationToolAssignment = locationToolAssignmentDiff.GetNewLocationToolAssignment();

            var locationToolAssignmentChanges = new LocPowChanges()
            {
                GlobalHistoryId = globalHistoryId,
                LocPowId = newLocationToolAssignment.Id.ToLong(),
                UserId = locationToolAssignmentDiff.GetUser().UserId.ToLong(),
                USERCOMMENT = locationToolAssignmentDiff.GetHistoryComment()?.ToDefaultString()
            };

            return locationToolAssignmentChanges;
        }

        private void AddSpecificLocPowHistoryEntry(long globalHistoryId)
        {
            var locationToolAssignmentHistory = new LocPowHistory() { GLOBALHISTORYID = globalHistoryId };
            _dbContext.LocPowHistories.Add(locationToolAssignmentHistory);
        }

        public List<LocationToolAssignment> InsertCondRotsWithHistory(List<LocationToolAssignmentDiff> locationToolAssignmentDiffs, bool returnList)
        {
            var insertedLocationToolAssignments = new List<LocationToolAssignment>();
            foreach (var locationToolAssignmentDiff in locationToolAssignmentDiffs)
            {
                InsertSingleCondRotWithHistory(locationToolAssignmentDiff, insertedLocationToolAssignments);
            }

            return returnList ? insertedLocationToolAssignments : null;
        }

        private void InsertSingleCondRotWithHistory(LocationToolAssignmentDiff locationToolAssignmentDiff, List<LocationToolAssignment> insertedLocationToolAssignments)
        {
            var currentTimestamp = _timeDataAccess.UtcNow();
            var globalHistoryId = _globalHistoryDataAccess.CreateGlobalHistory("DATA INSERTED", currentTimestamp);
            AddSpecificCondRotHistoryEntry(globalHistoryId);

            InsertSingleCondRot(locationToolAssignmentDiff.GetNewLocationToolAssignment(), currentTimestamp, insertedLocationToolAssignments);

            var condRotChanges = CreateCondRotChanges(globalHistoryId, locationToolAssignmentDiff);
            AddCondRotChangesForInsert(locationToolAssignmentDiff, condRotChanges);

            _dbContext.SaveChanges();
        }

        private void InsertSingleCondRot(LocationToolAssignment locationToolAssignment, DateTime currentTimestamp, List<LocationToolAssignment> insertedLocationToolAssignments)
        {
            var dbLocationToolAssignment = GetCondRotFromLocationToolAssignment(locationToolAssignment);
            dbLocationToolAssignment.ALIVE = true;
            dbLocationToolAssignment.TSN = currentTimestamp;
            dbLocationToolAssignment.TSA = currentTimestamp;
            _dbContext.CondRots.Add(dbLocationToolAssignment);
            _dbContext.SaveChanges();
            insertedLocationToolAssignments.Add(locationToolAssignment);
        }

        private CondRot GetCondRotFromLocationToolAssignment(LocationToolAssignment assignment)
        {
            return new CondRot
            {
                LOCPOWID = assignment.Id.ToLong(),
                NOM1 = assignment.TestParameters?.SetPoint1,
                MIN1 = assignment.TestParameters?.Minimum1,
                MAX1 = assignment.TestParameters?.Maximum1,
                MS = assignment.TestParameters?.Threshold1,
                NOM2 = assignment.TestParameters?.SetPoint2,
                MIN2 = assignment.TestParameters?.Minimum2,
                MAX2 = assignment.TestParameters?.Maximum2,
                CLASSID = assignment.TestParameters?.ToleranceClass1?.Id?.ToLong() ?? 0,
                CLASSID2 = assignment.TestParameters?.ToleranceClass2?.Id?.ToLong() ?? 0,
                AC_ENDTIME = assignment.TestTechnique?.EndCycleTime,
                AC_FILTERFREQ = assignment.TestTechnique?.FilterFrequency,
                AC_CYCLECOMPLETE = assignment.TestTechnique?.CycleComplete,
                AC_MEASUREDELAYTIME = assignment.TestTechnique?.MeasureDelayTime,
                AC_RESETTIME = assignment.TestTechnique?.ResetTime,
                AC_CMCMKSPCTESTTYPE = assignment.TestTechnique?.MustTorqueAndAngleBeInLimits,
                AC_CYCLESTARTROT = assignment.TestTechnique?.CycleStart,
                AC_STARTFINALANGLEROT = assignment.TestTechnique?.StartFinalAngle,
                AC_SLIPTORQUE = assignment.TestTechnique?.SlipTorque,
                AC_TORQUECOEFFICIENT = assignment.TestTechnique?.TorqueCoefficient,
                AC_MINIMUMPULSE = assignment.TestTechnique?.MinimumPulse,
                AC_MAXIMUMPULSE = assignment.TestTechnique?.MaximumPulse,
                AC_THRESHOLD = assignment.TestTechnique?.Threshold,
                I_CTRL = (long)(assignment.TestParameters?.ControlledBy ?? LocationControlledBy.Angle),
                TestLevelSetIdMfu = assignment.TestLevelSetMfu?.Id?.ToLong() ?? 0,
                TestLevelSetIdChk = assignment.TestLevelSetChk?.Id?.ToLong() ?? 0,
                TestLevelNumberMfu = assignment.TestLevelNumberMfu,
                TestLevelNumberChk = assignment.TestLevelNumberChk,
                TESTSTART_MFU = assignment.StartDateMfu,
                TESTSTART = assignment.StartDateChk,
                PLANOK_MFU = assignment.TestOperationActiveMfu,
                PLANOK = assignment.TestOperationActiveChk
            };
        }

        private void AddCondRotChangesForInsert(LocationToolAssignmentDiff locationToolAssignmentDiff, CondRotChanges changes)
        {
            var newLocationToolAssignment = locationToolAssignmentDiff.GetNewLocationToolAssignment();

            changes.Action = "INSERT";
            changes.ALIVENEW = true;
            changes.NOM1NEW = newLocationToolAssignment.TestParameters?.SetPoint1;
            changes.MIN1NEW = newLocationToolAssignment.TestParameters?.Minimum1;
            changes.MAX1NEW = newLocationToolAssignment.TestParameters?.Maximum1;
            changes.MSNEW = newLocationToolAssignment.TestParameters?.Threshold1;
            changes.NOM2NEW = newLocationToolAssignment.TestParameters?.SetPoint2;
            changes.MIN2NEW = newLocationToolAssignment.TestParameters?.Minimum2;
            changes.MAX2NEW = newLocationToolAssignment.TestParameters?.Maximum2;
            changes.CLASSIDNEW = newLocationToolAssignment.TestParameters?.ToleranceClass1?.Id?.ToLong() ?? 0;
            changes.CLASSID2NEW = newLocationToolAssignment.TestParameters?.ToleranceClass2?.Id?.ToLong() ?? 0;
            changes.AC_ENDTIMENEW = newLocationToolAssignment.TestTechnique?.EndCycleTime;
            changes.AC_FILTERFREQNEW = newLocationToolAssignment.TestTechnique?.FilterFrequency;
            changes.AC_CYCLECOMPLETENEW = newLocationToolAssignment.TestTechnique?.CycleComplete;
            changes.AC_MEASUREDELAYTIMENEW = newLocationToolAssignment.TestTechnique?.MeasureDelayTime;
            changes.AC_RESETTIMENEW = newLocationToolAssignment.TestTechnique?.ResetTime;
            changes.AC_CMCMKSPCTESTTYPENEW = newLocationToolAssignment.TestTechnique?.MustTorqueAndAngleBeInLimits;
            changes.AC_CYCLESTARTROTNEW = newLocationToolAssignment.TestTechnique?.CycleStart;
            changes.AC_STARTFINALANGLEROTNEW = newLocationToolAssignment.TestTechnique?.StartFinalAngle;
            changes.AC_SLIPTORQUENEW = newLocationToolAssignment.TestTechnique?.SlipTorque;
            changes.AC_TORQUECOEFFICIENTNEW = newLocationToolAssignment.TestTechnique?.TorqueCoefficient;
            changes.AC_MINIMUMPULSENEW = newLocationToolAssignment.TestTechnique?.MinimumPulse;
            changes.AC_MAXIMUMPULSENEW = newLocationToolAssignment.TestTechnique?.MaximumPulse;
            changes.AC_THRESHOLDNEW = newLocationToolAssignment.TestTechnique?.Threshold;
            changes.I_CTRLNEW = (long) (newLocationToolAssignment.TestParameters?.ControlledBy ?? LocationControlledBy.Angle);
            changes.TestLevelSetIdMfuNew = newLocationToolAssignment.TestLevelSetMfu?.Id.ToLong() ?? 0;
            changes.TestLevelSetIdChkNew = newLocationToolAssignment.TestLevelSetChk?.Id.ToLong() ?? 0;
            changes.TestLevelNumberMfuNew = newLocationToolAssignment.TestLevelNumberMfu;
            changes.TestLevelNumberChkNew = newLocationToolAssignment.TestLevelNumberChk;
            changes.TESTSTART_MFUNEW = newLocationToolAssignment.StartDateMfu;
            changes.TESTSTARTNEW = newLocationToolAssignment.StartDateChk;
            changes.PLANOK_MFUNEW = newLocationToolAssignment.TestOperationActiveMfu;
            changes.PLANOKNEW = newLocationToolAssignment.TestOperationActiveChk;

            _dbContext.CondRotChanges.Add(changes);
        } 

        public List<LocationToolAssignment> UpdateCondRotsWithHistory(List<LocationToolAssignmentDiff> locationToolAssignmentDiffs)
        {
            var leftOvers = new List<LocationToolAssignment>();
            foreach (var locationToolAssignmentDiff in locationToolAssignmentDiffs)
            {
                var oldLocationToolAssignment = locationToolAssignmentDiff.GetOldLocationToolAssignment();
                var newLocationToolAssignment = locationToolAssignmentDiff.GetNewLocationToolAssignment();

                var itemToUpdate = _dbContext.CondRots.Find(newLocationToolAssignment.Id.ToLong());
                if (!CondRotUpdateWithHistoryPreconditions(itemToUpdate, oldLocationToolAssignment))
                {
                    AddUseStatistics("CondRot");
                    ApplyCurrentServerStateToOldLocationToolAssignment(oldLocationToolAssignment, itemToUpdate);
                    leftOvers.Add(_mapper.DirectPropertyMapping(itemToUpdate));
                }
                UpdateSingleCondRotWithHistory(locationToolAssignmentDiff, itemToUpdate);
            }

            return leftOvers;
        }

        private bool CondRotUpdateWithHistoryPreconditions(DbEntities.CondRot itemToUpdate, LocationToolAssignment newLocationToolAssignment)
        {
            return itemToUpdate.LOCPOWID == newLocationToolAssignment.Id.ToLong() &&
                   itemToUpdate.NOM1 == newLocationToolAssignment.TestParameters?.SetPoint1 &&
                   itemToUpdate.MIN1 == newLocationToolAssignment.TestParameters?.Minimum1 &&
                   itemToUpdate.MAX1 == newLocationToolAssignment.TestParameters?.Maximum1 &&
                   itemToUpdate.MS == newLocationToolAssignment.TestParameters?.Threshold1 &&
                   itemToUpdate.NOM2 == newLocationToolAssignment.TestParameters?.SetPoint2 &&
                   itemToUpdate.MIN2 == newLocationToolAssignment.TestParameters?.Minimum2 &&
                   itemToUpdate.MAX2 == newLocationToolAssignment.TestParameters?.Maximum2 &&
                   itemToUpdate.CLASSID == newLocationToolAssignment.TestParameters?.ToleranceClass1?.Id?.ToLong() &&
                   itemToUpdate.CLASSID2 == newLocationToolAssignment.TestParameters?.ToleranceClass2?.Id?.ToLong() &&
                   itemToUpdate.AC_ENDTIME == newLocationToolAssignment.TestTechnique?.EndCycleTime &&
                   itemToUpdate.AC_FILTERFREQ == newLocationToolAssignment.TestTechnique?.FilterFrequency &&
                   itemToUpdate.AC_CYCLECOMPLETE == newLocationToolAssignment.TestTechnique?.CycleComplete &&
                   itemToUpdate.AC_MEASUREDELAYTIME == newLocationToolAssignment.TestTechnique?.MeasureDelayTime &&
                   itemToUpdate.AC_RESETTIME == newLocationToolAssignment.TestTechnique?.ResetTime &&
                   itemToUpdate.AC_CMCMKSPCTESTTYPE == newLocationToolAssignment.TestTechnique?.MustTorqueAndAngleBeInLimits &&
                   itemToUpdate.AC_CYCLESTARTROT == newLocationToolAssignment.TestTechnique?.CycleStart &&
                   itemToUpdate.AC_STARTFINALANGLEROT == newLocationToolAssignment.TestTechnique?.StartFinalAngle &&
                   itemToUpdate.AC_SLIPTORQUE == newLocationToolAssignment.TestTechnique?.SlipTorque &&
                   itemToUpdate.AC_TORQUECOEFFICIENT == newLocationToolAssignment.TestTechnique?.TorqueCoefficient &&
                   itemToUpdate.AC_MINIMUMPULSE == newLocationToolAssignment.TestTechnique?.MinimumPulse &&
                   itemToUpdate.AC_MAXIMUMPULSE == newLocationToolAssignment.TestTechnique?.MaximumPulse &&
                   itemToUpdate.AC_THRESHOLD == newLocationToolAssignment.TestTechnique?.Threshold &&
                   itemToUpdate.I_CTRL == (long)(newLocationToolAssignment.TestParameters?.ControlledBy ?? LocationControlledBy.Angle) &&
                   itemToUpdate.ALIVE == newLocationToolAssignment.TestParameters?.Alive &&
                   itemToUpdate.TestLevelSetIdMfu == (newLocationToolAssignment.TestLevelSetMfu?.Id.ToLong() ?? 0) &&
                   itemToUpdate.TestLevelSetIdChk == (newLocationToolAssignment.TestLevelSetChk?.Id.ToLong() ?? 0) &&
                   itemToUpdate.TestLevelNumberMfu == newLocationToolAssignment.TestLevelNumberMfu &&
                   itemToUpdate.TestLevelNumberChk == newLocationToolAssignment.TestLevelNumberChk &&
                   itemToUpdate.TESTSTART_MFU == newLocationToolAssignment.StartDateMfu &&
                   itemToUpdate.TESTSTART == newLocationToolAssignment.StartDateChk &&
                   itemToUpdate.PLANOK_MFU == newLocationToolAssignment.TestOperationActiveMfu &&
                   itemToUpdate.PLANOK == newLocationToolAssignment.TestOperationActiveChk;
        }

        private void ApplyCurrentServerStateToOldLocationToolAssignment(LocationToolAssignment oldLocationToolAssignment, DbEntities.CondRot itemToUpdate)
        {
            if (oldLocationToolAssignment.TestParameters == null)
            {
                oldLocationToolAssignment.TestParameters = new TestParameters();
            }

            if (oldLocationToolAssignment.TestTechnique == null)
            {
                oldLocationToolAssignment.TestTechnique = new TestTechnique();
            }

            if (oldLocationToolAssignment.TestParameters.ToleranceClass1 == null)
            {
                oldLocationToolAssignment.TestParameters.ToleranceClass1 = new Server.Core.Entities.ToleranceClass();
            }

            if (oldLocationToolAssignment.TestParameters.ToleranceClass2 == null)
            {
                oldLocationToolAssignment.TestParameters.ToleranceClass2 = new Server.Core.Entities.ToleranceClass();
            }


            oldLocationToolAssignment.TestParameters.SetPoint1 = itemToUpdate.NOM1.GetValueOrDefault(0);
            oldLocationToolAssignment.TestParameters.Minimum1 = itemToUpdate.MIN1.GetValueOrDefault(0);
            oldLocationToolAssignment.TestParameters.Maximum1 = itemToUpdate.MAX1.GetValueOrDefault(0);
            oldLocationToolAssignment.TestParameters.Threshold1 = itemToUpdate.MS.GetValueOrDefault(0);
            oldLocationToolAssignment.TestParameters.SetPoint2 = itemToUpdate.NOM2.GetValueOrDefault(0);
            oldLocationToolAssignment.TestParameters.Minimum2 = itemToUpdate.MIN2.GetValueOrDefault(0);
            oldLocationToolAssignment.TestParameters.Maximum2 = itemToUpdate.MAX2.GetValueOrDefault(0);
            oldLocationToolAssignment.TestParameters.ToleranceClass1.Id = new ToleranceClassId(itemToUpdate.CLASSID.GetValueOrDefault(0));
            oldLocationToolAssignment.TestParameters.ToleranceClass2.Id = new ToleranceClassId(itemToUpdate.CLASSID2.GetValueOrDefault(0));
            oldLocationToolAssignment.TestTechnique.EndCycleTime = itemToUpdate.AC_ENDTIME.GetValueOrDefault(0);
            oldLocationToolAssignment.TestTechnique.FilterFrequency = itemToUpdate.AC_FILTERFREQ.GetValueOrDefault(0);
            oldLocationToolAssignment.TestTechnique.CycleComplete = itemToUpdate.AC_CYCLECOMPLETE.GetValueOrDefault(0);
            oldLocationToolAssignment.TestTechnique.MeasureDelayTime = itemToUpdate.AC_MEASUREDELAYTIME.GetValueOrDefault(0);
            oldLocationToolAssignment.TestTechnique.ResetTime = itemToUpdate.AC_RESETTIME.GetValueOrDefault(0);
            oldLocationToolAssignment.TestTechnique.MustTorqueAndAngleBeInLimits = itemToUpdate.AC_CMCMKSPCTESTTYPE.GetValueOrDefault(false);
            oldLocationToolAssignment.TestTechnique.CycleStart = itemToUpdate.AC_CYCLESTARTROT.GetValueOrDefault(0);
            oldLocationToolAssignment.TestTechnique.StartFinalAngle = itemToUpdate.AC_STARTFINALANGLEROT.GetValueOrDefault(0);
            oldLocationToolAssignment.TestTechnique.SlipTorque = itemToUpdate.AC_SLIPTORQUE.GetValueOrDefault(0);
            oldLocationToolAssignment.TestTechnique.TorqueCoefficient = itemToUpdate.AC_TORQUECOEFFICIENT.GetValueOrDefault(0);
            oldLocationToolAssignment.TestTechnique.MinimumPulse = (int)itemToUpdate.AC_MINIMUMPULSE.GetValueOrDefault(0);
            oldLocationToolAssignment.TestTechnique.MaximumPulse = (int)itemToUpdate.AC_MAXIMUMPULSE.GetValueOrDefault(0);
            oldLocationToolAssignment.TestTechnique.Threshold = (int)itemToUpdate.AC_THRESHOLD.GetValueOrDefault(0);
            oldLocationToolAssignment.TestParameters.ControlledBy = (LocationControlledBy) itemToUpdate.I_CTRL.GetValueOrDefault(0);
            oldLocationToolAssignment.TestParameters.Alive = itemToUpdate.ALIVE.GetValueOrDefault(false);
            LoadLocationToolAssignmentTestLevelSet(oldLocationToolAssignment, itemToUpdate);
            oldLocationToolAssignment.TestLevelNumberMfu = itemToUpdate.TestLevelNumberMfu.GetValueOrDefault(0);
            oldLocationToolAssignment.TestLevelNumberChk = itemToUpdate.TestLevelNumberChk.GetValueOrDefault(0);
            oldLocationToolAssignment.StartDateMfu = itemToUpdate.TESTSTART_MFU.GetValueOrDefault(new DateTime());
            oldLocationToolAssignment.StartDateChk = itemToUpdate.TESTSTART.GetValueOrDefault(new DateTime());
            oldLocationToolAssignment.TestOperationActiveMfu = itemToUpdate.PLANOK_MFU.GetValueOrDefault(false);
            oldLocationToolAssignment.TestOperationActiveChk = itemToUpdate.PLANOK.GetValueOrDefault(false);
        }

        private void UpdateSingleCondRotWithHistory(LocationToolAssignmentDiff locationToolAssignmentDiff, DbEntities.CondRot locationToolAssignmentToUpdate)
        {
            var currentTimestamp = _timeDataAccess.UtcNow();
            var globalHistoryId = _globalHistoryDataAccess.CreateGlobalHistory("DATA UPDATED", currentTimestamp);
            AddSpecificCondRotHistoryEntry(globalHistoryId);

            UpdateSingleCondRot(locationToolAssignmentToUpdate, locationToolAssignmentDiff, currentTimestamp);

            var condRotChanges = CreateCondRotChanges(globalHistoryId, locationToolAssignmentDiff);
            AddCondRotChangesForUpdate(locationToolAssignmentDiff, condRotChanges);
            _dbContext.SaveChanges();
        }

        private void UpdateSingleCondRot(DbEntities.CondRot condRotToUpdate, LocationToolAssignmentDiff locationToolAssignmentDiff, DateTime currentTimestamp)
        {
            UpdateDbCondRotFromLocationToolAssignmentEntity(condRotToUpdate, locationToolAssignmentDiff.GetNewLocationToolAssignment());
            condRotToUpdate.LOCPOWID = condRotToUpdate.LOCPOWID;
            condRotToUpdate.TSA = currentTimestamp;
        }

        private void UpdateDbCondRotFromLocationToolAssignmentEntity(DbEntities.CondRot dbCondRot, LocationToolAssignment locationToolAssignmentEntity)
        {
            dbCondRot.ALIVE = locationToolAssignmentEntity.TestParameters?.Alive;
            dbCondRot.NOM1 = locationToolAssignmentEntity.TestParameters?.SetPoint1;
            dbCondRot.MIN1 = locationToolAssignmentEntity.TestParameters?.Minimum1;
            dbCondRot.MAX1 = locationToolAssignmentEntity.TestParameters?.Maximum1;
            dbCondRot.MS = locationToolAssignmentEntity.TestParameters?.Threshold1;
            dbCondRot.NOM2 = locationToolAssignmentEntity.TestParameters?.SetPoint2;
            dbCondRot.MIN2 = locationToolAssignmentEntity.TestParameters?.Minimum2;
            dbCondRot.MAX2 = locationToolAssignmentEntity.TestParameters?.Maximum2;
            dbCondRot.CLASSID = locationToolAssignmentEntity.TestParameters?.ToleranceClass1?.Id?.ToLong() ?? 0;
            dbCondRot.CLASSID2 = locationToolAssignmentEntity.TestParameters?.ToleranceClass2?.Id?.ToLong() ?? 0;
            dbCondRot.AC_ENDTIME = locationToolAssignmentEntity.TestTechnique?.EndCycleTime;
            dbCondRot.AC_FILTERFREQ = locationToolAssignmentEntity.TestTechnique?.FilterFrequency;
            dbCondRot.AC_CYCLECOMPLETE = locationToolAssignmentEntity.TestTechnique?.CycleComplete;
            dbCondRot.AC_MEASUREDELAYTIME = locationToolAssignmentEntity.TestTechnique?.MeasureDelayTime;
            dbCondRot.AC_RESETTIME = locationToolAssignmentEntity.TestTechnique?.ResetTime;
            dbCondRot.AC_CMCMKSPCTESTTYPE = locationToolAssignmentEntity.TestTechnique?.MustTorqueAndAngleBeInLimits;
            dbCondRot.AC_CYCLESTARTROT = locationToolAssignmentEntity.TestTechnique?.CycleStart;
            dbCondRot.AC_STARTFINALANGLEROT = locationToolAssignmentEntity.TestTechnique?.StartFinalAngle;
            dbCondRot.AC_SLIPTORQUE = locationToolAssignmentEntity.TestTechnique?.SlipTorque;
            dbCondRot.AC_TORQUECOEFFICIENT = locationToolAssignmentEntity.TestTechnique?.TorqueCoefficient;
            dbCondRot.AC_MINIMUMPULSE = locationToolAssignmentEntity.TestTechnique?.MinimumPulse;
            dbCondRot.AC_MAXIMUMPULSE = locationToolAssignmentEntity.TestTechnique?.MaximumPulse;
            dbCondRot.AC_THRESHOLD = locationToolAssignmentEntity.TestTechnique?.Threshold;
            dbCondRot.I_CTRL = (long)(locationToolAssignmentEntity.TestParameters?.ControlledBy ?? LocationControlledBy.Angle);
            dbCondRot.TestLevelSetIdMfu = locationToolAssignmentEntity.TestLevelSetMfu?.Id.ToLong() ?? 0;
            dbCondRot.TestLevelSetIdChk = locationToolAssignmentEntity.TestLevelSetChk?.Id.ToLong() ?? 0;
            dbCondRot.TestLevelNumberMfu = locationToolAssignmentEntity.TestLevelNumberMfu;
            dbCondRot.TestLevelNumberChk = locationToolAssignmentEntity.TestLevelNumberChk;
            dbCondRot.TESTSTART_MFU = locationToolAssignmentEntity.StartDateMfu;
            dbCondRot.TESTSTART = locationToolAssignmentEntity.StartDateChk;
            dbCondRot.PLANOK_MFU = locationToolAssignmentEntity.TestOperationActiveMfu;
            dbCondRot.PLANOK = locationToolAssignmentEntity.TestOperationActiveChk;
        }

        private void AddCondRotChangesForUpdate(LocationToolAssignmentDiff locationToolAssignmentDiff, CondRotChanges change)
        {
            var oldLocationToolAssignment = locationToolAssignmentDiff.GetOldLocationToolAssignment();
            var newLocationToolAssignment = locationToolAssignmentDiff.GetNewLocationToolAssignment();

            change.Action = "UPDATE";

            change.ALIVEOLD = oldLocationToolAssignment.TestParameters?.Alive;
            change.ALIVENEW = newLocationToolAssignment.TestParameters?.Alive;
            change.NOM1OLD = oldLocationToolAssignment.TestParameters?.SetPoint1;
            change.NOM1NEW = newLocationToolAssignment.TestParameters?.SetPoint1;
            change.MIN1OLD = oldLocationToolAssignment.TestParameters?.Minimum1;
            change.MIN1NEW = newLocationToolAssignment.TestParameters?.Minimum1;
            change.MAX1OLD = oldLocationToolAssignment.TestParameters?.Maximum1;
            change.MAX1NEW = newLocationToolAssignment.TestParameters?.Maximum1;
            change.MSOLD = oldLocationToolAssignment.TestParameters?.Threshold1;
            change.MSNEW = newLocationToolAssignment.TestParameters?.Threshold1;
            change.NOM2OLD = oldLocationToolAssignment.TestParameters?.SetPoint2;
            change.NOM2NEW = newLocationToolAssignment.TestParameters?.SetPoint2;
            change.MIN2OLD = oldLocationToolAssignment.TestParameters?.Minimum2;
            change.MIN2NEW = newLocationToolAssignment.TestParameters?.Minimum2;
            change.MAX2OLD = oldLocationToolAssignment.TestParameters?.Maximum2;
            change.MAX2NEW = newLocationToolAssignment.TestParameters?.Maximum2;
            change.CLASSIDOLD = oldLocationToolAssignment.TestParameters?.ToleranceClass1?.Id?.ToLong() ?? 0;
            change.CLASSIDNEW = newLocationToolAssignment.TestParameters?.ToleranceClass1?.Id?.ToLong() ?? 0;
            change.CLASSID2OLD = oldLocationToolAssignment.TestParameters?.ToleranceClass2?.Id?.ToLong() ?? 0;
            change.CLASSID2NEW = newLocationToolAssignment.TestParameters?.ToleranceClass2?.Id?.ToLong() ?? 0;
            change.AC_ENDTIMEOLD = oldLocationToolAssignment.TestTechnique?.EndCycleTime;
            change.AC_ENDTIMENEW = newLocationToolAssignment.TestTechnique?.EndCycleTime;
            change.AC_FILTERFREQOLD = oldLocationToolAssignment.TestTechnique?.FilterFrequency;
            change.AC_FILTERFREQNEW = newLocationToolAssignment.TestTechnique?.FilterFrequency;
            change.AC_CYCLECOMPLETEOLD = oldLocationToolAssignment.TestTechnique?.CycleComplete;
            change.AC_CYCLECOMPLETENEW = newLocationToolAssignment.TestTechnique?.CycleComplete;
            change.AC_MEASUREDELAYTIMEOLD = oldLocationToolAssignment.TestTechnique?.MeasureDelayTime;
            change.AC_MEASUREDELAYTIMENEW = newLocationToolAssignment.TestTechnique?.MeasureDelayTime;
            change.AC_RESETTIMEOLD = oldLocationToolAssignment.TestTechnique?.ResetTime;
            change.AC_RESETTIMENEW = newLocationToolAssignment.TestTechnique?.ResetTime;
            change.AC_CMCMKSPCTESTTYPEOLD = oldLocationToolAssignment.TestTechnique?.MustTorqueAndAngleBeInLimits;
            change.AC_CMCMKSPCTESTTYPENEW = newLocationToolAssignment.TestTechnique?.MustTorqueAndAngleBeInLimits;
            change.AC_CYCLESTARTROTOLD = oldLocationToolAssignment.TestTechnique?.CycleStart;
            change.AC_CYCLESTARTROTNEW = newLocationToolAssignment.TestTechnique?.CycleStart;
            change.AC_STARTFINALANGLEROTOLD = oldLocationToolAssignment.TestTechnique?.StartFinalAngle;
            change.AC_STARTFINALANGLEROTNEW = newLocationToolAssignment.TestTechnique?.StartFinalAngle;
            change.AC_SLIPTORQUEOLD = oldLocationToolAssignment.TestTechnique?.SlipTorque;
            change.AC_SLIPTORQUENEW = newLocationToolAssignment.TestTechnique?.SlipTorque;
            change.AC_TORQUECOEFFICIENTOLD = oldLocationToolAssignment.TestTechnique?.TorqueCoefficient;
            change.AC_TORQUECOEFFICIENTNEW = newLocationToolAssignment.TestTechnique?.TorqueCoefficient;
            change.AC_MINIMUMPULSEOLD = oldLocationToolAssignment.TestTechnique?.MinimumPulse;
            change.AC_MINIMUMPULSENEW = newLocationToolAssignment.TestTechnique?.MinimumPulse;
            change.AC_MAXIMUMPULSEOLD = oldLocationToolAssignment.TestTechnique?.MaximumPulse;
            change.AC_MAXIMUMPULSENEW = newLocationToolAssignment.TestTechnique?.MaximumPulse;
            change.AC_THRESHOLDOLD = oldLocationToolAssignment.TestTechnique?.Threshold;
            change.AC_THRESHOLDNEW = newLocationToolAssignment.TestTechnique?.Threshold;
            change.I_CTRLOLD = (long)(oldLocationToolAssignment.TestParameters?.ControlledBy ?? LocationControlledBy.Angle);
            change.I_CTRLNEW = (long)(newLocationToolAssignment.TestParameters?.ControlledBy ?? LocationControlledBy.Angle);
            change.TestLevelSetIdMfuNew = newLocationToolAssignment.TestLevelSetMfu?.Id.ToLong() ?? 0;
            change.TestLevelSetIdMfuOld = oldLocationToolAssignment.TestLevelSetMfu?.Id.ToLong() ?? 0;
            change.TestLevelSetIdChkNew = newLocationToolAssignment.TestLevelSetChk?.Id.ToLong() ?? 0;
            change.TestLevelSetIdChkOld = oldLocationToolAssignment.TestLevelSetChk?.Id.ToLong() ?? 0;
            change.TestLevelNumberMfuNew = newLocationToolAssignment.TestLevelNumberMfu;
            change.TestLevelNumberMfuOld = oldLocationToolAssignment.TestLevelNumberMfu;
            change.TestLevelNumberChkNew = newLocationToolAssignment.TestLevelNumberChk;
            change.TestLevelNumberChkOld = oldLocationToolAssignment.TestLevelNumberChk;
            change.TESTSTART_MFUNEW = newLocationToolAssignment.StartDateMfu;
            change.TESTSTART_MFUOLD = oldLocationToolAssignment.StartDateMfu;
            change.TESTSTARTNEW = newLocationToolAssignment.StartDateChk;
            change.TESTSTARTOLD = oldLocationToolAssignment.StartDateChk;
            change.PLANOK_MFUNEW = newLocationToolAssignment.TestOperationActiveMfu;
            change.PLANOK_MFUOLD = oldLocationToolAssignment.TestOperationActiveMfu;
            change.PLANOKNEW = newLocationToolAssignment.TestOperationActiveChk;
            change.PLANOKOLD = oldLocationToolAssignment.TestOperationActiveChk;

            _dbContext.CondRotChanges.Add(change);
        }

        private CondRotChanges CreateCondRotChanges(long globalHistoryId, LocationToolAssignmentDiff locationToolAssignmentDiff)
        {
            var newLocationToolAssignment = locationToolAssignmentDiff.GetNewLocationToolAssignment();

            var locationToolAssignmentChanges = new CondRotChanges()
            {
                GlobalHistoryId = globalHistoryId,
                CondRotId = newLocationToolAssignment.Id.ToLong(),
                UserId = locationToolAssignmentDiff.GetUser().UserId.ToLong(),
                USERCOMMENT = locationToolAssignmentDiff.GetHistoryComment()?.ToDefaultString()
            };

            return locationToolAssignmentChanges;
        }

        private void AddSpecificCondRotHistoryEntry(long globalHistoryId)
        {
            var locationToolAssignmentHistory = new CondRotHistory() { GLOBALHISTORYID = globalHistoryId };
            _dbContext.CondRotHistories.Add(locationToolAssignmentHistory);
        }
    }
}
