using System;
using System.Collections.Generic;
using Common.Types.Enums;
using Core.Enums;
using Server.Core.Entities;

namespace Server.TestHelper.Factories
{
    public class CreateToolModel
    {
        public static ToolModel Parameterized(
            long id,
            string descritpion,
            Manufacturer manufacturer,
            double? minPower,
            double? maxPower,
            double? airPressure,
            ToolType toolType,
            double weight,
            double? batteryVoltage,
            long? maxRotationSpeed,
            double? airConsumption,
            ShutOff shutOff,
            DriveSize driveSize,
            SwitchOff switchOff,
            DriveType driveType,
            ConstructionType constructionType,
            double cmLimit,
            double cmkLimit,
            bool alive,
            ToolModelType modelType,
            ToolModelClass modelClass)
        {
            return new ToolModel
            {
                Id = new ToolModelId(id),
                Description = descritpion,
                Manufacturer = manufacturer,
                MinPower = minPower,
                MaxPower = maxPower,
                AirPressure = airPressure,
                ToolType = toolType,
                Weight = weight,
                BatteryVoltage = batteryVoltage,
                MaxRotationSpeed = maxRotationSpeed,
                AirConsumption = airConsumption,
                ShutOff = shutOff,
                DriveSize = driveSize,
                SwitchOff = switchOff,
                DriveType = driveType,
                ConstructionType = constructionType,
                CmLimit = cmLimit,
                CmkLimit = cmkLimit,
                Alive = alive,
                ModelType = modelType,
                Class = modelClass
            };
        }

        public static ToolModel Anonymous()
        {
            return Parameterized(
                897645,
                "12345",
                CreateManufacturer.Anonymous(),
                null,
                null,
                null,
                null,
                237.5,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                5,
                6,
                true,
                ToolModelType.ClickWrench,
                ToolModelClass.DriverFixSet);
        }

        private static double? RandomNullableDouble(Random randomizer)
        {
            if (randomizer.Next() % 2 == 0)
            {
                return randomizer.NextDouble();
            }
            return null;
        }

        private static long? RandomNullableLong(Random randomizer)
        {
            if (randomizer.Next() % 2 == 0)
            {
                return randomizer.Next( );
            }
            return null;
        }

        public static ToolModel Randomized(int seed)
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
            return Parameterized(randomizer.Next(),
                CreateString.Randomized((int) (randomizer.NextDouble() * 50), randomizer.Next()),
                CreateManufacturer.Randomized(randomizer.Next()),
                RandomNullableDouble(randomizer),
                RandomNullableDouble(randomizer),
                RandomNullableDouble(randomizer),
                CreateToolTypeRandomized(randomizer.Next()),
                randomizer.NextDouble(),
                RandomNullableDouble(randomizer),
                RandomNullableLong(randomizer),
                RandomNullableDouble(randomizer),
                CreateShutOffRandomized(randomizer.Next()),
                CreateDriveSizeRandomized(randomizer.Next()),
                CreateSwitchOffRandomized(randomizer.Next()),
                CreateDriveTypeRandomized(randomizer.Next()),
                CreateConstructionTypeRandomized(randomizer.Next()),
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                randomizer.Next()%2==0,
                toolModelTypes[randomizer.Next(0, toolModelTypes.Count)],
                toolModelClasses[randomizer.Next(0, toolModelClasses.Count)]);
        }

        private static ToolType CreateToolTypeRandomized(int seed)
        {
            var nodeid = State.NodeId.ToolType;
            var randomizer = new Random(seed);
            return new ToolType
            {
                Alive = (randomizer.Next() % 1) == 1,
                ListId = new HelperTableEntityId(randomizer.Next()),
                NodeId = nodeid,
                Value = new HelperTableEntityValue(CreateString.Randomized(randomizer.Next(0, 50), randomizer.Next()))
            };
        }

        private static ShutOff CreateShutOffRandomized(int seed)
        {
            var nodeid = State.NodeId.ShutOff;
            var randomizer = new Random(seed);
            return new ShutOff
            {
                Alive = (randomizer.Next() % 1) == 1,
                ListId = new HelperTableEntityId(randomizer.Next()),
                NodeId = nodeid,
                Value = new HelperTableEntityValue(CreateString.Randomized(randomizer.Next(0, 50), randomizer.Next()))
            };
        }

        private static SwitchOff CreateSwitchOffRandomized(int seed)
        {
            var nodeid = State.NodeId.SwitchOff;
            var randomizer = new Random(seed);
            return new SwitchOff
            {
                Alive = (randomizer.Next() % 1) == 1,
                ListId = new HelperTableEntityId(randomizer.Next()),
                NodeId = nodeid,
                Value = new HelperTableEntityValue(CreateString.Randomized(randomizer.Next(0, 50), randomizer.Next()))
            };
        }

        private static DriveType CreateDriveTypeRandomized(int seed)
        {
            var nodeid = State.NodeId.DriveType;
            var randomizer = new Random(seed);
            return new DriveType
            {
                Alive = (randomizer.Next() % 1) == 1,
                ListId = new HelperTableEntityId(randomizer.Next()),
                NodeId = nodeid,
                Value = new HelperTableEntityValue(CreateString.Randomized(randomizer.Next(0, 50), randomizer.Next()))
            };
        }

        private static ConstructionType CreateConstructionTypeRandomized(int seed)
        {
            var nodeid = State.NodeId.ConstructionType;
            var randomizer = new Random(seed);
            return new ConstructionType
            {
                Alive = (randomizer.Next() % 1) == 1,
                ListId = new HelperTableEntityId(randomizer.Next()),
                NodeId = nodeid,
                Value = new HelperTableEntityValue(CreateString.Randomized(randomizer.Next(0, 50), randomizer.Next()))
            };
        }

        private static DriveSize CreateDriveSizeRandomized(int seed)
        {
            var nodeid = State.NodeId.DriveSize;
            var randomizer = new Random(seed);
            return new DriveSize
            {
                Alive = (randomizer.Next() % 1) == 1,
                ListId = new HelperTableEntityId(randomizer.Next()),
                NodeId = nodeid,
                Value = new HelperTableEntityValue(CreateString.Randomized(randomizer.Next(0, 50), randomizer.Next()))
            };
        }
    }
}
