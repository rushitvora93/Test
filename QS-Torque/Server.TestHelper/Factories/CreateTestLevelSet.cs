using System;
using Server.Core.Entities;

namespace Server.TestHelper.Factories
{
    public class CreateTestLevelSet
    {
        public static TestLevelSet Parametrized(long id, string name, TestLevel testLevel1, TestLevel testLevel2, TestLevel testLevel3)
        {
            return new TestLevelSet()
            {
                Id = new TestLevelSetId(id),
                Name = new TestLevelSetName(name),
                TestLevel1 = testLevel1,
                TestLevel2 = testLevel2,
                TestLevel3 = testLevel3
            };
        }

        public static TestLevelSet Randomized(int seed)
        {
            var randomizer = new Random(seed);
            return Parametrized(
                randomizer.Next(),
                CreateString.Randomized(randomizer.Next(0, 10), randomizer.Next()),
                CreateTestLevel.Randomized(randomizer.Next()),
                CreateTestLevel.Randomized(randomizer.Next()),
                CreateTestLevel.Randomized(randomizer.Next()));
        }
    }
}
