using Core.Entities;
using Core.Entities.ToolTypes;
using Core.Enums;
using InterfaceAdapters.Models;
using NUnit.Framework;
using TestHelper.Factories;

namespace InterfaceAdapters.Test.Models
{
    public class ToolModelModelTest
    {
        [Test]
        public void MapModelToEntityTest()
        {
            var toolModelModel = new ToolModelModel(new Core.Entities.ToolModel(), new NullLocalizationWrapper())
            {
                Id = 123,
                Description = "Des465",
                ModelType = new ClickWrenchModel(new NullLocalizationWrapper()),
                Class = ToolModelClassModel.CreateToolModelClassModelFromClass(ToolModelClass.DriverScale, new NullLocalizationWrapper()),
                MinPower = 987.5,
                MaxPower = 11356.987,
                AirPressure = 286.1,
                ToolType = HelperTableItemModel.GetModelForToolType(new ToolType() { ListId = new HelperTableEntityId(654), Value = new HelperTableDescription("ToolType") }),
                Weight = 987.321,
                BatteryVoltage = 369.258,
                MaxRotationSpeed = 951357,
                AirConsumption = 741.852,
                SwitchOff = HelperTableItemModel.GetModelForSwitchOff(new SwitchOff() { ListId = new HelperTableEntityId(654), Value = new HelperTableDescription("SwitchOff") }),
                DriveSize = HelperTableItemModel.GetModelForDriveSize(new DriveSize() { ListId = new HelperTableEntityId(85), Value = new HelperTableDescription("DriveSize") }),
                ShutOff = HelperTableItemModel.GetModelForShutOff(new ShutOff() { ListId = new HelperTableEntityId(936), Value = new HelperTableDescription("ShutOff") }),
                DriveType = HelperTableItemModel.GetModelForDriveType(new DriveType() { ListId = new HelperTableEntityId(852), Value = new HelperTableDescription("DriveType") }),
                ConstructionType = HelperTableItemModel.GetModelForConstructionType(new ConstructionType() { ListId = new HelperTableEntityId(258), Value = new HelperTableDescription("ConstructionType") }),
                Picture = new PictureModel(new Picture()) { FileName = "Name of File" }
            };

            var entity = toolModelModel?.Entity;

            Assert.AreEqual(toolModelModel.Id, entity.Id.ToLong());
            Assert.AreEqual(toolModelModel.Description, entity.Description.ToDefaultString());
            Assert.IsTrue(IsToolTypeEqualToToolTypeModel(entity.ModelType, toolModelModel.ModelType));
            Assert.AreEqual(toolModelModel.Class.ToolModelClass, entity.Class);
            Assert.AreEqual(toolModelModel.MinPower, entity.MinPower);
            Assert.AreEqual(toolModelModel.MaxPower, entity.MaxPower);
            Assert.AreEqual(toolModelModel.AirPressure, entity.AirPressure);
            Assert.AreEqual(toolModelModel.ToolType.ListId, entity.ToolType.ListId.ToLong());
            Assert.AreEqual(toolModelModel.Weight, entity.Weight);
            Assert.AreEqual(toolModelModel.BatteryVoltage, entity.BatteryVoltage);
            Assert.AreEqual(toolModelModel.MaxRotationSpeed, entity.MaxRotationSpeed);
            Assert.AreEqual(toolModelModel.AirConsumption, entity.AirConsumption);
            Assert.AreEqual(toolModelModel.SwitchOff.ListId, entity.SwitchOff.ListId.ToLong());
            Assert.AreEqual(toolModelModel.DriveSize.ListId, entity.DriveSize.ListId.ToLong());
            Assert.AreEqual(toolModelModel.ShutOff.ListId, entity.ShutOff.ListId.ToLong());
            Assert.AreEqual(toolModelModel.DriveType.ListId, entity.DriveType.ListId.ToLong());
            Assert.AreEqual(toolModelModel.ConstructionType.ListId, entity.ConstructionType.ListId.ToLong());
            Assert.AreEqual(toolModelModel.Picture.SeqId, entity.Picture.SeqId);
        }

