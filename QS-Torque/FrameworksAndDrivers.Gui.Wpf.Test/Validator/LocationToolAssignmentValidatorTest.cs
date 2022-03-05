using Core.Entities;
using Core.Entities.ToolTypes;
using FrameworksAndDrivers.Gui.Wpf.Validator;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Core.PhysicalValueTypes;
using InterfaceAdapters.Models;
using TestHelper.Factories;

namespace FrameworksAndDrivers.Gui.Wpf.Test.Validator
{
    public class LocationToolAssignmentValidatorTest
    {
        enum Eror
        {
            blub
        };

        class Test
        {
            public int TestProp { get; set; }
        };

        [Test]
        public void ValidatorReturnsFalseOnFuncReturnErrors()
        {
           Assert.IsFalse(Validator<Eror, Test>.Validate(new Test(), (p) => new List<Eror> {Eror.blub}));
        }

        [Test]
        public void ValidatorReturnsTrueOnFuncReturnsNull()
        {
            Assert.IsTrue(Validator<Eror, Test>.Validate(new Test(), (p) => null));
        }

        [Test]
        public void ValidatorReturnsTrueOnFuncReturnsEmptyList()
        {
            Assert.IsTrue(Validator<Eror, Test>.Validate(new Test(), (p) => new List<Eror>()));
        }

        [Test]
        public void ValidateWithTestTechniqueNullReturnsTrue()
        {
            var locationToolAssignmentValidator = new LocationToolAssignmentValidator();
            LocationToolAssignment entity = CreateValidLocationToolAssignent();
            entity.TestTechnique = null;
            Assert.IsTrue(locationToolAssignmentValidator.Validate(
                LocationToolAssignmentModel.GetModelFor(entity,
                    new NullLocalizationWrapper())));
        }

        private static List<(string, Action<LocationToolAssignment>, bool)> clickWrenchFields =
            new List<(string, Action<LocationToolAssignment>, bool)>
            {
                (nameof(TestTechnique.EndCycleTime),
                    (locationToolAssignment) => locationToolAssignment.TestTechnique.EndCycleTime = -1, false),
                (nameof(TestTechnique.FilterFrequency),
                    (locationToolAssignment) => locationToolAssignment.TestTechnique.FilterFrequency = -1, false),
                (nameof(TestTechnique.SlipTorque),
                    (locationToolAssignment) => locationToolAssignment.TestTechnique.SlipTorque = -1, false),
                (nameof(TestTechnique.TorqueCoefficient),
                    (locationToolAssignment) => locationToolAssignment.TestTechnique.TorqueCoefficient = 0, true),
                (nameof(TestTechnique.MinimumPulse),
                    (locationToolAssignment) => locationToolAssignment.TestTechnique.MinimumPulse = -1, true),
                (nameof(TestTechnique.MaximumPulse),
                    (locationToolAssignment) => locationToolAssignment.TestTechnique.MaximumPulse = 300, true),
                (nameof(TestTechnique.Threshold),
                    (locationToolAssignment) => locationToolAssignment.TestTechnique.Threshold = -1, true),
            };

        [Test]
        [TestCaseSource(nameof(clickWrenchFields))]
        
        public void ValidateForClickWrench((string propertieName, Action<LocationToolAssignment> changeLocationToolAssignment, bool result) parameters)
        {
            var locationToolAssignmentValidator = new LocationToolAssignmentValidator();
            var locationToolAssignment = CreateValidLocationToolAssignent();
            locationToolAssignment.AssignedTool.ToolModel.ModelType = new ClickWrench();
            parameters.changeLocationToolAssignment?.Invoke(locationToolAssignment);
            Assert.AreEqual(parameters.result,
                locationToolAssignmentValidator.Validate(
                    LocationToolAssignmentModel.GetModelFor(locationToolAssignment, new NullLocalizationWrapper())));
        }

        private static List<(string, Action<LocationToolAssignment>, bool)> pulseDriverFields =
            new List<(string, Action<LocationToolAssignment>, bool)>
            {
                (nameof(TestTechnique.EndCycleTime),
                    (locationToolAssignment) => locationToolAssignment.TestTechnique.EndCycleTime = -1, false),
                (nameof(TestTechnique.FilterFrequency),
                    (locationToolAssignment) => locationToolAssignment.TestTechnique.FilterFrequency = -1, false),
                (nameof(TestTechnique.SlipTorque),
                    (locationToolAssignment) => locationToolAssignment.TestTechnique.SlipTorque = -1, true),
                (nameof(TestTechnique.TorqueCoefficient),
                    (locationToolAssignment) => locationToolAssignment.TestTechnique.TorqueCoefficient = 0, false),
                (nameof(TestTechnique.MinimumPulse),
                    (locationToolAssignment) => locationToolAssignment.TestTechnique.MinimumPulse = -1, false),
                (nameof(TestTechnique.MaximumPulse),
                    (locationToolAssignment) => locationToolAssignment.TestTechnique.MaximumPulse = 300, false),
                (nameof(TestTechnique.Threshold),
                    (locationToolAssignment) => locationToolAssignment.TestTechnique.Threshold = -1, false),
            };

