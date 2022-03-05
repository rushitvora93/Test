using System;
using System.Collections.Generic;
using Client.Core.Entities;
using Common.Types.Enums;
using Core.Entities;
using TestHelper.Factories;

namespace Client.TestHelper.Factories
{
    public class CreateClassicProcessTest : AbstractEntityFactory
    {
        public static ClassicProcessTest Parameterized(long id, long numberOfTests, double nominalValueUnit1,
            double lowerLimitUnit1, double upperLimitUnit1, DateTime timestamp, double average,
            MeaUnit controlledByUnitId,
            double lowerInterventionLimitUnit1, double lowerInterventionLimitUnit2, double lowerLimitUnit2,
            double nominalValueUnit2,
            double standardDeviation, double testValueMaximum, double testValueMinimum, MeaUnit unit1Id,
            MeaUnit unit2Id,
            double upperInterventionLimitUnit1, double upperInterventionLimitUnit2, double upperLimitUnit2,
            TestEquipment testEquipment, ClassicTestLocation tesLocation, long result, 
            ToleranceClass toleranceClass1, ToleranceClass toleranceClass2)
        {
            return new ClassicProcessTest()
            {
                Id = new GlobalHistoryId(id),
                NumberOfTests = numberOfTests,
                NominalValueUnit1 = nominalValueUnit1,
                LowerLimitUnit1 = lowerLimitUnit1,
                UpperLimitUnit1 = upperLimitUnit1,
                Timestamp = timestamp,
                Average = average,
                ControlledByUnitId = controlledByUnitId,
                LowerInterventionLimitUnit1 = lowerInterventionLimitUnit1,
                LowerInterventionLimitUnit2 = lowerInterventionLimitUnit2,
                LowerLimitUnit2 = lowerLimitUnit2,
                NominalValueUnit2 = nominalValueUnit2,
                StandardDeviation = standardDeviation,
                TestValueMaximum = testValueMaximum,
                TestValueMinimum = testValueMinimum,
                Unit1Id = unit1Id,
                Unit2Id = unit2Id,
                UpperInterventionLimitUnit1 = upperInterventionLimitUnit1,
                UpperInterventionLimitUnit2 = upperInterventionLimitUnit2,
                UpperLimitUnit2 = upperLimitUnit2,
                TestEquipment = testEquipment,
                TestLocation = tesLocation,
                Result = new TestResult(result),
                ToleranceClassUnit1 = toleranceClass1,
                ToleranceClassUnit2 = toleranceClass2
            };
        }

        public static ClassicProcessTest Randomized(int seed)
        {
            var randomizer = new Random(seed);

            var meaUnits = new List<MeaUnit>()
            {
                MeaUnit.Nm,
                MeaUnit.Deg
            };

            return Parameterized(
                randomizer.Next(),
                randomizer.Next(),
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                DateTime.Now,
                randomizer.NextDouble(),
                meaUnits[randomizer.Next(0, meaUnits.Count - 1)],
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                meaUnits[randomizer.Next(0, meaUnits.Count - 1)],
                meaUnits[randomizer.Next(0, meaUnits.Count - 1)],
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                randomizer.NextDouble(),
                CreateTestEquipment.Randomized(seed),
                CreateClassicTestLocation.Randomized(seed),
                randomizer.Next(0,1),
                CreateToleranceClass.Randomized(seed),
                CreateToleranceClass.Randomized(seed));
        }

        public static ClassicProcessTest WithUnitsAndValues(MeaUnit controlledByUnitId, MeaUnit unit1, MeaUnit unit2, List<(double, double)> values)
        {
            var test = new ClassicProcessTest
            {
                ControlledByUnitId = controlledByUnitId,
                Unit1Id = unit1,
                Unit2Id = unit2,
                TestValues = new List<ClassicProcessTestValue>()
            };

            foreach (var val in values)
            {
                test.TestValues.Add(
                    new ClassicProcessTestValue()
                    {
                        ProcessTest = test,
                        ValueUnit1 = val.Item1,
                        ValueUnit2 = val.Item2
                    });
            }

            return test;
        }
    }
}
