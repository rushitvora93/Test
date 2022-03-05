using System;
using Server.Core.Entities;

namespace Server.TestHelper.Factories
{
    public class CreateProcessControlCondition
    {
        public static ProcessControlCondition Parametrized(long id, long locationId, double upperMeasuringLimit,
            double lowerMeasuringLimit, double upperInterventionLimit, double lowerInterventionLimit, TestLevelSet testLevelSet,
            int testLevelNumber, ProcessControlTech processControlTech, DateTime? startDate, bool testOperationActive)
        {
            return new ProcessControlCondition()
            {
                Id = new ProcessControlConditionId(id),
                Location = new Location() { Id = new LocationId(locationId) },
                UpperMeasuringLimit = upperMeasuringLimit,
                LowerMeasuringLimit = lowerMeasuringLimit,
                UpperInterventionLimit = upperInterventionLimit,
                LowerInterventionLimit = lowerInterventionLimit,
                TestLevelSet = testLevelSet,
                TestLevelNumber = testLevelNumber,
                ProcessControlTech = processControlTech,
                StartDate = startDate,
                TestOperationActive = testOperationActive
            };
        }

        public static ProcessControlCondition Randomized(int seed)
        {
            var randomizer = new Random(seed);
            return Parametrized(
                randomizer.Next(),
                randomizer.Next(),
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                CreateTestLevelSet.Randomized(randomizer.Next()),
                randomizer.Next(),
                CreateQstProcessControlTech.Randomized(randomizer.Next()),
                DateTime.Now,
                randomizer.Next(0, 1) == 0);
        }
    }
}
