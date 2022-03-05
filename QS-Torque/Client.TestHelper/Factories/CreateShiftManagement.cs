using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Client.TestHelper.Factories
{
    public class CreateShiftManagement : AbstractEntityFactory
    {
        public static ShiftManagement Randomized(int seed)
        {
            var randomizer = new Random(seed);

            return new ShiftManagement()
            {
                FirstShiftStart = TimeSpan.FromTicks(randomizer.Next()),
                FirstShiftEnd = TimeSpan.FromTicks(randomizer.Next()),
                SecondShiftStart = TimeSpan.FromTicks(randomizer.Next()),
                SecondShiftEnd = TimeSpan.FromTicks(randomizer.Next()),
                ThirdShiftStart = TimeSpan.FromTicks(randomizer.Next()),
                ThirdShiftEnd = TimeSpan.FromTicks(randomizer.Next()),
                IsSecondShiftActive = randomizer.Next(0, 1) == 1,
                IsThirdShiftActive = randomizer.Next(0, 1) == 1,
                ChangeOfDay = TimeSpan.FromTicks(randomizer.Next()),
                FirstDayOfWeek = (DayOfWeek)randomizer.Next(0, 6)
            };
        }
    }
}
