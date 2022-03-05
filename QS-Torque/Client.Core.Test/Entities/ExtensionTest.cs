using NUnit.Framework;
using System.Collections.Generic;
using Common.Types.Enums;
using TestHelper.Checker;
using Core.Entities;

namespace Client.Core.Test.Entities
{
    class ExtensionTest
    {
        #region IQstEquality EqualsById

        [TestCaseSource(nameof(EqualsByIdTestSource))]
        public void EqualsByIdWithDifferentParameterMeansInequality((EqualityParameter<Extension> parameter, EqualityTestHelper<Extension> helper) helperTuple)
        {
            helperTuple.helper.CheckInequalityForParameter(helperTuple.parameter);
        }

        [Test]
        public void EqualsByIdWithRightIsNullMeansInequality()
        {
            var helper = GetEqualsByIdTestHelper();
            helper.CheckInequalityWithRightIsNull();
        }

        [TestCase(1, 0, ExtensionCorrection.UseFactor)]
        [TestCase(1.1, 0, ExtensionCorrection.UseFactor)]
        [TestCase(1, 5, ExtensionCorrection.UseGauge)]
        [TestCase(5, 5, ExtensionCorrection.UseFactor)]
        public void ExtensionCorrectionFromValuesReturnsCorrectValue(double factor, double gauge, ExtensionCorrection result)
        {
            var extension = new Extension
            {
                FactorTorque = factor, 
                Length = gauge
            };

            Assert.AreEqual(result, extension.ExtensionCorrection);
        }

        [Test]
        public void SetExtensionCorrectionGaugeSetsFactorTo1()
        {
            var extension = new Extension
            {
                FactorTorque = 5,
                Length = 5
            };
            extension.ExtensionCorrection = ExtensionCorrection.UseGauge;
            Assert.AreEqual(1, extension.FactorTorque);
        }

        [Test]
        public void SetExtensionCorrectionFactorSetsLengthTo0()
        {
            var extension = new Extension
            {
                FactorTorque = 5,
                Length = 5
            };
            extension.ExtensionCorrection = ExtensionCorrection.UseFactor;
            Assert.AreEqual(0, extension.Length);
        }

        [Test]
        public void EqualsByIdWithEqualIdMeansEquality()
        {
            var helper = GetEqualsByIdTestHelper();
            helper.CheckEqualityForParameterList();
        }


        private static IEnumerable<(EqualityParameter<Extension>, EqualityTestHelper<Extension>)> EqualsByIdTestSource()
        {
            var helper = GetEqualsByIdTestHelper();

            foreach (var param in helper.EqualityParameterList)
            {
                yield return (param, helper);
            }
        }

        private static EqualityTestHelper<Extension> GetEqualsByIdTestHelper()
        {
            return new EqualityTestHelper<Extension>(
                (left, right) => left.EqualsById(right),
                () => new Extension(),
                new List<EqualityParameter<Extension>>()
                {
                    new EqualityParameter<Extension>()
                    {
                        SetParameter = (entity, value) => entity.Id = value as ExtensionId,
                        CreateParameterValue = () => new ExtensionId(8520),
                        CreateOtherParameterValue = () => new ExtensionId(7895453)
                    }
                });
        }

        #endregion

        [TestCase("", true)]
        [TestCase("abc", false)]
        [TestCase(null, true)]
        public void ValidateReturnsInventoryNumberIsEmpty(string inventoryNumber, bool result)
        {
            var entity = new Extension { InventoryNumber = new ExtensionInventoryNumber(inventoryNumber) };
            Assert.AreEqual(result, entity.Validate(nameof(Extension.InventoryNumber)) == Extension.ExtensionValidationError.InventoryNumberIsEmpty);
        }

        [TestCase("", true)]
        [TestCase("abc", false)]
        [TestCase(null, true)]
        public void ValidateReturnsDescriptionIsEmpty(string description, bool result)
        {
            var entity = new Extension { Description = description };
            Assert.AreEqual(result, entity.Validate(nameof(Extension.Description)) == Extension.ExtensionValidationError.DescriptionIsEmpty);
        }

