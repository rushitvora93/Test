using System;
using Core;
using Core.Entities;

namespace Server.Core.Entities
{
    public class ToolUsageId : QstIdentifier, IEquatable<ToolUsageId>
    {
        public ToolUsageId(long value)
            : base(value)
        {
        }

        public bool Equals(ToolUsageId other)
        {
            return Equals((QstIdentifier)other);
        }
    }

    public class ToolUsageDescription : TypeCheckedString<MaxLength<CtInt25>, Blacklist<NewLines>, NoCheck>
    {
        public ToolUsageDescription(string description)
            : base(description)
        {
        }
    }

    public class ToolUsage : IQstEquality<ToolUsage>, IUpdate<ToolUsage>, ICopy<ToolUsage>
    {
        public ToolUsageId Id { get; set; }
        public ToolUsageDescription Description { get; set; }
        public bool? Alive { get; set; }


        public bool EqualsById(ToolUsage other)
        {
            return this.Id.Equals(other?.Id);
        }

        public bool EqualsByContent(ToolUsage other)
        {
            if (other == null)
            {
                return false;
            }

            return this.Id.Equals(other.Id) &&
                   this.Description.Equals(other.Description) &&
                   this.Alive.Equals(other.Alive);
        }

        public void UpdateWith(ToolUsage other)
        {
            if (other == null)
            {
                return;
            }

            this.Id = other.Id;
            this.Description = other.Description;
            this.Alive = other.Alive;
        }

        public ToolUsage CopyDeep()
        {
            return new ToolUsage()
            {
                Id = this.Id != null ? new ToolUsageId(this.Id.ToLong()) : null,
                Description = this.Description != null ? new ToolUsageDescription(this.Description.ToDefaultString()) : null,
                Alive = this.Alive
            };
        }
    }
}