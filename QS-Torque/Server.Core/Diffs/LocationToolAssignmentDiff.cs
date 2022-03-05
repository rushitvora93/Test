using Server.Core.Entities;

namespace Server.Core.Diffs
{
	public class LocationToolAssignmentDiff
	{
		public LocationToolAssignmentDiff(User byUser, LocationToolAssignment oldLocationToolAssignment, LocationToolAssignment newLocationToolAssignment, HistoryComment comment = null)
		{
			_user = byUser;
			_oldLocationToolAssignment = oldLocationToolAssignment;
			_newLocationToolAssignment = newLocationToolAssignment;
            _comment = comment;
        }

		public User GetUser()
		{
			return _user;
		}

		public LocationToolAssignment GetNewLocationToolAssignment()
		{
			return _newLocationToolAssignment;
		}

		public LocationToolAssignment GetOldLocationToolAssignment()
		{
			return _oldLocationToolAssignment;
		}

        public HistoryComment GetHistoryComment()
        {
            return _comment;
        }

		private User _user;
		private LocationToolAssignment _oldLocationToolAssignment;
		private LocationToolAssignment _newLocationToolAssignment;
        private readonly HistoryComment _comment;
    }
}
