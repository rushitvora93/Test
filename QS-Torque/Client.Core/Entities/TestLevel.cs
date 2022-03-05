using System;

namespace Core.Entities
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
        public int SampleNumber { get; set; }
        public bool ConsiderWorkingCalendar { get; set; }
        public bool IsActive { get; set; }


        public bool EqualsByContent(TestLevel other)
        {
            if (other == null)
            {
                return false;
            }

            return Id.Equals(other.Id) &&
                   (TestInterval?.EqualsByContent(other.TestInterval) ?? other.TestInterval == null) &&
                   SampleNumber == other.SampleNumber &&
                   ConsiderWorkingCalendar == other.ConsiderWorkingCalendar &&
                   IsActive == other.IsActive;
        }

        public bool EqualsById(TestLevel other)
        {
            return Id.Equals(other?.Id);
        }

        public void UpdateWith(TestLevel other)
        {
            if (other == null)
            {
                return;
            }

            this.Id = new TestLevelId(other.Id.ToLong());
            this.TestInterval = other.TestInterval.CopyDeep();
            this.SampleNumber = other.SampleNumber;
            this.ConsiderWorkingCalendar = other.ConsiderWorkingCalendar;
            this.IsActive = other.IsActive;
        }

        public TestLevel CopyDeep()
        {
            return new TestLevel()
            {
                Id = this.Id != null ? new TestLevelId(this.Id.ToLong()) : null,
                TestInterval = TestInterval?.CopyDeep() ?? null,
                SampleNumber = SampleNumber,
                ConsiderWorkingCalendar = ConsiderWorkingCalendar,
                IsActive = IsActive
            };
        }
    }
}
