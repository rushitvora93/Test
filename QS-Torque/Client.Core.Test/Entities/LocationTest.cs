using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Enums;
using Core.PhysicalValueTypes;
using NUnit.Framework;
using TestHelper.Checker;
using TestHelper.Factories;
using TestHelper.Mock;

namespace Core.Test.Entities
{
    class LocationTest
    {
        [TestCase(78955, 123545)]
        [TestCase(786905, 43526)]
        public void UpdateToleranceLimitsCallsMethodsInToleranceClasses(double setpointTorque, double setpointAngle)
        {
            var toleranceClassTorque = new ToleranceClassMock() { LowerLimit = 2, UpperLimit = 5 };
            var toleranceClassAngle = new ToleranceClassMock() { LowerLimit = 2, UpperLimit = 5 };

            var location = CreateLocation.Anonymous();
            location.SetPointTorque = Torque.FromNm(setpointTorque);
            location.ToleranceClassTorque = toleranceClassTorque;
            location.SetPointAngle = Angle.FromDegree(setpointAngle);
            location.ToleranceClassAngle = toleranceClassAngle;

            location.UpdateToleranceLimits();

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

            var location = CreateLocation.Anonymous();
            location.ToleranceClassTorque = toleranceClassTorque;
            location.ToleranceClassAngle = toleranceClassAngle;

            location.UpdateToleranceLimits();

            Assert.AreEqual(toleranceClassTorque.GetLowerLimitForValueReturnValue, location.MinimumTorque.Nm);
            Assert.AreEqual(toleranceClassTorque.GetUpperLimitForValueReturnValue, location.MaximumTorque.Nm);
            Assert.AreEqual(toleranceClassAngle.GetLowerLimitForValueReturnValue, location.MinimumAngle.Degree);
            Assert.AreEqual(toleranceClassAngle.GetUpperLimitForValueReturnValue, location.MaximumAngle.Degree);
        }

        [Test]
        public void UpdateToleranceLimitsDoesNotUpdateLimitsIfLimitsOfToleranceClassAreZero()
        {
            var toleranceClassTorque = new ToleranceClassMock();
            var toleranceClassAngle = new ToleranceClassMock();

            var location = CreateLocation.Anonymous();
            location.ToleranceClassTorque = toleranceClassTorque;
            location.ToleranceClassAngle = toleranceClassAngle;


            location.UpdateToleranceLimits();

            Assert.IsFalse(toleranceClassTorque.WasGetUpperLimitForValueCalled);
            Assert.IsFalse(toleranceClassTorque.WasGetLowerLimitForValueCalled);
            Assert.IsFalse(toleranceClassAngle.WasGetUpperLimitForValueCalled);
            Assert.IsFalse(toleranceClassAngle.WasGetLowerLimitForValueCalled);
        }


        [Test]
        public void EqualsByIdWithDifferentIdsMeansInequality()
        {
            var left = CreateLocation.IdOnly(97486153);
            var right = CreateLocation.IdOnly(147896325);
            Assert.IsFalse(left.EqualsById(right));
        }

        [Test]
        public void EqualsByIdWithNullMeansInequality()
        {
            var left = CreateLocation.IdOnly(97486153);
            Assert.IsFalse(left.EqualsById(null));
        }

        [Test]
        public void EqualsByIdWithEqualIdsMeansEquality()
        {
            var left = CreateLocation.IdOnly(97486153);
            var right = CreateLocation.IdOnly(97486153);
            Assert.IsTrue(left.EqualsById(right));
        }

        [TestCaseSource(nameof(EqualsByContentTestSource))]
        public void EqualsByContentWithDifferentParameterMeansInequality((EqualityParameter<Location> parameter, EqualityTestHelper<Location> helper) helperTuple)
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

        [TestCase(0)]
        [TestCase(-5)]
        [TestCase(-98541)]
        public void ValidateReturnsSetpointTorqueIsLessThanOrEqualToZeroOnValidateWithSetpointTorque(double setpointTorque)
        {
            var entity = new Location()
            {
                SetPointTorque = Torque.FromNm(setpointTorque),
                ControlledBy = LocationControlledBy.Torque
            };
            var result = entity.Validate(nameof(Location.SetPointTorque)).ToList();
            Assert.IsTrue(result.Contains(LocationValidationError.SetpointTorqueIsLessThanOrEqualToZero));
        }

