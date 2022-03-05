using System;
using Client.Core;
using Client.Core.Entities;
using Common.Types.Enums;
using Core.Entities;
using Core.Enums;
using Core.PhysicalValueTypes;
using Core.UseCases.Communication;
using TransferToTestEquipmentService;
using ToleranceClass = DtoTypes.ToleranceClass;

namespace FrameworksAndDrivers.RemoteData.GRPC.Test
{
    static class EqualityChecker
    {
        public static bool ArePrimitiveDateTimeAndDtoEqual(DateTime primitive, BasicTypes.DateTime dto)
        {
            return primitive.Ticks == dto.Ticks;
        }

        public static bool CompareLocationDtoWithLocation(DtoTypes.Location dto, Location entity, bool withParentId = true)
        {
            return dto.Id == entity.Id.ToLong() &&
                   dto.Number == entity.Number.ToDefaultString() &&
                   dto.Description == entity.Description.ToDefaultString() &&
                   (!withParentId || dto.ParentDirectoryId == entity.ParentDirectoryId.ToLong()) &&
                   dto.ConfigurableField1.Value == entity.ConfigurableField1.ToDefaultString() &&
                   dto.ConfigurableField2.Value == entity.ConfigurableField2.ToDefaultString() &&
                   dto.ConfigurableField3 == entity.ConfigurableField3 &&
                   dto.ControlledBy == (long)entity.ControlledBy &&
                   CompareToleranceClassWithToleranceClassDto(entity.ToleranceClassTorque, dto.ToleranceClass1) &&
                   CompareToleranceClassWithToleranceClassDto(entity.ToleranceClassAngle, dto.ToleranceClass2) &&
                   dto.SetPoint1 == entity.SetPointTorque.Nm &&
                   dto.SetPoint2 == entity.SetPointAngle.Degree &&
                   dto.Maximum1 == entity.MaximumTorque.Nm &&
                   dto.Maximum2 == entity.MaximumAngle.Degree &&
                   dto.Minimum1 == entity.MinimumTorque.Nm &&
                   dto.Minimum2 == entity.MinimumAngle.Degree &&
                   dto.Threshold1 == entity.ThresholdTorque.Nm &&
                   dto.Comment.Value == entity.Comment;
        }

        public static bool CompareToolWithToolDto(Tool entity, DtoTypes.Tool dto)
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
                   dto.InventoryNumber == entity.InventoryNumber?.ToDefaultString() &&
                   dto.SerialNumber == entity.SerialNumber?.ToDefaultString() &&
                   CompareToolModelWithToolModelDto(entity.ToolModel, dto.ToolModel) &&
                   CompareStatusWithStatusDto(entity.Status, dto.Status);
        }

        private static bool CompareNullableDouble(BasicTypes.NullableDouble dto, double? entity)
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

        private static bool CompareNullableString(BasicTypes.NullableString dto, string entity)
        {
            if (dto.IsNull && entity == null)
            {
                return true;
            }

            return dto.Value == entity;
        }

        private static bool CompareNullableLong(BasicTypes.NullableLong dto, long? entity)
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


        private static bool CompareNullableLong(BasicTypes.NullableLong dto, Angle entity)
        {
            if (dto.IsNull && entity == null)
            {
                return true;
            }

            if (!dto.IsNull && entity == null)
            {
                return false;
            }

            return dto.Value == (long)entity.Degree;
        }

        private static bool CompareNullableDouble(BasicTypes.NullableDouble dto, Angle entity)
        {
            if (dto.IsNull && entity == null)
            {
                return true;
            }

            if (!dto.IsNull && entity == null)
            {
                return false;
            }

            return dto.Value == entity.Degree;
        }

        private static bool CompareNullableDouble(BasicTypes.NullableDouble dto, Torque entity)
        {
            if (dto.IsNull && entity == null)
            {
                return true;
            }

            if (!dto.IsNull && entity == null)
            {
                return false;
            }

            return dto.Value == entity.Nm;
        }

