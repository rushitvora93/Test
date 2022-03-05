using Core.Entities;
using Core.Entities.ReferenceLink;

namespace Core
{
    public interface IToolDisplayFormatter
    {
        string Format(ToolReferenceLink link);
        string Format(Tool tool);
    }
}
