using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.TestHelper.Factories
{
    public class CreateWorkingCalendar : AbstractEntityFactory
    {
        public static WorkingCalendar Randomized(int seed)
        {
            var randomizer = new Random(seed);

            return new WorkingCalendar()
            {
                AreSaturdaysFree = randomizer.Next(0, 1) == 1,
                AreSundaysFree = randomizer.Next(0, 1) == 1
            };
        }
    }
}
