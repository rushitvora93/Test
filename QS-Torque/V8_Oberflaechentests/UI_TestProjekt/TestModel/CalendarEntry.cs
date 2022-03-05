
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI_TestProjekt.TestModel
{
    public class CalendarEntry
    {
        private DateTime calendarDay = DateTime.Today;
        private string description = "";
        private bool isAnnually = false;
        private bool isExtraShift = false;

        public DateTime CalendarDay { get => calendarDay; set => calendarDay = value; }
        public string Description { get => description; set => description = value; }
        public bool IsAnnually { get => isAnnually; set => isAnnually = value; }
        public bool IsExtraShift { get => isExtraShift; set => isExtraShift = value; }

        public CalendarEntry(DateTime calendarDay, string description, bool isAnnually, bool isExtraShift)
        {
            CalendarDay = calendarDay;
            Description = description;
            IsAnnually = isAnnually;
            IsExtraShift = isExtraShift;
        }

        public string getRepetitionString()
        {
            if (IsAnnually)
            {
                return "Annually";
            }
            else
            {
                return "Once";
            }
        }

        public string getDayTypeString()
        {
            if (isExtraShift)
            {
                return "Extra shift";
            }
            else
            {
                return "Holiday";
            }
        }

        public static class CalendarEntryListHeaderStrings
        {
            public const string CalendarDate = "Date";
            public const string Description = "Description";
            public const string Type = "Type";
        }

        public bool CalendarEntryIsWeekday()
        {
            return CalendarDay.DayOfWeek != DayOfWeek.Saturday && CalendarDay.DayOfWeek != DayOfWeek.Sunday;
        }

        public bool CalendarEntryIsValidSaturday(bool saturdayIsFree)
        {
            return CalendarDay.DayOfWeek == DayOfWeek.Saturday &&
            ((saturdayIsFree && IsExtraShift) || (!saturdayIsFree && !IsExtraShift));
        }

        public bool CalendarEntryIsValidSunday(bool sundayIsFree)
        {
            return CalendarDay.DayOfWeek == DayOfWeek.Sunday && ((sundayIsFree && IsExtraShift)
            || (!sundayIsFree && !IsExtraShift));
        }

        public string GetCalenderDateRowString()
        {
            return CalendarDay.ToString("d", CultureInfo.CreateSpecificCulture("en-us"));
        }
    }
}
