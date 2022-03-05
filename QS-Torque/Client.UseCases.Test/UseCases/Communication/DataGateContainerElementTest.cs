using System.Collections.Generic;
using Core.UseCases.Communication.DataGate;
using NUnit.Framework;

namespace Core.Test.UseCases.Communication.DataGate
{
    class DataGateContainerElementTest
    {
        [Test]
        public void AcceptingVisitorCallsVisitOnVisitor()
        {
            var visitor = new VisitorMock();
            var element = new Container(null);
            element.Accept(visitor);
            Assert.AreSame(element, visitor.lastVisitContainerParameterElement);
        }

        [TestCase("Something")]
        [TestCase("DadaGade")]
        public void GettingNameFromNamedElementReturnsElementName(string name)
        {
            var element = new Container(new ElementName(name));
            Assert.AreEqual(new ElementName(name), element.GetName());
        }

        // todo: more cases??? do i need them?
        [Test]
        public void AddingElementsAppearInEnumerable()
        {
            var element = new Container(null);
            var content = new Content(null, null);
            element.Add(content);
            CollectionAssert.AreEqual(new List<IElement>{content}, element);
        }

        [Test]
        public void RemovedElementDoesNotAppearInEnumerable()
        {
            var content = new Content(null, null);
            var element = new Container(null){ content };
            element.Remove(content);
            CollectionAssert.AreEqual(new List<IElement>{}, element);
        }

        [Test]
        public void RemovedElementDoesNotAppearInEnumerableButRestStaysIntact()
        {
            var content = new Content(null, null);
            var notRemovedContent = new Content(null, null);
            var element = new Container(null) { content, notRemovedContent };
            element.Remove(content);
            CollectionAssert.AreEqual(new List<IElement> { notRemovedContent }, element);
        }

        [Test]
        public void AnotherRemovedElementDoesNotAppearInEnumerableButRestStaysIntact()
        {
            var content = new Content(null, null);
            var notRemovedContent = new Content(null, null);
            var element = new Container(null) { notRemovedContent, content };
            element.Remove(content);
            CollectionAssert.AreEqual(new List<IElement> { notRemovedContent }, element);
        }
    }
}