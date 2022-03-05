using Core.PhysicalValueTypes;
using NUnit.Framework;

namespace Core.Test.PhysicalValueTypes
{
    class AngleTest
    {
        [TestCase(123, 7047.380, 7047.381)]
        [TestCase(963.5, 55204.483, 55204.484)]
        public void AngleFromRadiantConvertsCorrectlyToDegree(double rad, double resultMin, double resultMax)
        {
            var angle = Angle.FromRadiant(rad);
            var deg = angle.Degree;
            Assert.IsTrue(deg >= resultMin);
            Assert.IsTrue(deg < resultMax);
        }

        [TestCase(7411, 129.346, 129.347)]
        [TestCase(258.6, 4.513, 4.514)]
        public void AngleFromDegreeConvertsCorrectlyToRadiant(double deg, double resultMin, double resultMax)
        {
            var angle = Angle.FromDegree(deg);
            var rad = angle.Radiant;
            Assert.IsTrue(rad >= resultMin);
            Assert.IsTrue(rad < resultMax);
        }
    }
}
