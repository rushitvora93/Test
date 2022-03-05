using Core.Entities;

namespace Server.Core.Entities.ReferenceLink
{
    public abstract class ReferenceLink
    {
        public QstIdentifier Id { get; set; }
        public virtual string DisplayName { get; set; }
    }
}
