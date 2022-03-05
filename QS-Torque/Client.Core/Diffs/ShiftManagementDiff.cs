using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Client.Core.Diffs
{
    public class ShiftManagementDiff
    {
        public readonly ShiftManagement Old;
        public readonly ShiftManagement New;
        public User User;
        public HistoryComment Comment;

        public ShiftManagementDiff(ShiftManagement oldEntity, ShiftManagement newEntity, User user, HistoryComment comment)
        {
            Old = oldEntity;
            New = newEntity;
            User = user;
            Comment = comment;
        }

        public ShiftManagementDiff(ShiftManagement oldEntity, ShiftManagement newEntity)
        {
            Old = oldEntity;
            New = newEntity;
        }
    }
}
