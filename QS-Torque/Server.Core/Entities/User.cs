using Core.Entities;

namespace Server.Core.Entities
{
	public class UserId: QstIdentifier
	{
		public UserId(long value)
			: base(value)
		{
		}
	}

    public class User
    {
        public UserId UserId { get; set; }
        public string UserName { get; set; }
        public Group Group { get; set; }
        public string QstEncryptedPassword { get; set; }
	}
}
