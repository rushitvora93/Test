using System.Collections.Generic;
using NUnit.Framework;
using Server.Core.Entities;
using TestHelper.Checker;

namespace Server.Core.Test.Entities
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
                        CreateParameterValue = () => new TestLevelSetName("woerhweriohd"),
                        CreateOtherParameterValue = () => new TestLevelSetName("67584930ß"),
                        ParameterName = nameof(TestLevelSet.Name)
                    },
                    new EqualityParameter<TestLevelSet>()
                    {
                        SetParameter = (entity, value) => entity.TestLevel1 = value as TestLevel,
                        CreateParameterValue = () => new TestLevel() { Id = new TestLevelId(1), SampleNumber = 5 },
                        CreateOtherParameterValue = () => new TestLevel() { Id = new TestLevelId(2), SampleNumber = 10 },
                        ParameterName = nameof(TestLevelSet.TestLevel1)
                    },
                    new EqualityParameter<TestLevelSet>()
                    {
                        SetParameter = (entity, value) => entity.TestLevel2 = value as TestLevel,
                        CreateParameterValue = () => new TestLevel() { Id = new TestLevelId(1), SampleNumber = 5 },
                        CreateOtherParameterValue = () => new TestLevel() { Id = new TestLevelId(2), SampleNumber = 10 },
                        ParameterName = nameof(TestLevelSet.TestLevel2)
                    },
                    new EqualityParameter<TestLevelSet>()
                    {
                        SetParameter = (entity, value) => entity.TestLevel3 = value as TestLevel,
                        CreateParameterValue = () => new TestLevel() { Id = new TestLevelId(1), SampleNumber = 5 },
                        CreateOtherParameterValue = () => new TestLevel() { Id = new TestLevelId(2), SampleNumber = 10 },
                        ParameterName = nameof(TestLevelSet.TestLevel3)
                    }
                });
        }
    }
}
