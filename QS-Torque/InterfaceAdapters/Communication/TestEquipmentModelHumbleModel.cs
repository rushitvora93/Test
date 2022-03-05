using System;
using System.ComponentModel;
using Client.Core.Entities;
using Core;
using Core.Entities;
using InterfaceAdapters.Localization;

namespace InterfaceAdapters.Communication
{
    public class TestEquipmentModelHumbleModel : 
        BindableBase, 
        IEquatable<TestEquipmentModelHumbleModel>, 
        IQstEquality<TestEquipmentModelHumbleModel>, 
        ICopy<TestEquipmentModelHumbleModel>, 
        IUpdate<TestEquipmentModel>,
        IDataErrorInfo
    {
        private ILocalizationWrapper _localization;
        public Core.Entities.TestEquipmentModel Entity { get; private set; }

        public TestEquipmentModelHumbleModel(Core.Entities.TestEquipmentModel entity, ILocalizationWrapper localization)
        {
            _localization = localization;
            Entity = entity ?? new Core.Entities.TestEquipmentModel();
            RaisePropertyChanged();
        }

        public static TestEquipmentModelHumbleModel GetModelFor(Core.Entities.TestEquipmentModel entity, ILocalizationWrapper localization)
        {
            return entity != null ? new TestEquipmentModelHumbleModel(entity, localization) : null;
        }


        public long Id
        {
            get => Entity.Id.ToLong();
            set
            {
                Entity.Id = new TestEquipmentModelId(value);
                RaisePropertyChanged();
            }
        }

        public string TestEquipmentModelName
        {
            get => Entity.TestEquipmentModelName.ToDefaultString();
            set
            {
                Entity.TestEquipmentModelName = new TestEquipmentModelName(value);
                RaisePropertyChanged();
            }
        }

        public string ManufacturerName => Entity.Manufacturer?.Name?.ToDefaultString();

        public string ResultFilePath
        {
            get => Entity.ResultFilePath?.ToDefaultString();
            set
            {
                Entity.ResultFilePath = new TestEquipmentSetupPath(value);
                RaisePropertyChanged();
            }
        }

        public string DriverProgramPath
        {
            get => Entity.DriverProgramPath?.ToDefaultString();
            set
            {
                Entity.DriverProgramPath = new TestEquipmentSetupPath(value);
                RaisePropertyChanged();
            }
        }

        public string CommunicationFilePath
        {
            get => Entity.CommunicationFilePath?.ToDefaultString();
            set
            {
                Entity.CommunicationFilePath = new TestEquipmentSetupPath(value);
                RaisePropertyChanged();
            }
        }

        public string StatusFilePath
        {
            get => Entity.StatusFilePath?.ToDefaultString();
            set
            {
                Entity.StatusFilePath = new TestEquipmentSetupPath(value);
                RaisePropertyChanged();
            }
        }

        public TestEquipmentTypeModel TestEquipmentType
        {
            get => new TestEquipmentTypeModel(_localization, Entity.Type);
            set => Entity.Type = value.Type;
        }

        public bool TransferUser
        {
            get => Entity.TransferUser;
            set
            {
                Entity.TransferUser = value;
                RaisePropertyChanged();
            }
        }

        public bool TransferAdapter
        {
            get => Entity.TransferAdapter;
            set
            {
                Entity.TransferAdapter = value;
                RaisePropertyChanged();
            }
        }
        public bool TransferTransducer
        {
            get => Entity.TransferTransducer;
            set
            {
                Entity.TransferTransducer = value;
                RaisePropertyChanged();
            }
        }
        public bool AskForIdent
        {
            get => Entity.AskForIdent;
            set
            {
                Entity.AskForIdent = value;
                RaisePropertyChanged();
            }
        }
        public bool TransferCurves
        {
            get => Entity.TransferCurves;
            set
            {
                Entity.TransferCurves = value;
                RaisePropertyChanged();
            }
        }
        public bool UseErrorCodes
        {
            get => Entity.UseErrorCodes;
            set
            {
                Entity.UseErrorCodes = value;
                RaisePropertyChanged();
            }
        }
        public bool AskForSign
        {
            get => Entity.AskForSign;
            set
            {
                Entity.AskForSign = value;
                RaisePropertyChanged();
            }
        }
        public bool DoLoseCheck
        {
            get => Entity.DoLoseCheck;
            set
            {
                Entity.DoLoseCheck = value;
                RaisePropertyChanged();
            }
        }
        public bool CanDeleteMeasurements
        {
            get => Entity.CanDeleteMeasurements;
            set
            {
                Entity.CanDeleteMeasurements = value;
                RaisePropertyChanged();
            }
        }

        public bool ConfirmMeasurements
        {
            get => Entity.ConfirmMeasurements;
            set
            {
                Entity.ConfirmMeasurements = value;
                RaisePropertyChanged();
            }
        }

        public bool CanUseQstStandard
        {
            get => Entity.CanUseQstStandard;
            set
            {
                Entity.CanUseQstStandard = value;
                RaisePropertyChanged();
            }
        }

        public bool TransferLocationPictures
        {
            get => Entity.TransferLocationPictures;
            set
            {
                Entity.TransferLocationPictures = value;
                RaisePropertyChanged();
            }
        }

        public bool TransferNewLimits
        {
            get => Entity.TransferNewLimits;
            set
            {
                Entity.TransferNewLimits = value;
                RaisePropertyChanged();
            }
        }

        public bool TransferAttributes
        {
            get => Entity.TransferAttributes;
            set
            {
                Entity.TransferAttributes = value;
                RaisePropertyChanged();
            }
        }

        public bool UseForCtl
        {
            get => Entity.UseForCtl;
            set
            {
                Entity.UseForCtl = value;
                RaisePropertyChanged();
            }
        }

        public bool UseForRot
        {
            get => Entity.UseForRot;
            set
            {
                Entity.UseForRot = value;
                RaisePropertyChanged();
            }
        }

        public DataGateVersion DataGateVersion
        {
            get => Entity.DataGateVersion;
            set
            {
                Entity.DataGateVersion = value;
                RaisePropertyChanged();
            }
        }

        public TestEquipmentModelHumbleModel CopyDeep()
        {
            return new TestEquipmentModelHumbleModel(Entity.CopyDeep(), _localization);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public bool Equals(TestEquipmentModelHumbleModel other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return this.EqualsById(other);
        }


        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TestEquipmentModelHumbleModel)obj);
        }

        public bool EqualsById(TestEquipmentModelHumbleModel other)
        {
            return this.Entity.EqualsById(other?.Entity);
        }

        public bool EqualsByContent(TestEquipmentModelHumbleModel other)
        {
            return this.Entity.EqualsByContent(other?.Entity);
        }

        public void UpdateWith(TestEquipmentModel other)
        {
            Entity.UpdateWith(other);
            RaisePropertyChanged(null);
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

                if (error == TestEquipmentModelValidationError.NameIsEmpty)
                {
                    return _localization.Strings.GetParticularString("TestEquipmentModelValidationError", "The test equipment model name cannot be empty").TrimEnd();
                }

                return "";
            }
        }
    }
}
