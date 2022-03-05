using Core;

namespace Server.Core.Entities
{
    public class TestTechnique : IEqualsByContent<TestTechnique>, ICopy<TestTechnique>
    {
        public double EndCycleTime { get; set; }
        public double FilterFrequency { get; set; }
        public double CycleComplete { get; set; }
        public double MeasureDelayTime { get; set; }
        public double ResetTime { get; set; }
        public bool MustTorqueAndAngleBeInLimits { get; set; }
        public double CycleStart { get; set; }
        public double StartFinalAngle { get; set; }
        public double SlipTorque { get; set; }
        public double TorqueCoefficient { get; set; }
        public int MinimumPulse { get; set; }
        public int MaximumPulse { get; set; }
        public int Threshold { get; set; }

        public bool EqualsByContent(TestTechnique other)
        {
            if (other == null)
            {
                return false;
            }

            return this.EndCycleTime == other.EndCycleTime &&
                   this.FilterFrequency == other.FilterFrequency &&
                   this.CycleComplete == other.CycleComplete &&
                   this.MeasureDelayTime == other.MeasureDelayTime &&
                   this.ResetTime == other.ResetTime &&
                   this.MustTorqueAndAngleBeInLimits == other.MustTorqueAndAngleBeInLimits &&
                   this.CycleStart == other.CycleStart &&
                   this.StartFinalAngle == other.StartFinalAngle &&
                   this.SlipTorque == other.SlipTorque &&
                   this.TorqueCoefficient == other.TorqueCoefficient &&
                   this.MinimumPulse == other.MinimumPulse &&
                   this.MaximumPulse == other.MaximumPulse &&
                    Threshold == other.Threshold;
        }

        public void UpdateWith(TestTechnique other)
        {
            if (other == null)
            {
                return;
            }

            this.EndCycleTime = other.EndCycleTime;
            this.FilterFrequency = other.FilterFrequency;
            this.CycleComplete = other.CycleComplete;
            this.MeasureDelayTime = other.MeasureDelayTime;
            this.ResetTime = other.ResetTime;
            this.MustTorqueAndAngleBeInLimits = other.MustTorqueAndAngleBeInLimits;
            this.CycleStart = other.CycleStart;
            this.StartFinalAngle = other.StartFinalAngle;
            this.SlipTorque = other.SlipTorque;
            this.TorqueCoefficient = other.TorqueCoefficient;
            this.MinimumPulse = other.MinimumPulse;
            this.MaximumPulse = other.MaximumPulse;
            Threshold = other.Threshold;
        }

        public TestTechnique CopyDeep()
        {
            return new TestTechnique()
            {
                EndCycleTime = this.EndCycleTime,
                FilterFrequency = this.FilterFrequency,
                CycleComplete = this.CycleComplete,
                MeasureDelayTime = this.MeasureDelayTime,
                ResetTime = this.ResetTime,
                MustTorqueAndAngleBeInLimits = this.MustTorqueAndAngleBeInLimits,
                CycleStart = this.CycleStart,
                StartFinalAngle = this.StartFinalAngle,
                SlipTorque = this.SlipTorque,
                TorqueCoefficient = this.TorqueCoefficient,
                MinimumPulse = this.MinimumPulse,
                MaximumPulse = this.MaximumPulse,
                Threshold = this.Threshold
            };
        }
    }
}
