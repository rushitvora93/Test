using Core;
using Core.Entities;
using Core.Entities.ReferenceLink;

namespace TestHelper.Mock
{
    public class MockLocationDisplayFormatter : ILocationDisplayFormatter
    {
        public string DisplayString { get; set; }
        public Location FormatLocation { get; set; }

        public string Format(LocationReferenceLink link)
        {
            return DisplayString;
        }

        public string Format(Location location)
        {
            FormatLocation = location;
            return DisplayString;
        }
    }
}
