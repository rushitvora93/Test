using System;
using Core.Entities;
using TestHelper.Factories;

namespace Client.TestHelper.Factories
{
    public class CreateClassicTestLocation : AbstractEntityFactory
    {
        public static ClassicTestLocation Parameterized(long locationId, long treeId, string path)
        {
            return new ClassicTestLocation()
            {
                LocationId = new LocationId(locationId),
                LocationDirectoryId = new LocationDirectoryId(treeId),
                LocationTreePath = path
            };
        }

        public static ClassicTestLocation Randomized(int seed)
        {
            var randomizer = new Random(seed);
            return Parameterized(
                randomizer.Next(),
                randomizer.Next(),
                CreateString.Randomized(randomizer.Next(0, 30), randomizer.Next()));
        }
    }
}
