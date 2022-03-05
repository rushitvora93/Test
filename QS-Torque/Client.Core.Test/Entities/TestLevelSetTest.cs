using System.Collections.Generic;
using Core.Entities;
using NUnit.Framework;
using TestHelper.Checker;

namespace Core.Test.Entities
{
    public class TestLevelSetTest
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
        public void EqualsByIdWithDifferentParameterMeansInequality((EqualityParameter<TestLevelSet> parameter, EqualityTestHelper<TestLevelSet> helper) helperTuple)
        {
            helperTuple.helper.CheckInequalityForParameter(helperTuple.parameter);
        }

        [TestCaseSource(nameof(EqualsByContentTestSource))]
        public void EqualsByContentWithDifferentParameterMeansInequality((EqualityParameter<TestLevelSet> parameter, EqualityTestHelper<TestLevelSet> helper) helperTuple)
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



        private static IEnumerable<(EqualityParameter<TestLevelSet>, EqualityTestHelper<TestLevelSet>)> EqualsByIdTestSource()
        {
            var helper = GetEqualsByIdTestHelper();

            foreach (var param in helper.EqualityParameterList)
            {
                yield return (param, helper);
            }
        }

        private static EqualityTestHelper<TestLevelSet> GetEqualsByIdTestHelper()
        {
            return new EqualityTestHelper<TestLevelSet>(
                (left, right) => left.EqualsById(right),
                () => new TestLevelSet(),
                new List<EqualityParameter<TestLevelSet>>()
                {
                    new EqualityParameter<TestLevelSet>()
                    {
                        SetParameter = (entity, value) => entity.Id = value as TestLevelSetId,
                        CreateParameterValue = () => new TestLevelSetId(8520),
                        CreateOtherParameterValue = () => new TestLevelSetId(7895453)
                    }
                });
        }

        private static IEnumerable<(EqualityParameter<TestLevelSet>, EqualityTestHelper<TestLevelSet>)> EqualsByContentTestSource()
        {
            var helper = GetEqualsByContentTestHelper();

            foreach (var param in helper.EqualityParameterList)
            {
                yield return (param, helper);
            }
        }

        private static EqualityTestHelper<TestLevelSet> GetEqualsByContentTestHelper()
        {
            return new EqualityTestHelper<TestLevelSet>(
                (left, right) => left.EqualsByContent(right),
                () => new TestLevelSet(),
                new List<EqualityParameter<TestLevelSet>>()
                {
                    new EqualityParameter<TestLevelSet>()
                    {
                        SetParameter = (entity, value) => entity.Id = value as TestLevelSetId,
                        CreateParameterValue = () => new TestLevelSetId(8520),
                        CreateOtherParameterValue = () => new TestLevelSetId(7895453),
                        ParameterName = nameof(TestLevelSet.Id)
                    },
                    new EqualityParameter<TestLevelSet>()
                    {
                        SetParameter = (entity, value) => entity.Name = value as TestLevelSetName,
                        CreateParameterValue = () => new TestLevelSetName("fowdplr,gkert"),
                        CreateOtherParameterValue = () => new TestLevelSetName("7895453werftghjk"),
                        ParameterName = nameof(TestLevelSet.Name)
                    },
                    new EqualityParameter<TestLevelSet>()
                    {
                        SetParameter = (entity, value) => entity.TestLevel1 = value as TestLevel,
                        CreateParameterValue = () => new TestLevel()
                        {
                            Id = new TestLevelId(12),
                            TestInterval = new Interval() {IntervalValue = 100},
                            ConsiderWorkingCalendar = true,
                            SampleNumber = 50,
                            IsActive = false
                        },
                        CreateOtherParameterValue = () => new TestLevel()
                        {
                            Id = new TestLevelId(89),
                            TestInterval = new Interval() {IntervalValue = 54},
                            ConsiderWorkingCalendar = false,
                            SampleNumber = 65,
                            IsActive = true
                        },
                        ParameterName = nameof(TestLevelSet.TestLevel1)
                    },
                    new EqualityParameter<TestLevelSet>()
                    {
                        SetParameter = (entity, value) => entity.TestLevel2 = value as TestLevel,
                        CreateParameterValue = () => new TestLevel()
                        {
                            Id = new TestLevelId(45),
                            TestInterval = new Interval() {IntervalValue = 63},
                            ConsiderWorkingCalendar = false,
                            SampleNumber = 10,
                            IsActive = true
                        },
                        CreateOtherParameterValue = () => new TestLevel()
                        {
                            Id = new TestLevelId(32),
                            TestInterval = new Interval() {IntervalValue = 96},
                            ConsiderWorkingCalendar = true,
                            SampleNumber = 75,
                            IsActive = false
                        },
                        ParameterName = nameof(TestLevelSet.TestLevel2)
                    },
                    new EqualityParameter<TestLevelSet>()
                    {
                        SetParameter = (entity, value) => entity.TestLevel3 = value as TestLevel,
                        CreateParameterValue = () => new TestLevel()
                        {
                            Id = new TestLevelId(73),
                            TestInterval = new Interval() {IntervalValue = 19},
                            ConsiderWorkingCalendar = true,
                            SampleNumber = 38,
                            IsActive = false
                        },
                        CreateOtherParameterValue = () => new TestLevel()
                        {
                            Id = new TestLevelId(84),
                            TestInterval = new Interval() {IntervalValue = 45},
                            ConsiderWorkingCalendar = false,
                            SampleNumber = 26,
                            IsActive = true
                        },
                        ParameterName = nameof(TestLevelSet.TestLevel3)
                    }
                });
        }
    }
}
