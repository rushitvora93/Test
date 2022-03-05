using Server.Core.Entities;

namespace Server.Core.Diffs
{
	public class HelperTableEntityDiff
	{
		public HelperTableEntityDiff(User byUser, HistoryComment comment, HelperTableEntity oldHelperTableEntity, HelperTableEntity newHelperTableEntity)
		{
			_user = byUser;
			_comment = comment;
			_oldHelperTableEntity = oldHelperTableEntity;
			_newHelperTableEntity = newHelperTableEntity;
		}

		public User GetUser()
		{
			return _user;
		}

		public HelperTableEntity GetNewHelperTableEntity()
		{
			return _newHelperTableEntity;
		}

		public HelperTableEntity GetOldHelperTableEntity()
		{
			return _oldHelperTableEntity;
		}

        public HistoryComment GetComment()
        {
            return _comment;
        }

		private User _user;
		private HistoryComment _comment;
		private HelperTableEntity _oldHelperTableEntity;
		private HelperTableEntity _newHelperTableEntity;
	}
}
