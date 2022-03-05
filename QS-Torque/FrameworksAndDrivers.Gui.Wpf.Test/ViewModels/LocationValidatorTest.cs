using Core.Enums;
using Core.PhysicalValueTypes;
using FrameworksAndDrivers.Gui.Wpf.ViewModel;
using InterfaceAdapters.Models;
using NUnit.Framework;
using TestHelper.Factories;

namespace FrameworksAndDrivers.Gui.Wpf.Test.ViewModels
{
    class LocationValidatorTest
    {
        [Test]
        public void ValidateLocationReturnsTrueWithValidLocation()
        {
            var validator = new LocationValidator();
            Assert.IsTrue(validator.ValidateLocation(LocationModel.GetModelFor(CreateLocation.Anonymous(), new NullLocalizationWrapper(), null)));
        }

        [TestCase("")]
        [TestCase(null)]
        public void ValidateLocationReturnsFalseIfNumberIsEmpty(string number)
        {
            var validator = new LocationValidator();
            var locationControlledByTorque = GetLocationModelControlledByTorque();
            var locationControlledByAngle = GetLocationModelControlledByAngle();

            locationControlledByTorque.Number = number;
            locationControlledByAngle.Number = number;

            Assert.IsFalse(validator.ValidateLocation(locationControlledByTorque));
            Assert.IsFalse(validator.ValidateLocation(locationControlledByAngle));
        }

        [TestCase("")]
        [TestCase(null)]
        public void ValidateLocationReturnsFalseIfDescriptionIsEmpty(string description)
        {
            var validator = new LocationValidator();
            var locationControlledByTorque = GetLocationModelControlledByTorque();
            var locationControlledByAngle = GetLocationModelControlledByAngle();

            locationControlledByTorque.Description = description;
            locationControlledByAngle.Description = description;

            Assert.IsFalse(validator.ValidateLocation(locationControlledByTorque));
            Assert.IsFalse(validator.ValidateLocation(locationControlledByAngle));
        }

        [TestCase(0)]
        [TestCase(-9)]
        public void ValidateLocationReturnsFalseIfSetpointTorqueIsLessOrEqualToZeroAndControlledByTorque(double torque)
        {
            var validator = new LocationValidator();
            var locationControlledByTorque = GetLocationModelControlledByTorque();
            locationControlledByTorque.SetPointTorque = torque;

            Assert.IsFalse(validator.ValidateLocation(locationControlledByTorque));
        }

        [TestCase(0.001)]
        [TestCase(3)]
        [TestCase(1000)]
        public void ValidateLocationReturnsTrueIfSetpointTorqueIsGreaterThanZeroAndControlledByTorque(double torque)
        {
            var validator = new LocationValidator();
            var locationControlledByTorque = GetLocationModelControlledByTorque();
            locationControlledByTorque.SetPointTorque = torque;
            locationControlledByTorque.ThresholdTorque = 0.001;
            locationControlledByTorque.MinimumTorque = 0;
            locationControlledByTorque.MaximumTorque = 10000;

            Assert.IsTrue(validator.ValidateLocation(locationControlledByTorque));
        }

        [TestCase(-0.001)]
        [TestCase(-9)]
        public void ValidateLocationReturnsFalseIfSetpointAngleIsLessThanZeroAndControlledByTorque(double angle)
        {
            var validator = new LocationValidator();
            var locationControlledByTorque = GetLocationModelControlledByTorque();
            locationControlledByTorque.SetPointAngle = angle;

            Assert.IsFalse(validator.ValidateLocation(locationControlledByTorque));
        }

        [TestCase(0)]
        [TestCase(-9)]
        public void ValidateLocationReturnsFalseIfSetpointAngleIsLessOrEqualToZeroAndControlledByAngle(double angle)
        {
            var validator = new LocationValidator();
            var locationControlledByAngle = GetLocationModelControlledByAngle();
            locationControlledByAngle.SetPointAngle = angle;

            Assert.IsFalse(validator.ValidateLocation(locationControlledByAngle));
        }

        [TestCase(-0.001)]
        [TestCase(-9)]
        public void ValidateLocationReturnsFalseIfSetpointTorqueIsLessThanZeroAndControlledByAngle(double torque)
        {
            var validator = new LocationValidator();
            var locationControlledByAngle = GetLocationModelControlledByAngle();
            locationControlledByAngle.SetPointTorque = torque;

            Assert.IsFalse(validator.ValidateLocation(locationControlledByAngle));
        }
        
