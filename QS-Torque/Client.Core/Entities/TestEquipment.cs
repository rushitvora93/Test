 using System;
 using Common.Types.Enums;
 using Core.Enums;
 using Core.PhysicalValueTypes;

 namespace Core.Entities
{

    public class TestEquipment: IQstEquality<TestEquipment>, ICopy<TestEquipment>, IUpdate<TestEquipment>
    {
        public TestEquipment()
        {
            SerialNumber = new TestEquipmentSerialNumber("");
            InventoryNumber = new TestEquipmentInventoryNumber("");
            Version = new TestEquipmentVersion("");
            CalibrationNorm = new CalibrationNorm("");
        }

        public bool EqualsByContent(TestEquipment other)
        {
            if (other == null)
            {
                return false;
            }

            return
                Id.Equals(other.Id)
                && SerialNumber.Equals(other.SerialNumber)
                && InventoryNumber.Equals(other.InventoryNumber)
                && TransferUser.Equals(other.TransferUser)
                && TransferAdapter.Equals(other.TransferAdapter)
                && TransferTransducer.Equals(other.TransferTransducer)
                && AskForIdent.Equals(other.AskForIdent)
                && TransferCurves.Equals(other.TransferCurves)
                && UseErrorCodes.Equals(other.UseErrorCodes)
                && AskForSign.Equals(other.AskForSign)
                && DoLoseCheck.Equals(other.DoLoseCheck)
                && CanDeleteMeasurements.Equals(other.CanDeleteMeasurements)
                && ConfirmMeasurements.Equals(other.ConfirmMeasurements)
                && TransferLocationPictures.Equals(other.TransferLocationPictures)
                && TransferNewLimits.Equals(other.TransferNewLimits)
                && TransferAttributes.Equals(other.TransferAttributes)
                && UseForCtl.Equals(other.UseForCtl)
                && UseForRot.Equals(other.UseForRot)
                && CanUseQstStandard.Equals(other.CanUseQstStandard)
                && (TestEquipmentModel?.EqualsByContent(other.TestEquipmentModel) ?? other.TestEquipmentModel == null)
                && (Status?.EqualsByContent(other.Status) ?? other.Status == null)
                && (CapacityMin?.Equals(other.CapacityMin) ?? other.CapacityMin == null)
                && (CapacityMax?.Equals(other.CapacityMax) ?? other.CapacityMax == null)
                && (Version?.Equals(other.Version) ?? other.Version?.ToDefaultString() == null)
                && (CalibrationNorm?.Equals(other.CalibrationNorm) ?? other.CalibrationNorm == null)
                && LastCalibration.GetValueOrDefault().Equals(other.LastCalibration.GetValueOrDefault())
                && (CalibrationInterval?.EqualsByContent(other.CalibrationInterval) ??
                    other.CalibrationInterval == null);
        }

        public bool EqualsById(TestEquipment other)
        {
            return this.Id.Equals(other?.Id);
        }

        public string StatusFilePath()
        {
            return TestEquipmentModel?.StatusFilePath?.ToDefaultString();
        }

        public string ResultFilePath()
        {
            return TestEquipmentModel?.ResultFilePath?.ToDefaultString();
        }

        public DateTime? GetNextCalibrationDate()
        {
            if (LastCalibration == null)
            {
                return null;
            }

            if (CalibrationInterval == null)
            {
                return LastCalibration;
            }

            return LastCalibration.Value.AddDays(CalibrationInterval.IntervalValue);
        }

        public bool HasCapacity()
        {
            return TestEquipmentModel.Type == TestEquipmentType.Wrench;
        }

