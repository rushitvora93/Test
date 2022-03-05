using System;
using System.Collections.Generic;
using Core.Entities;
using Core.PhysicalValueTypes;
using NUnit.Framework;
using TestHelper.Checker;
using TestHelper.Factories;

namespace Core.Test.Entities
{
    class LocationToolAssignmentTest
    {
        [Test]
        public void EqualsByIdWithDifferentIdsMeansInequality()
        {
            var left = CreateLocationToolAssignment.IdOnly(97486153);
            var right = CreateLocationToolAssignment.IdOnly(147896325);

            Assert.IsFalse(left.EqualsById(right));
        }

        [Test]
        public void EqualsByIdWithNullMeansInequality()
        {
            var left = CreateLocationToolAssignment.IdOnly(97486153);

            Assert.IsFalse(left.EqualsById(null));
        }

        [Test]
        public void EqualsByIdWithEqualIdsMeansEquality()
        {
            var left = CreateLocationToolAssignment.IdOnly(97486153);
            var right = CreateLocationToolAssignment.IdOnly(97486153);

            Assert.IsTrue(left.EqualsById(right));
        }

        [TestCaseSource(nameof(EqualsByContentTestSource))]
        public void EqualsByContentWithDifferentParameterMeansInequality((EqualityParameter<LocationToolAssignment> parameter, EqualityTestHelper<LocationToolAssignment> helper) helperTuple)
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

        [Test]
        public void GetTestLevelMfuReturnsTestLevel2()
        {
            var testLevel = new TestLevel() { IsActive = true };
            var locTool = new LocationToolAssignment()
            {
                TestLevelNumberMfu = 2,
                TestLevelSetMfu = new TestLevelSet() { TestLevel2 = testLevel }
            };
            Assert.AreSame(testLevel, locTool.GetTestLevel(Enums.TestType.Mfu));
        }

