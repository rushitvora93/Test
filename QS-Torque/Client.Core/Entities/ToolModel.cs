using Core.Enums;
using System;
using Core.Entities.ToolTypes;

namespace Core.Entities
{
    public class ToolModelId: QstIdentifier, IEquatable<ToolModelId>
	{
		public ToolModelId(long value)
			: base(value)
		{
		}

        public bool Equals(ToolModelId other)
        {
            return Equals((QstIdentifier)other);
        }
    }

    public class ToolModelDescription : TypeCheckedString<MaxLength<CtInt40>, Blacklist<NewLines>, NoCheck>
    {
        public ToolModelDescription(string description)
            : base(description)
        {
        }
    }

    public class ToolModel : IQstEquality<ToolModel>, IUpdate<ToolModel>, ICopy<ToolModel>
    {
        public ToolModelId Id { get; set; }
        public ToolModelDescription Description { get; set; }
        public AbstractToolType ModelType { get; set; }
        public ToolModelClass Class { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public double? MinPower { get; set; }
        public double? MaxPower { get; set; }
        public double? AirPressure { get; set; }
        public ToolType ToolType { get; set; }
        public double Weight { get; set; }
        public double? BatteryVoltage { get; set; }
        public long? MaxRotationSpeed { get; set; }
        public double? AirConsumption { get; set; }
        public SwitchOff SwitchOff { get; set; }
        public DriveSize DriveSize { get; set; }
        public ShutOff ShutOff { get; set; }
        public DriveType DriveType { get; set; }
        public ConstructionType ConstructionType { get; set; }
        public Picture Picture { get; set; }
        public double CmLimit { get; set; }
        public double CmkLimit { get; set; }


        public bool EqualsById(ToolModel other)
        {
            return this.Id.Equals(other?.Id);
        }

        public bool EqualsByContent(ToolModel other)
        {
            if (other == null)
            {
                return false;
            }

            return this.Id.Equals(other.Id) &&
                   (this.Description?.Equals(other.Description) ?? other.Description == null) &&
                   (this.ModelType?.EqualsByType(other.ModelType) ?? other.ModelType == null) &&
                   this.Class == other.Class &&
                   (this.Manufacturer?.EqualsByContent(other.Manufacturer) ?? other.Manufacturer == null) &&
                   this.MinPower.GetValueOrDefault() == other.MinPower.GetValueOrDefault() &&
                   this.MaxPower.GetValueOrDefault() == other.MaxPower.GetValueOrDefault() &&
                   this.AirPressure.GetValueOrDefault() == other.AirPressure.GetValueOrDefault() &&
                   (this.ToolType?.EqualsByContent(other.ToolType) ?? other.ToolType == null) &&
                   this.Weight == other.Weight &&
                   this.BatteryVoltage.GetValueOrDefault() == other.BatteryVoltage.GetValueOrDefault() &&
                   this.MaxRotationSpeed.GetValueOrDefault() == other.MaxRotationSpeed.GetValueOrDefault() &&
                   this.AirConsumption.GetValueOrDefault() == other.AirConsumption.GetValueOrDefault() &&
                   (this.SwitchOff?.EqualsByContent(other.SwitchOff) ?? other.SwitchOff == null) &&
                   (this.DriveSize?.EqualsByContent(other.DriveSize) ?? other.DriveSize == null) &&
                   (this.ShutOff?.EqualsByContent(other.ShutOff) ?? other.ShutOff == null) &&
                   (this.DriveType?.EqualsByContent(other.DriveType) ?? other.DriveType == null) &&
                   (this.ConstructionType?.EqualsByContent(other.ConstructionType) ?? other.ConstructionType == null) &&
                   (this.Picture?.EqualsByContent(other.Picture) ?? other.Picture == null) &&
                   this.CmLimit == other.CmLimit &&
                   this.CmkLimit == other.CmkLimit;
        }

        public void UpdateWith(ToolModel other)
        {
            if (other == null)
            {
                return;
            }

            this.Id = other.Id;
            this.Description = other.Description;
            this.ModelType = other.ModelType;
            this.Class = other.Class;
            this.Manufacturer = other.Manufacturer;
            this.MinPower = other.MinPower;
            this.MaxPower = other.MaxPower;
            this.AirPressure = other.AirPressure;
            this.ToolType = other.ToolType;
            this.Weight = other.Weight;
            this.BatteryVoltage = other.BatteryVoltage;
            this.MaxRotationSpeed = other.MaxRotationSpeed;
            this.AirConsumption = other.AirConsumption;
            this.SwitchOff = other.SwitchOff;
            this.DriveSize = other.DriveSize;
            this.ShutOff = other.ShutOff;
            this.DriveType = other.DriveType;
            this.ConstructionType = other.ConstructionType;
            this.Picture = other.Picture;
            this.CmLimit = other.CmLimit;
            this.CmkLimit = other.CmkLimit;
        }

        public ToolModel CopyDeep()
        {
            return new ToolModel()
            {
                Id = this.Id != null ? new ToolModelId(this.Id.ToLong()) : null,
                Description = this.Description != null ? new ToolModelDescription(this.Description.ToDefaultString()) : null,
                ModelType = this.ModelType,
                Class = this.Class,
                Manufacturer = this.Manufacturer?.CopyDeep(),
                MinPower = this.MinPower,
                MaxPower = this.MaxPower,
                AirPressure = this.AirPressure,
                ToolType = this.ToolType?.CopyDeep(),
                Weight = this.Weight,
                BatteryVoltage = this.BatteryVoltage,
                MaxRotationSpeed = this.MaxRotationSpeed,
                AirConsumption = this.AirConsumption,
                SwitchOff = this.SwitchOff?.CopyDeep(),
                DriveSize = this.DriveSize?.CopyDeep(),
                ShutOff = this.ShutOff?.CopyDeep(),
                DriveType = this.DriveType?.CopyDeep(),
                ConstructionType = this.ConstructionType?.CopyDeep(),
                Picture = this.Picture?.CopyDeep(),
                CmLimit = this.CmLimit,
                CmkLimit = this.CmkLimit
            };
        }
    }
}
