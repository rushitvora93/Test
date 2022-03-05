using System;
using Core;
using Core.Entities;
using Core.Enums;
using Core.PhysicalValueTypes;

namespace Client.Core.Entities
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
        public Torque UpperMeasuringLimit { get; set; }
        public Torque LowerMeasuringLimit { get; set; }
        public Torque UpperInterventionLimit { get; set; }
        public Torque LowerInterventionLimit { get; set; }
        public TestLevelSet TestLevelSet { get; set; }
        public int TestLevelNumber { get; set; }
        public bool TestOperationActive { get; set; }
        public DateTime? StartDate { get; set; }
        public ProcessControlTech ProcessControlTech { get; set; }
        public DateTime? NextTestDate { get; set; }
        public Shift? NextTestShift { get; set; }

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
                UpperMeasuringLimit = Torque.FromNm(this.UpperMeasuringLimit.Nm),
                LowerMeasuringLimit = Torque.FromNm(this.LowerMeasuringLimit.Nm),
                UpperInterventionLimit = Torque.FromNm(this.UpperInterventionLimit.Nm),
                LowerInterventionLimit = Torque.FromNm(this.LowerInterventionLimit.Nm),
                TestLevelSet = this.TestLevelSet?.CopyDeep(),
                TestLevelNumber = this.TestLevelNumber,
                StartDate = this.StartDate,
                TestOperationActive = this.TestOperationActive,
                ProcessControlTech = this.ProcessControlTech?.CopyDeep()
            };
        }

        public ProcessControlValidationError? Validate(string property)
        {
            switch (property)
            {
                case nameof(UpperMeasuringLimit):
                    if (UpperMeasuringLimit.Nm <= LowerMeasuringLimit.Nm || UpperMeasuringLimit.Nm < 0 || UpperMeasuringLimit.Nm > 9999)
                    {
                        return ProcessControlValidationError.UpperMeasuringLimitLessThanOrEqualToLowerMeasuringLimitOrNotBetween0And9999;
                    }
                    break;
                case nameof(LowerMeasuringLimit):
                    if (UpperMeasuringLimit.Nm <= LowerMeasuringLimit.Nm || LowerMeasuringLimit.Nm < 0 || LowerMeasuringLimit.Nm > 9999)
                    {
                        return ProcessControlValidationError.LowerMeasuringLimitGreaterThanOrEqualToUpperMeasuringLimitOrNotBetween0And9999;
                    }
                    break;
                case nameof(UpperInterventionLimit):
                    if (UpperInterventionLimit.Nm < LowerInterventionLimit.Nm || UpperInterventionLimit.Nm < 0 || UpperInterventionLimit.Nm > 9999)
                    {
                        return ProcessControlValidationError.UpperInterventionLimitLessThanLowerInterventionLimitOrNotBetween0And9999;
                    }
                    break;
                case nameof(LowerInterventionLimit):
                    if (UpperInterventionLimit.Nm < LowerInterventionLimit.Nm || LowerInterventionLimit.Nm < 0 || LowerInterventionLimit.Nm > 9999)
                    {
                        return ProcessControlValidationError.LowerInterventionLimitGreaterThanUpperMeasuringLimitOrNotBetween0And9999;
                    }
                    break;

            }

            return null;
        }
    }

    public enum ProcessControlValidationError
    {
        UpperMeasuringLimitLessThanOrEqualToLowerMeasuringLimitOrNotBetween0And9999,
        LowerMeasuringLimitGreaterThanOrEqualToUpperMeasuringLimitOrNotBetween0And9999,
        UpperInterventionLimitLessThanLowerInterventionLimitOrNotBetween0And9999,
        LowerInterventionLimitGreaterThanUpperMeasuringLimitOrNotBetween0And9999
    }
}

