using System.Collections.Generic;
using BasicTypes;
using Common.Types.Enums;
using Core.Entities;
using Core.Enums;
using DtoTypes;
using Server.Core.Entities;
using Server.Core.Enums;
using TransferToTestEquipmentService;
using ClassicChkTest = Server.Core.Entities.ClassicChkTest;
using ClassicChkTestValue = Server.Core.Entities.ClassicChkTestValue;
using ClassicMfuTest = Server.Core.Entities.ClassicMfuTest;
using ClassicMfuTestValue = Server.Core.Entities.ClassicMfuTestValue;
using ClassicProcessTestValue = Server.Core.Entities.ClassicProcessTestValue;
using DateTime = System.DateTime;
using HelperTableEntity = Server.Core.Entities.HelperTableEntity;
using Location = Server.Core.Entities.Location;
using LocationDirectory = Server.Core.Entities.LocationDirectory;
using LocationToolAssignment = Server.Core.Entities.LocationToolAssignment;
using Manufacturer = Server.Core.Entities.Manufacturer;
using QstSetup = Server.Core.Entities.QstSetup;
using Status = Server.Core.Entities.Status;
using TestEquipment = Server.Core.Entities.TestEquipment;
using TestEquipmentModel = Server.Core.Entities.TestEquipmentModel;
using TestParameters = Server.Core.Entities.TestParameters;
using TestTechnique = Server.Core.Entities.TestTechnique;
using ToleranceClass = Server.Core.Entities.ToleranceClass;
using Tool = Server.Core.Entities.Tool;
using ToolModel = Server.Core.Entities.ToolModel;
using ToolUsage = Server.Core.Entities.ToolUsage;
using Extension = Server.Core.Entities.Extension;

namespace FrameworksAndDrivers.NetworkView.Test
{
    static class EqualityChecker
    {
        public static bool ArePrimitiveDateTimeAndDtoEqual(DateTime primitive, BasicTypes.DateTime dto)
        {
            return primitive.Ticks == dto.Ticks;
        }

        public static bool CompareLocationDtoWithLocation(DtoTypes.Location dto, Location entity)
        {
            return dto.Id == entity.Id.ToLong() &&
                   dto.Number == entity.Number.ToDefaultString() &&
                   dto.Description == entity.Description.ToDefaultString() &&
                   dto.ParentDirectoryId == entity.ParentDirectoryId.ToLong() &&
                   dto.ConfigurableField1.Value == entity.ConfigurableField1.ToDefaultString() &&
                   dto.ConfigurableField2.Value == entity.ConfigurableField2.ToDefaultString() &&
                   dto.ConfigurableField3 == entity.ConfigurableField3 &&
                   dto.ControlledBy == (long) entity.ControlledBy &&
                   CompareToleranceClassDtoWithToleranceClass(dto.ToleranceClass1, entity.ToleranceClass1) &&
                   CompareToleranceClassDtoWithToleranceClass(dto.ToleranceClass2, entity.ToleranceClass2) &&
                   dto.SetPoint1 == entity.SetPoint1 &&
                   dto.SetPoint2 == entity.SetPoint2 &&
                   dto.Maximum1 == entity.Maximum1 &&
                   dto.Maximum2 == entity.Maximum2 &&
                   dto.Minimum1 == entity.Minimum1 &&
                   dto.Minimum2 == entity.Minimum2 &&
                   dto.Threshold1 == entity.Threshold1 &&
                   dto.Comment.Value == entity.Comment &&
                   dto.Alive == entity.Alive;
        }

        public static bool CompareLocationDirectoryDtoWithLocationDirectory(DtoTypes.LocationDirectory dto,
            LocationDirectory entity)
        {
            return dto.Id == entity.Id.ToLong() &&
                   dto.Name == entity.Name.ToDefaultString() &&
                   dto.ParentId == entity.ParentId.ToLong() &&
                   dto.Alive == entity.Alive;
        }


        public static bool CompareStatusDtoWithStatus(DtoTypes.Status statusDto, Status status)
        {
            if (statusDto == null && status == null)
            {
                return true;
            }

            if (statusDto == null || status == null)
            {
                return false;
            }

            return status.Id.ToLong() == statusDto.Id &&
                   status.Value.ToDefaultString() == statusDto.Description &&
                   status.Alive == statusDto.Alive;
        }

        public static bool CompareIntervalDtoToInterval(BasicTypes.Interval dto, Core.Entities.Interval entity)
        {
            if (dto == null && entity == null)
            {
                return true;
            }

            if (dto == null || entity == null)
            {
                return false;
            }

            return dto.IntervalValue == entity.IntervalValue && dto.IntervalType == (long)entity.Type;
        }

        public static bool CompareToolDtoWithTool(DtoTypes.Tool dto, Tool entity)
        {
            return dto.Id == entity.Id.ToLong() &&
                   dto.Accessory.Value == entity.Accessory &&
                   dto.AdditionalConfigurableField1.Value == entity.AdditionalConfigurableField1.ToDefaultString() &&
                   dto.AdditionalConfigurableField2.Value == entity.AdditionalConfigurableField2.ToDefaultString() &&
                   dto.AdditionalConfigurableField3.Value == entity.AdditionalConfigurableField3.ToDefaultString() &&
                   dto.Comment.Value == entity.Comment &&
                   dto.ConfigurableField?.ListId == entity.ConfigurableField?.ListId?.ToLong() &&
                   dto.ConfigurableField?.Value == entity.ConfigurableField?.Value?.ToDefaultString() &&
                   dto.CostCenter?.ListId == entity.CostCenter?.ListId?.ToLong() &&
                   dto.CostCenter?.Value == entity.CostCenter?.Value?.ToDefaultString() &&
                   dto.InventoryNumber == entity.InventoryNumber &&
                   dto.SerialNumber == entity.SerialNumber &&
                   CompareToolModelDtoWithToolModel(dto.ToolModel, entity.ToolModel) &&
                   CompareStatusDtoWithStatus(dto.Status, entity.Status);
        }