        [TestCase(-1, true)]
        [TestCase(0, false)]
        [TestCase(9999, false)]
        [TestCase(10000, true)]
        public void ValidateReturnsLengthNotGreaterOrEqualTo0AndLessThan10000(double gauge, bool result)
        {
            var entity = new Extension { Length = gauge};
            Assert.AreEqual(result, entity.Validate(nameof(Extension.Length)) == Extension.ExtensionValidationError.LengthNotGreaterOrEqualTo0AndLessThan10000);
        }

        [TestCase(0.8, true)]
        [TestCase(0.9, false)]
        [TestCase(1.5, false)]
        [TestCase(1.6, true)]
        public void ValidateReturnsFactorNotBetween0Point9And1Point5(double factor, bool result)
        {
            var entity = new Extension { FactorTorque = factor };
            Assert.AreEqual(result, entity.Validate(nameof(Extension.FactorTorque)) == Extension.ExtensionValidationError.FactorNotBetween0Point9And1Point5);
        }

        [TestCase(-1, true)]
        [TestCase(0, false)]
        [TestCase(1, false)]
        [TestCase(99, false)]
        [TestCase(100, true)]
        public void ValidateReturnsBendingCompensationNotGreaterOrEqual0AndLess100(double bending, bool result)
        {
            var entity = new Extension { Bending = bending };
            Assert.AreEqual(result, entity.Validate(nameof(Extension.Bending)) == Extension.ExtensionValidationError.BendingCompensationNotGreaterOrEqual0AndLess100);
        }

        #region IQstEquality EqualsByContent

        [TestCaseSource(nameof(EqualsByContentTestSource))]
        public void EqualsByContentWithDifferentParameterMeansInequality((EqualityParameter<Extension> parameter, EqualityTestHelper<Extension> helper) helperTuple)
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

        private static IEnumerable<(EqualityParameter<Extension>, EqualityTestHelper<Extension>)> EqualsByContentTestSource()
        {
            var helper = GetEqualsByContentTestHelper();

            foreach (var param in helper.EqualityParameterList)
            {
                yield return (param, helper);
            }
        }

        private static EqualityTestHelper<Extension> GetEqualsByContentTestHelper()
        {
            return new EqualityTestHelper<Extension>(
                (left, right) => left.EqualsByContent(right),
                () => new Extension(),
                new List<EqualityParameter<Extension>>()
                {
                    new EqualityParameter<Extension>()
                    {
                        SetParameter = (entity, value) => entity.Id = value as ExtensionId,
                        CreateParameterValue = () => new ExtensionId(8520),
                        CreateOtherParameterValue = () => new ExtensionId(7895453),
                        ParameterName = nameof(Extension.Id)
                    },
                    new EqualityParameter<Extension>()
                    {
                        SetParameter = (entity, value) => entity.InventoryNumber = value as ExtensionInventoryNumber,
                        CreateParameterValue = () => new ExtensionInventoryNumber("11111"),
                        CreateOtherParameterValue = () => new ExtensionInventoryNumber("9"),
                        ParameterName = nameof(Extension.InventoryNumber)
                    },
                    new EqualityParameter<Extension>()
                    {
                        SetParameter = (entity, value) => entity.Description = value as string,
                        CreateParameterValue = () => "asdfwer",
                        CreateOtherParameterValue = () => "onkonsoshd",
                        ParameterName = nameof(Extension.Description)
                    },
                    new EqualityParameter<Extension>()
                    {
                        SetParameter = (entity, value) => entity.FactorTorque = (double)value,
                        CreateParameterValue = () => 984561.5d,
                        CreateOtherParameterValue = () => 9464756d,
                        ParameterName = nameof(Extension.FactorTorque)
                    },
                    new EqualityParameter<Extension>()
                    {
                        SetParameter = (entity, value) => entity.Length = (double)value,
                        CreateParameterValue = () => 235d,
                        CreateOtherParameterValue = () => 4d,
                        ParameterName = nameof(Extension.Length)
                    },
                    new EqualityParameter<Extension>()
                    {
                        SetParameter = (entity, value) => entity.Bending = (double)value,
                        CreateParameterValue = () => 56578.3d,
                        CreateOtherParameterValue = () => 478952.2d,
                        ParameterName = nameof(Extension.Bending)
                    }
                });
        }

        #endregion
    }
}
