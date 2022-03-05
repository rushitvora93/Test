using System;

namespace Core.Entities
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


        public bool EqualsByContent(TestLevelSet other)
        {
            if (other == null)
            {
                return false;
            }

            return Id.Equals(other.Id) &&
                   (Name?.Equals(other.Name) ?? other.Name == null) &&
                   (TestLevel1?.EqualsByContent(other.TestLevel1) ?? other.TestLevel1 == null) &&
                   (TestLevel2?.EqualsByContent(other.TestLevel2) ?? other.TestLevel2 == null) &&
                   (TestLevel3?.EqualsByContent(other.TestLevel3) ?? other.TestLevel3 == null);
        }

        public bool EqualsById(TestLevelSet other)
        {
            return Id.Equals(other?.Id);
        }

        public void UpdateWith(TestLevelSet other)
        {
            if (other == null)
            {
                return;
            }

            this.Id = new TestLevelSetId(other.Id.ToLong());
            this.Name = new TestLevelSetName(other.Name.ToDefaultString());
            this.TestLevel1 = other.TestLevel1.CopyDeep();
            this.TestLevel2 = other.TestLevel2.CopyDeep();
            this.TestLevel3 = other.TestLevel3.CopyDeep();
        }

        public TestLevelSet CopyDeep()
        {
            return new TestLevelSet()
            {
                Id = this.Id != null ? new TestLevelSetId(this.Id.ToLong()) : null,
                Name = Name != null ? new TestLevelSetName(Name?.ToDefaultString()) : null,
                TestLevel1 = TestLevel1?.CopyDeep(),
                TestLevel2 = TestLevel2?.CopyDeep(),
                TestLevel3 = TestLevel3?.CopyDeep()
            };
        }
    }
}
