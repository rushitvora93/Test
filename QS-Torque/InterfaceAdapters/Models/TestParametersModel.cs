using System.ComponentModel;
using Core;
using Core.Entities;
using Core.Enums;
using Core.PhysicalValueTypes;
using InterfaceAdapters.Localization;

namespace InterfaceAdapters.Models
{
    public class TestParametersModel : BindableBase, IEqualsByContent<TestParametersModel>, IUpdate<TestParametersModel>, IDataErrorInfo
    {
        private ILocalizationWrapper _localization;

        public TestParameters Entity { get; private set; }

        public double SetPointTorque
        {
            get => Entity.SetPointTorque.Nm;
            set
            {
                Entity.SetPointTorque = Torque.FromNm(value);
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(TestParametersModel.MinimumTorque));
                RaisePropertyChanged(nameof(TestParametersModel.MaximumTorque));
            }
        }

        public ToleranceClassModel ToleranceClassTorque
        {
            get => ToleranceClassModel.GetModelFor(Entity.ToleranceClassTorque);
            set
            {
                Entity.ToleranceClassTorque = value?.Entity;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(TestParametersModel.MinimumTorque));
                RaisePropertyChanged(nameof(TestParametersModel.MaximumTorque));
            }
        }

        public double MinimumTorque
        {
            get => Entity.MinimumTorque.Nm;
            set
            {
                Entity.MinimumTorque = Torque.FromNm(value);
                RaisePropertyChanged();
            }
        }

        public double MaximumTorque
        {
            get => Entity.MaximumTorque.Nm;
            set
            {
                Entity.MaximumTorque = Torque.FromNm(value);
                RaisePropertyChanged();
            }
        }

        public double ThresholdTorque
        {
            get => Entity.ThresholdTorque.Degree;
            set
            {
                Entity.ThresholdTorque = Angle.FromDegree(value);
                RaisePropertyChanged();
            }
        }

        public double SetPointAngle
        {
            get => Entity.SetPointAngle.Degree;
            set
            {
                Entity.SetPointAngle = Angle.FromDegree(value);
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(TestParametersModel.MinimumAngle));
                RaisePropertyChanged(nameof(TestParametersModel.MaximumAngle));
            }
        }


        public ToleranceClassModel ToleranceClassAngle
        {
            get => ToleranceClassModel.GetModelFor(Entity.ToleranceClassAngle);
            set
            {
                Entity.ToleranceClassAngle = value?.Entity;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(TestParametersModel.MinimumAngle));
                RaisePropertyChanged(nameof(TestParametersModel.MaximumAngle));
            }
        }

        public double MinimumAngle
        {
            get => Entity.MinimumAngle.Degree;
            set
            {
                Entity.MinimumAngle = Angle.FromDegree(value);
                RaisePropertyChanged();
            }
        }

        public double MaximumAngle
        {
            get => Entity.MaximumAngle.Degree;
            set
            {
                Entity.MaximumAngle = Angle.FromDegree(value);
                RaisePropertyChanged();
            }
        }

        public LocationControlledBy ControlledBy
        {
            get => Entity.ControlledBy;
            set
            {
                Entity.ControlledBy = value;
                RaisePropertyChanged();
            }
        }

        public TestParametersModel(TestParameters entity, ILocalizationWrapper localization)
        {
            Entity = entity;
            _localization = localization;
        }

        public static TestParametersModel GetModelFor(TestParameters entity, ILocalizationWrapper localization)
        {
            return entity != null ? new TestParametersModel(entity, localization) : null;
        }

        public bool EqualsByContent(TestParametersModel other)
        {
            return this.Entity.EqualsByContent(other?.Entity);
        }

        public void UpdateWith(TestParametersModel other)
        {
            if (other == null)
            {
                return;
            }

            SetPointTorque = other.SetPointTorque;
            ToleranceClassTorque = other.ToleranceClassTorque;
            MinimumTorque = other.MinimumTorque;
            MaximumTorque = other.MaximumTorque;
            ThresholdTorque = other.ThresholdTorque;
            SetPointAngle = other.SetPointAngle;
            ToleranceClassAngle = other.ToleranceClassAngle;
            MinimumAngle = other.MinimumAngle;
            MaximumAngle = other.MaximumAngle;
            ThresholdTorque = other.ThresholdTorque;
            if (other.ControlledBy != ControlledBy)
            {
                ControlledBy = other.ControlledBy;
            }
        }

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
                foreach (var testParameterError in errors)
                {
                    switch (testParameterError)
                    {
                        case TestParameterError.MinimumAngleGreaterThenSetPointAngle:
                            result += _localization.Strings.GetParticularString("TestParameterError", 
                                "The minimum angle has to be less than or equal to the setpoint angle") + "\n";
                            break;
                        case TestParameterError.MaximumAngleLessThenSetPointAngle:
                            result += _localization.Strings.GetParticularString("TestParameterError",
                                "The maximum angle has to be greater than or equal to the setpoint angle") + "\n";
                            break;
                        case TestParameterError.SetPointAngleLessThenZero:
                            result += _localization.Strings.GetParticularString("TestParameterError",
                                "The set point angle has to be greater than zero") + "\n";
                            break;
                        case TestParameterError.MinimumTorqueGreaterThenSetPointTorque:
                            result += _localization.Strings.GetParticularString("TestParameterError", 
                                "The minimum torque has to be less than or equal to the setpoint torque") + "\n";
                            break;
                        case TestParameterError.MaximumTorqueLessThenSetPointTorque:
                            result += _localization.Strings.GetParticularString("TestParameterError",
                                "The maximum torque has to be greater than or equal to the setpoint torque") + "\n";
                            break;
                        case TestParameterError.SetPointTorqueLessThenZero:
                            result += _localization.Strings.GetParticularString("TestParameterError",
                                "The set point torque has to be greater than zero") + "\n";
                            break;
                        case TestParameterError.ThresholdTorqueLessOrEqualThanZero:
                            result += _localization.Strings.GetParticularString("TestParameterError",
                                "The threshold torque has to be greater than zero") + "\n";
                            break;
                    }
                }

                return result;
            }
        }

        public string Error { get; }
    }
}
