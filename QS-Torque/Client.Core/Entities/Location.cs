using System;
using System.Collections.Generic;
using System.Linq;
using Core.Enums;
using Core.PhysicalValueTypes;

namespace Core.Entities
{
    public class LocationId : QstIdentifier, IEquatable<LocationId>
    {
        public LocationId(long value)
            : base(value)
        {
        }

        public bool Equals(LocationId other)
        {
            return Equals((QstIdentifier) other);
        }
    }

    public class LocationNumber : TypeCheckedString<MaxLength<CtInt30>, Blacklist<NewLines>, NoCheck>
    {
        public LocationNumber(string number)
            : base(number)
        {
        }
    }

    public class LocationDescription : TypeCheckedString<MaxLength<CtInt50>, Blacklist<NewLines>, NoCheck>
    {
        public LocationDescription(string description)
            : base(description)
        {
        }
    }

    public class LocationConfigurableField1 : TypeCheckedString<MaxLength<CtInt15>, Blacklist<NewLines>, NoCheck>
    {
        public LocationConfigurableField1(string value)
            : base(value)
        {
        }
    }

    public class LocationConfigurableField2 : TypeCheckedString<MaxLength<CtInt1>, Blacklist<NewLines>, NoCheck>
    {
        public LocationConfigurableField2(string value)
            : base(value)
        {
        }
    }

    public enum LocationValidationError
    {
        SetpointTorqueIsLessThanOrEqualToZero,
        SetpointTorqueIsLessThanZero,
        MinimumTorqueHasToBeLessOrEqualThanSetpointTorque,
        MaximumTorqueHasToBeGreaterOrEqualThanSetpointTorque,
        ThresholdTorqueIsLessThanOrEqualToZero,
        ThresholdTorqueIsGreaterThanSetpointTorque,
        SetpointAngleIsLessThanOrEqualToZero,
        SetpointAngleIsLessThanZero,
        MinimumAngleHasToBeLessOrEqualThanSetpointAngle,
        MaximumAngleHasToBeGreaterOrEqualThanSetpointAngle,
        MinimumTorqueHasToBeGreaterThanOrEqualToZero,
        MaximumTorqueHasToBeGreaterThanOrEqualToZero,
        MinimumAngleHasToBeGreaterThanOrEqualToZero,
        MaximumAngleHasToBeGreaterThanOrEqualToZero
    }


    public class Location : IQstEquality<Location>, IUpdate<Location>, ICopy<Location>
    {
        public LocationId Id { get; set; }
        public LocationNumber Number { get; set; }
        public LocationDescription Description { get; set; }
        public LocationDirectoryId ParentDirectoryId { get; set; }
        public LocationControlledBy ControlledBy { get; set; }
        public List<LocationDirectory> LocationDirectoryPath { get; set; }

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

        private Torque _thresholdTorque = Torque.FromNm(0);

        public Torque ThresholdTorque
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

        public LocationConfigurableField1 ConfigurableField1 { get; set; }
        public LocationConfigurableField2 ConfigurableField2 { get; set; }
        public bool ConfigurableField3 { get; set; }
        public string Comment { get; set; }
        public Picture Picture { get; set; }


        public virtual void UpdateToleranceLimits()
        {
            if (ToleranceClassTorque != null)
            {
                if (ToleranceClassTorque.LowerLimit != 0 || ToleranceClassTorque.UpperLimit != 0)
                {
                    MinimumTorque = Torque.FromNm(ToleranceClassTorque.GetLowerLimitForValue(SetPointTorque?.Nm ?? 0));
                    MaximumTorque = Torque.FromNm(ToleranceClassTorque.GetUpperLimitForValue(SetPointTorque?.Nm ?? 0));
                }
            }

            if (ToleranceClassAngle != null)
            {
                if (ToleranceClassAngle.LowerLimit != 0 || ToleranceClassAngle.UpperLimit != 0)
                {
                    MinimumAngle = Angle.FromDegree(ToleranceClassAngle.GetLowerLimitForValue(SetPointAngle.Degree));
                    MaximumAngle = Angle.FromDegree(ToleranceClassAngle.GetUpperLimitForValue(SetPointAngle.Degree));
                }
            }
        }


        public bool EqualsById(Location other)
        {
            return this.Id.Equals(other?.Id);
        }

