using System;
using System.Collections.Generic;
using System.Linq;
using BasicTypes;
using Common.Types.Enums;
using Core.Enums;
using DtoTypes;
using Manufacturer = DtoTypes.Manufacturer;
using Status = DtoTypes.Status;

namespace FrameworksAndDrivers.NetworkView.Test
{
    public static class DtoFactory
    {
        public static string CreateStringRandomized(int length, int seed)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            var random = new Random(seed);
            var randomString = new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
            return randomString;
        }

        public static BasicTypes.DateTime CreateDateTimeRandomized(int seed)
        {
            var random = new Random(seed);
            var date = new System.DateTime(random.Next(1900, 2030), random.Next(1, 12), random.Next(0, 27),
                           random.Next(0, 24), random.Next(0, 60), random.Next(0, 60));
            return new BasicTypes.DateTime()
            {
                Ticks = date.Ticks
            };
        }

        public static DtoTypes.Tool CreateToolDto(long id, string serialNumber, string inventoryNumber, bool alive, string comment, string addField1, string addField2, string addField3, string accessory, Status status, DtoTypes.HelperTableEntity costCenter, DtoTypes.HelperTableEntity configurableField, DtoTypes.ToolModel toolModel)
        {
            return new DtoTypes.Tool()
            {
                Id = id,
                SerialNumber = serialNumber,
                InventoryNumber = inventoryNumber,
                Alive = alive,
                Comment = new NullableString() { IsNull = false, Value = comment },
                Status = status,
                ConfigurableField = configurableField,
                Accessory = new NullableString() { IsNull = false, Value = accessory },
                CostCenter = costCenter,
                AdditionalConfigurableField1 = new NullableString(){IsNull = false, Value = addField1},
                AdditionalConfigurableField2 = new NullableString() { IsNull = false, Value = addField2 },
                AdditionalConfigurableField3 = new NullableString() { IsNull = false, Value = addField3 },
                ToolModel = toolModel,
            };
        }

        public static DtoTypes.ToolModel CreateToolModelDto(
            long id,
            string description,
            Manufacturer manufacturer,
            double minPower,
            double maxPower,
            double airPressure,
            DtoTypes.HelperTableEntity toolType,
            double weight,
            double batteryVoltage,
            long maxRotationSpeed,
            double airConsumption,
            DtoTypes.HelperTableEntity switchOff,
            DtoTypes.HelperTableEntity driveSize,
            DtoTypes.HelperTableEntity shutOff,
            DtoTypes.HelperTableEntity driveType,
            DtoTypes.HelperTableEntity constructionType,
            double cmLimit,
            double cmkLimit,
            bool alive,
            long modelType,
            long classId)
        {
            return new DtoTypes.ToolModel()
            {
                Id = id, 
                Description = description,
                Manufacturer = manufacturer,
                MinPower = new NullableDouble {IsNull = false, Value = minPower},
                MaxPower = new NullableDouble { IsNull = false, Value = maxPower },
                AirPressure = new NullableDouble { IsNull = false, Value = airPressure },
                ToolType = toolType,
                Weight = weight,
                BatteryVoltage = new NullableDouble { IsNull = false, Value = batteryVoltage },
                MaxRotationSpeed = new NullableLong { IsNull = false, Value = maxRotationSpeed },
                AirConsumption = new NullableDouble { IsNull = false, Value = airConsumption },
                SwitchOff = switchOff,
                DriveSize = driveSize,
                ShutOff = shutOff,
                DriveType = driveType,
                ConstructionType = constructionType,
                CmLimit = cmLimit,
                CmkLimit = cmkLimit,
                Alive = alive,
                ModelType = modelType,
                Class = classId
            };
        }