        private static bool IsToolTypeEqualToToolTypeModel(AbstractToolType toolType,
            AbstractToolTypeModel toolTypeModel)
        {
            switch (toolType)
            {
                case ClickWrench clickWrench:
                    return toolTypeModel is ClickWrenchModel;
                case ECDriver ecDriver:
                    return toolTypeModel is ECDriverModel;
                case General general:
                    return toolTypeModel is GeneralModel;
                case MDWrench mdWrench:
                    return toolTypeModel is MDWrenchModel;
                case ProductionWrench productionWrench:
                    return toolTypeModel is ProductionWrenchModel;
                case PulseDriver pulseDriver:
                    return toolTypeModel is PulseDriverModel;
                case PulseDriverShutOff pulseDriverShutOff:
                    return toolTypeModel is PulseDriverShutOffModel;
                default:
                    return false;
            }
        }

        [Test]
        public void MapEntityToModelTest()
        {
            var entity = new Core.Entities.ToolModel()
            {
                Id = new ToolModelId(123),
                Description = new ToolModelDescription("Des465"),
                ModelType = new ClickWrench(),
                Class = Core.Enums.ToolModelClass.DriverFixSet,
                MinPower = 987.5,
                MaxPower = 11356.987,
                AirPressure = 286.1,
                ToolType = new ToolType() { ListId = new HelperTableEntityId(654), Value = new HelperTableDescription("ToolType") },
                Weight = 987.321,
                BatteryVoltage = 369.258,
                MaxRotationSpeed = 951357,
                AirConsumption = 741.852,
                SwitchOff = new SwitchOff() { ListId = new HelperTableEntityId(654), Value = new HelperTableDescription("SwitchOff") },
                DriveSize = new DriveSize() { ListId = new HelperTableEntityId(85), Value = new HelperTableDescription("DriveSize") },
                ShutOff = new ShutOff() { ListId = new HelperTableEntityId(936), Value = new HelperTableDescription("ShutOff") },
                DriveType = new DriveType() { ListId = new HelperTableEntityId(852), Value = new HelperTableDescription("DriveType") },
                ConstructionType = new ConstructionType() { ListId = new HelperTableEntityId(258), Value = new HelperTableDescription("ConstructionType") },
                Picture = new Picture() { FileName = "Name of File" }
            };

            var model = ToolModelModel.GetModelFor(entity, new NullLocalizationWrapper());

            Assert.AreEqual(entity.Id.ToLong(), model.Id);
            Assert.AreEqual(entity.Description.ToDefaultString(), model.Description);
            Assert.IsTrue(IsToolTypeEqualToToolTypeModel(entity.ModelType, model.ModelType));
            Assert.AreEqual(entity.Class, model.Class.ToolModelClass);
            Assert.AreEqual(entity.MinPower, model.MinPower);
            Assert.AreEqual(entity.MaxPower, model.MaxPower);
            Assert.AreEqual(entity.AirPressure, model.AirPressure);
            Assert.AreEqual(entity.ToolType.ListId.ToLong(), model.ToolType.ListId);
            Assert.AreEqual(entity.Weight, model.Weight);
            Assert.AreEqual(entity.BatteryVoltage, model.BatteryVoltage);
            Assert.AreEqual(entity.MaxRotationSpeed, model.MaxRotationSpeed);
            Assert.AreEqual(entity.AirConsumption, model.AirConsumption);
            Assert.AreEqual(entity.SwitchOff.ListId.ToLong(), model.SwitchOff.ListId);
            Assert.AreEqual(entity.DriveSize.ListId.ToLong(), model.DriveSize.ListId);
            Assert.AreEqual(entity.ShutOff.ListId.ToLong(), model.ShutOff.ListId);
            Assert.AreEqual(entity.DriveType.ListId.ToLong(), model.DriveType.ListId);
            Assert.AreEqual(entity.ConstructionType.ListId.ToLong(), model.ConstructionType.ListId);
        }
        
