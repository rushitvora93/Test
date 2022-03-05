using System;
using System.Collections.Generic;
using Core.Diffs;
using Core.Entities;
using Core.UseCases;

namespace TestHelper.Mock
{
    public class LocationToolAssignmentUseCaseMock : ILocationToolAssignmentUseCase
    {
        public Location LoadAssignedToolsForLocationParameter;
        public LocationToolAssignment RemoveLocationToolAssignmentParameter;

        public LocationToolAssignmentUseCaseMock()
        {
        }

        public int UpdateLocationToolAssignmentCallCount { get; set; }
        public int LoadUnusedToolUsagesForLocationCallCount { get; set; }
        public bool LoadLocationToolAssignmentsCalled { get; set; }

        public void LoadLocationToolAssignments()
        {
            LoadLocationToolAssignmentsCalled = true;
        }

        public void AssignToolToLocation(LocationToolAssignment assignment)
        {
            throw new NotImplementedException();
        }

        public void LoadToolAssignmentsForLocation(Location location)
        {
            LoadAssignedToolsForLocationParameter = location;
        }

        public void AddTestConditions(LocationToolAssignment assignment)
        {
            throw new NotImplementedException();
        }

        public void LoadUnusedToolUsagesForLocation(LocationId locationid)
        {
            LoadUnusedToolUsagesForLocationCallCount++;
        }

        public void RemoveLocationToolAssignment(LocationToolAssignment assignment)
        {
            RemoveLocationToolAssignmentParameter = assignment;
        }

        public void LoadLocationReferencesForTool(ToolId toolId)
        {
            throw new NotImplementedException();
        }

        public void UpdateLocationToolAssignment(List<LocationToolAssignmentDiff> diff, ILocationToolAssignmentErrorHandler errorHandler = null, ILocationToolAssignmentDiffShower diffShower = null)
        {
            UpdateLocationToolAssignmentCallCount++;
        }

        public void UpdateLocationToolAssignments(List<LocationToolAssignmentDiff> diffs)
        {
            throw new NotImplementedException();
        }
    }
}
