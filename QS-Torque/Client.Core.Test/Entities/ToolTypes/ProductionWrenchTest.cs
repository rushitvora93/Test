using Core.Entities;
using Core.Entities.ToolTypes;
using NUnit.Framework;
using TestHelper.Mock;

namespace Core.Test.Entities.ToolTypes
{
    class ProductionWrenchTest
    {
        [Test]
        public void ProductionWrenchHasCorrectName()
        {
            var toolType = new ProductionWrench();
            Assert.AreEqual("ProductionWrench", toolType.Name);
        }

        [TestCase(nameof(ToolModel.Class), true)]
        [TestCase(nameof(ToolModel.BatteryVoltage), false)]
        [TestCase(nameof(ToolModel.MaxRotationSpeed), false)]
        [TestCase(nameof(ToolModel.AirPressure), false)]
        [TestCase(nameof(ToolModel.AirConsumption), false)]
        public void HasPropertyGivesCorrectResult(string property, bool expectedResult)
        {
            var toolType = new ProductionWrench();
            Assert.AreEqual(expectedResult, toolType.DoesToolTypeHasProperty(property));
        }

        [Test]
        public void AcceptingVisitorCallsCorrectVisitFunction()
        {
            var toolType = new ProductionWrench();
            var visitor = new AbstractToolTypeVisitorMock();
            toolType.Accept(visitor);
            Assert.IsTrue(visitor.VisitProductionWrenchWasCalled);
        }
    }
}
