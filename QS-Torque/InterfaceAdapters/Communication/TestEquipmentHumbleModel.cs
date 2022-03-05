using System;
using System.ComponentModel;
using Client.Core.Entities;
using Common.Types.Enums;
using Core;
using Core.Entities;
using Core.Enums;
using Core.PhysicalValueTypes;
using InterfaceAdapters.Localization;
using InterfaceAdapters.Models;

namespace InterfaceAdapters.Communication
{
    public class TestEquipmentHumbleModel: 
        BindableBase, 
        IEquatable<TestEquipmentHumbleModel>, 
        IQstEquality<TestEquipmentHumbleModel>, 
        ICopy<TestEquipmentHumbleModel>, 
        IUpdate<TestEquipment>,
        IDataErrorInfo
    {
        private readonly ILocalizationWrapper _localization;
        public TestEquipment Entity { get; private set; }

        public TestEquipmentHumbleModel(TestEquipment entity, ILocalizationWrapper localization)
        {
            _localization = localization;
            Entity = entity ?? new TestEquipment();
            RaisePropertyChanged();
        }

        public static TestEquipmentHumbleModel GetModelFor(TestEquipment entity, ILocalizationWrapper localization)
        {
            return entity != null ? new TestEquipmentHumbleModel(entity, localization) : null;
        }

        public long Id
        {
            get => Entity.Id.ToLong();
            set
            {
                Entity.Id = new TestEquipmentId(value);
                RaisePropertyChanged();
            }
        }

        public string SerialNumber
        {
            get => Entity.SerialNumber?.ToDefaultString();
            set
            {
                Entity.SerialNumber = new TestEquipmentSerialNumber(value);
                RaisePropertyChanged();
            }
        }

        public string InventoryNumber
        {
            get => Entity.InventoryNumber?.ToDefaultString();
            set
            {
                Entity.InventoryNumber = new TestEquipmentInventoryNumber(value);
                RaisePropertyChanged();
            }
        }

        public HelperTableItemModel<Status, string> Status
        {
            get => Entity.Status != null ? HelperTableItemModel.GetModelForStatus(Entity.Status) : null;
            set
            {
                Entity.Status = value?.Entity;
                RaisePropertyChanged();
            }
        }

        public TestEquipmentModelHumbleModel Model
        {
            get => Entity.TestEquipmentModel != null ? TestEquipmentModelHumbleModel.GetModelFor(Entity.TestEquipmentModel, _localization) : null;
            set
            {
                Entity.TestEquipmentModel = value?.Entity;
                RaisePropertyChanged();
            }
        }
        
        public bool TransferUserVisible => Entity.TestEquipmentModel.TransferUser;

        public bool TransferUser
        {
            get => Entity.TransferUser;
            set
            {
                Entity.TransferUser = value;
                RaisePropertyChanged();
            }
        }

        public bool TransferAdapterVisible => Entity.TestEquipmentModel.TransferAdapter;
        public bool TransferAdapter
        {
            get => Entity.TransferAdapter;
            set
            {
                Entity.TransferAdapter = value;
                RaisePropertyChanged();
            }
        }

        public bool TransferTransducerVisible => Entity.TestEquipmentModel.TransferTransducer;
        public bool TransferTransducer
        {
            get => Entity.TransferTransducer;
            set
            {
                Entity.TransferTransducer = value;
                RaisePropertyChanged();
            }
        }

        public bool AskForIdentVisible => Entity.TestEquipmentModel.AskForIdent;
        public TestEquipmentBehaviourAskForIdent AskForIdent
        {
            get => Entity.AskForIdent;
            set
            {
                Entity.AskForIdent = value;
                RaisePropertyChanged();
            }
        }

        public bool TransferCurvesVisible => Entity.TestEquipmentModel.TransferCurves;
        public TestEquipmentBehaviourTransferCurves TransferCurves
        {
            get => Entity.TransferCurves;
            set
            {
                Entity.TransferCurves = value;
                RaisePropertyChanged();
            }
        }

        public bool UseErrorCodesVisible => Entity.TestEquipmentModel.UseErrorCodes;
        public bool UseErrorCodes
        {
            get => Entity.UseErrorCodes;
            set
            {
                Entity.UseErrorCodes = value;
                RaisePropertyChanged();
            }
        }

        public bool AskForSignVisible => Entity.TestEquipmentModel.AskForSign;
        public bool AskForSign
        {
            get => Entity.AskForSign;
            set
            {
                Entity.AskForSign = value;
                RaisePropertyChanged();
            }
        }

        public bool DoLoseCheckVisible => Entity.TestEquipmentModel.DoLoseCheck;
        public bool DoLoseCheck
        {
            get => Entity.DoLoseCheck;
            set
            {
                Entity.DoLoseCheck = value;
                RaisePropertyChanged();
            }
        }

        public bool CanDeleteMeasurementsVisible => Entity.TestEquipmentModel.CanDeleteMeasurements;
        public bool CanDeleteMeasurements
        {
            get => Entity.CanDeleteMeasurements;
            set
            {
                Entity.CanDeleteMeasurements = value;
                RaisePropertyChanged();
            }
        }

        public bool CanUseQstStandardVisible => Entity.TestEquipmentModel.CanUseQstStandard;
        public bool CanUseQstStandard
        {
            get => Entity.CanUseQstStandard;
            set
            {
                Entity.CanUseQstStandard = value;
                RaisePropertyChanged();
            }
        }

        public bool ConfirmMeasurementsVisible => Entity.TestEquipmentModel.ConfirmMeasurements;
        public TestEquipmentBehaviourConfirmMeasurements ConfirmMeasurements
        {
            get => Entity.ConfirmMeasurements;
            set
            {
                Entity.ConfirmMeasurements = value;
                RaisePropertyChanged();
            }
        }

        public bool TransferLocationPicturesVisible => Entity.TestEquipmentModel.TransferLocationPictures;
        public bool TransferLocationPictures
        {
            get => Entity.TransferLocationPictures;
            set
            {
                Entity.TransferLocationPictures = value;
                RaisePropertyChanged();
            }
        }

        public bool TransferNewLimitsVisible => Entity.TestEquipmentModel.TransferNewLimits;
        public bool TransferNewLimits
        {
            get => Entity.TransferNewLimits;
            set
            {
                Entity.TransferNewLimits = value;
                RaisePropertyChanged();
            }
        }

        public bool TransferAttributesVisible => Entity.TestEquipmentModel.TransferAttributes;
        public bool TransferAttributes
        {
            get => Entity.TransferAttributes;
            set
            {
                Entity.TransferAttributes = value;
                RaisePropertyChanged();
            }
        }
        public bool UseForVisible => UseForCtlVisible || UseForRotVisible;
        public bool UseForCtlVisible => Entity.TestEquipmentModel.UseForCtl;
        public bool UseForCtl
        {
            get => Entity.UseForCtl;
            set
            {
                Entity.UseForCtl = value;
                RaisePropertyChanged();
            }
        }

        public bool UseForRotVisible => Entity.TestEquipmentModel.UseForRot;
        public bool UseForRot
        {
            get => Entity.UseForRot;
            set
            {
                Entity.UseForRot = value;
                RaisePropertyChanged();
            }
        }

        public string Version
        {
            get => Entity.Version?.ToDefaultString();
            set
            {
                Entity.Version = new TestEquipmentVersion(value);
                RaisePropertyChanged();
            }
        }

        public double CapacityMin
        {
            get => Entity.CapacityMin.Nm;
            set
            {
                Entity.CapacityMin = Torque.FromNm(value);
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(CapacityMax));
            }
        }

