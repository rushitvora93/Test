using System;
using System.Collections.Generic;
using Common.Types.Enums;
using Core.Entities;
using Core.Enums;
using NUnit.Framework;
using Server.Core.Entities;
using Server.TestHelper.Factories;
using TestHelper.Checker;

namespace Server.Core.Test.Entities
{
    class TestEquipmentTest
    {
        [TestCaseSource(nameof(EqualsByContentTestSource))]
        public void EqualsByContentWithDifferentParameterMeansInequality((EqualityParameter<TestEquipment> parameter, EqualityTestHelper<TestEquipment> helper) helperTuple)
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
            var lhs = CreateTestEquipment.Anonymous();
            var rhs = CreateTestEquipment.Anonymous();
            Assert.IsTrue(lhs.EqualsByContent(rhs));
        }

        [Test]
        public void EqualsWithRightIsNullMeansInequality()
        {
            var testEquipment = CreateTestEquipment.Anonymous();
            Assert.IsFalse(testEquipment.EqualsByContent(null));
        }

        [Test]
        public void TestEquipmentModelEqualsCalledWhenCompareTestEquipment()
        {
            var modelMock = new TestEquipmentModelMock();
            var lhs = CreateTestEquipment.Randomized(12324);
            var rhs = lhs.CopyDeep();
            lhs.TestEquipmentModel = modelMock;
            rhs.TestEquipmentModel = modelMock;
            _ = lhs.EqualsByContent(rhs);
            Assert.IsTrue(modelMock.TestEquipmentModelEqualsByContentCalled);
        }

        [Test]
        public void CopyingTestEquipmentIsEqualButNotSame()
        {
            var equipment = CreateTestEquipment.Randomized(23432536);
            var result = equipment.CopyDeep();
            Assert.AreNotSame(equipment, result);
            Assert.IsTrue(result.EqualsByContent(equipment));
        }


        private static IEnumerable<(EqualityParameter<TestEquipment>, EqualityTestHelper<TestEquipment>)> EqualsByContentTestSource()
        {
            var helper = GetEqualsByContentTestHelper();

            foreach (var param in helper.EqualityParameterList)
            {
                yield return (param, helper);
            }
        }

