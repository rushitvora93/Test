using System;

namespace Core.Entities
{
    public class GlobalHistoryId : QstIdentifier, IEquatable<GlobalHistoryId>
    {
        public GlobalHistoryId(long value)
            : base(value)
        {
        }

        public bool Equals(GlobalHistoryId other)
        {
            return Equals((QstIdentifier)other);
        }
    }
}
