using Core.Entities;

namespace Core.Diffs
{
    public class TestEquipmentDiff
    {
        public TestEquipmentDiff(TestEquipment oldTestEquipment, TestEquipment newTestEquipment, User user, HistoryComment comment = null)
        {
            OldTestEquipment = oldTestEquipment;
            NewTestEquipment = newTestEquipment;
            Comment = comment;
            User = user;
        }

        public TestEquipment OldTestEquipment { get; set; }
        public TestEquipment NewTestEquipment { get; set; }
        public User User { get; set; }
        public HistoryComment Comment { get; set; }
    }
}
