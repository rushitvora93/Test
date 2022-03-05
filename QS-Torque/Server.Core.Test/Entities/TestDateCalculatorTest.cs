using System;
using System.Collections.Generic;
using Core.Entities;
using NUnit.Framework;
using Server.Core.Entities;
using Server.Core.Enums;

namespace Server.Core.Test.Entities
{
    class TestIntervalAdderMock : ITestIntervalAdder
    {
        public DateTime AddIntervalToDateAndShiftParameterBaseDate { get; set; }
        public Shift? AddIntervalToDateAndShiftParameterBaseShift { get; set; }
        public TestLevel AddIntervalToDateAndShiftParameterIntervalInfo { get; set; }
        public (DateTime, Shift?) AddIntervalToDateAndShiftReturnValue { get; set; }
        public DateTime AddIntervalToDateAndShiftParameterBaseDate2 { get; set; }
        public Shift? AddIntervalToDateAndShiftParameterBaseShift2 { get; set; }
        public TestLevel AddIntervalToDateAndShiftParameterIntervalInfo2 { get; set; }
        public (DateTime, Shift?) AddIntervalToDateAndShiftReturnValue2 { get; set; }
        public DateTime AddIntervalToDateAndShiftParameterBaseDate3 { get; set; }
        public Shift? AddIntervalToDateAndShiftParameterBaseShift3 { get; set; }
        public TestLevel AddIntervalToDateAndShiftParameterIntervalInfo3 { get; set; }
        public (DateTime, Shift?) AddIntervalToDateAndShiftReturnValue3 { get; set; }
        public int AddIntervalToDateAndShiftCallCount { get; set; } = 0;
        
        public (DateTime resultDate, Shift? resultShift) AddIntervalToDateAndShift(DateTime baseDate, Shift? baseShift, TestLevel intervalInfo)
        {
            if (AddIntervalToDateAndShiftCallCount == 0)
            {
                AddIntervalToDateAndShiftParameterBaseDate = baseDate;
                AddIntervalToDateAndShiftParameterBaseShift = baseShift;
                AddIntervalToDateAndShiftParameterIntervalInfo = intervalInfo;
                AddIntervalToDateAndShiftCallCount++;
                return AddIntervalToDateAndShiftReturnValue;
            }
            else if(AddIntervalToDateAndShiftCallCount == 1)
            {
                AddIntervalToDateAndShiftParameterBaseDate2 = baseDate;
                AddIntervalToDateAndShiftParameterBaseShift2 = baseShift;
                AddIntervalToDateAndShiftParameterIntervalInfo2 = intervalInfo;
                AddIntervalToDateAndShiftCallCount++;
                return AddIntervalToDateAndShiftReturnValue2;
            }
            else
            {
                AddIntervalToDateAndShiftParameterBaseDate3 = baseDate;
                AddIntervalToDateAndShiftParameterBaseShift3 = baseShift;
                AddIntervalToDateAndShiftParameterIntervalInfo3 = intervalInfo;
                AddIntervalToDateAndShiftCallCount++;
                return AddIntervalToDateAndShiftReturnValue3;
            }
        }
    }

    public class TestDateCalculatorTest
    {
        [TestCase(2)]
        [TestCase(5)]
        public void CalculateTestDateReturnsStartDateAsResultDateIfItIsInTheFuture(int offset)
        {
            var tuple = CreateTestDateCalculatorTuple();
            var startDate = DateTime.Now.ToUniversalTime().Date.AddDays(offset);
            var result = tuple.calculator.CalculateTestDate(new TestLevel(), null, null, startDate, null);
            Assert.AreEqual(startDate, result.resultDate);
        }

