using Core.Entities;

namespace Core.Diffs
{
    public class LocationDirectoryDiff
    {
        public User User { get; set; }
        public HistoryComment Comment { get; set; }
        public LocationDirectory OldLocationDirectory { get; set; }
        public LocationDirectory NewLocationDirectory { get; set; }

        public LocationDirectoryDiff(User user, LocationDirectory oldLocationDirectory, LocationDirectory newLocationDirectory)
        {
            User = user;
            OldLocationDirectory = oldLocationDirectory;
            NewLocationDirectory = newLocationDirectory;
        }
    }
}
