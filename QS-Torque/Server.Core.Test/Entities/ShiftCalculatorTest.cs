using System;
using System.Collections.Generic;
using NUnit.Framework;
using Server.Core.Entities;
using Server.Core.Enums;

namespace Server.Core.Test.Entities
{
    public class ShiftCalculatorTest
    {
        [TestCaseSource(nameof(_shiftCalculatorTestSource))]
        public void GetShiftForDateTimeReturnsCorrectValue((ShiftManagement shiftManagement, DateTime dateTime, Shift expectedShift) tuple)
        {
            var entity = new ShiftCalculator(tuple.shiftManagement);
            Assert.AreEqual(tuple.expectedShift, entity.GetShiftForDateTime(tuple.dateTime));
        }

        [TestCaseSource(nameof(_shiftCalculatorTestSource))]
        public void GetShiftForTimeSpanReturnsCorrectValue((ShiftManagement shiftManagement, DateTime dateTime, Shift expectedShift) tuple)
        {
            var entity = new ShiftCalculator(tuple.shiftManagement);
            Assert.AreEqual(tuple.expectedShift, entity.GetShiftForTimeSpan(tuple.dateTime.TimeOfDay));
        }



        private static IEnumerable<(ShiftManagement, DateTime, Shift)> _shiftCalculatorTestSource = new List<(ShiftManagement, DateTime, Shift)>()
        {
            (
                new ShiftManagement()
                {
                    FirstShiftStart = TimeSpan.FromHours(6),
                    FirstShiftEnd = TimeSpan.FromHours(14),
                    SecondShiftStart = TimeSpan.FromHours(14),
                    SecondShiftEnd = TimeSpan.FromHours(22),
                    ThirdShiftStart = TimeSpan.FromHours(22),
                    ThirdShiftEnd = TimeSpan.FromHours(6),
                    IsSecondShiftActive = true,
                    IsThirdShiftActive = true
                },
                new DateTime(2021, 4, 8, 9, 0, 0),
                Shift.FirstShiftOfDay
            ),
            (
                new ShiftManagement()
                {
                    FirstShiftStart = TimeSpan.FromHours(6),
                    FirstShiftEnd = TimeSpan.FromHours(14),
                    SecondShiftStart = TimeSpan.FromHours(14),
                    SecondShiftEnd = TimeSpan.FromHours(22),
                    ThirdShiftStart = TimeSpan.FromHours(22),
                    ThirdShiftEnd = TimeSpan.FromHours(6),
                    IsSecondShiftActive = true,
                    IsThirdShiftActive = true
                },
                new DateTime(2021, 4, 8, 15, 0, 0),
                Shift.SecondShiftOfDay
            ),
            (
                new ShiftManagement()
                {
                    FirstShiftStart = TimeSpan.FromHours(6),
                    FirstShiftEnd = TimeSpan.FromHours(14),
                    SecondShiftStart = TimeSpan.FromHours(14),
                    SecondShiftEnd = TimeSpan.FromHours(22),
                    ThirdShiftStart = TimeSpan.FromHours(22),
                    ThirdShiftEnd = TimeSpan.FromHours(6),
                    IsSecondShiftActive = true,
                    IsThirdShiftActive = true
                },
                new DateTime(2021, 4, 8, 23, 0, 0),
                Shift.ThirdShiftOfDay
            ),
            (
                new ShiftManagement()
                {
                    FirstShiftStart = TimeSpan.FromHours(6),
                    FirstShiftEnd = TimeSpan.FromHours(14),
                    SecondShiftStart = TimeSpan.FromHours(14),
                    SecondShiftEnd = TimeSpan.FromHours(22),
                    ThirdShiftStart = TimeSpan.FromHours(22),
                    ThirdShiftEnd = TimeSpan.FromHours(6),
                    IsSecondShiftActive = true,
                    IsThirdShiftActive = true
                },
                new DateTime(2021, 4, 8, 3, 0, 0),
                Shift.ThirdShiftOfDay
            ),
            (
                new ShiftManagement()
                {
                    FirstShiftStart = TimeSpan.FromHours(6),
                    FirstShiftEnd = TimeSpan.FromHours(14),
                    SecondShiftStart = TimeSpan.FromHours(14),
                    SecondShiftEnd = TimeSpan.FromHours(22),
                    ThirdShiftStart = TimeSpan.FromHours(22),
                    ThirdShiftEnd = TimeSpan.FromHours(6),
                    IsSecondShiftActive = true,
                    IsThirdShiftActive = false
                },
                new DateTime(2021, 4, 8, 23, 0, 0),
                Shift.SecondShiftOfDay
            ),
            (
                new ShiftManagement()
                {
                    FirstShiftStart = TimeSpan.FromHours(6),
                    FirstShiftEnd = TimeSpan.FromHours(14),
                    SecondShiftStart = TimeSpan.FromHours(14),
                    SecondShiftEnd = TimeSpan.FromHours(22),
                    ThirdShiftStart = TimeSpan.FromHours(22),
                    ThirdShiftEnd = TimeSpan.FromHours(6),
                    IsSecondShiftActive = false,
                    IsThirdShiftActive = true
                },
                new DateTime(2021, 4, 8, 16, 0, 0),
                Shift.FirstShiftOfDay
            ),
            (
                new ShiftManagement()
                {
                    FirstShiftStart = TimeSpan.FromHours(6),
                    FirstShiftEnd = TimeSpan.FromHours(14),
                    SecondShiftStart = TimeSpan.FromHours(14),
                    SecondShiftEnd = TimeSpan.FromHours(22),
                    ThirdShiftStart = TimeSpan.FromHours(22),
                    ThirdShiftEnd = TimeSpan.FromHours(6),
                    IsSecondShiftActive = false,
                    IsThirdShiftActive = false
                },
                new DateTime(2021, 4, 8, 0, 0, 0),
                Shift.FirstShiftOfDay
            ),
            (
                new ShiftManagement()
                {
                    FirstShiftStart = TimeSpan.FromHours(6),
                    FirstShiftEnd = TimeSpan.FromHours(14),
                    SecondShiftStart = TimeSpan.FromHours(14),
                    SecondShiftEnd = TimeSpan.FromHours(22),
                    ThirdShiftStart = TimeSpan.FromHours(22),
                    ThirdShiftEnd = TimeSpan.FromHours(6),
                    IsSecondShiftActive = false,
                    IsThirdShiftActive = false
                },
                new DateTime(2021, 4, 8, 10, 0, 0),
                Shift.FirstShiftOfDay
            ),
            (
                new ShiftManagement()
                {
                    FirstShiftStart = TimeSpan.FromHours(6),
                    FirstShiftEnd = TimeSpan.FromHours(14),
                    SecondShiftStart = TimeSpan.FromHours(14),
                    SecondShiftEnd = TimeSpan.FromHours(22),
                    ThirdShiftStart = TimeSpan.FromHours(22),
                    ThirdShiftEnd = TimeSpan.FromHours(6),
                    IsSecondShiftActive = false,
                    IsThirdShiftActive = false
                },
                new DateTime(2021, 4, 8, 17, 0, 0),
                Shift.FirstShiftOfDay
            ),
            (
                new ShiftManagement()
                {
                    FirstShiftStart = TimeSpan.FromHours(6),
                    FirstShiftEnd = TimeSpan.FromHours(12),
                    SecondShiftStart = TimeSpan.FromHours(14),
                    SecondShiftEnd = TimeSpan.FromHours(20),
                    ThirdShiftStart = TimeSpan.FromHours(22),
                    ThirdShiftEnd = TimeSpan.FromHours(4),
                    IsSecondShiftActive = true,
                    IsThirdShiftActive = true
                },
                new DateTime(2021, 4, 8, 13, 0, 0),
                Shift.FirstShiftOfDay
            ),
            (
                new ShiftManagement()
                {
                    FirstShiftStart = TimeSpan.FromHours(6),
                    FirstShiftEnd = TimeSpan.FromHours(12),
                    SecondShiftStart = TimeSpan.FromHours(14),
                    SecondShiftEnd = TimeSpan.FromHours(20),
                    ThirdShiftStart = TimeSpan.FromHours(22),
                    ThirdShiftEnd = TimeSpan.FromHours(4),
                    IsSecondShiftActive = true,
                    IsThirdShiftActive = true
                },
                new DateTime(2021, 4, 8, 21, 0, 0),
                Shift.SecondShiftOfDay
            ),
            (
                new ShiftManagement()
                {
                    FirstShiftStart = TimeSpan.FromHours(6),
                    FirstShiftEnd = TimeSpan.FromHours(12),
                    SecondShiftStart = TimeSpan.FromHours(14),
                    SecondShiftEnd = TimeSpan.FromHours(20),
                    ThirdShiftStart = TimeSpan.FromHours(22),
                    ThirdShiftEnd = TimeSpan.FromHours(4),
                    IsSecondShiftActive = true,
                    IsThirdShiftActive = true
                },
                new DateTime(2021, 4, 8, 5, 0, 0),
                Shift.ThirdShiftOfDay
            )
        };
    }
}