        private static bool CompareNullableDouble(NullableDouble dto, double? entity)
        {
            if (dto.IsNull && entity == null)
            {
                return true;
            }
            if(dto.IsNull && entity.HasValue == false)
            {
                return true;
            }

            return dto.Value == entity;
        }

        private static bool CompareNullableInt(NullableInt dto, int? entity)
        {
            if (dto.IsNull && entity == null)
            {
                return true;
            }
            if (dto.IsNull && entity.HasValue == false)
            {
                return true;
            }

            return dto.Value == entity;
        }

        private static bool CompareNullableLong(NullableLong dto, long? entity)
        {
            if (dto.IsNull && entity == null)
            {
                return true;
            }
            if (dto.IsNull && entity.HasValue == false)
            {
                return true;
            }

            return dto.Value == entity;
        }

        private static bool CompareNullableString(NullableString dto, string entity)
        {
            if (dto.IsNull && entity == null)
            {
                return true;
            }

            return dto.Value == entity;
        }

        private static bool CompareNullableDate(BasicTypes.NullableDateTime dto, DateTime? entity)
        {
            if (dto.IsNull && entity == null)
            {
                return true;
            }
            if (dto.IsNull && entity.HasValue == false)
            {
                return true;
            }

            return ArePrimitiveDateTimeAndDtoEqual(entity.Value, dto.Value);
        }

        public static bool CompareToolModelDtoWithToolModel(DtoTypes.ToolModel dto, ToolModel entity)
        {
            if (dto == null && entity == null)
            {
                return true;
            }

            if (dto == null || entity == null)
            {
                return false;
            }

            return
                dto.Id == entity.Id.ToLong() &&
                dto.Description == entity.Description &&
                dto.ModelType == (long)entity.ModelType &&
                dto.Class == (long)entity.Class &&
                CompareManufacturerDtoWithManufacturer(dto.Manufacturer, entity.Manufacturer) &&
                CompareNullableDouble(dto.MinPower, entity.MinPower) &&
                CompareNullableDouble(dto.MaxPower, entity.MaxPower) &&
                CompareNullableDouble(dto.AirPressure, entity.AirPressure) &&
                CompareHelperTableEntityDtoWithHelperTableEntity(dto.ToolType, entity.ToolType) &&
                dto.Weight == entity.Weight &&
                CompareNullableDouble(dto.BatteryVoltage, entity.BatteryVoltage) &&
                CompareNullableLong(dto.MaxRotationSpeed, entity.MaxRotationSpeed) &&
                CompareNullableDouble(dto.AirConsumption, entity.AirConsumption) &&
                CompareHelperTableEntityDtoWithHelperTableEntity(dto.SwitchOff, entity.SwitchOff) &&
                CompareHelperTableEntityDtoWithHelperTableEntity(dto.DriveSize, entity.DriveSize) &&
                CompareHelperTableEntityDtoWithHelperTableEntity(dto.ShutOff, entity.ShutOff) &&
                CompareHelperTableEntityDtoWithHelperTableEntity(dto.DriveType, entity.DriveType) &&
                CompareHelperTableEntityDtoWithHelperTableEntity(dto.ConstructionType, entity.ConstructionType) &&
                // picture
                dto.CmLimit == entity.CmLimit &&
                dto.CmkLimit == entity.CmkLimit &&
                dto.Alive == entity.Alive;
        }

        public static bool CompareTestTechniqueDtoWithTestTechnique(DtoTypes.TestTechnique dto, TestTechnique entity)
        {
            return entity.CycleComplete == dto.CycleComplete &&
                   entity.CycleStart == dto.CycleStart &&
                   entity.EndCycleTime == dto.EndCycleTime &&
                   entity.FilterFrequency == dto.FilterFrequency &&
                   entity.MaximumPulse == dto.MaximumPulse &&
                   entity.MeasureDelayTime == dto.MeasureDelayTime &&
                   entity.MinimumPulse == dto.MinimumPulse &&
                   entity.MustTorqueAndAngleBeInLimits == dto.MustTorqueAndAngleBeInLimits &&
                   entity.ResetTime == dto.ResetTime &&
                   entity.SlipTorque == dto.SlipTorque &&
                   entity.StartFinalAngle == dto.StartFinalAngle &&
                   entity.Threshold == dto.Threshold &&
                   entity.TorqueCoefficient == dto.TorqueCoefficient;
        }

        public static bool CompareTestParametersDtoWithTestParameters(DtoTypes.TestParameters dto, TestParameters entity)
        {
            return entity.Minimum1 == dto.Minimum1 &&
                   entity.Maximum1 == dto.Maximum1 &&
                   entity.SetPoint1 == dto.SetPoint1 &&
                   entity.Threshold1 == dto.Threshold1 &&
                   entity.Minimum2 == dto.Minimum2 &&
                   entity.Maximum2 == dto.Maximum2 &&
                   entity.SetPoint2 == dto.SetPoint2 &&
                   entity.ControlledBy == (LocationControlledBy) dto.ControlledBy;
        }

