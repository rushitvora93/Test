using System.Collections.Generic;
using Core.Enums;
using Core.PhysicalValueTypes;

namespace Core.Entities
{
    public enum TestParameterError
    {
        MinimumAngleGreaterThenSetPointAngle,
        MaximumAngleLessThenSetPointAngle,
        SetPointAngleLessThenZero,
        MinimumTorqueGreaterThenSetPointTorque,
        MaximumTorqueLessThenSetPointTorque,
        SetPointTorqueLessThenZero,
        ThresholdTorqueLessOrEqualThanZero
    }

    public class TestParameters : IEqualsByContent<TestParameters>, ICopy<TestParameters>
    {
        private Torque _setPointTorque = Torque.FromNm(0);

        public Torque SetPointTorque
        {
            get => _setPointTorque;
            set
            {
                if (value != null)
                {
                    _setPointTorque = value;
                    UpdateToleranceLimits(); 
                }
            }
        }

        private ToleranceClass _toleranceClassTorque;
        public ToleranceClass ToleranceClassTorque
        {
            get => _toleranceClassTorque;
            set
            {
                _toleranceClassTorque = value;
                UpdateToleranceLimits();
            }
        }

        private Torque _minimumTorque = Torque.FromNm(0);
        public Torque MinimumTorque
        {
            get => _minimumTorque;
            set
            {
                if (value != null)
                {
                    _minimumTorque = value; 
                }
            }
        }

        private Torque _maximumTorque = Torque.FromNm(0);
        public Torque MaximumTorque
        {
            get => _maximumTorque;
            set
            {
                if (value != null)
                {
                    _maximumTorque = value; 
                }
            }
        }

        private Angle _thresholdTorque = Angle.FromDegree(0);

        public Angle ThresholdTorque
        {
            get => _thresholdTorque;
            set
            {
                if (value != null)
                {
                    _thresholdTorque = value;
                }
            }
        }

        private Angle _setpointAngle = Angle.FromDegree(0);

        public Angle SetPointAngle
        {
            get => _setpointAngle;
            set
            {
                if (value != null)
                {
                    _setpointAngle = value;
                    UpdateToleranceLimits();
                }
            }
        }

        private ToleranceClass _toleranceClassAngle;

        public ToleranceClass ToleranceClassAngle
        {
            get => _toleranceClassAngle;
            set
            {
                _toleranceClassAngle = value;
                UpdateToleranceLimits();
            }
        }

        private Angle _minimumAngle = Angle.FromDegree(0);

        public Angle MinimumAngle
        {
            get => _minimumAngle;
            set
            {
                if (value != null)
                {
                    _minimumAngle = value;
                }
            }
        }

        private Angle _maximumAngle = Angle.FromDegree(0);
        public Angle MaximumAngle
        {
            get => _maximumAngle;
            set
            {
                if (value != null)
                {
                    _maximumAngle = value;
                }
            }
        }

        public LocationControlledBy ControlledBy { get; set; }

        public virtual void UpdateToleranceLimits()
        {
            if (ToleranceClassHasLowerAndUpperLimit(ToleranceClassTorque))
            {
                MinimumTorque = Torque.FromNm(ToleranceClassTorque.GetLowerLimitForValue(SetPointTorque.Nm));
                MaximumTorque = Torque.FromNm(ToleranceClassTorque.GetUpperLimitForValue(SetPointTorque.Nm));
            }

            if (ToleranceClassHasLowerAndUpperLimit(ToleranceClassAngle))
            {
                MinimumAngle = Angle.FromDegree(ToleranceClassAngle.GetLowerLimitForValue(SetPointAngle.Degree));
                MaximumAngle = Angle.FromDegree(ToleranceClassAngle.GetUpperLimitForValue(SetPointAngle.Degree));
            }
        }

