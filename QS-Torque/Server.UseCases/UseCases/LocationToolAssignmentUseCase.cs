using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Server.Core.Diffs;
using Server.Core.Entities;
using Server.Core.Entities.ReferenceLink;
using Server.Core.Enums;

namespace Server.UseCases.UseCases
{
    public interface ILocationToolAssignmentData
    {
        void Commit();
        List<LocationToolAssignment> LoadLocationToolAssignments();
        LocationToolAssignment GetLocationToolAssignmentById(LocationToolAssignmentId id);
        List<LocationToolAssignmentId> GetLocationToolAssignmentIdsForTestLevelSet(TestLevelSetId id);
        void SaveNextTestDatesFor(LocationToolAssignmentId id, DateTime? nextTestDateMfu, Shift? nextTestShiftMfu, DateTime? nextTestDateChk, Shift? nextTestShiftChk,
        DateTime? endOfLastTestPeriodMfu, Shift? endOfLastTestPeriodShiftMfu, DateTime? endOfLastTestPeriodChk, Shift? endOfLastTestPeriodShiftChk);
        List<LocationReferenceLink> LoadLocationReferenceLinksForTool(ToolId toolId);
        List<ToolUsage> LoadUnusedToolUsagesForLocation(LocationId locationId);
        List<LocationToolAssignment> GetLocationToolAssignmentsByLocationId(LocationId locationId);
        List<LocationToolAssignment> GetLocationToolAssignmentsByIds(List<LocationToolAssignmentId> ids);
        List<LocationToolAssignment> InsertLocPowsWithHistory(List<LocationToolAssignmentDiff> locationToolAssignmentDiffs, bool returnList);
        List<LocationToolAssignment> InsertCondRotsWithHistory(List<LocationToolAssignmentDiff> locationToolAssignmentDiffs, bool returnList);
        List<LocationToolAssignment> UpdateLocPowsWithHistory(List<LocationToolAssignmentDiff> locationToolAssignmentDiffs);
        List<LocationToolAssignment> UpdateCondRotsWithHistory(List<LocationToolAssignmentDiff> locationToolAssignmentDiffs);
    }


    public interface ILocationToolAssignmentUseCase
    {
        List<LocationToolAssignment> LoadLocationToolAssignments();
        List<LocationReferenceLink> LoadLocationReferenceLinksForTool(ToolId toolId);
        List<ToolUsage> LoadUnusedToolUsagesForLocation(LocationId locationId);
        List<LocationToolAssignment> GetLocationToolAssignmentsByLocationId(LocationId locationId);
        List<LocationToolAssignment> GetLocationToolAssignmentsByIds(List<LocationToolAssignmentId> ids);
        void AddTestConditions(LocationToolAssignment assignment, User user);
        void UpdateLocationToolAssignment(List<LocationToolAssignmentDiff> diffs);
        List<LocationToolAssignmentId> InsertLocationToolAssignment(List<LocationToolAssignmentDiff> diffs);
    }


    public class LocationToolAssignmentUseCase : ILocationToolAssignmentUseCase
    {
        private ILocationToolAssignmentData _locationToolAssignmentData;

        public LocationToolAssignmentUseCase(ILocationToolAssignmentData locationToolAssignmentData)
        {
            _locationToolAssignmentData = locationToolAssignmentData;
        }
        
        public List<LocationToolAssignment> LoadLocationToolAssignments()
        {
            return _locationToolAssignmentData.LoadLocationToolAssignments();
        }

        public List<LocationReferenceLink> LoadLocationReferenceLinksForTool(ToolId toolId)
        {
            return _locationToolAssignmentData.LoadLocationReferenceLinksForTool(toolId);
        }

        public List<ToolUsage> LoadUnusedToolUsagesForLocation(LocationId locationId)
        {
            return _locationToolAssignmentData.LoadUnusedToolUsagesForLocation(locationId);
        }

        public List<LocationToolAssignment> GetLocationToolAssignmentsByLocationId(LocationId locationId)
        {
            return _locationToolAssignmentData.GetLocationToolAssignmentsByLocationId(locationId);
        }

