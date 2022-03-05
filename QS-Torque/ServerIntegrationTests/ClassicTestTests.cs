using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.UseCases.Communication;
using FrameworksAndDrivers.RemoteData.GRPC.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestHelper.Mock;

namespace ServerIntegrationTests
{
    [TestClass]
    public class ClassicTestTests
    {
        private readonly TestSetup _testSetup;

        public ClassicTestTests()
        {
            _testSetup = new TestSetup();
        }

        [TestMethod]
        public void LoadToolsFromLocationTests()
        {
            var location = TestDataCreator.CreateLocation(_testSetup, "loc_" + DateTime.Now.Ticks);
            var tool1 = TestDataCreator.CreateTool(_testSetup, "t_" + DateTime.Now.Ticks);
            var tool2 = TestDataCreator.CreateTool(_testSetup, "t_" + DateTime.Now.Ticks);

            var locationToolAssignment1 = TestDataCreator.CreateLocationToolAssignment(_testSetup, true, location, tool1);
            var locationToolAssignment2 = TestDataCreator.CreateLocationToolAssignment(_testSetup, true, location, tool2);

            var tests1 = TestDataCreator.CreateTestEquipmentChkTests(_testSetup, locationToolAssignment1);
            var tests2 = TestDataCreator.CreateTestEquipmentChkTests(_testSetup, locationToolAssignment2);

            var dataAccess = new ClassicTestDataAccess(_testSetup.ClientFactory, new TimeDataAccessMock());
            
            var toolsFromLocationTests = dataAccess.LoadToolsFromLocationTests(location.Id);

            Assert.AreEqual(2, toolsFromLocationTests.Count);

            var data1 = toolsFromLocationTests.SingleOrDefault(x => x.Key.Id.ToLong() == tool1.Id.ToLong());
            Assert.IsTrue(tool1.EqualsByContent(data1.Key));
            Assert.AreEqual(tests1.Item2.Select(x => x.TestTimestamp).Min(), data1.Value.firsttest);
            Assert.AreEqual(tests1.Item2.Select(x => x.TestTimestamp).Max(), data1.Value.lasttest);
            Assert.IsTrue(data1.Value.isToolAssignmentActive);

            var data2 = toolsFromLocationTests.SingleOrDefault(x => x.Key.Id.ToLong() == tool2.Id.ToLong());
            Assert.IsTrue(tool2.EqualsByContent(data2.Key));
            Assert.AreEqual(tests2.Item2.Select(x => x.TestTimestamp).Min(), data2.Value.firsttest);
            Assert.AreEqual(tests2.Item2.Select(x => x.TestTimestamp).Max(), data2.Value.lasttest);
            Assert.IsTrue(data2.Value.isToolAssignmentActive);

            var locationToolAssignmentDataAccess = new LocationToolAssignmentDataAccess(_testSetup.ClientFactory,
                new MockLocationDisplayFormatter(), new TimeDataAccessMock());
            locationToolAssignmentDataAccess.RemoveLocationToolAssignment(locationToolAssignment1, _testSetup.TestUser);
            toolsFromLocationTests = dataAccess.LoadToolsFromLocationTests(location.Id);
            data1 = toolsFromLocationTests.SingleOrDefault(x => x.Key.Id.ToLong() == tool1.Id.ToLong());
            data2 = toolsFromLocationTests.SingleOrDefault(x => x.Key.Id.ToLong() == tool2.Id.ToLong());
            Assert.IsFalse(data1.Value.isToolAssignmentActive);
            Assert.IsTrue(data2.Value.isToolAssignmentActive);
        }

        [TestMethod]
        public void GetClassicChkHeaderFromTool()
        {
            var tests = TestDataCreator.CreateTestEquipmentChkTests(_testSetup);
            var testEquipmentTestResults = tests.Item2;
            var testEquipment = tests.Item1;
            var dataAccess = new ClassicTestDataAccess(_testSetup.ClientFactory, new TimeDataAccessMock());
            var chkTestsFromTool = dataAccess.GetClassicChkHeaderFromTool(tests.Item3.AssignedTool.Id, tests.Item3.AssignedLocation.Id);

            var i = 0;
            foreach (var locationToolTest in chkTestsFromTool)
            {
                CheckTestEquipmentTestResultAndChkTest(testEquipmentTestResults[i], locationToolTest, testEquipment);
                i++;
            }
        }

