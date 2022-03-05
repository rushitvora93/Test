using System.Collections.Generic;
using Core.Entities;
using NUnit.Framework;
using TestHelper.Checker;

namespace Core.Test.Entities
{
    class ToolTest
    {
        [Test]
        public void EqualsByIdWithDifferentIdsMeansInequality()
        {
            var left = new Tool() { Id = new ToolId(97486153) };
            var right = new Tool() { Id = new ToolId(147896325) };

            Assert.IsFalse(left.EqualsById(right));
        }

        [Test]
        public void EqualsByIdWithNullMeansInequality()
        {
            var left = new Tool() { Id = new ToolId(97486153) };

            Assert.IsFalse(left.EqualsById(null));
        }

        [Test]
        public void EqualsByIdWithEqualIdsMeansEquality()
        {
            var left = new Tool() { Id = new ToolId(97486153) };
            var right = new Tool() { Id = new ToolId(97486153) };

            Assert.IsTrue(left.EqualsById(right));
        }

        [TestCaseSource(nameof(EqualsByContentTestSource))]
        public void EqualsByContentWithDifferentParameterMeansInequality((EqualityParameter<Tool> parameter, EqualityTestHelper<Tool> helper) helperTuple)
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



        private static IEnumerable<(EqualityParameter<Tool>, EqualityTestHelper<Tool>)> EqualsByContentTestSource()
        {
            var helper = GetEqualsByContentTestHelper();

            foreach (var param in helper.EqualityParameterList)
            {
                yield return (param, helper);
            }
        }

        private static EqualityTestHelper<Tool> GetEqualsByContentTestHelper()
        {
            return new EqualityTestHelper<Tool>(
                (left, right) => left.EqualsByContent(right),
                () => new Tool(),
                new List<EqualityParameter<Tool>>()
                {
                    new EqualityParameter<Tool>()
                    {
                        SetParameter = (entity, value) => entity.Id = value as ToolId,
                        CreateParameterValue = () => new ToolId(8520),
                        CreateOtherParameterValue = () => new ToolId(7895453),
                        ParameterName = nameof(Tool.Id)
                    },
                    new EqualityParameter<Tool>()
                    {
                        SetParameter = (entity, value) => entity.InventoryNumber = value as ToolInventoryNumber,
                        CreateParameterValue = () => new ToolInventoryNumber("aweutfijsr"),
                        CreateOtherParameterValue = () => new ToolInventoryNumber("0+9ßü8p79o68i57r"),
                        ParameterName = nameof(Tool.InventoryNumber)
                    },
                    new EqualityParameter<Tool>()
                    {
                        SetParameter = (entity, value) => entity.SerialNumber = value as ToolSerialNumber,
                        CreateParameterValue = () => new ToolSerialNumber("aweutfijsr"),
                        CreateOtherParameterValue = () => new ToolSerialNumber("0+9ßü8p79o68i57r"),
                        ParameterName = nameof(Tool.SerialNumber)
                    },
                    new EqualityParameter<Tool>()
                    {
                        SetParameter = (entity, value) => entity.ToolModel = value as ToolModel,
                        CreateParameterValue = () => new ToolModel(){Id = new ToolModelId(7984512), Description = new ToolModelDescription("rp0to9iguzhfvcnjdksl")},
                        CreateOtherParameterValue = () => new ToolModel(){Id = new ToolModelId(78452368), Description = new ToolModelDescription("-.oli,kujmznhegtrbf")},
                        ParameterName = nameof(Tool.ToolModel)
                    },
                    new EqualityParameter<Tool>()
                    {
                        SetParameter = (entity, value) => entity.Status = value as Status,
                        CreateParameterValue = () => new Status(){ListId = new HelperTableEntityId(789), Value = new StatusDescription("qwertzuiopü")},
                        CreateOtherParameterValue = () => new Status(){ListId = new HelperTableEntityId(456), Value = new StatusDescription("asdfghjklöä")},
                        ParameterName = nameof(Tool.Status)
                    },
                    new EqualityParameter<Tool>()
                    {
                        SetParameter = (entity, value) => entity.CostCenter = value as CostCenter,
                        CreateParameterValue = () => new CostCenter(){ListId = new HelperTableEntityId(789), Value = new HelperTableDescription("qwertzuiopü")},
                        CreateOtherParameterValue = () => new CostCenter(){ListId = new HelperTableEntityId(456), Value = new HelperTableDescription("asdfghjklöä")},
                        ParameterName = nameof(Tool.CostCenter)
                    },
                    new EqualityParameter<Tool>()
                    {
                        SetParameter = (entity, value) => entity.ConfigurableField = value as ConfigurableField,
                        CreateParameterValue = () => new ConfigurableField(){ListId = new HelperTableEntityId(789), Value = new HelperTableDescription("qwertzuiopü")},
                        CreateOtherParameterValue = () => new ConfigurableField(){ListId = new HelperTableEntityId(456), Value = new HelperTableDescription("asdfghjklöä")},
                        ParameterName = nameof(Tool.ConfigurableField)
                    },
                    new EqualityParameter<Tool>()
                    {
                        SetParameter = (entity, value) => entity.Accessory = value as string,
                        CreateParameterValue = () => "aweutfijsr",
                        CreateOtherParameterValue = () => "0+9ßü8p79o68i57r",
                        ParameterName = nameof(Tool.Accessory)
                    },
                    new EqualityParameter<Tool>()
                    {
                        SetParameter = (entity, value) => entity.Comment = value as string,
                        CreateParameterValue = () => "aweutfijsr",
                        CreateOtherParameterValue = () => "0+9ßü8p79o68i57r",
                        ParameterName = nameof(Tool.Comment)
                    },
                    new EqualityParameter<Tool>()
                    {
                        SetParameter = (entity, value) => entity.AdditionalConfigurableField1 = value as ConfigurableFieldString40,
                        CreateParameterValue = () => new ConfigurableFieldString40("ortigujm"),
                        CreateOtherParameterValue = () => new ConfigurableFieldString40("pgoiujhpö9ij"),
                        ParameterName = nameof(Tool.AdditionalConfigurableField1)
                    },
                    new EqualityParameter<Tool>()
                    {
                        SetParameter = (entity, value) => entity.AdditionalConfigurableField2 = value as ConfigurableFieldString80,
                        CreateParameterValue = () => new ConfigurableFieldString80("ortigujm"),
                        CreateOtherParameterValue = () => new ConfigurableFieldString80("pgoiujhpö9ij"),
                        ParameterName = nameof(Tool.AdditionalConfigurableField2)
                    },
                    new EqualityParameter<Tool>()
                    {
                        SetParameter = (entity, value) => entity.AdditionalConfigurableField3 = value as ConfigurableFieldString250,
                        CreateParameterValue = () => new ConfigurableFieldString250("ortigujm"),
                        CreateOtherParameterValue = () => new ConfigurableFieldString250("pgoiujhpö9ij"),
                        ParameterName = nameof(Tool.AdditionalConfigurableField3)
                    },
                    new EqualityParameter<Tool>()
                    {
                        SetParameter = (entity, value) => entity.Picture = value as Picture,
                        CreateParameterValue = () => new Picture(){SeqId = 789, FileName = "qwertzuiopü"},
                        CreateOtherParameterValue = () => new Picture(){SeqId = 456, FileName = "asdfghjklöä"},
                        ParameterName = nameof(Tool.Picture)
                    }
                });
        }
    }
}