        [TestCase(true, true, IntervalType.XTimesAShift, Shift.FirstShiftOfDay)]
        [TestCase(false, true, IntervalType.XTimesAShift, Shift.FirstShiftOfDay)]
        [TestCase(true, false, IntervalType.XTimesAShift, Shift.FirstShiftOfDay)]
        [TestCase(false, false, IntervalType.XTimesAShift, Shift.FirstShiftOfDay)]
        [TestCase(true, true, IntervalType.EveryXShifts, Shift.FirstShiftOfDay)]
        [TestCase(false, true, IntervalType.EveryXShifts, Shift.FirstShiftOfDay)]
        [TestCase(true, false, IntervalType.EveryXShifts, Shift.FirstShiftOfDay)]
        [TestCase(false, false, IntervalType.EveryXShifts, Shift.FirstShiftOfDay)]
        [TestCase(true, true, IntervalType.XTimesADay, null)]
        [TestCase(true, true, IntervalType.EveryXDays, null)]
        [TestCase(true, true, IntervalType.XTimesAWeek, null)]
        [TestCase(true, true, IntervalType.EveryXDays, null)]
        public void CalculateTestDateReturnsFirstShiftOfDayAsResultShiftIfStartDateIsInTheFuture(bool lateActive, bool thirdActive, IntervalType intervalType, Shift? expectedShift)
        {
            var tuple = CreateTestDateCalculatorTuple(new ShiftManagement()
            {
                IsSecondShiftActive = lateActive,
                IsThirdShiftActive = thirdActive
            });
            var startDate = DateTime.Now.ToUniversalTime().Date.AddDays(5);
            var result = tuple.calculator.CalculateTestDate(new TestLevel()
            {
                TestInterval = new Interval() { Type = intervalType }
            }, null, null, startDate, null);
            Assert.AreEqual(expectedShift, result.resultShift);
        }

        [TestCase(true, true, IntervalType.XTimesAShift)]
        [TestCase(false, true, IntervalType.XTimesAShift)]
        [TestCase(true, false, IntervalType.XTimesAShift)]
        [TestCase(false, false, IntervalType.XTimesAShift)]
        [TestCase(true, true, IntervalType.EveryXShifts)]
        [TestCase(false, true, IntervalType.EveryXShifts)]
        [TestCase(true, false, IntervalType.EveryXShifts)]
        [TestCase(false, false, IntervalType.EveryXShifts)]
        [TestCase(true, true, IntervalType.XTimesADay)]
        [TestCase(true, true, IntervalType.EveryXDays)]
        [TestCase(true, true, IntervalType.XTimesAWeek)]
        [TestCase(true, true, IntervalType.EveryXDays)]
        public void CalculateTestDateReturnsFirstShiftOfDayOrNullAsNewEndOfLastTestPeriodShiftIfStartDateIsInTheFuture(bool lateActive, bool thirdActive, IntervalType intervalType)
        {
            var tuple = CreateTestDateCalculatorTuple(new ShiftManagement()
            {
                IsSecondShiftActive = lateActive,
                IsThirdShiftActive = thirdActive
            });
            var startDate = DateTime.Now.ToUniversalTime().Date.AddDays(5);
            var result = tuple.calculator.CalculateTestDate(new TestLevel()
            {
                TestInterval = new Interval() { Type = intervalType }
            }, null, null, startDate, (sd, ss, ed, es) => new List<(DateTime, Shift?)>());
            if(intervalType == IntervalType.EveryXShifts || intervalType == IntervalType.XTimesAShift)
            {
                Assert.AreEqual(Shift.FirstShiftOfDay, result.newEndOfLastTestPeriodShift);
            }
            else
            {
                Assert.IsNull(result.newEndOfLastTestPeriodShift); 
            }
        }

        [TestCase("2020-12-19 15:00:00", Shift.SecondShiftOfDay, IntervalType.EveryXShifts, "2020-03-05")]
        [TestCase("2020-12-19 23:00:00", null, IntervalType.EveryXDays, "2009-03-05")]
        public void CalculateTestDateCallsGetClassicTestsForTimePeriodCorrectly(DateTime now, Shift? shiftNow, IntervalType intervalType, DateTime startDate)
        {
            var tuple = CreateTestDateCalculatorTuple(new ShiftManagement()
            {
                IsSecondShiftActive = true,
                IsThirdShiftActive = true,
                FirstShiftStart = new TimeSpan(6, 0, 0),
                FirstShiftEnd = new TimeSpan(14, 0, 0),
                SecondShiftStart = new TimeSpan(14, 0, 0),
                SecondShiftEnd = new TimeSpan(22, 0, 0),
                ThirdShiftStart = new TimeSpan(22, 0, 0),
                ThirdShiftEnd = new TimeSpan(6, 0, 0)
            });
            var today = now.ToUniversalTime().Date;
            
            DateTime paramStartDate = new DateTime();
            Shift? paramStartShift = Shift.FirstShiftOfDay;
            DateTime paramEndDate = new DateTime();
            Shift? paramEndShift = Shift.FirstShiftOfDay;
            bool invoked = false;

            Func<DateTime, Shift?, DateTime, Shift?, List<(DateTime, Shift?)>> getClassicTestDatesForTimePeriod = (sd, ss, ed, es) =>
            {
                if(!invoked)
                {
                    paramStartDate = sd;
                    paramStartShift = ss;
                    paramEndDate = ed;
                    paramEndShift = es;
                }

                invoked = true;
                return new List<(DateTime, Shift?)>();
            };

            tuple.calculator.CalculateTestDate(new TestLevel() { TestInterval = new Interval() { Type = intervalType } },
                new DateTime(1998, 8, 8),
                Shift.FirstShiftOfDay,
                startDate,
                getClassicTestDatesForTimePeriod,
                now);
            
            Assert.AreEqual(startDate, paramStartDate);
            Assert.AreEqual(today, paramEndDate);
            Assert.AreEqual(shiftNow, paramEndShift);
            
            if(intervalType == IntervalType.EveryXShifts || intervalType == IntervalType.XTimesAShift)
            {
                Assert.AreEqual(Shift.FirstShiftOfDay, paramStartShift);
            }
            else
            {
                Assert.IsNull(paramStartShift);
            }
        }