        [TestMethod]
        public void GetValuesFromClassicChkHeader()
        {
            var tests = TestDataCreator.CreateTestEquipmentChkTests(_testSetup);
            var testEquipmentTestResults = tests.Item2;
            var dataAccess = new ClassicTestDataAccess(_testSetup.ClientFactory, new TimeDataAccessMock());
            var chkTestsFromTool = dataAccess.GetClassicChkHeaderFromTool(tests.Item3.AssignedTool.Id, tests.Item3.AssignedLocation.Id);

            var i = 0;
            foreach (var locationToolTest in chkTestsFromTool)
            {
                var testValues = dataAccess.GetValuesFromClassicChkHeader(new List<ClassicChkTest>() { locationToolTest }).OrderBy(x => x.Position).ToList();
                var j = 0;
                foreach (var val in testValues)
                {
                    CheckTestEquipmentAndChkTestValue(testEquipmentTestResults[i], val, j);
                    j++;
                }
                i++;
            }
        }

        //TODO: CLassicProcessTest-Tests sobald das EInfügen Implementiert ist!

        [TestMethod]
        public void GetClassicMfuHeaderFromTool()
        {
            var cmCmk = (1.77, 1.67);
            var tests = TestDataCreator.CreateTestEquipmentMfuTests(_testSetup, cmCmk);
            var testEquipmentTestResults = tests.Item2;
            var testEquipment = tests.Item1;
            var dataAccess = new ClassicTestDataAccess(_testSetup.ClientFactory, new TimeDataAccessMock());
            var chkTestsFromTool = dataAccess.GetClassicMfuHeaderFromTool(tests.Item3.AssignedTool.Id, tests.Item3.AssignedLocation.Id);

            var i = 0;
            foreach (var locationToolTest in chkTestsFromTool)
            {
                CheckTestEquipmentTestResultAndMfuTest(testEquipmentTestResults[i], locationToolTest, testEquipment, cmCmk);
                i++;
            }
        }

        [TestMethod]
        public void GetValuesFromClassicMfuHeader()
        {
            var tests = TestDataCreator.CreateTestEquipmentMfuTests(_testSetup, (1.77, 1.67));
            var testEquipmentTestResults = tests.Item2;
            var dataAccess = new ClassicTestDataAccess(_testSetup.ClientFactory, new TimeDataAccessMock());
            var chkTestsFromTool = dataAccess.GetClassicMfuHeaderFromTool(tests.Item3.AssignedTool.Id, tests.Item3.AssignedLocation.Id);

            var i = 0;
            foreach (var locationToolTest in chkTestsFromTool)
            {
                var testValues = dataAccess.GetValuesFromClassicMfuHeader(new List<ClassicMfuTest>() { locationToolTest }).OrderBy(x => x.Position).ToList();
                var j = 0;
                foreach (var val in testValues)
                {
                    CheckTestEquipmentAndMfuTestValue(testEquipmentTestResults[i], val, j);
                    j++;
                }
                i++;
            }
        }

