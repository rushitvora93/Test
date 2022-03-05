using Core.Entities;
using Core.Entities.ReferenceLink;
using NUnit.Framework;

namespace Core.Test.Entities.ReferenceLInk
{
    public class ToolReferenceLinkTest
    {
        [TestFixture]
        public class MockToolDisplayFormatte : IToolDisplayFormatter
        {
            public int FormatCalledCounter = 0;
            public string FormatReturnValue = null;
            public string Format(ToolReferenceLink link)
            {
                FormatCalledCounter++;
                return FormatReturnValue;
            }

            public string Format(Tool tool)
            {
                throw new System.NotImplementedException();
            }
        }

        [Test]
        [TestCase("Test")]
        [TestCase("Blub")]
        public void DisplayMemberCallsFormatterFormat(string returnValue)
        {
            MockToolDisplayFormatte formatter = new MockToolDisplayFormatte();
            var locationReferenceLink = new ToolReferenceLink(new QstIdentifier(1), null, null, formatter);
            formatter.FormatReturnValue = returnValue;
            var displayName = locationReferenceLink.DisplayName;
            Assert.AreEqual(1, formatter.FormatCalledCounter);
            Assert.AreEqual(returnValue, displayName);
        }
    }
}