        [TestCase(-1)]
        [TestCase(-5)]
        [TestCase(-98541)]
        public void ValidateReturnsSetpointTorqueIsLessThanZeroOnValidateWithSetpointTorque(double setpointTorque)
        {
            var entity = new Location()
            {
                SetPointTorque = Torque.FromNm(setpointTorque),
                ControlledBy = LocationControlledBy.Angle
            };
            var result = entity.Validate(nameof(Location.SetPointTorque)).ToList();
            Assert.IsTrue(result.Contains(LocationValidationError.SetpointTorqueIsLessThanZero));
        }

        [TestCase(5, 5.1)]
        [TestCase(5, 6)]
        [TestCase(100, 1000)]
        public void ValidateReturnsMinimumTorqueHasToBeLessOrEqualThanSetpointTorqueOnValidateWithMinimumTorque(double setpointTorque, double minTorque)
        {
            var entity = new Location()
            {
                SetPointTorque = Torque.FromNm(setpointTorque),
                MinimumTorque = Torque.FromNm(minTorque)
            };
            var result = entity.Validate(nameof(Location.MinimumTorque)).ToList();
            Assert.IsTrue(result.Contains(LocationValidationError.MinimumTorqueHasToBeLessOrEqualThanSetpointTorque));
        }

        [Test]
        public void ValidateReturnsNothingWithSetpointTorqueAndMinimumTorqueIsZero()
        {
            var entity = new Location()
            {
                SetPointTorque = Torque.FromNm(0),
                MinimumTorque = Torque.FromNm(0)
            };
            var result = entity.Validate(nameof(Location.MinimumTorque)).ToList();
            Assert.IsFalse(result.Contains(LocationValidationError.MinimumTorqueHasToBeLessOrEqualThanSetpointTorque));
        }

        [TestCase(-1)]
        [TestCase(-0.1)]
        public void ValidateReturnsMinimumTorqueHasToBeGreaterThanOrEqualToZeroWithMinimumTorque(double minimumTorque)
        {
            var entity = new Location()
            {
                SetPointTorque = Torque.FromNm(0),
                MinimumTorque = Torque.FromNm(minimumTorque),
                ControlledBy = LocationControlledBy.Angle
            };
            var result = entity.Validate(nameof(Location.MinimumTorque)).ToList();
            Assert.IsTrue(result.Contains(LocationValidationError.MinimumTorqueHasToBeGreaterThanOrEqualToZero));
        }

        [TestCase(5.1, 5)]
        [TestCase(6, 5)]
        [TestCase(1000, 100)]
        public void ValidateReturnsMaximumTorqueHasToBeGreaterOrEqualThanSetpointTorqueWithMaximumTorque(double setpointTorque, double maxTorque)
        {
            var entity = new Location()
            {
                SetPointTorque = Torque.FromNm(setpointTorque),
                MaximumTorque = Torque.FromNm(maxTorque)
            };
            var result = entity.Validate(nameof(Location.MaximumTorque)).ToList();
            Assert.IsTrue(result.Contains(LocationValidationError.MaximumTorqueHasToBeGreaterOrEqualThanSetpointTorque));
        }

        [Test]
        public void ValidateReturnsNothingWithSetpointTorqueAndMaximumTorqueIsZero()
        {
            var entity = new Location()
            {
                SetPointTorque = Torque.FromNm(0),
                MaximumTorque = Torque.FromNm(0)
            };
            var result = entity.Validate(nameof(Location.MaximumTorque)).ToList();
            Assert.IsFalse(result.Contains(LocationValidationError.MaximumTorqueHasToBeGreaterOrEqualThanSetpointTorque));
        }

        [TestCase(-1)]
        [TestCase(-0.5)]
        public void ValidateReturnsMaximumTorqueHasToBeGreaterThanOrEqualToZeroWithMinimumTorque(double maximumTorque)
        {
            var entity = new Location()
            {
                SetPointTorque = Torque.FromNm(0),
                MaximumTorque = Torque.FromNm(maximumTorque),
                ControlledBy = LocationControlledBy.Angle
            };
            var result = entity.Validate(nameof(Location.MaximumTorque)).ToList();
            Assert.IsTrue(result.Contains(LocationValidationError.MaximumTorqueHasToBeGreaterThanOrEqualToZero));
        }


