using Core.UseCases.Communication.DataGate;
using NUnit.Framework;

namespace Core.Test.UseCases.Communication
{
    namespace DataGate
    {
        class DataGateSemanticModelTest
        {
            [Test]
            public void CreatingSemanticModelSetsElementAsRoot()
            {
                var root = new Content(null, null);
                var model = new SemanticModel(root);
                var visitor = new VisitorMock();
                model.Accept(visitor);
                Assert.AreSame(root, visitor.lastVisitContentParameterElement);
            }
        }

        class VisitorMock : IElementVisitor
        {
            public void Visit(Content element)
            {
                lastVisitContentParameterElement = element;
            }

            public void Visit(Container element)
            {
                lastVisitContainerParameterElement = element;
            }

            public void Visit(HiddenContent element)
            {
                lastVisitHiddenContentParameterElement = element;
            }

            public Content lastVisitContentParameterElement = null;
            public Container lastVisitContainerParameterElement = null;
            public HiddenContent lastVisitHiddenContentParameterElement = null;
        }
    }
}

