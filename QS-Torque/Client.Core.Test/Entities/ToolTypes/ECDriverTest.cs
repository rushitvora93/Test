using Core.Entities;
using Core.Entities.ToolTypes;
using NUnit.Framework;
using TestHelper.Mock;

namespace Core.Test.Entities.ToolTypes
{
    class ECDriverTest
    {
        [Test]
        public void ECDriverHasCorrectName()
        {
            var toolType = new ECDriver();
            Assert.AreEqual("ECDriver", toolType.Name);
        }

        [TestCase(nameof(ToolModel.Class), false)]
        [TestCase(nameof(ToolModel.BatteryVoltage), true)]
        [TestCase(nameof(ToolModel.MaxRotationSpeed), true)]
        [TestCase(nameof(ToolModel.AirPressure), false)]
        [TestCase(nameof(ToolModel.AirConsumption), false)]
        public void HasPropertyGivesCorrectResult(string property, bool expectedResult)
        {
            var toolType = new ECDriver();
            Assert.AreEqual(expectedResult, toolType.DoesToolTypeHasProperty(property));
        }

        [Test]
        public void AcceptingVisitorCallsCorrectVisitFunction()
        {
            var toolType = new ECDriver();
            var visitor = new AbstractToolTypeVisitorMock();
            toolType.Accept(visitor);
            Assert.IsTrue(visitor.VisitECDriverWasCalled);
        }
    }
}
