using System;
using System.Collections.Generic;
using Common.Types.Enums;
using Core;
using Core.Entities;

namespace Server.Core.Entities
{
    public class TestEquipmentModelName : TypeCheckedString<MaxLength<CtInt20>, Blacklist<NewLines>, NoCheck>, IEquatable<TestEquipmentModelName>
    {
        public TestEquipmentModelName(string modelName)
            : base(modelName)
        {
        }

        public bool Equals(TestEquipmentModelName other)
        {
            return ToDefaultString().Equals(other?.ToDefaultString());
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
            return ToDefaultString().Equals(other?.ToDefaultString());
        }
    }

    public class TestEquipmentModelId : QstIdentifier, IEquatable<TestEquipmentModelId>
    {
        public TestEquipmentModelId(long value)
            : base(value)
        {
        }

        public bool Equals(TestEquipmentModelId other)
        {
            return Equals((QstIdentifier)other);
        }
    }

    public class TestEquipmentModel : IQstEquality<TestEquipmentModel>, ICopy<TestEquipmentModel>
    {
        public TestEquipmentModel()
        {
            TestEquipmentModelName = new TestEquipmentModelName("");
            CommunicationFilePath = new TestEquipmentSetupPath("");
            DriverProgramPath = new TestEquipmentSetupPath("");
            ResultFilePath = new TestEquipmentSetupPath("");
            StatusFilePath = new TestEquipmentSetupPath("");
        }

        public TestEquipmentModelId Id { get; set; }
        public TestEquipmentModelName TestEquipmentModelName { get; set; }
        public TestEquipmentSetupPath DriverProgramPath { get; set; }
        public TestEquipmentSetupPath CommunicationFilePath { get; set; }
        public TestEquipmentSetupPath ResultFilePath { get; set; }
        public TestEquipmentSetupPath StatusFilePath { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public TestEquipmentType Type { get; set; }
        public bool TransferUser { get; set; }
        public bool TransferAdapter { get; set; }
        public bool TransferTransducer { get; set; }
        public bool AskForIdent { get; set; }
        public bool TransferCurves { get; set; }
        public bool UseErrorCodes { get; set; }
        public bool AskForSign { get; set; }
        public bool DoLoseCheck { get; set; }
        public bool CanDeleteMeasurements { get; set; }
        public bool ConfirmMeasurements { get; set; }
        public bool TransferLocationPictures { get; set; }
        public bool TransferNewLimits { get; set; }
        public bool TransferAttributes { get; set; }
        public bool Alive { get; set; }
        public bool UseForCtl { get; set; }
        public bool UseForRot { get; set; }
        public bool CanUseQstStandard { get; set; }
        public long DataGateVersion { get; set; }

        public List<TestEquipment> TestEquipments { get; set; }

        public virtual bool EqualsByContent(TestEquipmentModel other)
        {
            if (other == null)
            {
                return false;
            }
            return
                Id.Equals(other.Id)
                && TestEquipmentModelName.Equals(other.TestEquipmentModelName)
                && DriverProgramPath.Equals(other.DriverProgramPath)
                && CommunicationFilePath.Equals(other.CommunicationFilePath)
                && ResultFilePath.Equals(other.ResultFilePath)
                && StatusFilePath.Equals(other.StatusFilePath)
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
                && Type.Equals(other.Type)
                && Alive.Equals(other.Alive)
                && UseForCtl.Equals(other.UseForCtl)
                && UseForRot.Equals(other.UseForRot)
                && CanUseQstStandard.Equals(other.CanUseQstStandard)
                && DataGateVersion.Equals(other.DataGateVersion)
                && (this.Manufacturer?.EqualsByContent(other.Manufacturer) ?? other.Manufacturer == null);
        }

        public bool EqualsById(TestEquipmentModel other)
        {
            return this.Id.Equals(other?.Id);
        }

        public TestEquipmentModel CopyDeep()
        {
            return new TestEquipmentModel
            {
                Id = new TestEquipmentModelId(Id.ToLong()),
                CommunicationFilePath = new TestEquipmentSetupPath(CommunicationFilePath?.ToDefaultString()),
                DriverProgramPath = new TestEquipmentSetupPath(DriverProgramPath?.ToDefaultString()),
                StatusFilePath = new TestEquipmentSetupPath(StatusFilePath?.ToDefaultString()),
                ResultFilePath = new TestEquipmentSetupPath(ResultFilePath?.ToDefaultString()),
                TestEquipmentModelName =
                    new TestEquipmentModelName(TestEquipmentModelName.ToDefaultString()),
                Manufacturer = Manufacturer?.CopyDeep(),
                Type = Type,
                TransferUser = TransferUser,
                TransferAdapter = TransferAdapter,
                TransferTransducer = TransferTransducer,
                AskForIdent = AskForIdent,
                TransferCurves = TransferCurves,
                UseErrorCodes = UseErrorCodes,
                AskForSign = AskForSign,
                DoLoseCheck = DoLoseCheck,
                CanDeleteMeasurements = CanDeleteMeasurements,
                ConfirmMeasurements = ConfirmMeasurements,
                TransferLocationPictures = TransferLocationPictures,
                TransferNewLimits = TransferNewLimits,
                TransferAttributes = TransferAttributes,
                Alive = Alive,
                DataGateVersion = DataGateVersion,
                UseForCtl = UseForCtl,
                UseForRot = UseForRot,
                CanUseQstStandard = CanUseQstStandard
            };
        }
    }
}
