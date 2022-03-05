using Core.Entities;
using System;
using Core;
using InterfaceAdapters.Localization;

namespace InterfaceAdapters.Models
{
    public class ToolModelModel : BindableBase, IEquatable<ToolModelModel>, IQstEquality<ToolModelModel>, IUpdate<Core.Entities.ToolModel>, ICopy<ToolModelModel>
    {
        public Core.Entities.ToolModel Entity { get; private set; }
        private ILocalizationWrapper _localization;


        #region Properties
        
        public long Id
        {
            get => Entity.Id.ToLong();
            set
            {
                Entity.Id = new ToolModelId(value);
                RaisePropertyChanged();
            }
        }

        public string Description
        {
            get => Entity.Description?.ToDefaultString();
            set
            {
                Entity.Description = new ToolModelDescription(value);
                RaisePropertyChanged();
            }
        }
        
        public AbstractToolTypeModel ModelType
        {
            get => AbstractToolTypeModel.MapToolTypeToToolTypeModel(Entity.ModelType, _localization);
            set
            {
                Entity.ModelType = AbstractToolTypeModel.MapToolTypeModelToToolType(value);
                RaisePropertyChanged();
            }
        }
        
        public ToolModelClassModel Class
        {
            get
            {
                if (ShouldPropertyBeVisible(nameof(Class)))
                {
                    return ToolModelClassModel.CreateToolModelClassModelFromClass(Entity.Class, _localization);
                }
                else
                {
                    return null;
                }
            } 
            set
            {
                Entity.Class = value?.ToolModelClass ?? (Core.Enums.ToolModelClass)(-1);
                RaisePropertyChanged();
            }
        }
        
        public ManufacturerModel Manufacturer
        {
            get => ManufacturerModel.GetModelFor(Entity.Manufacturer);
            set
            {
                Entity.Manufacturer = value?.Entity;
                RaisePropertyChanged();
            }
        }
        
        public double? MinPower
        {
            get => Entity.MinPower;
            set
            {
                Entity.MinPower = value;
                RaisePropertyChanged();
            }
        }
        
        public double? MaxPower
        {
            get => Entity.MaxPower;
            set
            {
                Entity.MaxPower = value;
                RaisePropertyChanged();
            }
        }
        
        public double? AirPressure
        {
            get => Entity.AirPressure;
            set
            {
                Entity.AirPressure = value;
                RaisePropertyChanged();
            }
        }

        public HelperTableItemModel<ToolType, string> ToolType
        {
            get => HelperTableItemModel.GetModelForToolType(Entity.ToolType);
            set
            {
                Entity.ToolType = value?.Entity;
                RaisePropertyChanged();
            }
        }
        
        public double Weight
        {
            get => Entity.Weight;
            set
            {
                Entity.Weight = value;
                RaisePropertyChanged();
            }
        }
        
        public double? BatteryVoltage
        {
            get => Entity.BatteryVoltage;
            set
            {
                Entity.BatteryVoltage = value;
                RaisePropertyChanged();
            }
        }
        
        public long? MaxRotationSpeed
        {
            get => Entity.MaxRotationSpeed;
            set
            {
                Entity.MaxRotationSpeed = value;
                RaisePropertyChanged();
            }
        }
        
        public double? AirConsumption
        {
            get => Entity.AirConsumption;
            set
            {
                Entity.AirConsumption = value;
                RaisePropertyChanged();
            }
        }
        
        public HelperTableItemModel<SwitchOff, string> SwitchOff
        {
            get => HelperTableItemModel.GetModelForSwitchOff(Entity.SwitchOff);
            set
            {
                Entity.SwitchOff = value?.Entity;
                RaisePropertyChanged();
            }
        }
        
        public HelperTableItemModel<DriveSize, string> DriveSize
        {
            get => HelperTableItemModel.GetModelForDriveSize(Entity.DriveSize);
            set
            {
                Entity.DriveSize = value?.Entity;
                RaisePropertyChanged();
            }
        }
        
        public HelperTableItemModel<ShutOff, string> ShutOff
        {
            get => HelperTableItemModel.GetModelForShutOff(Entity.ShutOff);
            set
            {
                Entity.ShutOff = value?.Entity;
                RaisePropertyChanged();
            }
        }
        
        public HelperTableItemModel<DriveType, string> DriveType
        {
            get => HelperTableItemModel.GetModelForDriveType(Entity.DriveType);
            set
            {
                Entity.DriveType = value?.Entity;
                RaisePropertyChanged();
            }
        }
        
        public HelperTableItemModel<ConstructionType, string> ConstructionType
        {
            get => HelperTableItemModel.GetModelForConstructionType(Entity.ConstructionType);
            set
            {
                Entity.ConstructionType = value?.Entity;
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
        
        public double CmLimit
        {
            get => Entity.CmLimit;
            set
            {
                Entity.CmLimit = value;
                RaisePropertyChanged();
            }
        }
        
        public double CmkLimit
        {
            get => Entity.CmkLimit;
            set
            {
                Entity.CmkLimit = value;
                RaisePropertyChanged();
            }
        }

        #endregion


        public ToolModelModel(Core.Entities.ToolModel entity, ILocalizationWrapper localization)
        {
            Entity = entity ?? new Core.Entities.ToolModel();
            _localization = localization;
            RaisePropertyChanged();
        }


        #region Mapping

        public static ToolModelModel GetModelFor(Core.Entities.ToolModel entity, ILocalizationWrapper wrapper)
        {
            return entity != null ? new ToolModelModel(entity, wrapper) : null;
        }

        #endregion

        #region Methods

        public void UpdateWith(Core.Entities.ToolModel other)
        {
            this.Entity.UpdateWith(other);
            RaisePropertyChanged();
        }
        
        public bool ShouldPropertyBeVisible(string propertyName)
        {
            return ModelType?.ShouldPropertyBeVisible(propertyName) ?? true;
        }

        public bool Equals(ToolModelModel other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return this.EqualsById(other);
        }

        public ToolModelModel CopyDeep()
        {
            return new ToolModelModel(Entity.CopyDeep(), _localization);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ToolModelModel) obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        #endregion

        public bool EqualsById(ToolModelModel other)
        {
            return this.Entity.EqualsById(other?.Entity);
        }

        public bool EqualsByContent(ToolModelModel other)
        {
            return this.Entity.EqualsByContent(other?.Entity);
        }
    }
}