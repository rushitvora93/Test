using System;
using System.Collections.Generic;
using Client.TestHelper.Factories;
using Core.Entities;
using Core.Entities.ToolTypes;
using Core.Enums;

namespace TestHelper.Factories
{
    public class CreateToolModel : AbstractEntityFactory
    {
        public static ToolModel Parameterized(
            long id,
            AbstractToolType modelType,
            string description,
            ToolType toolType,
            Manufacturer manufacturer,
            Picture picture,
            ToolModelClass toolModelClass,
            double? minPower,
            double? maxPower,
            double? airPressure,
            double? batteryVoltage,
            long? maxRotationSpeed,
            double weight,
            double? airConsumption,
            double cmLimit,
            double cmkLimit,
            ConstructionType constructionType,
            DriveSize driveSize,
            DriveType driveType,
            ShutOff shutOff,
            SwitchOff switchOff)
        {
            return new ToolModel
            {
                Id = new ToolModelId(id),
                ModelType = modelType,
                Description = new ToolModelDescription(description),
                ToolType = toolType,
                Manufacturer = manufacturer,
                Picture = picture,
                Class = toolModelClass,
                MinPower = minPower,
                MaxPower = maxPower,
                AirPressure = airPressure,
                BatteryVoltage =  batteryVoltage,
                MaxRotationSpeed = maxRotationSpeed,
                Weight = weight,
                AirConsumption = airConsumption,
                CmLimit = cmLimit,
                CmkLimit = cmkLimit,
                ConstructionType = constructionType,
                DriveSize = driveSize,
                DriveType = driveType,
                ShutOff = shutOff,
                SwitchOff = switchOff
            };
        }

        public static ToolModel Anonymous()
        {
            return Parameterized(
                897645,
                new General(),
                "12345", 
                null,
                CreateManufacturer.Anonymous(),
                null,
                ToolModelClass.DriverFixSet,
                1,
                2,
                3,
                4,
                5,
                0.0,
                6,
                0.0,
                0.0,
                null,
                null,
                null,
                null,
                null
                );
        }

        

        public static ToolModel Randomized(int seed)
        {
            var randomizer = new Random(seed);

            List<ToolModelClass> toolModelClasses = new List<ToolModelClass>
            {
                ToolModelClass.DriverFixSet,
                ToolModelClass.DriverScale,
                ToolModelClass.DriverWithoutScale,
                ToolModelClass.WrenchFixSet,
                ToolModelClass.WrenchScale,
                ToolModelClass.WrenchWithoutScale
            };

            ConstructionType thing = new ConstructionType{};

            return Parameterized(
                randomizer.Next(),
                RandomAbstractToolType(randomizer),
                CreateString.Randomized(randomizer.Next(0,40), randomizer.Next()),
                CreateToolType.Randomized(randomizer.Next()),
                CreateManufacturer.Randomized(randomizer.Next()),
                null, //TODO: random picture?
                toolModelClasses[randomizer.Next(0,  toolModelClasses.Count)],
                RandomNullableDouble(randomizer),
                RandomNullableDouble(randomizer),
                RandomNullableDouble(randomizer),
                RandomNullableDouble(randomizer),
                RandomNullableLong(randomizer),
                randomizer.NextDouble(),
                RandomNullableDouble(randomizer),
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                CreateConstructionType.Randomized(randomizer.Next()),
                CreateDriveSize.Randomized(randomizer.Next()),
                CreateDriveType.Randomized(randomizer.Next()),
                CreateShutOff.Randomized(randomizer.Next()),
                CreateSwitchOff.Randomized(randomizer.Next()));
        }

        private static AbstractToolType RandomAbstractToolType(Random randomizer)
        {
            AbstractToolType toolType = null;
            switch (randomizer.Next(0, 7))
            {
                case 0:
                    toolType = new PulseDriver();
                    break;
                case 1:
                    toolType = new PulseDriverShutOff();
                    break;
                case 2:
                    toolType = new General();
                    break;
                case 3:
                    toolType = new ECDriver();
                    break;
                case 4:
                    toolType = new ClickWrench();
                    break;
                case 5:
                    toolType = new MDWrench();
                    break;
                case 6:
                    toolType = new ProductionWrench();
                    break;
            }

            return toolType;
        }

        public static ToolModel WithId(long id)
        {
            var toolModel = Anonymous();
            toolModel.Id = new ToolModelId(id);
            return toolModel;
        }

        public static ToolModel RandomizedWithType(int seed, AbstractToolType modelTyp)
        {
            var toolModel = Randomized(seed);
            toolModel.ModelType = modelTyp;
            return toolModel;
        }

        public static ToolModel RandomizedWithTypeAndClass(int seed, AbstractToolType modelTyp, ToolModelClass toolModelClass)
        {
            var toolModel = Randomized(seed);
            toolModel.ModelType = modelTyp;
            toolModel.Class = toolModelClass;
            return toolModel;
        }
    }
}
