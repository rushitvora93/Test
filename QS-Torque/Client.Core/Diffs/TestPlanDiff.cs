using Core.Entities;

namespace Core.Diffs
{
    public class TestPlanDiff
    {
        public User User { get; set; }
        public HistoryComment Comment { get; set; }
        public TestPlan OldTestPlan { get; set; }
        public TestPlan NewTestPlan { get; set; }

        public TestPlanDiff(User user, HistoryComment comment, TestPlan oldTestPlan, TestPlan newTestPlan)
        {
            User = user;
            Comment = comment;
            OldTestPlan = oldTestPlan;
            NewTestPlan = newTestPlan;
        }

        public TestPlanDiff(User user, TestPlan oldTestPlan, TestPlan newTestPlan)
        {
            User = user;
            OldTestPlan = oldTestPlan;
            NewTestPlan = newTestPlan;
        }
    }

    // Represents the DTO of the server, will be replaced by the server DTO in the future
    public class TestPlanDiff_Dto
    {
        public long UserId { get; set; }
        public string Comment { get; set; }
        public TestPlan_Dto OldTestPlan { get; set; }
        public TestPlan_Dto NewTestPlan { get; set; }
    }
}
