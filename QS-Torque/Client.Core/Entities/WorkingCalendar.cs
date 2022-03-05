using System;

namespace Core.Entities
{
    public class WorkingCalendarId : QstIdentifier, IEquatable<WorkingCalendarId>
    {
        public WorkingCalendarId(long value)
            : base(value)
        {
        }

        public bool Equals(WorkingCalendarId other)
        {
            return Equals((QstIdentifier)other);
        }
    }

    public class WorkingCalendarName : TypeCheckedString<MaxLength<CtInt80>, Blacklist<NewLines>, NoCheck>
    {
        public WorkingCalendarName(string name)
            : base(name)
        {
        }
    }

    public class WorkingCalendar
    {
        public WorkingCalendarId Id { get; set; }
        public WorkingCalendarName Name { get; set; }
        public bool AreSaturdaysFree { get; set; }
        public bool AreSundaysFree { get; set; }
    }
}
