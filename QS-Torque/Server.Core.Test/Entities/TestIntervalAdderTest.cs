using System;
using System.Collections.Generic;
using Core.Entities;
using NUnit.Framework;
using Server.Core.Entities;
using Server.Core.Enums;

namespace Server.Core.Test.Entities
{
    public class TestIntervalAdderTest
    {
        [TestCase("2020-12-14 00:00:00", 4, "2020-12-18 00:00:00")]
        [TestCase("2020-12-14 00:00:00", 7, "2020-12-21 00:00:00")]
        public void AddIntervalToDateAndShiftAddsDaysCorrectlyEveryXDays(DateTime baseDate, int days, DateTime resultDate)
        {
            var adder = new TestIntervalAdder(new WorkingCalendar(), new List<WorkingCalendarEntry>(), new ShiftManagement());
            var result = adder.AddIntervalToDateAndShift(baseDate, null, new TestLevel()
            {
                TestInterval = new Interval()
                {
                    IntervalValue = days,
                    Type = IntervalType.EveryXDays
                },
                ConsiderWorkingCalendar = false
            });
            Assert.AreEqual(resultDate, result.resultDate);
            Assert.IsNull(result.resultShift);
        }

        [TestCase("2020-12-14 00:00:00", "2020-12-15 00:00:00")]
        [TestCase("2020-12-20 00:00:00", "2020-12-21 00:00:00")]
        public void AddIntervalToDateAndShiftAddsDaysCorrectlyXTimesADay(DateTime baseDate, DateTime resultDate)
        {
            var adder = new TestIntervalAdder(new WorkingCalendar(), new List<WorkingCalendarEntry>(), new ShiftManagement());
            var result = adder.AddIntervalToDateAndShift(baseDate, null, new TestLevel()
            {
                TestInterval = new Interval()
                {
                    Type = IntervalType.XTimesADay
                },
                ConsiderWorkingCalendar = false
            });
            Assert.AreEqual(resultDate, result.resultDate);
            Assert.IsNull(result.resultShift);
        }

        [TestCase("2020-12-14 00:00:00", "2020-12-21 00:00:00")]
        [TestCase("2020-12-28 00:00:00", "2021-01-04 00:00:00")]
        public void AddIntervalToDateAndShiftAddsDaysCorrectlyXTimesAWeek(DateTime baseDate, DateTime resultDate)
        {
            var adder = new TestIntervalAdder(new WorkingCalendar(), new List<WorkingCalendarEntry>(), new ShiftManagement());
            var result = adder.AddIntervalToDateAndShift(baseDate, null, new TestLevel()
            {
                TestInterval = new Interval()
                {
                    Type = IntervalType.XTimesAWeek
                },
                ConsiderWorkingCalendar = false
            });
            Assert.AreEqual(resultDate, result.resultDate);
            Assert.IsNull(result.resultShift);
        }

        [TestCase("2021-10-04 00:00:00", "2021-11-04 00:00:00")]
        [TestCase("2020-02-20 00:00:00", "2020-03-20 00:00:00")]
        public void AddIntervalToDateAndShiftAddsDaysCorrectlyXTimesAMonth(DateTime baseDate, DateTime resultDate)
        {
            var adder = new TestIntervalAdder(new WorkingCalendar(), new List<WorkingCalendarEntry>(), new ShiftManagement());
            var result = adder.AddIntervalToDateAndShift(baseDate, null, new TestLevel()
            {
                TestInterval = new Interval()
                {
                    Type = IntervalType.XTimesAMonth
                },
                ConsiderWorkingCalendar = false
            });
            Assert.AreEqual(resultDate, result.resultDate);
            Assert.IsNull(result.resultShift);
        }

        [TestCase("2021-10-04 00:00:00", "2022-10-04 00:00:00")]
        [TestCase("2020-01-20 00:00:00", "2021-01-20 00:00:00")]
        public void AddIntervalToDateAndShiftAddsDaysCorrectlyXTimesAYear(DateTime baseDate, DateTime resultDate)
        {
            var adder = new TestIntervalAdder(new WorkingCalendar(), new List<WorkingCalendarEntry>(), new ShiftManagement());
            var result = adder.AddIntervalToDateAndShift(baseDate, null, new TestLevel()
            {
                TestInterval = new Interval()
                {
                    Type = IntervalType.XTimesAYear
                },
                ConsiderWorkingCalendar = false
            });
            Assert.AreEqual(resultDate, result.resultDate);
            Assert.IsNull(result.resultShift);
        }

