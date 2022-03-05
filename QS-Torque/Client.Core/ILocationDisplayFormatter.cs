using Core.Entities;
using Core.Entities.ReferenceLink;

namespace Core
{
    public interface ILocationDisplayFormatter
    {
        string Format(LocationReferenceLink link);
        string Format(Location location);
    }
}
