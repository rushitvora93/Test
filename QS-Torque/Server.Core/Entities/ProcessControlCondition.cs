using System;
using Core;
using Core.Entities;
using Server.Core.Enums;

namespace Server.Core.Entities
{
    public class ProcessControlConditionId : QstIdentifier, IEquatable<ProcessControlConditionId>
    {
        public ProcessControlConditionId(long value)
            : base(value)
        {
        }

        public bool Equals(ProcessControlConditionId other)
        {
            return Equals((QstIdentifier)other);
        }
    }

    public class ProcessControlCondition : IQstEquality<ProcessControlCondition>, IUpdate<ProcessControlCondition>, ICopy<ProcessControlCondition>
    {
        public ProcessControlConditionId Id { get; set; }
        public Location Location { get; set; }
        public double? UpperMeasuringLimit { get; set; }
        public double? LowerMeasuringLimit { get; set; }
        public double? UpperInterventionLimit { get; set; }
        public double? LowerInterventionLimit { get; set; }
        public TestLevelSet TestLevelSet { get; set; }
        public int TestLevelNumber { get; set; }
        public bool TestOperationActive { get; set; }
        public DateTime? StartDate { get; set; }
        public bool Alive { get; set; }
        public ProcessControlTech ProcessControlTech { get; set; }
        public DateTime? NextTestDate { get; set; }
        public Shift? NextTestShift { get; set; }
        public DateTime? EndOfLastTestPeriod { get; set; }
        public Shift? EndOfLastTestPeriodShift { get; set; }

        public bool EqualsById(ProcessControlCondition other)
        {
            return this.Id.Equals(other?.Id);
        }

        public bool EqualsByContent(ProcessControlCondition other)
        {
            if (other == null)
            {
                return false;
            }

            return this.Id.Equals(other.Id) &&
                   (this.Location?.EqualsById(other.Location) ?? other.Location == null) &&
                   this.UpperMeasuringLimit.Equals(other.UpperMeasuringLimit) &&
                   this.LowerMeasuringLimit.Equals(other.LowerMeasuringLimit) &&
                   this.UpperInterventionLimit.Equals(other.UpperInterventionLimit) &&
                   this.LowerInterventionLimit.Equals(other.LowerInterventionLimit) &&
                   (this.TestLevelSet?.EqualsByContent(other.TestLevelSet) ?? other.TestLevelSet == null) &&
                   (this.StartDate?.Equals(other.StartDate) ?? other.StartDate == null) &&
                   this.TestOperationActive == other.TestOperationActive &&
                   (this.ProcessControlTech?.EqualsByContent(other.ProcessControlTech) ?? other.ProcessControlTech == null) &&
                   this.TestLevelNumber == other.TestLevelNumber;
        }

        public void UpdateWith(ProcessControlCondition other)
        {
            if (other == null)
            {
                return;
            }

            this.Id = other.Id;
            this.Location = other.Location;
            this.UpperMeasuringLimit = other.UpperMeasuringLimit;
            this.LowerMeasuringLimit = other.LowerMeasuringLimit;
            this.UpperInterventionLimit = other.UpperInterventionLimit;
            this.LowerInterventionLimit = other.LowerInterventionLimit;
            this.TestLevelNumber = other.TestLevelNumber;
            this.TestLevelSet = other.TestLevelSet;
            this.StartDate = other.StartDate;
            this.TestOperationActive = other.TestOperationActive;
            this.ProcessControlTech = other.ProcessControlTech;
        }

        public ProcessControlCondition CopyDeep()
        {
            return new ProcessControlCondition()
            {
                Id = this.Id != null ? new ProcessControlConditionId(this.Id.ToLong()) : null,
                Location = this.Location != null ? this.Location.CopyDeep() : null,
                UpperMeasuringLimit = this.UpperMeasuringLimit,
                LowerMeasuringLimit = this.LowerMeasuringLimit,
                UpperInterventionLimit = this.UpperInterventionLimit,
                LowerInterventionLimit = this.LowerInterventionLimit,
                TestLevelSet = this.TestLevelSet?.CopyDeep(),
                TestLevelNumber = this.TestLevelNumber,
                StartDate = this.StartDate,
                TestOperationActive = this.TestOperationActive,
                ProcessControlTech = this.ProcessControlTech?.CopyDeep()
            };
        }
    }
}