        public bool EqualsByContent(Location other)
        {
            if (other == null)
            {
                return false;
            }

            var eqByContent = this.Id.Equals(other.Id) &&
                   (this.Number?.Equals(other.Number) ?? other.Number == null) &&
                   (this.Description?.Equals(other.Description) ?? other.Description == null) &&
                   (this.ParentDirectoryId?.Equals(other.ParentDirectoryId) ?? other.ParentDirectoryId == null) &&
                   this.ControlledBy == other.ControlledBy &&
                   this.SetPointTorque.Equals(other.SetPointTorque) &&
                   (this.ToleranceClassTorque?.EqualsByContent(other.ToleranceClassTorque) ??
                    other.ToleranceClassTorque == null) &&
                   this.MinimumTorque.Equals(other.MinimumTorque) &&
                   this.MaximumTorque.Equals(other.MaximumTorque) &&
                   this.ThresholdTorque.Equals(other.ThresholdTorque) &&
                   this.SetPointAngle.Equals(other.SetPointAngle) &&
                   (this.ToleranceClassAngle?.EqualsByContent(other.ToleranceClassAngle) ??
                    other.ToleranceClassAngle == null) &&
                   this.MinimumAngle.Equals(other.MinimumAngle) &&
                   this.MaximumAngle.Equals(other.MaximumAngle) &&
                   (this.ConfigurableField1?.Equals(other.ConfigurableField1) ?? other.ConfigurableField1 == null) &&
                   (this.ConfigurableField2?.Equals(other.ConfigurableField2) ?? other.ConfigurableField2 == null) &&
                   this.ConfigurableField3 == other.ConfigurableField3 &&
                   this.Comment == other.Comment &&
                   (this.Picture?.EqualsByContent(other.Picture) ?? other.Picture == null);

            if (!eqByContent)
            {
                return false;
            }

            if (this.LocationDirectoryPath == null && other.LocationDirectoryPath == null)
            {
                return true;
            }

            if (this.LocationDirectoryPath?.Count != other.LocationDirectoryPath?.Count)
            {
                return false;
            }

            return !this.LocationDirectoryPath.Where((t, i) => !t.EqualsByContent(other.LocationDirectoryPath[i])).Any();
        }

        public void UpdateWith(Location other)
        {
            if (other == null)
            {
                return;
            }

            this.Id = other.Id;
            this.Number = other.Number;
            this.Description = other.Description;
            this.ParentDirectoryId = other.ParentDirectoryId;
            this.ControlledBy = other.ControlledBy;
            this.SetPointTorque = other.SetPointTorque;
            this.ToleranceClassTorque = other.ToleranceClassTorque;
            this.MinimumTorque = other.MinimumTorque;
            this.MaximumTorque = other.MaximumTorque;
            this.ThresholdTorque = other.ThresholdTorque;
            this.SetPointAngle = other.SetPointAngle;
            this.ToleranceClassAngle = other.ToleranceClassAngle;
            this.MinimumAngle = other.MinimumAngle;
            this.MaximumAngle = other.MaximumAngle;
            this.ConfigurableField1 = other.ConfigurableField1;
            this.ConfigurableField2 = other.ConfigurableField2;
            this.ConfigurableField3 = other.ConfigurableField3;
            this.Comment = other.Comment;
            this.Picture = other.Picture;
            this.LocationDirectoryPath = other.LocationDirectoryPath;
        }