        [TestCase(9, 9.001)]
        [TestCase(15, 10)]
        public void ValidateLocationReturnsFalseIfSetpointTorqueIsLessThanMinimum(double minimum, double torque)
        {
            var validator = new LocationValidator();
            var locationControlledByTorque = GetLocationModelControlledByTorque();
            var locationControlledByAngle = GetLocationModelControlledByAngle();

            locationControlledByTorque.MinimumTorque = minimum;
            locationControlledByTorque.SetPointTorque = torque;
            locationControlledByAngle.MinimumTorque = minimum;
            locationControlledByAngle.SetPointTorque = torque;

            Assert.IsFalse(validator.ValidateLocation(locationControlledByTorque));
            Assert.IsFalse(validator.ValidateLocation(locationControlledByAngle));
        }

        [TestCase(9, 10)]
        [TestCase(15, 100)]
        public void ValidateLocationReturnsTrueIfSetpointTorqueIsGreaterThanMinimum(double minimum, double torque)
        {
            var validator = new LocationValidator();
            var locationControlledByTorque = GetLocationModelControlledByTorque();
            var locationControlledByAngle = GetLocationModelControlledByAngle();

            locationControlledByTorque.MinimumTorque = minimum;
            locationControlledByTorque.SetPointTorque = torque;
            locationControlledByTorque.MaximumTorque = 10000;
            locationControlledByAngle.MinimumTorque = minimum;
            locationControlledByAngle.SetPointTorque = torque;
            locationControlledByAngle.MaximumTorque = 10000;

            Assert.IsTrue(validator.ValidateLocation(locationControlledByTorque));
            Assert.IsTrue(validator.ValidateLocation(locationControlledByAngle));
        }

        [TestCase(9, 9.001)]
        [TestCase(10, 20)]
        public void ValidateLocationReturnsFalseIfSetpointTorqueGreaterThanMaximum(double maximum, double torque)
        {
            var validator = new LocationValidator();
            var locationControlledByTorque = GetLocationModelControlledByTorque();
            var locationControlledByAngle = GetLocationModelControlledByAngle();

            locationControlledByTorque.MaximumTorque = maximum;
            locationControlledByTorque.SetPointTorque = torque;
            locationControlledByAngle.MaximumTorque = maximum;
            locationControlledByAngle.SetPointTorque = torque;

            Assert.IsFalse(validator.ValidateLocation(locationControlledByTorque));
            Assert.IsFalse(validator.ValidateLocation(locationControlledByAngle));
        }

        [TestCase(10, 9)]
        [TestCase(100, 20)]
        public void ValidateLocationReturnsTrueIfSetpointTorqueLessThanMaximum(double maximum, double torque)
        {
            var validator = new LocationValidator();
            var locationControlledByTorque = GetLocationModelControlledByTorque();
            var locationControlledByAngle = GetLocationModelControlledByAngle();

            locationControlledByTorque.MaximumTorque = maximum;
            locationControlledByTorque.SetPointTorque = torque;
            locationControlledByAngle.MaximumTorque = maximum;
            locationControlledByAngle.SetPointTorque = torque;

            Assert.IsTrue(validator.ValidateLocation(locationControlledByTorque));
            Assert.IsTrue(validator.ValidateLocation(locationControlledByAngle));
        }

        [TestCase(9, 9.001)]
        [TestCase(15, 10)]
        public void ValidateLocationReturnsFalseIfSetpointAngleLessThanMinimum(double minimum, double angle)
        {
            var validator = new LocationValidator();
            var locationControlledByTorque = GetLocationModelControlledByTorque();
            var locationControlledByAngle = GetLocationModelControlledByAngle();

            locationControlledByTorque.MinimumAngle = minimum;
            locationControlledByTorque.SetPointAngle = angle;
            locationControlledByAngle.MinimumAngle = minimum;
            locationControlledByAngle.SetPointAngle = angle;

            Assert.IsFalse(validator.ValidateLocation(locationControlledByTorque));
            Assert.IsFalse(validator.ValidateLocation(locationControlledByAngle));
        }

        [TestCase(9, 10)]
        [TestCase(15, 100)]
        public void ValidateLocationReturnsFalseIfSetpointAngleGreaterThanMinimum(double minimum, double angle)
        {
            var validator = new LocationValidator();
            var locationControlledByTorque = GetLocationModelControlledByTorque();
            var locationControlledByAngle = GetLocationModelControlledByAngle();

            locationControlledByTorque.MinimumAngle = minimum;
            locationControlledByTorque.SetPointAngle = angle;
            locationControlledByTorque.MaximumAngle = 10000;
            locationControlledByAngle.MinimumAngle = minimum;
            locationControlledByAngle.SetPointAngle = angle;
            locationControlledByAngle.MaximumAngle = 10000;

            Assert.IsTrue(validator.ValidateLocation(locationControlledByTorque));
            Assert.IsTrue(validator.ValidateLocation(locationControlledByAngle));
        }

