using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Entities.ToolTypes;
using Core.Enums;
using NUnit.Framework;
using TestHelper.Checker;

namespace Core.Test.Entities
{
    class ToolModelTest
    {
        private class AbstractModelTypeMock : AbstractToolType {
            public override string Name { get; }
            public override bool DoesToolTypeHasProperty(string propertyName)
            {
                throw new NotImplementedException();
            }

            public override void Accept(IAbstractToolTypeVisitor visitor)
            {
                throw new NotImplementedException();
            }
        }

        private class SecondAbstractModelTypeMock : AbstractToolType
        {
            public override string Name { get; }
            public override bool DoesToolTypeHasProperty(string propertyName)
            {
                throw new NotImplementedException();
            }

            public override void Accept(IAbstractToolTypeVisitor visitor)
            {
                throw new NotImplementedException();
            }
        }


        [Test]
        public void EqualsByIdWithDifferentIdsMeansInequality()
        {
            var left = new ToolModel() { Id = new ToolModelId(97486153) };
            var right = new ToolModel() { Id = new ToolModelId(147896325) };

            Assert.IsFalse(left.EqualsById(right));
        }

        [Test]
        public void EqualsByIdWithNullMeansInequality()
        {
            var left = new ToolModel() { Id = new ToolModelId(97486153) };

            Assert.IsFalse(left.EqualsById(null));
        }

        [Test]
        public void EqualsByIdWithEqualIdsMeansEquality()
        {
            var left = new ToolModel() { Id = new ToolModelId(97486153) };
            var right = new ToolModel() { Id = new ToolModelId(97486153) };

            Assert.IsTrue(left.EqualsById(right));
        }

        [TestCaseSource(nameof(EqualsByContentTestSource))]
        public void EqualsByContentWithDifferentParameterMeansInequality((EqualityParameter<ToolModel> parameter, EqualityTestHelper<ToolModel> helper) helperTuple)
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



        private static IEnumerable<(EqualityParameter<ToolModel>, EqualityTestHelper<ToolModel>)> EqualsByContentTestSource()
        {
            var helper = GetEqualsByContentTestHelper();

            foreach (var param in helper.EqualityParameterList)
            {
                yield return (param, helper);
            }
        }

