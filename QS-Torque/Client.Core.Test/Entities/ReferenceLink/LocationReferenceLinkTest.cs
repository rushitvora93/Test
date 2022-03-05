using Core.Entities;
using Core.Entities.ReferenceLink;
using NUnit.Framework;

namespace Core.Test.Entities.ReferenceLInk
{
    [TestFixture]
    public class LocationReferenceLinkTest
    {
        class MockLocationDisplayFormatter : ILocationDisplayFormatter
        {
            public int FormatCalledCounter = 0;
            public string FormatReturnValue = null;
            public string Format(LocationReferenceLink link)
            {
                FormatCalledCounter++;
                return FormatReturnValue;
            }

            public string Format(Location location)
            {
                throw new System.NotImplementedException();
            }
        }

        [Test]
        [TestCase("Test")]
        [TestCase("Blub")]
        public void DisplayMemberCallsFormatterFormat(string returnValue)
        {
            MockLocationDisplayFormatter formatter = new MockLocationDisplayFormatter();
            var locationReferenceLink = new LocationReferenceLink(new QstIdentifier(1), null, null, formatter);
            formatter.FormatReturnValue = returnValue;
            var displayName = locationReferenceLink.DisplayName;
            Assert.AreEqual(1, formatter.FormatCalledCounter);
            Assert.AreEqual(returnValue, displayName);
        }
    }
}