        public static void CheckTestEquipmentTestResultAndChkTest(TestEquipmentTestResult testEquipmentTestResult, ClassicChkTest classicChkTest, TestEquipment testEquipment)
        {
            Assert.IsTrue(classicChkTest.Id.ToLong() != 0);
            Assert.AreEqual(testEquipmentTestResult.ResultFromDataGate.Values.Count, classicChkTest.NumberOfTests);
            Assert.AreEqual(testEquipmentTestResult.Values.Min(), classicChkTest.TestValueMinimum);
            Assert.AreEqual(testEquipmentTestResult.Values.Max(), classicChkTest.TestValueMaximum);
            Assert.AreEqual(testEquipmentTestResult.Average, classicChkTest.Average);
            Assert.IsTrue((testEquipmentTestResult.StandardDeviation == null && classicChkTest.StandardDeviation == null)
                              || (testEquipmentTestResult.StandardDeviation.Value == classicChkTest.StandardDeviation.Value));
            Assert.AreEqual(testEquipmentTestResult.TestResult.LongValue, classicChkTest.Result.LongValue);
            Assert.AreEqual(testEquipmentTestResult.ResultFromDataGate.Min1, classicChkTest.LowerLimitUnit1);
            Assert.AreEqual(testEquipmentTestResult.ResultFromDataGate.Max1, classicChkTest.UpperLimitUnit1);
            Assert.AreEqual(testEquipmentTestResult.ResultFromDataGate.Min2, classicChkTest.LowerLimitUnit2);
            Assert.AreEqual(testEquipmentTestResult.ResultFromDataGate.Max2, classicChkTest.UpperLimitUnit2);
            Assert.AreEqual(testEquipmentTestResult.ResultFromDataGate.Nom1, classicChkTest.NominalValueUnit1);
            Assert.AreEqual(testEquipmentTestResult.ResultFromDataGate.Nom2, classicChkTest.NominalValueUnit2);
            Assert.AreEqual(testEquipmentTestResult.ResultFromDataGate.Unit1Id, (long)classicChkTest.Unit1Id);
            Assert.AreEqual(testEquipmentTestResult.ResultFromDataGate.Unit2Id, (long)classicChkTest.Unit2Id);
            Assert.AreEqual(testEquipmentTestResult.LocationToolAssignment.TestParameters.ToleranceClassTorque.Id.ToLong(), classicChkTest.ToleranceClassUnit1.Id.ToLong());
            Assert.AreEqual(testEquipmentTestResult.LocationToolAssignment.TestParameters.ToleranceClassAngle.Id.ToLong(), classicChkTest.ToleranceClassUnit2.Id.ToLong());
            Assert.AreEqual(testEquipmentTestResult.ControlUnit(), classicChkTest.ControlledByUnitId);
            Assert.AreEqual(testEquipment.Id.ToLong(), classicChkTest.TestEquipment.Id.ToLong());
            Assert.AreEqual(testEquipmentTestResult.LocationToolAssignment.TestParameters.ThresholdTorque.Degree, classicChkTest.ThresholdTorque);
            Assert.AreEqual(testEquipmentTestResult.LocationToolAssignment.AssignedLocation.Id.ToLong(), classicChkTest.TestLocation.LocationId.ToLong());
            Assert.AreEqual(testEquipmentTestResult.LocationToolAssignment.AssignedLocation.ParentDirectoryId.ToLong(), classicChkTest.TestLocation.LocationDirectoryId.ToLong());
            Assert.AreEqual(testEquipmentTestResult.LocationTreePath, classicChkTest.TestLocation.LocationTreePath);
            Assert.AreEqual(testEquipmentTestResult.TestTimestamp, classicChkTest.Timestamp);
        }

