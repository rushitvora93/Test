using Server.Core.Entities;

namespace Server.Core.Diffs
{
	public class LocationDirectoryDiff
	{
		public LocationDirectoryDiff(User byUser, HistoryComment comment, LocationDirectory oldLocationDirectory, LocationDirectory newLocationDirectory)
		{
			_user = byUser;
			_comment = comment;
			_oldLocationDirectory = oldLocationDirectory;
			_newLocationDirectory = newLocationDirectory;
		}

		public User GetUser()
		{
			return _user;
		}

		public LocationDirectory GetNewLocationDirectory()
		{
			return _newLocationDirectory;
		}

		public LocationDirectory GetOldLocationDirectory()
		{
			return _oldLocationDirectory;
		}

        public HistoryComment GetComment()
        {
            return _comment;
        }

		private User _user;
		private HistoryComment _comment;
		private LocationDirectory _oldLocationDirectory;
		private LocationDirectory _newLocationDirectory;
	}
}
