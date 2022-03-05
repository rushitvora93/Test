using Core.Entities;

namespace Server.Core.Entities
{
	public class GroupId : QstIdentifier
	{
		public GroupId(long value)
			: base(value)
		{
		}
	}

	public class Group
    {
        public GroupId Id { get; set; }

        public string GroupName { get; set; }

        /*
         * Has to be extended with further Attributes, but I (Johannes) don't no which one we need 
         */
    }
}
