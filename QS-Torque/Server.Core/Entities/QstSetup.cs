using System;
using Core.Entities;

namespace Server.Core.Entities
{
    public class QstSetupId : QstIdentifier, IEquatable<QstSetupId>
    {
        public QstSetupId(long value)
            : base(value)
        {
        }

        public bool Equals(QstSetupId other)
        {
            return Equals((QstIdentifier)other);
        }
    }

    public class QstSetupName : TypeCheckedString<MaxLength<CtInt4000>, Blacklist<NewLines>, NoCheck>
    {
        public QstSetupName(string name)
            : base(name)
        {
        }
    }

    public class QstSetupValue : TypeCheckedString<MaxLength<CtInt4000>, Blacklist<NewLines>, NoCheck>
    {
        public QstSetupValue(string value)
            : base(value)
        {
        }
    }

    public class QstSetup
    {
        public QstSetupId Id { get; set; }
        public QstSetupName Name { get; set; }
        public QstSetupValue Value { get; set; }
        public long UserId { get; set; }
    }
}