        public static DtoTypes.ToolModel CreateToolModelDtoRandomized(int seed)
        {
            var toolModelClasses = new List<ToolModelClass>
            {
                ToolModelClass.DriverFixSet,
                ToolModelClass.DriverScale,
                ToolModelClass.DriverWithoutScale,
                ToolModelClass.WrenchFixSet,
                ToolModelClass.WrenchScale,
                ToolModelClass.WrenchWithoutScale
            };

            var toolModelTypes = new List<ToolModelType>
            {
                ToolModelType.ClickWrench,
                ToolModelType.ECDriver,
                ToolModelType.General,
                ToolModelType.MDWrench,
                ToolModelType.ProductionWrench,
                ToolModelType.PulseDriver,
                ToolModelType.PulseDriverShutOff,
            };

            var randomizer = new Random(seed);
            return CreateToolModelDto(
                randomizer.Next(),
                CreateStringRandomized(randomizer.Next(0, 50), randomizer.Next()),
                CreateManufacturerDtoRandomized(randomizer.Next()),
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                CreateToolTypeRandomized(randomizer.Next()),
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                randomizer.Next(),
                randomizer.NextDouble(),
                CreateSwitchOffRandomized(randomizer.Next()),
                CreateDriveSizeRandomized(randomizer.Next()),
                CreateShutOffRandomized(randomizer.Next()),
                CreateDriveTypeRandomized(randomizer.Next()),
                CreateConstructionTypeRandomized(randomizer.Next()),
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                randomizer.Next(0,1)==1,
                (long)toolModelTypes[randomizer.Next(0, toolModelTypes.Count)],
                (long)toolModelClasses[randomizer.Next(0, toolModelClasses.Count)]);
        }

        public static DtoTypes.HelperTableEntity CreateToolTypeRandomized(int seed)
        {
            var randomizer = new Random(seed);
            return new HelperTableEntity
            {
                Alive = (randomizer.Next() % 1) == 1,
                ListId = randomizer.Next(),
                NodeId = (long)65, //ToolType = 65,
                Value = CreateStringRandomized(randomizer.Next(0, 50), randomizer.Next())
            };
        }

        public static DtoTypes.HelperTableEntity CreateSwitchOffRandomized(int seed)
        {
            var randomizer = new Random(seed);
            return new HelperTableEntity
            {
                Alive = (randomizer.Next() % 1) == 1,
                ListId = randomizer.Next(),
                NodeId = (long)103, //SwitchOff = 103,
                Value = CreateStringRandomized(randomizer.Next(0, 50), randomizer.Next())
            };
        }

        public static DtoTypes.HelperTableEntity CreateShutOffRandomized(int seed)
        {
            var randomizer = new Random(seed);
            return new HelperTableEntity
            {
                Alive = (randomizer.Next() % 1) == 1,
                ListId = randomizer.Next(),
                NodeId = (long)105, //ShutOff = 105,
                Value = CreateStringRandomized(randomizer.Next(0, 50), randomizer.Next())
            };
        }

        public static DtoTypes.HelperTableEntity CreateDriveTypeRandomized(int seed)
        {
            var randomizer = new Random(seed);
            return new HelperTableEntity
            {
                Alive = (randomizer.Next() % 1) == 1,
                ListId = randomizer.Next(),
                NodeId = (long)101, //DriveType = 101,
                Value = CreateStringRandomized(randomizer.Next(0, 50), randomizer.Next())
            };
        }

        public static DtoTypes.HelperTableEntity CreateConstructionTypeRandomized(int seed)
        {
            var randomizer = new Random(seed);
            return new HelperTableEntity
            {
                Alive = (randomizer.Next() % 1) == 1,
                ListId = randomizer.Next(),
                NodeId = (long)102, //ConstructionType = 102,
                Value = CreateStringRandomized(randomizer.Next(0, 50), randomizer.Next())
            };
        }

        public static DtoTypes.HelperTableEntity CreateDriveSizeRandomized(int seed)
        {
            var randomizer = new Random(seed);
            return new HelperTableEntity
            {
                Alive = (randomizer.Next() % 1) == 1,
                ListId = randomizer.Next(),
                NodeId = (long)104, //DriveSize = 102,
                Value = CreateStringRandomized(randomizer.Next(0, 50), randomizer.Next())
            };
        }

