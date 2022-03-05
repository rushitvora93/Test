using Core.Entities;

namespace Core.Diffs
{
    public class LocationToolAssignmentDiff
    {
        public User User { get; set; }
        public HistoryComment Comment { get; set; }
        public LocationToolAssignment OldLocationToolAssignment { get; set; }
        public LocationToolAssignment NewLocationToolAssignment { get; set; }   
    }
}