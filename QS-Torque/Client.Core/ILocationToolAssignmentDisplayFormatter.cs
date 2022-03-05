using Core.Entities;
using Core.Entities.ReferenceLink;

namespace Core
{
    public interface ILocationToolAssignmentDisplayFormatter
    {
        string Format(LocationToolAssignmentReferenceLink link);
        string Format(LocationToolAssignment locationToolAssignment);
    }
}
