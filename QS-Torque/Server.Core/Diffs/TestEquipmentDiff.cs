using Server.Core.Entities;

namespace Server.Core.Diffs
{
	public class TestEquipmentDiff
	{
		public TestEquipmentDiff(User byUser, HistoryComment comment, TestEquipment oldTestEquipment, TestEquipment newTestEquipment)
		{
			_user = byUser;
			_comment = comment;
			_oldTestEquipment = oldTestEquipment;
			_newTestEquipment = newTestEquipment;
		}

		public User GetUser()
		{
			return _user;
		}

		public TestEquipment GetNewTestEquipment()
		{
			return _newTestEquipment;
		}

		public TestEquipment GetOldTestEquipment()
		{
			return _oldTestEquipment;
		}

        public HistoryComment GetComment()
        {
            return _comment;
        }

		private User _user;
		private HistoryComment _comment;
		private TestEquipment _oldTestEquipment;
		private TestEquipment _newTestEquipment;
	}
}
