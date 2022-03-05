using System;
using Core.Entities;

namespace Core.UseCases.Communication.DataGate
{
    public class ElementName : TypeCheckedString<NoWhiteSpace, NoCheck, NoCheck>, IEquatable<ElementName>
    {
        public ElementName(string value)
            : base(value)
        {
        }


        public bool Equals(ElementName other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            return ToDefaultString() == other.ToDefaultString();
        }
    }
}