        private static bool CompareNullableInt(BasicTypes.NullableInt dto, int? entity)
        {
            if (dto.IsNull && entity == null)
            {
                return true;
            }

            if (!dto.IsNull && entity == null)
            {
                return false;
            }

            return dto.Value == entity.Value;
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

        public static bool CompareToolModelWithToolModelDto(ToolModel entity, DtoTypes.ToolModel dto)
        {
            var assigner = new Assigner();

            if (dto == null && entity == null)
            {
                return true;
            }

            if (dto == null || entity == null)
            {
                return false;
            }

            long entityModelType = 0;
            assigner.Assign((value) => { entityModelType = value; }, entity.ModelType);
            long entityClass = 0;
            assigner.Assign((value) => { entityClass = value; }, entity.Class);

            return 
                entity.Id.ToLong() == dto.Id &&
                entity.Description?.ToDefaultString() == dto.Description &&
                entityModelType == dto.ModelType &&
                entityClass == dto.Class &&
                CompareManufacturerWithManufacturerDto(entity.Manufacturer, dto.Manufacturer) &&
                CompareNullableDouble(dto.MinPower, entity.MinPower) &&
                CompareNullableDouble(dto.MaxPower, entity.MaxPower) &&
                CompareNullableDouble(dto.AirPressure, entity.AirPressure) &&
                // tool type?
                dto.Weight == entity.Weight &&
                CompareNullableDouble(dto.BatteryVoltage, entity.BatteryVoltage) &&
                CompareNullableLong(dto.MaxRotationSpeed, entity.MaxRotationSpeed) &&
                CompareNullableDouble(dto.AirConsumption, entity.AirConsumption) &&
                // switchoff
                // shutoff
                // drive type
                // construction type
                // picture
                dto.CmLimit == entity.CmLimit &&
                dto.CmkLimit == entity.CmkLimit;
        }

        public static bool CompareClassicMfuTestDtoWithClassicMfuTest(ClassicMfuTest entity, DtoTypes.ClassicMfuTest dto)
        {
            return entity.Id.ToLong() == dto.Id &&
                   ArePrimitiveDateTimeAndDtoEqual(entity.Timestamp,dto.Timestamp) &&
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
                   entity.Cmk == dto.Cmk &&
                   entity.Cm == dto.Cm &&
                   entity.LimitCm == dto.LimitCm &&
                   entity.LimitCmk == dto.LimitCmk &&
                   entity.TestLocation.LocationId.ToLong() == dto.TestLocation.LocationId &&
                   entity.TestLocation.LocationDirectoryId.ToLong() == dto.TestLocation.LocationDirectoryId &&
                   CompareTestEquipmentWithTestEquipmentDto(entity.TestEquipment, dto.TestEquipment);
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
                   CompareTestEquipmentWithTestEquipmentDto(entity.TestEquipment, dto.TestEquipment);
        }

        public static bool CompareTestEquipmentModelWithTestEquipmentModelDto(TestEquipmentModel entity,DtoTypes.TestEquipmentModel dto)
        {
            return entity.Id.ToLong() == dto.Id &&
                    entity.TestEquipmentModelName.ToDefaultString() == dto.TestEquipmentModelName &&
                    entity.CommunicationFilePath.ToDefaultString() == dto.CommunicationFilePath.Value &&
                    entity.DriverProgramPath.ToDefaultString() == dto.DriverProgramPath.Value &&
                    entity.StatusFilePath.ToDefaultString() == dto.StatusFilePath.Value &&
                    entity.ResultFilePath.ToDefaultString() == dto.ResultFilePath.Value &&
                    CompareManufacturerWithManufacturerDto(entity.Manufacturer, dto.Manufacturer) &&
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
                    dto.UseForCtl == entity.UseForCtl &&
                    dto.UseForRot == entity.UseForRot &&
                    dto.CanUseQstStandard == entity.CanUseQstStandard &&
                    dto.DataGateVersion == entity.DataGateVersion.DataGateVersionsId &&
                    dto.Type == (long)entity.Type;
        }

        public static bool CompareTestEquipmentWithTestEquipmentDto(TestEquipment entity, DtoTypes.TestEquipment dto)
        {
            return entity.Id.ToLong() == dto.Id &&
                   entity.InventoryNumber.ToDefaultString() == dto.InventoryNumber.Value &&
                   entity.SerialNumber.ToDefaultString() == dto.SerialNumber.Value &&
                   CompareTestEquipmentModelWithTestEquipmentModelDto(entity.TestEquipmentModel, dto.TestEquipmentModel) &&
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
                   dto.UseForCtl == entity.UseForCtl &&
                   dto.UseForRot == entity.UseForRot &&
                   dto.CanUseQstStandard == entity.CanUseQstStandard &&
                   CompareNullableString(dto.Version, entity.Version?.ToDefaultString()) &&
                   CompareStatusWithStatusDto(entity.Status, dto.Status) && 
                   dto.CapacityMin ==  entity.CapacityMin.Nm &&
                   dto.CapacityMax == entity.CapacityMax.Nm &&
                   CompareNullableString(dto.Version, entity.Version?.ToDefaultString()) &&
                   CompareNullableString(dto.CalibrationNorm, entity.CalibrationNorm?.ToDefaultString()) &&
                   CompareNullableDate(dto.LastCalibration, entity.LastCalibration) &&
                   CompareIntervalDtoToInterval(dto.CalibrationInterval, entity.CalibrationInterval);
        }

        public static bool CompareIntervalDtoToInterval(BasicTypes.Interval dto, Interval entity)
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

        public static bool CompareLocationDirectoryDtoWithLocationDirectory(DtoTypes.LocationDirectory dto, LocationDirectory entity, bool withParentId = true)
        {
            return dto.Id == entity.Id.ToLong() &&
                   dto.Name == entity.Name.ToDefaultString() &&
                   (!withParentId || dto.ParentId == entity.ParentId.ToLong());
        }

        public static bool CompareManufacturerWithManufacturerDto(Manufacturer manufacturer,
            DtoTypes.Manufacturer dtoManufacturer)
        {
            if (manufacturer == null && dtoManufacturer == null)
            {
                return true;
            }

            if (manufacturer == null || dtoManufacturer == null)
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
                   manufacturer.Comment == dtoManufacturer.Comment.Value;
        }

        public static bool CompareTestParametersDtoWithTestParameters(DtoTypes.TestParameters dto, TestParameters entity)
        {
            return entity.MinimumTorque.Nm == dto.Minimum1 &&
                   entity.MaximumTorque.Nm == dto.Maximum1 &&
                   entity.SetPointTorque.Nm == dto.SetPoint1 &&
                   entity.ThresholdTorque.Degree == dto.Threshold1 &&
                   entity.MinimumAngle.Degree == dto.Minimum2 &&
                   entity.MaximumAngle.Degree == dto.Maximum2 &&
                   entity.SetPointAngle.Degree == dto.SetPoint2 &&
                   entity.ControlledBy == (LocationControlledBy) dto.ControlledBy;
        }

        public static bool CompareToleranceClassWithToleranceClassDto(Core.Entities.ToleranceClass tol, ToleranceClass tolDto)
        {
            return tol.Id.ToLong() == tolDto.Id &&
                   tol.Name == tolDto.Name &&
                   tol.Relative == tolDto.Relative &&
                   tol.LowerLimit == tolDto.LowerLimit &&
                   tol.UpperLimit == tolDto.UpperLimit;
        }
        
        public static bool CompareToolUsageWithToolUsageDto(Core.Entities.ToolUsage toolUsage, DtoTypes.ToolUsage dtoToolUsage)
        {
            return toolUsage.ListId.ToLong() == dtoToolUsage.Id &&
                   toolUsage.Value.ToDefaultString() == dtoToolUsage.Description;
        }

        public static bool AreLocationToolAssignmentEntityAndDtoEqual(LocationToolAssignment entity, DtoTypes.LocationToolAssignment dto)
        {
            return entity.Id.ToLong() == dto.Id &&
                   CompareLocationDtoWithLocation(dto.AssignedLocation, entity.AssignedLocation) &&
                   CompareToolWithToolDto(entity.AssignedTool, dto.AssignedTool) &&
                   CompareTestTechniqueDtoWithTestTechnique(dto.TestTechnique, entity.TestTechnique) &&
                   CompareTestParametersDtoWithTestParameters(dto.TestParameters, entity.TestParameters) &&
                   CompareToleranceClassWithToleranceClassDto(entity.TestParameters.ToleranceClassTorque, dto.TestParameters.ToleranceClass1) &&
                   CompareToleranceClassWithToleranceClassDto(entity.TestParameters.ToleranceClassAngle, dto.TestParameters.ToleranceClass2) &&
                   CompareToolUsageWithToolUsageDto(entity.ToolUsage, dto.ToolUsage) && 
                   entity.TestLevelNumberChk == dto.TestLevelNumberChk &&
                   ArePrimitiveDateTimeAndDtoEqual(entity.NextTestDateMfu.Value, dto.NextTestDateMfu.Value) &&
                   ArePrimitiveDateTimeAndDtoEqual(entity.NextTestDateChk.Value, dto.NextTestDateChk.Value) &&
                   (int)entity.NextTestShiftMfu == dto.NextTestShiftMfu?.Value &&
                   (int)entity.NextTestShiftChk == dto.NextTestShiftChk?.Value &&
                   entity.TestLevelSetMfu?.Id?.ToLong() == dto.TestLevelSetMfu?.Id &&
                   entity.TestLevelSetMfu?.Name?.ToDefaultString() == dto.TestLevelSetMfu?.Name &&
                   entity.TestLevelNumberMfu == dto.TestLevelNumberMfu &&
                   entity.TestLevelSetChk?.Id?.ToLong() == dto.TestLevelSetChk?.Id &&
                   entity.TestLevelSetChk?.Name?.ToDefaultString() == dto.TestLevelSetChk?.Name;
        }

        public static bool CompareLocationToolAssignmentsForTransferDtoWithLocationToolAssignmentForTransfer(TransferToTestEquipmentService.LocationToolAssignmentForTransfer dto, Core.UseCases.Communication.LocationToolAssignmentForTransfer entity)
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

        public static bool CompareStatusWithStatusDto(Status status, DtoTypes.Status statusDto)
        {
            return statusDto?.Id == status?.ListId?.ToLong() && statusDto?.Description == status?.Value?.ToDefaultString();
        }

        public static bool CompareProcessControlConditionToDto(ProcessControlCondition entity, DtoTypes.ProcessControlCondition dto)
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
                   && dto.LowerInterventionLimit == entity.LowerInterventionLimit?.Nm
                   && dto.UpperInterventionLimit == entity.UpperInterventionLimit?.Nm
                   && dto.LowerMeasuringLimit == entity.LowerMeasuringLimit?.Nm
                   && dto.UpperMeasuringLimit == entity.UpperMeasuringLimit?.Nm
                   && CompareTestLevelToTestLevelDto(entity.TestLevelSet, dto.TestLevelSet)
                   && entity.TestLevelNumber == dto.TestLevelNumber
                   && CompareNullableDate(dto.StartDate, entity.StartDate)
                   && entity.TestOperationActive == dto.TestOperationActive
                   && CompareProcessControlTechToDto(entity.ProcessControlTech, dto.ProcessControlTech);
            
        }

