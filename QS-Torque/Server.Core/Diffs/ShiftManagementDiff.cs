using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Core.Entities;

namespace Server.Core.Diffs
{
    public class ShiftManagementDiff
    {
        public ShiftManagement Old { get; set; }
        public ShiftManagement New { get; set; }
        public User User { get; set; }
        public HistoryComment Comment { get; set; }

        public ShiftManagementDiff(ShiftManagement oldEntity, ShiftManagement newEntity, User user, HistoryComment comment)
        {
            Old = oldEntity;
            New = newEntity;
            User = user;
            Comment = comment;
        }

        public ShiftManagementDiff() { }
    }
}
