using System.Collections.Generic;
using Client.Core.Entities;
using Client.TestHelper.Factories;
using Common.Types.Enums;
using Core.Entities;
using NUnit.Framework;
using TestHelper.Checker;

namespace Core.Test.Entities
{
    class TestEquipmentModelTest
    {

        [TestCaseSource(nameof(EqualsByContentTestSource))]
        public void EqualsByContentWithDifferentParameterMeansInequality((EqualityParameter<TestEquipmentModel> parameter, EqualityTestHelper<TestEquipmentModel> helper) helperTuple)
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
            var lhs = new TestEquipmentModel() {Id = new TestEquipmentModelId(5)};
            var rhs = new TestEquipmentModel() { Id = new TestEquipmentModelId(5) }; ;
            Assert.IsTrue(lhs.EqualsByContent(rhs));
        }

        [Test]
        public void CopyingTestEquipmentIsEqualButNotSame()
        {
            var equipmentModel = CreateTestEquipmentModel.Randomized(214324646);
            var result = equipmentModel.CopyDeep();
            Assert.AreNotSame(equipmentModel, result);
            Assert.IsTrue(result.EqualsByContent(equipmentModel));
        }

        [TestCase(TestEquipmentType.Analyse, true)]
        [TestCase(TestEquipmentType.Wrench, true)]
        [TestCase(TestEquipmentType.AcqTool, false)]
        [TestCase(TestEquipmentType.Bench, false)]
        [TestCase(TestEquipmentType.ManualInput, false)]
        [TestCase(TestEquipmentType.TestTool, false)]
        public void HasTransferAttributesReturnsCorrectValue(TestEquipmentType type, bool result)
        {
            var equipmentModel = CreateTestEquipmentModel.Randomized(214324646);
            equipmentModel.Type = type;
            Assert.AreEqual(result, equipmentModel.HasTransferAttributes());
        }

        [TestCase(TestEquipmentType.Analyse, true)]
        [TestCase(TestEquipmentType.Wrench, true)]
        [TestCase(TestEquipmentType.AcqTool, false)]
        [TestCase(TestEquipmentType.Bench, false)]
        [TestCase(TestEquipmentType.ManualInput, false)]
        [TestCase(TestEquipmentType.TestTool, false)]
        public void HasTestBehaviorReturnsCorrectValue(TestEquipmentType type, bool result)
        {
            var equipmentModel = CreateTestEquipmentModel.Randomized(214324646);
            equipmentModel.Type = type;
            Assert.AreEqual(result, equipmentModel.HasTestBehavior());
        }

        [TestCase("", true)]
        [TestCase("abc", false)]
        [TestCase(null, true)]
        public void ValidateReturnsModelNameIsEmpty(string name, bool result)
        {
            var entity = new TestEquipmentModel { TestEquipmentModelName = new TestEquipmentModelName(name) };
            Assert.AreEqual(result, entity.Validate(nameof(TestEquipmentModel.TestEquipmentModelName)) == TestEquipmentModelValidationError.NameIsEmpty);
        }

        private static IEnumerable<(EqualityParameter<TestEquipmentModel>, EqualityTestHelper<TestEquipmentModel>)> EqualsByContentTestSource()
        {
            var helper = GetEqualsByContentTestHelper();

            foreach (var param in helper.EqualityParameterList)
            {
                yield return (param, helper);
            }
        }