        [TestCase(IntervalType.EveryXShifts, "2020-03-05", "2009-03-05", Shift.SecondShiftOfDay, "2020-05-06", Shift.ThirdShiftOfDay)]
        [TestCase(IntervalType.EveryXDays, "2009-03-05", "1999-03-05", Shift.ThirdShiftOfDay, "1999-03-05", null)]
        public void CalculateTestDateCallsAdderIfThereAreOneTestsInPeriodForDynamicInterval(IntervalType intervalType, DateTime startDate, DateTime testDate, Shift? testShift, DateTime resultDate, Shift? resultShift)
        {
            var tuple = CreateTestDateCalculatorTuple();
            var today = DateTime.Now.ToUniversalTime().Date;
            var testLevel = new TestLevel() { TestInterval = new Interval() { Type = intervalType } };
            tuple.adder.AddIntervalToDateAndShiftReturnValue = (resultDate, resultShift);

            var result = tuple.calculator.CalculateTestDate(testLevel,
                new DateTime(1998, 8, 8),
                Shift.FirstShiftOfDay,
                startDate,
                (sd, ss, ed, es) => new List<(DateTime, Shift?)>() { (testDate, testShift) });

            Assert.AreEqual(testDate, tuple.adder.AddIntervalToDateAndShiftParameterBaseDate);
            Assert.AreEqual(testShift, tuple.adder.AddIntervalToDateAndShiftParameterBaseShift);
            Assert.AreSame(testLevel, tuple.adder.AddIntervalToDateAndShiftParameterIntervalInfo);
            Assert.AreEqual(resultShift, result.resultShift);
            Assert.AreEqual(resultDate, result.resultDate);
            Assert.AreEqual(intervalType == IntervalType.EveryXShifts ? Shift.FirstShiftOfDay : null, result.newEndOfLastTestPeriodShift);
            Assert.AreEqual(startDate, result.newEndOfLastTestPeriod);
        }

        [TestCase(IntervalType.EveryXShifts, "2020-03-05")]
        [TestCase(IntervalType.EveryXDays, "2009-03-05")]
        public void CalculateTestDateReturnsCorrectValueIfThereAreNoTestsInPeriodForDynamicInterval(IntervalType intervalType, DateTime startDate)
        {
            var tuple = CreateTestDateCalculatorTuple();
            var today = DateTime.Now.ToUniversalTime().Date;
            var testLevel = new TestLevel() { TestInterval = new Interval() { Type = intervalType } };

            var result = tuple.calculator.CalculateTestDate(testLevel,
                new DateTime(1998, 8, 8),
                Shift.FirstShiftOfDay,
                startDate,
                (sd, ss, ed, es) => new List<(DateTime, Shift?)>());

            Assert.AreEqual(intervalType == IntervalType.EveryXShifts ? Shift.FirstShiftOfDay : null, result.resultShift);
            Assert.AreEqual(startDate, result.resultDate);
            Assert.AreEqual(intervalType == IntervalType.EveryXShifts ? Shift.FirstShiftOfDay : null, result.newEndOfLastTestPeriodShift);
            Assert.AreEqual(startDate, result.newEndOfLastTestPeriod);
        }

