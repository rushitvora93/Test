using Core.Entities;
using Core.Entities.ToolTypes;
using NUnit.Framework;
using TestHelper.Mock;

namespace Core.Test.Entities.ToolTypes
{
    class PulseDriverTest
    {
        [Test]
        public void PulseDriverHasCorrectName()
        {
            var toolType = new PulseDriver();
            Assert.AreEqual("PulseDriver", toolType.Name);
        }

        [TestCase(nameof(ToolModel.Class), false)]
        [TestCase(nameof(ToolModel.BatteryVoltage), false)]
        [TestCase(nameof(ToolModel.MaxRotationSpeed), true)]
        [TestCase(nameof(ToolModel.AirPressure), true)]
        [TestCase(nameof(ToolModel.AirConsumption), true)]
        public void HasPropertyGivesCorrectResult(string property, bool expectedResult)
        {
            var toolType = new PulseDriver();
            Assert.AreEqual(expectedResult, toolType.DoesToolTypeHasProperty(property));
        }

        [Test]
        public void AcceptingVisitorCallsCorrectVisitFunction()
        {
            var toolType = new PulseDriver();
            var visitor = new AbstractToolTypeVisitorMock();
            toolType.Accept(visitor);
            Assert.IsTrue(visitor.VisitPulseDriverWasCalled);
        }
    }
}
