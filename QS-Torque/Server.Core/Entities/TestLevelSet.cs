using System;
using Core;
using Core.Entities;

namespace Server.Core.Entities
{
    public class TestLevelSetId : QstIdentifier, IEquatable<TestLevelSetId>
    {
        public TestLevelSetId(long value)
            : base(value)
        {
        }

        public bool Equals(TestLevelSetId other)
        {
            return Equals((QstIdentifier)other);
        }
    }

    public class TestLevelSetName : TypeCheckedString<MaxLength<CtInt200>, Blacklist<NewLines>, NoCheck>
    {
        public TestLevelSetName(string name)
            : base(name)
        {
        }
    }

    public class TestLevelSet : IQstEquality<TestLevelSet>, IUpdate<TestLevelSet>, ICopy<TestLevelSet>
    {
        public TestLevelSetId Id { get; set; }
        public TestLevelSetName Name { get; set; }
        public TestLevel TestLevel1 { get; set; }
        public TestLevel TestLevel2 { get; set; }
        public TestLevel TestLevel3 { get; set; }


        public bool EqualsById(TestLevelSet other)
        {
            return this.Id.Equals(other?.Id);
        }

        public bool EqualsByContent(TestLevelSet other)
        {
            if (other == null)
            {
                return false;
            }

            return this.Id.Equals(other.Id) &&
                   (this.Name?.Equals(other.Name) ?? other.Name == null) &&
                   (this.TestLevel1?.EqualsByContent(other.TestLevel1) ?? other.TestLevel1 == null) &&
                   (this.TestLevel2?.EqualsByContent(other.TestLevel2) ?? other.TestLevel2 == null) &&
                   (this.TestLevel3?.EqualsByContent(other.TestLevel3) ?? other.TestLevel3 == null);
        }

        public void UpdateWith(TestLevelSet other)
        {
            if (other == null)
            {
                return;
            }

            this.Id = other.Id;
            this.Name = other.Name;
            this.TestLevel1 = other.TestLevel1;
            this.TestLevel2 = other.TestLevel2;
            this.TestLevel3 = other.TestLevel3;
        }

        public TestLevelSet CopyDeep()
        {
            return new TestLevelSet()
            {
                Id = this.Id != null ? new TestLevelSetId(this.Id.ToLong()) : null,
                Name = this.Name != null ? new TestLevelSetName(this.Name.ToDefaultString()) : null,
                TestLevel1 = this.TestLevel1 != null ? this.TestLevel1.CopyDeep() : null,
                TestLevel2 = this.TestLevel2 != null ? this.TestLevel2.CopyDeep() : null,
                TestLevel3 = this.TestLevel3 != null ? this.TestLevel3.CopyDeep() : null
            };
        }
    }
}