        public List<LocationToolAssignment> GetLocationToolAssignmentsByIds(List<LocationToolAssignmentId> ids)
        {
            return _locationToolAssignmentData.GetLocationToolAssignmentsByIds(ids);
        }

        public List<LocationToolAssignmentId> InsertLocationToolAssignment(List<LocationToolAssignmentDiff> diffs)
        {
            var locationToolAssignments = _locationToolAssignmentData.InsertLocPowsWithHistory(diffs, true);

            _locationToolAssignmentData.InsertCondRotsWithHistory(
                diffs.Where(x => x.GetNewLocationToolAssignment().TestParameters != null).ToList(), false);

            _locationToolAssignmentData.Commit();

            return locationToolAssignments.Select(x => x.Id).ToList();
        }

        public void UpdateLocationToolAssignment(List<LocationToolAssignmentDiff> diffs)
        {
            var diffs4UpdateLocPow = diffs.Where(x =>
                !CheckForLocPowChanges(x.GetOldLocationToolAssignment(), x.GetNewLocationToolAssignment()))
                .ToList();
            
            _locationToolAssignmentData.UpdateLocPowsWithHistory(diffs4UpdateLocPow);

            var diffs4UpdateCondRot = diffs.Where(x =>
                x.GetNewLocationToolAssignment().TestParameters != null &&
                !CheckForCondRotChanges(x.GetOldLocationToolAssignment(), x.GetNewLocationToolAssignment()))
                .ToList();

            diffs4UpdateCondRot.ForEach(x =>
            {
                if(x.GetOldLocationToolAssignment().StartDateMfu != x.GetNewLocationToolAssignment().StartDateMfu && x.GetOldLocationToolAssignment().StartDateChk == x.GetNewLocationToolAssignment().StartDateChk)
                {
                    _locationToolAssignmentData.SaveNextTestDatesFor(x.GetNewLocationToolAssignment().Id,
                        x.GetNewLocationToolAssignment().NextTestDateMfu,
                        x.GetNewLocationToolAssignment().NextTestShiftMfu,
                        x.GetNewLocationToolAssignment().NextTestDateChk,
                        x.GetNewLocationToolAssignment().NextTestShiftChk,
                        x.GetNewLocationToolAssignment().StartDateMfu,
                        (x.GetNewLocationToolAssignment().GetTestLevel(TestType.Mfu).TestInterval.Type == IntervalType.EveryXShifts || x.GetNewLocationToolAssignment().GetTestLevel(TestType.Mfu).TestInterval.Type == IntervalType.XTimesAShift) ? Shift.FirstShiftOfDay : null,
                        x.GetNewLocationToolAssignment().EndOfLastTestPeriodChk,
                        x.GetNewLocationToolAssignment().EndOfLastTestPeriodShiftChk);
                }
                if (x.GetOldLocationToolAssignment().StartDateChk != x.GetNewLocationToolAssignment().StartDateChk && x.GetOldLocationToolAssignment().StartDateMfu == x.GetNewLocationToolAssignment().StartDateMfu)
                {
                    _locationToolAssignmentData.SaveNextTestDatesFor(x.GetNewLocationToolAssignment().Id,
                        x.GetNewLocationToolAssignment().NextTestDateMfu,
                        x.GetNewLocationToolAssignment().NextTestShiftMfu,
                        x.GetNewLocationToolAssignment().NextTestDateChk,
                        x.GetNewLocationToolAssignment().NextTestShiftChk,
                        x.GetNewLocationToolAssignment().EndOfLastTestPeriodMfu,
                        x.GetNewLocationToolAssignment().EndOfLastTestPeriodShiftMfu,
                        x.GetNewLocationToolAssignment().StartDateChk,
                        (x.GetNewLocationToolAssignment().GetTestLevel(TestType.Chk).TestInterval.Type == IntervalType.EveryXShifts || x.GetNewLocationToolAssignment().GetTestLevel(TestType.Chk).TestInterval.Type == IntervalType.XTimesAShift) ? Shift.FirstShiftOfDay : null);
                }
                if (x.GetOldLocationToolAssignment().StartDateChk != x.GetNewLocationToolAssignment().StartDateChk && x.GetOldLocationToolAssignment().StartDateMfu != x.GetNewLocationToolAssignment().StartDateMfu)
                {
                    _locationToolAssignmentData.SaveNextTestDatesFor(x.GetNewLocationToolAssignment().Id,
                        x.GetNewLocationToolAssignment().NextTestDateMfu,
                        x.GetNewLocationToolAssignment().NextTestShiftMfu,
                        x.GetNewLocationToolAssignment().NextTestDateChk,
                        x.GetNewLocationToolAssignment().NextTestShiftChk,
                        x.GetNewLocationToolAssignment().StartDateMfu,
                        (x.GetNewLocationToolAssignment().GetTestLevel(TestType.Mfu).TestInterval.Type == IntervalType.EveryXShifts || x.GetNewLocationToolAssignment().GetTestLevel(TestType.Mfu).TestInterval.Type == IntervalType.XTimesAShift) ? Shift.FirstShiftOfDay : null,
                        x.GetNewLocationToolAssignment().StartDateChk,
                        (x.GetNewLocationToolAssignment().GetTestLevel(TestType.Chk).TestInterval.Type == IntervalType.EveryXShifts || x.GetNewLocationToolAssignment().GetTestLevel(TestType.Chk).TestInterval.Type == IntervalType.XTimesAShift) ? Shift.FirstShiftOfDay : null);
                }
            });

            _locationToolAssignmentData.UpdateCondRotsWithHistory(diffs4UpdateCondRot);

            _locationToolAssignmentData.Commit();
        }

