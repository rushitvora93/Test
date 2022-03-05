using System;
using Core.Entities;
using State;

namespace Server.Core.Entities
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

    public class HelperTableEntityValue : TypeCheckedString<MaxLength<CtInt50>, Blacklist<NewLines>, NoCheck>
    {
        public HelperTableEntityValue(string value)
            : base(value)
        {
        }
    }

    public class HelperTableEntity
    {
        public HelperTableEntityId ListId { get; set; }
        public HelperTableEntityValue Value { get; set; }
        public NodeId NodeId { get; set; }
        public bool Alive { get; set; }
    }
}
