using System;
using Client.Core.Entities;
using Core.Entities;
using Core.PhysicalValueTypes;

namespace Client.TestHelper.Factories
{
    public class CreateProcessControlCondition
    {
        public static ProcessControlCondition Parametrized(long id, Location location, double upperMeasuringLimit,
            double lowerMeasuringLimit, double upperInterventionLimit, double lowerInterventionLimit, TestLevelSet testLevelSet,
            int testLevelNumber, ProcessControlTech processControlTech, DateTime? startDate, bool testOperationActive)
        {
            return new ProcessControlCondition()
            {
                Id = new ProcessControlConditionId(id),
                Location = location,
                UpperMeasuringLimit = Torque.FromNm(upperMeasuringLimit),
                LowerMeasuringLimit = Torque.FromNm(lowerMeasuringLimit),
                UpperInterventionLimit = Torque.FromNm(upperInterventionLimit),
                LowerInterventionLimit = Torque.FromNm(lowerInterventionLimit),
                TestLevelSet = testLevelSet,
                TestLevelNumber = testLevelNumber,
                ProcessControlTech = processControlTech,
                StartDate = startDate,
                TestOperationActive = testOperationActive
            };
        }

        public static ProcessControlCondition Anonymous()
        {
            return Parametrized(1, new Location() { Id = new LocationId(1) }, 10, 5, 20, 7, null, 1, CreateQstProcessControlTech.Anonymous(), DateTime.Now, true);
        }

        public static ProcessControlCondition Randomized(int seed)
        {
            var randomizer = new Random(seed);
            return Parametrized(
                randomizer.Next(),
                new Location() { Id = new LocationId(randomizer.Next()) },
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                CreateTestLevelSet.Randomized(randomizer.Next()),
                randomizer.Next(),
                CreateQstProcessControlTech.Randomized(randomizer.Next()),
                DateTime.Now, 
                randomizer.Next(0,1) == 0)
            ;
        }

        public static ProcessControlCondition WithId(long id)
        {
            var processControlCondition = Randomized(32453);
            processControlCondition.Id = new ProcessControlConditionId(id);
            processControlCondition.StartDate = new DateTime(2020, 1, 1);
            return processControlCondition;
        }

        public static ProcessControlCondition RandomizedWithId(int seed, long id)
        {
            var processControlCondition = Randomized(seed);
            processControlCondition.Id = new ProcessControlConditionId(id);
            processControlCondition.StartDate = new DateTime(2020, 1, 1);
            return processControlCondition;
        }

        public static ProcessControlCondition WithLocationId(long id)
        {
            var processControlCondition = Anonymous();
            processControlCondition.Location = new Location() { Id = new LocationId(id) };
            return processControlCondition;
        }
    }
}
