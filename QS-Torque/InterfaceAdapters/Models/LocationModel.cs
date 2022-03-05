using System;
using System.ComponentModel;
using System.Linq;
using Core;
using Core.Entities;
using Core.Enums;
using Core.PhysicalValueTypes;
using Core.UseCases;
using InterfaceAdapters.Localization;

namespace InterfaceAdapters.Models
{
    public interface IMovableTreeItem
    {
        void ChangeParent(LocationDirectoryId newParentId);
    }

    public class LocationModel : BindableBase, IMovableTreeItem, IQstEquality<LocationModel>, IUpdate<Location>, ICopy<LocationModel>, IDataErrorInfo
    {
        public Location Entity { get; private set; }
        private ILocalizationWrapper _localization;
        

        public long Id
        {
            get => Entity.Id.ToLong();
            set
            {
                Entity.Id = new LocationId(value);
                RaisePropertyChanged();
            }
        }
        
        public string Number
        {
            get => Entity.Number?.ToDefaultString();
            set
            {
                Entity.Number = new LocationNumber(value);
                RaisePropertyChanged();
            }
        }
        
        public string Description
        {
            get => Entity.Description?.ToDefaultString();
            set
            {
                Entity.Description = new LocationDescription(value);
                RaisePropertyChanged();
            }
        }
        
        public long ParentId
        {
            get => Entity.ParentDirectoryId?.ToLong() ?? -1;
            set
            {
                Entity.ParentDirectoryId = new LocationDirectoryId(value);
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
                RaisePropertyChanged(nameof(LocationModel.SetPointTorque));
                RaisePropertyChanged(nameof(LocationModel.SetPointAngle));
                RaisePropertyChanged(nameof(LocationModel.ThresholdTorque));
            }
        }
        
        public double SetPointTorque
        {
            get => Entity.SetPointTorque.Nm;
            set
            {
                Entity.SetPointTorque = Torque.FromNm(value);
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(LocationModel.MinimumTorque));
                RaisePropertyChanged(nameof(LocationModel.MaximumTorque));
                RaisePropertyChanged(nameof(LocationModel.MinimumAngle));
                RaisePropertyChanged(nameof(LocationModel.MaximumAngle));
                RaisePropertyChanged(nameof(LocationModel.ThresholdTorque));
            }
        }
        
        public ToleranceClassModel ToleranceClassTorque
        {
            get => ToleranceClassModel.GetModelFor(Entity.ToleranceClassTorque);
            set
            {
                Entity.ToleranceClassTorque = value?.Entity;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(LocationModel.MinimumTorque));
                RaisePropertyChanged(nameof(LocationModel.MaximumTorque));
                RaisePropertyChanged(nameof(LocationModel.MinimumAngle));
                RaisePropertyChanged(nameof(LocationModel.MaximumAngle));
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
        
        public ToleranceClassModel ToleranceClassAngle
        {
            get => ToleranceClassModel.GetModelFor(Entity.ToleranceClassAngle);
            set
            {
                Entity.ToleranceClassAngle = value?.Entity;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(LocationModel.MinimumTorque));
                RaisePropertyChanged(nameof(LocationModel.MaximumTorque));
                RaisePropertyChanged(nameof(LocationModel.MinimumAngle));
                RaisePropertyChanged(nameof(LocationModel.MaximumAngle));
            }
        }