        public static bool CompareToleranceClassDtoWithToleranceClass(DtoTypes.ToleranceClass dtoToleranceClass, ToleranceClass toleranceClass)
        {
            return dtoToleranceClass.Id == toleranceClass.Id.ToLong() &&
                   dtoToleranceClass.Name == toleranceClass.Name &&
                   dtoToleranceClass.Relative == toleranceClass.Relative &&
                   dtoToleranceClass.LowerLimit == toleranceClass.LowerLimit &&
                   dtoToleranceClass.UpperLimit == toleranceClass.UpperLimit &&
                   dtoToleranceClass.Alive == toleranceClass.Alive;
        }

        public static bool CompareToolUsageDtoWithToolUsage(DtoTypes.ToolUsage dtoToolUsage, ToolUsage toolUsage)
        {
            return dtoToolUsage.Id == toolUsage.Id.ToLong() && dtoToolUsage.Alive == toolUsage.Alive &&
                   dtoToolUsage.Description == toolUsage.Description.ToDefaultString();
        }


        public static bool CompareLocationToolAssignmentsForTransferDtoWithLocationToolAssignmentForTransfer(TransferToTestEquipmentService.LocationToolAssignmentForTransfer dto, Server.Core.Entities.LocationToolAssignmentForTransfer entity)
        {
            return entity.LocationToolAssignmentId.ToLong() == dto.LocationToolAssignmentId
                    && entity.ToolUsageId.ToLong() == dto.ToolUsageId
                    && entity.ToolUsageDescription.ToDefaultString() == dto.ToolUsageDescription
                    && entity.LocationId.ToLong() == dto.LocationId
                    && entity.LocationNumber.ToDefaultString() == dto.LocationNumber
                    && entity.LocationDescription.ToDefaultString() == dto.LocationDescription
                    && entity.LocationFreeFieldCategory == dto.LocationFreeFieldCategory
                    && entity.LocationFreeFieldDocumentation == dto.LocationFreeFieldDocumentation
                    && entity.ToolId.ToLong() == dto.ToolId
                    && entity.ToolSerialNumber == dto.ToolSerialNumber
                    && entity.ToolInventoryNumber == dto.ToolInventoryNumber
                    && EqualityChecker.ArePrimitiveDateTimeAndDtoEqual(entity.NextTestDate.Value, dto.NextTestDate.Value)
                    && EqualityChecker.ArePrimitiveDateTimeAndDtoEqual(entity.LastTestDate.Value, dto.LastTestDate.Value)
                    && dto.NextTestDateShift.IsNull ? entity.NextTestDateShift == null : entity.NextTestDateShift.Value == (Shift)dto.NextTestDateShift.Value
                    && CompareIntervalDtoToInterval(dto.TestInterval, entity.TestInterval)
                    && entity.SampleNumber == dto.SampleNumber;
        }

        public static bool AreLocationToolAssignmentEntityAndDtoEqual(LocationToolAssignment entity, DtoTypes.LocationToolAssignment dto)
        {
            return entity.Id.ToLong() == dto.Id &&
                   CompareLocationDtoWithLocation(dto.AssignedLocation, entity.AssignedLocation) &&
                   CompareToolDtoWithTool(dto.AssignedTool, entity.AssignedTool) &&
                   CompareTestTechniqueDtoWithTestTechnique(dto.TestTechnique, entity.TestTechnique) &&
                   CompareTestParametersDtoWithTestParameters(dto.TestParameters, entity.TestParameters) &&
                   CompareToleranceClassDtoWithToleranceClass(dto.TestParameters.ToleranceClass1, entity.TestParameters.ToleranceClass1) &&
                   CompareToleranceClassDtoWithToleranceClass(dto.TestParameters.ToleranceClass2, entity.TestParameters.ToleranceClass2) &&
                   CompareToolUsageDtoWithToolUsage(dto.ToolUsage, entity.ToolUsage) && entity.TestLevelNumberChk == dto.TestLevelNumberChk &&
                   ArePrimitiveDateTimeAndDtoEqual(entity.NextTestDateMfu.Value, dto.NextTestDateMfu.Value) &&
                   ArePrimitiveDateTimeAndDtoEqual(entity.NextTestDateChk.Value, dto.NextTestDateChk.Value) &&
                   (int)entity.NextTestShiftMfu == dto.NextTestShiftMfu.Value &&
                   (int)entity.NextTestShiftChk == dto.NextTestShiftChk.Value &&
                   entity.TestLevelSetMfu.Id.ToLong() == dto.TestLevelSetMfu.Id &&
                   entity.TestLevelSetMfu.Name.ToDefaultString() == dto.TestLevelSetMfu.Name &&
                   entity.TestLevelNumberMfu == dto.TestLevelNumberMfu &&
                   entity.TestLevelSetChk.Id.ToLong() == dto.TestLevelSetChk.Id &&
                   entity.TestLevelSetChk.Name.ToDefaultString() == dto.TestLevelSetChk.Name;
        }

