using Core.Entities;
using NUnit.Framework;

namespace Core.Test.Entities
{
    class IntervalTest
    {
        [Test]
        public void EqualsByContentWithDifferentIdsMeansInequality()
        {
            var left = new Interval() { IntervalValue = 97486153 };
            var right = new Interval() { IntervalValue = 147896325 };

            Assert.IsFalse(left.EqualsByContent(right));
        }

        [Test]
        public void EqualsByContentWithDifferentTypeMeansInequality()
        {
            var left = new Interval() { IntervalValue = 97486153, Type = IntervalType.EveryXShifts };
            var right = new Interval() { IntervalValue = 97486153, Type = IntervalType.XTimesAShift };

            Assert.IsFalse(left.EqualsByContent(right));
        }

        [Test]
        public void EqualsByContentWithNullMeansInequality()
        {
            var left = new Interval() { IntervalValue = 97486153 };

            Assert.IsFalse(left.EqualsByContent(null));
        }

        [Test]
        public void EqualsByIdWithEqualIdsMeansEquality()
        {
            var left = new Interval() { IntervalValue = 97486153, Type = IntervalType.EveryXShifts};
            var right = new Interval() { IntervalValue = 97486153, Type = IntervalType.EveryXShifts };

            Assert.IsTrue(left.EqualsByContent(right));
        }

        [Test]
        public void CopyDeepCopiesRight()
        {
            var interval = new Interval() { IntervalValue = 97486153, Type = IntervalType.EveryXShifts };
            var copy = interval.CopyDeep();

            Assert.AreNotSame(interval, copy);
            Assert.IsTrue(interval.EqualsByContent(copy));
        }


        [TestCase(901, IntervalErrors.PeriodEveryXDaysHasToBeLessOrEqual900, true)]
        [TestCase(4000, IntervalErrors.PeriodEveryXDaysHasToBeLessOrEqual900, true)]
        [TestCase(900, IntervalErrors.PeriodEveryXDaysHasToBeLessOrEqual900, false)]
        [TestCase(-5, IntervalErrors.PeriodEveryXDaysHasToBeGreaterThan0, true)]
        [TestCase(-25, IntervalErrors.PeriodEveryXDaysHasToBeGreaterThan0, true)]
        [TestCase(1, IntervalErrors.PeriodEveryXDaysHasToBeGreaterThan0, false)]
        public void ValidateReturnsCorrectValue(int periodEveryXDays, IntervalErrors intervalError, bool hasError)
        {
            var interval = new Interval();
            interval.IntervalValue = periodEveryXDays;
            var result = interval.Validate(nameof(Interval.IntervalValue));
            Assert.AreEqual(hasError, result.Contains(intervalError));
        }
    }
}