        public double ThresholdTorque
        {
            get => Entity.ThresholdTorque.Nm;
            set
            {
                Entity.ThresholdTorque = Torque.FromNm(value);
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
                RaisePropertyChanged(nameof(LocationModel.MinimumTorque));
                RaisePropertyChanged(nameof(LocationModel.MaximumTorque));
                RaisePropertyChanged(nameof(LocationModel.MinimumAngle));
                RaisePropertyChanged(nameof(LocationModel.MaximumAngle));
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
        
        public string ConfigurableField1
        {
            get => Entity.ConfigurableField1?.ToDefaultString();
            set
            {
                Entity.ConfigurableField1 = new LocationConfigurableField1(value);
                RaisePropertyChanged();
            }
        }
        
        public string ConfigurableField2
        {
            get => Entity.ConfigurableField2?.ToDefaultString();
            set
            {
                Entity.ConfigurableField2 = new LocationConfigurableField2(value);
                RaisePropertyChanged();
            }
        }
        
        public bool ConfigurableField3
        {
            get => Entity.ConfigurableField3;
            set
            {
                Entity.ConfigurableField3 = value;
                RaisePropertyChanged();
            }
        }
        
        public string Comment
        {
            get => Entity.Comment;
            set
            {
                Entity.Comment = value;
                RaisePropertyChanged();
            }
        }
        
        public PictureModel Picture
        {
            get => PictureModel.GetModelFor(Entity.Picture);
            set
            {
                Entity.Picture = value?.Entity;
                RaisePropertyChanged();
            }
        }

        private Action<LocationModel, LocationDirectoryId> _changeParentAction;


        public LocationModel(Location entity, ILocalizationWrapper localization, Action<LocationModel, LocationDirectoryId> changeParentAction)
        {
            Entity = entity ?? new Location();
            _localization = localization;
            _changeParentAction = changeParentAction;
            RaisePropertyChanged();
        }


        public static LocationModel GetModelFor(Location entity, ILocalizationWrapper localization, ILocationUseCase useCase)
        {
            return entity != null ? new LocationModel(entity, localization, (model, id) => useCase.ChangeLocationParent(model?.Entity, id)) : null;
        }


        #region Methods
        public virtual void UpdateWith(Location other)
        {
            this.Entity.UpdateWith(other);
            RaisePropertyChanged();
        }

        #endregion


        public string Error { get; }

        public string this[string columnName]
        {
            get
            {
                var errors = Entity.Validate(columnName);
                if(errors == null || errors.ToList().Count <= 0)
                {
                    return null;
                }
                string result = "";

                foreach(var error in errors)
                {
                    switch (error)
                    {
                        case LocationValidationError.SetpointTorqueIsLessThanOrEqualToZero:
                            result += _localization.Strings.GetParticularString("LocationValidationError", "The setpoint torque has to be greater than zero") + "\n";
                            break;
                        case LocationValidationError.SetpointTorqueIsLessThanZero:
                            result += _localization.Strings.GetParticularString("LocationValidationError", "The setpoint torque has to be greater than or equal to zero") + "\n";
                            break;
                        case LocationValidationError.MinimumTorqueHasToBeLessOrEqualThanSetpointTorque:
                            result += _localization.Strings.GetParticularString("LocationValidationError", "The minimum torque has to be less than or equal to the setpoint torque") + "\n";
                            break;
                        case LocationValidationError.MaximumTorqueHasToBeGreaterOrEqualThanSetpointTorque:
                            result += _localization.Strings.GetParticularString("LocationValidationError", "The maximum torque has to be greater than or equal to the setpoint torque") + "\n";
                            break;
                        case LocationValidationError.ThresholdTorqueIsLessThanOrEqualToZero:
                            result += _localization.Strings.GetParticularString("LocationValidationError", "The threshold torque has to be greater than zero") + "\n";
                            break;
                        case LocationValidationError.ThresholdTorqueIsGreaterThanSetpointTorque:
                            result += _localization.Strings.GetParticularString("LocationValidationError", "The threshold torque has to be less than or equal to the setpoint torque") + "\n";
                            break;
                        case LocationValidationError.SetpointAngleIsLessThanOrEqualToZero:
                            result += _localization.Strings.GetParticularString("LocationValidationError", "The setpoint angle has to be greater than zero") + "\n";
                            break;
                        case LocationValidationError.SetpointAngleIsLessThanZero:
                            result += _localization.Strings.GetParticularString("LocationValidationError", "The setpoint angle has to be greater than or equal to zero") + "\n";
                            break;
                        case LocationValidationError.MinimumAngleHasToBeLessOrEqualThanSetpointAngle:
                            result += _localization.Strings.GetParticularString("LocationValidationError", "The minimum angle has to be less than or equal to the setpoint angle") + "\n";
                            break;
                        case LocationValidationError.MaximumAngleHasToBeGreaterOrEqualThanSetpointAngle:
                            result += _localization.Strings.GetParticularString("LocationValidationError", "The maximum angle has to be greater than or equal to the setpoint angle") + "\n";
                            break;
                        case LocationValidationError.MinimumTorqueHasToBeGreaterThanOrEqualToZero:
                            result += _localization.Strings.GetParticularString("LocationValidationError", "The minimum torque has to be greater than or equal to zero") + "\n";
                            break;
                        case LocationValidationError.MaximumTorqueHasToBeGreaterThanOrEqualToZero:
                            result += _localization.Strings.GetParticularString("LocationValidationError", "The maximum torque has to be greater than or equal to zero") + "\n";
                            break;
                        case LocationValidationError.MinimumAngleHasToBeGreaterThanOrEqualToZero:
                            result += _localization.Strings.GetParticularString("LocationValidationError", "The minimum angle has to be greater than or equal to zero") + "\n";
                            break;
                        case LocationValidationError.MaximumAngleHasToBeGreaterThanOrEqualToZero:
                            result += _localization.Strings.GetParticularString("LocationValidationError", "The maximum angle has to be greater than or equal to zero") + "\n";
                            break;
                    }
                }

                return result.TrimEnd();
            }
        }

        public void ChangeParent(LocationDirectoryId newParentId)
        {
            _changeParentAction.Invoke(this, newParentId);
        }

        public bool EqualsById(LocationModel other)
        {
            return this.Entity.EqualsById(other?.Entity);
        }

        public bool EqualsByContent(LocationModel other)
        {
            return this.Entity.EqualsByContent(other?.Entity);
        }

        public LocationModel CopyDeep()
        {
            return new LocationModel(Entity.CopyDeep(), _localization, _changeParentAction);
        }
    }
}