        private static EqualityTestHelper<ToolModel> GetEqualsByContentTestHelper()
        {
            return new EqualityTestHelper<ToolModel>(
                (left, right) => left.EqualsByContent(right),
                () => new ToolModel(),
                new List<EqualityParameter<ToolModel>>()
                {
                    new EqualityParameter<ToolModel>()
                    {
                        SetParameter = (entity, value) => entity.Id = value as ToolModelId,
                        CreateParameterValue = () => new ToolModelId(8520),
                        CreateOtherParameterValue = () => new ToolModelId(7895453),
                        ParameterName = nameof(ToolModel.Id)
                    },
                    new EqualityParameter<ToolModel>()
                    {
                        SetParameter = (entity, value) => entity.Description = value as ToolModelDescription,
                        CreateParameterValue = () => new ToolModelDescription("aweutfijsr"),
                        CreateOtherParameterValue = () => new ToolModelDescription("0+9ßü8p79o68i57r"),
                        ParameterName = nameof(ToolModel.Description)
                    },
                    new EqualityParameter<ToolModel>()
                    {
                        SetParameter = (entity, value) => entity.ModelType = value as AbstractToolType,
                        CreateParameterValue = () => new AbstractModelTypeMock(),
                        CreateOtherParameterValue = () => new SecondAbstractModelTypeMock(),
                        ParameterName = nameof(ToolModel.ModelType)
                    },
                    new EqualityParameter<ToolModel>()
                    {
                        SetParameter = (entity, value) => entity.Class = (ToolModelClass)value,
                        CreateParameterValue = () => ToolModelClass.DriverFixSet,
                        CreateOtherParameterValue = () => ToolModelClass.DriverWithoutScale,
                        ParameterName = nameof(ToolModel.Class)
                    },
                    new EqualityParameter<ToolModel>()
                    {
                        SetParameter = (entity, value) => entity.Manufacturer = value as Manufacturer,
                        CreateParameterValue = () => new Manufacturer(){Id = new ManufacturerId(7984512), Name = new ManufacturerName("rp0to9iguzhfvcnjdksl")},
                        CreateOtherParameterValue = () => new Manufacturer(){Id = new ManufacturerId(78452368), Name = new ManufacturerName("-.oli,kujmznhegtrbf")},
                        ParameterName = nameof(ToolModel.Manufacturer)
                    },
                    new EqualityParameter<ToolModel>()
                    {
                        SetParameter = (entity, value) => entity.MinPower = (double)value,
                        CreateParameterValue = () => 9846132d,
                        CreateOtherParameterValue = () => 984651645.65456,
                        ParameterName = nameof(ToolModel.MinPower)
                    },
                    new EqualityParameter<ToolModel>()
                    {
                        SetParameter = (entity, value) => entity.MaxPower = (double)value,
                        CreateParameterValue = () => 9846132d,
                        CreateOtherParameterValue = () => 984651645.65456,
                        ParameterName = nameof(ToolModel.MaxPower)
                    },
                    new EqualityParameter<ToolModel>()
                    {
                        SetParameter = (entity, value) => entity.AirPressure = (double)value,
                        CreateParameterValue = () => 9846132d,
                        CreateOtherParameterValue = () => 984651645.65456,
                        ParameterName = nameof(ToolModel.AirPressure)
                    },
                    new EqualityParameter<ToolModel>()
                    {
                        SetParameter = (entity, value) => entity.ToolType = value as ToolType,
                        CreateParameterValue = () => new ToolType(){ListId = new HelperTableEntityId(789), Value = new HelperTableDescription("qwertzuiopü")},
                        CreateOtherParameterValue = () => new ToolType(){ListId = new HelperTableEntityId(456), Value = new HelperTableDescription("asdfghjklöä")},
                        ParameterName = nameof(ToolModel.ToolType)
                    },
                    new EqualityParameter<ToolModel>()
                    {
                        SetParameter = (entity, value) => entity.Weight = (double)value,
                        CreateParameterValue = () => 9846132d,
                        CreateOtherParameterValue = () => 984651645.65456,
                        ParameterName = nameof(ToolModel.Weight)
                    },
                    new EqualityParameter<ToolModel>()
                    {
                        SetParameter = (entity, value) => entity.BatteryVoltage = (double)value,
                        CreateParameterValue = () => 9846132d,
                        CreateOtherParameterValue = () => 984651645.65456,
                        ParameterName = nameof(ToolModel.BatteryVoltage)
                    },
                    new EqualityParameter<ToolModel>()
                    {
                        SetParameter = (entity, value) => entity.MaxRotationSpeed = (int)value,
                        CreateParameterValue = () => 9846132,
                        CreateOtherParameterValue = () => 123654789,
                        ParameterName = nameof(ToolModel.MaxRotationSpeed)
                    },
                    new EqualityParameter<ToolModel>()
                    {
                        SetParameter = (entity, value) => entity.AirConsumption = (double)value,
                        CreateParameterValue = () => 9846132d,
                        CreateOtherParameterValue = () => 984651645.65456,
                        ParameterName = nameof(ToolModel.AirConsumption)
                    },
                    new EqualityParameter<ToolModel>()
                    {
                        SetParameter = (entity, value) => entity.SwitchOff = value as SwitchOff,
                        CreateParameterValue = () => new SwitchOff(){ListId = new HelperTableEntityId(789), Value = new HelperTableDescription("qwertzuiopü")},
                        CreateOtherParameterValue = () => new SwitchOff(){ListId = new HelperTableEntityId(456), Value = new HelperTableDescription("asdfghjklöä")},
                        ParameterName = nameof(ToolModel.SwitchOff)
                    },
                    new EqualityParameter<ToolModel>()
                    {
                        SetParameter = (entity, value) => entity.DriveSize = value as DriveSize,
                        CreateParameterValue = () => new DriveSize(){ListId = new HelperTableEntityId(789), Value = new HelperTableDescription("qwertzuiopü")},
                        CreateOtherParameterValue = () => new DriveSize(){ListId = new HelperTableEntityId(456), Value = new HelperTableDescription("asdfghjklöä")},
                        ParameterName = nameof(ToolModel.DriveSize)
                    },
                    new EqualityParameter<ToolModel>()
                    {
                        SetParameter = (entity, value) => entity.ShutOff = value as ShutOff,
                        CreateParameterValue = () => new ShutOff(){ListId = new HelperTableEntityId(789), Value = new HelperTableDescription("qwertzuiopü")},
                        CreateOtherParameterValue = () => new ShutOff(){ListId = new HelperTableEntityId(456), Value = new HelperTableDescription("asdfghjklöä")},
                        ParameterName = nameof(ToolModel.ShutOff)
                    },
                    new EqualityParameter<ToolModel>()
                    {
                        SetParameter = (entity, value) => entity.DriveType = value as DriveType,
                        CreateParameterValue = () => new DriveType(){ListId = new HelperTableEntityId(789), Value = new HelperTableDescription("qwertzuiopü")},
                        CreateOtherParameterValue = () => new DriveType(){ListId = new HelperTableEntityId(456), Value = new HelperTableDescription("asdfghjklöä")},
                        ParameterName = nameof(ToolModel.ToolType)
                    },
                    new EqualityParameter<ToolModel>()
                    {
                        SetParameter = (entity, value) => entity.ConstructionType = value as ConstructionType,
                        CreateParameterValue = () => new ConstructionType(){ListId = new HelperTableEntityId(789), Value = new HelperTableDescription("qwertzuiopü")},
                        CreateOtherParameterValue = () => new ConstructionType(){ListId = new HelperTableEntityId(456), Value = new HelperTableDescription("asdfghjklöä")},
                        ParameterName = nameof(ToolModel.ConstructionType)
                    },
                    new EqualityParameter<ToolModel>()
                    {
                        SetParameter = (entity, value) => entity.Picture = value as Picture,
                        CreateParameterValue = () => new Picture(){SeqId = 789, FileName = "qwertzuiopü"},
                        CreateOtherParameterValue = () => new Picture(){SeqId = 456, FileName = "asdfghjklöä"},
                        ParameterName = nameof(ToolModel.Picture)
                    },
                    new EqualityParameter<ToolModel>()
                    {
                        SetParameter = (entity, value) => entity.CmLimit = (double)value,
                        CreateParameterValue = () => 9846132d,
                        CreateOtherParameterValue = () => 984651645.65456,
                        ParameterName = nameof(ToolModel.CmLimit)
                    },
                    new EqualityParameter<ToolModel>()
                    {
                        SetParameter = (entity, value) => entity.CmkLimit = (double)value,
                        CreateParameterValue = () => 9846132d,
                        CreateOtherParameterValue = () => 984651645.65456,
                        ParameterName = nameof(ToolModel.CmkLimit)
                    }
                });
        }
    }
}