        [TestCase("TextNachUpdate", 5.742, 10.743, 3.654, 99.132, 15.436, 14, 12.123, 57489, 9823437)]
        [TestCase("AndererTextNachUpdate", 1.913, 12.515, 89.321, 25.625, 17.134, 13, 12.364, 4785621, 1235684)]
        public void AreToolModelModelsEqualTest(string description, double airPressure, double minPower, double maxPower, double batteryVoltage, double weight, long maxRotationSpeed, double airConsumption, double cmLimit, double cmkLimit)
        {
            var first = new ToolModelModel(new Core.Entities.ToolModel(), new NullLocalizationWrapper())
            {
                Id = 4,
                Description = description,
                ModelType = new ECDriverModel(new NullLocalizationWrapper()),
                Class = ToolModelClassModel.CreateToolModelClassModelFromClass(ToolModelClass.DriverScale, new NullLocalizationWrapper()),
                Manufacturer = new ManufacturerModel(new Manufacturer()) { Id = 1 },
                MinPower = minPower,
                MaxPower = maxPower,
                AirPressure = airPressure,
                ToolType = HelperTableItemModel.GetModelForToolType(new ToolType() { ListId = new HelperTableEntityId(96), Value = new HelperTableDescription("Test") }),
                Weight = weight,
                BatteryVoltage = batteryVoltage,
                MaxRotationSpeed = maxRotationSpeed,
                AirConsumption = airConsumption,
                SwitchOff = HelperTableItemModel.GetModelForSwitchOff(new SwitchOff() { ListId = new HelperTableEntityId(100), Value = new HelperTableDescription("Test") }),
                DriveSize = HelperTableItemModel.GetModelForDriveSize(new DriveSize() { ListId = new HelperTableEntityId(99), Value = new HelperTableDescription("Test") }),
                ShutOff = HelperTableItemModel.GetModelForShutOff(new ShutOff() { ListId = new HelperTableEntityId(95), Value = new HelperTableDescription("Test") }),
                DriveType = HelperTableItemModel.GetModelForDriveType(new DriveType() { ListId = new HelperTableEntityId(98), Value = new HelperTableDescription("Test") }),
                ConstructionType = HelperTableItemModel.GetModelForConstructionType(new ConstructionType() { ListId = new HelperTableEntityId(97), Value = new HelperTableDescription("Test") }),
                Picture = new PictureModel(new Picture()) { SeqId = 96 },
                CmkLimit = cmkLimit,
                CmLimit = cmLimit
            };
            var second = new ToolModelModel(new Core.Entities.ToolModel(), new NullLocalizationWrapper())
            {
                Id = 4,
                Description = description,
                ModelType = new ECDriverModel(new NullLocalizationWrapper()),
                Class = ToolModelClassModel.CreateToolModelClassModelFromClass(ToolModelClass.DriverScale, new NullLocalizationWrapper()),
                Manufacturer = new ManufacturerModel(new Manufacturer()) { Id = 1 },
                MinPower = minPower,
                MaxPower = maxPower,
                AirPressure = airPressure,
                ToolType = HelperTableItemModel.GetModelForToolType(new ToolType() { ListId = new HelperTableEntityId(96), Value = new HelperTableDescription("Test") }),
                Weight = weight,
                BatteryVoltage = batteryVoltage,
                MaxRotationSpeed = maxRotationSpeed,
                AirConsumption = airConsumption,
                SwitchOff = HelperTableItemModel.GetModelForSwitchOff(new SwitchOff() { ListId = new HelperTableEntityId(100), Value = new HelperTableDescription("Test") }),
                DriveSize = HelperTableItemModel.GetModelForDriveSize(new DriveSize() { ListId = new HelperTableEntityId(99), Value = new HelperTableDescription("Test") }),
                ShutOff = HelperTableItemModel.GetModelForShutOff(new ShutOff() { ListId = new HelperTableEntityId(95), Value = new HelperTableDescription("Test") }),
                DriveType = HelperTableItemModel.GetModelForDriveType(new DriveType() { ListId = new HelperTableEntityId(98), Value = new HelperTableDescription("Test") }),
                ConstructionType = HelperTableItemModel.GetModelForConstructionType(new ConstructionType() { ListId = new HelperTableEntityId(97), Value = new HelperTableDescription("Test") }),
                Picture = new PictureModel(new Picture()) { SeqId = 96 },
                CmkLimit = cmkLimit,
                CmLimit = cmLimit
            };

            Assert.IsTrue(first.EqualsByContent(second));
        }