        public static bool CompareClassicChkTestDtoWithClassicChkTest(ClassicChkTest entity, DtoTypes.ClassicChkTest dto)
        {
            return entity.Id.ToLong() == dto.Id &&
                   ArePrimitiveDateTimeAndDtoEqual(entity.Timestamp, dto.Timestamp) &&
                   entity.NumberOfTests == dto.NumberOfTests &&
                   entity.LowerLimitUnit1 == dto.LowerLimitUnit1 &&
                   entity.NominalValueUnit1 == dto.NominalValueUnit1 &&
                   entity.UpperLimitUnit1 == dto.UpperLimitUnit1 &&
                   entity.Unit1Id == (MeaUnit)dto.Unit1Id &&
                   entity.LowerLimitUnit2 == dto.LowerLimitUnit2 &&
                   entity.NominalValueUnit2 == dto.NominalValueUnit2 &&
                   entity.UpperLimitUnit2 == dto.UpperLimitUnit2 &&
                   entity.Unit2Id == (MeaUnit)dto.Unit2Id &&
                   entity.TestValueMinimum == dto.TestValueMinimum &&
                   entity.TestValueMaximum == dto.TestValueMaximum &&
                   entity.Average == dto.Average &&
                   entity.StandardDeviation.Value == dto.StandardDeviation.Value &&
                   entity.ControlledByUnitId == (MeaUnit)dto.ControlledByUnitId &&
                   entity.ThresholdTorque == dto.ThresholdTorque &&
                   entity.SensorSerialNumber == dto.SensorSerialNumber.Value &&
                   entity.Result.LongValue == dto.Result &&
                   entity.ToleranceClassUnit1.Id.ToLong() == dto.ToleranceClassUnit1 &&
                   entity.ToleranceClassUnit2.Id.ToLong() == dto.ToleranceClassUnit2 &&
                   entity.TestLocation.LocationId.ToLong() == dto.TestLocation.LocationId &&
                   entity.TestLocation.LocationDirectoryId.ToLong() == dto.TestLocation.LocationDirectoryId &&
                   entity.LocationToolAssignmentId.ToLong() == dto.LocationToolAssignmentId &&
                   CompareTestEquipmentWithTestEquipmentDto(entity.TestEquipment, dto.TestEquipment);
        }

        public static bool CompareClassicMfuTestDtoWithClassicChkTest(ClassicMfuTest entity, DtoTypes.ClassicMfuTest dto)
        {
            return entity.Id.ToLong() == dto.Id &&
                   ArePrimitiveDateTimeAndDtoEqual(entity.Timestamp, dto.Timestamp) &&
                   entity.NumberOfTests == dto.NumberOfTests &&
                   entity.LowerLimitUnit1 == dto.LowerLimitUnit1 &&
                   entity.NominalValueUnit1 == dto.NominalValueUnit1 &&
                   entity.UpperLimitUnit1 == dto.UpperLimitUnit1 &&
                   entity.Unit1Id == (MeaUnit)dto.Unit1Id &&
                   entity.LowerLimitUnit2 == dto.LowerLimitUnit2 &&
                   entity.NominalValueUnit2 == dto.NominalValueUnit2 &&
                   entity.UpperLimitUnit2 == dto.UpperLimitUnit2 &&
                   entity.Unit2Id == (MeaUnit)dto.Unit2Id &&
                   entity.TestValueMinimum == dto.TestValueMinimum &&
                   entity.Cmk == dto.Cmk &&
                   entity.Cm == dto.Cm &&
                   entity.TestValueMaximum == dto.TestValueMaximum &&
                   entity.Average == dto.Average &&
                   entity.StandardDeviation.Value == dto.StandardDeviation.Value &&
                   entity.ControlledByUnitId == (MeaUnit)dto.ControlledByUnitId &&
                   entity.ThresholdTorque == dto.ThresholdTorque &&
                   entity.SensorSerialNumber == dto.SensorSerialNumber.Value &&
                   entity.Result.LongValue == dto.Result &&
                   entity.ToleranceClassUnit1.Id.ToLong() == dto.ToleranceClassUnit1 &&
                   entity.ToleranceClassUnit2.Id.ToLong() == dto.ToleranceClassUnit2 &&
                   entity.TestLocation.LocationId.ToLong() == dto.TestLocation.LocationId &&
                   entity.TestLocation.LocationDirectoryId.ToLong() == dto.TestLocation.LocationDirectoryId &&
                   entity.LocationToolAssignmentId.ToLong() == dto.LocationToolAssignmentId &&
                   CompareTestEquipmentWithTestEquipmentDto(entity.TestEquipment, dto.TestEquipment);
        }

        public static bool CompareTestEquipmentModelWithTestEquipmentModel(TestEquipmentModel entity, DtoTypes.TestEquipmentModel dto)
        {
            return entity.TestEquipmentModelName.ToDefaultString() == dto.TestEquipmentModelName &&
                   CompareNullableString(dto.CommunicationFilePath, entity.CommunicationFilePath?.ToDefaultString()) &&
                   CompareNullableString(dto.DriverProgramPath, entity.DriverProgramPath?.ToDefaultString()) &&
                   CompareManufacturerDtoWithManufacturer(dto.Manufacturer, entity.Manufacturer) &&
                   dto.TransferUser == entity.TransferUser &&
                   dto.TransferAdapter == entity.TransferAdapter &&
                   dto.TransferTransducer == entity.TransferTransducer &&
                   dto.AskForIdent == entity.AskForIdent &&
                   dto.TransferCurves == entity.TransferCurves &&
                   dto.UseErrorCodes == entity.UseErrorCodes &&
                   dto.AskForSign == entity.AskForSign &&
                   dto.DoLoseCheck == entity.DoLoseCheck &&
                   dto.CanDeleteMeasurements == entity.CanDeleteMeasurements &&
                   dto.ConfirmMeasurements == entity.ConfirmMeasurements &&
                   dto.TransferLocationPictures == entity.TransferLocationPictures &&
                   dto.TransferNewLimits == entity.TransferNewLimits &&
                   dto.TransferAttributes == entity.TransferAttributes &&
                   dto.Alive == entity.Alive &&
                   dto.DataGateVersion == entity.DataGateVersion &&
                   dto.UseForCtl == entity.UseForCtl &&
                   dto.UseForRot == entity.UseForRot &&
                   dto.CanUseQstStandard == entity.CanUseQstStandard &&
                   dto.Type == (long)entity.Type;
        }