        [Test]
        public void GetTestLevelMfuReturnsTestLevel3()
        {
            var testLevel = new TestLevel() { IsActive = true };
            var locTool = new LocationToolAssignment()
            {
                TestLevelNumberMfu = 3,
                TestLevelSetMfu = new TestLevelSet() { TestLevel3 = testLevel }
            };
            Assert.AreSame(testLevel, locTool.GetTestLevel(Enums.TestType.Mfu));
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void GetTestLevelMfuReturnsTestLevel1InAllOtherCases(int testLevelNumber)
        {
            var testLevel = new TestLevel() { IsActive = true };
            var locTool = new LocationToolAssignment()
            {
                TestLevelNumberMfu = testLevelNumber,
                TestLevelSetMfu = new TestLevelSet() 
                { 
                    TestLevel1 = testLevel,
                    TestLevel2 = new TestLevel(),
                    TestLevel3 = new TestLevel()
                }
            };
            Assert.AreSame(testLevel, locTool.GetTestLevel(Enums.TestType.Mfu));
        }

        [Test]
        public void GetTestLevelChkReturnsTestLevel2()
        {
            var testLevel = new TestLevel() { IsActive = true };
            var locTool = new LocationToolAssignment()
            {
                TestLevelNumberChk = 2,
                TestLevelSetChk = new TestLevelSet() { TestLevel2 = testLevel }
            };
            Assert.AreSame(testLevel, locTool.GetTestLevel(Enums.TestType.Chk));
        }

        [Test]
        public void GetTestLevelChkReturnsTestLevel3()
        {
            var testLevel = new TestLevel() { IsActive = true };
            var locTool = new LocationToolAssignment()
            {
                TestLevelNumberChk = 3,
                TestLevelSetChk = new TestLevelSet() { TestLevel3 = testLevel }
            };
            Assert.AreSame(testLevel, locTool.GetTestLevel(Enums.TestType.Chk));
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void GetTestLevelChkReturnsTestLevel1InAllOtherCases(int testLevelNumber)
        {
            var testLevel = new TestLevel() { IsActive = true };
            var locTool = new LocationToolAssignment()
            {
                TestLevelNumberChk = testLevelNumber,
                TestLevelSetChk = new TestLevelSet()
                {
                    TestLevel1 = testLevel,
                    TestLevel2 = new TestLevel(),
                    TestLevel3 = new TestLevel()
                }
            };
            Assert.AreSame(testLevel, locTool.GetTestLevel(Enums.TestType.Chk));
        }



        private static IEnumerable<(EqualityParameter<LocationToolAssignment>, EqualityTestHelper<LocationToolAssignment>)> EqualsByContentTestSource()
        {
            var helper = GetEqualsByContentTestHelper();

            foreach (var param in helper.EqualityParameterList)
            {
                yield return (param, helper);
            }
        }

        private static EqualityTestHelper<LocationToolAssignment> GetEqualsByContentTestHelper()
        {
            return new EqualityTestHelper<LocationToolAssignment>(
            (left, right) => left.EqualsByContent(right),
            () => new LocationToolAssignment(),
            new List<EqualityParameter<LocationToolAssignment>>()
            {
                new EqualityParameter<LocationToolAssignment>()
                {
                    SetParameter = (entity, value) => entity.Id = (LocationToolAssignmentId)value,
                    CreateParameterValue = () => new LocationToolAssignmentId(8520),
                    CreateOtherParameterValue = () => new LocationToolAssignmentId(7895453),
                    ParameterName = nameof(LocationToolAssignment.Id)
                },
                new EqualityParameter<LocationToolAssignment>()
                {
                    SetParameter = (entity, value) => entity.AssignedLocation = (Location)value,
                    CreateParameterValue = () => CreateLocation.Anonymous(),
                    CreateOtherParameterValue = () => CreateLocation.IdOnly(78451),
                    ParameterName = nameof(LocationToolAssignment.AssignedLocation)
                },
                new EqualityParameter<LocationToolAssignment>()
                {
                    SetParameter = (entity, value) => entity.AssignedTool = (Tool)value,
                    CreateParameterValue = () => CreateTool.Anonymous(),
                    CreateOtherParameterValue = () => CreateTool.WithId(78451),
                    ParameterName = nameof(LocationToolAssignment.AssignedTool)
                },
                new EqualityParameter<LocationToolAssignment>()
                {
                    SetParameter = (entity, value) => entity.ToolUsage = (ToolUsage)value,
                    CreateParameterValue = () => new ToolUsage(){ListId = new HelperTableEntityId(789), Value = new ToolUsageDescription("qwertzuiopü")},
                    CreateOtherParameterValue = () => new ToolUsage(){ListId = new HelperTableEntityId(456), Value = new ToolUsageDescription("asdfghjklöä")},
                    ParameterName = nameof(LocationToolAssignment.ToolUsage)
                },
                new EqualityParameter<LocationToolAssignment>()
                {
                    SetParameter = (entity, value) => entity.TestParameters = (Core.Entities.TestParameters)value,
                    CreateParameterValue = () => new Core.Entities.TestParameters(){MaximumAngle = Angle.FromDegree(7847512), MaximumTorque =Torque.FromNm(3474554)},
                    CreateOtherParameterValue = () => new Core.Entities.TestParameters(){MinimumTorque = Torque.FromNm(7847512), MinimumAngle = Angle.FromDegree(3474554)},
                    ParameterName = nameof(LocationToolAssignment.TestParameters)
                },
                new EqualityParameter<LocationToolAssignment>()
                {
                    SetParameter = (entity, value) => entity.TestTechnique = (TestTechnique)value,
                    CreateParameterValue= () => new TestTechnique() {CycleStart = 984765, CycleComplete = 946785132, StartFinalAngle = 8645, MaximumPulse = 5412, MeasureDelayTime = 86451},
                    CreateOtherParameterValue  = () => new TestTechnique() {EndCycleTime = 23456, SlipTorque = 987423, MinimumPulse = 345678, ResetTime = 7654, MustTorqueAndAngleBeInLimits = true, FilterFrequency = 584, TorqueCoefficient = 634556},
                    ParameterName = nameof(LocationToolAssignment.TestTechnique)
                },
                new EqualityParameter<LocationToolAssignment>()
                {
                    SetParameter = (entity, value) => entity.StartDateMfu = (DateTime)value,
                    CreateParameterValue = () => new DateTime(3216, 11, 8),
                    CreateOtherParameterValue = () => new DateTime(2021, 3, 29),
                    ParameterName = nameof(LocationToolAssignment.StartDateMfu)
                },
                new EqualityParameter<LocationToolAssignment>()
                {
                    SetParameter = (entity, value) => entity.StartDateChk = (DateTime)value,
                    CreateParameterValue = () => new DateTime(3216, 11, 8),
                    CreateOtherParameterValue = () => new DateTime(2021, 3, 29),
                    ParameterName = nameof(LocationToolAssignment.StartDateChk)
                },
                new EqualityParameter<LocationToolAssignment>()
                {
                    SetParameter = (entity, value) => entity.TestOperationActiveMfu = (bool)value,
                    CreateParameterValue = () => true,
                    CreateOtherParameterValue = () => false,
                    ParameterName = nameof(LocationToolAssignment.TestOperationActiveMfu)
                },
                new EqualityParameter<LocationToolAssignment>()
                {
                    SetParameter = (entity, value) => entity.TestOperationActiveChk = (bool)value,
                    CreateParameterValue = () => true,
                    CreateOtherParameterValue = () => false,
                    ParameterName = nameof(LocationToolAssignment.TestOperationActiveChk)
                }
            });
        }
    }
}