        public double CapacityMax
        {
            get => Entity.CapacityMax.Nm;
            set
            {
                Entity.CapacityMax = Torque.FromNm(value);
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(CapacityMin));
            }
        }

        public long CalibrationInterval
        {
            get => Entity.CalibrationInterval.IntervalValue;
            set
            {
                Entity.CalibrationInterval = new Interval()
                {
                    Type = IntervalType.EveryXDays,
                    IntervalValue = value
                };
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(NextCalibration));
            }
        }

        public DateTime? LastCalibration
        {
            get => Entity.LastCalibration;
            set
            {
                Entity.LastCalibration = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(NextCalibration));
            }
        }

        public string CalibrationNorm
        {
            get => Entity.CalibrationNorm?.ToDefaultString();
            set
            {
                Entity.CalibrationNorm = new CalibrationNorm(value);
                RaisePropertyChanged();
            }
        }

        public DateTime? NextCalibration => Entity.GetNextCalibrationDate();

        public DataGateVersion DataGateVersion => Entity.TestEquipmentModel.DataGateVersion;

        public bool FeaturesVisible => TransferOfVisible || TestBehaviorVisible;

        public bool TransferOfVisible => (TransferUserVisible || TransferAdapterVisible || TransferTransducerVisible ||
                                         TransferAttributesVisible || TransferLocationPicturesVisible ||
                                         TransferNewLimitsVisible || TransferCurvesVisible) && Entity.TestEquipmentModel.HasTransferAttributes();

        public bool TestBehaviorVisible => (AskForIdentVisible || AskForSignVisible || UseErrorCodesVisible ||
                                           DoLoseCheckVisible || CanDeleteMeasurementsVisible ||
                                           ConfirmMeasurementsVisible || CanUseQstStandardVisible) && Entity.TestEquipmentModel.HasTestBehavior();

        public string DriverProgramPath
        {
            get => Entity.TestEquipmentModel.DriverProgramPath.ToDefaultString();
            set
            {
                Entity.TestEquipmentModel.DriverProgramPath = new TestEquipmentSetupPath(value);
                RaisePropertyChanged(DriverProgramPath);
            }
        }
 
        public string CommunicationFilePath
        {
            get => Entity.TestEquipmentModel.CommunicationFilePath.ToDefaultString();
            set
            {
                Entity.TestEquipmentModel.CommunicationFilePath = new TestEquipmentSetupPath(value);
                RaisePropertyChanged(CommunicationFilePath);
            }
        }

        public bool Equals(TestEquipmentHumbleModel other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return this.EqualsById(other);
        }

        public TestEquipmentHumbleModel CopyDeep()
        {
            return new TestEquipmentHumbleModel(Entity.CopyDeep(), _localization);
        }


        public void UpdateWith(TestEquipment other)
        {
            Entity.UpdateWith(other);
            RaisePropertyChanged(null);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public bool EqualsById(TestEquipmentHumbleModel other)
        {
            return this.Entity.EqualsById(other?.Entity);
        }

        public bool EqualsByContent(TestEquipmentHumbleModel other)
        {
            return this.Entity.EqualsByContent(other?.Entity);
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
                    case TestEquipmentValidationError.SerialNumberIsEmpty:
                        return _localization.Strings.GetParticularString("TestEquipmentValidationError", "The serial number cannot be empty");
                        
                    case TestEquipmentValidationError.InventoryNumberIsEmpty:
                        return _localization.Strings.GetParticularString("TestEquipmentValidationError", "The inventory number cannot be empty");

                    case TestEquipmentValidationError.CapacityMaxLessThanCapacityMin:
                        return _localization.Strings.GetParticularString("TestEquipmentValidationError", "The capacity maximum has to be greater than or equal to capacity minimum");

                    case TestEquipmentValidationError.CapacityMinGreaterThanCapacityMax:
                        return _localization.Strings.GetParticularString("TestEquipmentValidationError", "The capacity minimum hast to be less than or equal to capacity maximum");

                }

                return "";
            }
        }
    }
}
