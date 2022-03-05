using System.Collections.Generic;
using Core.Entities;
using Core.Enums;
using Core.PhysicalValueTypes;
using NUnit.Framework;
using TestHelper.Checker;
using TestHelper.Mock;
using TestParameters = Core.Entities.TestParameters;

namespace Core.Test.Entities
{
    class TestParametersTest
    {
        [TestCase(78955, 123545)]
        [TestCase(786905, 43526)]
        public void UpdateToleranceLimitsCallsMethodsInToleranceClasses(double setpointTorque, double setpointAngle)
        {
            var toleranceClassTorque = new ToleranceClassMock() { LowerLimit = 2, UpperLimit = 5 };
            var toleranceClassAngle = new ToleranceClassMock() { LowerLimit = 2, UpperLimit = 5 };

            var testParameters = new TestParameters()
            {
                SetPointTorque = Torque.FromNm(setpointTorque),
                ToleranceClassTorque = toleranceClassTorque,
                SetPointAngle = Angle.FromDegree(setpointAngle),
                ToleranceClassAngle = toleranceClassAngle
            };

            testParameters.UpdateToleranceLimits();

            Assert.IsTrue(toleranceClassTorque.WasGetLowerLimitForValueCalled);
            Assert.IsTrue(toleranceClassTorque.WasGetUpperLimitForValueCalled);
            Assert.IsTrue(toleranceClassAngle.WasGetLowerLimitForValueCalled);
            Assert.IsTrue(toleranceClassAngle.WasGetUpperLimitForValueCalled);
            Assert.AreEqual(setpointTorque, toleranceClassTorque.GetLowerLimitForValueParameter);
            Assert.AreEqual(setpointTorque, toleranceClassTorque.GetUpperLimitForValueParameter);
            Assert.AreEqual(setpointAngle, toleranceClassAngle.GetLowerLimitForValueParameter);
            Assert.AreEqual(setpointAngle, toleranceClassAngle.GetUpperLimitForValueParameter);
        }

        [Test]
        public void UpdateToleranceLimitsAssignsLimitsCorrect()
        {
            var toleranceClassTorque = new ToleranceClassMock()
            {
                LowerLimit = 2,
                GetLowerLimitForValueReturnValue = 78465,
                GetUpperLimitForValueReturnValue = 31546
            };
            var toleranceClassAngle = new ToleranceClassMock()
            {
                UpperLimit = 5,
                GetLowerLimitForValueReturnValue = 8970,
                GetUpperLimitForValueReturnValue = 654738,
            };

            var testParameters = new TestParameters()
            {
                ToleranceClassTorque = toleranceClassTorque,
                ToleranceClassAngle = toleranceClassAngle
            };

            testParameters.UpdateToleranceLimits();

            Assert.AreEqual(toleranceClassTorque.GetLowerLimitForValueReturnValue, testParameters.MinimumTorque.Nm);
            Assert.AreEqual(toleranceClassTorque.GetUpperLimitForValueReturnValue, testParameters.MaximumTorque.Nm);
            Assert.AreEqual(toleranceClassAngle.GetLowerLimitForValueReturnValue, testParameters.MinimumAngle.Degree);
            Assert.AreEqual(toleranceClassAngle.GetUpperLimitForValueReturnValue, testParameters.MaximumAngle.Degree);
        }

        [Test]
        public void UpdateToleranceLimitsDoesNotUpdateLimitsIfLimitsOfToleranceClassAreZero()
        {
            var toleranceClassTorque = new ToleranceClassMock();
            var toleranceClassAngle = new ToleranceClassMock();

            var testParameters = new TestParameters()
            {
                ToleranceClassTorque = toleranceClassTorque,
                ToleranceClassAngle = toleranceClassAngle
            };

            testParameters.UpdateToleranceLimits();

            Assert.IsFalse(toleranceClassTorque.WasGetUpperLimitForValueCalled);
            Assert.IsFalse(toleranceClassTorque.WasGetLowerLimitForValueCalled);
            Assert.IsFalse(toleranceClassAngle.WasGetUpperLimitForValueCalled);
            Assert.IsFalse(toleranceClassAngle.WasGetLowerLimitForValueCalled);
        }