        [TestCase(0)]
        [TestCase(-5)]
        [TestCase(-98541)]
        public void ValidateReturnsThresholdTorqueIsLessThanOrEqualToZeroOnValidateWithThresholdTorque(double thresholdTorque)
        {
            var entity = new Location()
            {
                ThresholdTorque = Torque.FromNm(thresholdTorque)
            };
            var result = entity.Validate(nameof(Location.ThresholdTorque)).ToList();
            Assert.IsTrue(result.Contains(LocationValidationError.ThresholdTorqueIsLessThanOrEqualToZero));
        }

        [TestCase(5, 6)]
        [TestCase(100, 1000)]
        public void ValidateReturnsThresholdTorqueIsGreaterThanSetpointTorqueOnValidateWithThresholdTorque(double setpointTorque, double thresholdTorque)
        {
            var entity = new Location()
            {
                SetPointTorque = Torque.FromNm(setpointTorque),
                ThresholdTorque = Torque.FromNm(thresholdTorque),
                ControlledBy = LocationControlledBy.Angle
            };
            var result = entity.Validate(nameof(Location.ThresholdTorque)).ToList();
            Assert.IsTrue(result.Contains(LocationValidationError.ThresholdTorqueIsGreaterThanSetpointTorque));
        }

        [Test]
        public void ValidateDontReturnsThresholdTorqueIsGreaterThanSetPointTorqueOnValidateWithNoSetPointTorque()
        {
            var entity = new Location()
            {
                SetPointTorque = Torque.FromNm(0),
                ThresholdTorque = Torque.FromNm(10),
                ControlledBy = LocationControlledBy.Angle
            };
            var result = entity.Validate(nameof(Location.ThresholdTorque)).ToList();
            Assert.IsFalse(result.Contains(LocationValidationError.ThresholdTorqueIsGreaterThanSetpointTorque));
        }

        [TestCase(0)]
        [TestCase(-5)]
        [TestCase(-98541)]
        public void ValidateReturnsSetpointAngleIsLessThanOrEqualToZeroOnValidateWithSetpointAngle(double setpointAngle)
        {
            var entity = new Location()
            {
                SetPointAngle = Angle.FromDegree(setpointAngle),
                ControlledBy = LocationControlledBy.Angle
            };
            var result = entity.Validate(nameof(Location.SetPointAngle)).ToList();
            Assert.IsTrue(result.Contains(LocationValidationError.SetpointAngleIsLessThanOrEqualToZero));
        }

        [TestCase(-1)]
        [TestCase(-5)]
        [TestCase(-98541)]
        public void ValidateReturnsSetpointAngleIsLessThanZeroOnValidateWithSetpointAngle(double setpointAngle)
        {
            var entity = new Location()
            {
                SetPointAngle = Angle.FromDegree(setpointAngle),
                ControlledBy = LocationControlledBy.Torque
            };
            var result = entity.Validate(nameof(Location.SetPointAngle)).ToList();
            Assert.IsTrue(result.Contains(LocationValidationError.SetpointAngleIsLessThanZero));
        }

        [TestCase(5, 5.1)]
        [TestCase(5, 6)]
        [TestCase(100, 1000)]
        public void ValidateReturnsMinimumAngleHasToBeLessOrEqualThanSetpointAngleOnValidateWithMinimumAngle(double setpointAngle, double minAngle)
        {
            var entity = new Location()
            {
                SetPointAngle = Angle.FromDegree(setpointAngle),
                MinimumAngle = Angle.FromDegree(minAngle)
            };
            var result = entity.Validate(nameof(Location.MinimumAngle)).ToList();
            Assert.IsTrue(result.Contains(LocationValidationError.MinimumAngleHasToBeLessOrEqualThanSetpointAngle));
        }