        [TestCase(9, 9.001)]
        [TestCase(10, 20)]
        public void ValidateLocationReturnsFalseIfSetpointAngleGreaterToMaximum(double maximum, double angle)
        {
            var validator = new LocationValidator();
            var locationControlledByTorque = GetLocationModelControlledByTorque();
            var locationControlledByAngle = GetLocationModelControlledByAngle();

            locationControlledByTorque.MaximumAngle = maximum;
            locationControlledByTorque.SetPointAngle = angle;
            locationControlledByAngle.MaximumAngle = maximum;
            locationControlledByAngle.SetPointAngle = angle;

            Assert.IsFalse(validator.ValidateLocation(locationControlledByTorque));
            Assert.IsFalse(validator.ValidateLocation(locationControlledByAngle));
        }

        [TestCase(10, 9)]
        [TestCase(100, 20)]
        public void ValidateLocationReturnsTrueIfSetpointAngleLessThanMaximum(double maximum, double angle)
        {
            var validator = new LocationValidator();
            var locationControlledByTorque = GetLocationModelControlledByTorque();
            var locationControlledByAngle = GetLocationModelControlledByAngle();

            locationControlledByTorque.MaximumAngle = maximum;
            locationControlledByTorque.SetPointAngle = angle;
            locationControlledByAngle.MaximumAngle = maximum;
            locationControlledByAngle.SetPointAngle = angle;

            Assert.IsTrue(validator.ValidateLocation(locationControlledByTorque));
            Assert.IsTrue(validator.ValidateLocation(locationControlledByAngle));
        }

        [TestCase(0)]
        [TestCase(-9)]
        public void ValidateLocationReturnsFalseIfThresholdTorqueIsLessOrEqualToZero(double thresholdTorque)
        {
            var validator = new LocationValidator();
            var location = LocationModel.GetModelFor(CreateLocation.Anonymous(), new NullLocalizationWrapper(), null);
            location.ThresholdTorque = thresholdTorque;

            Assert.IsFalse(validator.ValidateLocation(location));
        }

        [TestCase(10.001, 10)]
        [TestCase(30, 2)]
        public void ValidateLocationReturnsFalseIfThresholdTorqueIsGreaterThanSetpointTorque(double thresholdTorque, double torque)
        {
            var validator = new LocationValidator();
            var locationControlledByAngle = GetLocationModelControlledByAngle();
            locationControlledByAngle.ThresholdTorque = thresholdTorque;
            locationControlledByAngle.SetPointTorque = torque;
            locationControlledByAngle.MaximumTorque = 1000;

            Assert.IsFalse(validator.ValidateLocation(locationControlledByAngle));
        }

        [TestCase(10.1)]
        [TestCase(5.3)]
        public void ValidateLocationReturnsTrueIfThresholdTorqueIsGreaterThanSetPointTorqueWithNoSetPointTorque(double thresholdTorque)
        {
            var validator = new LocationValidator();
            var locationControlledByAngle = GetLocationModelControlledByAngle();
            locationControlledByAngle.Entity.ThresholdTorque = Torque.FromNm(thresholdTorque);
            locationControlledByAngle.SetPointTorque = 0;
            locationControlledByAngle.MinimumTorque = 0;
            locationControlledByAngle.MaximumTorque = 0;

            Assert.IsTrue(validator.ValidateLocation(locationControlledByAngle));
        }

        [Test]
        public void ValidateLocationReturnsFalseIfSetpointAngleAndMaximumAngleAreZero()
        {
            var validator = new LocationValidator();
            var location = GetLocationModelControlledByTorque();
            location.SetPointAngle = 0;
            location.MinimumAngle = 1000;
            location.MaximumAngle = 0;

            Assert.IsFalse(validator.ValidateLocation(location));
        }


        private static LocationModel GetLocationModelControlledByTorque()
        {
            var location = LocationModel.GetModelFor(CreateLocation.Anonymous(), new NullLocalizationWrapper(), null);
            location.ControlledBy = LocationControlledBy.Torque;
            return location;
        }

        private static LocationModel GetLocationModelControlledByAngle()
        {
            var location = LocationModel.GetModelFor(CreateLocation.Anonymous(), new NullLocalizationWrapper(), null);
            location.ControlledBy = LocationControlledBy.Angle;
            return location;
        }
    }
}
