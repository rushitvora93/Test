using System;

namespace Core.Entities
{
    public class WorkingCalendarEntryDescription : TypeCheckedString<MaxLength<CtInt30>, Blacklist<NewLines>, NoCheck>
    {
        public WorkingCalendarEntryDescription(string name)
            : base(name)
        {
        }
    }

    public enum WorkingCalendarEntryType
    {
        ExtraShift = 0,
        Holiday = 1
    }

    public enum WorkingCalendarEntryRepetition
    {
        Once = 0,
        Yearly = 1
    }

    public class WorkingCalendarEntry
    {
        public DateTime Date { get; set; }
        public WorkingCalendarEntryDescription Description { get; set; }
        public WorkingCalendarEntryType Type { get; set; }
        public WorkingCalendarEntryRepetition Repetition { get; set; }
    }

    public class WorkingCalendarEntryValidator
    {
        public static bool IsWorkingCalendarEntryValidAtDate(WorkingCalendarEntry entry, bool areSaturdaysFree, bool areSundaysFree)
        {
            var date = entry.Date.Date;

            if (areSaturdaysFree && date.DayOfWeek == DayOfWeek.Saturday && entry.Type == WorkingCalendarEntryType.Holiday)
            {
                return false;
            }
            if (areSundaysFree && date.DayOfWeek == DayOfWeek.Sunday && entry.Type == WorkingCalendarEntryType.Holiday)
            {
                return false;
            }
            if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday && entry.Type == WorkingCalendarEntryType.ExtraShift)
            {
                return false;
            }
            if (!areSaturdaysFree && date.DayOfWeek == DayOfWeek.Saturday && entry.Type == WorkingCalendarEntryType.ExtraShift)
            {
                return false;
            }
            if (!areSundaysFree && date.DayOfWeek == DayOfWeek.Sunday && entry.Type == WorkingCalendarEntryType.ExtraShift)
            {
                return false;
            }

            return true;
        }
    }
}
