using Core.UseCases.Communication.DataGate;
using NUnit.Framework;

namespace Core.Test.UseCases.Communication.DataGate
{
    class DataGateContentElementTest
    {
        [Test]
        public void AcceptingVisitorCallsVisitOnVisitor()
        {
            var visitor = new VisitorMock();
            var element = new Content(null, null);
            element.Accept(visitor);
            Assert.AreSame(element, visitor.lastVisitContentParameterElement);
        }

        [TestCase("Something")]
        [TestCase("DadaGade")]
        public void GettingNameFromNamedElementReturnsElementName(string name)
        {
            var element = new Content(new ElementName(name), null);
            Assert.AreEqual(new ElementName(name), element.GetName());
        }

        [TestCase("Something")]
        [TestCase("DadaGade")]
        public void GettingValueFromElementReturnsElementValue(string value)
        {
            var element = new Content(null, value);
            Assert.AreEqual(value, element.GetValue());
        }
    }
}