using System;
using System.Diagnostics;
using Common.Types.Enums;

namespace Core.Entities
{
    public class ExtensionId : QstIdentifier, IEquatable<ExtensionId>
    {
        public ExtensionId(long value)
            : base(value)
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


        private ExtensionCorrection? _extensionCorrection;
        public ExtensionCorrection ExtensionCorrection
        {
            get
            {
                if (_extensionCorrection != null)
                {
                    return _extensionCorrection.Value;
                }

                return FactorTorque == 1 && Length == 0 ? ExtensionCorrection.UseFactor :
                       FactorTorque != 1 ? ExtensionCorrection.UseFactor : ExtensionCorrection.UseGauge;
            }

            set
            {
                _extensionCorrection = value;
                if (value == ExtensionCorrection.UseGauge)
                {
                    FactorTorque = 1;
                }
                else
                {
                    Length = 0;
                }
            }
        }


        public Extension CopyDeep()
        {
            return new Extension()
            {
                Id = this.Id != null ? new ExtensionId(this.Id.ToLong()) : null,
                InventoryNumber = this.InventoryNumber,
                Description = this.Description,
                Length = this.Length,
                FactorTorque = this.FactorTorque,
                Bending = this.Bending
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
                   this.Bending == other.Bending;
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
        }

        public ExtensionValidationError? Validate(string property)
        {
            switch (property)
            {
                case nameof(InventoryNumber):
                    if (InventoryNumber?.ToDefaultString() == null || InventoryNumber.ToDefaultString() == "")
                    {
                        return ExtensionValidationError.InventoryNumberIsEmpty;
                    }
                    break;
                case nameof(Description):
                    if (string.IsNullOrEmpty(Description))
                    {
                        return ExtensionValidationError.DescriptionIsEmpty;
                    }
                    break;

                case nameof(Length):
                    if (Length < 0 || Length >= 10000)
                    {
                        return ExtensionValidationError.LengthNotGreaterOrEqualTo0AndLessThan10000;
                    }
                    break;

                case nameof(FactorTorque):
                    if (FactorTorque < 0.9 || FactorTorque > 1.5)
                    {
                        return ExtensionValidationError.FactorNotBetween0Point9And1Point5;
                    }
                    break;

                case nameof(Bending):
                    if (Bending < 0 || Bending >= 100)
                    {
                        return ExtensionValidationError.BendingCompensationNotGreaterOrEqual0AndLess100;
                    }
                    break;
            }

            return null;
        }

        public enum ExtensionValidationError
        {
            InventoryNumberIsEmpty,
            DescriptionIsEmpty,
            FactorNotBetween0Point9And1Point5,
            LengthNotGreaterOrEqualTo0AndLessThan10000,
            BendingCompensationNotGreaterOrEqual0AndLess100
        }
    }
}