        [TestCase(IntervalType.EveryXShifts, "2020-03-05", Shift.ThirdShiftOfDay)]
        [TestCase(IntervalType.EveryXDays, "2009-03-05", Shift.SecondShiftOfDay)]
        public void CalculateTestDateCallsAdderIfThereAreTestsInPeriodForDynamicInterval(IntervalType intervalType, DateTime lastTestDate, Shift lastTestShift)
        {
            var tuple = CreateTestDateCalculatorTuple();
            var today = DateTime.Now.ToUniversalTime().Date;
            var testLevel = new TestLevel() { TestInterval = new Interval() { Type = intervalType } };

            tuple.calculator.CalculateTestDate(testLevel,
                new DateTime(),
                Shift.FirstShiftOfDay,
                new DateTime(1998, 8, 8),
                (sd, ss, ed, es) => new List<(DateTime, Shift?)>()
                    {
                        (lastTestDate.AddDays(-5), Shift.SecondShiftOfDay),
                        (lastTestDate, lastTestShift),
                        (lastTestDate.AddDays(-2), Shift.ThirdShiftOfDay)
                    });

            Assert.AreEqual(lastTestDate, tuple.adder.AddIntervalToDateAndShiftParameterBaseDate);
            Assert.AreEqual(lastTestShift, tuple.adder.AddIntervalToDateAndShiftParameterBaseShift);
            Assert.AreSame(testLevel, tuple.adder.AddIntervalToDateAndShiftParameterIntervalInfo);
        }

        [TestCase(IntervalType.EveryXShifts, "2020-03-05", Shift.ThirdShiftOfDay, "2020-06-15", Shift.SecondShiftOfDay)]
        [TestCase(IntervalType.EveryXDays, "2009-03-05", Shift.SecondShiftOfDay, "2020-06-15", null)]
        public void CalculateTestDateReturnsDataFromAdderIfThereAreTestsInPeriodForDynamicInterval(IntervalType intervalType, DateTime lastTestDate, Shift lastTestShift, DateTime resultDate, Shift? resultShift)
        {
            var tuple = CreateTestDateCalculatorTuple();
            var today = DateTime.Now.ToUniversalTime().Date;
            tuple.adder.AddIntervalToDateAndShiftReturnValue = (resultDate, resultShift);
            
            var result = tuple.calculator.CalculateTestDate(new TestLevel() { TestInterval = new Interval() { Type = intervalType } },
                new DateTime(),
                Shift.FirstShiftOfDay,
                new DateTime(1998, 8, 8),
                (sd, ss, ed, es) => new List<(DateTime, Shift?)>()
                {
                    (lastTestDate.AddDays(-5), Shift.SecondShiftOfDay),
                    (lastTestDate, lastTestShift),
                    (lastTestDate.AddDays(-2), Shift.ThirdShiftOfDay)
                });

            Assert.AreEqual(resultDate, result.resultDate);
            Assert.AreEqual(resultShift, result.resultShift);
            Assert.AreEqual(new DateTime(1998, 8, 8), result.newEndOfLastTestPeriod);
            if (intervalType == IntervalType.EveryXShifts)
            {
                Assert.AreEqual(Shift.FirstShiftOfDay, result.newEndOfLastTestPeriodShift); 
            }
            else
            {
                Assert.IsNull(result.newEndOfLastTestPeriodShift);
            }
        }
        
