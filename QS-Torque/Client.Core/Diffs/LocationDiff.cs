using Client.Core.Enums;
using Core.Entities;
using System;

namespace Core.Diffs
{
    public class LocationDiff
    {
        public User User { get; set; }
        public HistoryComment Comment { get; set; }
        public Location OldLocation { get; set; }
        public Location NewLocation { get; set; }
        public DiffType Type { get; set; }
        public DateTime TimeStamp { get; set; }

        public LocationDiff()
        {

        }

        public LocationDiff(User user, Location oldLocation, Location newLocation, HistoryComment historyComment = null)
        {
            User = user;
            OldLocation = oldLocation;
            NewLocation = newLocation;
            Comment = historyComment;
        }
    }
}
