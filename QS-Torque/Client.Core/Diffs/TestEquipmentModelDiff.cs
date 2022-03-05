using Core.Entities;

namespace Client.Core.Diffs
{
    public class TestEquipmentModelDiff
    {
        public TestEquipmentModelDiff(TestEquipmentModel oldTestEquipmentModel, TestEquipmentModel newTestEquipmentModel, User user, HistoryComment comment = null)
        {
            OldTestEquipmentModel = oldTestEquipmentModel;
            NewTestEquipmentModel = newTestEquipmentModel;
            User = user;
            Comment = comment;
        }

        public TestEquipmentModel OldTestEquipmentModel { get; set; }
        public TestEquipmentModel NewTestEquipmentModel { get; set; }
        public User User { get; set; }
        public HistoryComment Comment { get; set; }
    }
}