        [TestCase("2020-12-19 15:00:00", Shift.SecondShiftOfDay, IntervalType.XTimesADay, "2020-12-17", Shift.FirstShiftOfDay, "2020-12-18")]
        [TestCase("2020-12-19 23:00:00", Shift.ThirdShiftOfDay, IntervalType.XTimesAWeek, "2020-12-14", Shift.SecondShiftOfDay, "2020-12-14")]
        [TestCase("2020-12-20 07:00:00", Shift.FirstShiftOfDay, IntervalType.XTimesADay, "2020-12-16", Shift.ThirdShiftOfDay, "2020-12-19")]
        [TestCase("2020-12-19 15:00:00", Shift.SecondShiftOfDay, IntervalType.XTimesAWeek, "2020-12-09", null, "2020-12-16")]
        [TestCase("2020-12-19 23:00:00", Shift.ThirdShiftOfDay, IntervalType.XTimesADay, "2020-12-11", Shift.FirstShiftOfDay, "2020-12-18")]
        [TestCase("2020-12-20 07:00:00", Shift.FirstShiftOfDay, IntervalType.XTimesAWeek, "2020-11-25", null, "2020-12-16")]
        public void CalculateTestDateCallsGetClassicTestDatesForTimePeriodForStaticDaysAndWeeksInterval(DateTime now, Shift shiftNow, IntervalType intervalType, DateTime endOfLastTestPeriod, Shift? endOfLastTestPeriodShift, DateTime timePeriodStart)
        {
            var tuple = CreateTestDateCalculatorTuple(new ShiftManagement()
            {
                IsSecondShiftActive = true,
                IsThirdShiftActive = true,
                FirstShiftStart = new TimeSpan(6, 0, 0),
                FirstShiftEnd = new TimeSpan(14, 0, 0),
                SecondShiftStart = new TimeSpan(14, 0, 0),
                SecondShiftEnd = new TimeSpan(22, 0, 0),
                ThirdShiftStart = new TimeSpan(22, 0, 0),
                ThirdShiftEnd = new TimeSpan(6, 0, 0)
            });
            var today = now.ToUniversalTime().Date;

            DateTime paramStartDate = new DateTime();
            Shift? paramStartShift = Shift.FirstShiftOfDay;
            DateTime paramEndDate = new DateTime();
            Shift? paramEndShift = Shift.FirstShiftOfDay;

            Func<DateTime, Shift?, DateTime, Shift?, List<(DateTime, Shift?)>> getClassicTestDatesForTimePeriod = (sd, ss, ed, es) =>
            {
                paramStartDate = sd;
                paramStartShift = ss;
                paramEndDate = ed;
                paramEndShift = es;
                return new List<(DateTime, Shift?)>();
            };
            tuple.adder.AddIntervalToDateAndShiftReturnValue = (timePeriodStart, endOfLastTestPeriodShift);
            tuple.adder.AddIntervalToDateAndShiftReturnValue2 = (timePeriodStart, endOfLastTestPeriodShift);
            
            tuple.calculator.CalculateTestDate(new TestLevel() { TestInterval = new Interval() { Type = intervalType } },
                endOfLastTestPeriod,
                endOfLastTestPeriodShift,
                new DateTime(1998, 8, 8),
                getClassicTestDatesForTimePeriod,
                now);

            Assert.AreEqual(timePeriodStart, paramStartDate);
            Assert.AreEqual(endOfLastTestPeriodShift, paramStartShift);
            Assert.AreEqual(today, paramEndDate);
            Assert.AreEqual(shiftNow, paramEndShift);
        }

        [TestCase("2020-12-19 15:00:00", Shift.SecondShiftOfDay, "2020-12-19", Shift.FirstShiftOfDay, "2020-12-19", Shift.FirstShiftOfDay, true, true)]
        [TestCase("2020-12-19 23:00:00", Shift.ThirdShiftOfDay, "2020-12-19", Shift.SecondShiftOfDay, "2020-12-19", Shift.SecondShiftOfDay, true, true)]
        [TestCase("2020-12-20 07:00:00", Shift.FirstShiftOfDay, "2020-12-19", Shift.ThirdShiftOfDay, "2020-12-19", Shift.ThirdShiftOfDay, true, true)]
        [TestCase("2020-12-19 15:00:00", Shift.SecondShiftOfDay, "2020-12-18", Shift.ThirdShiftOfDay, "2020-12-19", Shift.FirstShiftOfDay, true, true)]
        [TestCase("2020-12-19 23:00:00", Shift.ThirdShiftOfDay, "2020-12-19", Shift.FirstShiftOfDay, "2020-12-19", Shift.SecondShiftOfDay, true, true)]
        [TestCase("2020-12-20 07:00:00", Shift.FirstShiftOfDay, "2020-12-19", Shift.SecondShiftOfDay, "2020-12-19", Shift.ThirdShiftOfDay, true, true)]
        [TestCase("2020-12-19 23:00:00", Shift.ThirdShiftOfDay, "2020-12-19", Shift.FirstShiftOfDay, "2020-12-19", Shift.FirstShiftOfDay, false, true)]
        [TestCase("2020-12-19 23:00:00", Shift.ThirdShiftOfDay, "2020-12-18", Shift.ThirdShiftOfDay, "2020-12-19", Shift.FirstShiftOfDay, false, true)]
        [TestCase("2020-12-19 07:00:00", Shift.FirstShiftOfDay, "2020-12-17", Shift.FirstShiftOfDay, "2020-12-18", Shift.FirstShiftOfDay, false, false)]
        [TestCase("2020-12-19 07:00:00", Shift.FirstShiftOfDay, "2020-12-18", Shift.FirstShiftOfDay, "2020-12-18", Shift.SecondShiftOfDay, true, false)]
        public void CalculateTestDateCallsGetClassicTestDatesForTimePeriodForStaticShiftsInterval(DateTime now, Shift shiftNow, DateTime endOfLastTestPeriod, Shift endOfLastTestPeriodShift, DateTime timePeriodStart, Shift timePeriodStartShift, bool lateActive, bool thirdActive)
        {
            var tuple = CreateTestDateCalculatorTuple(new ShiftManagement()
            {
                IsSecondShiftActive = lateActive,
                IsThirdShiftActive = thirdActive,
                FirstShiftStart = new TimeSpan(6, 0, 0),
                FirstShiftEnd = new TimeSpan(14, 0, 0),
                SecondShiftStart = new TimeSpan(14, 0, 0),
                SecondShiftEnd = new TimeSpan(22, 0, 0),
                ThirdShiftStart = new TimeSpan(22, 0, 0),
                ThirdShiftEnd = new TimeSpan(6, 0, 0)
            });

            DateTime paramStartDate = new DateTime();
            Shift? paramStartShift = Shift.FirstShiftOfDay;
            DateTime paramEndDate = new DateTime();
            Shift? paramEndShift = Shift.FirstShiftOfDay;

            Func<DateTime, Shift?, DateTime, Shift?, List<(DateTime, Shift?)>> getClassicTestDatesForTimePeriod = (sd, ss, ed, es) =>
            {
                paramStartDate = sd;
                paramStartShift = ss;
                paramEndDate = ed;
                paramEndShift = es;
                return new List<(DateTime, Shift?)>();
            };
            tuple.adder.AddIntervalToDateAndShiftReturnValue = (timePeriodStart, timePeriodStartShift);
            tuple.adder.AddIntervalToDateAndShiftReturnValue2 = (timePeriodStart, timePeriodStartShift);

            tuple.calculator.CalculateTestDate(new TestLevel() { TestInterval = new Interval() { Type = IntervalType.XTimesAShift} },
                endOfLastTestPeriod,
                endOfLastTestPeriodShift,
                new DateTime(1998, 8, 8),
                getClassicTestDatesForTimePeriod,
                now);

            Assert.AreEqual(timePeriodStart, paramStartDate);
            Assert.AreEqual(timePeriodStartShift, paramStartShift);
            Assert.AreEqual(now.Date, paramEndDate);
            Assert.AreEqual(shiftNow, paramEndShift);
        }

