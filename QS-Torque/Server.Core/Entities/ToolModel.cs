using System;
using Common.Types.Enums;
using Core;
using Core.Entities;
using Core.Enums;

namespace Server.Core.Entities
{
    public class ToolModelId : QstIdentifier, IEquatable<ToolModelId>
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

    public class ToolModel : IQstEquality<ToolModel>, IUpdate<ToolModel>, ICopy<ToolModel>
    {
        public ToolModelId Id;
        public string Description;
        public ToolModelType ModelType;
        public ToolModelClass Class;
        public Manufacturer Manufacturer;
        public double? MinPower;
        public double? MaxPower;
        public double? AirPressure;
        public ToolType ToolType;
        public double Weight;
        public double? BatteryVoltage;
        public long? MaxRotationSpeed;
        public double? AirConsumption;
        public SwitchOff SwitchOff;
        public DriveSize DriveSize;
        public ShutOff ShutOff;
        public DriveType DriveType;
        public ConstructionType ConstructionType;
        // picture
        public double CmLimit;
        public double CmkLimit;
        public bool Alive;

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
                   this.Description == other.Description &&
                   this.ModelType == other.ModelType &&
                   this.Class == other.Class &&
                   (this.Manufacturer?.EqualsByContent(other.Manufacturer) ?? other.Manufacturer == null) &&
                   this.MinPower == other.MinPower &&
                   this.MaxPower == other.MaxPower &&
                   this.AirPressure == other.AirPressure &&
                   this.Weight == other.Weight &&
                   this.BatteryVoltage == other.BatteryVoltage &&
                   this.MaxRotationSpeed == other.MaxRotationSpeed &&
                   this.AirConsumption == other.AirConsumption &&
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
            this.Weight = other.Weight;
            this.BatteryVoltage = other.BatteryVoltage;
            this.MaxRotationSpeed = other.MaxRotationSpeed;
            this.AirConsumption = other.AirConsumption;
            this.CmLimit = other.CmLimit;
            this.CmkLimit = other.CmkLimit;
        }

        public ToolModel CopyDeep()
        {
            return new ToolModel()
            {
                Id = this.Id != null ? new ToolModelId(this.Id.ToLong()) : null,
                Description = this.Description,
                ModelType = this.ModelType,
                Class = this.Class,
                Manufacturer = this.Manufacturer?.CopyDeep(),
                MinPower = this.MinPower,
                MaxPower = this.MaxPower,
                AirPressure = this.AirPressure,
                Weight = this.Weight,
                BatteryVoltage = this.BatteryVoltage,
                MaxRotationSpeed = this.MaxRotationSpeed,
                AirConsumption = this.AirConsumption,
                CmLimit = this.CmLimit,
                CmkLimit = this.CmkLimit
            };
        }

    }
}
