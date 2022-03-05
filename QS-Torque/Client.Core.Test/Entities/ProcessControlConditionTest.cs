using System;
using System.Collections.Generic;
using Client.Core.Entities;
using Client.TestHelper.Factories;
using Core.Entities;
using Core.PhysicalValueTypes;
using NUnit.Framework;
using TestHelper.Checker;

namespace Client.Core.Test.Entities
{
    class ProcessControlConditionTest
    {
        [TestCaseSource(nameof(EqualsByContentTestSource))]
        public void EqualsByContentWithDifferentParameterMeansInequality((EqualityParameter<ProcessControlCondition> parameter, EqualityTestHelper<ProcessControlCondition> helper) helperTuple)
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
        public void SameContentMeansEquality()
        {
            var processControlCondition = CreateProcessControlCondition.Randomized(4354);
            var lhs = processControlCondition.CopyDeep();
            var rhs = processControlCondition.CopyDeep();
            Assert.IsTrue(lhs.EqualsByContent(rhs));
        }

        [Test]
        public void EqualsWithRightIsNullMeansInequality()
        {
            var processControlCondition = CreateProcessControlCondition.Randomized(4353);
            Assert.IsFalse(processControlCondition.EqualsByContent(null));
        }

        [Test]
        public void CopyingTestEquipmentIsEqualButNotSame()
        {
            var condition = CreateProcessControlCondition.Randomized(678678);
            var result = condition.CopyDeep();
            Assert.AreNotSame(condition, result);
            Assert.IsTrue(result.EqualsByContent(condition));
        }

        [TestCase(3, 2, true)]
        [TestCase(7.8, 2.3, true)]
        [TestCase(5, 8, false)]
        [TestCase(6, 6, true)]
        [TestCase(-1, 6, true)]
        [TestCase(10000, 200000, true)]
        public void ValidateReturnsLowerMeasuringLimitGreaterThanOrEqualToUpperMeasuringLimitOrNotBetween0And9999(double min, double max, bool result)
        {
            var entity = new ProcessControlCondition()
            {
                LowerMeasuringLimit = Torque.FromNm(min),
                UpperMeasuringLimit = Torque.FromNm(max)
            };
            Assert.AreEqual(result, entity.Validate(nameof(ProcessControlCondition.LowerMeasuringLimit)) == ProcessControlValidationError.LowerMeasuringLimitGreaterThanOrEqualToUpperMeasuringLimitOrNotBetween0And9999);
        }

        [TestCase(3, 2, true)]
        [TestCase(7.8, 2.3, true)]
        [TestCase(5, 8, false)]
        [TestCase(6, 6, true)]
        [TestCase(-5, -3, true)]
        [TestCase(3, 200000, true)]
        public void ValidateReturnsUpperMeasuringLimitLessThanOrEqualToLowerMeasuringLimitOrNotBetween0And9999(double min, double max, bool result)
        {
            var entity = new ProcessControlCondition()
            {
                LowerMeasuringLimit = Torque.FromNm(min),
                UpperMeasuringLimit = Torque.FromNm(max)
            };
            Assert.AreEqual(result, entity.Validate(nameof(ProcessControlCondition.UpperMeasuringLimit)) == ProcessControlValidationError.UpperMeasuringLimitLessThanOrEqualToLowerMeasuringLimitOrNotBetween0And9999);
        }

        [TestCase(3, 2, true)]
        [TestCase(7.8, 2.3, true)]
        [TestCase(5, 8, false)]
        [TestCase(6, 6, false)]
        [TestCase(-1, 6, true)]
        [TestCase(10000, 200000, true)]
        public void ValidateReturnsLowerInterventionLimitGreaterThanUpperMeasuringLimitOrNotBetween0And9999(double min, double max, bool result)
        {
            var entity = new ProcessControlCondition()
            {
                LowerInterventionLimit = Torque.FromNm(min),
                UpperInterventionLimit = Torque.FromNm(max)
            };
            Assert.AreEqual(result, entity.Validate(nameof(ProcessControlCondition.LowerInterventionLimit)) == ProcessControlValidationError.LowerInterventionLimitGreaterThanUpperMeasuringLimitOrNotBetween0And9999);
        }

        [TestCase(3, 2, true)]
        [TestCase(7.8, 2.3, true)]
        [TestCase(5, 8, false)]
        [TestCase(6, 6, false)]
        [TestCase(-5, -3, true)]
        [TestCase(3, 200000, true)]
        public void ValidateReturnsUpperInterventionLimitLessThanLowerInterventionLimitOrNotBetween0And9999(double min, double max, bool result)
        {
            var entity = new ProcessControlCondition()
            {
                LowerInterventionLimit = Torque.FromNm(min),
                UpperInterventionLimit = Torque.FromNm(max)
            };
            Assert.AreEqual(result, entity.Validate(nameof(ProcessControlCondition.UpperInterventionLimit)) == ProcessControlValidationError.UpperInterventionLimitLessThanLowerInterventionLimitOrNotBetween0And9999);
        }