        public TestEquipment CopyDeep()
        {
            var copy = new TestEquipment();
            if (Id != null)
            {
                copy.Id = new TestEquipmentId(Id.ToLong());
            }
            copy.SerialNumber = new TestEquipmentSerialNumber(SerialNumber.ToDefaultString());
            copy.InventoryNumber = new TestEquipmentInventoryNumber(InventoryNumber.ToDefaultString());
            copy.TestEquipmentModel = TestEquipmentModel?.CopyDeep();
            copy.TransferUser = TransferUser;
            copy.TransferAdapter = TransferAdapter;
            copy.TransferTransducer = TransferTransducer;
            copy.AskForIdent = AskForIdent;
            copy.TransferCurves = TransferCurves;
            copy.UseErrorCodes = UseErrorCodes;
            copy.AskForSign = AskForSign;
            copy.DoLoseCheck = DoLoseCheck;
            copy.CanDeleteMeasurements = CanDeleteMeasurements;
            copy.ConfirmMeasurements = ConfirmMeasurements;
            copy.TransferLocationPictures = TransferLocationPictures;
            copy.TransferNewLimits = TransferNewLimits;
            copy.TransferAttributes = TransferAttributes;
            copy.UseForCtl = UseForCtl;
            copy.UseForRot = UseForRot;
            copy.CanUseQstStandard = CanUseQstStandard;
            copy.Status = Status?.CopyDeep();
            copy.Version = new TestEquipmentVersion(Version?.ToDefaultString());
            copy.LastCalibration = LastCalibration;
            copy.CalibrationInterval = CalibrationInterval == null
                ? null
                : new Interval() {Type = CalibrationInterval.Type, IntervalValue = CalibrationInterval.IntervalValue};
            copy.CapacityMin = CapacityMin == null ? null : Torque.FromNm(CapacityMin.Nm);
            copy.CapacityMax = CapacityMax == null ? null : Torque.FromNm(CapacityMax.Nm);
            copy.CalibrationNorm = new CalibrationNorm(CalibrationNorm?.ToDefaultString());
            return copy;
        }

        public void UpdateWith(TestEquipment other)
        {
            if (other == null)
            {
                return;
            }

            Id = other.Id;
            SerialNumber = other.SerialNumber;
            InventoryNumber = other.InventoryNumber;
            TestEquipmentModel.UpdateWith(other.TestEquipmentModel);
            TransferAdapter = other.TransferAdapter;
            TransferUser = other.TransferUser;
            TransferTransducer = other.TransferTransducer;
            AskForIdent = other.AskForIdent;
            TransferCurves = other.TransferCurves;
            UseErrorCodes = other.UseErrorCodes;
            AskForSign = other.AskForSign;
            DoLoseCheck = other.DoLoseCheck;
            CanDeleteMeasurements = other.CanDeleteMeasurements;
            ConfirmMeasurements = other.ConfirmMeasurements;
            TransferLocationPictures = other.TransferLocationPictures;
            TransferNewLimits = other.TransferNewLimits;
            TransferAttributes = other.TransferAttributes;
            UseForCtl = other.UseForCtl;
            UseForRot = other.UseForRot;
            CanUseQstStandard = other.CanUseQstStandard;
            Status = other.Status;
            Version = other.Version;
            LastCalibration = other.LastCalibration;
            CalibrationInterval = other.CalibrationInterval;
            CapacityMin = other.CapacityMin;
            CapacityMax = other.CapacityMax;
            CalibrationNorm = other.CalibrationNorm;
        }

        public TestEquipmentValidationError? Validate(string property)
        {
            switch (property)
            {
                case nameof(SerialNumber):
                    if (SerialNumber?.ToDefaultString() == null || SerialNumber.ToDefaultString() == "")
                    {
                        return TestEquipmentValidationError.SerialNumberIsEmpty;
                    }
                    break;
                case nameof(InventoryNumber):
                    if (InventoryNumber?.ToDefaultString() == null || InventoryNumber.ToDefaultString() == "")
                    {
                        return TestEquipmentValidationError.InventoryNumberIsEmpty;
                    }
                    break;

                case nameof(CapacityMax):
                    if (CapacityMax.Nm < CapacityMin.Nm)
                    {
                        return TestEquipmentValidationError.CapacityMaxLessThanCapacityMin;
                    }
                    break;
                case nameof(CapacityMin):
                    if (CapacityMax.Nm < CapacityMin.Nm)
                    {
                        return TestEquipmentValidationError.CapacityMinGreaterThanCapacityMax;
                    }
                    break;
            }

            return null;
        }


