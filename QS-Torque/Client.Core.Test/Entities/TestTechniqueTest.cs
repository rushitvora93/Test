using System.Collections.Generic;
using Core.Entities;
using NUnit.Framework;
using TestHelper.Checker;

namespace Core.Test.Entities
{
    class TestTechniqueTest
    {
        [TestCaseSource(nameof(EqualsByContentTestSource))]
        public void EqualsByContentWithDifferentParameterMeansInequality((EqualityParameter<TestTechnique> parameter, EqualityTestHelper<TestTechnique> helper) helperTuple)
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
        [TestCase(-5)]
        [TestCase(-25)]
        public void ValidateReturnsEndCycleTimeHasToBeGreaterThanZeroPointOneWithEndCycleTime(int testValue)
        {
            var testTechnique = new TestTechnique();
            testTechnique.EndCycleTime = testValue;
            var result = testTechnique.Validate(nameof(TestTechnique.EndCycleTime));
            Assert.IsTrue(result.Contains(TestTechniqueError.EndCycleTimeHasToBeGreaterThanZeroPointOne));
        }

        [Test]
        [TestCase(0)]
        [TestCase(-5)]
        public void ValidateReturnsEndCycleTimeHasToBeLessThanFiveWithEndCycleTime(int testValue)
        {
            var testTechnique = new TestTechnique();
            testTechnique.EndCycleTime = testValue;
            var result = testTechnique.Validate(nameof(TestTechnique.EndCycleTime));
            Assert.IsTrue(result.Contains(TestTechniqueError.EndCycleTimeHasToBeGreaterThanZeroPointOne));
        }

        [Test]
        [TestCase(26)]
        [TestCase(43)]
        public void ValidateReturnsFilterFrequencyHasToBeGreaterThanOneHundredWithFilterFrequency(int testValue)
        {
            var testTechnique = new TestTechnique();
            testTechnique.FilterFrequency = testValue;
            var result = testTechnique.Validate(nameof(TestTechnique.FilterFrequency));
            Assert.IsTrue(result.Contains(TestTechniqueError.FilterFrequencyHasToBeGreaterThanOneHundred));
        }

        [Test]
        [TestCase(1100)]
        [TestCase(2000)]
        public void ValidateReturnsFilterFrequencyHasToBeLessThanOneThousendWithFilterFrequency(int testValue)
        {
            var testTechnique = new TestTechnique();
            testTechnique.FilterFrequency = testValue;
            var result = testTechnique.Validate(nameof(TestTechnique.FilterFrequency));
            Assert.IsTrue(result.Contains(TestTechniqueError.FilterFrequencyHasToBeLessThanOneThousend));
        }

        [Test]
        [TestCase(-1)]
        [TestCase(-5)]
        public void ValidateReturnsSlipTorqueHasToBeGreaterOrEqualToZeroWithSlipTorque(int testValue)
        {
            var testTechnique = new TestTechnique();
            testTechnique.SlipTorque = testValue;
            var result = testTechnique.Validate(nameof(TestTechnique.SlipTorque));
            Assert.IsTrue(result.Contains(TestTechniqueError.SlipTorqueHasToBeGreaterOrEqualToZero));
        }

        [Test]
        [TestCase(11000)]
        [TestCase(23000)]
        public void ValidateReturnsSlipTorqueHasToBeLessThanTenThousendWithSlipTorque(int testValue)
        {
            var testTechnique = new TestTechnique();
            testTechnique.SlipTorque = testValue;
            var result = testTechnique.Validate(nameof(TestTechnique.SlipTorque));
            Assert.IsTrue(result.Contains(TestTechniqueError.SlipTorqueHasToBeLessThanTenThousend ));
        }

        [Test]
        [TestCase(-1)]
        [TestCase(-5)]
        public void ValidateReturnsTorqueCoefficientHasToBeGreaterThanZeroPointOneWithSlipTorque(int testValue)
        {
            var testTechnique = new TestTechnique();
            testTechnique.TorqueCoefficient = testValue;
            var result = testTechnique.Validate(nameof(TestTechnique.TorqueCoefficient));
            Assert.IsTrue(result.Contains(TestTechniqueError.TorqueCoefficientHasToBeGreaterThanZeroPointOne));
        }

