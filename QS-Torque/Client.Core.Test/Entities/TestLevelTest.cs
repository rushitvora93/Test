using System.Collections.Generic;
using Core.Entities;
using NUnit.Framework;
using TestHelper.Checker;

namespace Core.Test.Entities
{
    public class TestLevelTest
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
        public void EqualsByIdWithDifferentParameterMeansInequality((EqualityParameter<TestLevel> parameter, EqualityTestHelper<TestLevel> helper) helperTuple)
        {
            helperTuple.helper.CheckInequalityForParameter(helperTuple.parameter);
        }

        [TestCaseSource(nameof(EqualsByContentTestSource))]
        public void EqualsByContentWithDifferentParameterMeansInequality((EqualityParameter<TestLevel> parameter, EqualityTestHelper<TestLevel> helper) helperTuple)
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



        private static IEnumerable<(EqualityParameter<TestLevel>, EqualityTestHelper<TestLevel>)> EqualsByIdTestSource()
        {
            var helper = GetEqualsByIdTestHelper();

            foreach (var param in helper.EqualityParameterList)
            {
                yield return (param, helper);
            }
        }

        private static EqualityTestHelper<TestLevel> GetEqualsByIdTestHelper()
        {
            return new EqualityTestHelper<TestLevel>(
                (left, right) => left.EqualsById(right),
                () => new TestLevel(),
                new List<EqualityParameter<TestLevel>>()
                {
                    new EqualityParameter<TestLevel>()
                    {
                        SetParameter = (entity, value) => entity.Id = value as TestLevelId,
                        CreateParameterValue = () => new TestLevelId(8520),
                        CreateOtherParameterValue = () => new TestLevelId(7895453)
                    }
                });
        }

        private static IEnumerable<(EqualityParameter<TestLevel>, EqualityTestHelper<TestLevel>)> EqualsByContentTestSource()
        {
            var helper = GetEqualsByContentTestHelper();

            foreach (var param in helper.EqualityParameterList)
            {
                yield return (param, helper);
            }
        }

        private static EqualityTestHelper<TestLevel> GetEqualsByContentTestHelper()
        {
            return new EqualityTestHelper<TestLevel>(
                (left, right) => left.EqualsByContent(right),
                () => new TestLevel(),
                new List<EqualityParameter<TestLevel>>()
                {
                    new EqualityParameter<TestLevel>()
                    {
                        SetParameter = (entity, value) => entity.Id = value as TestLevelId,
                        CreateParameterValue = () => new TestLevelId(8520),
                        CreateOtherParameterValue = () => new TestLevelId(7895453),
                        ParameterName = nameof(TestLevel.Id)
                    },
                    new EqualityParameter<TestLevel>()
                    {
                        SetParameter = (entity, value) => entity.TestInterval = value as Interval,
                        CreateParameterValue = () => new Interval() {IntervalValue = 1},
                        CreateOtherParameterValue = () => new Interval() {IntervalValue = 1000},
                        ParameterName = nameof(TestLevel.TestInterval)
                    },
                    new EqualityParameter<TestLevel>()
                    {
                        SetParameter = (entity, value) => entity.SampleNumber = (int)value,
                        CreateParameterValue = () => 345678,
                        CreateOtherParameterValue = () => 987654,
                        ParameterName = nameof(TestLevel.SampleNumber)
                    },
                    new EqualityParameter<TestLevel>()
                    {
                        SetParameter = (entity, value) => entity.ConsiderWorkingCalendar = (bool)value,
                        CreateParameterValue = () => true,
                        CreateOtherParameterValue = () => false,
                        ParameterName = nameof(TestLevel.ConsiderWorkingCalendar)
                    },
                    new EqualityParameter<TestLevel>()
                    {
                        SetParameter = (entity, value) => entity.IsActive = (bool)value,
                        CreateParameterValue = () => true,
                        CreateOtherParameterValue = () => false,
                        ParameterName = nameof(TestLevel.IsActive)
                    }
                });
        }
    }
}
