using System;
using System.Collections.Generic;
using Common.Types.Enums;
using Core.Entities;
using Core.Enums;
using Core.PhysicalValueTypes;
using NUnit.Framework;
using TestHelper.Checker;
using TestHelper.Factories;
using TestHelper.Mock;

namespace Core.Test.Entities
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

        [TestCase("Path")]
        [TestCase("ImNotAtTheBeachThisIsABathtub")]
        public void GettingStatusFileGivesPathFromTestEquipmentModel(string expectedPath)
        {
            var equipment = CreateTestEquipment.WithModelStatusPath(expectedPath);
            Assert.AreEqual(expectedPath, equipment.StatusFilePath());
        }

        [TestCase(@"C:\Why\Are\Paths\So\Shitty\To\Test")]
        [TestCase("NoBodyOfWaterIsSafeWithoutALifeguard")]
        public void GettingResultFileGivesPathFromTestEquipmentModel(string expectedPath)
        {
            var equipment = CreateTestEquipment.WithModelResultPath(expectedPath);
            Assert.AreEqual(expectedPath, equipment.ResultFilePath());
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
            var lhs = CreateTestEquipment.WithModel(modelMock);
            var rhs = CreateTestEquipment.WithModel(new TestEquipmentModelMock());
            _ = lhs.EqualsByContent(rhs);
            Assert.IsTrue(modelMock.TestEquipmentModelEqualsByContentCalled);
        }

        [Test]
        public void CopyingTestEquipmentIsEqualButNotSame()
        {
            var equipment =
                CreateTestEquipment.Randomized(678678);
            var result = equipment.CopyDeep();
            Assert.AreNotSame(equipment, result);
            Assert.IsTrue(result.EqualsByContent(equipment));
        }

        [TestCase("", true)]
        [TestCase("abc", false)]
        [TestCase(null, true)]
        public void ValidateReturnsSerialNumberIsEmpty(string serialNumber, bool result)
        {
            var entity = new TestEquipment {SerialNumber = new TestEquipmentSerialNumber(serialNumber)};
            Assert.AreEqual(result, entity.Validate(nameof(TestEquipment.SerialNumber)) == TestEquipmentValidationError.SerialNumberIsEmpty);
        }

        [TestCase("", true)]
        [TestCase("abc", false)]
        [TestCase(null, true)]
        public void ValidateReturnsInventoryNumberIsEmpty(string inventoryNumber, bool result)
        {
            var entity = new TestEquipment {InventoryNumber = new TestEquipmentInventoryNumber(inventoryNumber)};
            Assert.AreEqual(result, entity.Validate(nameof(TestEquipment.InventoryNumber)) == TestEquipmentValidationError.InventoryNumberIsEmpty);
        }

        [TestCase(3,2, true)]
        [TestCase(7.8, 2.3, true)]
        [TestCase(5, 8, false)]
        [TestCase(6, 6, false)]
        public void ValidateReturnsCapacityMaxLessThanCapacityMin(double min, double max, bool result)
        {
            var entity = new TestEquipment
            {
                CapacityMin = Torque.FromNm(min),
                CapacityMax = Torque.FromNm(max)
            };
            Assert.AreEqual(result, entity.Validate(nameof(TestEquipment.CapacityMax)) == TestEquipmentValidationError.CapacityMaxLessThanCapacityMin);
        }

        [TestCase(3, 2, true)]
        [TestCase(7.8, 2.3, true)]
        [TestCase(5, 8, false)]
        [TestCase(6, 6, false)]
        public void ValidateReturnsCapacityMinGreaterThanCapacityMax(double min, double max, bool result)
        {
            var entity = new TestEquipment
            {
                CapacityMin = Torque.FromNm(min),
                CapacityMax = Torque.FromNm(max)
            };
            Assert.AreEqual(result, entity.Validate(nameof(TestEquipment.CapacityMin)) == TestEquipmentValidationError.CapacityMinGreaterThanCapacityMax);
        }

        [Test]
        public void GetNextCalibrationDateReturnsNull()
        {
            var entity = CreateTestEquipment.Anonymous();

            entity.LastCalibration = null;
            Assert.IsNull(entity.GetNextCalibrationDate());
        }

        static List<DateTime> GetNextCalibrationDateReturnsLastCalibrationDateData = new List<DateTime>()
        {
            new DateTime(2021,12,1,2,3,4),
            new DateTime(2020,11,2,2,3,4),
        };

        [TestCaseSource(nameof(GetNextCalibrationDateReturnsLastCalibrationDateData))]
        public void GetNextCalibrationDateReturnsLastCalibrationDate(DateTime date)
        {
            var entity = CreateTestEquipment.Anonymous();

            entity.LastCalibration = date;
            entity.CalibrationInterval = null;
            Assert.AreEqual(date, entity.GetNextCalibrationDate());
        }

        static List<(DateTime, long, DateTime)> GetNextCalibrationDateReturnsCorrectValueData = new List<(DateTime, long, DateTime)>()
        {
            (
                new DateTime(2020,1,2), 5, new DateTime(2020,1,7)
            ),
            (
                new DateTime(2021,11,6), 1, new DateTime(2021,11,7)
            )
        };

        [TestCaseSource(nameof(GetNextCalibrationDateReturnsCorrectValueData))]
        public void GetNextCalibrationDateReturnsCorrectValue((DateTime lastCalibration, long interval, DateTime result) data)
        {
            var entity = CreateTestEquipment.Anonymous();

            entity.LastCalibration = data.lastCalibration;
            entity.CalibrationInterval = new Interval(){IntervalValue = data.interval};
            Assert.AreEqual(data.result, entity.GetNextCalibrationDate());
        }

        [TestCase(TestEquipmentType.TestTool, false)]
        [TestCase(TestEquipmentType.Wrench, true)]
        [TestCase(TestEquipmentType.AcqTool, false)]
        [TestCase(TestEquipmentType.Analyse, false)]
        [TestCase(TestEquipmentType.Bench, false)]
        public void HasCapacityReturnsCorrectValue(TestEquipmentType testEquipmentType, bool expectedResult)
        {
            var entity = CreateTestEquipment.Anonymous();
            entity.TestEquipmentModel.Type = testEquipmentType;
            Assert.AreEqual(expectedResult, entity.HasCapacity());
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
                        CreateParameterValue = () => TestEquipmentBehaviourTransferCurves.Never,
                        CreateOtherParameterValue = () => TestEquipmentBehaviourTransferCurves.Always,
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
                        CreateParameterValue = () => TestEquipmentBehaviourConfirmMeasurements.Always,
                        CreateOtherParameterValue = () => TestEquipmentBehaviourConfirmMeasurements.OnlyNio,
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
                        SetParameter = (entity, value) => entity.CapacityMax = (Torque)value,
                        CreateParameterValue = () => Torque.FromNm(1),
                        CreateOtherParameterValue = () => Torque.FromNm(4),
                        ParameterName = nameof(TestEquipment.CapacityMax)
                    },
                    new EqualityParameter<TestEquipment>()
                    {
                        SetParameter = (entity, value) => entity.CapacityMin = (Torque)value,
                        CreateParameterValue = () => Torque.FromNm(1),
                        CreateOtherParameterValue = () => Torque.FromNm(4),
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
                        CreateParameterValue = () => new Status(){ListId = new HelperTableEntityId(1), Value = new StatusDescription("3")},
                        CreateOtherParameterValue = () => new Status(){ListId = new HelperTableEntityId(7), Value = new StatusDescription("37")},
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