        public static DtoTypes.TestEquipment CreateTestEquipmentDtoRandomized(int seed)
        {
            var randomizer = new Random(seed);
            return CreateTestEquipmentDto(
                randomizer.Next(),
                CreateStringRandomized(randomizer.Next(0, 10), randomizer.Next()),
                CreateStringRandomized(randomizer.Next(0, 10), randomizer.Next()),
                randomizer.Next(0, 1) == 1,
                CreateTestEquipmentModelDtoRandomized(randomizer.Next()),
                randomizer.Next(0,1) == 1,
                randomizer.Next(0, 1) == 1,
                randomizer.Next(0, 2),
                randomizer.Next(0, 4),
                randomizer.Next(0, 1) == 1,
                randomizer.Next(0, 1) == 1,
                randomizer.Next(0, 1) == 1,
                randomizer.Next(0, 1) == 1,
                randomizer.Next(0, 2),
                randomizer.Next(0, 1) == 1,
                randomizer.Next(0, 1) == 1,
                randomizer.Next(0, 1) == 1,
                randomizer.Next(0, 1) == 1,
                randomizer.Next(0, 1) == 1,
                randomizer.Next(0, 1) == 1,
                randomizer.Next(0, 1),
                randomizer.Next(0, 300),
                CreateDateTimeRandomized(seed),
                randomizer.NextDouble(), 
                randomizer.NextDouble(), 
                CreateStringRandomized(randomizer.Next(0, 10), randomizer.Next()),
                CreateStringRandomized(randomizer.Next(0, 30), randomizer.Next()),
                CreateStatusRandomized(seed),
                randomizer.Next(0, 1) == 1);
        }

        public static Status CreateStatusRandomized(int seed)
        {
            var randomizer = new Random(seed);
            return new Status()
            {
                Id = randomizer.Next(),
                Alive = randomizer.Next() % 2 == 1,
                Description = CreateStringRandomized(randomizer.Next(0, 10), randomizer.Next())
            };
        }

        public static DtoTypes.TestEquipment CreateTestEquipmentDto(long id, string inventoryNumber, string serialNumber, bool alive, TestEquipmentModel testEquipmentModel,
            bool transferTransducer, bool transferAdapter, long transferCurves, long askForIdent, bool transferLocPicts, bool transferUser, bool transferNewLimits,
            bool useErrorCodes, long confirmMeasurements, bool useForCtl, bool useForRot, bool askForSign, bool doLoseCheck, bool transferAttributes, 
            bool canDeleteMeasurements, long intervalType, long intervalValue, BasicTypes.DateTime lastCalibration, double capacityMax,
            double capacityMin, string version, string calibrationNorm, Status status, bool canUseQstStandard)
        {
            return new TestEquipment()
            {
                Id = id,
                InventoryNumber = new NullableString() { IsNull = false, Value = inventoryNumber },
                SerialNumber = new NullableString() { IsNull = false, Value = serialNumber },
                Alive = alive,
                TestEquipmentModel = testEquipmentModel,
                TransferTransducer = transferTransducer,
                TransferAdapter = transferAdapter,
                TransferCurves = transferCurves,
                AskForIdent = askForIdent,
                TransferLocationPictures = transferLocPicts,
                TransferUser = transferUser,
                TransferNewLimits = transferNewLimits,
                UseErrorCodes = useErrorCodes,
                ConfirmMeasurements = confirmMeasurements,
                UseForCtl = useForCtl,
                UseForRot = useForRot,
                AskForSign = askForSign,
                DoLoseCheck = doLoseCheck,
                TransferAttributes = transferAttributes,
                CanDeleteMeasurements = canDeleteMeasurements,
                CalibrationInterval = new Interval() { IntervalType = intervalType, IntervalValue = intervalValue },
                LastCalibration = new NullableDateTime() { IsNull = false, Value = lastCalibration },
                CapacityMax = capacityMax,
                CapacityMin = capacityMin,
                Version = new NullableString() { IsNull = false, Value = version },
                CalibrationNorm = new NullableString() { IsNull = false, Value = calibrationNorm },
                Status = status,
                CanUseQstStandard = canUseQstStandard
            };
        }

