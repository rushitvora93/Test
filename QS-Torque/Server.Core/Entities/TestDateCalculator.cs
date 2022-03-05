using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Server.Core.Enums;

namespace Server.Core.Entities
{
    public interface ITestDateCalculator
    {
        /// <param name="getClassicTestDatesForTimePeriod">Parameter 1: start date of period; Parameter 2: start shift of period; Parameter 3: end date of period ; Parameter 4: end shift of period</param>
        (DateTime resultDate, Shift? resultShift, DateTime? newEndOfLastTestPeriod, Shift? newEndOfLastTestPeriodShift) CalculateTestDate(TestLevel intervalInfo,
                DateTime? endOfLastTestPeriod,
                Shift? endOfLastTestPeriodShift,
                DateTime startDate,
                Func<DateTime, Shift?, DateTime, Shift?, List<(DateTime, Shift?)>> getClassicTestDatesForTimePeriod);
    }


    public class TestDateCalculator : ITestDateCalculator
    {
        private ITestIntervalAdder _testIntervalAdder;
        private ShiftManagement _shiftManagement;
        private List<Shift> _activeShifts = new List<Shift>();

        public TestDateCalculator(ITestIntervalAdder testIntervalAdder, ShiftManagement shiftManagement)
        {
            _testIntervalAdder = testIntervalAdder;
            _shiftManagement = shiftManagement;
            
            _activeShifts.Add(Shift.FirstShiftOfDay);       // First shift is always active
            if (_shiftManagement.IsSecondShiftActive)
            {
                _activeShifts.Add(Shift.SecondShiftOfDay);
            }
            if (_shiftManagement.IsThirdShiftActive)
            {
                _activeShifts.Add(Shift.ThirdShiftOfDay);
            }

            if (_activeShifts.Count == 0)
            {
                throw new InvalidOperationException("There have to be some active shifts");
            }
        }

        /// <param name="getClassicTestDatesForTimePeriod">Parameter 1: start date of period; Parameter 2: start shift of period; Parameter 3: end date of period ; Parameter 4: end shift of period</param>
        public (DateTime resultDate, Shift? resultShift, DateTime? newEndOfLastTestPeriod, Shift? newEndOfLastTestPeriodShift)
            CalculateTestDate(TestLevel intervalInfo,
                DateTime? endOfLastTestPeriod,
                Shift? endOfLastTestPeriodShift,
                DateTime startDate,
                Func<DateTime, Shift?, DateTime, Shift?, List<(DateTime, Shift?)>> getClassicTestDatesForTimePeriod)
        {
            return CalculateTestDate(intervalInfo, endOfLastTestPeriod, endOfLastTestPeriodShift, startDate, getClassicTestDatesForTimePeriod, null);
        }