        [TestCaseSource(nameof(EqualsByContentTestSource))]
        public void EqualsByContentWithDifferentParameterMeansInequality((EqualityParameter<TestParameters> parameter, EqualityTestHelper<TestParameters> helper) helperTuple)
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
        [TestCase(15, 36)]
        [TestCase(25, 78)]
        public void ValidateReturnsMinimumAngleGreaterThenSetPointAngleOnValidateWithMinimumAngle(double setPointAngle, double minimumAngle)
        {
            var entity = new TestParameters();
            entity.SetPointAngle = Angle.FromDegree(setPointAngle);
            entity.MinimumAngle = Angle.FromDegree(minimumAngle);
            var result = entity.Validate(nameof(TestParameters.MinimumAngle));
            Assert.IsTrue(result.Contains(TestParameterError.MinimumAngleGreaterThenSetPointAngle));
        }

        [Test]
        [TestCase(36, 15)]
        [TestCase(78, 25)]
        public void ValidateReturnsMaximumAngleLessThenSetPointAngleOnValidateWithMaximumAngle(double setPointAngle,
            double maximumAngle)
        {
            var entity = new TestParameters();
            entity.SetPointAngle = Angle.FromDegree(setPointAngle);
            entity.MaximumAngle = Angle.FromDegree(maximumAngle);
            var result = entity.Validate(nameof(TestParameters.MaximumAngle));
            Assert.IsTrue(result.Contains(TestParameterError.MaximumAngleLessThenSetPointAngle));
        }

        [Test]
        [TestCase(-5)]
        [TestCase(-15)]
        public void ValidateReturnsSetPointAngleLessThenZeroOnValidateSetPointAngle(double setPointAngle)
        {
            var entity = new TestParameters();
            entity.SetPointAngle = Angle.FromDegree(setPointAngle);
            var result = entity.Validate(nameof(TestParameters.SetPointAngle));
            Assert.IsTrue(result.Contains(TestParameterError.SetPointAngleLessThenZero));
        }

        [Test]
        [TestCase(15, 36)]
        [TestCase(25, 78)]
        public void ValidateReturnsMinimumTorqueGreaterThenSetPointTorqueOnValidateWithMinimumTorque(double setPointTorque, double minimumTorque)
        {
            var entity = new TestParameters();
            entity.SetPointTorque = Torque.FromNm(setPointTorque);
            entity.MinimumTorque = Torque.FromNm(minimumTorque);
            var result = entity.Validate(nameof(TestParameters.MinimumTorque));
            Assert.IsTrue(result.Contains(TestParameterError.MinimumTorqueGreaterThenSetPointTorque));
        }

        [Test]
        [TestCase(36, 15)]
        [TestCase(78, 25)]
        public void ValidateReturnsMaximumTorqueLessThenSetPointTorqueOnValidateWithMaximumTorque(double setPointTorque,
            double maximumTorque)
        {
            var entity = new TestParameters();
            entity.SetPointTorque = Torque.FromNm(setPointTorque);
            entity.MaximumTorque = Torque.FromNm(maximumTorque);
            var result = entity.Validate(nameof(TestParameters.MaximumTorque));
            Assert.IsTrue(result.Contains(TestParameterError.MaximumTorqueLessThenSetPointTorque));
        }