        [TestCase(IntervalType.XTimesADay, 5, 2, "2020-03-05", Shift.ThirdShiftOfDay, "2020-03-06")]
        [TestCase(IntervalType.XTimesADay, 9, 8, "2009-03-05", Shift.SecondShiftOfDay, "2009-03-06")]
        [TestCase(IntervalType.XTimesAWeek, 2, 1, "1999-03-05", null, "1999-03-06")]
        public void CalculateTestDateCallsAdderIfThereAreNotEnoughTestsInPeriodForStaticInterval(IntervalType intervalType, int intervalValue, int returnedTests, DateTime endOfLastTestPeriod, Shift? endOfLastTestPeriodShift, DateTime now)
        {
            var tuple = CreateTestDateCalculatorTuple(new ShiftManagement()
            {
                IsSecondShiftActive = true,
                IsThirdShiftActive = true,
                FirstShiftStart = new TimeSpan(6, 0, 0),
                FirstShiftEnd = new TimeSpan(14, 0, 0),
                SecondShiftStart = new TimeSpan(14, 0, 0),
                SecondShiftEnd = new TimeSpan(22, 0, 0),
                ThirdShiftStart = new TimeSpan(22, 0, 0),
                ThirdShiftEnd = new TimeSpan(6, 0, 0)
            });
            var testLevel = new TestLevel() { TestInterval = new Interval() { Type = intervalType, IntervalValue = intervalValue} };
            var list = new List<(DateTime, Shift?)>();
            for(int i = 0; i < returnedTests; i++)
            {
                list.Add((new DateTime(), null));
            }

            tuple.calculator.CalculateTestDate(testLevel,
                endOfLastTestPeriod,
                endOfLastTestPeriodShift,
                new DateTime(1998, 8, 8),
                (sd, ss, ed, es) => list,
                now);

            Assert.AreEqual(endOfLastTestPeriod, tuple.adder.AddIntervalToDateAndShiftParameterBaseDate2);
            Assert.AreEqual(endOfLastTestPeriodShift, tuple.adder.AddIntervalToDateAndShiftParameterBaseShift2);
            Assert.AreSame(testLevel, tuple.adder.AddIntervalToDateAndShiftParameterIntervalInfo2);
        }