        public static DtoTypes.TestEquipmentModel CreateTestEquipmentModelDto(long id, string name, string driverProgramPath, string communicationFilePath, bool alive, long type,
            bool transferUser, bool transferAdapter, bool transferTransducer, bool askForIdent, bool transferCurves, bool useErrorCodes, bool askForSign, bool doLoseCheck,
            bool canDeleteMeasurements, bool confirmMeasurements, bool transferLocationPicts, bool transferNewLimits, bool transferAttributes, string statusFilePath, string resultFilePath,
            int dataGateVersion, bool useForRot, bool useForCtl, bool canUseQstStandard)
        {
            return new TestEquipmentModel()
            {
                Id = id,
                Alive = alive,
                DriverProgramPath = new NullableString() { IsNull = false, Value = driverProgramPath },
                CommunicationFilePath = new NullableString() { IsNull = false, Value = communicationFilePath },
                TestEquipmentModelName = name,
                TransferUser = transferUser,
                TransferAdapter = transferAdapter,
                AskForIdent = askForIdent,
                AskForSign = askForSign,
                CanDeleteMeasurements = canDeleteMeasurements,
                ConfirmMeasurements = confirmMeasurements,
                DoLoseCheck = doLoseCheck,
                TransferAttributes = transferAttributes,
                TransferCurves = transferCurves,
                TransferLocationPictures = transferLocationPicts,
                TransferNewLimits = transferNewLimits,
                TransferTransducer = transferTransducer,
                UseErrorCodes = useErrorCodes,
                Type = type,
                StatusFilePath = new NullableString() { IsNull = false, Value = statusFilePath },
                ResultFilePath = new NullableString() { IsNull = false, Value = resultFilePath },
                UseForCtl = useForCtl,
                UseForRot = useForRot,
                DataGateVersion = dataGateVersion,
                CanUseQstStandard = canUseQstStandard
            };
        }

        public static DtoTypes.TestEquipmentModel CreateTestEquipmentModelDtoRandomized(int seed)
        {
            var randomizer = new Random(seed);
            return CreateTestEquipmentModelDto(
                randomizer.Next(),
                CreateStringRandomized(randomizer.Next(0, 10), randomizer.Next()),
                CreateStringRandomized(randomizer.Next(0, 10), randomizer.Next()),
                CreateStringRandomized(randomizer.Next(0, 10), randomizer.Next()),
                randomizer.Next(0, 1) == 1,
                randomizer.Next(),
                randomizer.Next(0, 1) == 1,
                randomizer.Next(0, 1) == 1,
                randomizer.Next(0, 1) == 1,
                randomizer.Next(0, 1) == 1,
                randomizer.Next(0, 1) == 1,
                randomizer.Next(0, 1) == 1,
                randomizer.Next(0, 1) == 1,
                randomizer.Next(0, 1) == 1,
                randomizer.Next(0, 1) == 1,
                randomizer.Next(0, 1) == 1,
                randomizer.Next(0, 1) == 1,
                randomizer.Next(0, 1) == 1,
                randomizer.Next(0, 1) == 1,
                CreateStringRandomized(randomizer.Next(0, 20), randomizer.Next()),
                CreateStringRandomized(randomizer.Next(0, 20), randomizer.Next()),
                randomizer.Next(0, 7),
                randomizer.Next(0, 1) == 1,
                randomizer.Next(0, 1) == 1,
                randomizer.Next(0, 1) == 1);
        }


        public static DtoTypes.Manufacturer CreateManufacturerDto(long manufacturerId, string name, string country, string street,
            string person, string houseNumber, string city, string fax, string phoneNumber, string zipCode, string comment, bool alive)
        {
            return new DtoTypes.Manufacturer()
            {
                ManufacturerId = manufacturerId,
                ManufactuerName = new NullableString() { IsNull = false, Value = name },
                Country = new NullableString() { IsNull = false, Value = country },
                Street = new NullableString() { IsNull = false, Value = street },
                Person = new NullableString() { IsNull = false, Value = person },
                HouseNumber = new NullableString() { IsNull = false, Value = houseNumber },
                City = new NullableString() { IsNull = false, Value = city },
                Fax = new NullableString() { IsNull = false, Value = fax },
                PhoneNumber = new NullableString() { IsNull = false, Value = phoneNumber },
                ZipCode = new NullableString() { IsNull = false, Value = zipCode },
                Comment = new NullableString() { IsNull = false, Value = comment },
                Alive = alive
            };
        }

