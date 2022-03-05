using Core.Entities;
using NUnit.Framework;

namespace Core.Test.Entities
{
    public class ExplicitlyNullableTest
    {
        [TestCase(true, 1)]
        [TestCase(true, 7)]
        [TestCase(false, null)]
        public void HasValueReturnsCorrectValue(bool expectedResult, double? val)
        {
            var explicitlyType = new ExplicitlyNullable<double>(val);
            Assert.AreEqual(expectedResult, explicitlyType.HasValue());
        }

        [TestCase(1, 10, 1)]
        [TestCase(10078, 10, 10078)]
        [TestCase(19, 19, null)]
        [TestCase(23, 23, null)]
        public void GetValueOrDefaultReturnsCorrectValue(int expectedValue, int defaultValue, int? val)
        {
            var explicitlyType = new ExplicitlyNullable<int>(val);
            Assert.AreEqual(expectedValue, explicitlyType.GetValueOrDefault(defaultValue));
        }
    }
}