        public static bool CompareTestLevelToTestLevelDto(TestLevelSet entity, DtoTypes.TestLevelSet dto)
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

        public static bool CompareTestLevelToTestLevelDto(TestLevel entity, DtoTypes.TestLevel dto)
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

        public static bool CompareIntervalToIntervalDto(Interval entity, BasicTypes.Interval dto)
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

        public static bool CompareProcessControlTechToDto(Client.Core.Entities.ProcessControlTech entity,
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
                case Client.Core.Entities.QstProcessControlTech qstProcessControlTech:
                    return CompareQstProcessControlTechToDto(qstProcessControlTech, dto.QstProcessControlTech);
            }

            return false;
        }

        public static bool CompareQstProcessControlTechToDto(Client.Core.Entities.QstProcessControlTech entity, DtoTypes.QstProcessControlTech dto)
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
                    dto.ManufacturerId == (long)entity.ManufacturerId &&
                    dto.TestMethod == (long)entity.TestMethod &&
                    CompareExtensionWithExtensionDto(entity.Extension, dto.Extension) &&
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

        public static bool CompareProcessControlForTransferWithDto(ProcessControlDataForTransfer dto, ProcessControlForTransfer entity)
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


        public static bool CompareExtensionWithExtensionDto(Extension ext, DtoTypes.Extension extDto)
        {
            return ext.Id.ToLong() == extDto.Id &&
                   ext.Description == extDto.Description.Value &&
                   ext.InventoryNumber?.ToDefaultString() == extDto.InventoryNumber &&
                   ext.Bending == extDto.Bending &&
                   ext.FactorTorque == extDto.FactorTorque &&
                   ext.Length == extDto.Length;
        }

        public static bool CompareClassicProcessTestWithDto(ClassicProcessTest test, DtoTypes.ClassicProcessTest testDto)
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

        public static bool CompareClassicTestLocationWithDto(ClassicTestLocation test, DtoTypes.ClassicTestLocation testDto)
        {
            return test.LocationId.ToLong() == testDto.LocationId &&
                   test.LocationDirectoryId.ToLong() == testDto.LocationDirectoryId &&
                   CompareNullableString(testDto.TreePath, test.LocationTreePath);
        }
    }
}
