using Client.Core.Validator;
using Client.TestHelper.Factories;
using NUnit.Framework;

namespace Client.Core.Test.Validator
{
    public class ExtensionValidatorTest
    {
        [TestCase("", false)]
        [TestCase("s", true)]
        public void ValidateReturnsCorrectValueForInventoryNumber(string inventoryNumber, bool result)
        {
            var validator = new ExtensionValidator();
            var extension = CreateExtension.RandomizedWithInventoryNumber(1234, inventoryNumber);
            extension.FactorTorque = 1;
            extension.Length = 0;
            Assert.AreEqual(result, validator.Validate(extension));
        }

        [TestCase("", false)]
        [TestCase("s", true)]
        public void ValidateReturnsCorrectValueForDescription(string description, bool result)
        {
            var validator = new ExtensionValidator();
            var extension = CreateExtension.RandomizedWithDescription(1234, description);
            extension.FactorTorque = 1;
            extension.Length = 0;
            Assert.AreEqual(result, validator.Validate(extension));
        }

        [TestCase(0.8, false)]
        [TestCase(0.9, true)]
        [TestCase(1.5, true)]
        [TestCase(1.6, false)]
        public void ValidateReturnsCorrectValueForFactor(double factor, bool result)
        {
            var validator = new ExtensionValidator();
            var extension = CreateExtension.Randomized(1234);
            extension.FactorTorque = factor;
            extension.Length = 0;
            Assert.AreEqual(result, validator.Validate(extension));
        }

        [TestCase(-1, false)]
        [TestCase(0, true)]
        [TestCase(9999, true)]
        [TestCase(10000, false)]
        public void ValidateReturnsCorrectValueForLength(double gauge, bool result)
        {
            var validator = new ExtensionValidator();
            var extension = CreateExtension.Randomized(1234);
            extension.FactorTorque = 1;
            extension.Length = gauge;
            Assert.AreEqual(result, validator.Validate(extension));
        }

        [TestCase(-1, false)]
        [TestCase(0, true)]
        [TestCase(1, true)]
        [TestCase(99, true)]
        [TestCase(100, false)]
        public void ValidateReturnsCorrectValueForBending(double bending, bool result)
        {
            var validator = new ExtensionValidator();
            var extension = CreateExtension.Randomized(1234);
            extension.FactorTorque = 1;
            extension.Length = 0;
            extension.Bending = bending;
            Assert.AreEqual(result, validator.Validate(extension));
        }
    }
}