        public static bool CompareTestEquipmentWithTestEquipmentDto(TestEquipment entity, DtoTypes.TestEquipment dto)
        {
            return entity.Id.ToLong() == dto.Id &&
                   CompareNullableString(dto.InventoryNumber, entity.InventoryNumber?.ToDefaultString()) &&
                   CompareNullableString(dto.SerialNumber, entity.SerialNumber?.ToDefaultString()) &&
                   CompareTestEquipmentModelWithTestEquipmentModel(entity.TestEquipmentModel, dto.TestEquipmentModel) &&
                   dto.TransferUser == entity.TransferUser &&
                   dto.TransferAdapter == entity.TransferAdapter &&
                   dto.TransferTransducer == entity.TransferTransducer &&
                   dto.AskForIdent == (long)entity.AskForIdent &&
                   dto.TransferCurves == (long)entity.TransferCurves &&
                   dto.UseErrorCodes == entity.UseErrorCodes &&
                   dto.AskForSign == entity.AskForSign &&
                   dto.DoLoseCheck == entity.DoLoseCheck &&
                   dto.CanDeleteMeasurements == entity.CanDeleteMeasurements &&
                   dto.ConfirmMeasurements == (long)entity.ConfirmMeasurements &&
                   dto.TransferLocationPictures == entity.TransferLocationPictures &&
                   dto.TransferNewLimits == entity.TransferNewLimits &&
                   dto.TransferAttributes == entity.TransferAttributes &&
                   dto.Alive == entity.Alive &&
                   dto.UseForCtl == entity.UseForCtl &&
                   dto.UseForRot == entity.UseForRot &&
                   dto.CanUseQstStandard == entity.CanUseQstStandard &&
                   CompareNullableString(dto.Version, entity.Version?.ToDefaultString()) &&
                   CompareStatusDtoWithStatus(dto.Status, entity.Status) &&
                   dto.CapacityMin == entity.CapacityMin &&
                   dto.CapacityMax == entity.CapacityMax &&
                   CompareNullableString(dto.Version, entity.Version?.ToDefaultString()) &&
                   CompareNullableDate(dto.LastCalibration, entity.LastCalibration) &&
                   CompareNullableString(dto.CalibrationNorm, entity.CalibrationNorm?.ToDefaultString()) &&
                   CompareIntervalDtoToInterval(dto.CalibrationInterval, entity.CalibrationInterval);
        }

        public static bool CompareManufacturerDtoWithManufacturer(DtoTypes.Manufacturer dtoManufacturer, Manufacturer manufacturer)
        {
            if (dtoManufacturer == null && manufacturer == null)
            {
                return true;
            }

            if (dtoManufacturer == null || manufacturer == null)
            {
                return false;
            }

            return manufacturer.Id.ToLong() == dtoManufacturer.ManufacturerId &&
                   manufacturer.Name.ToDefaultString() == dtoManufacturer.ManufactuerName.Value &&
                   manufacturer.Street == dtoManufacturer.Street.Value &&
                   manufacturer.HouseNumber == dtoManufacturer.HouseNumber.Value &&
                   manufacturer.City == dtoManufacturer.City.Value &&
                   manufacturer.Plz == dtoManufacturer.ZipCode.Value &&
                   manufacturer.PhoneNumber == dtoManufacturer.PhoneNumber.Value &&
                   manufacturer.Fax == dtoManufacturer.Fax.Value &&
                   manufacturer.Person == dtoManufacturer.Person.Value &&
                   manufacturer.Country == dtoManufacturer.Country.Value &&
                   manufacturer.Comment == dtoManufacturer.Comment.Value &&
                   manufacturer.Alive == dtoManufacturer.Alive;
        }

        public static bool CompareClassicChkTestValueDtoWithClassicChkTestValue(ClassicChkTestValue entity, DtoTypes.ClassicChkTestValue dto)
        {
            return entity.Id.ToLong() == dto.Id &&
                   entity.Position == dto.Position &&
                   entity.ValueUnit1 == dto.ValueUnit1 &&
                   entity.ValueUnit2 == dto.ValueUnit2;
        }

        public static bool CompareClassicMfuTestValueDtoWithClassicMfuTestValue(ClassicMfuTestValue entity, DtoTypes.ClassicMfuTestValue dto)
        {
            return entity.Id.ToLong() == dto.Id &&
                   entity.Position == dto.Position &&
                   entity.ValueUnit1 == dto.ValueUnit1 &&
                   entity.ValueUnit2 == dto.ValueUnit2;
        }

        public static bool CompareHelperTableEntityDtoWithHelperTableEntity(DtoTypes.HelperTableEntity dto, HelperTableEntity entity)
        {
            return dto.ListId == entity.ListId.ToLong() &&
                   dto.Value == entity.Value.ToDefaultString() &&
                   dto.Alive == entity.Alive &&
                   dto.NodeId == (long)entity.NodeId;
        }

        public static bool CompareQstSetupDtoWithQstSetup(DtoTypes.QstSetup dtoSetup, QstSetup setup)
        {
            return dtoSetup.Id == setup.Id.ToLong() &&
                   dtoSetup.Name == setup.Name.ToDefaultString() &&
                   dtoSetup.Value == setup.Value.ToDefaultString();
        }