        [TestCase(12, "desc", ToolModelClass.DriverScale, 23, 54.32, 98.76, 10, 45, 85.12, 96.13, 356, 46.91, 78, 45, 21, 89, 56, 32,
                  13, "desc", ToolModelClass.DriverScale, 23, 54.32, 98.76, 10, 45, 85.12, 96.13, 356, 46.91, 78, 45, 21, 89, 56, 32)]
        [TestCase(12, "desc", ToolModelClass.DriverScale, 23, 54.32, 98.76, 10, 45, 85.12, 96.13, 356, 46.91, 78, 45, 21, 89, 56, 32,
                  12, "asce", ToolModelClass.DriverScale, 23, 54.32, 98.76, 10, 45, 85.12, 96.13, 356, 46.91, 78, 45, 21, 89, 56, 32)]
        [TestCase(12, "desc", ToolModelClass.DriverScale, 23, 54.32, 98.76, 10, 45, 85.12, 96.13, 356, 46.91, 78, 45, 21, 89, 56, 32,
                  12, "desc", ToolModelClass.WrenchScale, 23, 54.32, 98.76, 10, 45, 85.12, 96.13, 356, 46.91, 78, 45, 21, 89, 56, 32)]
        [TestCase(12, "desc", ToolModelClass.DriverScale, 23, 54.32, 98.76, 10, 45, 85.12, 96.13, 356, 46.91, 78, 45, 21, 89, 56, 32,
                  12, "desc", ToolModelClass.DriverScale, 24, 54.32, 98.76, 10, 45, 85.12, 96.13, 356, 46.91, 78, 45, 21, 89, 56, 32)]
        [TestCase(12, "desc", ToolModelClass.DriverScale, 23, 54.32, 98.76, 10, 45, 85.12, 96.13, 356, 46.91, 78, 45, 21, 89, 56, 32,
                  12, "desc", ToolModelClass.DriverScale, 23, 55.32, 98.76, 10, 45, 85.12, 96.13, 356, 46.91, 78, 45, 21, 89, 56, 32)]
        [TestCase(12, "desc", ToolModelClass.DriverScale, 23, 54.32, 98.76, 10, 45, 85.12, 96.13, 356, 46.91, 78, 45, 21, 89, 56, 32,
                  12, "desc", ToolModelClass.DriverScale, 23, 54.32, 99.76, 10, 45, 85.12, 96.13, 356, 46.91, 78, 45, 21, 89, 56, 32)]
        [TestCase(12, "desc", ToolModelClass.DriverScale, 23, 54.32, 98.76, 10, 45, 85.12, 96.13, 356, 46.91, 78, 45, 21, 89, 56, 32,
                  12, "desc", ToolModelClass.DriverScale, 23, 54.32, 98.76, 11, 45, 85.12, 96.13, 356, 46.91, 78, 45, 21, 89, 56, 32)]
        [TestCase(12, "desc", ToolModelClass.DriverScale, 23, 54.32, 98.76, 10, 45, 85.12, 96.13, 356, 46.91, 78, 45, 21, 89, 56, 32,
                  12, "desc", ToolModelClass.DriverScale, 23, 54.32, 98.76, 10, 46, 85.12, 96.13, 356, 46.91, 78, 45, 21, 89, 56, 32)]
        [TestCase(12, "desc", ToolModelClass.DriverScale, 23, 54.32, 98.76, 10, 45, 85.12, 96.13, 356, 46.91, 78, 45, 21, 89, 56, 32,
                  12, "desc", ToolModelClass.DriverScale, 23, 54.32, 98.76, 10, 45, 86.12, 96.13, 356, 46.91, 78, 45, 21, 89, 56, 32)]
        [TestCase(12, "desc", ToolModelClass.DriverScale, 23, 54.32, 98.76, 10, 45, 85.12, 96.13, 356, 46.91, 78, 45, 21, 89, 56, 32,
                  12, "desc", ToolModelClass.DriverScale, 23, 54.32, 98.76, 10, 45, 85.12, 97.13, 356, 46.91, 78, 45, 21, 89, 56, 32)]
        [TestCase(12, "desc", ToolModelClass.DriverScale, 23, 54.32, 98.76, 10, 45, 85.12, 96.13, 356, 46.91, 78, 45, 21, 89, 56, 32,
                  12, "desc", ToolModelClass.DriverScale, 23, 54.32, 98.76, 10, 45, 85.12, 96.13, 357, 46.91, 78, 45, 21, 89, 56, 32)]
        [TestCase(12, "desc", ToolModelClass.DriverScale, 23, 54.32, 98.76, 10, 45, 85.12, 96.13, 356, 46.91, 78, 45, 21, 89, 56, 32,
                  12, "desc", ToolModelClass.DriverScale, 23, 54.32, 98.76, 10, 45, 85.12, 96.13, 356, 47.91, 78, 45, 21, 89, 56, 32)]
        [TestCase(12, "desc", ToolModelClass.DriverScale, 23, 54.32, 98.76, 10, 45, 85.12, 96.13, 356, 46.91, 78, 45, 21, 89, 56, 32,
                  12, "desc", ToolModelClass.DriverScale, 23, 54.32, 98.76, 10, 45, 85.12, 96.13, 356, 46.91, 79, 45, 21, 89, 56, 32)]
        [TestCase(12, "desc", ToolModelClass.DriverScale, 23, 54.32, 98.76, 10, 45, 85.12, 96.13, 356, 46.91, 78, 45, 21, 89, 56, 32,
                  12, "desc", ToolModelClass.DriverScale, 23, 54.32, 98.76, 10, 45, 85.12, 96.13, 356, 46.91, 78, 46, 21, 89, 56, 32)]
        [TestCase(12, "desc", ToolModelClass.DriverScale, 23, 54.32, 98.76, 10, 45, 85.12, 96.13, 356, 46.91, 78, 45, 21, 89, 56, 32,
                  12, "desc", ToolModelClass.DriverScale, 23, 54.32, 98.76, 10, 45, 85.12, 96.13, 356, 46.91, 78, 45, 22, 89, 56, 32)]
        [TestCase(12, "desc", ToolModelClass.DriverScale, 23, 54.32, 98.76, 10, 45, 85.12, 96.13, 356, 46.91, 78, 45, 21, 89, 56, 32,
                  12, "desc", ToolModelClass.DriverScale, 23, 54.32, 98.76, 10, 45, 85.12, 96.13, 356, 46.91, 78, 45, 21, 90, 56, 32)]
        [TestCase(12, "desc", ToolModelClass.DriverScale, 23, 54.32, 98.76, 10, 45, 85.12, 96.13, 356, 46.91, 78, 45, 21, 89, 56, 32,
                  12, "desc", ToolModelClass.DriverScale, 23, 54.32, 98.76, 10, 45, 85.12, 96.13, 356, 46.91, 78, 45, 21, 89, 57, 32)]
        [TestCase(12, "desc", ToolModelClass.DriverScale, 23, 54.32, 98.76, 10, 45, 85.12, 96.13, 356, 46.91, 78, 45, 21, 89, 56, 32,
                  12, "desc", ToolModelClass.DriverScale, 23, 54.32, 98.76, 10, 45, 85.12, 96.13, 356, 46.91, 78, 45, 21, 89, 56, 33)]
        [TestCase(12, "desc", -1, 23, 54.32, 98.76, 10, 45, 85.12, 96.13, 356, 46.91, 78, 45, 21, 89, 56, 32,
                  12, "desc", ToolModelClass.DriverScale, 23, 54.32, 98.76, 10, 45, 85.12, 96.13, 356, 46.91, 78, 45, 21, 89, 56, 32)]
        public void AreToolModelModelsNotEqualTest(long id,
                                                   string description, 
                                                   ToolModelClass toolModelClass,
                                                   long manufacturerId,
                                                   double minPower, 
                                                   double maxPower, 
                                                   double airPressure,
                                                   long toolTypeId,
                                                   double weight, 
                                                   double batteryVoltage, 
                                                   long maxRotationSpeed, 
                                                   double airConsumption, 
                                                   long switchOffId,
                                                   long driveSizeId,
                                                   long shutOffId,
                                                   long driveTypeId,
                                                   long constructionTypeId,
                                                   long pictureId,
                                                   long idTwo,
                                                   string descriptionTwo,
                                                   ToolModelClass toolModelClassTwo,
                                                   long manufacturerIdTwo,
                                                   double minPowerTwo,
                                                   double maxPowerTwo,
                                                   double airPressureTwo,
                                                   long toolTypeIdTwo,
                                                   double weightTwo,
                                                   double batteryVoltageTwo,
                                                   long maxRotationSpeedTwo,
                                                   double airConsumptionTwo,
                                                   long switchOffIdTwo,
                                                   long driveSizeIdTwo,
                                                   long shutOffIdTwo,
                                                   long driveTypeIdTwo,
                                                   long constructionTypeIdTwo,
                                                   long pictureIdTwo)
        {
            var first = new ToolModelModel(new Core.Entities.ToolModel(), new NullLocalizationWrapper())
            {
                Id = id,
                Description = description,
                ModelType = new ECDriverModel(new NullLocalizationWrapper()),
                Class = ToolModelClassModel.CreateToolModelClassModelFromClass(toolModelClass, new NullLocalizationWrapper()),
                Manufacturer = new ManufacturerModel(new Manufacturer()) { Id = manufacturerId },
                MinPower = minPower,
                MaxPower = maxPower,
                AirPressure = airPressure,
                ToolType = HelperTableItemModel.GetModelForToolType(new ToolType() { ListId = new HelperTableEntityId(toolTypeId), Value = new HelperTableDescription("Test") }),
                Weight = weight,
                BatteryVoltage = batteryVoltage,
                MaxRotationSpeed = maxRotationSpeed,
                AirConsumption = airConsumption,
                SwitchOff = HelperTableItemModel.GetModelForSwitchOff(new SwitchOff() { ListId = new HelperTableEntityId(switchOffId), Value = new HelperTableDescription("Test") }),
                DriveSize = HelperTableItemModel.GetModelForDriveSize(new DriveSize() { ListId = new HelperTableEntityId(driveSizeId), Value = new HelperTableDescription("Test") }),
                ShutOff = HelperTableItemModel.GetModelForShutOff(new ShutOff() { ListId = new HelperTableEntityId(shutOffId), Value = new HelperTableDescription("Test") }),
                DriveType = HelperTableItemModel.GetModelForDriveType(new DriveType() { ListId = new HelperTableEntityId(driveTypeId), Value = new HelperTableDescription("Test") }),
                ConstructionType = HelperTableItemModel.GetModelForConstructionType(new ConstructionType() { ListId = new HelperTableEntityId(constructionTypeId), Value = new HelperTableDescription("Test") }),
                Picture = new PictureModel(new Picture()) { SeqId = pictureId },
            };
            var second = new ToolModelModel(new Core.Entities.ToolModel(), new NullLocalizationWrapper())
            {
                Id = idTwo,
                Description = descriptionTwo,
                ModelType = new ECDriverModel(new NullLocalizationWrapper()),
                Class = ToolModelClassModel.CreateToolModelClassModelFromClass(toolModelClassTwo, new NullLocalizationWrapper()),
                Manufacturer = new ManufacturerModel(new Manufacturer()) { Id = manufacturerIdTwo },
                MinPower = minPowerTwo,
                MaxPower = maxPowerTwo,
                AirPressure = airPressureTwo,
                ToolType = HelperTableItemModel.GetModelForToolType(new ToolType() { ListId = new HelperTableEntityId(toolTypeIdTwo), Value = new HelperTableDescription("Test") }),
                Weight = weightTwo,
                BatteryVoltage = batteryVoltageTwo,
                MaxRotationSpeed = maxRotationSpeedTwo,
                AirConsumption = airConsumptionTwo,
                SwitchOff = HelperTableItemModel.GetModelForSwitchOff(new SwitchOff() { ListId = new HelperTableEntityId(switchOffIdTwo), Value = new HelperTableDescription("Test") }),
                DriveSize = HelperTableItemModel.GetModelForDriveSize(new DriveSize() { ListId = new HelperTableEntityId(driveSizeIdTwo), Value = new HelperTableDescription("Test") }),
                ShutOff = HelperTableItemModel.GetModelForShutOff(new ShutOff() { ListId = new HelperTableEntityId(shutOffIdTwo), Value = new HelperTableDescription("Test") }),
                DriveType = HelperTableItemModel.GetModelForDriveType(new DriveType() { ListId = new HelperTableEntityId(driveTypeIdTwo), Value = new HelperTableDescription("Test") }),
                ConstructionType = HelperTableItemModel.GetModelForConstructionType(new ConstructionType() { ListId = new HelperTableEntityId(constructionTypeIdTwo), Value = new HelperTableDescription("Test") }),
                Picture = new PictureModel(new Picture()) { SeqId = pictureIdTwo },
            };

            Assert.IsFalse(first.EqualsByContent(second));
        }

