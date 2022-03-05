using System;
using Client.Core.Entities;
using Common.Types.Enums;
using Core.Entities;
using TestHelper.Factories;

namespace Client.TestHelper.Factories
{
    public class CreateExtension : AbstractEntityFactory
    {
        public static Extension Parametrized(long id, string desc, string inventoryNumber, double bending, double factorTorque, double length)
        {
            return new Extension()
            {
                Id = new ExtensionId(id),
                Description = desc,
                InventoryNumber = new ExtensionInventoryNumber(inventoryNumber),
                Bending = bending,
                FactorTorque = factorTorque,
                Length = length
            };
        }

        public static Extension RandomizedWithDescription(int seed, string desc)
        {
            var extension = Randomized(seed);
            extension.Description = desc;
            return extension;
        }

        public static Extension Randomized(int seed)
        {
            var randomizer = new Random(seed);

            return Parametrized(randomizer.Next(),
                CreateString.Randomized(randomizer.Next(0, 10), randomizer.Next()),
                CreateString.Randomized(randomizer.Next(0, 10), randomizer.Next()),
                randomizer.Next(0,99),
                randomizer.Next(),
                randomizer.Next());
        }

        public static Extension WithId(int id)
        {
            return Parametrized(id, "a", "b", 0, 0, 0);
        }

        public static Extension RandomizedWithId(int seed, int id)
        {
            var extension = Randomized(seed);
            extension.Id = new ExtensionId(id);
            return extension;
        }

        public static Extension RandomizedWithInventoryNumber(int seed, string inventoryNumber)
        {
            var extension = Randomized(seed);
            extension.InventoryNumber = new ExtensionInventoryNumber(inventoryNumber);
            return extension;
        }

        public static Extension RandomizedWithIdAndInventoryNumber(int seed, long id, string inventoryNumber)
        {
            var extension = Randomized(seed);
            extension.InventoryNumber = new ExtensionInventoryNumber(inventoryNumber);
            extension.Id = new ExtensionId(id);
            return extension;
        }
    }
}