        public static DtoTypes.Manufacturer CreateManufacturerDtoRandomized(int seed)
        {
            var randomizer = new Random(seed);
            return CreateManufacturerDto(
                randomizer.Next(),
                CreateStringRandomized(randomizer.Next(0,30),randomizer.Next()),
                CreateStringRandomized(randomizer.Next(0, 30), randomizer.Next()),
                CreateStringRandomized(randomizer.Next(0, 30), randomizer.Next()),
                CreateStringRandomized(randomizer.Next(0, 30), randomizer.Next()),
                CreateStringRandomized(randomizer.Next(0, 30), randomizer.Next()),
                CreateStringRandomized(randomizer.Next(0, 30), randomizer.Next()),
                CreateStringRandomized(randomizer.Next(0, 30), randomizer.Next()),
                CreateStringRandomized(randomizer.Next(0, 30), randomizer.Next()),
                CreateStringRandomized(randomizer.Next(0, 30), randomizer.Next()),
                CreateStringRandomized(randomizer.Next(0, 30), randomizer.Next()),
                randomizer.Next(0,1)==1);
        }

        public static DtoTypes.Location CreateLocation(long id, string number, string description, long parentId,
            string c1, string c2, bool c3, long controlledBy, DtoTypes.ToleranceClass toleranceClass1, DtoTypes.ToleranceClass toleranceClass2, double setpoint1, double setpoint2,
            double max1, double max2, double min1, double min2, double threshold1, string comment, bool alive)
        {
            return new DtoTypes.Location()
            {
                Id = id,
                Number = number,
                Description = description,
                ParentDirectoryId = parentId,
                ConfigurableField1 = new NullableString() { IsNull = false, Value = c1 },
                ConfigurableField2 = new NullableString() { IsNull = false, Value = c2 },
                ConfigurableField3 = c3,
                ControlledBy = controlledBy,
                ToleranceClass1 = toleranceClass1,
                ToleranceClass2 = toleranceClass2,
                SetPoint1 = setpoint1,
                SetPoint2 = setpoint2,
                Minimum1 = min1,
                Minimum2 = min2,
                Maximum1 = max1,
                Maximum2 = max1,
                Threshold1 = threshold1,
                Comment = new NullableString() { IsNull = false, Value = comment },
                Alive = alive
            };
        }

        public static DtoTypes.Location CreateLocationDtoRandomized(int seed)
        {
            var randomizer = new Random(seed);
            return CreateLocation(
                randomizer.Next(),
                CreateStringRandomized(randomizer.Next(0, 30), randomizer.Next()),
                CreateStringRandomized(randomizer.Next(0, 50), randomizer.Next()),
                randomizer.Next(),
                CreateStringRandomized(randomizer.Next(0, 15), randomizer.Next()),
                CreateStringRandomized(randomizer.Next(0, 1), randomizer.Next()),
                randomizer.Next(0, 1) == 1,
                randomizer.Next(0, 1),
                CreateToleranceDtoClassRandomized(randomizer.Next()),
                CreateToleranceDtoClassRandomized(randomizer.Next()),
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                CreateStringRandomized(randomizer.Next(0, 30), randomizer.Next()),
                randomizer.Next(0, 1) == 0);
        }

        public static DtoTypes.LocationDirectory CreateLocationDirectory(long id, string name, long parentId, bool alive)
        {
            return new DtoTypes.LocationDirectory() { Id = 1, Name = name, ParentId = parentId, Alive = alive };
        }