        [TestCase("2020-12-14 00:00:00", Shift.FirstShiftOfDay, 1, "2020-12-14 00:00:00", Shift.SecondShiftOfDay, true, true)]
        [TestCase("2020-12-14 00:00:00", Shift.SecondShiftOfDay, 1, "2020-12-14 00:00:00", Shift.ThirdShiftOfDay, true, true)]
        [TestCase("2020-12-14 00:00:00", Shift.ThirdShiftOfDay, 1, "2020-12-15 00:00:00", Shift.FirstShiftOfDay, true, true)]
        [TestCase("2020-12-21 00:00:00", Shift.FirstShiftOfDay, 3, "2020-12-22 00:00:00", Shift.FirstShiftOfDay, true, true)]
        [TestCase("2020-12-21 00:00:00", Shift.FirstShiftOfDay, 8, "2020-12-23 00:00:00", Shift.ThirdShiftOfDay, true, true)]
        [TestCase("2020-12-21 00:00:00", Shift.SecondShiftOfDay, 10, "2020-12-24 00:00:00", Shift.ThirdShiftOfDay, true, true)]
        [TestCase("2020-12-14 00:00:00", Shift.FirstShiftOfDay, 1, "2020-12-15 00:00:00", Shift.FirstShiftOfDay,  false, false)]
        [TestCase("2020-12-14 00:00:00", Shift.FirstShiftOfDay, 2, "2020-12-16 00:00:00", Shift.FirstShiftOfDay, false, false)]
        [TestCase("2020-12-14 00:00:00", Shift.FirstShiftOfDay, 2, "2020-12-15 00:00:00", Shift.FirstShiftOfDay, true, false)]
        [TestCase("2020-12-14 00:00:00", Shift.FirstShiftOfDay, 5, "2020-12-16 00:00:00", Shift.ThirdShiftOfDay, false, true)]
        [TestCase("2020-12-14 00:00:00", Shift.SecondShiftOfDay, 10, "2020-12-19 00:00:00", Shift.SecondShiftOfDay, true, false)]
        [TestCase("2020-12-14 00:00:00", null, 5, "2020-12-16 00:00:00", Shift.ThirdShiftOfDay, false, true)]
        public void AddIntervalToDateAndShiftAddsDaysCorrectlyEveryXShifts(DateTime baseDate, Shift? baseShift, int shifts, DateTime resultDate, Shift resultShift, bool secondActive, bool thirdActive)
        {
            var adder = new TestIntervalAdder(new WorkingCalendar(), new List<WorkingCalendarEntry>(), new ShiftManagement()
            {
                IsSecondShiftActive = secondActive,
                IsThirdShiftActive = thirdActive
            });
            var result = adder.AddIntervalToDateAndShift(baseDate, baseShift, new TestLevel()
            {
                TestInterval = new Interval()
                {
                    IntervalValue = shifts,
                    Type = IntervalType.EveryXShifts
                },
                ConsiderWorkingCalendar = false
            });
            Assert.AreEqual(resultDate, result.resultDate);
            Assert.AreEqual(resultShift, result.resultShift);
        }

        [TestCase("2020-12-14 00:00:00", Shift.FirstShiftOfDay, "2020-12-14 00:00:00", Shift.SecondShiftOfDay, true, true)]
        [TestCase("2020-12-14 00:00:00", Shift.SecondShiftOfDay, "2020-12-14 00:00:00", Shift.ThirdShiftOfDay, true, true)]
        [TestCase("2020-12-14 00:00:00", Shift.ThirdShiftOfDay, "2020-12-15 00:00:00", Shift.FirstShiftOfDay, true, true)]
        [TestCase("2020-12-14 00:00:00", Shift.FirstShiftOfDay, "2020-12-15 00:00:00", Shift.FirstShiftOfDay, false, false)]
        [TestCase("2020-12-14 00:00:00", null, "2020-12-15 00:00:00", Shift.FirstShiftOfDay, false, false)]
        public void AddIntervalToDateAndShiftAddsDaysCorrectlyXTimesAShift(DateTime baseDate, Shift? baseShift, DateTime resultDate, Shift resultShift, bool secondActive, bool thirdActive)
        {
            var adder = new TestIntervalAdder(new WorkingCalendar(), new List<WorkingCalendarEntry>(), new ShiftManagement()
            {
                IsSecondShiftActive = secondActive,
                IsThirdShiftActive = thirdActive
            });
            var result = adder.AddIntervalToDateAndShift(baseDate, baseShift, new TestLevel()
            {
                TestInterval = new Interval()
                {
                    Type = IntervalType.XTimesAShift
                },
                ConsiderWorkingCalendar = false
            });
            Assert.AreEqual(resultDate, result.resultDate);
            Assert.AreEqual(resultShift, result.resultShift);
        }

