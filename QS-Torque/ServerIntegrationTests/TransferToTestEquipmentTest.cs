using System;
using System.Collections.Generic;
using System.Linq;
using Client.Core.Diffs;
using Core.Diffs;
using Core.Entities;
using Core.Enums;
using Core.UseCases.Communication;
using FrameworksAndDrivers.RemoteData.GRPC.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestHelper.Mock;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace ServerIntegrationTests
{
    [TestClass]
    public class TransferToTestEquipmentTest
    {
        private readonly TestSetup _testSetup;

        public TransferToTestEquipmentTest()
        {
            _testSetup = new TestSetup();
        }

        [TestMethod]
        public void LoadProcessControlDataForTransfer()
        {
            var processControlDataAccess = new ProcessControlDataAccess(_testSetup.ClientFactory);

            var dataAccess = new TransferToTestEquipmentDataAccess(_testSetup.ClientFactory, new TimeDataAccessMock(), null);

            var location = TestDataCreator.CreateLocation(_testSetup, "loc_" + DateTime.Now);
            var processControl = TestDataCreator.CreateProcessControlCondition(_testSetup, location);
            var changedProcessControl = processControl.CopyDeep();
            processControl.TestOperationActive = true;
            changedProcessControl.TestOperationActive = false;

            var diff = new ProcessControlConditionDiff(_testSetup.TestUser, new HistoryComment(""), processControl,
                changedProcessControl);

            processControlDataAccess.SaveProcessControlCondition(new List<ProcessControlConditionDiff> { diff });

            var processControlForTransfers = dataAccess.LoadProcessControlDataForTransfer();

            var result = processControlForTransfers.SingleOrDefault(x =>
                x.ProcessControlConditionId.ToLong() == processControl.Id.ToLong());

            Assert.IsNull(result);

            processControl.TestOperationActive = false;
            changedProcessControl.TestOperationActive = true;

            diff = new ProcessControlConditionDiff(_testSetup.TestUser, new HistoryComment(""), processControl,
                changedProcessControl);

            processControlDataAccess.SaveProcessControlCondition(new List<ProcessControlConditionDiff>() { diff });

            processControlForTransfers = dataAccess.LoadProcessControlDataForTransfer();

            result = processControlForTransfers.SingleOrDefault(x =>
                x.ProcessControlConditionId.ToLong() == processControl.Id.ToLong());

            Assert.IsNotNull(result);
            Assert.AreEqual(changedProcessControl.Location.Id.ToLong(), result.LocationId.ToLong());
            Assert.AreEqual(changedProcessControl.Id.ToLong(), result.ProcessControlConditionId.ToLong());
            Assert.AreEqual(changedProcessControl.ProcessControlTech.Id.ToLong(), result.ProcessControlTechId.ToLong());
            Assert.AreEqual(location.Number.ToDefaultString(), result.LocationNumber.ToDefaultString());
            Assert.AreEqual(location.Description.ToDefaultString(), result.LocationDescription.ToDefaultString());
            Assert.AreEqual(changedProcessControl.LowerMeasuringLimit.Nm, result.MinimumTorque.Nm);
            Assert.AreEqual(changedProcessControl.UpperMeasuringLimit.Nm, result.MaximumTorque.Nm);
            Assert.AreEqual((changedProcessControl.LowerMeasuringLimit.Nm + changedProcessControl.UpperMeasuringLimit.Nm) / 2, result.SetPointTorque.Nm);
            Assert.AreEqual(changedProcessControl.TestLevelSet.TestLevel3.TestInterval.IntervalValue, result.TestInterval.IntervalValue);
            Assert.AreEqual(changedProcessControl.TestLevelSet.TestLevel3.TestInterval.Type, result.TestInterval.Type);
            Assert.AreEqual(changedProcessControl.TestLevelSet.TestLevel3.SampleNumber, result.SampleNumber);
        }

        [TestMethod]
        public void LoadLocationToolAssignmentsForTransfer()
        {
            var locationToolAssignmentDataAccess = new LocationToolAssignmentDataAccess(_testSetup.ClientFactory,
                new MockLocationDisplayFormatter(), new TimeDataAccessMock());

            var dataAccess = new TransferToTestEquipmentDataAccess(_testSetup.ClientFactory, new TimeDataAccessMock(),
                locationToolAssignmentDataAccess);

            var newLocationToolAssignment = TestDataCreator.CreateLocationToolAssignment(_testSetup, true);

            LoadLocationToolAssignmentsForTransferCheckTestType(locationToolAssignmentDataAccess, dataAccess, TestType.Chk, newLocationToolAssignment);
            LoadLocationToolAssignmentsForTransferCheckTestType(locationToolAssignmentDataAccess, dataAccess, TestType.Mfu, newLocationToolAssignment);
        }

        private void LoadLocationToolAssignmentsForTransferCheckTestType(LocationToolAssignmentDataAccess locationToolAssignmentDataAccess, TransferToTestEquipmentDataAccess dataAccess, TestType testType, Core.Entities.LocationToolAssignment newLocationToolAssignment)
        {
            var changedToolAssignment = newLocationToolAssignment.CopyDeep();
            newLocationToolAssignment.TestOperationActiveMfu = true;
            newLocationToolAssignment.TestOperationActiveChk = true;
            changedToolAssignment.TestOperationActiveMfu = false;
            changedToolAssignment.TestOperationActiveChk = false;

            var diff = new LocationToolAssignmentDiff
            {
                OldLocationToolAssignment = newLocationToolAssignment,
                NewLocationToolAssignment = changedToolAssignment,
                User = _testSetup.TestUser
            };
            locationToolAssignmentDataAccess.UpdateLocationToolAssignment(new List<LocationToolAssignmentDiff>() { diff });

            var loadLocationToolAssignmentsForTransferData = dataAccess.LoadLocationToolAssignmentsForTransfer(testType);

            var result = loadLocationToolAssignmentsForTransferData.SingleOrDefault(x =>
                x.LocationToolAssignmentId.ToLong() == newLocationToolAssignment.Id.ToLong());

            Assert.IsNull(result);
            
            diff.OldLocationToolAssignment.TestOperationActiveMfu = false;
            diff.OldLocationToolAssignment.TestOperationActiveChk = false;
            diff.NewLocationToolAssignment.TestOperationActiveMfu = true;
            diff.NewLocationToolAssignment.TestOperationActiveChk = true;

            locationToolAssignmentDataAccess.UpdateLocationToolAssignment(new List<LocationToolAssignmentDiff>() { diff });

            loadLocationToolAssignmentsForTransferData = dataAccess.LoadLocationToolAssignmentsForTransfer(testType);

            result = loadLocationToolAssignmentsForTransferData.Single(x =>
                x.LocationToolAssignmentId.ToLong() == newLocationToolAssignment.Id.ToLong());

            Assert.AreEqual(newLocationToolAssignment.Id.ToLong(), result.LocationToolAssignmentId.ToLong());
            Assert.AreEqual(newLocationToolAssignment.AssignedLocation.Id.ToLong(), result.LocationId.ToLong());
            Assert.AreEqual(newLocationToolAssignment.AssignedLocation.Description.ToDefaultString(), result.LocationDescription.ToDefaultString());
            Assert.AreEqual(newLocationToolAssignment.AssignedLocation.ConfigurableField2.ToDefaultString(), result.LocationFreeFieldCategory);
            Assert.AreEqual(newLocationToolAssignment.AssignedLocation.ConfigurableField3, result.LocationFreeFieldDocumentation);
            Assert.AreEqual(newLocationToolAssignment.AssignedLocation.Number.ToDefaultString(), result.LocationNumber.ToDefaultString());
            Assert.AreEqual(newLocationToolAssignment.AssignedTool.Id.ToLong(), result.ToolId.ToLong());
            Assert.AreEqual(newLocationToolAssignment.AssignedTool.SerialNumber.ToDefaultString(), result.ToolSerialNumber);
            Assert.AreEqual(newLocationToolAssignment.AssignedTool.InventoryNumber.ToDefaultString(), result.ToolInventoryNumber);
            Assert.AreEqual(newLocationToolAssignment.ToolUsage.ListId.ToLong(), result.ToolUsageId.ToLong());
            Assert.AreEqual(newLocationToolAssignment.ToolUsage.Value.ToDefaultString(), result.ToolUsageDescription.ToDefaultString());
            
            if (testType == TestType.Chk)
            {
                Assert.AreEqual(newLocationToolAssignment.TestLevelSetChk.TestLevel3.TestInterval.IntervalValue, result.TestInterval.IntervalValue);
                Assert.AreEqual(newLocationToolAssignment.TestLevelSetChk.TestLevel3.TestInterval.Type, result.TestInterval.Type);
                Assert.AreEqual(newLocationToolAssignment.TestLevelSetChk.TestLevel3.SampleNumber, result.SampleNumber);
            }
            else if (testType == TestType.Mfu)
            {
                Assert.AreEqual(newLocationToolAssignment.TestLevelSetMfu.TestLevel2.TestInterval.IntervalValue, result.TestInterval.IntervalValue);
                Assert.AreEqual(newLocationToolAssignment.TestLevelSetMfu.TestLevel2.TestInterval.Type, result.TestInterval.Type);
                Assert.AreEqual(newLocationToolAssignment.TestLevelSetMfu.TestLevel2.SampleNumber, result.SampleNumber);
            }
        }

        [TestMethod]
        public void FillWithLocationToolAssignmentsData()
        {
            var locationToolAssignmentDataAccess = new LocationToolAssignmentDataAccess(_testSetup.ClientFactory,
                new MockLocationDisplayFormatter(), new TimeDataAccessMock());

            var dataAccess = new TransferToTestEquipmentDataAccess(_testSetup.ClientFactory, new TimeDataAccessMock(),
                locationToolAssignmentDataAccess);

            var dataGateResult = new DataGateResults()
            {
                Results = new List<DataGateResult>()
                {
                    new DataGateResult()
                    {
                        LocationToolAssignmentId = -1
                    }
                }
            };

            var result = dataAccess.FillWithLocationToolAssignmentsData(dataGateResult);

            Assert.AreEqual(0, result.Count);

            var newLocationToolAssignment = TestDataCreator.CreateLocationToolAssignment(_testSetup, true);
            dataGateResult.Results.First().LocationToolAssignmentId = newLocationToolAssignment.Id.ToLong();

            var testEquipmentTestResult = dataAccess.FillWithLocationToolAssignmentsData(dataGateResult).First();

            Assert.IsNotNull(testEquipmentTestResult);

            Assert.AreEqual(newLocationToolAssignment.Id.ToLong(), testEquipmentTestResult.LocationToolAssignment.Id.ToLong());
            Assert.AreEqual(newLocationToolAssignment.AssignedLocation.Number.ToDefaultString(), testEquipmentTestResult.LocationNumber.ToDefaultString());
            Assert.AreEqual(newLocationToolAssignment.AssignedLocation.Description.ToDefaultString(), testEquipmentTestResult.LocationDescription.ToDefaultString());
            Assert.AreEqual(newLocationToolAssignment.AssignedTool.InventoryNumber.ToDefaultString(), testEquipmentTestResult.ToolInventoryNumber);
            Assert.AreEqual(newLocationToolAssignment.AssignedTool.SerialNumber.ToDefaultString(), testEquipmentTestResult.ToolSerialNumber);
            Assert.AreEqual(666, testEquipmentTestResult.TestResult.LongValue);
            Assert.AreSame(dataGateResult.Results.First(), testEquipmentTestResult.ResultFromDataGate);
        }

        [TestMethod]
        public void SaveTestEquipmentTestResultChk()
        {
            var tests = TestDataCreator.CreateTestEquipmentChkTests(_testSetup);

            var newLocationToolAssignment = tests.Item3;
            var testEquipment = tests.Item1;
            var testEquipmentTestResults = tests.Item2;

            var classicTestDataAccess = new ClassicTestDataAccess(_testSetup.ClientFactory, new TimeDataAccessMock());

            var locationToolTests = classicTestDataAccess.GetClassicChkHeaderFromTool(newLocationToolAssignment.AssignedTool.Id,
                newLocationToolAssignment.AssignedLocation.Id);

            Assert.IsTrue(locationToolTests.Count > 0);
            var i = 0;
            foreach (var locationToolTest in locationToolTests)
            {
                ClassicTestTests.CheckTestEquipmentTestResultAndChkTest(testEquipmentTestResults[i], locationToolTest, testEquipment);

                var testValues = classicTestDataAccess.GetValuesFromClassicChkHeader(new List<ClassicChkTest>() {locationToolTest}).OrderBy(x => x.Position).ToList();
                var j = 0;
                foreach (var val in testValues)
                {
                    ClassicTestTests.CheckTestEquipmentAndChkTestValue(testEquipmentTestResults[i], val, j);
                    j++;
                }
                i++;
            }
        }

        [TestMethod]
        public void SaveTestEquipmentTestResultMfu()
        {
            var cmCmk = (1.77, 1.67);
            var tests = TestDataCreator.CreateTestEquipmentMfuTests(_testSetup, cmCmk);

            var testEquipment = tests.Item1;
            var testEquipmentTestResults = tests.Item2;
            var newLocationToolAssignment = tests.Item3;

            var classicTestDataAccess = new ClassicTestDataAccess(_testSetup.ClientFactory, new TimeDataAccessMock());

            var locationToolTests = classicTestDataAccess.GetClassicMfuHeaderFromTool(newLocationToolAssignment.AssignedTool.Id,
                newLocationToolAssignment.AssignedLocation.Id);

            Assert.IsTrue(locationToolTests.Count > 0);
            var i = 0;
            foreach (var locationToolTest in locationToolTests)
            {
                ClassicTestTests.CheckTestEquipmentTestResultAndMfuTest(testEquipmentTestResults[i], locationToolTest, testEquipment, cmCmk);

                var testValues = classicTestDataAccess.GetValuesFromClassicMfuHeader(new List<ClassicMfuTest>() { locationToolTest }).OrderBy(x => x.Position).ToList();
                var j = 0;
                foreach (var val in testValues)
                {
                    ClassicTestTests.CheckTestEquipmentAndMfuTestValue(testEquipmentTestResults[i], val, j);
                    j++;
                }
                i++;
            }
        }
    }
}