        private static IEnumerable<(EqualityParameter<ProcessControlCondition>, EqualityTestHelper<ProcessControlCondition>)> EqualsByContentTestSource()
        {
            var helper = GetEqualsByContentTestHelper();

            foreach (var param in helper.EqualityParameterList)
            {
                yield return (param, helper);
            }
        }

        private static EqualityTestHelper<ProcessControlCondition> GetEqualsByContentTestHelper()
        {
            return new EqualityTestHelper<ProcessControlCondition>(
                (left, right) => left.EqualsByContent(right),
                () => new ProcessControlCondition(),
                new List<EqualityParameter<ProcessControlCondition>>()
                {
                    new EqualityParameter<ProcessControlCondition>()
                    {
                        SetParameter = (entity, value) => entity.Id = (ProcessControlConditionId)value,
                        CreateParameterValue = () => new ProcessControlConditionId(1),
                        CreateOtherParameterValue = () => new ProcessControlConditionId(2),
                        ParameterName = nameof(ProcessControlCondition.Id)
                    },
                    new EqualityParameter<ProcessControlCondition>()
                    {
                        SetParameter = (entity, value) => entity.Location = (Location)value,
                        CreateParameterValue = () => new Location() { Id = new LocationId(1) },
                        CreateOtherParameterValue = () => new Location() { Id = new LocationId(2) },
                        ParameterName = nameof(ProcessControlCondition.Location)
                    },
                    new EqualityParameter<ProcessControlCondition>()
                    {
                        SetParameter = (entity, value) => entity.UpperMeasuringLimit = (Torque)value,
                        CreateParameterValue = () => Torque.FromNm(34),
                        CreateOtherParameterValue = () => Torque.FromNm(67),
                        ParameterName = nameof(ProcessControlCondition.UpperMeasuringLimit)
                    },
                    new EqualityParameter<ProcessControlCondition>()
                    {
                        SetParameter = (entity, value) => entity.LowerMeasuringLimit = (Torque)value,
                        CreateParameterValue = () => Torque.FromNm(3),
                        CreateOtherParameterValue = () => Torque.FromNm(56),
                        ParameterName = nameof(ProcessControlCondition.LowerMeasuringLimit)
                    },
                    new EqualityParameter<ProcessControlCondition>()
                    {
                        SetParameter = (entity, value) => entity.UpperInterventionLimit = (Torque)value,
                        CreateParameterValue = () => Torque.FromNm(7),
                        CreateOtherParameterValue = () => Torque.FromNm(87),
                        ParameterName = nameof(ProcessControlCondition.UpperInterventionLimit)
                    },
                    new EqualityParameter<ProcessControlCondition>()
                    {
                        SetParameter = (entity, value) => entity.LowerInterventionLimit = (Torque)value,
                        CreateParameterValue = () => Torque.FromNm(4),
                        CreateOtherParameterValue = () => Torque.FromNm(7),
                        ParameterName = nameof(ProcessControlCondition.LowerInterventionLimit)
                    },
                    new EqualityParameter<ProcessControlCondition>()
                    {
                        SetParameter = (entity, value) => entity.TestLevelSet = (TestLevelSet)value,
                        CreateParameterValue = () => new TestLevelSet(){Id = new TestLevelSetId(1), Name = new TestLevelSetName("a")},
                        CreateOtherParameterValue = () => new TestLevelSet(){Id = new TestLevelSetId(14), Name = new TestLevelSetName("b")},
                        ParameterName = nameof(ProcessControlCondition.TestLevelSet)
                    },
                    new EqualityParameter<ProcessControlCondition>()
                    {
                        SetParameter = (entity, value) => entity.TestLevelNumber = (int)value,
                        CreateParameterValue = () => 1,
                        CreateOtherParameterValue = () => 2,
                        ParameterName = nameof(ProcessControlCondition.TestLevelNumber)
                    },
                    new EqualityParameter<ProcessControlCondition>()
                    {
                        SetParameter = (entity, value) => entity.StartDate = (DateTime?)value,
                        CreateParameterValue = () => new DateTime(2021,3,4),
                        CreateOtherParameterValue = () => new DateTime(2020,1,6),
                        ParameterName = nameof(ProcessControlCondition.StartDate)
                    },
                    new EqualityParameter<ProcessControlCondition>()
                    {
                        SetParameter = (entity, value) => entity.TestOperationActive = (bool)value,
                        CreateParameterValue = () => true,
                        CreateOtherParameterValue = () => false,
                        ParameterName = nameof(ProcessControlCondition.TestOperationActive)
                    },
                    new EqualityParameter<ProcessControlCondition>()
                    {
                        SetParameter = (entity, value) => entity.ProcessControlTech = (QstProcessControlTech)value,
                        CreateParameterValue = () => CreateQstProcessControlTech.Randomized(4356466),
                        CreateOtherParameterValue = () => CreateQstProcessControlTech.Randomized(23456),
                        ParameterName = nameof(ProcessControlCondition.ProcessControlTech)
                    }
                });
        }
    }
}