        public static bool CompareClassicChkTestWithDto(ClassicChkTest entity, DtoTypes.ClassicChkTest dto)
        {
            return entity.Id.ToLong() == dto.Id &&
                   entity.Average == dto.Average &&
                   entity.ControlledByUnitId == (MeaUnit) dto.ControlledByUnitId &&
                   entity.LowerLimitUnit1 == dto.LowerLimitUnit1 &&
                   entity.LowerLimitUnit2 == dto.LowerLimitUnit2 &&
                   entity.NominalValueUnit1 == dto.NominalValueUnit1 &&
                   entity.NominalValueUnit2 == dto.NominalValueUnit2 &&
                   entity.NumberOfTests == dto.NumberOfTests &&
                   entity.Result.LongValue == dto.Result &&
                   entity.SensorSerialNumber == dto.SensorSerialNumber.Value &&
                   entity.StandardDeviation == dto.StandardDeviation.Value &&
                   entity.TestEquipment.Id.ToLong() == dto.TestEquipment.Id &&
                   entity.TestLocation.LocationId.ToLong() == dto.TestLocation.LocationId &&
                   entity.TestValueMaximum == dto.TestValueMaximum &&
                   entity.TestValueMinimum == dto.TestValueMinimum &&
                   entity.User.UserId.ToLong() == dto.User.UserId &&
                   entity.ThresholdTorque == dto.ThresholdTorque &&
                   ArePrimitiveDateTimeAndDtoEqual(entity.Timestamp, dto.Timestamp) &&
                   entity.ToolId == dto.ToolId &&
                   entity.Unit1Id == (MeaUnit) dto.Unit1Id &&
                   entity.Unit2Id == (MeaUnit) dto.Unit2Id &&
                   entity.UpperLimitUnit1 == dto.UpperLimitUnit1 &&
                   entity.UpperLimitUnit2 == dto.UpperLimitUnit2 &&
                   entity.ToleranceClassUnit1.Id.ToLong() == dto.ToleranceClassUnit1 &&
                   entity.ToleranceClassUnit2.Id.ToLong() == dto.ToleranceClassUnit2 &&
                   CompareTestValuesWithTestValuesDto(entity.TestValues, dto.TestValues);
        }

        private static bool CompareTestValuesWithTestValuesDto(List<ClassicChkTestValue> entityTestValues, ListOfClassicChkTestValue dtoTestValues)
        {
            if (entityTestValues.Count != dtoTestValues.ClassicChkTestValues.Count)
            {
                return false;
            }

            var i = 0;
            foreach (var val in entityTestValues)
            {
                if (!CompareTestValueWithTestValueDto(val, dtoTestValues.ClassicChkTestValues[i]))
                {
                    return false;
                }
                i++;
            }

            return true;
        }

        private static bool CompareTestValueWithTestValueDto(ClassicChkTestValue entity, DtoTypes.ClassicChkTestValue dto)
        {
            return entity.Id.ToLong() == dto.Id &&
                   entity.Position == dto.Position &&
                   entity.ValueUnit1 == dto.ValueUnit1 &&
                   entity.ValueUnit2 == dto.ValueUnit2;
        }

        public static bool CompareClassicMfuTestWithDto(ClassicMfuTest entity, DtoTypes.ClassicMfuTest dto)
        {
            return entity.Id.ToLong() == dto.Id &&
                   entity.Average == dto.Average &&
                   entity.ControlledByUnitId == (MeaUnit)dto.ControlledByUnitId &&
                   entity.LowerLimitUnit1 == dto.LowerLimitUnit1 &&
                   entity.LowerLimitUnit2 == dto.LowerLimitUnit2 &&
                   entity.NominalValueUnit1 == dto.NominalValueUnit1 &&
                   entity.NominalValueUnit2 == dto.NominalValueUnit2 &&
                   entity.Cm == dto.Cm &&
                   entity.Cmk == dto.Cmk &&
                   entity.LimitCm == dto.LimitCm &&
                   entity.LimitCmk == dto.LimitCmk &&
                   entity.NumberOfTests == dto.NumberOfTests &&
                   entity.Result.LongValue == dto.Result &&
                   entity.SensorSerialNumber == dto.SensorSerialNumber.Value &&
                   entity.StandardDeviation == dto.StandardDeviation.Value &&
                   entity.TestEquipment.Id.ToLong() == dto.TestEquipment.Id &&
                   entity.TestLocation.LocationId.ToLong() == dto.TestLocation.LocationId &&
                   entity.TestValueMaximum == dto.TestValueMaximum &&
                   entity.TestValueMinimum == dto.TestValueMinimum &&
                   entity.User.UserId.ToLong() == dto.User.UserId &&
                   entity.ThresholdTorque == dto.ThresholdTorque &&
                   ArePrimitiveDateTimeAndDtoEqual(entity.Timestamp, dto.Timestamp) &&
                   entity.ToolId == dto.ToolId &&
                   entity.Unit1Id == (MeaUnit)dto.Unit1Id &&
                   entity.Unit2Id == (MeaUnit)dto.Unit2Id &&
                   entity.UpperLimitUnit1 == dto.UpperLimitUnit1 &&
                   entity.UpperLimitUnit2 == dto.UpperLimitUnit2 &&
                   entity.ToleranceClassUnit1.Id.ToLong() == dto.ToleranceClassUnit1 &&
                   entity.ToleranceClassUnit2.Id.ToLong() == dto.ToleranceClassUnit2 &&
                   CompareTestValuesWithTestValuesDto(entity.TestValues, dto.TestValues);
        }

        private static bool CompareTestValuesWithTestValuesDto(List<ClassicMfuTestValue> entityTestValues, ListOfClassicMfuTestValue dtoTestValues)
        {
            if (entityTestValues.Count != dtoTestValues.ClassicMfuTestValues.Count)
            {
                return false;
            }

            var i = 0;
            foreach (var val in entityTestValues)
            {
                if (!CompareTestValueWithTestValueDto(val, dtoTestValues.ClassicMfuTestValues[i]))
                {
                    return false;
                }
                i++;
            }

            return true;
        }

