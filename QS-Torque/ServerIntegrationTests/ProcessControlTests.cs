using Client.Core.Diffs;
using Client.Core.Entities;
using Core.Entities;
using Core.PhysicalValueTypes;
using FrameworksAndDrivers.RemoteData.GRPC.DataAccess;
using Microsoft.VisualBasic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Assert = NUnit.Framework.Assert;

namespace ServerIntegrationTests
{
    [TestClass]
    public class ProcessControlTests
    {
        private readonly TestSetup _testSetup;

        public ProcessControlTests()
        {
            _testSetup = new TestSetup();
        }


        [TestMethod]
        public void AddProcessControl()
        {
            var dataAccess = new ProcessControlDataAccess(_testSetup.ClientFactory);
            var location = TestDataCreator.CreateLocation(_testSetup, System.DateTime.Now.Ticks.ToString());
            var newProcessControlCondition = AddNewProcessControlConditionForTest(dataAccess, location);

            var result = dataAccess.LoadProcessControlConditionForLocation(location);

            Assert.IsTrue(newProcessControlCondition.Id.ToLong() != 0);
            Assert.IsTrue(newProcessControlCondition.EqualsByContent(result));
        }

        [TestMethod]
        public void SaveProcessControl()
        {
            var dataAccess = new ProcessControlDataAccess(_testSetup.ClientFactory);
            var location = TestDataCreator.CreateLocation(_testSetup, System.DateTime.Now.Ticks.ToString());
            var oldProcessControlCondition = AddNewProcessControlConditionForTest(dataAccess, location);

            var updatedProcessControl = oldProcessControlCondition.CopyDeep();
            updatedProcessControl.LowerInterventionLimit = Torque.FromNm(updatedProcessControl.LowerInterventionLimit.Nm + 1);
            updatedProcessControl.UpperInterventionLimit = Torque.FromNm(updatedProcessControl.UpperInterventionLimit.Nm + 11);
            updatedProcessControl.LowerMeasuringLimit = Torque.FromNm(updatedProcessControl.LowerMeasuringLimit.Nm + 12);
            updatedProcessControl.UpperMeasuringLimit = Torque.FromNm(updatedProcessControl.UpperMeasuringLimit.Nm + 15);
            var tech = ((QstProcessControlTech)updatedProcessControl.ProcessControlTech);
            tech.AlarmAngleMt = Angle.FromDegree(tech.AlarmAngleMt.Degree + 1);
            tech.AlarmAnglePa = Angle.FromDegree(tech.AlarmAnglePa.Degree + 12);
            tech.AngleForFurtherTurningPa = Angle.FromDegree(tech.AngleForFurtherTurningPa.Degree + 3);
            tech.AngleLimitMt = Angle.FromDegree(tech.AngleLimitMt.Degree + 4);
            tech.TargetAnglePa = Angle.FromDegree(tech.TargetAnglePa.Degree + 6);
            tech.AlarmTorqueMt = Torque.FromNm(tech.AlarmTorqueMt.Nm + 7);
            tech.AlarmTorquePa = Torque.FromNm(tech.AlarmTorquePa.Nm + 2);
            tech.MinimumTorqueMt = Torque.FromNm(tech.MinimumTorqueMt.Nm + 11);
            tech.StartAngleCountingPa = Torque.FromNm(tech.StartAngleCountingPa.Nm + 41);
            tech.StartAngleMt = Torque.FromNm(tech.StartAngleMt.Nm + 16);
            tech.StartMeasurementMt = Torque.FromNm(tech.StartMeasurementMt.Nm + 71);
            tech.StartMeasurementPa = Torque.FromNm(tech.StartMeasurementPa.Nm + 81);
            tech.StartMeasurementPeak = Torque.FromNm(tech.StartMeasurementPeak.Nm + 11);

            var diff = new ProcessControlConditionDiff(_testSetup.TestUser, new HistoryComment(""),
                oldProcessControlCondition, updatedProcessControl);

            dataAccess.SaveProcessControlCondition(new List<Client.Core.Diffs.ProcessControlConditionDiff>() { diff });

            var result = dataAccess.LoadProcessControlConditionForLocation(location);

            Assert.IsTrue(updatedProcessControl.EqualsByContent(result));
        }

        [TestMethod]
        public void RemoveProcessControl()
        {
            var dataAccess = new ProcessControlDataAccess(_testSetup.ClientFactory);
            var location = TestDataCreator.CreateLocation(_testSetup, System.DateTime.Now.Ticks.ToString());
            AddNewProcessControlConditionForTest(dataAccess, location);

            var result = dataAccess.LoadProcessControlConditionForLocation(location);
            Assert.IsNotNull(result);

            dataAccess.RemoveProcessControlCondition(result, _testSetup.TestUser);
            result = dataAccess.LoadProcessControlConditionForLocation(location);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void LoadProcessControlConditionForLocation()
        {
            var dataAccess = new ProcessControlDataAccess(_testSetup.ClientFactory);
            var location = TestDataCreator.CreateLocation(_testSetup, System.DateTime.Now.Ticks.ToString());
            var newProcessControlCondition = AddNewProcessControlConditionForTest(dataAccess, location);

            var result = dataAccess.LoadProcessControlConditionForLocation(location);

            Assert.IsTrue(newProcessControlCondition.EqualsByContent(result));
        }

        private ProcessControlCondition AddNewProcessControlConditionForTest(ProcessControlDataAccess dataAccess, Location location)
        {
            var condition = dataAccess.LoadProcessControlConditionForLocation(location);
            Assert.IsNull(condition);

            return TestDataCreator.CreateProcessControlCondition(_testSetup, location);
        }

    }
}
