using Client.Core.Validator;
using Client.TestHelper.Factories;
using Core.PhysicalValueTypes;
using NUnit.Framework;
using TestHelper.Factories;

namespace Client.Core.Test.Validator
{
    public class TestEquipmentValidatorTest
    {
        [TestCase("", false)]
        [TestCase("s", true)]
        public void ValidateReturnsCorrectValueForSerialNumber(string serialNumber, bool result)
        {
            var validator = new TestEquipmentValidator();
            var testEquipment = CreateTestEquipment.WithSerialNumber(serialNumber);
            Assert.AreEqual(result,validator.Validate(testEquipment));
        }

        [TestCase("", false)]
        [TestCase("s", true)]
        public void ValidateReturnsCorrectValueForInventoryNumber(string serialNumber, bool result)
        {
            var validator = new TestEquipmentValidator();
            var testEquipment = CreateTestEquipment.WithSerialNumber(serialNumber);
            Assert.AreEqual(result, validator.Validate(testEquipment));
        }

        [TestCase("", false)]
        [TestCase("s", true)]
        public void ValidateReturnsCorrectValueForModelName(string modelName, bool result)
        {
            var validator = new TestEquipmentValidator();
            var testEquipment = CreateTestEquipmentModel.RandomizedWithName(12324, modelName);
            Assert.AreEqual(result, validator.Validate(testEquipment));
        }


        [TestCase(3, 2, false)]
        [TestCase(7.8, 2.3, false)]
        [TestCase(5, 8, true)]
        [TestCase(6, 6, true)]
        public void ValidateReturnsCorrectValueForMinAndMaxCapacity(double min, double max, bool result)
        {
            var testEquipment = CreateTestEquipment.Anonymous();
            testEquipment.CapacityMin = Torque.FromNm(min);
            testEquipment.CapacityMax = Torque.FromNm(max);

            var validator = new TestEquipmentValidator();
            Assert.AreEqual(result, validator.Validate(testEquipment));
        }
    }
}