        public TestEquipmentId Id;
        public TestEquipmentSerialNumber SerialNumber;
        public TestEquipmentInventoryNumber InventoryNumber;
        public TestEquipmentModel TestEquipmentModel { get; set; }
        public bool TransferUser { get; set; }
        public bool TransferAdapter { get; set; }
        public bool TransferTransducer { get; set; }
        public TestEquipmentBehaviourAskForIdent AskForIdent { get; set; }
        public TestEquipmentBehaviourTransferCurves TransferCurves { get; set; }
        public bool UseErrorCodes { get; set; }
        public bool AskForSign { get; set; }
        public bool DoLoseCheck { get; set; }
        public bool CanDeleteMeasurements { get; set; }
        public TestEquipmentBehaviourConfirmMeasurements ConfirmMeasurements { get; set; }
        public bool TransferLocationPictures { get; set; }
        public bool TransferNewLimits { get; set; }
        public bool TransferAttributes { get; set; }
        public bool UseForRot { get; set; }
        public bool UseForCtl { get; set; }
        public bool CanUseQstStandard { get; set; }
        public Status Status { get; set; }
        public TestEquipmentVersion Version { get; set; }
        public DateTime? LastCalibration { get; set; }
        public Interval CalibrationInterval { get; set; }
        public CalibrationNorm CalibrationNorm { get; set; }
        public Torque CapacityMin { get; set; }
        public Torque CapacityMax { get; set; }
    }

    public class TestEquipmentId : QstIdentifier, IEquatable<TestEquipmentId>
    {
        public TestEquipmentId(long value)
            : base(value)
        {
        }

        public bool Equals(TestEquipmentId other)
        {
            return Equals((QstIdentifier)other);
        }
    }

    public class TestEquipmentSerialNumber : TypeCheckedString<MaxLength<CtInt20>, Blacklist<NewLines>, NoCheck>, IEquatable<TestEquipmentSerialNumber>
    {
        public TestEquipmentSerialNumber(string serialNumber)
            : base(serialNumber)
        {
        }

        public bool Equals(TestEquipmentSerialNumber other)
        {
            return ToDefaultString().Equals(other.ToDefaultString());
        }
    }

    public class CalibrationNorm : TypeCheckedString<MaxLength<CtInt30>, Blacklist<NewLines>, NoCheck>, IEquatable<CalibrationNorm>
    {
        public CalibrationNorm(string calibrationNorm)
            : base(calibrationNorm)
        {
        }

        public bool Equals(CalibrationNorm other)
        {
            return ToDefaultString().Equals(other.ToDefaultString());
        }
    }

    public class TestEquipmentVersion : TypeCheckedString<MaxLength<CtInt10>, Blacklist<NewLines>, NoCheck>, IEquatable<TestEquipmentVersion>
    {
        public TestEquipmentVersion(string version)
            : base(version)
        {
        }

        public bool Equals(TestEquipmentVersion other)
        {
            if (ToDefaultString() == null && other?.ToDefaultString() == null)
            {
                return true;
            }

            if (ToDefaultString() == null && other?.ToDefaultString() != null)
            {
                return false;
            }
            return ToDefaultString().Equals(other?.ToDefaultString());
        }
    }

    public class TestEquipmentInventoryNumber : TypeCheckedString<MaxLength<CtInt20>, Blacklist<NewLines>, NoCheck>, IEquatable<TestEquipmentSerialNumber>
    {
        public TestEquipmentInventoryNumber(string inventoryNumber)
            : base(inventoryNumber)
        {
        }

        public bool Equals(TestEquipmentSerialNumber other)
        {
            return ToDefaultString().Equals(other.ToDefaultString());
        }
    }

    public class TestEquipmentSetupPath : TypeCheckedString<MaxLength<CtInt250>, Blacklist<NewLines>, NoCheck>, IEquatable<TestEquipmentSetupPath>
    {
        public TestEquipmentSetupPath(string path)
            : base(path)
        {
        }

        public bool Equals(TestEquipmentSetupPath other)
        {
            return ToDefaultString().Equals(other.ToDefaultString());
        }
    }

    public enum TestEquipmentValidationError
    {
        SerialNumberIsEmpty,
        InventoryNumberIsEmpty,
        CapacityMaxLessThanCapacityMin,
        CapacityMinGreaterThanCapacityMax,
    }
}
