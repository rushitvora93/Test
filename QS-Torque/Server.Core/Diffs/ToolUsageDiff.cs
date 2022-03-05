using Server.Core.Entities;

namespace Server.Core.Diffs
{
    public class ToolUsageDiff
	{
		public ToolUsageDiff(User byUser, HistoryComment comment, ToolUsage oldToolUsage, ToolUsage newToolUsage)
		{
			_user = byUser;
			_comment = comment;
			_oldToolUsage = oldToolUsage;
			_newToolUsage = newToolUsage;
		}

		public User GetUser()
		{
			return _user;
		}

		public ToolUsage GetNewToolUsage()
		{
			return _newToolUsage;
		}

		public ToolUsage GetOldToolUsage()
		{
			return _oldToolUsage;
		}

        public HistoryComment GetComment()
        {
            return _comment;
        }

		private User _user;
		private HistoryComment _comment;
		private ToolUsage _oldToolUsage;
		private ToolUsage _newToolUsage;
	}
}
