using System;
using Common.Types.Enums;
using Core;
using Core.Entities;

namespace Client.Core.Entities
{
    public class ProcessControlTechId : QstIdentifier, IEquatable<ProcessControlTechId>
    {
        public ProcessControlTechId(long value)
            : base(value)
        {
        }

        public bool Equals(ProcessControlTechId other)
        {
            return Equals((QstIdentifier)other);
        }
    }

    public abstract class ProcessControlTech : IQstEquality<ProcessControlTech>, IUpdate<ProcessControlTech>, ICopy<ProcessControlTech>
    {
        public ProcessControlTechId Id { get; set; }
        public ProcessControlConditionId ProcessControlConditionId { get; set; }
        public ManufacturerIds ManufacturerId { get; set; }
        public TestMethod TestMethod { get; set; }
        public Extension Extension { get; set; }

        public abstract bool EqualsByContent(ProcessControlTech otherProcessControlTech);
        public abstract ProcessControlTech CopyDeep();
        public abstract bool EqualsById(ProcessControlTech other);
        public abstract void UpdateWith(ProcessControlTech other);
    }
}