        [Test]
        public void ValidateReturnsNothingWithSetpointAngleAndMinimumAngleIsZero()
        {
            var entity = new Location()
            {
                SetPointAngle = Angle.FromDegree(0),
                MinimumAngle = Angle.FromDegree(0)
            };
            var result = entity.Validate(nameof(Location.MinimumAngle)).ToList();
            Assert.IsFalse(result.Contains(LocationValidationError.MinimumAngleHasToBeLessOrEqualThanSetpointAngle));
        }


        [TestCase(-1)]
        [TestCase(-0.1)]
        public void ValidateReturnsMinimumAngleHasToBeGreaterThanOrEqualToZeroWithMinimumAngle(double minimumAngle)
        {
            var entity = new Location()
            {
                SetPointAngle = Angle.FromDegree(0),
                MinimumAngle = Angle.FromDegree(minimumAngle),
                ControlledBy = LocationControlledBy.Torque
            };
            var result = entity.Validate(nameof(Location.MinimumAngle)).ToList();
            Assert.IsTrue(result.Contains(LocationValidationError.MinimumAngleHasToBeGreaterThanOrEqualToZero));
        }

        [TestCase(5.1, 5)]
        [TestCase(6, 5)]
        [TestCase(1000, 100)]
        public void ValidateReturnsMaximumAngleHasToBeGreaterOrEqualThanSetpointAngleOnValidateWithMaximumTorque(double setpointAngle, double maxAngle)
        {
            var entity = new Location()
            {
                SetPointAngle = Angle.FromDegree(setpointAngle),
                MaximumAngle = Angle.FromDegree(maxAngle)
            };
            var result = entity.Validate(nameof(Location.MaximumAngle)).ToList();
            Assert.IsTrue(result.Contains(LocationValidationError.MaximumAngleHasToBeGreaterOrEqualThanSetpointAngle));
        }

        [Test]
        public void ValidateReturnsNothingWithSetpointAngleAndMaximumAngleIsZero()
        {
            var entity = new Location()
            {
                SetPointAngle = Angle.FromDegree(0),
                MaximumAngle = Angle.FromDegree(0)
            };
            var result = entity.Validate(nameof(Location.MaximumAngle)).ToList();
            Assert.IsFalse(result.Contains(LocationValidationError.MaximumAngleHasToBeGreaterOrEqualThanSetpointAngle));
        }

        [TestCase(-1)]
        [TestCase(-0.1)]
        public void ValidateReturnsMaximumAngleHasToBeGreaterThanOrEqualToZeroWithMaximumAngle(double maximumAngle)
        {
            var entity = new Location()
            {
                SetPointAngle = Angle.FromDegree(0),
                MaximumAngle = Angle.FromDegree(maximumAngle),
                ControlledBy = LocationControlledBy.Torque
            };
            var result = entity.Validate(nameof(Location.MaximumAngle)).ToList();
            Assert.IsTrue(result.Contains(LocationValidationError.MaximumAngleHasToBeGreaterThanOrEqualToZero));
        }

        private static IEnumerable<(EqualityParameter<Location>, EqualityTestHelper<Location>)> EqualsByContentTestSource()
        {
            var helper = GetEqualsByContentTestHelper();

            foreach (var param in helper.EqualityParameterList)
            {
                yield return (param, helper);
            }
        }

