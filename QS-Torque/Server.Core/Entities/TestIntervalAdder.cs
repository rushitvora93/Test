using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Server.Core.Enums;

namespace Server.Core.Entities
{
    public interface ITestIntervalAdder
    {
        (DateTime resultDate, Shift? resultShift) AddIntervalToDateAndShift(DateTime baseDate, Shift? baseShift, TestLevel intervalInfo);
    }
    
    public class TestIntervalAdder : ITestIntervalAdder
    {
        private WorkingCalendar _workingCalendar;
        private List<WorkingCalendarEntry> _workingCalendarEntries;
        private ShiftManagement _shiftManagement;

        public TestIntervalAdder(WorkingCalendar workingCalendar, List<WorkingCalendarEntry> workingCalendarEntries, ShiftManagement shiftManagement)
        {
            _workingCalendar = workingCalendar;
            _workingCalendarEntries = workingCalendarEntries;
            _shiftManagement = shiftManagement;
        }
        
        public (DateTime resultDate, Shift? resultShift) AddIntervalToDateAndShift(DateTime baseDate, Shift? baseShift, TestLevel intervalInfo)
        {
            DateTime resultDate = baseDate;
            Shift? resultShift = null;
            
            if(intervalInfo.TestInterval.Type == IntervalType.EveryXShifts)
            {
                var result = AddShiftsToDateAndShift(baseDate, baseShift ?? GetActiveShifts().First(), intervalInfo.TestInterval.IntervalValue);
                resultDate = result.date;
                resultShift = result.shift;
            }
            else if (intervalInfo.TestInterval.Type == IntervalType.XTimesAShift)
            {
                var result = AddShiftsToDateAndShift(baseDate, baseShift ?? GetActiveShifts().First(), 1);
                resultDate = result.date;
                resultShift = result.shift;
            }
            else if(intervalInfo.TestInterval.Type == IntervalType.EveryXDays)
            {
                resultDate = baseDate.AddDays(intervalInfo.TestInterval.IntervalValue);
            }
            else if (intervalInfo.TestInterval.Type == IntervalType.XTimesADay)
            {
                resultDate = baseDate.AddDays(1);
            }
            else if (intervalInfo.TestInterval.Type == IntervalType.XTimesAWeek)
            {
                resultDate = baseDate.AddDays(7);
            }
            else if (intervalInfo.TestInterval.Type == IntervalType.XTimesAMonth)
            {
                resultDate = baseDate.AddMonths(1);
            }
            else if (intervalInfo.TestInterval.Type == IntervalType.XTimesAYear)
            {
                resultDate = baseDate.AddYears(1);
            }

            if (intervalInfo.ConsiderWorkingCalendar)
            {
                if (intervalInfo.TestInterval.IsIntervalStatic())
                {
                    while(IsDateAHoliday(resultDate))
                    {
                        resultDate = resultDate.AddDays(1);
                    }
                }
                else if (intervalInfo.TestInterval.IsIntervalDynamic())
                {
                    DateTime iterator = baseDate;
                    while (iterator <= resultDate)
                    {
                        if(IsDateAHoliday(iterator))
                        {
                            resultDate = resultDate.AddDays(1);
                        }
                        
                        iterator = iterator.AddDays(1);
                    }
                }
            }

            return (resultDate, resultShift);
        }
        
        private (DateTime date, Shift shift) AddShiftsToDateAndShift(DateTime baseDate, Shift baseShift, long shiftCount)
        {
            var activeShiftCount = 1l + (_shiftManagement.IsSecondShiftActive ? 1 : 0) + (_shiftManagement.IsThirdShiftActive ? 1 : 0);  // First shift is always active

            var activeShifts = GetActiveShifts();

            var resultDate = baseDate.AddDays((int)(shiftCount / activeShiftCount));
            var baseShiftIndex = activeShifts.IndexOf(baseShift);
            var resultShiftIndex = (baseShiftIndex == -1 ? 0 : baseShiftIndex) + shiftCount % activeShiftCount;
            var resultShift = baseShift;
            
            if (resultShiftIndex > activeShiftCount - 1)
            {
                resultDate = resultDate.AddDays(1);
                resultShift = activeShifts[(int)(resultShiftIndex - activeShiftCount)];
            }
            else
            {
                resultShift = activeShifts[(int)resultShiftIndex];
            }

            return (resultDate, resultShift);
        }
        
        private List<Shift> GetActiveShifts()
        {
            List<Shift> activeShifts = new List<Shift>() { Shift.FirstShiftOfDay };     // First shift is always active
            if (_shiftManagement.IsSecondShiftActive)
            {
                activeShifts.Add(Shift.SecondShiftOfDay);
            }
            if (_shiftManagement.IsThirdShiftActive)
            {
                activeShifts.Add(Shift.ThirdShiftOfDay);
            }
            return activeShifts;
        }
        
        private bool IsDateAHoliday(DateTime date)
        {
            var result = false;
            
            if(_workingCalendar.AreSaturdaysFree && date.DayOfWeek == DayOfWeek.Saturday || _workingCalendar.AreSundaysFree && date.DayOfWeek == DayOfWeek.Sunday)
            {
                // Yearly entries
                result = !_workingCalendarEntries.Any(x =>
                    x.Date.Date < date.Date &&
                    x.Repetition == WorkingCalendarEntryRepetition.Yearly &&
                    x.Date.Day == date.Day &&
                    x.Date.Month == date.Month &&
                    x.Type == WorkingCalendarEntryType.ExtraShift);
                // Single entries
                result = result && !_workingCalendarEntries.Any(x =>
                    x.Date.Date == date.Date &&
                    x.Repetition == WorkingCalendarEntryRepetition.Once &&
                    x.Type == WorkingCalendarEntryType.ExtraShift);
            }
            else
            {
                // Yearly entries
                result = _workingCalendarEntries.Any(x =>
                    x.Date.Date < date.Date &&
                    x.Repetition == WorkingCalendarEntryRepetition.Yearly &&
                    x.Date.Day == date.Day &&
                    x.Date.Month == date.Month &&
                    x.Type == WorkingCalendarEntryType.Holiday);
                // Single entries
                result = result || _workingCalendarEntries.Any(x =>
                    x.Date.Date == date.Date &&
                    x.Repetition == WorkingCalendarEntryRepetition.Once &&
                    x.Type == WorkingCalendarEntryType.Holiday);
            }

            return result;
        }
    }
}
