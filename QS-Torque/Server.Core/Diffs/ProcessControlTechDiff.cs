using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Core.Entities;

namespace Server.Core.Diffs
{
    public class ProcessControlTechDiff
    {
        public ProcessControlTechDiff(User byUser, HistoryComment comment, ProcessControlTech oldTech, ProcessControlTech newTech)
        {
            _user = byUser;
            _comment = comment;
            _oldTech = oldTech;
            _newTech = newTech;
        }

        public User GetUser()
        {
            return _user;
        }

        public ProcessControlTech GetNewProcessControlTech()
        {
            return _newTech;
        }

        public ProcessControlTech GetOldProcessControlTech()
        {
            return _oldTech;
        }

        public HistoryComment GetComment()
        {
            return _comment;
        }

        private User _user;
        private HistoryComment _comment;
        private ProcessControlTech _oldTech;
        private ProcessControlTech _newTech;
    }
}