        private static bool ToleranceClassHasLowerAndUpperLimit(ToleranceClass toleranceClass)
        {
            return toleranceClass != null && (toleranceClass.LowerLimit != 0 || toleranceClass.UpperLimit != 0);
        }


        public bool EqualsByContent(TestParameters other)
        {
            if (other == null)
            {
                return false;
            }

            return
                SetPointTorque.Equals(other.SetPointTorque) &&
                (ToleranceClassTorque?.EqualsByContent(other.ToleranceClassTorque) ?? other.ToleranceClassTorque == null) &&
                MinimumTorque.Equals(other.MinimumTorque) &&
                MaximumTorque.Equals(other.MaximumTorque) &&
                ThresholdTorque.Equals(other.ThresholdTorque) &&
                SetPointAngle.Equals(other.SetPointAngle) &&
                (ToleranceClassAngle?.EqualsByContent(other.ToleranceClassAngle) ?? other.ToleranceClassAngle == null) &&
                MinimumAngle.Equals(other.MinimumAngle) &&
                MaximumAngle.Equals(other.MaximumAngle) &&
                ControlledBy.Equals(other.ControlledBy);
        }

        public void UpdateWith(TestParameters other)
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
            ControlledBy = other.ControlledBy;
        }

        public TestParameters CopyDeep()
        {
            return new TestParameters()
            {
                SetPointTorque = this.SetPointTorque,
                ToleranceClassTorque = this.ToleranceClassTorque?.CopyDeep(),
                MinimumTorque = this.MinimumTorque,
                MaximumTorque = this.MaximumTorque,
                ThresholdTorque = this.ThresholdTorque,
                SetPointAngle = this.SetPointAngle,
                ToleranceClassAngle = this.ToleranceClassAngle?.CopyDeep(),
                MinimumAngle = this.MinimumAngle,
                MaximumAngle = this.MaximumAngle,
                ControlledBy = this.ControlledBy
            };
        }

        public List<TestParameterError> Validate(string property)
        {
            List<TestParameterError> testParameterErrors = new List<TestParameterError>();
            switch (property)
            {
                case nameof(MinimumAngle):
                    if (MinimumAngle?.Degree > SetPointAngle?.Degree)
                    {
                        testParameterErrors.Add(TestParameterError.MinimumAngleGreaterThenSetPointAngle);
                    }
                    return testParameterErrors;
                case nameof(MaximumAngle):
                    if (MaximumAngle?.Degree < SetPointAngle?.Degree)
                    {
                        testParameterErrors.Add(TestParameterError.MaximumAngleLessThenSetPointAngle);
                    }
                    return testParameterErrors;
                case nameof(SetPointAngle):
                    if (SetPointAngle?.Degree < 0)
                    {
                        testParameterErrors.Add(TestParameterError.SetPointAngleLessThenZero);
                    }

                    return testParameterErrors;
                case nameof(MinimumTorque):
                    if (MinimumTorque?.Nm > SetPointTorque?.Nm)
                    {
                        testParameterErrors.Add(TestParameterError.MinimumTorqueGreaterThenSetPointTorque);
                    }

                    return testParameterErrors;
                case nameof(MaximumTorque):
                    if (MaximumTorque?.Nm < SetPointTorque?.Nm)
                    {
                        testParameterErrors.Add(TestParameterError.MaximumTorqueLessThenSetPointTorque);
                    }
                    return testParameterErrors;
                case nameof(SetPointTorque):
                    if (SetPointTorque?.Nm< 0)
                    {
                        testParameterErrors.Add(TestParameterError.SetPointTorqueLessThenZero);
                    }
                    return testParameterErrors;

                case nameof(ThresholdTorque):
                    if (ControlledBy == LocationControlledBy.Angle && ThresholdTorque?.Degree <= 0)
                    {
                        testParameterErrors.Add(TestParameterError.ThresholdTorqueLessOrEqualThanZero);
                    }
                    return testParameterErrors;

            }

            return testParameterErrors;
        }
    }
}