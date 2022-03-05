using Server.Core.Entities;

namespace Server.Core.Diffs
{
	public class ToolDiff
	{
		public ToolDiff(User byUser, HistoryComment comment, Tool oldTool, Tool newTool)
		{
			_user = byUser;
			_comment = comment;
			_oldTool = oldTool;
			_newTool = newTool;
		}

		public User GetUser()
		{
			return _user;
		}

		public Tool GetNewTool()
		{
			return _newTool;
		}

		public Tool GetOldTool()
		{
			return _oldTool;
		}

        public HistoryComment GetComment()
        {
            return _comment;
        }

		private User _user;
		private HistoryComment _comment;
		private Tool _oldTool;
		private Tool _newTool;
	}
}
