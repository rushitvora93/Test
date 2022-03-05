using Core;
using Core.Entities;
using Core.Entities.ReferenceLink;

namespace TestHelper.Mock
{
    public class MockToolDisplayFormatter : IToolDisplayFormatter
    {
        public string DisplayString = "";
        public string Format(ToolReferenceLink link)
        {
            return DisplayString;
        }

        public string Format(Tool tool)
        {
            return DisplayString;
        }
    }
}