        [TestCase(IntervalType.XTimesADay, 5, 2, "2020-03-05", Shift.ThirdShiftOfDay, "2020-03-06", "2020-04-06", Shift.FirstShiftOfDay)]
        [TestCase(IntervalType.XTimesADay, 9, 8, "2009-03-05", Shift.SecondShiftOfDay, "2009-03-06", "2020-07-06", Shift.SecondShiftOfDay)]
        [TestCase(IntervalType.XTimesAWeek, 2, 1, "1999-03-05", null, "1999-03-06", "2020-06-06", Shift.ThirdShiftOfDay)]
        public void CalculateTestDateReturnsDataFromAdderIfThereAreNotEnoughTestsInPeriodForStaticInterval(IntervalType intervalType, int intervalValue, int returnedTests, DateTime endOfLastTestPeriod, Shift? endOfLastTestPeriodShift, DateTime now, DateTime resultDate, Shift resultShift)
        {
            var tuple = CreateTestDateCalculatorTuple(new ShiftManagement()
            {
                IsSecondShiftActive = true,
                IsThirdShiftActive = true,
                FirstShiftStart = new TimeSpan(6, 0, 0),
                FirstShiftEnd = new TimeSpan(14, 0, 0),
                SecondShiftStart = new TimeSpan(14, 0, 0),
                SecondShiftEnd = new TimeSpan(22, 0, 0),
                ThirdShiftStart = new TimeSpan(22, 0, 0),
                ThirdShiftEnd = new TimeSpan(6, 0, 0)
            });
            var testLevel = new TestLevel() { TestInterval = new Interval() { Type = intervalType, IntervalValue = intervalValue } };
            var list = new List<(DateTime, Shift?)>();
            for (int i = 0; i < returnedTests; i++)
            {
                list.Add((new DateTime(), null));
            }
            tuple.adder.AddIntervalToDateAndShiftReturnValue = (resultDate, resultShift);
            tuple.adder.AddIntervalToDateAndShiftReturnValue2 = (resultDate, resultShift);

            var result = tuple.calculator.CalculateTestDate(testLevel,
                endOfLastTestPeriod,
                endOfLastTestPeriodShift,
                new DateTime(1998, 8, 8),
                (sd, ss, ed, es) => list,
                now);

            Assert.AreEqual(resultDate, result.resultDate);
            Assert.AreEqual(resultShift, result.resultShift);
            Assert.AreEqual(endOfLastTestPeriod, result.newEndOfLastTestPeriod);
            Assert.AreEqual(endOfLastTestPeriodShift, result.newEndOfLastTestPeriodShift);
        }

        [TestCase(IntervalType.XTimesADay, 5, 6, "2020-03-05", Shift.ThirdShiftOfDay, "2020-03-25", Shift.FirstShiftOfDay, "2020-03-06")]
        [TestCase(IntervalType.XTimesADay, 9, 15, "2009-03-05", Shift.SecondShiftOfDay, "2009-03-25", null, "2009-03-06")]
        [TestCase(IntervalType.XTimesAWeek, 2, 10, "1999-03-05", null, "2000-03-25", Shift.SecondShiftOfDay, "1999-03-06")]
        public void CalculateTestDateCallsAdderFirstlyIfThereAreTestsInPeriodForStaticInterval(IntervalType intervalType, int intervalValue, int returnedTests, DateTime endOfLastTestPeriod, Shift? endOfLastTestPeriodShift, DateTime adderDateReturn, Shift? adderShiftReturn, DateTime now)
        {
            var tuple = CreateTestDateCalculatorTuple(new ShiftManagement()
            {
                IsSecondShiftActive = true,
                IsThirdShiftActive = true,
                FirstShiftStart = new TimeSpan(6, 0, 0),
                FirstShiftEnd = new TimeSpan(14, 0, 0),
                SecondShiftStart = new TimeSpan(14, 0, 0),
                SecondShiftEnd = new TimeSpan(22, 0, 0),
                ThirdShiftStart = new TimeSpan(22, 0, 0),
                ThirdShiftEnd = new TimeSpan(6, 0, 0)
            });
            var testLevel = new TestLevel() { TestInterval = new Interval() { Type = intervalType, IntervalValue = intervalValue }, ConsiderWorkingCalendar = true };
            var list = new List<(DateTime, Shift?)>();
            for (int i = 0; i < returnedTests; i++)
            {
                list.Add((new DateTime(), null));
            }
            tuple.adder.AddIntervalToDateAndShiftReturnValue2 = (adderDateReturn, adderShiftReturn);

            var result = tuple.calculator.CalculateTestDate(testLevel,
                endOfLastTestPeriod,
                endOfLastTestPeriodShift,
                new DateTime(1998, 8, 8),
                (sd, ss, ed, es) => list,
                now);

            Assert.AreEqual(endOfLastTestPeriod, tuple.adder.AddIntervalToDateAndShiftParameterBaseDate2);
            Assert.AreEqual(endOfLastTestPeriodShift, tuple.adder.AddIntervalToDateAndShiftParameterBaseShift2);
            Assert.IsFalse(tuple.adder.AddIntervalToDateAndShiftParameterIntervalInfo2.ConsiderWorkingCalendar);
            Assert.IsTrue(testLevel.TestInterval.EqualsByContent(tuple.adder.AddIntervalToDateAndShiftParameterIntervalInfo2.TestInterval));
            Assert.AreEqual(endOfLastTestPeriod, result.newEndOfLastTestPeriod);
            Assert.AreEqual(endOfLastTestPeriodShift, result.newEndOfLastTestPeriodShift);
        }

