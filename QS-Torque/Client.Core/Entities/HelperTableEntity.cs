using System;

namespace Core.Entities
{
	public class HelperTableEntityId : QstIdentifier, IEquatable<HelperTableEntityId>
	{
		public HelperTableEntityId(long value)
			: base(value)
		{
		}

        public bool Equals(HelperTableEntityId other)
        {
            return Equals((QstIdentifier)other);
        }
    }

	public  abstract class HelperTableEntity
    {
        public HelperTableEntityId ListId { get; set; }
    }

    public interface IStandardHelperTable
    {
        HelperTableEntityId ListId { get; set; }
        HelperTableDescription Value { get; set; }
    }
}