        [TestCase("TextNachUpdate", 5.742, 10.743, 3.654, 99.132, 15.436, 14, 12.123, 57489, 9823437)]
        [TestCase("AndererTextNachUpdate", 1.913, 12.515, 89.321, 25.625, 17.134, 13, 12.364, 4785621, 1235684)]
        public void AreToolModelModelsNotEqualBecauseOfModelTypeTest(string description, double airPressure, double minPower, double maxPower, double batteryVoltage, double weight, long maxRotationSpeed, double airConsumption, double cmLimit, double cmkLimit)
        {
            var first = new ToolModelModel(new Core.Entities.ToolModel(), new NullLocalizationWrapper())
            {
                Id = 4,
                Description = description,
                ModelType = new ECDriverModel(new NullLocalizationWrapper()),
                Class = ToolModelClassModel.CreateToolModelClassModelFromClass(ToolModelClass.DriverScale, new NullLocalizationWrapper()),
                Manufacturer = new ManufacturerModel(new Manufacturer()) { Id = 1 },
                MinPower = minPower,
                MaxPower = maxPower,
                AirPressure = airPressure,
                ToolType = HelperTableItemModel.GetModelForToolType(new ToolType() { ListId = new HelperTableEntityId(96), Value = new HelperTableDescription("Test") }),
                Weight = weight,
                BatteryVoltage = batteryVoltage,
                MaxRotationSpeed = maxRotationSpeed,
                AirConsumption = airConsumption,
                SwitchOff = HelperTableItemModel.GetModelForSwitchOff(new SwitchOff() { ListId = new HelperTableEntityId(100), Value = new HelperTableDescription("Test") }),
                DriveSize = HelperTableItemModel.GetModelForDriveSize(new DriveSize() { ListId = new HelperTableEntityId(99), Value = new HelperTableDescription("Test") }),
                ShutOff = HelperTableItemModel.GetModelForShutOff(new ShutOff() { ListId = new HelperTableEntityId(95), Value = new HelperTableDescription("Test") }),
                DriveType = HelperTableItemModel.GetModelForDriveType(new DriveType() { ListId = new HelperTableEntityId(98), Value = new HelperTableDescription("Test") }),
                ConstructionType = HelperTableItemModel.GetModelForConstructionType(new ConstructionType() { ListId = new HelperTableEntityId(97), Value = new HelperTableDescription("Test") }),
                Picture = new PictureModel(new Picture()) { SeqId = 96 },
                CmkLimit = cmkLimit,
                CmLimit = cmLimit
            };
            var second = new ToolModelModel(new Core.Entities.ToolModel(), new NullLocalizationWrapper())
            {
                Id = 4,
                Description = description,
                ModelType = new GeneralModel(new NullLocalizationWrapper()),
                Class = ToolModelClassModel.CreateToolModelClassModelFromClass(ToolModelClass.DriverScale, new NullLocalizationWrapper()),
                Manufacturer = new ManufacturerModel(new Manufacturer()) { Id = 1 },
                MinPower = minPower,
                MaxPower = maxPower,
                AirPressure = airPressure,
                ToolType = HelperTableItemModel.GetModelForToolType(new ToolType() { ListId = new HelperTableEntityId(96), Value = new HelperTableDescription("Test") }),
                Weight = weight,
                BatteryVoltage = batteryVoltage,
                MaxRotationSpeed = maxRotationSpeed,
                AirConsumption = airConsumption,
                SwitchOff = HelperTableItemModel.GetModelForSwitchOff(new SwitchOff() { ListId = new HelperTableEntityId(100), Value = new HelperTableDescription("Test") }),
                DriveSize = HelperTableItemModel.GetModelForDriveSize(new DriveSize() { ListId = new HelperTableEntityId(99), Value = new HelperTableDescription("Test") }),
                ShutOff = HelperTableItemModel.GetModelForShutOff(new ShutOff() { ListId = new HelperTableEntityId(95), Value = new HelperTableDescription("Test") }),
                DriveType = HelperTableItemModel.GetModelForDriveType(new DriveType() { ListId = new HelperTableEntityId(98), Value = new HelperTableDescription("Test") }),
                ConstructionType = HelperTableItemModel.GetModelForConstructionType(new ConstructionType() { ListId = new HelperTableEntityId(97), Value = new HelperTableDescription("Test") }),
                Picture = new PictureModel(new Picture()) { SeqId = 96 },
                CmkLimit = cmkLimit,
                CmLimit = cmLimit
            };

            Assert.IsFalse(first.EqualsByContent(second));
        }