        [TestCase("2020-12-14 00:00:00", "2020-12-16 00:00:00", true, true)]
        [TestCase("2020-12-28 00:00:00", "2020-12-30 00:00:00", true, true)]
        [TestCase("2020-12-21 00:00:00", "2020-12-24 00:00:00", true, true)]
        [TestCase("2020-12-18 00:00:00", "2020-12-21 00:00:00", true, true)]
        [TestCase("2020-12-18 00:00:00", "2020-12-20 00:00:00", true, false)]
        [TestCase("2020-12-18 00:00:00", "2020-12-19 00:00:00", false, true)]
        public void AddIntervalToDateAndShiftConsidersWorkingCalendarForXTimesADay(DateTime baseDate, DateTime resultDate, bool saturdaysFree, bool sundaysFree)
        {
            var adder = new TestIntervalAdder(new WorkingCalendar()
                {
                    AreSaturdaysFree = saturdaysFree,
                    AreSundaysFree = sundaysFree
                }, 
                new List<WorkingCalendarEntry>()
                {
                    new WorkingCalendarEntry() { Date = new DateTime(2020, 12, 15), Repetition = WorkingCalendarEntryRepetition.Once, Type = WorkingCalendarEntryType.Holiday },
                    new WorkingCalendarEntry() { Date = new DateTime(2020, 12, 22), Repetition = WorkingCalendarEntryRepetition.Once, Type = WorkingCalendarEntryType.Holiday },
                    new WorkingCalendarEntry() { Date = new DateTime(2020, 12, 23), Repetition = WorkingCalendarEntryRepetition.Once, Type = WorkingCalendarEntryType.Holiday },
                    new WorkingCalendarEntry() { Date = new DateTime(2019, 12, 29), Repetition = WorkingCalendarEntryRepetition.Yearly, Type = WorkingCalendarEntryType.Holiday }
                }, 
                new ShiftManagement()
                {
                    IsSecondShiftActive = true,
                    IsThirdShiftActive = true
                });
            var result = adder.AddIntervalToDateAndShift(baseDate, Shift.FirstShiftOfDay, new TestLevel()
            {
                TestInterval = new Interval()
                {
                    Type = IntervalType.XTimesADay
                },
                ConsiderWorkingCalendar = true
            });
            Assert.AreEqual(resultDate, result.resultDate);
            Assert.IsNull(result.resultShift);
        }

        [TestCase("2020-12-08 00:00:00", 1, "2020-12-16 00:00:00", true, true)]
        [TestCase("2020-12-15 00:00:00", 1, "2020-12-24 00:00:00", true, true)]
        [TestCase("2020-12-22 00:00:00", 1, "2020-12-30 00:00:00", true, true)]
        public void AddIntervalToDateAndShiftConsidersWorkingCalendarForXTimesAWeek(DateTime baseDate, int weeks, DateTime resultDate, bool saturdaysFree, bool sundaysFree)
        {
            var adder = new TestIntervalAdder(new WorkingCalendar()
                {
                    AreSaturdaysFree = saturdaysFree,
                    AreSundaysFree = sundaysFree
                },
                new List<WorkingCalendarEntry>()
                {
                    new WorkingCalendarEntry() { Date = new DateTime(2020, 12, 15), Repetition = WorkingCalendarEntryRepetition.Once, Type = WorkingCalendarEntryType.Holiday },
                    new WorkingCalendarEntry() { Date = new DateTime(2020, 12, 22), Repetition = WorkingCalendarEntryRepetition.Once, Type = WorkingCalendarEntryType.Holiday },
                    new WorkingCalendarEntry() { Date = new DateTime(2020, 12, 23), Repetition = WorkingCalendarEntryRepetition.Once, Type = WorkingCalendarEntryType.Holiday },
                    new WorkingCalendarEntry() { Date = new DateTime(2019, 12, 29), Repetition = WorkingCalendarEntryRepetition.Yearly, Type = WorkingCalendarEntryType.Holiday }
                },
                new ShiftManagement()
                {
                    IsSecondShiftActive = true,
                    IsThirdShiftActive = true
                });
            var result = adder.AddIntervalToDateAndShift(baseDate, Shift.FirstShiftOfDay, new TestLevel()
            {
                TestInterval = new Interval()
                {
                    IntervalValue = weeks,
                    Type = IntervalType.XTimesAWeek
                },
                ConsiderWorkingCalendar = true
            });
            Assert.AreEqual(resultDate, result.resultDate);
            Assert.IsNull(result.resultShift);
        }

