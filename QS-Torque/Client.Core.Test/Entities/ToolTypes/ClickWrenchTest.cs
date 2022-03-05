using Core.Entities;
using Core.Entities.ToolTypes;
using TestHelper.Mock;
using NUnit.Framework;

namespace Core.Test.Entities.ToolTypes
{
    class ClickWrenchTest
    {
        [Test]
        public void ClickWrenchHasCorrectName()
        {
            var toolType = new ClickWrench();
            Assert.AreEqual("ClickWrench", toolType.Name);
        }

        [TestCase(nameof(ToolModel.Class), true)]
        [TestCase(nameof(ToolModel.BatteryVoltage), false)]
        [TestCase(nameof(ToolModel.MaxRotationSpeed), false)]
        [TestCase(nameof(ToolModel.AirPressure), false)]
        [TestCase(nameof(ToolModel.AirConsumption), false)]
        public void HasPropertyGivesCorrectResult(string property, bool expectedResult)
        {
            var toolType = new ClickWrench();
            Assert.AreEqual(expectedResult, toolType.DoesToolTypeHasProperty(property));
        }

        [Test]
        public void AcceptingVisitorCallsCorrectVisitFunction()
        {
            var toolType = new ClickWrench();
            var visitor = new AbstractToolTypeVisitorMock();
            toolType.Accept(visitor);
            Assert.IsTrue(visitor.VisitClickWrenchWasCalled);
        }
    }
}
