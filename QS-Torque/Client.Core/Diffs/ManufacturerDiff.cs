using Core.Entities;

namespace Core.UseCases
{
    public class ManufacturerDiff
	{
		public ManufacturerDiff(User byUser, HistoryComment comment, Manufacturer oldManufacturer, Manufacturer newManufacturer)
		{
			_user = byUser;
			_comment = comment;
			_oldManufacturer = oldManufacturer;
			_newManufacturer = newManufacturer;
		}

		public User GetUser()
		{
			return _user;
		}

		public Manufacturer GetNewManufacturer()
		{
			return _newManufacturer;
		}

		public Manufacturer GetOldManufacturer()
		{
			return _oldManufacturer;
		}

        public HistoryComment GetComment()
        {
            return _comment;
        }

		private User _user;
		private HistoryComment _comment;
		private Manufacturer _oldManufacturer;
		private Manufacturer _newManufacturer;
	}
}
