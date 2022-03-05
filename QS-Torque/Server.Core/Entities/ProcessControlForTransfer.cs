using System;
using Common.Types.Enums;
using Core.Entities;
using Server.Core.Enums;

namespace Server.Core.Entities
{
    public class ProcessControlForTransfer
    {
        public LocationNumber LocationNumber;
        public LocationId LocationId;
        public LocationDescription LocationDescription;
        public ProcessControlConditionId ProcessControlConditionId;
        public ProcessControlTechId ProcessControlTechId;
        public double? SetPointTorque;
        public double? MinimumTorque;
        public double? MaximumTorque;
        public TestMethod TestMethod;
        public DateTime? LastTestDate;
        public DateTime? NextTestDate;
        public Interval TestInterval;
        public int SampleNumber;
        public Shift? NextTestDateShift;
    }
}