        [TestCase("2020-12-14 00:00:00", Shift.ThirdShiftOfDay, "2020-12-16 00:00:00", Shift.FirstShiftOfDay, true, true)]
        [TestCase("2020-12-28 00:00:00", Shift.ThirdShiftOfDay, "2020-12-30 00:00:00", Shift.FirstShiftOfDay, true, true)]
        [TestCase("2020-12-21 00:00:00", Shift.ThirdShiftOfDay, "2020-12-24 00:00:00", Shift.FirstShiftOfDay, true, true)]
        [TestCase("2020-12-18 00:00:00", Shift.ThirdShiftOfDay, "2020-12-21 00:00:00", Shift.FirstShiftOfDay, true, true)]
        [TestCase("2020-12-18 00:00:00", Shift.ThirdShiftOfDay, "2020-12-20 00:00:00", Shift.FirstShiftOfDay, true, false)]
        [TestCase("2020-12-18 00:00:00", Shift.ThirdShiftOfDay, "2020-12-19 00:00:00", Shift.FirstShiftOfDay, false, true)]
        public void AddIntervalToDateAndShiftConsidersWorkingCalendarForXTimesAShift(DateTime baseDate, Shift baseShift, DateTime resultDate, Shift resultShift, bool saturdaysFree, bool sundaysFree)
        {
            var adder = new TestIntervalAdder(new WorkingCalendar()
                {
                    AreSaturdaysFree = saturdaysFree,
                    AreSundaysFree = sundaysFree
                },
                new List<WorkingCalendarEntry>()
                {
                    new WorkingCalendarEntry() { Date = new DateTime(2020, 12, 15), Repetition = WorkingCalendarEntryRepetition.Once, Type = WorkingCalendarEntryType.Holiday },
                    new WorkingCalendarEntry() { Date = new DateTime(2020, 12, 22), Repetition = WorkingCalendarEntryRepetition.Once, Type = WorkingCalendarEntryType.Holiday },
                    new WorkingCalendarEntry() { Date = new DateTime(2020, 12, 23), Repetition = WorkingCalendarEntryRepetition.Once, Type = WorkingCalendarEntryType.Holiday },
                    new WorkingCalendarEntry() { Date = new DateTime(2019, 12, 29), Repetition = WorkingCalendarEntryRepetition.Yearly, Type = WorkingCalendarEntryType.Holiday }
                },
                new ShiftManagement()
                {
                    IsSecondShiftActive = true,
                    IsThirdShiftActive = true
                });
            var result = adder.AddIntervalToDateAndShift(baseDate, baseShift, new TestLevel()
            {
                TestInterval = new Interval()
                {
                    Type = IntervalType.XTimesAShift
                },
                ConsiderWorkingCalendar = true
            });
            Assert.AreEqual(resultDate, result.resultDate);
            Assert.AreEqual(resultShift, result.resultShift);
        }

        [TestCase("2020-11-30 00:00:00", 7, "2020-12-09 00:00:00", true, true)]
        [TestCase("2020-11-30 00:00:00", 7, "2020-12-08 00:00:00", true, false)]
        [TestCase("2020-11-30 00:00:00", 7, "2020-12-08 00:00:00", false, true)]
        [TestCase("2020-11-30 00:00:00", 7, "2020-12-07 00:00:00", false, false)]
        [TestCase("2020-12-07 00:00:00", 7, "2020-12-16 00:00:00", true, true)]
        [TestCase("2020-12-07 00:00:00", 7, "2020-12-14 00:00:00", true, false)]
        [TestCase("2020-12-14 00:00:00", 7, "2020-12-25 00:00:00", true, true)]
        [TestCase("2020-12-14 00:00:00", 7, "2020-12-24 00:00:00", true, false)]
        [TestCase("2020-12-28 00:00:00", 7, "2021-01-05 00:00:00", false, false)]
        public void AddIntervalToDateAndShiftConsidersWorkingCalendarForEveryXDays(DateTime baseDate, int days, DateTime resultDate, bool saturdaysFree, bool sundaysFree)
        {
            var adder = new TestIntervalAdder(new WorkingCalendar()
                {
                    AreSaturdaysFree = saturdaysFree,
                    AreSundaysFree = sundaysFree
                },
                new List<WorkingCalendarEntry>()
                {
                    new WorkingCalendarEntry() { Date = new DateTime(2020, 12, 12), Repetition = WorkingCalendarEntryRepetition.Once, Type = WorkingCalendarEntryType.ExtraShift },
                    new WorkingCalendarEntry() { Date = new DateTime(2019, 12, 19), Repetition = WorkingCalendarEntryRepetition.Yearly, Type = WorkingCalendarEntryType.ExtraShift },
                    new WorkingCalendarEntry() { Date = new DateTime(2020, 12, 15), Repetition = WorkingCalendarEntryRepetition.Once, Type = WorkingCalendarEntryType.Holiday },
                    new WorkingCalendarEntry() { Date = new DateTime(2020, 12, 22), Repetition = WorkingCalendarEntryRepetition.Once, Type = WorkingCalendarEntryType.Holiday },
                    new WorkingCalendarEntry() { Date = new DateTime(2020, 12, 23), Repetition = WorkingCalendarEntryRepetition.Once, Type = WorkingCalendarEntryType.Holiday },
                    new WorkingCalendarEntry() { Date = new DateTime(2019, 12, 29), Repetition = WorkingCalendarEntryRepetition.Yearly, Type = WorkingCalendarEntryType.Holiday }
                },
                new ShiftManagement()
                {
                    IsSecondShiftActive = true,
                    IsThirdShiftActive = true
                });
            var result = adder.AddIntervalToDateAndShift(baseDate, Shift.FirstShiftOfDay, new TestLevel()
            {
                TestInterval = new Interval()
                {
                    IntervalValue = days,
                    Type = IntervalType.EveryXDays
                },
                ConsiderWorkingCalendar = true
            });
            Assert.AreEqual(resultDate, result.resultDate);
            Assert.IsNull(result.resultShift);
        }

