using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Enums;
using NUnit.Framework;
using TestHelper.Checker;

namespace Core.Test.Entities
{
    public class TestPlanLocationToolAssignmentTest
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
        public void EqualsByIdWithDifferentParameterMeansInequality((EqualityParameter<TestPlanLocationToolAssignment> parameter, EqualityTestHelper<TestPlanLocationToolAssignment> helper) helperTuple)
        {
            helperTuple.helper.CheckInequalityForParameter(helperTuple.parameter);
        }


        [TestCaseSource(nameof(EqualsByContentTestSource))]
        public void EqualsByContentWithDifferentParameterMeansInequality((EqualityParameter<TestPlanLocationToolAssignment> parameter, EqualityTestHelper<TestPlanLocationToolAssignment> helper) helperTuple)
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



        private static IEnumerable<(EqualityParameter<TestPlanLocationToolAssignment>, EqualityTestHelper<TestPlanLocationToolAssignment>)> EqualsByIdTestSource()
        {
            var helper = GetEqualsByIdTestHelper();

            foreach (var param in helper.EqualityParameterList)
            {
                yield return (param, helper);
            }
        }

        private static EqualityTestHelper<TestPlanLocationToolAssignment> GetEqualsByIdTestHelper()
        {
            return new EqualityTestHelper<TestPlanLocationToolAssignment>(
                (left, right) => left.EqualsById(right),
                () => new TestPlanLocationToolAssignment(),
                new List<EqualityParameter<TestPlanLocationToolAssignment>>()
                {
                    new EqualityParameter<TestPlanLocationToolAssignment>()
                    {
                        SetParameter = (entity, value) => entity.Id = value as TestPlanLocationToolAssignmentId,
                        CreateParameterValue = () => new TestPlanLocationToolAssignmentId(8520),
                        CreateOtherParameterValue = () => new TestPlanLocationToolAssignmentId(7895453)
                    }
                });
        }

        private static IEnumerable<(EqualityParameter<TestPlanLocationToolAssignment>, EqualityTestHelper<TestPlanLocationToolAssignment>)> EqualsByContentTestSource()
        {
            var helper = GetEqualsByContentTestHelper();

            foreach (var param in helper.EqualityParameterList)
            {
                yield return (param, helper);
            }
        }

        private static EqualityTestHelper<TestPlanLocationToolAssignment> GetEqualsByContentTestHelper()
        {
            return new EqualityTestHelper<TestPlanLocationToolAssignment>(
                (left, right) => left.EqualsByContent(right),
                () => new TestPlanLocationToolAssignment(),
                new List<EqualityParameter<TestPlanLocationToolAssignment>>()
                {
                    new EqualityParameter<TestPlanLocationToolAssignment>()
                    {
                        SetParameter = (entity, value) => entity.Id = value as TestPlanLocationToolAssignmentId,
                        CreateParameterValue = () => new TestPlanLocationToolAssignmentId(8520),
                        CreateOtherParameterValue = () => new TestPlanLocationToolAssignmentId(7895453),
                        ParameterName = nameof(TestPlanLocationToolAssignment.Id)
                    },
                    new EqualityParameter<TestPlanLocationToolAssignment>()
                    {
                        SetParameter = (entity, value) => entity.LocationToolAssignment = value as LocationToolAssignment,
                        CreateParameterValue = () => new LocationToolAssignment() { Id = new LocationToolAssignmentId(1) } ,
                        CreateOtherParameterValue = () => new LocationToolAssignment() { Id = new LocationToolAssignmentId(2) } ,
                        ParameterName = nameof(TestPlanLocationToolAssignment.LocationToolAssignment)
                    },
                    new EqualityParameter<TestPlanLocationToolAssignment>()
                    {
                        SetParameter = (entity, value) => entity.TestPlans = value as List<TestPlan>,
                        CreateParameterValue = () => new List<TestPlan>() { new TestPlan() { Id = new TestPlanId(1) } },
                        CreateOtherParameterValue = () => new List<TestPlan>() { new TestPlan() { Id = new TestPlanId(2) } },
                        ParameterName = nameof(TestPlanLocationToolAssignment.TestPlans)
                    },
                    new EqualityParameter<TestPlanLocationToolAssignment>()
                    {
                        SetParameter = (entity, value) => entity.IsActive = (bool)value,
                        CreateParameterValue = () => true,
                        CreateOtherParameterValue = () => false,
                        ParameterName = nameof(TestPlanLocationToolAssignment.IsActive)
                    },
                    new EqualityParameter<TestPlanLocationToolAssignment>()
                    {
                        SetParameter = (entity, value) => entity.TestType = (TestType)value,
                        CreateParameterValue = () => TestType.Chk,
                        CreateOtherParameterValue = () => TestType.Mfu,
                        ParameterName = nameof(TestPlanLocationToolAssignment.TestType)
                    },
                    new EqualityParameter<TestPlanLocationToolAssignment>()
                    {
                        SetParameter = (entity, value) => entity.TestPeriodStartDate = (DateTime)value,
                        CreateParameterValue = () => DateTime.Today,
                        CreateOtherParameterValue = () => DateTime.Today.AddDays(1),
                        ParameterName = nameof(TestPlanLocationToolAssignment.TestPeriodStartDate)
                    },
                    new EqualityParameter<TestPlanLocationToolAssignment>()
                    {
                        SetParameter = (entity, value) => entity.TestPeriodStartShift = (Shift)value,
                        CreateParameterValue = () => Shift.SecondShiftOfDay,
                        CreateOtherParameterValue = () => Shift.ThirdShiftOfDay,
                        ParameterName = nameof(TestPlanLocationToolAssignment.TestPeriodStartShift)
                    }
                });
        }
    }
}