        private static EqualityTestHelper<TestEquipment> GetEqualsByContentTestHelper()
        {
            return new EqualityTestHelper<TestEquipment>(
                (left, right) => left.EqualsByContent(right),
                () => new TestEquipment(),
                new List<EqualityParameter<TestEquipment>>()
                {
                    new EqualityParameter<TestEquipment>()
                    {
                        SetParameter = (entity, value) => entity.Id = (TestEquipmentId)value,
                        CreateParameterValue = () => new TestEquipmentId(1),
                        CreateOtherParameterValue = () => new TestEquipmentId(2),
                        ParameterName = nameof(TestEquipment.Id)
                    },
                    new EqualityParameter<TestEquipment>()
                    {
                        SetParameter = (entity, value) => entity.SerialNumber = (TestEquipmentSerialNumber)value,
                        CreateParameterValue = () => new TestEquipmentSerialNumber("1"),
                        CreateOtherParameterValue = () => new TestEquipmentSerialNumber("2"),
                        ParameterName = nameof(TestEquipment.SerialNumber)
                    },
                    new EqualityParameter<TestEquipment>()
                    {
                        SetParameter = (entity, value) => entity.InventoryNumber = (TestEquipmentInventoryNumber)value,
                        CreateParameterValue = () => new TestEquipmentInventoryNumber("1"),
                        CreateOtherParameterValue = () => new TestEquipmentInventoryNumber("2"),
                        ParameterName = nameof(TestEquipment.InventoryNumber)
                    },
                    new EqualityParameter<TestEquipment>()
                    {
                        SetParameter = (entity, value) => entity.TransferUser = (bool)value,
                        CreateParameterValue = () => true,
                        CreateOtherParameterValue = () => false,
                        ParameterName = nameof(TestEquipment.TransferUser)
                    },
                    new EqualityParameter<TestEquipment>()
                    {
                        SetParameter = (entity, value) => entity.TransferAdapter = (bool)value,
                        CreateParameterValue = () => true,
                        CreateOtherParameterValue = () => false,
                        ParameterName = nameof(TestEquipment.TransferAdapter)
                    },
                    new EqualityParameter<TestEquipment>()
                    {
                        SetParameter = (entity, value) => entity.TransferTransducer = (bool)value,
                        CreateParameterValue = () => true,
                        CreateOtherParameterValue = () => false,
                        ParameterName = nameof(TestEquipment.TransferTransducer)
                    },
                    new EqualityParameter<TestEquipment>()
                    {
                        SetParameter = (entity, value) => entity.AskForSign = (bool)value,
                        CreateParameterValue = () => true,
                        CreateOtherParameterValue = () => false,
                        ParameterName = nameof(TestEquipment.AskForSign)
                    },
                    new EqualityParameter<TestEquipment>()
                    {
                        SetParameter = (entity, value) => entity.AskForIdent = (TestEquipmentBehaviourAskForIdent)value,
                        CreateParameterValue = () => TestEquipmentBehaviourAskForIdent.Never,
                        CreateOtherParameterValue = () => TestEquipmentBehaviourAskForIdent.PerTest,
                        ParameterName = nameof(TestEquipment.AskForIdent)
                    },
                    new EqualityParameter<TestEquipment>()
                    {
                        SetParameter = (entity, value) => entity.TransferCurves = (TestEquipmentBehaviourTransferCurves)value,
                        CreateParameterValue = () => TestEquipmentBehaviourTransferCurves.Always,
                        CreateOtherParameterValue = () => TestEquipmentBehaviourTransferCurves.OnlyNio,
                        ParameterName = nameof(TestEquipment.TransferCurves)
                    },
                    new EqualityParameter<TestEquipment>()
                    {
                        SetParameter = (entity, value) => entity.UseErrorCodes = (bool)value,
                        CreateParameterValue = () => true,
                        CreateOtherParameterValue = () => false,
                        ParameterName = nameof(TestEquipment.UseErrorCodes)
                    },
                    new EqualityParameter<TestEquipment>()
                    {
                        SetParameter = (entity, value) => entity.DoLoseCheck = (bool)value,
                        CreateParameterValue = () => true,
                        CreateOtherParameterValue = () => false,
                        ParameterName = nameof(TestEquipment.DoLoseCheck)
                    },
                    new EqualityParameter<TestEquipment>()
                    {
                        SetParameter = (entity, value) => entity.CanDeleteMeasurements = (bool)value,
                        CreateParameterValue = () => true,
                        CreateOtherParameterValue = () => false,
                        ParameterName = nameof(TestEquipment.CanDeleteMeasurements)
                    },
                    new EqualityParameter<TestEquipment>()
                    {
                        SetParameter = (entity, value) => entity.ConfirmMeasurements = (TestEquipmentBehaviourConfirmMeasurements)value,
                        CreateParameterValue = () => TestEquipmentBehaviourConfirmMeasurements.Never,
                        CreateOtherParameterValue = () => TestEquipmentBehaviourConfirmMeasurements.Always,
                        ParameterName = nameof(TestEquipment.ConfirmMeasurements)
                    },
                    new EqualityParameter<TestEquipment>()
                    {
                        SetParameter = (entity, value) => entity.TransferLocationPictures = (bool)value,
                        CreateParameterValue = () => true,
                        CreateOtherParameterValue = () => false,
                        ParameterName = nameof(TestEquipment.TransferLocationPictures)
                    },
                    new EqualityParameter<TestEquipment>()
                    {
                        SetParameter = (entity, value) => entity.TransferNewLimits = (bool)value,
                        CreateParameterValue = () => true,
                        CreateOtherParameterValue = () => false,
                        ParameterName = nameof(TestEquipment.TransferNewLimits)
                    },
                    new EqualityParameter<TestEquipment>()
                    {
                        SetParameter = (entity, value) => entity.TransferAttributes = (bool)value,
                        CreateParameterValue = () => true,
                        CreateOtherParameterValue = () => false,
                        ParameterName = nameof(TestEquipment.TransferAttributes)
                    },
                    new EqualityParameter<TestEquipment>()
                    {
                        SetParameter = (entity, value) => entity.UseForCtl = (bool)value,
                        CreateParameterValue = () => true,
                        CreateOtherParameterValue = () => false,
                        ParameterName = nameof(TestEquipment.UseForCtl)
                    },
                    new EqualityParameter<TestEquipment>()
                    {
                        SetParameter = (entity, value) => entity.UseForRot = (bool)value,
                        CreateParameterValue = () => true,
                        CreateOtherParameterValue = () => false,
                        ParameterName = nameof(TestEquipment.UseForRot)
                    },
                    new EqualityParameter<TestEquipment>()
                    {
                        SetParameter = (entity, value) => entity.CanUseQstStandard = (bool)value,
                        CreateParameterValue = () => true,
                        CreateOtherParameterValue = () => false,
                        ParameterName = nameof(TestEquipment.CanUseQstStandard)
                    },
                    new EqualityParameter<TestEquipment>()
                    {
                        SetParameter = (entity, value) => entity.Version = (TestEquipmentVersion)value,
                        CreateParameterValue = () => new TestEquipmentVersion("365"),
                        CreateOtherParameterValue = () => new TestEquipmentVersion("567"),
                        ParameterName = nameof(TestEquipment.Version)
                    },
                    new EqualityParameter<TestEquipment>()
                    {
                        SetParameter = (entity, value) => entity.CapacityMax = (double)value,
                        CreateParameterValue = () => 1.0,
                        CreateOtherParameterValue = () => 4.0,
                        ParameterName = nameof(TestEquipment.CapacityMax)
                    },
                    new EqualityParameter<TestEquipment>()
                    {
                        SetParameter = (entity, value) => entity.CapacityMin = (double)value,
                        CreateParameterValue = () => 5.8,
                        CreateOtherParameterValue = () => 8.6,
                        ParameterName = nameof(TestEquipment.CapacityMin)
                    },
                    new EqualityParameter<TestEquipment>()
                    {
                        SetParameter = (entity, value) => entity.LastCalibration = (DateTime?)value,
                        CreateParameterValue = () => new DateTime(2020,1,2,3,4,5,6),
                        CreateOtherParameterValue = () =>  new DateTime(2021,2,4,3,6,8,6),
                        ParameterName = nameof(TestEquipment.LastCalibration)
                    },
                    new EqualityParameter<TestEquipment>()
                    {
                        SetParameter = (entity, value) => entity.Status = (Status)value,
                        CreateParameterValue = () => new Status(){Id = new StatusId(1), Value = new StatusDescription("3")},
                        CreateOtherParameterValue = () => new Status(){Id = new StatusId(7), Value = new StatusDescription("37")},
                        ParameterName = nameof(TestEquipment.Status)
                    },
                    new EqualityParameter<TestEquipment>()
                    {
                        SetParameter = (entity, value) => entity.CalibrationInterval = (Interval)value,
                        CreateParameterValue = () => new Interval(){Type = IntervalType.EveryXDays, IntervalValue = 4},
                        CreateOtherParameterValue = () => new Interval(){Type = IntervalType.EveryXDays, IntervalValue = 7},
                        ParameterName = nameof(TestEquipment.CalibrationInterval)
                    },
                    new EqualityParameter<TestEquipment>()
                    {
                        SetParameter = (entity, value) => entity.CalibrationNorm = (CalibrationNorm)value,
                        CreateParameterValue = () => new CalibrationNorm("365"),
                        CreateOtherParameterValue = () => new CalibrationNorm("567"),
                        ParameterName = nameof(TestEquipment.CalibrationNorm)
                    }
                });
        }

        private class TestEquipmentModelMock : TestEquipmentModel
        {
            public bool TestEquipmentModelEqualsByContentCalled;
            public override bool EqualsByContent(TestEquipmentModel other)
            {
                TestEquipmentModelEqualsByContentCalled = true;
                return true;
            }
        }
    }
}