        /// <param name="getClassicTestDatesForTimePeriod">Parameter 1: start date of period; Parameter 2: start shift of period; Parameter 3: end date of period ; Parameter 4: end shift of period</param>
        public (DateTime resultDate, Shift? resultShift, DateTime? newEndOfLastTestPeriod, Shift? newEndOfLastTestPeriodShift) 
            CalculateTestDate(TestLevel intervalInfo, 
            DateTime? endOfLastTestPeriod,
            Shift? endOfLastTestPeriodShift,
            DateTime startDate,
            Func<DateTime, Shift?, DateTime, Shift?, List<(DateTime, Shift?)>> getClassicTestDatesForTimePeriod,
            DateTime? nullableNow)
        {
            var shiftCalculator = new ShiftCalculator(_shiftManagement);
            var now = nullableNow ?? DateTime.Now;
            var today = now.Date;
            bool intervalInShifts = intervalInfo.TestInterval.Type == IntervalType.EveryXShifts || intervalInfo.TestInterval.Type == IntervalType.XTimesAShift;

            if (today < startDate)
            {
                return (startDate,
                    intervalInShifts ? Shift.FirstShiftOfDay : null, 
                    startDate,
                    intervalInShifts ? Shift.FirstShiftOfDay : null);
            }

            if (intervalInfo.TestInterval.IsIntervalDynamic())
            {
                var existingTestsFromStartToNow = getClassicTestDatesForTimePeriod(startDate,
                    intervalInShifts ? Shift.FirstShiftOfDay : null,
                    today,
                    intervalInShifts ? shiftCalculator.GetShiftForDateTime(now) : null);

                if (existingTestsFromStartToNow.Count == 0)
                {
                    // Required tests do not exist
                    return (startDate,
                            intervalInShifts ? Shift.FirstShiftOfDay : null,
                            startDate,
                            intervalInShifts ? Shift.FirstShiftOfDay : null);
                }
                else
                {
                    var lastDynamicTestDate = existingTestsFromStartToNow.Max(x => x.Item1);
                    var lastDynamicTestShift = existingTestsFromStartToNow.Where(x => x.Item1 == lastDynamicTestDate).Max(x => x.Item2);
                    var dynamicFulfilledResult = _testIntervalAdder.AddIntervalToDateAndShift(lastDynamicTestDate, lastDynamicTestShift, intervalInfo);
                    return (dynamicFulfilledResult.resultDate, dynamicFulfilledResult.resultShift, startDate, intervalInShifts ? Shift.FirstShiftOfDay : null);
                }
            }

            if (intervalInfo.TestInterval.IsIntervalStatic())
            {
                if(today == startDate)
                {
                    if(!intervalInShifts || (intervalInShifts && shiftCalculator.GetShiftForDateTime(now) == Shift.FirstShiftOfDay))
                    {
                        return (startDate,
                                intervalInShifts ? Shift.FirstShiftOfDay : null,
                                startDate,
                                intervalInShifts ? Shift.FirstShiftOfDay : null);
                    }
                }

                var staticEndOfLastTestPeriod = endOfLastTestPeriod ?? startDate;
                var staticEndOfLastTestPeriodShift = endOfLastTestPeriodShift;

                while(DistanceFromDateAndShiftToNowIsGreaterThanStaticInterval(staticEndOfLastTestPeriod, staticEndOfLastTestPeriodShift, now, intervalInfo))
                {
                    var correctionInterval = intervalInfo.CopyDeep();
                    correctionInterval.ConsiderWorkingCalendar = false;
                    var testPeriodCorrectionResult = _testIntervalAdder.AddIntervalToDateAndShift(staticEndOfLastTestPeriod, staticEndOfLastTestPeriodShift, correctionInterval);
                    staticEndOfLastTestPeriod = testPeriodCorrectionResult.resultDate;
                    staticEndOfLastTestPeriodShift = testPeriodCorrectionResult.resultShift;
                }

                var startOfCurrentTestPeriod = _testIntervalAdder.AddIntervalToDateAndShift(staticEndOfLastTestPeriod, staticEndOfLastTestPeriodShift, new TestLevel()
                {
                    TestInterval = intervalInShifts ? new Interval() { IntervalValue = 1, Type = IntervalType.XTimesAShift } : new Interval() { IntervalValue = 1, Type = IntervalType.XTimesADay },
                    ConsiderWorkingCalendar = true
                });

                var staticTestDates = getClassicTestDatesForTimePeriod(startOfCurrentTestPeriod.Item1, startOfCurrentTestPeriod.Item2, today, shiftCalculator.GetShiftForDateTime(now));

                if(staticTestDates.Count >= intervalInfo.TestInterval.IntervalValue)
                {
                    var newTestPeriodEndIntervalInfo = intervalInfo.CopyDeep();
                    newTestPeriodEndIntervalInfo.ConsiderWorkingCalendar = false;
                    var newTestPeriodEnd = _testIntervalAdder.AddIntervalToDateAndShift(staticEndOfLastTestPeriod, staticEndOfLastTestPeriodShift, newTestPeriodEndIntervalInfo);
                    var staticFulfilledResult = _testIntervalAdder.AddIntervalToDateAndShift(newTestPeriodEnd.resultDate, newTestPeriodEnd.resultShift, intervalInfo);
                    if(intervalInShifts)
                    {
                        staticEndOfLastTestPeriod = staticEndOfLastTestPeriod.AddDays(-1);
                    }
                    return (staticFulfilledResult.resultDate, staticFulfilledResult.resultShift, staticEndOfLastTestPeriod, staticEndOfLastTestPeriodShift);
                }
                else
                {
                    var staticNotFulfilledResult = _testIntervalAdder.AddIntervalToDateAndShift(staticEndOfLastTestPeriod, staticEndOfLastTestPeriodShift, intervalInfo);
                    if (intervalInShifts)
                    {
                        staticEndOfLastTestPeriod = staticEndOfLastTestPeriod.AddDays(-1);
                    }
                    return (staticNotFulfilledResult.resultDate, staticNotFulfilledResult.resultShift, staticEndOfLastTestPeriod, staticEndOfLastTestPeriodShift);
                }
            }

            return (new DateTime(), Shift.FirstShiftOfDay, new DateTime(), Shift.FirstShiftOfDay);
        }
        
        private bool DistanceFromDateAndShiftToNowIsGreaterThanStaticInterval(DateTime endOfLastTestPeriod, Shift? endOfLastTestPeriodShift, DateTime now, TestLevel intervalInfo)
        {
            if (intervalInfo.TestInterval.Type == IntervalType.XTimesAShift)
            {
                var shiftCalculator = new ShiftCalculator(_shiftManagement);
                var shifts = (now.Date - endOfLastTestPeriod.Date).Days * _activeShifts.Count;
                shifts -= _activeShifts.IndexOf(endOfLastTestPeriodShift ?? _activeShifts.First());
                shifts += _activeShifts.IndexOf(shiftCalculator.GetShiftForDateTime(now));
                return shifts > 1;
            }
            else if (intervalInfo.TestInterval.Type == IntervalType.XTimesADay)
            {
                return (now.Date - endOfLastTestPeriod.Date).Days > 1;
            }
            else if (intervalInfo.TestInterval.Type == IntervalType.XTimesAWeek)
            {
                return (now.Date - endOfLastTestPeriod.Date).Days > 6;
            }

            return false;
        }
    }
}
