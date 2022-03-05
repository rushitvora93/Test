using Server.Core.Entities;

namespace Server.Core.Diffs
{
    public class StatusDiff
    {
        public StatusDiff(User byUser, HistoryComment comment, Status oldStatus, Status newStatus)
        {
            _user = byUser;
            _comment = comment;
            _oldStatus = oldStatus;
            _newStatus = newStatus;
        }

        public User GetUser()
        {
            return _user;
        }

        public Status GetNewStatus()
        {
            return _newStatus;
        }

        public Status GetOldStatus()
        {
            return _oldStatus;
        }

        public HistoryComment GetComment()
        {
            return _comment;
        }

        private User _user;
        private HistoryComment _comment;
        private Status _oldStatus;
        private Status _newStatus;
    }
}
