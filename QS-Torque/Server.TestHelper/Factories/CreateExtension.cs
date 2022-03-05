using System;
using Server.Core.Entities;

namespace Server.TestHelper.Factories
{
    public class CreateExtension
    {
        public static Extension Parametrized(long id, string description, string inventoryNumber, double bending, double factorTorque, double length)
        {
            return new Extension { Id = new ExtensionId(id), Description = description, InventoryNumber = new ExtensionInventoryNumber(inventoryNumber), Bending = bending, FactorTorque = bending, Length = length };
        }
        public static Extension Randomized(int seed)
        {
            var randomizer = new Random(seed);
            return Parametrized(
                randomizer.Next(),
                CreateString.Randomized(randomizer.Next(0, 10), randomizer.Next()),
                CreateString.Randomized(randomizer.Next(0, 10), randomizer.Next()),
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                randomizer.NextDouble());
        }

        public static Extension Anonymous()
        {
            return Parametrized(99, "", "", 0, 0, 0);
        }

        public static Extension WithId(long id)
        {
            var extension = Anonymous();
            extension.Id = new ExtensionId(id);
            return extension;
        }
    }
}