        [Test]
        [TestCase(15)]
        [TestCase(45)]
        public void ValidateReturnsTorqueCoefficientHasToBeLessThanTenWithSlipTorque(int testValue)
        {
            var testTechnique = new TestTechnique();
            testTechnique.TorqueCoefficient = testValue;
            var result = testTechnique.Validate(nameof(TestTechnique.TorqueCoefficient));
            Assert.IsTrue(result.Contains(TestTechniqueError.TorqueCoefficientHasToBeLessThanTen));
        }

        [Test]
        [TestCase(-1)]
        [TestCase(-5)]
        public void ValidateReturnsMinimumPulseHasToBeGreaterThanZeroWithMinimumPulse(int testValue)
        {
            var testTechnique = new TestTechnique();
            testTechnique.MinimumPulse = testValue;
            var result = testTechnique.Validate(nameof(TestTechnique.MinimumPulse));
            Assert.IsTrue(result.Contains(TestTechniqueError.MinimumPulseHasToBeGreaterThanZero));
        }

        [Test]
        [TestCase(300)]
        [TestCase(1000)]
        public void ValidateReturnsMinimumPulseHasToBeLessThanTwoHundredAndFiftyFiveWithMinimumPulse(int testValue)
        {
            var testTechnique = new TestTechnique();
            testTechnique.MinimumPulse = testValue;
            var result = testTechnique.Validate(nameof(TestTechnique.MinimumPulse));
            Assert.IsTrue(result.Contains(TestTechniqueError.MinimumPulseHasToBeLessThanTwoHundredAndFiftyFive));
        }

        [Test]
        [TestCase(-1)]
        [TestCase(-5)]
        public void ValidateReturnsMaximumPulseHasToBeGreaterThanZeroWithMaximumPulse(int testValue)
        {
            var testTechnique = new TestTechnique();
            testTechnique.MaximumPulse = testValue;
            var result = testTechnique.Validate(nameof(TestTechnique.MaximumPulse));
            Assert.IsTrue(result.Contains(TestTechniqueError.MaximumPulseHasToBeGreaterThanZero));
        }

        [Test]
        [TestCase(300)]
        [TestCase(1000)]
        public void ValidateReturnsMaximumPulseHasToBeLessThanTwoHundredAndFiftyFiveWithMaximumPulse(int testValue)
        {
            var testTechnique = new TestTechnique();
            testTechnique.MaximumPulse = testValue;
            var result = testTechnique.Validate(nameof(TestTechnique.MaximumPulse));
            Assert.IsTrue(result.Contains(TestTechniqueError.MaximumPulseHasToBeLessThanTwoHundredAndFiftyFive));
        }

        [Test]
        [TestCase(-1)]
        [TestCase(-5)]
        public void ValidateReturnsThresholdHasToBeGreaterThanZeroWithThreshold(int testValue)
        {
            var testTechnique = new TestTechnique();
            testTechnique.Threshold = testValue;
            var result = testTechnique.Validate(nameof(TestTechnique.Threshold));
            Assert.IsTrue(result.Contains(TestTechniqueError.ThresholdHasToBeGreaterThanZero));
        }

        [Test]
        [TestCase(300)]
        [TestCase(1000)]
        public void ValidateReturnsThresholdHasToBeLessThanOneHundredWithThreshold(int testValue)
        {
            var testTechnique = new TestTechnique();
            testTechnique.Threshold = testValue;
            var result = testTechnique.Validate(nameof(TestTechnique.Threshold));
            Assert.IsTrue(result.Contains(TestTechniqueError.ThresholdHasToBeLessThanOneHundred));
        }


        private static IEnumerable<(EqualityParameter<TestTechnique>, EqualityTestHelper<TestTechnique>)> EqualsByContentTestSource()
        {
            var helper = GetEqualsByContentTestHelper();

            foreach (var param in helper.EqualityParameterList)
            {
                yield return (param, helper);
            }
        }

