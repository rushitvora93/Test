using Core.Entities;
using Core.Entities.ToolTypes;
using NUnit.Framework;
using TestHelper.Mock;

namespace Core.Test.Entities.ToolTypes
{
    class PulseDriverShutOffTest
    {
        [Test]
        public void PulseDriverShutOffHasCorrectName()
        {
            var toolType = new PulseDriverShutOff();
            Assert.AreEqual("PulseDriverShutOff", toolType.Name);
        }

        [TestCase(nameof(ToolModel.Class), false)]
        [TestCase(nameof(ToolModel.BatteryVoltage), false)]
        [TestCase(nameof(ToolModel.MaxRotationSpeed), true)]
        [TestCase(nameof(ToolModel.AirPressure), true)]
        [TestCase(nameof(ToolModel.AirConsumption), true)]
        public void HasPropertyGivesCorrectResult(string property, bool expectedResult)
        {
            var toolType = new PulseDriverShutOff();
            Assert.AreEqual(expectedResult, toolType.DoesToolTypeHasProperty(property));
        }

        [Test]
        public void AcceptingVisitorCallsCorrectVisitFunction()
        {
            var toolType = new PulseDriverShutOff();
            var visitor = new AbstractToolTypeVisitorMock();
            toolType.Accept(visitor);
            Assert.IsTrue(visitor.VisitPulseDriverShutOffWasCalled);
        }
    }
}
