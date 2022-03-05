using System;
using System.Collections.Generic;
using System.Linq;
using Core.Diffs;
using Core.Entities;
using Core.Entities.ReferenceLink;
using Core.UseCases;

namespace TestHelper.Mock
{
    public class LocationToolAssignmentDataMock : ILocationToolAssignmentData
    {
        public List<LocationToolAssignmentId> GetLocationToolAssignmentsByIdsParameter;
        public List<LocationToolAssignment> GetLocationToolAssignmentsByIdsReturnValue;
        public int LoadAssignedToolsForLocationCallCount { get; set; }
        public List<LocationToolAssignment> LoadAssignedToolsForLocationReturn = new List<LocationToolAssignment>();
        public int RemoveLocationToolAssignmentCallCount { get; set; }
        public bool LoadAssignedToolsForLocationThrowsException { get; set; } = false;
        public int LoadLocationReferenceLinksForToolCallCount { get; set; }
        public List<ToolId> LoadLocationReferenceLinksForToolParameter { get; set; } = new List<ToolId>();
        public Dictionary<ToolId, List<LocationReferenceLink>> LoadLocationReferenceLinksForToolReturnValue { get; set; }
        public bool LoadLocationReferenceLinksForToolThrowsException { get; set; }
        public int UpdateNextChkTestDateCallCount { get; set; }
        public int UpdateNextMfuTestDateCalCount { get; set; }

        public DateTime UpdateNextChkTestDateTestTimestampParameter { get; set; }

        public LocationToolAssignment UpdateNextChkTestDateLocationToolAssignmentParameter { get; set; }

        public DateTime UpdateNextMfuTestDateTestTimestampParameter { get; set; }

        public LocationToolAssignment UpdateNextMfuTestDateLocationToolAssignmentParameter { get; set; }

        public List<LocationToolAssignment> LoadLocationToolAssignments()
        {
            throw new NotImplementedException();
        }

        public List<LocationToolAssignment> GetLocationToolAssignmentsByIds(List<LocationToolAssignmentId> ids)
        {
            GetLocationToolAssignmentsByIdsParameter = ids;
            return GetLocationToolAssignmentsByIdsReturnValue;
        }

        public void AssignToolToLocation(LocationToolAssignment assignment, User user)
        {
            throw new NotImplementedException();
        }

        public List<LocationToolAssignment> LoadAssignedToolsForLocation(LocationId locationId)
        {
            if (LoadAssignedToolsForLocationThrowsException)
            {
                throw new Exception();
            }
            LoadAssignedToolsForLocationCallCount++;
            return LoadAssignedToolsForLocationReturn;
        }

        public void AddTestConditions(LocationToolAssignment assignment, User user)
        {
            throw new NotImplementedException();
        }

        public List<ToolUsage> LoadUnusedToolUsagesForLocation(LocationId locationId)
        {
            throw new NotImplementedException();
        }

        public void RemoveLocationToolAssignment(LocationToolAssignment assignment, User user)
        {
            RemoveLocationToolAssignmentCallCount++;
        }

        public List<LocationReferenceLink> LoadLocationReferenceLinksForTool(ToolId toolId)
        {
            if (LoadLocationReferenceLinksForToolThrowsException)
            {
                throw new Exception();
            }
            LoadLocationReferenceLinksForToolParameter.Add(toolId);
            LoadLocationReferenceLinksForToolCallCount++;
            return LoadLocationReferenceLinksForToolReturnValue?.FirstOrDefault(x => x.Key.Equals(toolId)).Value??null;
        }

        public void UpdateLocationToolAssignment(List<LocationToolAssignmentDiff> diff)
        {
            throw new NotImplementedException();
        }

        public void UpdateNextChkTestDate(LocationToolAssignment assignment, DateTime testTimestamp)
        {
            UpdateNextChkTestDateLocationToolAssignmentParameter = assignment;
            UpdateNextChkTestDateTestTimestampParameter = testTimestamp;
            UpdateNextChkTestDateCallCount++;
        }


        public void UpdateNextMfuTestDate(LocationToolAssignment assignment, DateTime testTimestamp)
        {
            UpdateNextMfuTestDateLocationToolAssignmentParameter = assignment;
            UpdateNextMfuTestDateTestTimestampParameter = testTimestamp;
            UpdateNextMfuTestDateCalCount++;
        }

        public void RestoreLocationToolAssignment(LocationToolAssignment assignment, User user)
        {
            throw new NotImplementedException();
        }
    }
}