        public static DtoTypes.ToleranceClass CreateToleranceClassDto(long id, string name, bool relative, double lowerLimit, double upperLimit, bool alive)
        {
            return new DtoTypes.ToleranceClass()
            {
                Id = id,
                Name = name,
                Relative = relative,
                LowerLimit = lowerLimit,
                UpperLimit = upperLimit,
                Alive = alive
            };
        }

        public static DtoTypes.ToleranceClass CreateToleranceDtoClassRandomized(int seed)
        {
            var randomizer = new Random(seed);
            return CreateToleranceClassDto(
                randomizer.Next(),
                CreateStringRandomized(randomizer.Next(0, 30), randomizer.Next()),
                randomizer.Next(0, 1) == 1,
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                randomizer.Next(0, 1) == 1);
        }

        public static DtoTypes.ProcessControlCondition CreateProcessControlConditionDto(long id, Location location,
            double lowerInterventionLimit, double upperInterventionLimit, double lowerMeasuringLimit, double upperMeasuringLimit,
            int testLevelNumber, DtoTypes.TestLevelSet testLevelSet, bool alive, BasicTypes.DateTime startDate, bool testOperationActive)
        {
            return new DtoTypes.ProcessControlCondition()
            {
                Id = id,
                Location = location,
                LowerInterventionLimit = lowerInterventionLimit,
                UpperInterventionLimit = upperInterventionLimit,
                LowerMeasuringLimit = lowerMeasuringLimit,
                UpperMeasuringLimit = upperMeasuringLimit,
                TestLevelNumber = testLevelNumber,
                TestLevelSet = testLevelSet,
                Alive = alive,
                StartDate = new NullableDateTime(){IsNull = false,Value = startDate},
                TestOperationActive = testOperationActive
            };
        }

        public static DtoTypes.ProcessControlCondition CreateProcessControlConditionRandomized(int seed)
        {
            var randomizer = new Random(seed);
            return CreateProcessControlConditionDto(
                randomizer.Next(),
                new Location() { Id = randomizer.Next() },
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                randomizer.Next(),
                CreateTestLevelSetDtoRandomized(randomizer.Next()),
                randomizer.Next(0, 1) == 0,
                CreateDateTimeRandomized(randomizer.Next()),
                randomizer.Next(0, 1) == 0);
        }

        public static DtoTypes.QstProcessControlTech CreateQstProcessControlTechDto(long id, bool alive, TestMethod testMethod, ManufacturerIds manufacturerId, Extension extension, long processControlConditionId,
            double qstMinimumTorqueMt, double qstAlarmAngleMt, double qstAlarmAnglePa, double qstAlarmTorqueMt, double qstAlarmTorquePa, double qstAngleForFurtherTurningPa,
            long qstAngleLimitMt, double qstStartAngleCountingPa, double qstStartAngleMt, double qstStartMeasurementMt, double qstStartMeasurementPa, double qstStartMeasurementPeak, double qstTargetAnglePa)
        {
            return new DtoTypes.QstProcessControlTech()
            {
                Id = id,
                Alive = alive,
                TestMethod = (long) testMethod,
                ManufacturerId = (long) manufacturerId,
                Extension = extension,
                ProcessControlConditionId = processControlConditionId,
                QstMinimumTorqueMt = new NullableDouble(){IsNull = false, Value = qstMinimumTorqueMt },
                QstAlarmAngleMt = new NullableDouble() { IsNull = false, Value = qstAlarmAngleMt },
                QstAlarmAnglePa = new NullableDouble() { IsNull = false, Value = qstAlarmAnglePa },
                QstAlarmTorqueMt = new NullableDouble() { IsNull = false, Value = qstAlarmTorqueMt },
                QstAlarmTorquePa = new NullableDouble() { IsNull = false, Value = qstAlarmTorquePa },
                QstAngleForFurtherTurningPa = new NullableDouble() { IsNull = false, Value = qstAngleForFurtherTurningPa },
                QstAngleLimitMt = new NullableLong() { IsNull = false, Value = qstAngleLimitMt },
                QstStartAngleCountingPa = new NullableDouble() { IsNull = false, Value = qstStartAngleCountingPa },
                QstStartAngleMt = new NullableDouble() { IsNull = false, Value = qstStartAngleMt },
                QstStartMeasurementMt = new NullableDouble() { IsNull = false, Value = qstStartMeasurementMt },
                QstStartMeasurementPa = new NullableDouble() { IsNull = false, Value = qstStartMeasurementPa },
                QstStartMeasurementPeak = new NullableDouble() { IsNull = false, Value = qstStartMeasurementPeak },
                QstTargetAnglePa = new NullableDouble() { IsNull = false, Value = qstTargetAnglePa }
            };
        }

