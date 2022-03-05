using System.Collections.Generic;

namespace Core.Entities
{
    public enum TestTechniqueError
    {
        EndCycleTimeHasToBeGreaterThanZeroPointOne,
        EndCycleTimeHasToBeLessThanFive,
        FilterFrequencyHasToBeGreaterThanOneHundred,
        FilterFrequencyHasToBeLessThanOneThousend,
        SlipTorqueHasToBeGreaterOrEqualToZero,
        SlipTorqueHasToBeLessThanTenThousend,
        TorqueCoefficientHasToBeGreaterThanZeroPointOne,
        TorqueCoefficientHasToBeLessThanTen,
        MinimumPulseHasToBeGreaterThanZero,
        MinimumPulseHasToBeLessThanTwoHundredAndFiftyFive,
        MaximumPulseHasToBeGreaterThanZero,
        MaximumPulseHasToBeLessThanTwoHundredAndFiftyFive,
        ThresholdHasToBeGreaterThanZero,
        ThresholdHasToBeLessThanOneHundred
    }
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

        public List<TestTechniqueError> Validate(string propertyName)
        {
            List<TestTechniqueError> errors = new List<TestTechniqueError>();
            switch (propertyName)
            {
                case nameof(EndCycleTime):
                    if (EndCycleTime < 0.1)
                    {
                        errors.Add(TestTechniqueError.EndCycleTimeHasToBeGreaterThanZeroPointOne);
                    }

                    if (EndCycleTime > 5)
                    {
                        errors.Add(TestTechniqueError.EndCycleTimeHasToBeLessThanFive);
                    }

                    return errors;
                case nameof(FilterFrequency):
                    if (FilterFrequency < 100)
                    {
                        errors.Add(TestTechniqueError.FilterFrequencyHasToBeGreaterThanOneHundred);
                    }

                    if (FilterFrequency > 1000)
                    {
                        errors.Add(TestTechniqueError.FilterFrequencyHasToBeLessThanOneThousend);
                    }

                    return errors;
                case nameof(SlipTorque):
                    if (SlipTorque < 0)
                    {
                        errors.Add(TestTechniqueError.SlipTorqueHasToBeGreaterOrEqualToZero);
                    }

                    if (SlipTorque > 9999.9)
                    {
                        errors.Add(TestTechniqueError.SlipTorqueHasToBeLessThanTenThousend);
                    }

                    return errors;
                case nameof(TorqueCoefficient):
                    if (TorqueCoefficient < 0.1)
                    {
                        errors.Add(TestTechniqueError.TorqueCoefficientHasToBeGreaterThanZeroPointOne);
                    }

                    if (TorqueCoefficient > 10)
                    {
                        errors.Add(TestTechniqueError.TorqueCoefficientHasToBeLessThanTen);
                    }

                    return errors;
                case nameof(MinimumPulse):
                    if (MinimumPulse < 1)
                    {
                        errors.Add(TestTechniqueError.MinimumPulseHasToBeGreaterThanZero);
                    }

                    if (MinimumPulse > 255)
                    {
                        errors.Add(TestTechniqueError.MinimumPulseHasToBeLessThanTwoHundredAndFiftyFive);
                    }

                    return errors;
                case nameof(MaximumPulse):
                    if (MaximumPulse < 1)
                    {
                        errors.Add(TestTechniqueError.MaximumPulseHasToBeGreaterThanZero);
                    }

                    if (MaximumPulse > 255)
                    {
                        errors.Add(TestTechniqueError.MaximumPulseHasToBeLessThanTwoHundredAndFiftyFive);
                    }

                    return errors;
                case nameof(Threshold):
                    if (Threshold < 1)
                    {
                        errors.Add(TestTechniqueError.ThresholdHasToBeGreaterThanZero);
                    }

                    if (Threshold > 99)
                    {
                        errors.Add(TestTechniqueError.ThresholdHasToBeLessThanOneHundred);
                    }

                    return errors;
            }

            return errors;
        }
    }
}
