using Core.Entities;
using Core.Entities.ReferenceLink;
using NUnit.Framework;

namespace Core.Test.Entities.ReferenceLInk
{
    [TestFixture]
    public class LocationToolAssignmentReferenceLinkTest
    {
        class MockLocationToolAssignmentDisplayFormatter : ILocationToolAssignmentDisplayFormatter
        {
            public int FormatCalledCounter = 0;
            public string FormatReturnValue = null;
            public string Format(LocationToolAssignmentReferenceLink link)
            {
                FormatCalledCounter++;
                return FormatReturnValue;
            }

            public string Format(LocationToolAssignment locationToolAssignment)
            {
                throw new System.NotImplementedException();
            }
        }

        [Test]
        [TestCase("Test")]
        [TestCase("Blub")]
        public void DisplayMemberCallsFormatterFormat(string returnValue)
        {
            MockLocationToolAssignmentDisplayFormatter formatter = new MockLocationToolAssignmentDisplayFormatter();
            var locationReferenceLink = new LocationToolAssignmentReferenceLink(new QstIdentifier(1), null, null, null, null, formatter, null, null);
            formatter.FormatReturnValue = returnValue;
            var displayName = locationReferenceLink.DisplayName;
            Assert.AreEqual(1, formatter.FormatCalledCounter);
            Assert.AreEqual(returnValue, displayName);
        }
    }
}