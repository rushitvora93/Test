using Core.Entities;
using Core.Entities.ToolTypes;
using NUnit.Framework;
using TestHelper.Mock;

namespace Core.Test.Entities.ToolTypes
{
    class GeneralTest
    {
        [Test]
        public void GeneralHasCorrectName()
        {
            var toolType = new General();
            Assert.AreEqual("General", toolType.Name);
        }

        [TestCase(nameof(ToolModel.Class), false)]
        [TestCase(nameof(ToolModel.BatteryVoltage), true)]
        [TestCase(nameof(ToolModel.MaxRotationSpeed), true)]
        [TestCase(nameof(ToolModel.AirPressure), true)]
        [TestCase(nameof(ToolModel.AirConsumption), true)]
        public void HasPropertyGivesCorrectResult(string property, bool expectedResult)
        {
            var toolType = new General();
            Assert.AreEqual(expectedResult, toolType.DoesToolTypeHasProperty(property));
        }

        [Test]
        public void AcceptingVisitorCallsCorrectVisitFunction()
        {
            var toolType = new General();
            var visitor = new AbstractToolTypeVisitorMock();
            toolType.Accept(visitor);
            Assert.IsTrue(visitor.VisitGeneralWasCalled);
        }
    }
}
