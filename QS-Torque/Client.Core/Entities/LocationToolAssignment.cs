﻿using System;
using Core.Enums;

namespace Core.Entities
{
    public class LocationToolAssignmentId : QstIdentifier
    {
        public LocationToolAssignmentId(long value)
            : base(value)
        {
        }

        public bool Equals(LocationToolAssignmentId other)
        {
            return Equals((QstIdentifier)other);
        }
    }

    public class LocationToolAssignment : IQstEquality<LocationToolAssignment>, IUpdate<LocationToolAssignment>, ICopy<LocationToolAssignment>
    {
        public LocationToolAssignmentId Id { get; set; }
        public Location AssignedLocation { get; set; }
        public Tool AssignedTool { get; set; }
        public ToolUsage ToolUsage { get; set; }
        public TestParameters TestParameters { get; set; }
        public TestTechnique TestTechnique { get; set; }
        public TestLevelSet TestLevelSetMfu { get; set; }
        public int TestLevelNumberMfu { get; set; }
        public TestLevelSet TestLevelSetChk { get; set; }
        public int TestLevelNumberChk { get; set; }
        public DateTime? NextTestDateMfu { get; set; }
        public Shift? NextTestShiftMfu { get; set; }
        public DateTime? NextTestDateChk { get; set; }
        public Shift? NextTestShiftChk { get; set; }
        public DateTime StartDateMfu { get; set; }
        public DateTime StartDateChk { get; set; }
        public bool TestOperationActiveMfu { get; set; }
        public bool TestOperationActiveChk { get; set; }


        public bool EqualsById(LocationToolAssignment other)
        {
            return this.Id.Equals(other?.Id);
        }

        public bool EqualsByContent(LocationToolAssignment other)
        {
            if (other == null)
            {
                return false;
            }

            return this.Id.Equals(other.Id) &&
                   (this.AssignedLocation?.EqualsByContent(other.AssignedLocation) ?? other.AssignedLocation == null) &&
                   (this.AssignedTool?.EqualsByContent(other.AssignedTool) ?? other.AssignedTool == null) &&
                   (this.ToolUsage?.EqualsByContent(other.ToolUsage) ?? other.ToolUsage == null) &&
                   (this.TestParameters?.EqualsByContent(other.TestParameters) ?? other.TestParameters == null) &&
                   (this.TestTechnique?.EqualsByContent(other.TestTechnique) ?? other.TestTechnique == null) &&
                   (this.TestLevelSetMfu?.EqualsByContent(other.TestLevelSetMfu) ?? other.TestLevelSetMfu == null) &&
                   (this.TestLevelSetChk?.EqualsByContent(other.TestLevelSetChk) ?? other.TestLevelSetChk == null) &&
                   this.TestLevelNumberMfu == other.TestLevelNumberMfu &&
                   this.TestLevelNumberChk == other.TestLevelNumberChk &&
                   this.StartDateMfu == other.StartDateMfu &&
                   this.StartDateChk == other.StartDateChk &&
                   this.TestOperationActiveMfu == other.TestOperationActiveMfu &&
                   this.TestOperationActiveChk == other.TestOperationActiveChk;
        }

        public void UpdateWith(LocationToolAssignment other)
        {
            if (other == null)
            {
                return;
            }

            this.Id = other.Id;
            this.AssignedLocation = other.AssignedLocation;
            this.AssignedTool = other.AssignedTool;
            this.ToolUsage = other.ToolUsage;
            this.TestParameters = other.TestParameters;
            this.TestTechnique = other.TestTechnique;
            this.TestLevelSetMfu = other.TestLevelSetMfu;
            this.TestLevelNumberMfu = other.TestLevelNumberMfu;
            this.TestLevelSetChk = other.TestLevelSetChk;
            this.TestLevelNumberChk = other.TestLevelNumberChk;
            this.NextTestDateMfu = other.NextTestDateMfu;
            this.NextTestShiftMfu = other.NextTestShiftMfu;
            this.NextTestDateChk = other.NextTestDateChk;
            this.NextTestShiftChk = other.NextTestShiftChk;
            this.StartDateMfu = other.StartDateMfu;
            this.StartDateChk = other.StartDateChk;
            this.TestOperationActiveMfu = other.TestOperationActiveMfu;
            this.TestOperationActiveChk = other.TestOperationActiveChk;
        }

        public LocationToolAssignment CopyDeep()
        {
            return new LocationToolAssignment()
            {
                Id = this.Id != null ? new LocationToolAssignmentId(this.Id.ToLong()) : null,
                AssignedLocation = this.AssignedLocation?.CopyDeep(),
                AssignedTool = this.AssignedTool?.CopyDeep(),
                TestParameters = this.TestParameters?.CopyDeep(),
                TestTechnique = this.TestTechnique?.CopyDeep(),
                ToolUsage = this.ToolUsage?.CopyDeep(),
                TestLevelSetMfu = this.TestLevelSetMfu?.CopyDeep(),
                TestLevelNumberMfu = this.TestLevelNumberMfu,
                TestLevelSetChk = this.TestLevelSetChk?.CopyDeep(),
                TestLevelNumberChk = this.TestLevelNumberChk,
                NextTestDateMfu = this.NextTestDateMfu,
                NextTestShiftMfu = this.NextTestShiftMfu,
                NextTestDateChk = this.NextTestDateChk,
                NextTestShiftChk = this.NextTestShiftChk,
                StartDateMfu = this.StartDateMfu,
                StartDateChk = this.StartDateChk,
                TestOperationActiveMfu = this.TestOperationActiveMfu,
                TestOperationActiveChk = this.TestOperationActiveChk
            };
        }

        public TestLevel GetTestLevel(TestType type)
        {
            if(type == TestType.Chk)
            {
                if(TestLevelNumberChk == 2 && TestLevelSetChk.TestLevel2.IsActive)
                {
                    return TestLevelSetChk.TestLevel2;
                }
                else if (TestLevelNumberChk == 3 && TestLevelSetChk.TestLevel3.IsActive)
                {
                    return TestLevelSetChk.TestLevel3;
                }
                else
                {
                    return TestLevelSetChk.TestLevel1;
                }
            }

            if(type == TestType.Mfu)
            {
                if (TestLevelNumberMfu == 2 && TestLevelSetMfu.TestLevel2.IsActive)
                {
                    return TestLevelSetMfu.TestLevel2;
                }
                else if (TestLevelNumberMfu == 3 && TestLevelSetMfu.TestLevel3.IsActive)
                {
                    return TestLevelSetMfu.TestLevel3;
                }
                else
                {
                    return TestLevelSetMfu.TestLevel1;
                }
            }

            return null;
        }
    }
}
