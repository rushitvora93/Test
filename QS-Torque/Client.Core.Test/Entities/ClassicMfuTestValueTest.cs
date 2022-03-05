using Common.Types.Enums;
using Core.Entities;
using Core.Enums;
using NUnit.Framework;

namespace Core.Test.Entities
{
    class ClassicMfuTestValueTest
    {
        [TestCase(MeaUnit.Nm, MeaUnit.Nm, 1.0, 2.0, 1.0)]
        [TestCase(MeaUnit.Deg, MeaUnit.Nm, 11.0, 12.0, 12.0)]
        public void GetControlledByValueReturnsCorrectValue(MeaUnit controlledByUnitId, MeaUnit unit1Id, double valueUnit1, double valueUnit2, double resultValue)
        {
            var classicMfuTestValue = new ClassicMfuTestValue()
            {
                MfuTest = new ClassicMfuTest()
                {
                    ControlledByUnitId = controlledByUnitId,
                    Unit1Id = unit1Id,
                },
                ValueUnit1 = valueUnit1,
                ValueUnit2 = valueUnit2
            };
            Assert.AreEqual(resultValue, classicMfuTestValue.ControlledByValue);
        }
    }
}