        public static void CheckTestEquipmentTestResultAndMfuTest(TestEquipmentTestResult testEquipmentTestResult, ClassicMfuTest classicChkTest, TestEquipment testEquipment, (double, double) cmCmk)
        {
            Assert.IsTrue(classicChkTest.Id.ToLong() != 0);
            Assert.AreEqual(testEquipmentTestResult.ResultFromDataGate.Values.Count, classicChkTest.NumberOfTests);
            Assert.AreEqual(testEquipmentTestResult.Values.Min(), classicChkTest.TestValueMinimum);
            Assert.AreEqual(testEquipmentTestResult.Values.Max(), classicChkTest.TestValueMaximum);
            Assert.AreEqual(testEquipmentTestResult.Average, classicChkTest.Average);
            Assert.IsTrue((testEquipmentTestResult.StandardDeviation == null && classicChkTest.StandardDeviation == null)
                              || (testEquipmentTestResult.StandardDeviation.Value == classicChkTest.StandardDeviation.Value));
            Assert.AreEqual(testEquipmentTestResult.TestResult.LongValue, classicChkTest.Result.LongValue);
            Assert.AreEqual(testEquipmentTestResult.ResultFromDataGate.Min1, classicChkTest.LowerLimitUnit1);
            Assert.AreEqual(testEquipmentTestResult.ResultFromDataGate.Max1, classicChkTest.UpperLimitUnit1);
            Assert.AreEqual(testEquipmentTestResult.ResultFromDataGate.Min2, classicChkTest.LowerLimitUnit2);
            Assert.AreEqual(testEquipmentTestResult.ResultFromDataGate.Max2, classicChkTest.UpperLimitUnit2);
            Assert.AreEqual(testEquipmentTestResult.ResultFromDataGate.Nom1, classicChkTest.NominalValueUnit1);
            Assert.AreEqual(testEquipmentTestResult.ResultFromDataGate.Nom2, classicChkTest.NominalValueUnit2);
            Assert.AreEqual(testEquipmentTestResult.ResultFromDataGate.Unit1Id, (long)classicChkTest.Unit1Id);
            Assert.AreEqual(testEquipmentTestResult.ResultFromDataGate.Unit2Id, (long)classicChkTest.Unit2Id);
            Assert.AreEqual(testEquipmentTestResult.LocationToolAssignment.TestParameters.ToleranceClassTorque.Id.ToLong(), classicChkTest.ToleranceClassUnit1.Id.ToLong());
            Assert.AreEqual(testEquipmentTestResult.LocationToolAssignment.TestParameters.ToleranceClassAngle.Id.ToLong(), classicChkTest.ToleranceClassUnit2.Id.ToLong());
            Assert.AreEqual(testEquipmentTestResult.ControlUnit(), classicChkTest.ControlledByUnitId);
            Assert.AreEqual(testEquipment.Id.ToLong(), classicChkTest.TestEquipment.Id.ToLong());
            Assert.AreEqual(testEquipmentTestResult.LocationToolAssignment.TestParameters.ThresholdTorque.Degree, classicChkTest.ThresholdTorque);
            Assert.AreEqual(testEquipmentTestResult.LocationToolAssignment.AssignedLocation.Id.ToLong(), classicChkTest.TestLocation.LocationId.ToLong());
            Assert.AreEqual(testEquipmentTestResult.LocationToolAssignment.AssignedLocation.ParentDirectoryId.ToLong(), classicChkTest.TestLocation.LocationDirectoryId.ToLong());
            Assert.AreEqual(testEquipmentTestResult.LocationTreePath, classicChkTest.TestLocation.LocationTreePath);
            Assert.AreEqual(testEquipmentTestResult.TestTimestamp, classicChkTest.Timestamp);
            Assert.AreEqual(testEquipmentTestResult.Cm, classicChkTest.Cm);
            Assert.AreEqual(testEquipmentTestResult.Cmk, classicChkTest.Cmk);
            Assert.AreEqual(cmCmk.Item1, classicChkTest.LimitCm);
            Assert.AreEqual(cmCmk.Item2, classicChkTest.LimitCmk);
        }

        public static void CheckTestEquipmentAndChkTestValue(TestEquipmentTestResult testEquipmentTestResult, ClassicChkTestValue val, int pos)
        {
            Assert.AreEqual(testEquipmentTestResult.ResultFromDataGate.Values[pos].Value1, val.ValueUnit1);
            Assert.AreEqual(testEquipmentTestResult.ResultFromDataGate.Values[pos].Value2, val.ValueUnit2);
            Assert.AreEqual(pos, val.Position);
        }

        public static void CheckTestEquipmentAndMfuTestValue(TestEquipmentTestResult testEquipmentTestResult, ClassicMfuTestValue val, int pos)
        {
            Assert.AreEqual(testEquipmentTestResult.ResultFromDataGate.Values[pos].Value1, val.ValueUnit1);
            Assert.AreEqual(testEquipmentTestResult.ResultFromDataGate.Values[pos].Value2, val.ValueUnit2);
            Assert.AreEqual(pos, val.Position);
        }
    }
}
