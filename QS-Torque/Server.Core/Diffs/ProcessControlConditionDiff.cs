using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Core.Entities;

namespace Server.Core.Diffs
{
    public class ProcessControlConditionDiff
    {
        public ProcessControlConditionDiff(User byUser, HistoryComment comment, ProcessControlCondition oldCondition, ProcessControlCondition newCondition)
        {
            _user = byUser;
            _comment = comment;
            _oldCondition = oldCondition;
            _newCondition = newCondition;
        }

        public User GetUser()
        {
            return _user;
        }

        public ProcessControlCondition GetNewProcessControlCondition()
        {
            return _newCondition;
        }

        public ProcessControlCondition GetOldProcessControlCondition()
        {
            return _oldCondition;
        }

        public HistoryComment GetComment()
        {
            return _comment;
        }

        private User _user;
        private HistoryComment _comment;
        private ProcessControlCondition _oldCondition;
        private ProcessControlCondition _newCondition;
	}
}