        private static EqualityTestHelper<TestEquipmentModel> GetEqualsByContentTestHelper()
        {
            return new EqualityTestHelper<TestEquipmentModel>(
                (left, right) => left.EqualsByContent(right),
                () => new TestEquipmentModel(),
                new List<EqualityParameter<TestEquipmentModel>>()
                {
                    new EqualityParameter<TestEquipmentModel>()
                    {
                        SetParameter = (entity, value) => entity.Id = (TestEquipmentModelId)value,
                        CreateParameterValue = () => new TestEquipmentModelId(1),
                        CreateOtherParameterValue = () => new TestEquipmentModelId(2),
                        ParameterName = nameof(TestEquipmentModel.TestEquipmentModelName)
                    },
                    new EqualityParameter<TestEquipmentModel>()
                    {
                        SetParameter = (entity, value) => entity.DriverProgramPath = (TestEquipmentSetupPath)value,
                        CreateParameterValue = () => new TestEquipmentSetupPath("1"),
                        CreateOtherParameterValue = () => new TestEquipmentSetupPath("2"),
                        ParameterName = nameof(TestEquipmentModel.DriverProgramPath)
                    },
                    new EqualityParameter<TestEquipmentModel>()
                    {
                        SetParameter = (entity, value) => entity.CommunicationFilePath = (TestEquipmentSetupPath)value,
                        CreateParameterValue = () => new TestEquipmentSetupPath("1"),
                        CreateOtherParameterValue = () => new TestEquipmentSetupPath("2"),
                        ParameterName = nameof(TestEquipmentModel.CommunicationFilePath)
                    },
                    new EqualityParameter<TestEquipmentModel>()
                    {
                        SetParameter = (entity, value) => entity.StatusFilePath = (TestEquipmentSetupPath)value,
                        CreateParameterValue = () => new TestEquipmentSetupPath("1"),
                        CreateOtherParameterValue = () => new TestEquipmentSetupPath("2"),
                        ParameterName = nameof(TestEquipmentModel.StatusFilePath)
                    },
                    new EqualityParameter<TestEquipmentModel>()
                    {
                        SetParameter = (entity, value) => entity.ResultFilePath = (TestEquipmentSetupPath)value,
                        CreateParameterValue = () => new TestEquipmentSetupPath("1"),
                        CreateOtherParameterValue = () => new TestEquipmentSetupPath("2"),
                        ParameterName = nameof(TestEquipmentModel.ResultFilePath)
                    },
                    new EqualityParameter<TestEquipmentModel>()
                    {
                        SetParameter = (entity, value) => entity.TestEquipmentModelName = (TestEquipmentModelName)value,
                        CreateParameterValue = () => new TestEquipmentModelName("1"),
                        CreateOtherParameterValue = () => new TestEquipmentModelName("2"),
                        ParameterName = nameof(TestEquipmentModel.TestEquipmentModelName)
                    },
                    new EqualityParameter<TestEquipmentModel>()
                    {
                        SetParameter = (entity, value) => entity.Manufacturer = value as Manufacturer,
                        CreateParameterValue = () => new Manufacturer(){Id = new ManufacturerId(7984512), Name = new ManufacturerName("rp0to9iguzhfvcnjdksl")},
                        CreateOtherParameterValue = () => new Manufacturer(){Id = new ManufacturerId(78452368), Name = new ManufacturerName("-.oli,kujmznhegtrbf")},
                        ParameterName = nameof(TestEquipmentModel.Manufacturer)
                    },
                    new EqualityParameter<TestEquipmentModel>()
                    {
                        SetParameter = (entity, value) => entity.Type = (TestEquipmentType)value,
                        CreateParameterValue = () => TestEquipmentType.AcqTool,
                        CreateOtherParameterValue = () => TestEquipmentType.Analyse,
                        ParameterName = nameof(TestEquipmentModel.Type)
                    },
                    new EqualityParameter<TestEquipmentModel>()
                    {
                        SetParameter = (entity, value) => entity.TransferUser = (bool)value,
                        CreateParameterValue = () => true,
                        CreateOtherParameterValue = () => false,
                        ParameterName = nameof(TestEquipmentModel.TransferUser)
                    },
                    new EqualityParameter<TestEquipmentModel>()
                    {
                        SetParameter = (entity, value) => entity.TransferAdapter = (bool)value,
                        CreateParameterValue = () => true,
                        CreateOtherParameterValue = () => false,
                        ParameterName = nameof(TestEquipmentModel.TransferAdapter)
                    },
                    new EqualityParameter<TestEquipmentModel>()
                    {
                        SetParameter = (entity, value) => entity.TransferTransducer = (bool)value,
                        CreateParameterValue = () => true,
                        CreateOtherParameterValue = () => false,
                        ParameterName = nameof(TestEquipmentModel.TransferTransducer)
                    },
                    new EqualityParameter<TestEquipmentModel>()
                    {
                        SetParameter = (entity, value) => entity.AskForSign = (bool)value,
                        CreateParameterValue = () => true,
                        CreateOtherParameterValue = () => false,
                        ParameterName = nameof(TestEquipmentModel.AskForSign)
                    },
                    new EqualityParameter<TestEquipmentModel>()
                    {
                        SetParameter = (entity, value) => entity.AskForIdent = (bool)value,
                        CreateParameterValue = () => true,
                        CreateOtherParameterValue = () => false,
                        ParameterName = nameof(TestEquipmentModel.AskForIdent)
                    },
                    new EqualityParameter<TestEquipmentModel>()
                    {
                        SetParameter = (entity, value) => entity.TransferCurves = (bool)value,
                        CreateParameterValue = () => true,
                        CreateOtherParameterValue = () => false,
                        ParameterName = nameof(TestEquipmentModel.TransferCurves)
                    },
                    new EqualityParameter<TestEquipmentModel>()
                    {
                        SetParameter = (entity, value) => entity.UseErrorCodes = (bool)value,
                        CreateParameterValue = () => true,
                        CreateOtherParameterValue = () => false,
                        ParameterName = nameof(TestEquipmentModel.UseErrorCodes)
                    },
                    new EqualityParameter<TestEquipmentModel>()
                    {
                        SetParameter = (entity, value) => entity.DoLoseCheck = (bool)value,
                        CreateParameterValue = () => true,
                        CreateOtherParameterValue = () => false,
                        ParameterName = nameof(TestEquipmentModel.DoLoseCheck)
                    },
                    new EqualityParameter<TestEquipmentModel>()
                    {
                        SetParameter = (entity, value) => entity.CanDeleteMeasurements = (bool)value,
                        CreateParameterValue = () => true,
                        CreateOtherParameterValue = () => false,
                        ParameterName = nameof(TestEquipmentModel.CanDeleteMeasurements)
                    },
                    new EqualityParameter<TestEquipmentModel>()
                    {
                        SetParameter = (entity, value) => entity.ConfirmMeasurements = (bool)value,
                        CreateParameterValue = () => true,
                        CreateOtherParameterValue = () => false,
                        ParameterName = nameof(TestEquipmentModel.ConfirmMeasurements)
                    },
                    new EqualityParameter<TestEquipmentModel>()
                    {
                        SetParameter = (entity, value) => entity.TransferLocationPictures = (bool)value,
                        CreateParameterValue = () => true,
                        CreateOtherParameterValue = () => false,
                        ParameterName = nameof(TestEquipmentModel.TransferLocationPictures)
                    },
                    new EqualityParameter<TestEquipmentModel>()
                    {
                        SetParameter = (entity, value) => entity.TransferNewLimits = (bool)value,
                        CreateParameterValue = () => true,
                        CreateOtherParameterValue = () => false,
                        ParameterName = nameof(TestEquipmentModel.TransferNewLimits)
                    },
                    new EqualityParameter<TestEquipmentModel>()
                    {
                        SetParameter = (entity, value) => entity.TransferAttributes = (bool)value,
                        CreateParameterValue = () => true,
                        CreateOtherParameterValue = () => false,
                        ParameterName = nameof(TestEquipmentModel.TransferAttributes)
                    },
                    new EqualityParameter<TestEquipmentModel>()
                    {
                        SetParameter = (entity, value) => entity.UseForCtl = (bool)value,
                        CreateParameterValue = () => true,
                        CreateOtherParameterValue = () => false,
                        ParameterName = nameof(TestEquipmentModel.UseForCtl)
                    },
                    new EqualityParameter<TestEquipmentModel>()
                    {
                        SetParameter = (entity, value) => entity.UseForRot = (bool)value,
                        CreateParameterValue = () => true,
                        CreateOtherParameterValue = () => false,
                        ParameterName = nameof(TestEquipmentModel.UseForRot)
                    },
                    new EqualityParameter<TestEquipmentModel>()
                    {
                        SetParameter = (entity, value) => entity.CanUseQstStandard = (bool)value,
                        CreateParameterValue = () => true,
                        CreateOtherParameterValue = () => false,
                        ParameterName = nameof(TestEquipmentModel.CanUseQstStandard)
                    },
                    new EqualityParameter<TestEquipmentModel>()
                    {
                        SetParameter = (entity, value) => entity.DataGateVersion = (DataGateVersion)value,
                        CreateParameterValue = () => new DataGateVersion(1),
                        CreateOtherParameterValue = () => new DataGateVersion(2),
                        ParameterName = nameof(TestEquipmentModel.DataGateVersion)
                    },
                });
        }
    }
}
