using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Core.Diffs
{
    public class WorkingCalendarDiff
    {
        public readonly WorkingCalendar Old;
        public readonly WorkingCalendar New;
        public User User;
        public HistoryComment Comment;

        public WorkingCalendarDiff(WorkingCalendar oldEntity, WorkingCalendar newEntity, User user, HistoryComment comment = null)
        {
            Old = oldEntity;
            New = newEntity;
            User = user;
            Comment = comment;
        }

        public WorkingCalendarDiff(WorkingCalendar oldEntity, WorkingCalendar newEntity)
        {
            Old = oldEntity;
            New = newEntity;
        }
    }
}
