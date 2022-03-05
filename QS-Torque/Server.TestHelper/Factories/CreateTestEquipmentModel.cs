using System;
using Server.Core.Entities;

namespace Server.TestHelper.Factories
{
    public class CreateTestEquipmentModel
    {
        public static TestEquipmentModel Parametrized(long id, string testEquipmentModelName, string communicationFilePath, string driverProgramPath, Manufacturer manufacturer)
        {
            return new TestEquipmentModel()
            {
                Id = new TestEquipmentModelId(id),
                TestEquipmentModelName = new TestEquipmentModelName(testEquipmentModelName),
                CommunicationFilePath = new TestEquipmentSetupPath(communicationFilePath),
                DriverProgramPath = new TestEquipmentSetupPath(driverProgramPath),
                Manufacturer = manufacturer
            };
        }

        public static TestEquipmentModel Randomized(int seed)
        {
            var randomizer = new Random(seed);

            return Parametrized(randomizer.Next(),
                CreateString.Randomized((int)(randomizer.NextDouble() * 5), randomizer.Next()),
                CreateString.Randomized((int)(randomizer.NextDouble() * 5), randomizer.Next()),
                CreateString.Randomized((int)(randomizer.NextDouble() * 5), randomizer.Next()),
                CreateManufacturer.Randomized(randomizer.Next())
            );
        }
    }
}