        [TestCase("2020-11-30 00:00:00", 21, "2020-12-09 00:00:00", true, true)]
        [TestCase("2020-11-30 00:00:00", 21, "2020-12-08 00:00:00", true, false)]
        [TestCase("2020-11-30 00:00:00", 21, "2020-12-08 00:00:00", false, true)]
        [TestCase("2020-11-30 00:00:00", 21, "2020-12-07 00:00:00", false, false)]
        [TestCase("2020-12-07 00:00:00", 21, "2020-12-16 00:00:00", true, true)]
        [TestCase("2020-12-07 00:00:00", 21, "2020-12-14 00:00:00", true, false)]
        [TestCase("2020-12-14 00:00:00", 21, "2020-12-25 00:00:00", true, true)]
        [TestCase("2020-12-14 00:00:00", 21, "2020-12-24 00:00:00", true, false)]
        [TestCase("2020-12-28 00:00:00", 21, "2021-01-05 00:00:00", false, false)]
        public void AddIntervalToDateAndShiftConsidersWorkingCalendarForEveryXShifts(DateTime baseDate, int shifts, DateTime resultDate, bool saturdaysFree, bool sundaysFree)
        {
            var adder = new TestIntervalAdder(new WorkingCalendar()
            {
                AreSaturdaysFree = saturdaysFree,
                AreSundaysFree = sundaysFree
            },
                new List<WorkingCalendarEntry>()
                {
                    new WorkingCalendarEntry() { Date = new DateTime(2020, 12, 12), Repetition = WorkingCalendarEntryRepetition.Once, Type = WorkingCalendarEntryType.ExtraShift },
                    new WorkingCalendarEntry() { Date = new DateTime(2019, 12, 19), Repetition = WorkingCalendarEntryRepetition.Yearly, Type = WorkingCalendarEntryType.ExtraShift },
                    new WorkingCalendarEntry() { Date = new DateTime(2020, 12, 15), Repetition = WorkingCalendarEntryRepetition.Once, Type = WorkingCalendarEntryType.Holiday },
                    new WorkingCalendarEntry() { Date = new DateTime(2020, 12, 22), Repetition = WorkingCalendarEntryRepetition.Once, Type = WorkingCalendarEntryType.Holiday },
                    new WorkingCalendarEntry() { Date = new DateTime(2020, 12, 23), Repetition = WorkingCalendarEntryRepetition.Once, Type = WorkingCalendarEntryType.Holiday },
                    new WorkingCalendarEntry() { Date = new DateTime(2019, 12, 29), Repetition = WorkingCalendarEntryRepetition.Yearly, Type = WorkingCalendarEntryType.Holiday }
                },
                new ShiftManagement()
                {
                    IsSecondShiftActive = true,
                    IsThirdShiftActive = true
                });
            var result = adder.AddIntervalToDateAndShift(baseDate, Shift.FirstShiftOfDay, new TestLevel()
            {
                TestInterval = new Interval()
                {
                    IntervalValue = shifts,
                    Type = IntervalType.EveryXShifts
                },
                ConsiderWorkingCalendar = true
            });
            Assert.AreEqual(resultDate, result.resultDate);
        }
    }
}
