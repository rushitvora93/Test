using Core.Entities;

namespace Core.Diffs
{
    public class TestPlanLocationToolAssignmentDiff
    {
        public User User { get; set; }
        public HistoryComment Comment { get; set; }
        public TestPlanLocationToolAssignment OldAssignment { get; set; }
        public TestPlanLocationToolAssignment NewAssignment { get; set; }

        public TestPlanLocationToolAssignmentDiff(User user, HistoryComment comment, TestPlanLocationToolAssignment oldAssignment, TestPlanLocationToolAssignment newAssignment)
        {
            User = user;
            Comment = comment;
            OldAssignment = oldAssignment;
            NewAssignment = newAssignment;
        }

        public TestPlanLocationToolAssignmentDiff(TestPlanLocationToolAssignment oldAssignment, TestPlanLocationToolAssignment newAssignment)
        {
            OldAssignment = oldAssignment;
            NewAssignment = newAssignment;
        }
    }
}
