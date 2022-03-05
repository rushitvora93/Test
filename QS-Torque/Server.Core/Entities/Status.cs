using System;
using Core;
using Core.Entities;

namespace Server.Core.Entities
{
    public class StatusDescription : TypeCheckedString<MaxLength<CtInt30>, Blacklist<NewLines>, NoCheck>
    {
        public StatusDescription(string description)
            : base(description)
        {
        }
    }

    public class StatusId : QstIdentifier, IEquatable<StatusId>
    {
        public StatusId(long value)
            : base(value)
        {
        }

        public bool Equals(StatusId other)
        {
            return Equals((QstIdentifier)other);
        }
    }


    public class Status : IQstEquality<Status>, IUpdate<Status>, ICopy<Status>
    {
        public StatusId Id { get; set; }
        public StatusDescription Value { get; set; }
        public bool Alive { get; set; }


        public bool EqualsById(Status other)
        {
            return this.Id.Equals(other?.Id);
        }

        public bool EqualsByContent(Status other)
        {
            if (other == null)
            {
                return false;
            }

            return this.Id.Equals(other.Id) &&
                   this.Value.Equals(other.Value);
        }

        public void UpdateWith(Status other)
        {
            if (other == null)
            {
                return;
            }

            this.Id = other.Id;
            this.Value = other.Value;
        }

        public Status CopyDeep()
        {
            return new Status()
            {
                Id = this.Id != null ? new StatusId(this.Id.ToLong()) : null,
                Value = this.Value != null ? new StatusDescription(this.Value.ToDefaultString()) : null
            };
        }
    }
}