        public static DtoTypes.QstProcessControlTech CreateQstProcessControlTechRandomized(int seed)
        {
            var randomizer = new Random(seed);
            return CreateQstProcessControlTechDto(
                randomizer.Next(),
                randomizer.Next(0, 1) == 0,
                TestMethod.QST_MT,
                ManufacturerIds.ID_BLM,
                CreateExtensionRandomized(randomizer.Next()),
                randomizer.Next(),
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                randomizer.Next(),
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                randomizer.NextDouble());
        }

        public static DtoTypes.TestLevel CreateTestLevelDto(long id, bool considerWorkingCalendar, bool isActive,
            int sampleNumber, long intervalValue, long intervalType)
        {
            return new DtoTypes.TestLevel()
            {
                Id = id,
                ConsiderWorkingCalendar = considerWorkingCalendar,
                IsActive = isActive,
                SampleNumber = sampleNumber,
                TestInterval = new Interval()
                {
                    IntervalValue = intervalValue,
                    IntervalType = intervalType
                }
            };
        }

        public static DtoTypes.TestLevel CreateTestLevelDtoRandomized(int seed)
        {
            var randomizer = new Random(seed);
            return CreateTestLevelDto(
                randomizer.Next(),
                randomizer.Next(0, 1) == 0,
                randomizer.Next(0, 1) == 0,
                randomizer.Next(),
                randomizer.Next(0, 10),
                randomizer.Next(0, 5));
        }

        public static DtoTypes.TestLevelSet CreateTestLevelSetDto(long id, string name, DtoTypes.TestLevel testlevel1,
            DtoTypes.TestLevel testlevel2, DtoTypes.TestLevel testlevel3)
        {
            return new DtoTypes.TestLevelSet()
            {
                Id = id,
                Name = name,
                TestLevel1 = testlevel1,
                TestLevel2 = testlevel2,
                TestLevel3 = testlevel3
            };
        }

        public static DtoTypes.TestLevelSet CreateTestLevelSetDtoRandomized(int seed)
        {
            var randomizer = new Random(seed);
            return CreateTestLevelSetDto(
                randomizer.Next(),
                CreateStringRandomized(10, randomizer.Next()),
                CreateTestLevelDtoRandomized(randomizer.Next()),
                CreateTestLevelDtoRandomized(randomizer.Next()),
                CreateTestLevelDtoRandomized(randomizer.Next()));
        }

        public static DtoTypes.Extension CreateExtension(int id, string desc, string inventoryNumber, double bending, double factor, double length, bool alive)
        {
            return new DtoTypes.Extension()
            {
                Id = 1,
                Description = new NullableString() { IsNull = desc == null, Value = desc ?? "" },
                InventoryNumber = inventoryNumber,
                Bending = bending,
                FactorTorque = factor,
                Length = length,
                Alive = alive
            };
        }

        public static DtoTypes.Extension CreateExtensionRandomized(int seed)
        {
            var randomizer = new Random(seed);
            return CreateExtension(
                randomizer.Next(),
                CreateStringRandomized(randomizer.Next(0, 30), randomizer.Next()),
                CreateStringRandomized(randomizer.Next(0, 30), randomizer.Next()),
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                randomizer.Next(0, 1) == 1);
        }

