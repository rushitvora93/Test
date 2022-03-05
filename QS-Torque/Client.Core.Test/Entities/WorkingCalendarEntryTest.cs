using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using NUnit.Framework;

namespace Client.Core.Test.Entities
{
    class WorkingCalendarEntryTest
    {
        [TestCase("2021-04-24", WorkingCalendarEntryType.Holiday, true, false, false)]
        [TestCase("2021-04-24", WorkingCalendarEntryType.Holiday, true, true, false)]
        [TestCase("2021-04-25", WorkingCalendarEntryType.Holiday, false, true, false)]
        [TestCase("2021-04-25", WorkingCalendarEntryType.Holiday, true, true, false)]
        [TestCase("2021-04-23", WorkingCalendarEntryType.ExtraShift, false, false, false)]
        [TestCase("2021-04-23", WorkingCalendarEntryType.ExtraShift, true, false, false)]
        [TestCase("2021-04-23", WorkingCalendarEntryType.ExtraShift, false, true, false)]
        [TestCase("2021-04-23", WorkingCalendarEntryType.ExtraShift, true, true, false)]
        [TestCase("2021-04-24", WorkingCalendarEntryType.ExtraShift, false, false, false)]
        [TestCase("2021-04-24", WorkingCalendarEntryType.ExtraShift, false, true, false)]
        [TestCase("2021-04-25", WorkingCalendarEntryType.ExtraShift, false, false, false)]
        [TestCase("2021-04-25", WorkingCalendarEntryType.ExtraShift, true, false, false)]
        [TestCase("2021-04-23", WorkingCalendarEntryType.Holiday, true, true, true)]
        [TestCase("2021-04-24", WorkingCalendarEntryType.ExtraShift, true, false, true)]
        [TestCase("2021-04-25", WorkingCalendarEntryType.ExtraShift, false, true, true)]
        public void IsWorkingCalendarEntryValidAtDateValidatesCorrect(DateTime date, WorkingCalendarEntryType type, bool areSaturdaysFre, bool areSundaysFree, bool expectedResult)
        {
            var entry = new WorkingCalendarEntry()
            {
                Date = date,
                Type = type
            };
            Assert.AreEqual(expectedResult, WorkingCalendarEntryValidator.IsWorkingCalendarEntryValidAtDate(entry, areSaturdaysFre, areSundaysFree));
        }
    }
}