        [Test]
        [TestCase(-5)]
        [TestCase(-36)]
        public void ValidateReturnsSetPointTorqueLessThenZeroOnValidateSetPointTorque(double setPointTorque)
        {
            var entity = new TestParameters();
            entity.SetPointTorque = Torque.FromNm(setPointTorque);
            var result = entity.Validate(nameof(TestParameters.SetPointTorque));
            Assert.IsTrue(result.Contains(TestParameterError.SetPointTorqueLessThenZero));
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void ValidateReturnsThresholdTorqueLessOrEqualThanZeroOnValidateSetThresholdTorque(double thresholdtorque)
        {
            var entity = new TestParameters();
            entity.ThresholdTorque = Angle.FromDegree(thresholdtorque);
            var result = entity.Validate(nameof(TestParameters.ThresholdTorque));
            Assert.IsTrue(result.Contains(TestParameterError.ThresholdTorqueLessOrEqualThanZero));
        }

        private static IEnumerable<(EqualityParameter<TestParameters>, EqualityTestHelper<TestParameters>)> EqualsByContentTestSource()
        {
            var helper = GetEqualsByContentTestHelper();

            foreach (var param in helper.EqualityParameterList)
            {
                yield return (param, helper);
            }
        }

        private static EqualityTestHelper<TestParameters> GetEqualsByContentTestHelper()
        {
            return new EqualityTestHelper<TestParameters>(
                (left, right) => left.EqualsByContent(right),
                () => new TestParameters(),
                new List<EqualityParameter<TestParameters>>()
                {
                    new EqualityParameter<TestParameters>()
                    {
                        SetParameter = (entity, value) => entity.SetPointTorque = (Torque)value,
                        CreateParameterValue = () => Torque.FromNm(9846132d),
                        CreateOtherParameterValue = () => Torque.FromNm(984651645.65456),
                        ParameterName = nameof(TestParameters.SetPointTorque)
                    },
                    new EqualityParameter<TestParameters>()
                    {
                        SetParameter = (entity, value) => entity.ToleranceClassTorque = (ToleranceClass)value,
                        CreateParameterValue = () => new ToleranceClass(){Id = new ToleranceClassId(85236), Name = "ü3ß049r8utzghfjk"},
                        CreateOtherParameterValue = () => new ToleranceClass(){Id = new ToleranceClassId(98765432), Name = "yexrctvzubbinm"},
                        ParameterName = nameof(TestParameters.ToleranceClassTorque)
                    },
                    new EqualityParameter<TestParameters>()
                    {
                        SetParameter = (entity, value) => entity.MinimumTorque = (Torque)value,
                        CreateParameterValue = () => Torque.FromNm(9846132d),
                        CreateOtherParameterValue = () => Torque.FromNm(984651645.65456),
                        ParameterName = nameof(TestParameters.MinimumTorque)
                    },
                    new EqualityParameter<TestParameters>()
                    {
                        SetParameter = (entity, value) => entity.MaximumTorque = (Torque)value,
                        CreateParameterValue = () => Torque.FromNm(9846132d),
                        CreateOtherParameterValue = () => Torque.FromNm(984651645.65456),
                        ParameterName = nameof(TestParameters.MaximumTorque)
                    },
                    new EqualityParameter<TestParameters>()
                    {
                        SetParameter = (entity, value) => entity.ThresholdTorque = (Angle)value,
                        CreateParameterValue = () => Angle.FromDegree(9846132d),
                        CreateOtherParameterValue = () => Angle.FromDegree(984651645.65456),
                        ParameterName = nameof(TestParameters.ThresholdTorque)
                    },
                    new EqualityParameter<TestParameters>()
                    {
                        SetParameter = (entity, value) => entity.SetPointAngle = (Angle)value,
                        CreateParameterValue = () => Angle.FromDegree(9846132d),
                        CreateOtherParameterValue = () => Angle.FromDegree(984651645.65456d),
                        ParameterName = nameof(TestParameters.SetPointAngle)
                    },
                    new EqualityParameter<TestParameters>()
                    {
                        SetParameter = (entity, value) => entity.ToleranceClassAngle = (ToleranceClass)value,
                        CreateParameterValue = () => new ToleranceClass(){Id = new ToleranceClassId(85236), Name = "ü3ß049r8utzghfjk"},
                        CreateOtherParameterValue = () => new ToleranceClass(){Id = new ToleranceClassId(98765432), Name = "yexrctvzubbinm"},
                        ParameterName = nameof(TestParameters.ToleranceClassAngle)
                    },
                    new EqualityParameter<TestParameters>()
                    {
                        SetParameter = (entity, value) => entity.MinimumAngle = (Angle)value,
                        CreateParameterValue = () => Angle.FromDegree(9846132d),
                        CreateOtherParameterValue = () => Angle.FromDegree(984651645.65456d),
                        ParameterName = nameof(TestParameters.MinimumAngle)
                    },
                    new EqualityParameter<TestParameters>()
                    {
                        SetParameter = (entity, value) => entity.MaximumAngle = (Angle)value,
                        CreateParameterValue = () => Angle.FromDegree(9846132d),
                        CreateOtherParameterValue = () => Angle.FromDegree(984651645.65456d),
                        ParameterName = nameof(TestParameters.MaximumAngle)
                    },
                    new EqualityParameter<TestParameters>()
                    {
                        SetParameter = (entity, value) => entity.ControlledBy = (LocationControlledBy)value,
                        CreateParameterValue = ()=>LocationControlledBy.Torque,
                        CreateOtherParameterValue = ()=> LocationControlledBy.Angle,
                        ParameterName = nameof(TestParameters.ControlledBy)
                    }
                });
        }
    }
}