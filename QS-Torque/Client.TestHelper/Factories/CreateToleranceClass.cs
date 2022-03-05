using System;
using Core.Entities;

namespace TestHelper.Factories
{
    public class CreateToleranceClass
    {
        public static ToleranceClass Parametrized(long id, string name, bool relative, double lowerLimit, double upperLimit)
        {
            return new ToleranceClass { Id = new ToleranceClassId(id), Name = name, Relative = relative, LowerLimit = lowerLimit, UpperLimit = upperLimit};
        }

        public static ToleranceClass Randomized(int seed)
        {
            var randomizer = new Random(seed);
            return Parametrized(
                randomizer.Next(),
                CreateString.Randomized(randomizer.Next(0, 10), randomizer.Next()),
                randomizer.Next(0, 1) == 1,
                randomizer.NextDouble(),
                randomizer.NextDouble());
        }

        public static ToleranceClass Anonymous()
        {
            return Parametrized(99, "", false, 0,0);
        }

        public static ToleranceClass WithId(long id)
        {
            var toleranceClass = Anonymous();
            toleranceClass.Id = new ToleranceClassId(id);
            return toleranceClass;
        }
    }
}
