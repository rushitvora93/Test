using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using NUnit.Framework;
using TestHelper.Checker;

namespace Core.Test.Entities
{
    public class TestPlanTest
    {
        [Test]
        public void EqualsByIdWithEqualIdMeansEquality()
        {
            var helper = GetEqualsByIdTestHelper();
            helper.CheckEqualityForParameterList();
        }

        [Test]
        public void EqualsByIdWithRightIsNullMeansInequality()
        {
            var helper = GetEqualsByIdTestHelper();
            helper.CheckInequalityWithRightIsNull();
        }

        [TestCaseSource(nameof(EqualsByIdTestSource))]
        public void EqualsByIdWithDifferentParameterMeansInequality((EqualityParameter<TestPlan> parameter, EqualityTestHelper<TestPlan> helper) helperTuple)
        {
            helperTuple.helper.CheckInequalityForParameter(helperTuple.parameter);
        }

        [TestCaseSource(nameof(EqualsByContentTestSource))]
        public void EqualsByContentWithDifferentParameterMeansInequality((EqualityParameter<TestPlan> parameter, EqualityTestHelper<TestPlan> helper) helperTuple)
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

        [TestCase(-50, true)]
        [TestCase(-1, true)]
        [TestCase(0, true)]
        [TestCase(1, false)]
        public void ValidateReturnsIntervalIsLessThanOneShiftCorrectly(int value, bool errorExpected)
        {
            var testPlan = new TestPlan()
            {
                TestInterval = new Interval()
                {
                    IntervalValue = value
                }
            };
            var result = testPlan.Validate(nameof(TestPlan.TestInterval));
            Assert.AreEqual(errorExpected, result.Any(x => x == TestPlanValidationError.IntervalIsLessThanOneShift));
        }

        [TestCase(-50, true)]
        [TestCase(-1, true)]
        [TestCase(0, true)]
        [TestCase(1, false)]
        public void ValidateReturnsSampleNumberIsLessThanOneCorrectly(int value, bool errorExpected)
        {
            var testPlan = new TestPlan()
            {
                SampleNumber = value
            };
            var result = testPlan.Validate(nameof(TestPlan.SampleNumber));
            Assert.AreEqual(errorExpected, result.Any(x => x == TestPlanValidationError.SampleNumberIsLessThanOne));
        }

        [TestCase("2020-07-06 00:00:00", "2020-07-06 00:00:00", true, true)]
        [TestCase("2020-07-06 00:00:00", "2020-07-06 00:00:00", false, false)]
        [TestCase("2020-07-06 00:00:00", "2020-06-06 00:00:00", true, true)]
        [TestCase("2020-07-06 00:00:00", "2020-06-06 00:00:00", false, false)]
        [TestCase("2020-07-06 00:00:00", "2020-08-06 00:00:00", true, false)]
        [TestCase("2020-07-06 00:00:00", "2020-08-06 00:00:00", false, false)]
        public void ValidateReturnsEndDateIsNotGreaterThanStartDateCorrectly(DateTime startDate, DateTime endDate, bool isEndDateActive, bool errorExpected)
        {
            var testPlan = new TestPlan()
            {
                StartDate = startDate,
                IsEndDateEnabled = isEndDateActive,
                EndDate = endDate
            };
            var result = testPlan.Validate(nameof(TestPlan.EndDate));
            Assert.AreEqual(errorExpected, result.Any(x => x == TestPlanValidationError.EndDateIsNotGreaterThanStartDate));
        }


        private static IEnumerable<(EqualityParameter<TestPlan>, EqualityTestHelper<TestPlan>)> EqualsByIdTestSource()
        {
            var helper = GetEqualsByIdTestHelper();

            foreach (var param in helper.EqualityParameterList)
            {
                yield return (param, helper);
            }
        }