        [Test]
        [TestCaseSource(nameof(pulseDriverFields))]
        public void ValidateForPulseDriver((string propertieName, Action<LocationToolAssignment> changeLocationToolAssignment, bool result) parameters)
        {
            var locationToolAssignmentValidator = new LocationToolAssignmentValidator();
            var locationToolAssignment = CreateValidLocationToolAssignent();
            locationToolAssignment.AssignedTool.ToolModel.ModelType = new PulseDriver();
            parameters.changeLocationToolAssignment?.Invoke(locationToolAssignment);
            Assert.AreEqual(parameters.result,
                locationToolAssignmentValidator.Validate(
                    LocationToolAssignmentModel.GetModelFor(locationToolAssignment, new NullLocalizationWrapper())));
        }

        private static List<(string, Action<LocationToolAssignment>, bool)> powerToolFields =
            new List<(string, Action<LocationToolAssignment>, bool)>
            {
                (nameof(TestTechnique.EndCycleTime),
                    (locationToolAssignment) => locationToolAssignment.TestTechnique.EndCycleTime = -1, false),
                (nameof(TestTechnique.FilterFrequency),
                    (locationToolAssignment) => locationToolAssignment.TestTechnique.FilterFrequency = -1, false),
                (nameof(TestTechnique.SlipTorque),
                    (locationToolAssignment) => locationToolAssignment.TestTechnique.SlipTorque = -1, true),
                (nameof(TestTechnique.TorqueCoefficient),
                    (locationToolAssignment) => locationToolAssignment.TestTechnique.TorqueCoefficient = 0, true),
                (nameof(TestTechnique.MinimumPulse),
                    (locationToolAssignment) => locationToolAssignment.TestTechnique.MinimumPulse = -1, true),
                (nameof(TestTechnique.MaximumPulse),
                    (locationToolAssignment) => locationToolAssignment.TestTechnique.MaximumPulse = 300, true),
                (nameof(TestTechnique.Threshold),
                    (locationToolAssignment) => locationToolAssignment.TestTechnique.Threshold = -1, true),
            };
        [Test]
        [TestCaseSource(nameof(powerToolFields))]
        public void ValidateForPowerTool(
            (string propertieName, Action<LocationToolAssignment> changeLocationToolAssignment, bool result) parameters)
        {
            var locationToolAssignmentValidator = new LocationToolAssignmentValidator();
            var locationToolAssignment = CreateValidLocationToolAssignent();
            locationToolAssignment.AssignedTool.ToolModel.ModelType = new General();
            parameters.changeLocationToolAssignment?.Invoke(locationToolAssignment);
            Assert.AreEqual(parameters.result,
                locationToolAssignmentValidator.Validate(
                    LocationToolAssignmentModel.GetModelFor(locationToolAssignment, new NullLocalizationWrapper())));
        }

        private static List<(string, Action<LocationToolAssignment>, bool)> peakValueFields =
            new List<(string, Action<LocationToolAssignment>, bool)>
            {
                (nameof(TestTechnique.EndCycleTime),
                    (locationToolAssignment) => locationToolAssignment.TestTechnique.EndCycleTime = -1, false),
                (nameof(TestTechnique.FilterFrequency),
                    (locationToolAssignment) => locationToolAssignment.TestTechnique.FilterFrequency = -1, false),
                (nameof(TestTechnique.SlipTorque),
                    (locationToolAssignment) => locationToolAssignment.TestTechnique.SlipTorque = -1, true),
                (nameof(TestTechnique.TorqueCoefficient),
                    (locationToolAssignment) => locationToolAssignment.TestTechnique.TorqueCoefficient = 0, true),
                (nameof(TestTechnique.MinimumPulse),
                    (locationToolAssignment) => locationToolAssignment.TestTechnique.MinimumPulse = -1, true),
                (nameof(TestTechnique.MaximumPulse),
                    (locationToolAssignment) => locationToolAssignment.TestTechnique.MaximumPulse = 300, true),
                (nameof(TestTechnique.Threshold),
                    (locationToolAssignment) => locationToolAssignment.TestTechnique.Threshold = -1, true),
            };
        [Test]
        [TestCaseSource(nameof(peakValueFields))]
        public void ValidateForPeakValue(
            (string propertieName, Action<LocationToolAssignment> changeLocationToolAssignment, bool result) parameters)
        {
            var locationToolAssignmentValidator = new LocationToolAssignmentValidator();
            var locationToolAssignment = CreateValidLocationToolAssignent();
            locationToolAssignment.AssignedTool.ToolModel.ModelType = new ProductionWrench();
            parameters.changeLocationToolAssignment?.Invoke(locationToolAssignment);
            Assert.AreEqual(parameters.result,
                locationToolAssignmentValidator.Validate(
                    LocationToolAssignmentModel.GetModelFor(locationToolAssignment, new NullLocalizationWrapper())));
        }