        public void AddTestConditions(LocationToolAssignment assignment, User user)
        {
            var locationToolAssignmentDiff =
                new LocationToolAssignmentDiff(user, null, assignment);

            _locationToolAssignmentData.InsertCondRotsWithHistory(
                new List<LocationToolAssignmentDiff>() { locationToolAssignmentDiff }, false);

            _locationToolAssignmentData.Commit();
        }

        private bool CheckForCondRotChanges(LocationToolAssignment oldLocationToolAssignment, LocationToolAssignment newLocationToolAssignment)
        {
            return newLocationToolAssignment.TestParameters.EqualsByContent(oldLocationToolAssignment.TestParameters) &&
                    newLocationToolAssignment.TestTechnique.EqualsByContent(oldLocationToolAssignment.TestTechnique) &&
                    (newLocationToolAssignment.TestLevelSetMfu?.EqualsByContent(oldLocationToolAssignment.TestLevelSetMfu) ?? oldLocationToolAssignment.TestLevelSetMfu == null) &&
                    (newLocationToolAssignment.TestLevelSetChk?.EqualsByContent(oldLocationToolAssignment.TestLevelSetChk) ?? oldLocationToolAssignment.TestLevelSetChk == null) &&
                    newLocationToolAssignment.TestLevelNumberMfu == oldLocationToolAssignment.TestLevelNumberMfu &&
                    newLocationToolAssignment.TestLevelNumberChk == oldLocationToolAssignment.TestLevelNumberChk &&
                    newLocationToolAssignment.StartDateMfu == oldLocationToolAssignment.StartDateMfu &&
                    newLocationToolAssignment.StartDateChk == oldLocationToolAssignment.StartDateChk &&
                    newLocationToolAssignment.TestOperationActiveMfu == oldLocationToolAssignment.TestOperationActiveMfu &&
                    newLocationToolAssignment.TestOperationActiveChk == oldLocationToolAssignment.TestOperationActiveChk;
        }

        private bool CheckForLocPowChanges(LocationToolAssignment diffNewLocationToolAssignment, LocationToolAssignment diffOldLocationToolAssignment)
        {
            return diffNewLocationToolAssignment.Id.Equals(diffOldLocationToolAssignment.Id) &&
                   diffNewLocationToolAssignment.ToolUsage.EqualsByContent(diffOldLocationToolAssignment.ToolUsage) &&
                   diffNewLocationToolAssignment.AssignedLocation.EqualsByContent(diffOldLocationToolAssignment.AssignedLocation) &&
                   diffNewLocationToolAssignment.AssignedTool.EqualsByContent(diffOldLocationToolAssignment.AssignedTool) &&
                   diffNewLocationToolAssignment.Alive == diffOldLocationToolAssignment.Alive;
        }
    }
}