        private static EqualityTestHelper<TestPlan> GetEqualsByIdTestHelper()
        {
            return new EqualityTestHelper<TestPlan>(
                (left, right) => left.EqualsById(right),
                () => new TestPlan(),
                new List<EqualityParameter<TestPlan>>()
                {
                    new EqualityParameter<TestPlan>()
                    {
                        SetParameter = (entity, value) => entity.Id = value as TestPlanId,
                        CreateParameterValue = () => new TestPlanId(8520),
                        CreateOtherParameterValue = () => new TestPlanId(7895453)
                    }
                });
        }

        private static IEnumerable<(EqualityParameter<TestPlan>, EqualityTestHelper<TestPlan>)> EqualsByContentTestSource()
        {
            var helper = GetEqualsByContentTestHelper();

            foreach (var param in helper.EqualityParameterList)
            {
                yield return (param, helper);
            }
        }

        private static EqualityTestHelper<TestPlan> GetEqualsByContentTestHelper()
        {
            return new EqualityTestHelper<TestPlan>(
                (left, right) => left.EqualsByContent(right),
                () => new TestPlan(),
                new List<EqualityParameter<TestPlan>>()
                {
                    new EqualityParameter<TestPlan>()
                    {
                        SetParameter = (entity, value) => entity.Id = value as TestPlanId,
                        CreateParameterValue = () => new TestPlanId(8520),
                        CreateOtherParameterValue = () => new TestPlanId(7895453),
                        ParameterName = nameof(TestPlan.Id)
                    },
                    new EqualityParameter<TestPlan>()
                    {
                        SetParameter = (entity, value) => entity.Name = value as TestPlanName,
                        CreateParameterValue = () => new TestPlanName("fowdplr,gkert"),
                        CreateOtherParameterValue = () => new TestPlanName("7895453werftghjk"),
                        ParameterName = nameof(TestPlan.Name)
                    },
                    new EqualityParameter<TestPlan>()
                    {
                        SetParameter = (entity, value) => entity.TestInterval = value as Interval,
                        CreateParameterValue = () => new Interval() {IntervalValue = 1},
                        CreateOtherParameterValue = () => new Interval() {IntervalValue = 1000},
                        ParameterName = nameof(TestPlan.TestInterval)
                    },
                    new EqualityParameter<TestPlan>()
                    {
                        SetParameter = (entity, value) => entity.SampleNumber = (int)value,
                        CreateParameterValue = () => 345678,
                        CreateOtherParameterValue = () => 987654,
                        ParameterName = nameof(TestPlan.SampleNumber)
                    },
                    new EqualityParameter<TestPlan>()
                    {
                        SetParameter = (entity, value) => entity.Behavior = (TestPlanBehavior)value,
                        CreateParameterValue = () => TestPlanBehavior.Dynamic,
                        CreateOtherParameterValue = () => TestPlanBehavior.Static,
                        ParameterName = nameof(TestPlan.Behavior)
                    },
                    new EqualityParameter<TestPlan>()
                    {
                        SetParameter = (entity, value) => entity.ConsiderHolidays = (bool)value,
                        CreateParameterValue = () => true,
                        CreateOtherParameterValue = () => false,
                        ParameterName = nameof(TestPlan.ConsiderHolidays)
                    },
                    new EqualityParameter<TestPlan>()
                    {
                        SetParameter = (entity, value) => entity.StartDate = (DateTime)value,
                        CreateParameterValue = () => new DateTime(321, 2, 5),
                        CreateOtherParameterValue = () => new DateTime(987, 5, 9),
                        ParameterName = nameof(TestPlan.StartDate)
                    },
                    new EqualityParameter<TestPlan>()
                    {
                        SetParameter = (entity, value) => entity.IsEndDateEnabled = (bool)value,
                        CreateParameterValue = () => true,
                        CreateOtherParameterValue = () => false,
                        ParameterName = nameof(TestPlan.IsEndDateEnabled)
                    },
                    new EqualityParameter<TestPlan>()
                    {
                        SetParameter = (entity, value) => entity.EndDate = (DateTime)value,
                        CreateParameterValue = () => new DateTime(321, 2, 5),
                        CreateOtherParameterValue = () => new DateTime(987, 5, 9),
                        ParameterName = nameof(TestPlan.EndDate)
                    }
                });
        }
    }
}