        private static List<(string, Action<LocationToolAssignment>, bool)> mcsIntervalData =
            new List<(string, Action<LocationToolAssignment>, bool)>
            {
                (nameof(Interval.IntervalValue),
                    (locationToolAssignment) => locationToolAssignment.TestLevelSetMfu.TestLevel1.TestInterval.IntervalValue = 0, true),
                (nameof(Interval.IntervalValue),
                    (locationToolAssignment) => locationToolAssignment.TestLevelSetMfu.TestLevel1.TestInterval.IntervalValue = 1, true),
                (nameof(Interval.IntervalValue),
                    (locationToolAssignment) => locationToolAssignment.TestLevelSetMfu.TestLevel1.TestInterval.IntervalValue = 900, true),
                (nameof(Interval.IntervalValue),
                    (locationToolAssignment) => locationToolAssignment.TestLevelSetMfu.TestLevel1.TestInterval.IntervalValue = 901, true)
            };

        [TestCaseSource(nameof(mcsIntervalData))]
        public void ValidateForMcaIntervalReturnsCorrectValue((string propertieName, Action<LocationToolAssignment> changeLocationToolAssignment, bool result) parameters)
        {
            var locationToolAssignmentValidator = new LocationToolAssignmentValidator();
            var locationToolAssignment = CreateValidLocationToolAssignent();
            parameters.changeLocationToolAssignment?.Invoke(locationToolAssignment);
            Assert.AreEqual(parameters.result,
                locationToolAssignmentValidator.Validate(
                    LocationToolAssignmentModel.GetModelFor(locationToolAssignment, new NullLocalizationWrapper())));
        }

        private static List<(string, Action<LocationToolAssignment>, bool)> monitoringIntervalData =
            new List<(string, Action<LocationToolAssignment>, bool)>
            {
                (nameof(Interval.IntervalValue),
                    (locationToolAssignment) => locationToolAssignment.TestLevelSetChk.TestLevel1.TestInterval.IntervalValue = 0, true),
                (nameof(Interval.IntervalValue),
                    (locationToolAssignment) => locationToolAssignment.TestLevelSetChk.TestLevel1.TestInterval.IntervalValue = 1, true),
                (nameof(Interval.IntervalValue),
                    (locationToolAssignment) => locationToolAssignment.TestLevelSetChk.TestLevel1.TestInterval.IntervalValue = 900, true),
                (nameof(Interval.IntervalValue),
                    (locationToolAssignment) => locationToolAssignment.TestLevelSetChk.TestLevel1.TestInterval.IntervalValue = 901, true)
            };


        [TestCaseSource(nameof(monitoringIntervalData))]
        public void ValidateForMonitoringIntervalReturnsCorrectValue((string propertieName, Action<LocationToolAssignment> changeLocationToolAssignment, bool result) parameters)
        {
            var locationToolAssignmentValidator = new LocationToolAssignmentValidator();
            var locationToolAssignment = CreateValidLocationToolAssignent();
            parameters.changeLocationToolAssignment?.Invoke(locationToolAssignment);
            Assert.AreEqual(parameters.result,
                locationToolAssignmentValidator.Validate(
                    LocationToolAssignmentModel.GetModelFor(locationToolAssignment, new NullLocalizationWrapper())));
        }

        public LocationToolAssignment CreateValidLocationToolAssignent()
        {
            LocationToolAssignment entity = CreateLocationToolAssignment.Anonymous();
            entity.TestTechnique.EndCycleTime = 0.5;
            entity.TestTechnique.FilterFrequency = 110;
            entity.TestTechnique.TorqueCoefficient = 0.2;
            entity.TestTechnique.MinimumPulse = 1;
            entity.TestTechnique.MaximumPulse = 1;
            entity.TestTechnique.Threshold = 1;
            entity.TestParameters.ThresholdTorque = Angle.FromDegree(1);
            entity.TestLevelSetChk = new TestLevelSet()
            {
                Id = new TestLevelSetId(1),
                Name = new TestLevelSetName("gzfuirdoepü"),
                TestLevel1 = new TestLevel()
                {
                    Id = new TestLevelId(2),
                    SampleNumber = 6,
                    TestInterval = new Interval(),
                    IsActive = true
                }
            };
            entity.TestLevelSetMfu = new TestLevelSet()
            {
                Id = new TestLevelSetId(1),
                Name = new TestLevelSetName("gzfuirdoepü"),
                TestLevel1 = new TestLevel()
                {
                    Id = new TestLevelId(2),
                    SampleNumber = 6,
                    TestInterval = new Interval(),
                    IsActive = true
                }
            };
            entity.TestLevelNumberChk = 1;
            entity.TestLevelNumberMfu = 1;
            return entity;
        }
    }
}