        private static EqualityTestHelper<Location> GetEqualsByContentTestHelper()
        {
            return new EqualityTestHelper<Location>(
                (left, right) => left.EqualsByContent(right),
                () => new Location(),
                new List<EqualityParameter<Location>>()
                {
                    new EqualityParameter<Location>()
                    {
                        SetParameter = (entity, value) => entity.Id = value as LocationId,
                        CreateParameterValue = () => new LocationId(8520),
                        CreateOtherParameterValue = () => new LocationId(7895453),
                        ParameterName = nameof(Location.Id)
                    },
                    new EqualityParameter<Location>()
                    {
                        SetParameter = (entity, value) => entity.Number = value as LocationNumber,
                        CreateParameterValue = () => new LocationNumber("wüeproitugzhjfkdlsö"),
                        CreateOtherParameterValue = () => new LocationDescription("rßt0g9iuvjpyodflwaefa"),
                        ParameterName = nameof(Location.Number)
                    },
                    new EqualityParameter<Location>()
                    {
                        SetParameter = (entity, value) => entity.Description = value as LocationDescription,
                        CreateParameterValue = () => new LocationDescription("wüeproitugzhjfkdlsö"),
                        CreateOtherParameterValue = () => new LocationDescription("rßt0g9iuvjpyodflwaefa"),
                        ParameterName = nameof(Location.Description)
                    },
                    new EqualityParameter<Location>()
                    {
                        SetParameter = (entity, value) => entity.ParentDirectoryId = value as LocationDirectoryId,
                        CreateParameterValue = () => new LocationDirectoryId(8520),
                        CreateOtherParameterValue = () => new LocationDirectoryId(7895453),
                        ParameterName = nameof(Location.ParentDirectoryId)
                    },
                    new EqualityParameter<Location>()
                    {
                        SetParameter = (entity, value) => entity.ControlledBy = (LocationControlledBy)value,
                        CreateParameterValue = () => LocationControlledBy.Angle,
                        CreateOtherParameterValue = () => LocationControlledBy.Torque,
                        ParameterName = nameof(Location.ControlledBy)
                    },
                    new EqualityParameter<Location>()
                    {
                        SetParameter = (entity, value) => entity.SetPointTorque = (Torque)value,
                        CreateParameterValue = () => Torque.FromNm(9846132d),
                        CreateOtherParameterValue = () => Torque.FromNm(984651645.65456),
                        ParameterName = nameof(Location.SetPointTorque)
                    },
                    new EqualityParameter<Location>()
                    {
                        SetParameter = (entity, value) => entity.ToleranceClassTorque = (ToleranceClass)value,
                        CreateParameterValue = () => new ToleranceClass(){Id = new ToleranceClassId(85236), Name = "ü3ß049r8utzghfjk"},
                        CreateOtherParameterValue = () => new ToleranceClass(){Id = new ToleranceClassId(98765432), Name = "yexrctvzubbinm"},
                        ParameterName = nameof(Location.ToleranceClassTorque)
                    },
                    new EqualityParameter<Location>()
                    {
                        SetParameter = (entity, value) => entity.MinimumTorque = (Torque)value,
                        CreateParameterValue = () => Torque.FromNm(9846132d),
                        CreateOtherParameterValue = () => Torque.FromNm(984651645.65456),
                        ParameterName = nameof(Location.MinimumTorque)
                    },
                    new EqualityParameter<Location>()
                    {
                        SetParameter = (entity, value) => entity.MaximumTorque = (Torque)value,
                        CreateParameterValue = () => Torque.FromNm(9846132d),
                        CreateOtherParameterValue = () => Torque.FromNm(984651645.65456),
                        ParameterName = nameof(Location.MaximumTorque)
                    },
                    new EqualityParameter<Location>()
                    {
                        SetParameter = (entity, value) => entity.ThresholdTorque = (Torque)value,
                        CreateParameterValue = () => Torque.FromNm(9846132d),
                        CreateOtherParameterValue = () => Torque.FromNm(984651645.65456),
                        ParameterName = nameof(Location.ThresholdTorque)
                    },
                    new EqualityParameter<Location>()
                    {
                        SetParameter = (entity, value) => entity.SetPointAngle = (Angle)value,
                        CreateParameterValue = () => Angle.FromDegree(9846132d),
                        CreateOtherParameterValue = () => Angle.FromDegree(984651645.65456d),
                        ParameterName = nameof(Location.SetPointAngle)
                    },
                    new EqualityParameter<Location>()
                    {
                        SetParameter = (entity, value) => entity.ToleranceClassAngle = (ToleranceClass)value,
                        CreateParameterValue = () => new ToleranceClass(){Id = new ToleranceClassId(85236), Name = "ü3ß049r8utzghfjk"},
                        CreateOtherParameterValue = () => new ToleranceClass(){Id = new ToleranceClassId(98765432), Name = "yexrctvzubbinm"},
                        ParameterName = nameof(Location.ToleranceClassAngle)
                    },
                    new EqualityParameter<Location>()
                    {
                        SetParameter = (entity, value) => entity.MinimumAngle = (Angle)value,
                        CreateParameterValue = () => Angle.FromDegree(9846132d),
                        CreateOtherParameterValue = () => Angle.FromDegree(984651645.65456d),
                        ParameterName = nameof(Location.MinimumAngle)
                    },
                    new EqualityParameter<Location>()
                    {
                        SetParameter = (entity, value) => entity.MaximumAngle = (Angle)value,
                        CreateParameterValue = () => Angle.FromDegree(9846132d),
                        CreateOtherParameterValue = () => Angle.FromDegree(984651645.65456d),
                        ParameterName = nameof(Location.MaximumAngle)
                    },
                    new EqualityParameter<Location>()
                    {
                        SetParameter = (entity, value) => entity.ConfigurableField1 = (LocationConfigurableField1)value,
                        CreateParameterValue = () => new LocationConfigurableField1("wüepjfkdlsö"),
                        CreateOtherParameterValue = () => new LocationConfigurableField1("rßt0g9iwaefa"),
                        ParameterName = nameof(Location.ConfigurableField1)
                    },
                    new EqualityParameter<Location>()
                    {
                        SetParameter = (entity, value) => entity.ConfigurableField2 = (LocationConfigurableField2)value,
                        CreateParameterValue = () => new LocationConfigurableField2("A"),
                        CreateOtherParameterValue = () => new LocationConfigurableField2("B"),
                        ParameterName = nameof(Location.ConfigurableField2)
                    },
                    new EqualityParameter<Location>()
                    {
                        SetParameter = (entity, value) => entity.ConfigurableField3 = (bool)value,
                        CreateParameterValue = () => true,
                        CreateOtherParameterValue = () => false,
                        ParameterName = nameof(Location.ConfigurableField3)
                    },
                    new EqualityParameter<Location>()
                    {
                        SetParameter = (entity, value) => entity.Comment = (string)value,
                        CreateParameterValue = () => "qawyezrxutiz",
                        CreateOtherParameterValue = () => "äÜ-p96o8iuztrtedshg",
                        ParameterName = nameof(Location.Comment)
                    },
                    new EqualityParameter<Location>()
                    {
                        SetParameter = (entity, value) => entity.Picture = (Picture)value,
                        CreateParameterValue = () => new Picture(){SeqId = 789, FileName = "qwertzuiopü"},
                        CreateOtherParameterValue = () => new Picture(){SeqId = 456, FileName = "asdfghjklöä"},
                        ParameterName = nameof(Location.Picture)
                    },
                    new EqualityParameter<Location>()
                    {
                        SetParameter = (entity, value) => entity.LocationDirectoryPath = (List<LocationDirectory>)value,
                        CreateParameterValue = () => null,
                        CreateOtherParameterValue = () => new List<LocationDirectory>(),
                        ParameterName = nameof(Location.Picture)
                    },
                    new EqualityParameter<Location>()
                    {
                        SetParameter = (entity, value) => entity.LocationDirectoryPath = (List<LocationDirectory>)value,
                        CreateParameterValue = () => new List<LocationDirectory>(),
                        CreateOtherParameterValue = () => null,
                        ParameterName = nameof(Location.Picture)
                    },
                    new EqualityParameter<Location>()
                    {
                        SetParameter = (entity, value) => entity.LocationDirectoryPath = (List<LocationDirectory>)value,
                        CreateParameterValue = () => new List<LocationDirectory>(){ CreateLocationDirectory.Parameterized(1, "324", 2) },
                        CreateOtherParameterValue = () => new List<LocationDirectory>(){ CreateLocationDirectory.Parameterized(3, "4354647", 5) },
                        ParameterName = nameof(Location.LocationDirectoryPath)
                    },
                    new EqualityParameter<Location>()
                    {
                        SetParameter = (entity, value) => entity.LocationDirectoryPath = (List<LocationDirectory>)value,
                        CreateParameterValue = () => new List<LocationDirectory>(){ CreateLocationDirectory.Parameterized(1, "324", 2) },
                        CreateOtherParameterValue = () => new List<LocationDirectory>(){ CreateLocationDirectory.Parameterized(1, "324", 2), CreateLocationDirectory.Parameterized(6, "325465464", 9) },
                        ParameterName = nameof(Location.LocationDirectoryPath)
                    }
                });
        }
    }
}
