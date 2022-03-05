using System.Collections.Generic;
using NUnit.Framework;
using Server.Core.Entities;
using TestHelper.Checker;

namespace Server.Core.Test.Entities
{
    class ToleranceClassTest
    {
        [TestCase(78987, 1254854)]
        [TestCase(7589, 23456)]
        public void GetLowerLimitForValueReturnsCorrectValueOfAbsolutToleranceClass(double lowerLimit, double value)
        {
            var toleranceClass = new ToleranceClass()
            {
                Relative = false,
                LowerLimit = lowerLimit
            };

            Assert.AreEqual(value - lowerLimit, toleranceClass.GetLowerLimitForValue(value));
        }

        [TestCase(10, 1254854)]
        [TestCase(20, 23456)]
        public void GetLowerLimitForValueReturnsCorrectValueOfRelativeToleranceClass(double lowerLimit, double value)
        {
            var toleranceClass = new ToleranceClass()
            {
                Relative = true,
                LowerLimit = lowerLimit
            };

            Assert.AreEqual(value - (value * lowerLimit / 100), toleranceClass.GetLowerLimitForValue(value));
        }

        [TestCase(245, 12854)]
        [TestCase(7589, 23456)]
        public void GetUpperLimitForValueReturnsCorrectValueOfAbsolutToleranceClass(double upperLimit, double value)
        {
            var toleranceClass = new ToleranceClass()
            {
                Relative = false,
                UpperLimit = upperLimit
            };

            Assert.AreEqual(value + upperLimit, toleranceClass.GetUpperLimitForValue(value));
        }

        [TestCase(245, 12854)]
        [TestCase(7589, 23456)]
        public void GetUpperLimitForValueReturnsCorrectValueOfRelativeToleranceClass(double upperLimit, double value)
        {
            var toleranceClass = new ToleranceClass()
            {
                Relative = true,
                UpperLimit = upperLimit
            };

            Assert.AreEqual(value + (value * upperLimit / 100), toleranceClass.GetUpperLimitForValue(value));
        }

        [TestCase(5, 6)]
        [TestCase(3500, 98745)]
        public void AbsoluteToleranceClassReturnsZeroForMinValueIfItWouldBeNegative(double setpoint, double lowerLimit)
        {
            var toleranceClass = new ToleranceClass()
            {
                Relative = false,
                LowerLimit = lowerLimit
            };

            Assert.AreEqual(0, toleranceClass.GetLowerLimitForValue(setpoint));
        }

        [TestCase(5, 105)]
        [TestCase(963, 101)]
        public void RelativeToleranceClassReturnsZeroForMinValueIfItWouldBeNegative(double setpoint, double lowerLimit)
        {
            var toleranceClass = new ToleranceClass()
            {
                Relative = true,
                LowerLimit = lowerLimit
            };

            Assert.AreEqual(0, toleranceClass.GetLowerLimitForValue(setpoint));
        }


        #region IQstEquality EqualsById

        [TestCaseSource(nameof(EqualsByIdTestSource))]
        public void EqualsByIdWithDifferentParameterMeansInequality((EqualityParameter<ToleranceClass> parameter, EqualityTestHelper<ToleranceClass> helper) helperTuple)
        {
            helperTuple.helper.CheckInequalityForParameter(helperTuple.parameter);
        }

        [Test]
        public void EqualsByIdWithRightIsNullMeansInequality()
        {
            var helper = GetEqualsByIdTestHelper();
            helper.CheckInequalityWithRightIsNull();
        }

        [Test]
        public void EqualsByIdWithEqualIdMeansEquality()
        {
            var helper = GetEqualsByIdTestHelper();
            helper.CheckEqualityForParameterList();
        }


        private static IEnumerable<(EqualityParameter<ToleranceClass>, EqualityTestHelper<ToleranceClass>)> EqualsByIdTestSource()
        {
            var helper = GetEqualsByIdTestHelper();

            foreach (var param in helper.EqualityParameterList)
            {
                yield return (param, helper);
            }
        }

