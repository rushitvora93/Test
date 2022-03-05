using Server.Core.Entities;

namespace Server.Core.Diffs
{
	public class TestEquipmentModelDiff
	{
		public TestEquipmentModelDiff(User byUser, HistoryComment comment, TestEquipmentModel oldTestEquipmentModel, TestEquipmentModel newTestEquipmentModel)
		{
			_user = byUser;
			_comment = comment;
			_oldTestEquipmentModel = oldTestEquipmentModel;
			_newTestEquipmentModel = newTestEquipmentModel;
		}

		public User GetUser()
		{
			return _user;
		}

		public TestEquipmentModel GetNewTestEquipmentModel()
		{
			return _newTestEquipmentModel;
		}

		public TestEquipmentModel GetOldTestEquipmentModel()
		{
			return _oldTestEquipmentModel;
		}

        public HistoryComment GetComment()
        {
            return _comment;
        }

		private User _user;
		private HistoryComment _comment;
		private TestEquipmentModel _oldTestEquipmentModel;
		private TestEquipmentModel _newTestEquipmentModel;
	}
}