        private static bool CompareTestValueWithTestValueDto(ClassicMfuTestValue entity, DtoTypes.ClassicMfuTestValue dto)
        {
            return entity.Id.ToLong() == dto.Id &&
                   entity.Position == dto.Position &&
                   entity.ValueUnit1 == dto.ValueUnit1 &&
                   entity.ValueUnit2 == dto.ValueUnit2;
        }

        public static bool CompareProcessControlConditionToDto(Server.Core.Entities.ProcessControlCondition entity, DtoTypes.ProcessControlCondition dto)
        {
            if (entity == null && dto == null)
            {
                return true;
            }

            if (entity != null && dto == null || entity == null)
            {
                return false;
            }

            return dto.Id == entity.Id.ToLong()
                   && dto.Location.Id == entity.Location.Id.ToLong()
                   && dto.LowerInterventionLimit == entity.LowerInterventionLimit
                   && dto.UpperInterventionLimit == entity.UpperInterventionLimit
                   && dto.LowerMeasuringLimit == entity.LowerMeasuringLimit
                   && dto.UpperMeasuringLimit == entity.UpperMeasuringLimit 
                   && CompareTestLevelToTestLevelDto(entity.TestLevelSet, dto.TestLevelSet)
                   && entity.TestLevelNumber == dto.TestLevelNumber
                   && CompareNullableDate(dto.StartDate, entity.StartDate)
                   && entity.TestOperationActive == dto.TestOperationActive
                   && CompareProcessControlTechToDto(entity.ProcessControlTech, dto.ProcessControlTech);
        }

        public static bool CompareTestLevelToTestLevelDto(Server.Core.Entities.TestLevelSet entity, DtoTypes.TestLevelSet dto)
        {
            if (entity == null && dto == null)
            {
                return true;
            }

            if (entity != null && dto == null || entity == null)
            {
                return false;
            }

            return entity.Id?.ToLong() == dto.Id
                   && entity.Name?.ToDefaultString() == dto.Name
                   && CompareTestLevelToTestLevelDto(entity.TestLevel1, dto.TestLevel1)
                   && CompareTestLevelToTestLevelDto(entity.TestLevel2, dto.TestLevel2)
                   && CompareTestLevelToTestLevelDto(entity.TestLevel2, dto.TestLevel2);
        }

        public static bool CompareTestLevelToTestLevelDto(Server.Core.Entities.TestLevel entity, DtoTypes.TestLevel dto)
        {
            if (entity == null && dto == null)
            {
                return true;
            }

            if (entity != null && dto == null || entity == null)
            {
                return false;
            }

            return entity.Id?.ToLong() == dto.Id
                   && entity.ConsiderWorkingCalendar == dto.ConsiderWorkingCalendar
                   && entity.IsActive == dto.IsActive
                   && entity.SampleNumber == dto.SampleNumber
                   && CompareIntervalToIntervalDto(entity.TestInterval, dto.TestInterval);
        }

        public static bool CompareIntervalToIntervalDto(Core.Entities.Interval entity, BasicTypes.Interval dto)
        {
            if (entity == null && dto == null)
            {
                return true;
            }

            if (entity != null && dto == null || entity == null)
            {
                return false;
            }

            return (long)entity.Type == dto.IntervalType
                   && entity.IntervalValue == dto.IntervalValue;
        }

        public static bool CompareProcessControlTechToDto(Server.Core.Entities.ProcessControlTech entity,
            DtoTypes.ProcessControlTech dto)
        {
            if (entity == null && dto == null)
            {
                return true;
            }

            if (entity != null && dto == null || entity == null)
            {
                return false;
            }

            switch (entity)
            {
                case Server.Core.Entities.QstProcessControlTech qstProcessControlTech:
                    return CompareQstProcessControlTechToDto(qstProcessControlTech, dto.QstProcessControlTech);
            }

            return false;
        }

        public static bool CompareQstProcessControlTechToDto(Server.Core.Entities.QstProcessControlTech entity, DtoTypes.QstProcessControlTech dto)
        {
            if (entity == null && dto == null)
            {
                return true;
            }

            if (entity != null && dto == null || entity == null)
            {
                return false;
            }

            return dto.Id == entity.Id.ToLong() &&
                   dto.ProcessControlConditionId == entity.ProcessControlConditionId.ToLong() &&
                   dto.ManufacturerId == (long) entity.ManufacturerId &&
                   dto.TestMethod == (long) entity.TestMethod &&
                   CompareExtensionDtoWithExtension(dto.Extension, entity.Extension) &&
                   dto.Alive == entity.Alive &&
                   CompareNullableLong(dto.QstAngleLimitMt, entity.AngleLimitMt) &&
                   CompareNullableDouble(dto.QstStartMeasurementPeak, entity.StartMeasurementPeak) &&
                   CompareNullableDouble(dto.QstStartAngleCountingPa, entity.StartAngleCountingPa) &&
                   CompareNullableDouble(dto.QstAngleForFurtherTurningPa, entity.AngleForFurtherTurningPa) &&
                   CompareNullableDouble(dto.QstTargetAnglePa, entity.TargetAnglePa) &&
                   CompareNullableDouble(dto.QstStartMeasurementPa, entity.StartMeasurementPa) &&
                   CompareNullableDouble(dto.QstAlarmTorquePa, entity.AlarmTorquePa) &&
                   CompareNullableDouble(dto.QstAlarmAnglePa, entity.AlarmAnglePa) &&
                   CompareNullableDouble(dto.QstMinimumTorqueMt, entity.MinimumTorqueMt) &&
                   CompareNullableDouble(dto.QstStartAngleMt, entity.StartAngleMt) &&
                   CompareNullableDouble(dto.QstStartMeasurementMt, entity.StartMeasurementMt) &&
                   CompareNullableDouble(dto.QstAlarmTorqueMt, entity.AlarmTorqueMt) &&
                   CompareNullableDouble(dto.QstAlarmAngleMt, entity.AlarmAngleMt);
        }

