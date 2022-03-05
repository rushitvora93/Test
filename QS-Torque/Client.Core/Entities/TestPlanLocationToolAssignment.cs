using System;
using System.Collections.Generic;
using System.Linq;
using Core.Enums;

namespace Core.Entities
{
    public class TestPlanLocationToolAssignmentId : QstIdentifier, IEquatable<TestPlanLocationToolAssignmentId>
    {
        public TestPlanLocationToolAssignmentId(long value)
            : base(value)
        {
        }

        public bool Equals(TestPlanLocationToolAssignmentId other)
        {
            return Equals((QstIdentifier)other);
        }
    }

    public class TestPlanLocationToolAssignment : IQstEquality<TestPlanLocationToolAssignment>, IUpdate<TestPlanLocationToolAssignment>, ICopy<TestPlanLocationToolAssignment>
    {
        public TestPlanLocationToolAssignmentId Id { get; set; } = new TestPlanLocationToolAssignmentId(0);
        public LocationToolAssignment LocationToolAssignment { get; set; }
        public List<TestPlan> TestPlans { get; set; } = new List<TestPlan>();
        public bool IsActive { get; set; }
        public TestType TestType { get; set; }
        public DateTime TestPeriodStartDate { get; set; }
        public Shift TestPeriodStartShift { get; set; }

        public bool EqualsById(TestPlanLocationToolAssignment other)
        {
            return Id.Equals(other?.Id);
        }

        public bool EqualsByContent(TestPlanLocationToolAssignment other)
        {
            if (other == null)
            {
                return false;
            }

            if(!TestPlans?.TrueForAll(x => other.TestPlans.Any(y => x.EqualsByContent(y))) ?? other.TestPlans != null)
            {
                return false;
            }

            return Id.Equals(other.Id) &&
                   (LocationToolAssignment?.EqualsByContent(other.LocationToolAssignment) ?? other.LocationToolAssignment == null) &&
                   IsActive == other.IsActive &&
                   TestType == other.TestType &&
                   TestPeriodStartDate.Date == other.TestPeriodStartDate.Date &&
                   TestPeriodStartShift == other.TestPeriodStartShift;
        }

        public void UpdateWith(TestPlanLocationToolAssignment other)
        {
            if (other == null)
            {
                return;
            }

            this.Id = other.Id;
            this.LocationToolAssignment = other.LocationToolAssignment;
            this.IsActive = other.IsActive;
            this.TestType = other.TestType;
            this.TestPeriodStartDate = other.TestPeriodStartDate.Date;
            this.TestPeriodStartShift = other.TestPeriodStartShift;

            this.TestPlans = new List<TestPlan>();
            foreach (var item in other.TestPlans)
            {
                this.TestPlans.Add(item);
            }
        }

        public TestPlanLocationToolAssignment CopyDeep()
        {
            var newAssignment = new TestPlanLocationToolAssignment()
            {
                Id = this.Id != null ? new TestPlanLocationToolAssignmentId(this.Id.ToLong()) : null,
                LocationToolAssignment = this.LocationToolAssignment?.CopyDeep(),
                IsActive = this.IsActive,
                TestType = this.TestType,
                TestPeriodStartDate = this.TestPeriodStartDate,
                TestPeriodStartShift = this.TestPeriodStartShift
            };

            newAssignment.TestPlans = new List<TestPlan>();
            foreach (var item in this.TestPlans)
            {
                newAssignment.TestPlans.Add(item.CopyDeep());
            }

            return newAssignment;
        }
    }
}