        [TestCase(12, "desc", ToolModelClass.DriverScale, 23, 54.32, 98.76, 10, 45, 85.12, 96.13, 356, 46.91, 78, 45, 21, 89, 56, 32, 28.17, 39.37)]
        [TestCase(13, "asce", ToolModelClass.WrenchScale, 24, 55.32, 99.76, 11, 46, 86.12, 97.13, 357, 47.91, 79, 46, 22, 90, 57, 33, 29.17, 40.37)]
        public void CopyToolModelTest(long id,
                                      string description,
                                      ToolModelClass toolModelClass,
                                      long manufacturerId,
                                      double minPower,
                                      double maxPower,
                                      double airPressure,
                                      long toolTypeId,
                                      double weight,
                                      double batteryVoltage,
                                      long maxRotationSpeed,
                                      double airConsumption,
                                      long switchOffId,
                                      long driveSizeId,
                                      long shutOffId,
                                      long driveTypeId,
                                      long constructionTypeId,
                                      long pictureId,
                                      double cmLimit,
                                      double cmkLimit)
        {
            var model = new ToolModelModel(new Core.Entities.ToolModel(), new NullLocalizationWrapper())
            {
                Id = 4,
                Description = description,
                ModelType = new ECDriverModel(new NullLocalizationWrapper()),
                Class = ToolModelClassModel.CreateToolModelClassModelFromClass(ToolModelClass.DriverScale, new NullLocalizationWrapper()),
                Manufacturer = new ManufacturerModel(new Manufacturer()) { Id = 1, Name = "Test" },
                MinPower = minPower,
                MaxPower = maxPower,
                AirPressure = airPressure,
                ToolType = HelperTableItemModel.GetModelForToolType(new ToolType() { ListId = new HelperTableEntityId(96), Value = new HelperTableDescription("Test") }),
                Weight = weight,
                BatteryVoltage = batteryVoltage,
                MaxRotationSpeed = maxRotationSpeed,
                AirConsumption = airConsumption,
                SwitchOff = HelperTableItemModel.GetModelForSwitchOff(new SwitchOff() { ListId = new HelperTableEntityId(100), Value = new HelperTableDescription("Test") }),
                DriveSize = HelperTableItemModel.GetModelForDriveSize(new DriveSize() { ListId = new HelperTableEntityId(99), Value = new HelperTableDescription("Test") }),
                DriveType = HelperTableItemModel.GetModelForDriveType(new DriveType() { ListId = new HelperTableEntityId(98), Value = new HelperTableDescription("Test") }),
                ConstructionType = HelperTableItemModel.GetModelForConstructionType(new ConstructionType() { ListId = new HelperTableEntityId(97), Value = new HelperTableDescription("Test") }),
                Picture = new PictureModel(new Picture()) { SeqId = 96 },
                CmkLimit = cmkLimit,
                CmLimit = cmLimit
            };

            var copy = model.CopyDeep();

            Assert.IsFalse(model == copy);
            Assert.IsTrue(model.EqualsByContent(copy));
        }


        [Test]
        public void SetToolModelModelClassToNullSetsEntityClassToMinusOne()
        {
            var model = new ToolModelModel(CreateToolModel.Randomized(324536), new NullLocalizationWrapper());
            Assert.AreNotEqual(-1, model.Class.ToolModelClass);

            model.Class = null;
            Assert.AreEqual(-1, (long)model.Entity.Class);
        }
    }
}
