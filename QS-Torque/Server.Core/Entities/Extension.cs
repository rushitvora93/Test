using Core;
using Core.Entities;
using System;
using System.Diagnostics;

namespace Server.Core.Entities
{
    public class ExtensionId : QstIdentifier, IEquatable<ExtensionId>
    {
        public ExtensionId(long value) : base(value)
        {
        }

        public bool Equals(ExtensionId other)
        {
            return Equals((QstIdentifier)other);
        }
    }

    public class ExtensionInventoryNumber : TypeCheckedString<MaxLength<CtInt50>, Blacklist<NewLines>, NoCheck>, IEquatable<ExtensionInventoryNumber>
    {
        public ExtensionInventoryNumber(string serialNumber)
            : base(serialNumber)
        {
        }

        public bool Equals(ExtensionInventoryNumber other)
        {
            return ToDefaultString().Equals(other?.ToDefaultString());
        }
    }

    public class Extension : IQstEquality<Extension>, IUpdate<Extension>, ICopy<Extension>
    {
        public ExtensionId Id { get; set; }
        public ExtensionInventoryNumber InventoryNumber { get; set; }
        public string Description { get; set; }
        public double Length { get; set; }
        public double FactorTorque { get; set; }
        public double Bending { get; set; }
        public bool Alive { get; set; }

        public Extension CopyDeep()
        {
            return new Extension()
            {
                Id = this.Id != null ? new ExtensionId(this.Id.ToLong()) : null,
                InventoryNumber = this.InventoryNumber,
                Description = this.Description,
                Length = this.Length,
                FactorTorque = this.FactorTorque,
                Bending = this.Bending,
                Alive = this.Alive
            };
        }

        public bool EqualsByContent(Extension other)
        {
            if (other == null)
            {
                return false;
            }

            return this.Id.Equals(other.Id) &&
                   this.InventoryNumber?.ToDefaultString() == other.InventoryNumber?.ToDefaultString() &&
                   this.Description == other.Description &&
                   this.Length == other.Length &&
                   this.FactorTorque == other.FactorTorque &&
                   this.Bending == other.Bending &&
                   this.Alive == other.Alive;
        }

        public bool EqualsById(Extension other)
        {
            return this.Id.Equals(other?.Id);
        }

        public void UpdateWith(Extension other)
        {
            if (other == null)
            {
                return;
            }

            this.Id = other.Id;
            this.InventoryNumber = other.InventoryNumber;
            this.Description = other.Description;
            this.Length = other.Length;
            this.FactorTorque = other.FactorTorque;
            this.Bending = other.Bending;
            this.Alive = other.Alive;
        }
    }
}
