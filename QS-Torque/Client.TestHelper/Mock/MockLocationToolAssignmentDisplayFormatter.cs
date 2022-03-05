using Core;
using Core.Entities;
using Core.Entities.ReferenceLink;

namespace TestHelper.Mock
{
    public class MockLocationToolAssignmentDisplayFormatter : ILocationToolAssignmentDisplayFormatter
    {
        public string DisplayString { get; set; }
        public string Format(LocationToolAssignmentReferenceLink link)
        {
            return DisplayString;
        }

        public string Format(LocationToolAssignment locationToolAssignment)
        {
            return DisplayString;
        }
    }
}