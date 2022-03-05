using System;
using Common.Types.Enums;
using Core;
using Core.Entities;
using Core.Enums;

namespace Server.Core.Entities
{
    public class TestEquipment : IQstEquality<TestEquipment>, ICopy<TestEquipment>
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
                && Alive.Equals(other.Alive)
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
                && (TestEquipmentModel?.EqualsByContent(other.TestEquipmentModel) ?? other.TestEquipmentModel == null)
                && (Status?.EqualsByContent(other.Status) ?? other.Status == null)
                && CapacityMin.Equals(other.CapacityMin)
                && CapacityMax.Equals(other.CapacityMax)
                && Version.Equals(other.Version)
                && CalibrationNorm.Equals(other.CalibrationNorm)
                && LastCalibration.GetValueOrDefault().Equals(other.LastCalibration.GetValueOrDefault())
                && (CalibrationInterval?.EqualsByContent(other.CalibrationInterval) ??
                    other.CalibrationInterval == null);
        }

        public bool EqualsById(TestEquipment other)
        {
            return this.Id.Equals(other?.Id);
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
            copy.Alive = Alive;
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
                : new Interval() { Type = CalibrationInterval.Type, IntervalValue = CalibrationInterval.IntervalValue };
            copy.CapacityMin = CapacityMin;
            copy.CapacityMax = CapacityMax;
            copy.CalibrationNorm = new CalibrationNorm(CalibrationNorm?.ToDefaultString());
            return copy;
        }

        public TestEquipmentId Id { get; set; }
        public TestEquipmentSerialNumber SerialNumber { get; set; }
        public TestEquipmentInventoryNumber InventoryNumber { get; set; }
        public TestEquipmentModel TestEquipmentModel { get; set; }
        public bool Alive { get; set; }
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
        public bool CanUseQstStandard { get; set; }
        public bool UseForCtl { get; set; }
        public Status Status { get; set; }
        public TestEquipmentVersion Version { get; set; }
        public DateTime? LastCalibration { get; set; }
        public Interval CalibrationInterval { get; set; }
        public CalibrationNorm CalibrationNorm { get; set; }
        public double? CapacityMin { get; set; }
        public double? CapacityMax { get; set; }
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
            return ToDefaultString().Equals(other?.ToDefaultString());
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
            return ToDefaultString().Equals(other?.ToDefaultString());
        }
    }
}
