using Core.Entities;

namespace Server.Core.Entities.ReferenceLink
{
    public class LocationReferenceLink : ReferenceLink
    {
        public string Number { get; private set; }
        public string Description { get; private set; }

        public LocationReferenceLink(QstIdentifier id, string number, string description)
        {
            Id = id;
            Number = number;
            Description = description;
        }
    }
}