        public Location CopyDeep()
        {
            return new Location()
            {
                Id = this.Id != null ? new LocationId(this.Id.ToLong()) : null,
                Number = this.Number != null ? new LocationNumber(this.Number.ToDefaultString()) : null,
                Description = this.Description != null ? new LocationDescription(this.Description.ToDefaultString()) : null,
                ParentDirectoryId = this.ParentDirectoryId != null ? new LocationDirectoryId(this.ParentDirectoryId.ToLong()) : null,
                ControlledBy = this.ControlledBy,
                SetPointTorque = Torque.FromNm(this.SetPointTorque.Nm),
                ToleranceClassTorque = this.ToleranceClassTorque?.CopyDeep(),
                MinimumTorque = Torque.FromNm(this.MinimumTorque.Nm),
                MaximumTorque = Torque.FromNm(this.MaximumTorque.Nm),
                ThresholdTorque = Torque.FromNm(this.ThresholdTorque.Nm),
                SetPointAngle = Angle.FromDegree(this.SetPointAngle.Degree),
                ToleranceClassAngle = this.ToleranceClassAngle?.CopyDeep(),
                MinimumAngle = Angle.FromDegree(this.MinimumAngle.Degree),
                MaximumAngle = Angle.FromDegree(this.MaximumAngle.Degree),
                ConfigurableField1 = this.ConfigurableField1 != null ? new LocationConfigurableField1(this.ConfigurableField1.ToDefaultString()) : null,
                ConfigurableField2 = this.ConfigurableField2 != null ? new LocationConfigurableField2(this.ConfigurableField2.ToDefaultString()) : null,
                ConfigurableField3 = this.ConfigurableField3,
                Comment = this.Comment,
                Picture = this.Picture?.CopyDeep(),
                LocationDirectoryPath = this.LocationDirectoryPath
            };
        }

        public IEnumerable<LocationValidationError> Validate(string property)
        {
            switch (property)
            {
                case nameof(SetPointTorque):
                    if(ControlledBy == LocationControlledBy.Torque && SetPointTorque.Nm <= 0)
                    {
                        yield return LocationValidationError.SetpointTorqueIsLessThanOrEqualToZero;
                    }
                    if(ControlledBy == LocationControlledBy.Angle && SetPointTorque.Nm < 0)
                    {
                        yield return LocationValidationError.SetpointTorqueIsLessThanZero;
                    }
                    break;
                case nameof(Location.MinimumTorque):
                    if(MinimumTorque.Nm > SetPointTorque.Nm && SetPointTorque.Nm >= 0)
                    {
                        yield return LocationValidationError.MinimumTorqueHasToBeLessOrEqualThanSetpointTorque;
                    }
                    if (MinimumTorque.Nm < 0)
                    {
                        yield return LocationValidationError.MinimumTorqueHasToBeGreaterThanOrEqualToZero;
                    }
                    break;
                case nameof(Location.MaximumTorque):
                    if (MaximumTorque.Nm < SetPointTorque.Nm)
                    {
                        yield return LocationValidationError.MaximumTorqueHasToBeGreaterOrEqualThanSetpointTorque;
                    }
                    if (MaximumTorque.Nm < 0)
                    {
                        yield return LocationValidationError.MaximumTorqueHasToBeGreaterThanOrEqualToZero;
                    }
                    break;
                case nameof(Location.ThresholdTorque):
                    if(ThresholdTorque.Nm <= 0)
                    {
                        yield return LocationValidationError.ThresholdTorqueIsLessThanOrEqualToZero;
                    }
                    if(SetPointTorque.Nm > 0 && ThresholdTorque.Nm > SetPointTorque.Nm)
                    {
                        yield return LocationValidationError.ThresholdTorqueIsGreaterThanSetpointTorque;
                    }
                    break;
                case nameof(Location.SetPointAngle):
                    if (ControlledBy == LocationControlledBy.Angle && SetPointAngle.Degree <= 0)
                    {
                        yield return LocationValidationError.SetpointAngleIsLessThanOrEqualToZero;
                    }
                    if(ControlledBy == LocationControlledBy.Torque && SetPointAngle.Degree < 0)
                    {
                        yield return LocationValidationError.SetpointAngleIsLessThanZero;
                    }
                    break;
                case nameof(Location.MinimumAngle):
                    if(MinimumAngle.Degree > SetPointAngle.Degree && SetPointAngle.Degree >= 0)
                    {
                        yield return LocationValidationError.MinimumAngleHasToBeLessOrEqualThanSetpointAngle;
                    }
                    if (MinimumAngle.Degree < 0)
                    {
                        yield return LocationValidationError.MinimumAngleHasToBeGreaterThanOrEqualToZero;
                    }
                    break;
                case nameof(Location.MaximumAngle):
                    if(MaximumAngle.Degree < SetPointAngle.Degree)
                    {
                        yield return LocationValidationError.MaximumAngleHasToBeGreaterOrEqualThanSetpointAngle;
                    }
                    if (MaximumAngle.Degree < 0)
                    {
                        yield return LocationValidationError.MaximumAngleHasToBeGreaterThanOrEqualToZero;
                    }
                    break;
            }
        }
    }
}