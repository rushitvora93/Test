using System;
using Core.Entities;
using Server.Core.Enums;


namespace Server.Core.Entities
{
    public class LocationToolAssignmentForTransfer
    {
        public LocationToolAssignmentId LocationToolAssignmentId;
        public LocationNumber LocationNumber;
        public LocationId LocationId;
        public LocationDescription LocationDescription;
        public string LocationFreeFieldCategory;
        public bool LocationFreeFieldDocumentation;
        public HelperTableEntityId ToolUsageId;
        public ToolUsageDescription ToolUsageDescription;
        public ToolId ToolId;
        public string ToolSerialNumber;
        public string ToolInventoryNumber;
        public int SampleNumber { get; set; }
        public Interval TestInterval { get; set; }
        public DateTime? NextTestDate { get; set; }
        public DateTime? LastTestDate { get; set; }
        public Shift? NextTestDateShift { get; set; }
    }
}
