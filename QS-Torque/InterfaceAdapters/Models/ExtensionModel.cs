using Core;
using Core.Entities;
using System;
using System.ComponentModel;
using Client.Core.Entities;
using Common.Types.Enums;
using InterfaceAdapters.Localization;

namespace InterfaceAdapters.Models
{
    public class ExtensionModel : 
        BindableBase, 
        IEquatable<ExtensionModel>, 
        IQstEquality<ExtensionModel>, 
        IUpdate<Extension>, 
        ICopy<ExtensionModel>,
        IDataErrorInfo
    {
        private readonly ILocalizationWrapper _localization;
        public Extension Entity { get; private set; }
        public long Id
        {
            get => Entity.Id.ToLong();
            set
            {
                Entity.Id = new ExtensionId(value);
                RaisePropertyChanged();
            }
        }

        public string InventoryNumber
        {
            get => Entity.InventoryNumber?.ToDefaultString();
            set
            {
                Entity.InventoryNumber = new ExtensionInventoryNumber(value);
                RaisePropertyChanged();
            }
        }

        public string Description
        {
            get => Entity.Description;
            set
            {
                Entity.Description = value;
                RaisePropertyChanged();
            }
        }

        public ExtensionCorrection ExtensionCorrection
        {
            get => Entity.ExtensionCorrection;
            set
            {
                Entity.ExtensionCorrection = value;

                RaisePropertyChanged();
                RaisePropertyChanged(nameof(IsFactorVisible));
                RaisePropertyChanged(nameof(IsGaugeVisible));
                RaisePropertyChanged(nameof(Length));
                RaisePropertyChanged(nameof(FactorTorque));               
            }
        }


        public bool IsFactorVisible => ExtensionCorrection == ExtensionCorrection.UseFactor;
        public bool IsGaugeVisible => ExtensionCorrection == ExtensionCorrection.UseGauge;

        public double Length
        {
            get => Entity.Length;
            set
            {
                Entity.Length = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(FactorTorque));
            }
        }

        public double FactorTorque
        {
            get => Entity.FactorTorque;
            set
            {
                Entity.FactorTorque = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(Length));
            }
        }

        public double Bending
        {
            get => Entity.Bending;
            set
            {
                Entity.Bending = value;
                RaisePropertyChanged();
            }
        }

        public ExtensionModel(Extension entity, ILocalizationWrapper localization)
        {
            _localization = localization;
            Entity = entity ?? new Extension();
            RaisePropertyChanged();
        }

        public static ExtensionModel GetModelFor(Extension entity, ILocalizationWrapper localization)
        {
            return entity != null ? new ExtensionModel(entity, localization) : null;
        }

        public override bool Equals(object obj)
        {
            return obj is ExtensionModel em && Equals(em);
        }

        public bool Equals(ExtensionModel other)
        {
            return this.EqualsById(other);
        }

        public bool EqualsById(ExtensionModel other)
        {
            return Entity.EqualsById(other?.Entity);
        }

        public bool EqualsByContent(ExtensionModel other)
        {
            return Entity.EqualsByContent(other?.Entity);
        }

        public void UpdateWith(Extension other)
        {
            this.Entity.UpdateWith(other);
            RaisePropertyChanged("");
        }

        public ExtensionModel CopyDeep()
        {
            return new ExtensionModel(Entity.CopyDeep(), _localization);
        }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Description)}: {Description}, {nameof(InventoryNumber)}: {InventoryNumber}, {nameof(Length)}: {Length}, {nameof(FactorTorque)}: {FactorTorque}, {nameof(Bending)}: {Bending}, {nameof(Id)}: {Id}, {nameof(Description)}: {Description}, {nameof(InventoryNumber)}: {InventoryNumber}, {nameof(Length)}: {Length}, {nameof(FactorTorque)}: {FactorTorque}, {nameof(Bending)}: {Bending}";
        }

        public string Error { get; }

        public string this[string columnName]
        {
            get
            {
                var error = Entity.Validate(columnName);
                if (error == null)
                {
                    return null;
                }

                switch (error)
                {
                    case Extension.ExtensionValidationError.InventoryNumberIsEmpty:
                        return _localization.Strings.GetParticularString("ExtensionValidationError", "The inventory number cannot be empty!");

                    case Extension.ExtensionValidationError.DescriptionIsEmpty:
                        return _localization.Strings.GetParticularString("ExtensionValidationError", "The description cannot be empty!");

                    case Extension.ExtensionValidationError.LengthNotGreaterOrEqualTo0AndLessThan10000:
                        return _localization.Strings.GetParticularString("ExtensionValidationError", "The gauge has to be greater or equal to 0 and less than 10000!");

                    case Extension.ExtensionValidationError.FactorNotBetween0Point9And1Point5:
                        return _localization.Strings.GetParticularString("ExtensionValidationError", "The factor has to be between 0.9 and 1.5!");

                    case Extension.ExtensionValidationError.BendingCompensationNotGreaterOrEqual0AndLess100:
                        return _localization.Strings.GetParticularString("ExtensionValidationError", "The bending compensation has to be greater than ot equal to 0 and less than 100!");
                }

                return "";
            }
        }
    }
}
