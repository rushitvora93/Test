using Core.UseCases.Communication.DataGate;
using NUnit.Framework;

namespace Core.Test.UseCases.Communication.DataGate
{
    class DataGateHiddenContentElementTest
    {
        [Test]
        public void AcceptingVisitorCallsVisitOnVisitor()
        {
            var visitor = new VisitorMock();
            var element = new HiddenContent(null, null);
            element.Accept(visitor);
            Assert.AreSame(element, visitor.lastVisitHiddenContentParameterElement);
        }

        [TestCase("Something")]
        [TestCase("DadaGade")]
        public void GettingNameFromNamedElementReturnsElementName(string name)
        {
            var element = new HiddenContent(new ElementName(name), null);
            Assert.AreEqual(new ElementName(name), element.GetName());
        }

        [TestCase("Something")]
        [TestCase("DadaGade")]
        public void GettingValueFromElementReturnsElementValue(string value)
        {
            var element = new HiddenContent(null, value);
            Assert.AreEqual(value, element.GetValue());
        }
    }
}