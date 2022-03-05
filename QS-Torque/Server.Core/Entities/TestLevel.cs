using System;
using Core;
using Core.Entities;

namespace Server.Core.Entities
{
    public class TestLevelId : QstIdentifier, IEquatable<TestLevelId>
    {
        public TestLevelId(long value)
            : base(value)
        {
        }

        public bool Equals(TestLevelId other)
        {
            return Equals((QstIdentifier)other);
        }
    }

    public class TestLevel : IQstEquality<TestLevel>, IUpdate<TestLevel>, ICopy<TestLevel>
    {
        public TestLevelId Id { get; set; }
        public Interval TestInterval { get; set; } = new Interval();
        public long SampleNumber { get; set; }
        public bool ConsiderWorkingCalendar { get; set; }
        public bool IsActive { get; set; }


        public bool EqualsById(TestLevel other)
        {
            return this.Id.Equals(other?.Id);
        }

        public bool EqualsByContent(TestLevel other)
        {
            if (other == null)
            {
                return false;
            }
            
            return this.Id.Equals(other.Id) &&
                   (this.TestInterval?.EqualsByContent(other.TestInterval) ?? other.TestInterval == null) &&
                   this.SampleNumber == other.SampleNumber &&
                   this.ConsiderWorkingCalendar == other.ConsiderWorkingCalendar &&
                   this.IsActive == other.IsActive;
        }

        public void UpdateWith(TestLevel other)
        {
            if (other == null)
            {
                return;
            }

            this.Id = other.Id;
            this.TestInterval = other.TestInterval;
            this.SampleNumber = other.SampleNumber;
            this.ConsiderWorkingCalendar = other.ConsiderWorkingCalendar;
            this.IsActive = other.IsActive;
        }

        public TestLevel CopyDeep()
        {
            return new TestLevel()
            {
                Id = this.Id != null ? new TestLevelId(this.Id.ToLong()) : null,
                TestInterval = this.TestInterval?.CopyDeep(),
                SampleNumber = this.SampleNumber,
                ConsiderWorkingCalendar = this.ConsiderWorkingCalendar,
                IsActive = this.IsActive
            };
        }
    }
}