        private static EqualityTestHelper<TestTechnique> GetEqualsByContentTestHelper()
        {
            return new EqualityTestHelper<TestTechnique>(
                (left, right) => left.EqualsByContent(right),
                () => new TestTechnique(),
                new List<EqualityParameter<TestTechnique>>()
                {
                    new EqualityParameter<TestTechnique>()
                    {
                        SetParameter = (entity, value) => entity.EndCycleTime = (double)value,
                        CreateParameterValue = () => 984561.854d,
                        CreateOtherParameterValue = () => 7895453d,
                        ParameterName = nameof(TestTechnique.EndCycleTime)
                    },
                    new EqualityParameter<TestTechnique>()
                    {
                        SetParameter = (entity, value) => entity.FilterFrequency = (double)value,
                        CreateParameterValue = () => 984561.854d,
                        CreateOtherParameterValue = () => 7895453d,
                        ParameterName = nameof(TestTechnique.FilterFrequency)
                    },
                    new EqualityParameter<TestTechnique>()
                    {
                        SetParameter = (entity, value) => entity.CycleComplete = (double)value,
                        CreateParameterValue = () => 984561.854d,
                        CreateOtherParameterValue = () => 7895453d,
                        ParameterName = nameof(TestTechnique.CycleComplete)
                    },
                    new EqualityParameter<TestTechnique>()
                    {
                        SetParameter = (entity, value) => entity.MeasureDelayTime = (double)value,
                        CreateParameterValue = () => 984561.854d,
                        CreateOtherParameterValue = () => 7895453d,
                        ParameterName = nameof(TestTechnique.MeasureDelayTime)
                    },
                    new EqualityParameter<TestTechnique>()
                    {
                        SetParameter = (entity, value) => entity.ResetTime = (double)value,
                        CreateParameterValue = () => 984561.854d,
                        CreateOtherParameterValue = () => 7895453d,
                        ParameterName = nameof(TestTechnique.ResetTime)
                    },
                    new EqualityParameter<TestTechnique>()
                    {
                        SetParameter = (entity, value) => entity.MustTorqueAndAngleBeInLimits = (bool)value,
                        CreateParameterValue = () => true,
                        CreateOtherParameterValue = () => false,
                        ParameterName = nameof(TestTechnique.MustTorqueAndAngleBeInLimits)
                    },
                    new EqualityParameter<TestTechnique>()
                    {
                        SetParameter = (entity, value) => entity.CycleStart = (double)value,
                        CreateParameterValue = () => 984561.854d,
                        CreateOtherParameterValue = () => 7895453d,
                        ParameterName = nameof(TestTechnique.CycleStart)
                    },
                    new EqualityParameter<TestTechnique>()
                    {
                        SetParameter = (entity, value) => entity.StartFinalAngle = (double)value,
                        CreateParameterValue = () => 984561.854d,
                        CreateOtherParameterValue = () => 7895453d,
                        ParameterName = nameof(TestTechnique.StartFinalAngle)
                    },
                    new EqualityParameter<TestTechnique>()
                    {
                        SetParameter = (entity, value) => entity.SlipTorque = (double)value,
                        CreateParameterValue = () => 984561.854d,
                        CreateOtherParameterValue = () => 7895453d,
                        ParameterName = nameof(TestTechnique.SlipTorque)
                    },
                    new EqualityParameter<TestTechnique>()
                    {
                        SetParameter = (entity, value) => entity.TorqueCoefficient = (double)value,
                        CreateParameterValue = () => 984561.854d,
                        CreateOtherParameterValue = () => 7895453d,
                        ParameterName = nameof(TestTechnique.TorqueCoefficient)
                    },
                    new EqualityParameter<TestTechnique>()
                    {
                        SetParameter = (entity, value) => entity.MinimumPulse = (int)value,
                        CreateParameterValue = () => 984561,
                        CreateOtherParameterValue = () => 7895453,
                        ParameterName = nameof(TestTechnique.MinimumPulse)
                    },
                    new EqualityParameter<TestTechnique>()
                    {
                        SetParameter = (entity, value) => entity.MaximumPulse = (int)value,
                        CreateParameterValue = () => 984561,
                        CreateOtherParameterValue = () => 7895453,
                        ParameterName = nameof(TestTechnique.MaximumPulse)
                    },
                    new EqualityParameter<TestTechnique>()
                    {
                        SetParameter =  (entity, value) => entity.Threshold = (int)value,
                        CreateParameterValue = () => 5,
                        CreateOtherParameterValue = ()=>8,
                        ParameterName = nameof(TestTechnique.Threshold)
                    }
                });
        }
    }
}
