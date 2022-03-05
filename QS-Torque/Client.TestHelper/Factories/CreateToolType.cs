using Core.Entities;
using System;

namespace TestHelper.Factories
{
    public class CreateToolType
    {
        public static ToolType Anonymous()
        {
            return Parametrized(789, "TestValue");
        }

        public static ToolType Parametrized(int id, string description)
        {
            return new ToolType() { ListId = new HelperTableEntityId(id), Value = new HelperTableDescription(description) };
        }

        public static ToolType DescriptionOnly(string description)
        {
            return new ToolType() { Value = new HelperTableDescription(description) };
        }

        public static ToolType Randomized(int seed)
        {
            var randomizer = new Random(seed);
            return Parametrized(
                randomizer.Next(), 
                CreateString.Randomized(randomizer.Next(0,50), randomizer.Next()));
        }
    }

    public class CreateConstructionType
    {
        public static ConstructionType Parametrized(int id, string description)
        {
            return new ConstructionType() { ListId = new HelperTableEntityId(id), Value = new HelperTableDescription(description) };
        }

        public static ConstructionType Randomized(int seed)
        {
            var randomizer = new Random(seed);
            return Parametrized(
                randomizer.Next(),
                CreateString.Randomized(randomizer.Next(0, 50), randomizer.Next()));
        }
    }

    public class CreateDriveSize
    {
        public static DriveSize Parametrized(int id, string description)
        {
            return new DriveSize() { ListId = new HelperTableEntityId(id), Value = new HelperTableDescription(description) };
        }

        public static DriveSize Randomized(int seed)
        {
            var randomizer = new Random(seed);
            return Parametrized(
                randomizer.Next(),
                CreateString.Randomized(randomizer.Next(0, 50), randomizer.Next()));
        }
    }

    public class CreateDriveType
    {
        public static DriveType Parametrized(int id, string description)
        {
            return new DriveType() { ListId = new HelperTableEntityId(id), Value = new HelperTableDescription(description) };
        }

        public static DriveType Randomized(int seed)
        {
            var randomizer = new Random(seed);
            return Parametrized(
                randomizer.Next(),
                CreateString.Randomized(randomizer.Next(0, 50), randomizer.Next()));
        }
    }

    public class CreateShutOff
    {
        public static ShutOff Parametrized(int id, string description)
        {
            return new ShutOff() { ListId = new HelperTableEntityId(id), Value = new HelperTableDescription(description) };
        }

        public static ShutOff Randomized(int seed)
        {
            var randomizer = new Random(seed);
            return Parametrized(
                randomizer.Next(),
                CreateString.Randomized(randomizer.Next(0, 50), randomizer.Next()));
        }
    }

    public class CreateSwitchOff
    {
        public static SwitchOff Parametrized(int id, string description)
        {
            return new SwitchOff() { ListId = new HelperTableEntityId(id), Value = new HelperTableDescription(description) };
        }

        public static SwitchOff Randomized(int seed)
        {
            var randomizer = new Random(seed);
            return Parametrized(
                randomizer.Next(),
                CreateString.Randomized(randomizer.Next(0, 50), randomizer.Next()));
        }
    }
}
