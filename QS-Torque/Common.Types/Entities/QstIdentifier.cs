namespace Core.Entities
{
    public class QstIdentifier
	{
		public QstIdentifier(long value)
		{
			_value = value;
		}

		public long ToLong()
		{
			return _value;
		}

		protected bool Equals(QstIdentifier other)
		{
            if (other == null)
            {
                return false;
            }
			return _value.Equals(other._value);
		}

		private readonly long _value;
	}
}
