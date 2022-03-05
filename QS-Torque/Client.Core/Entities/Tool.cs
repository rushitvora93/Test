using System;

namespace Core.Entities
{
    public class ToolId : QstIdentifier, IEquatable<ToolId>
	{
		public ToolId(long value)
			: base(value)
		{
		}

        public bool Equals(ToolId other)
        {
            return Equals((QstIdentifier)other);
        }
    }

    public class ConfigurableFieldString40 : TypeCheckedString<MaxLength<CtInt40>, Blacklist<NewLines>, NoCheck>, IEquatable<ConfigurableFieldString40>
    {
        public ConfigurableFieldString40(string description)
            : base(description)
        {
        }

        public bool Equals(ConfigurableFieldString40 other)
        {
            return other != null && this.ToDefaultString() == other.ToDefaultString();
        }
    }

    public class ConfigurableFieldString80 : TypeCheckedString<MaxLength<CtInt80>, Blacklist<NewLines>, NoCheck>, IEquatable<ConfigurableFieldString80>
    {
        public ConfigurableFieldString80(string description)
            : base(description)
        {
        }

        public bool Equals(ConfigurableFieldString80 other)
        {
            return other != null && this.ToDefaultString() == other.ToDefaultString();
        }
    }

    public class ConfigurableFieldString250 : TypeCheckedString<MaxLength<CtInt250>, NoCheck, NoCheck>, IEquatable<ConfigurableFieldString250>
    {
        public ConfigurableFieldString250(string description)
            : base(description)
        {
        }

        public bool Equals(ConfigurableFieldString250 other)
        {
            return other != null && this.ToDefaultString() == other.ToDefaultString();
        }
    }

    public class ToolInventoryNumber : TypeCheckedString<MaxLength<CtInt50>, Blacklist<NewLines>, NoCheck>
    {
        public ToolInventoryNumber(string description)
            : base(description)
        {
        }
    }

    public class ToolSerialNumber : TypeCheckedString<MaxLength<CtInt20>, Blacklist<NewLines>, NoCheck>
    {
        public ToolSerialNumber(string description)
            : base(description)
        {
        }
    }

    public class Tool : IQstEquality<Tool>, IUpdate<Tool>, ICopy<Tool>
    {
        public ToolId Id { get; set; }
        public ToolInventoryNumber InventoryNumber { get; set; }
        public ToolSerialNumber SerialNumber { get; set; }
        public ToolModel ToolModel { get; set; }
        public Status Status { get; set; }
        public CostCenter CostCenter { get; set; }
        public ConfigurableField ConfigurableField { get; set; }
        public string Accessory { get; set; }
        public string Comment { get; set; }
        public ConfigurableFieldString40 AdditionalConfigurableField1 { get; set; }
        public ConfigurableFieldString80 AdditionalConfigurableField2 { get; set; }
        public ConfigurableFieldString250 AdditionalConfigurableField3 { get; set; }
        public Picture Picture { get; set; }


        public bool EqualsById(Tool other)
        {
            return this.Id.Equals(other?.Id);
        }

        public bool EqualsByContent(Tool other)
        {
            if (other == null)
            {
                return false;
            }

            return this.Id.Equals(other.Id) &&
                   (this.InventoryNumber?.Equals(other.InventoryNumber) ?? other.InventoryNumber == null) &&
                   (this.SerialNumber?.Equals(other.SerialNumber) ?? other.SerialNumber == null) &&
                   (this.ToolModel?.EqualsByContent(other.ToolModel) ?? other.ToolModel == null) &&
                   (this.Status?.EqualsByContent(other.Status) ?? other.Status == null) &&
                   (this.CostCenter?.EqualsByContent(other.CostCenter) ?? other.CostCenter == null) &&
                   (this.ConfigurableField?.EqualsByContent(other.ConfigurableField) ?? other.ConfigurableField == null) &&
                   this.Accessory == other.Accessory &&
                   this.Comment == other.Comment &&
                   (this.AdditionalConfigurableField1?.Equals(other.AdditionalConfigurableField1) ?? other.AdditionalConfigurableField1 == null) &&
                   (this.AdditionalConfigurableField2?.Equals(other.AdditionalConfigurableField2) ?? other.AdditionalConfigurableField2 == null) &&
                   (this.AdditionalConfigurableField3?.Equals(other.AdditionalConfigurableField3) ?? other.AdditionalConfigurableField3 == null) &&
                   (this.Picture?.EqualsByContent(other.Picture) ?? other.Picture == null);
        }

        public void UpdateWith(Tool other)
        {
            if (other == null)
            {
                return;
            }

            this.Id = other.Id;
            this.InventoryNumber = other.InventoryNumber;
            this.SerialNumber = other.SerialNumber;
            this.ToolModel = other.ToolModel;
            this.Status = other.Status;
            this.CostCenter = other.CostCenter;
            this.ConfigurableField = other.ConfigurableField;
            this.Accessory = other.Accessory;
            this.Comment = other.Comment;
            this.AdditionalConfigurableField1 = other.AdditionalConfigurableField1;
            this.AdditionalConfigurableField2 = other.AdditionalConfigurableField2;
            this.AdditionalConfigurableField3 = other.AdditionalConfigurableField3;
            this.Picture = other.Picture;
        }

        public Tool CopyDeep()
        {
            return new Tool()
            {
                Id = this.Id != null ? new ToolId(this.Id.ToLong()) : null,
                SerialNumber = this.SerialNumber != null ? new ToolSerialNumber(this.SerialNumber.ToDefaultString()) : null,
                InventoryNumber = this.InventoryNumber != null ? new ToolInventoryNumber(this.InventoryNumber.ToDefaultString()) : null,
                ToolModel = this.ToolModel?.CopyDeep(),
                Status = this.Status?.CopyDeep(),
                CostCenter = this.CostCenter?.CopyDeep(),
                ConfigurableField = this.ConfigurableField?.CopyDeep(),
                Accessory = this.Accessory,
                Comment = this.Comment,
                AdditionalConfigurableField1 = this.AdditionalConfigurableField1 != null ? new ConfigurableFieldString40(this.AdditionalConfigurableField1.ToDefaultString()) : null,
                AdditionalConfigurableField2 = this.AdditionalConfigurableField2 != null ? new ConfigurableFieldString80(this.AdditionalConfigurableField2.ToDefaultString()) : null,
                AdditionalConfigurableField3 = this.AdditionalConfigurableField3 != null ? new ConfigurableFieldString250(this.AdditionalConfigurableField3.ToDefaultString()) : null,
                Picture = this.Picture?.CopyDeep()
            };
        }
    }
}