        public static bool CompareProcessControlForTransferWithDto(TransferToTestEquipmentService.ProcessControlDataForTransfer dto, ProcessControlForTransfer entity)
        {
            return dto.LocationId == entity.LocationId.ToLong() &&
                   dto.ProcessControlConditionId == entity.ProcessControlConditionId.ToLong() &&
                   dto.ProcessControlTechId == entity.ProcessControlTechId.ToLong() &&
                   dto.LocationNumber == entity.LocationNumber.ToDefaultString() &&
                   dto.LocationDescription == entity.LocationDescription.ToDefaultString() &&
                   CompareNullableDouble(dto.MaximumTorque, entity.MaximumTorque) &&
                   CompareNullableDouble(dto.MinimumTorque, entity.MinimumTorque) &&
                   CompareNullableDouble(dto.SetPointTorque, entity.SetPointTorque) &&
                   CompareNullableDate(dto.LastTestDate, entity.LastTestDate) &&
                   CompareNullableDate(dto.NextTestDate, entity.NextTestDate) &&
                   dto.TestMethod == (long)entity.TestMethod &&
                   dto.SampleNumber == entity.SampleNumber &&
                   CompareIntervalDtoToInterval(dto.TestInterval, entity.TestInterval) &&
                   CompareNullableInt(dto.NextTestDateShift, (int?)entity.NextTestDateShift);
        }
        
        public static bool CompareExtensionDtoWithExtension(DtoTypes.Extension dtoExtension, Extension extension)
        {
            return dtoExtension.Id == extension.Id.ToLong() &&
                   dtoExtension.Description.Value == extension.Description &&
                   dtoExtension.InventoryNumber == extension.InventoryNumber?.ToDefaultString() &&
                   dtoExtension.Bending == extension.Bending &&
                   dtoExtension.FactorTorque == extension.FactorTorque &&
                   dtoExtension.Length == extension.Length &&
                   dtoExtension.Alive == extension.Alive;
        }


        public static bool CompareClassicProcessTestValueDtoWithClassicProcessTestValue(ClassicProcessTestValue entity, DtoTypes.ClassicProcessTestValue dto)
        {
            return entity.Id.ToLong() == dto.Id &&
                   entity.Position == dto.Position &&
                   entity.ValueUnit1 == dto.ValueUnit1 &&
                   entity.ValueUnit2 == dto.ValueUnit2;
        }

        public static bool CompareClassicProcessTestWithDto(Server.Core.Entities.ClassicProcessTest test, DtoTypes.ClassicProcessTest testDto)
        {
            return test.Id.ToLong() == testDto.Id &&
                   test.NumberOfTests == testDto.NumberOfTests &&
                   test.LowerLimitUnit1 == testDto.LowerLimitUnit1 &&
                   test.UpperLimitUnit1 == testDto.UpperLimitUnit1 &&
                   test.NominalValueUnit1 == testDto.NominalValueUnit1 &&
                   ArePrimitiveDateTimeAndDtoEqual(test.Timestamp, testDto.Timestamp) &&
                   test.Average == testDto.Average &&
                   (long)test.ControlledByUnitId == testDto.ControlledByUnitId &&
                   test.LowerInterventionLimitUnit1 == testDto.LowerInterventionLimitUnit1 &&
                   test.LowerInterventionLimitUnit2 == testDto.LowerInterventionLimitUnit2 &&
                   test.NominalValueUnit2 == testDto.NominalValueUnit2 &&
                   test.Result.LongValue == testDto.Result &&
                   CompareNullableDouble(testDto.StandardDeviation, test.StandardDeviation) &&
                   test.LowerLimitUnit2 == testDto.LowerLimitUnit2 &&
                   test.TestValueMaximum == testDto.TestValueMaximum &&
                   test.TestValueMinimum == testDto.TestValueMinimum &&
                   test.Timestamp.Ticks == testDto.Timestamp.Ticks &&
                   test.ToleranceClassUnit1.Id.ToLong() == testDto.ToleranceClassUnit1 &&
                   test.ToleranceClassUnit2.Id.ToLong() == testDto.ToleranceClassUnit2 &&
                   (long)test.Unit1Id == testDto.Unit1Id &&
                   (long)test.Unit2Id == testDto.Unit2Id &&
                   test.UpperLimitUnit2 == testDto.UpperLimitUnit2 &&
                   CompareTestEquipmentWithTestEquipmentDto(test.TestEquipment, testDto.TestEquipment) &&
                   CompareClassicTestLocationWithDto(test.TestLocation, testDto.TestLocation) &&
                   test.UpperInterventionLimitUnit1 == testDto.UpperInterventionLimitUnit1 &&
                   test.UpperInterventionLimitUnit2 == testDto.UpperInterventionLimitUnit2;
        }

        public static bool CompareClassicTestLocationWithDto(Server.Core.Entities.ClassicTestLocation test, DtoTypes.ClassicTestLocation testDto)
        {
            return test.LocationId.ToLong() == testDto.LocationId &&
                   test.LocationDirectoryId.ToLong() == testDto.LocationDirectoryId &&
                   CompareNullableString(testDto.TreePath, test.LocationTreePath);
        }
    }
}