        public static DtoTypes.ClassicProcessTest CreateProcessTest(long id, int numberOfTests, double nominalValueUnit1,
            double lowerLimitUnit1, double upperLimitUnit1, BasicTypes.DateTime timestamp)
        {
            return new DtoTypes.ClassicProcessTest()
            {
                Id = id,
                LowerLimitUnit1 = lowerLimitUnit1,
                NominalValueUnit1 = nominalValueUnit1,
                NumberOfTests = numberOfTests,
                UpperLimitUnit1 = upperLimitUnit1,
                Timestamp = timestamp
            };
        }

        public static DtoTypes.ClassicProcessTest CreateProcessTest(long id, int numberOfTests, double nominalValueUnit1,
            double lowerLimitUnit1, double upperLimitUnit1, BasicTypes.DateTime timestamp, double average,
            MeaUnit controlledByUnitId,
            double lowerInterventionLimitUnit1, double lowerInterventionLimitUnit2, double lowerLimitUnit2,
            double nominalValueUnit2,
            double standardDeviation, double testValueMaximum, double testValueMinimum, MeaUnit unit1Id,
            MeaUnit unit2Id,
            double upperInterventionLimitUnit1, double upperInterventionLimitUnit2, double upperLimitUnit2,
            DtoTypes.TestEquipment testEquipment, DtoTypes.ClassicTestLocation testLocation, long result,
            long toleranceClass1, long toleranceClass2)
        {
            return new DtoTypes.ClassicProcessTest()
            {
                Id = id,
                LowerLimitUnit1 = lowerLimitUnit1,
                NominalValueUnit1 = nominalValueUnit1,
                NumberOfTests = numberOfTests,
                UpperLimitUnit1 = upperLimitUnit1,
                Timestamp = timestamp,
                Average = average,
                ControlledByUnitId = (long)controlledByUnitId,
                LowerInterventionLimitUnit1 = lowerInterventionLimitUnit1,
                LowerInterventionLimitUnit2 = lowerInterventionLimitUnit2,
                LowerLimitUnit2 = lowerLimitUnit2,
                NominalValueUnit2 = nominalValueUnit2,
                StandardDeviation = new NullableDouble() { IsNull = false, Value = standardDeviation },
                TestValueMaximum = testValueMaximum,
                TestValueMinimum = testValueMinimum,
                Unit2Id = (long)unit2Id,
                Unit1Id = (long)unit1Id,
                UpperInterventionLimitUnit1 = upperInterventionLimitUnit1,
                UpperInterventionLimitUnit2 = upperInterventionLimitUnit2,
                Result = result,
                UpperLimitUnit2 = upperLimitUnit2,
                ToleranceClassUnit1 = toleranceClass1,
                ToleranceClassUnit2 = toleranceClass2,
                TestEquipment = testEquipment,
                TestLocation = testLocation
            };
        }

        public static DtoTypes.ClassicProcessTest CreateProcessTestRandomized(int seed)
        {
            var meaUnits = new List<MeaUnit>()
            {
                MeaUnit.Nm,
                MeaUnit.Deg
            };

            var randomizer = new Random(seed);
            return CreateProcessTest(
                randomizer.Next(),
                randomizer.Next(),
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                CreateDateTimeRandomized(seed),
                randomizer.NextDouble(),
                meaUnits[randomizer.Next(0, meaUnits.Count - 1)],
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                meaUnits[randomizer.Next(0, meaUnits.Count - 1)],
                meaUnits[randomizer.Next(0, meaUnits.Count - 1)],
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                CreateTestEquipmentDtoRandomized(seed),
                CreateClassicTestLocationRandomized(seed),
                randomizer.Next(),
                randomizer.Next(),
                randomizer.Next());
        }

        public static DtoTypes.ClassicTestLocation CreateClassicTestLocation(long locationId, long treeId, string path)
        {
            return new ClassicTestLocation()
            {
                LocationId = locationId,
                LocationDirectoryId = treeId,
                TreePath = new NullableString() { IsNull = false, Value = path }
            };
        }

        public static DtoTypes.ClassicTestLocation CreateClassicTestLocationRandomized(int seed)
        {
            var randomizer = new Random(seed);
            return CreateClassicTestLocation(randomizer.Next(), randomizer.Next(), CreateStringRandomized(10, seed));
        }
    }
}