        [TestCase(IntervalType.XTimesADay, 5, 6, "2020-03-05", Shift.ThirdShiftOfDay, "2020-03-25", Shift.FirstShiftOfDay, "2020-03-06")]
        [TestCase(IntervalType.XTimesADay, 9, 15, "2009-03-05", Shift.SecondShiftOfDay, "2009-03-25", null, "2009-03-06")]
        [TestCase(IntervalType.XTimesAWeek, 2, 10, "1999-03-05", null, "2000-03-25", Shift.SecondShiftOfDay, "1999-03-06")]
        public void CalculateTestDateCallsAdderSecondlyIfThereAreTestsInPeriodForStaticInterval(IntervalType intervalType, int intervalValue, int returnedTests, DateTime adderDateReturn1, Shift? adderShiftReturn1, DateTime adderDateReturn2, Shift? adderShiftReturn2, DateTime now)
        {
            var tuple = CreateTestDateCalculatorTuple(new ShiftManagement()
            {
                IsSecondShiftActive = true,
                IsThirdShiftActive = true,
                FirstShiftStart = new TimeSpan(6, 0, 0),
                FirstShiftEnd = new TimeSpan(14, 0, 0),
                SecondShiftStart = new TimeSpan(14, 0, 0),
                SecondShiftEnd = new TimeSpan(22, 0, 0),
                ThirdShiftStart = new TimeSpan(22, 0, 0),
                ThirdShiftEnd = new TimeSpan(6, 0, 0)
            });
            var testLevel = new TestLevel() { TestInterval = new Interval() { Type = intervalType, IntervalValue = intervalValue } };
            var list = new List<(DateTime, Shift?)>();
            for (int i = 0; i < returnedTests; i++)
            {
                list.Add((new DateTime(), null));
            }
            tuple.adder.AddIntervalToDateAndShiftReturnValue2 = (adderDateReturn1, adderShiftReturn1);
            tuple.adder.AddIntervalToDateAndShiftReturnValue3 = (adderDateReturn2, adderShiftReturn2);

            var result = tuple.calculator.CalculateTestDate(testLevel,
                now.AddDays(-1),
                null,
                new DateTime(1998, 8, 8),
                (sd, ss, ed, es) => list,
                now);

            Assert.AreEqual(3, tuple.adder.AddIntervalToDateAndShiftCallCount);
            Assert.AreEqual(adderDateReturn1, tuple.adder.AddIntervalToDateAndShiftParameterBaseDate3);
            Assert.AreEqual(adderShiftReturn1, tuple.adder.AddIntervalToDateAndShiftParameterBaseShift3);
            Assert.AreSame(testLevel, tuple.adder.AddIntervalToDateAndShiftParameterIntervalInfo3);
            Assert.AreEqual(adderDateReturn2, result.resultDate);
            Assert.AreEqual(adderShiftReturn2, result.resultShift);
        }


        static (TestDateCalculator calculator, TestIntervalAdderMock adder) CreateTestDateCalculatorTuple(ShiftManagement shiftManagement = null)
        {
            var adder = new TestIntervalAdderMock();
            var calculator = new TestDateCalculator(adder, shiftManagement ?? new ShiftManagement()
            {
                IsSecondShiftActive = true,
                IsThirdShiftActive = true
            });
            return (calculator, adder);
        }
    }
}
