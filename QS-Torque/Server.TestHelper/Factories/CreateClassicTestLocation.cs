using System;
using Server.Core.Entities;

namespace Server.TestHelper.Factories
{
    public class CreateClassicTestLocation
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
