using System;
using System.Collections.Generic;

namespace Core.Entities
{
    public class TestPlanId : QstIdentifier, IEquatable<TestPlanId>
    {
        public TestPlanId(long value)
            : base(value)
        {
        }

        public bool Equals(TestPlanId other)
        {
            return Equals((QstIdentifier)other);
        }
    }

    public class TestPlanName : TypeCheckedString<MaxLength<CtInt200>, Blacklist<NewLines>, NoCheck>
    {
        public TestPlanName(string name)
            : base(name)
        {
        }
    }

    public enum TestPlanBehavior
    {
        Dynamic,
        Static
    }

    public enum TestPlanValidationError
    {
        IntervalIsLessThanOneShift,
        SampleNumberIsLessThanOne,
        EndDateIsNotGreaterThanStartDate
    }

    public class TestPlan : IQstEquality<TestPlan>, IUpdate<TestPlan>, ICopy<TestPlan>
    {
        public TestPlanId Id { get; set; }
        public TestPlanName Name { get; set; }
        public Interval TestInterval { get; set; } = new Interval();
        public long SampleNumber { get; set; }
        public TestPlanBehavior Behavior { get; set; }
        public bool ConsiderHolidays { get; set; }
        public DateTime StartDate { get; set; }
        public bool IsEndDateEnabled { get; set; }
        public DateTime EndDate { get; set; }

        public bool EqualsById(TestPlan other)
        {
            return Id.Equals(other?.Id);
        }

        public bool EqualsByContent(TestPlan other)
        {
            if (other == null)
            {
                return false;
            }

            return Id.Equals(other.Id) &&
                   (Name?.Equals(other.Name) ?? other.Name == null) &&
                   (TestInterval?.EqualsByContent(other.TestInterval) ?? other.TestInterval == null) &&
                   SampleNumber == other.SampleNumber &&
                   Behavior == other.Behavior &&
                   ConsiderHolidays == other.ConsiderHolidays &&
                   StartDate == other.StartDate &&
                   IsEndDateEnabled == other.IsEndDateEnabled &&
                   EndDate == other.EndDate;
        }

        public void UpdateWith(TestPlan other)
        {
            if (other == null)
            {
                return;
            }

            this.Id = new TestPlanId(other.Id.ToLong());
            this.Name = new TestPlanName(other.Name.ToDefaultString());
            this.TestInterval = other.TestInterval.CopyDeep();
            this.SampleNumber = other.SampleNumber;
            this.Behavior = other.Behavior;
            this.ConsiderHolidays = other.ConsiderHolidays;
            this.StartDate = other.StartDate;
            this.IsEndDateEnabled = other.IsEndDateEnabled;
            this.EndDate = other.EndDate;
        }

        public TestPlan CopyDeep()
        {
            return new TestPlan()
            {
                Id = this.Id != null ? new TestPlanId(this.Id.ToLong()) : null,
                Name = Name != null ? new TestPlanName(Name?.ToDefaultString()) : null,
                TestInterval = TestInterval.CopyDeep(),
                SampleNumber = SampleNumber,
                Behavior = Behavior,
                ConsiderHolidays = ConsiderHolidays,
                StartDate = StartDate,
                IsEndDateEnabled = IsEndDateEnabled,
                EndDate = EndDate
            };
        }

        public IEnumerable<TestPlanValidationError> Validate(string property)
        {
            switch (property)
            {
                case nameof(TestInterval):
                    if(TestInterval.IntervalValue <= 0)
                    {
                        yield return TestPlanValidationError.IntervalIsLessThanOneShift;
                    }
                    break;
                case nameof(SampleNumber):
                    if(SampleNumber <= 0)
                    {
                        yield return TestPlanValidationError.SampleNumberIsLessThanOne;
                    }
                    break;
                case nameof(EndDate):
                    if(IsEndDateEnabled && StartDate.Date >= EndDate.Date)
                    {
                        yield return TestPlanValidationError.EndDateIsNotGreaterThanStartDate;
                    }
                    break;
            }
        }
    }

    // Represents the DTO of the server, will be replaced by the server DTO in the future
    public class TestPlan_Dto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long TestIntervalValue { get; set; }
        public IntervalType TestIntervalType { get; set; }
        public long SampleNumber { get; set; }
        public TestPlanBehavior Behavior { get; set; }
        public bool ConsiderHolidays { get; set; }
        public DateTime StartDate { get; set; }
        public bool IsEndDateEnabled { get; set; }
        public DateTime EndDate { get; set; }
    }
}
