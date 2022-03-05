using System.ComponentModel;
using Core;
using Core.Entities;
using InterfaceAdapters.Localization;

namespace InterfaceAdapters.Models
{
    public class TestTechniqueModel : BindableBase, IEqualsByContent<TestTechniqueModel>, IUpdate<TestTechniqueModel>, IDataErrorInfo
    {
        private ILocalizationWrapper _localization;
        public TestTechnique Entity { get; private set; }

        public double EndCycleTime
        {
            get => Entity.EndCycleTime;
            set
            {
                Entity.EndCycleTime = value;
                RaisePropertyChanged();
            }
        }

        public double FilterFrequency
        {
            get => Entity.FilterFrequency;
            set
            {
                Entity.FilterFrequency = value;
                RaisePropertyChanged();
            }
        }

        public double CycleComplete
        {
            get => Entity.CycleComplete;
            set
            {
                Entity.CycleComplete = value;
                RaisePropertyChanged();
            }
        }

        public double MeasureDelayTime
        {
            get => Entity.MeasureDelayTime;
            set
            {
                Entity.MeasureDelayTime = value;
                RaisePropertyChanged();
            }
        }

        public double ResetTime
        {
            get => Entity.ResetTime;
            set
            {
                Entity.ResetTime = value;
                RaisePropertyChanged();
            }
        }

        public bool MustTorqueAndAngleBeInLimits
        {
            get => Entity.MustTorqueAndAngleBeInLimits;
            set
            {
                Entity.MustTorqueAndAngleBeInLimits = value;
                RaisePropertyChanged();
            }
        }

        public double CycleStart
        {
            get => Entity.CycleStart;
            set
            {
                Entity.CycleStart = value;
                RaisePropertyChanged();
            }
        }

        public double StartFinalAngle
        {
            get => Entity.StartFinalAngle;
            set
            {
                Entity.StartFinalAngle = value;
                RaisePropertyChanged();
            }
        }

        public double SlipTorque
        {
            get => Entity.SlipTorque;
            set
            {
                Entity.SlipTorque = value;
                RaisePropertyChanged();
            }
        }

        public double TorqueCoefficient
        {
            get => Entity.TorqueCoefficient;
            set
            {
                Entity.TorqueCoefficient = value;
                RaisePropertyChanged();
            }
        }

        public int MinimumPulse
        {
            get => Entity.MinimumPulse;
            set
            {
                Entity.MinimumPulse = value;
                RaisePropertyChanged();
            }
        }

        public int MaximumPulse
        {
            get => Entity.MaximumPulse;
            set
            {
                Entity.MaximumPulse = value;
                RaisePropertyChanged();
            }
        }

        public int Threshold
        {
            get => Entity.Threshold;
            set
            {
                Entity.Threshold = value;
                RaisePropertyChanged();
            }
        }

        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                var errors = Entity.Validate(columnName);
                string result = null;
                if (errors == null || errors.Count <= 0)
                {
                    return result;
                }
                foreach (var error in errors)
                {
                    switch (error)
                    {
                        case TestTechniqueError.EndCycleTimeHasToBeGreaterThanZeroPointOne:
                            result = _localization.Strings.GetParticularString("TestTechniqueError",
                                "The end cycle time has to be greater than zero point one");
                            break;
                        case TestTechniqueError.EndCycleTimeHasToBeLessThanFive:
                            result = _localization.Strings.GetParticularString("TestTechniqueError",
                                "The end cycle time has to be less than five");
                            break;
                        case TestTechniqueError.FilterFrequencyHasToBeGreaterThanOneHundred:
                            result = _localization.Strings.GetParticularString("TestTechniqueError",
                                "Filter frequency has to be greater than one hundred");
                            break;
                        case TestTechniqueError.FilterFrequencyHasToBeLessThanOneThousend:
                            result = _localization.Strings.GetParticularString("TestTechniqueError",
                                "FIlter frequency has to be less than one thousend");
                            break;
                        case TestTechniqueError.SlipTorqueHasToBeGreaterOrEqualToZero:
                            result = _localization.Strings.GetParticularString("TestTechniqueError",
                                "Slip torque has to be greater or equal than zero");
                            break;
                        case TestTechniqueError.SlipTorqueHasToBeLessThanTenThousend:
                            result = _localization.Strings.GetParticularString("TestTechniqueError",
                                "Slip torque has to be less than ten thousend");
                            break;
                        case TestTechniqueError.TorqueCoefficientHasToBeGreaterThanZeroPointOne:
                            result = _localization.Strings.GetParticularString("TestTechniqueError",
                                "Torque coefficient has to be greater than zero point one");
                            break;
                        case TestTechniqueError.TorqueCoefficientHasToBeLessThanTen:
                            result = _localization.Strings.GetParticularString("TestTechniqueError",
                                "Torque coefficient has to be less than ten");
                            break;
                        case TestTechniqueError.MinimumPulseHasToBeGreaterThanZero:
                            result = _localization.Strings.GetParticularString("TestTechniqueError",
                                "Minimum pulse has to be greater than zero");
                            break;
                        case TestTechniqueError.MinimumPulseHasToBeLessThanTwoHundredAndFiftyFive:
                            result = _localization.Strings.GetParticularString("TestTechniqueError",
                                "Minimum pulse has to be less than two hunderd and fifty five");
                            break;
                        case TestTechniqueError.MaximumPulseHasToBeGreaterThanZero:
                            result = _localization.Strings.GetParticularString("TestTechniqueError",
                                "Maximum pulse has to be greater than zero");
                            break;
                        case TestTechniqueError.MaximumPulseHasToBeLessThanTwoHundredAndFiftyFive:
                            result = _localization.Strings.GetParticularString("TestTechniqueError",
                                "Maximum pulse has to be less than two hundred fifty five");
                            break;
                        case TestTechniqueError.ThresholdHasToBeGreaterThanZero:
                            result = _localization.Strings.GetParticularString("TestTechniqueError",
                                "Threshold has to be greater than zero");
                            break;
                        case TestTechniqueError.ThresholdHasToBeLessThanOneHundred:
                            result = _localization.Strings.GetParticularString("TestTechniqueError",
                                "Threshold has to be less than one hundred");
                            break;
                    }
                }

                return result;
            }
        }

        public void UpdateWith(TestTechniqueModel other)
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
            this.Threshold = other.Threshold;
        }

        public TestTechniqueModel(TestTechnique entity, ILocalizationWrapper localization)
        {
            Entity = entity;
            _localization = localization;
        }

        public static TestTechniqueModel GetModelFor(TestTechnique entity, ILocalizationWrapper localization)
        {
            return entity != null ? new TestTechniqueModel(entity, localization) : null;
        }

        public bool EqualsByContent(TestTechniqueModel other)
        {
            return this.Entity.EqualsByContent(other?.Entity);
        }
    }
}
