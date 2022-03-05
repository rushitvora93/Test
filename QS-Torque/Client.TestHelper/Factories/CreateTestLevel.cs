using System;
using System.Collections.Generic;
using Core.Entities;
using TestHelper.Factories;

namespace Client.TestHelper.Factories
{
    public class CreateTestLevel
    {
        public static TestLevel Parametrized(long id, IntervalType intervalType, long intervalValue, 
            bool considerWorkingCalendar, bool isActive, int sampleNumber)
        {
            return new TestLevel()
            {
                Id = new TestLevelId(id),
                TestInterval = new Interval()
                {
                    Type = intervalType,
                    IntervalValue = intervalValue
                },
                ConsiderWorkingCalendar = considerWorkingCalendar,
                IsActive = isActive,
                SampleNumber = sampleNumber
            };
        }

        public static TestLevel Randomized(int seed)
        {
            var intervalList = new List<IntervalType>()
            {
                IntervalType.EveryXDays,
                IntervalType.EveryXShifts,
                IntervalType.XTimesADay,
                IntervalType.XTimesAShift,
                IntervalType.XTimesAWeek,
                IntervalType.XTimesAMonth,
                IntervalType.XTimesAYear
            };

            var randomizer = new Random(seed);
            return Parametrized(
                randomizer.Next(),
                intervalList[randomizer.Next(0, intervalList.Count - 1)],
                randomizer.Next(0,10),
                randomizer.Next(0,1) == 0,
                randomizer.Next(0, 1) == 0,
                randomizer.Next(0,1));
        }
    }
}
