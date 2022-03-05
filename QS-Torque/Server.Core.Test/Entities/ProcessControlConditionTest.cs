using System;
using System.Collections.Generic;
using NUnit.Framework;
using Server.Core.Entities;
using Server.TestHelper.Factories;
using TestHelper.Checker;

namespace Server.Core.Test.Entities
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
            var equipment = CreateProcessControlCondition.Randomized(678678);
            var result = equipment.CopyDeep();
            Assert.AreNotSame(equipment, result);
            Assert.IsTrue(result.EqualsByContent(equipment));
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
                        SetParameter = (entity, value) => entity.UpperMeasuringLimit = (double)value,
                        CreateParameterValue = () => 34.0,
                        CreateOtherParameterValue = () => 67.0,
                        ParameterName = nameof(ProcessControlCondition.UpperMeasuringLimit)
                    },
                    new EqualityParameter<ProcessControlCondition>()
                    {
                        SetParameter = (entity, value) => entity.LowerMeasuringLimit = (double)value,
                        CreateParameterValue = () => 4.0,
                        CreateOtherParameterValue = () => 23.0,
                        ParameterName = nameof(ProcessControlCondition.LowerMeasuringLimit)
                    },
                    new EqualityParameter<ProcessControlCondition>()
                    {
                        SetParameter = (entity, value) => entity.UpperInterventionLimit = (double)value,
                        CreateParameterValue = () => 7.0,
                        CreateOtherParameterValue = () => 87.0,
                        ParameterName = nameof(ProcessControlCondition.UpperInterventionLimit)
                    },
                    new EqualityParameter<ProcessControlCondition>()
                    {
                        SetParameter = (entity, value) => entity.LowerInterventionLimit = (double)value,
                        CreateParameterValue = () => 4.0,
                        CreateOtherParameterValue = () => 7.0,
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
                        SetParameter = (entity, value) => entity.ProcessControlTech = (ProcessControlTech)value,
                        CreateParameterValue = () => CreateQstProcessControlTech.Randomized(4356466),
                        CreateOtherParameterValue = () => CreateQstProcessControlTech.Randomized(23456),
                        ParameterName = nameof(ProcessControlCondition.ProcessControlTech)
                    }
                });
        }
    }
}
