using Server.Core.Entities;

namespace Server.Core.Diffs
{
    public class ToolModelDiff
    {
        public ToolModelDiff(User byUser, HistoryComment comment, ToolModel oldToolModel, ToolModel newToolModel)
        {
            _user = byUser;
            _comment = comment;
            _old = oldToolModel;
            _new = newToolModel;
        }

        public User GetUser()
        {
            return _user;
        }

        public HistoryComment GetComment()
        {
            return _comment;
        }

        public ToolModel GetOld()
        {
            return _old;
        }

        public ToolModel GetNew()
        {
            return _new;
        }

        private readonly User _user;
        private readonly HistoryComment _comment;
        private readonly ToolModel _old;
        private readonly ToolModel _new;
    }
}