        private static EqualityTestHelper<ToleranceClass> GetEqualsByIdTestHelper()
        {
            return new EqualityTestHelper<ToleranceClass>(
                (left, right) => left.EqualsById(right),
                () => new ToleranceClass(),
                new List<EqualityParameter<ToleranceClass>>()
                {
                    new EqualityParameter<ToleranceClass>()
                    {
                        SetParameter = (entity, value) => entity.Id = value as ToleranceClassId,
                        CreateParameterValue = () => new ToleranceClassId(8520),
                        CreateOtherParameterValue = () => new ToleranceClassId(7895453)
                    }
                });
        }

        #endregion


        #region IQstEquality EqualsByContent

        [TestCaseSource(nameof(EqualsByContentTestSource))]
        public void EqualsByContentWithDifferentParameterMeansInequality((EqualityParameter<ToleranceClass> parameter, EqualityTestHelper<ToleranceClass> helper) helperTuple)
        {
            helperTuple.helper.CheckInequalityForParameter(helperTuple.parameter);
        }

        [Test]
        public void EqualsByContentWithRightIsNullMeansInequality()
        {
            var helper = GetEqualsByContentTestHelper();
            helper.CheckInequalityWithRightIsNull();
        }

        [Test]
        public void EqualsByContentWithEqualContentMeansEquality()
        {
            var helper = GetEqualsByContentTestHelper();
            helper.CheckEqualityForParameterList();
        }

        [Test]
        public void UpdateWithMeansContentEquality()
        {
            var helper = GetEqualsByContentTestHelper();
            helper.CheckEqualityAfterUpdate((left, right) => left.UpdateWith(right));
        }

        [Test]
        public void UpdateWithNullThrowsNoException()
        {
            var helper = GetEqualsByContentTestHelper();
            var left = helper.GetFilledEntity(helper.EqualityParameterList);

            Assert.DoesNotThrow(() => left.UpdateWith(null));
        }

        [Test]
        public void CopyMeansContentEqualityButNotReferenceEquality()
        {
            var helper = GetEqualsByContentTestHelper();
            helper.CheckEqualityAfterCopy(entity => entity.CopyDeep());
        }


        private static IEnumerable<(EqualityParameter<ToleranceClass>, EqualityTestHelper<ToleranceClass>)> EqualsByContentTestSource()
        {
            var helper = GetEqualsByContentTestHelper();

            foreach (var param in helper.EqualityParameterList)
            {
                yield return (param, helper);
            }
        }

        private static EqualityTestHelper<ToleranceClass> GetEqualsByContentTestHelper()
        {
            return new EqualityTestHelper<ToleranceClass>(
                (left, right) => left.EqualsByContent(right),
                () => new ToleranceClass(),
                new List<EqualityParameter<ToleranceClass>>()
                {
                    new EqualityParameter<ToleranceClass>()
                    {
                        SetParameter = (entity, value) => entity.Id = value as ToleranceClassId,
                        CreateParameterValue = () => new ToleranceClassId(8520),
                        CreateOtherParameterValue = () => new ToleranceClassId(7895453),
                        ParameterName = nameof(ToleranceClass.Id)
                    },
                    new EqualityParameter<ToleranceClass>()
                    {
                        SetParameter = (entity, value) => entity.Name = value as string,
                        CreateParameterValue = () => "rpotgiujvkl",
                        CreateOtherParameterValue = () => "rwä9öefzoighd",
                        ParameterName = nameof(ToleranceClass.Name)
                    },
                    new EqualityParameter<ToleranceClass>()
                    {
                        SetParameter = (entity, value) => entity.Relative = (bool)value,
                        CreateParameterValue = () => true,
                        CreateOtherParameterValue = () => false,
                        ParameterName = nameof(ToleranceClass.Relative)
                    },
                    new EqualityParameter<ToleranceClass>()
                    {
                        SetParameter = (entity, value) => entity.LowerLimit = (double)value,
                        CreateParameterValue = () => 984561.5d,
                        CreateOtherParameterValue = () => 9464756d,
                        ParameterName = nameof(ToleranceClass.LowerLimit)
                    },
                    new EqualityParameter<ToleranceClass>()
                    {
                        SetParameter = (entity, value) => entity.UpperLimit = (double)value,
                        CreateParameterValue = () => 34567d,
                        CreateOtherParameterValue = () => 8765d,
                        ParameterName = nameof(ToleranceClass.UpperLimit)
                    }
                });
        }

        #endregion
    }
}
