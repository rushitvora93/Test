using Server.Core.Entities;
using Server.Core.Enums;
using System;

namespace Server.Core.Diffs
{
	public class LocationDiff
	{
		public LocationDiff(User byUser, HistoryComment comment, Location oldLocation, Location newLocation)
		{
			User = byUser;
			Comment = comment;
			OldLocation = oldLocation;
			NewLocation = newLocation;
		}

		public User GetUser()
		{
			return User;
		}

		public Location GetNewLocation()
		{
			return NewLocation;
		}

		public Location GetOldLocation()
		{
			return OldLocation;
		}

        public HistoryComment GetComment()
        {
            return Comment;
        }

		public User User { get; set; }
		public HistoryComment Comment { get; set; }
		public Location OldLocation { get; set; }
		public Location NewLocation { get; set; }
		public DiffType Type { get; set; }
		public DateTime TimeStamp { get; set; }
	}
}
