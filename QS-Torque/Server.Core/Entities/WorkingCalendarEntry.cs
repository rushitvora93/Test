using System;
using Core.Entities;

namespace Server.Core.Entities
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
}
