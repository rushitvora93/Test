using Core;
using Server.Core.Enums;

namespace Server.Core.Entities
{
    public class TestParameters : IEqualsByContent<TestParameters>, ICopy<TestParameters>
    {
        public double SetPoint1 { get; set; }
        public ToleranceClass ToleranceClass1 { get; set; }
        public double Minimum1 { get; set; }
        public double Maximum1 { get; set; }
        public double Threshold1 { get; set; }
        public double SetPoint2 { get; set; }
        public ToleranceClass ToleranceClass2 { get; set; }
        public double Minimum2 { get; set; }
        public double Maximum2 { get; set; }
        public LocationControlledBy ControlledBy { get; set; }
        public bool Alive { get; set; }

        public bool EqualsByContent(TestParameters other)
        {
            if (other == null)
            {
                return false;
            }

            return
                SetPoint1.Equals(other.SetPoint1) &&
                (ToleranceClass1?.EqualsByContent(other.ToleranceClass1) ?? other.ToleranceClass1 == null) &&
                Minimum1.Equals(other.Minimum1) &&
                Maximum1.Equals(other.Maximum1) &&
                Threshold1.Equals(other.Threshold1) &&
                SetPoint2.Equals(other.SetPoint2) &&
                (ToleranceClass2?.EqualsByContent(other.ToleranceClass2) ?? other.ToleranceClass2 == null) &&
                Minimum2.Equals(other.Minimum2) &&
                Maximum2.Equals(other.Maximum2) &&
                ControlledBy.Equals(other.ControlledBy) &&
                Alive.Equals(other.Alive);
        }

        public void UpdateWith(TestParameters other)
        {
            if (other == null)
            {
                return;
            }

            SetPoint1 = other.SetPoint1;
            ToleranceClass1 = other.ToleranceClass1;
            Minimum1 = other.Minimum1;
            Maximum1 = other.Maximum1;
            Threshold1 = other.Threshold1;
            SetPoint2 = other.SetPoint2;
            ToleranceClass2 = other.ToleranceClass2;
            Minimum2 = other.Minimum2;
            Maximum2 = other.Maximum2;
            ControlledBy = other.ControlledBy;
            Alive = other.Alive;
        }

        public TestParameters CopyDeep()
        {
            return new TestParameters()
            {
                SetPoint1 = this.SetPoint1,
                ToleranceClass1 = this.ToleranceClass1?.CopyDeep(),
                Minimum1 = this.Minimum1,
                Maximum1 = this.Maximum1,
                Threshold1 = this.Threshold1,
                SetPoint2 = this.SetPoint2,
                ToleranceClass2 = this.ToleranceClass2?.CopyDeep(),
                Minimum2 = this.Minimum2,
                Maximum2 = this.Maximum2,
                ControlledBy = this.ControlledBy,
                Alive = this.Alive
            };
        }
    }
}
