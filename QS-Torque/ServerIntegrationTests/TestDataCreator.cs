using System;
using System.Collections.Generic;
using System.Linq;
using Client.Core.Entities;
using Common.Types.Enums;
using Core.Entities;
using Core.Enums;
using Core.UseCases.Communication;
using FrameworksAndDrivers.Gui.Wpf.Formatter;
using FrameworksAndDrivers.RemoteData.GRPC.DataAccess;
using FrameworksAndDrivers.RemoteData.GRPC.Utils;
using TestHelper.Mock;

namespace ServerIntegrationTests
{
    public static class TestDataCreator
    {
        public static Location CreateLocation(TestSetup testSetup, string userid, ToleranceClass angle = null, ToleranceClass torque = null)
        {
            var dataAccess = new LocationDataAccess(testSetup.ClientFactory, new PictureFromZipLoader());

            var location = TestHelper.Factories.CreateLocation.WithIdNumberDescription(0, userid, "");
            location.Comment = null;
            if (angle == null)
            {
                var clas = CreateToleranceClass(testSetup, "tol_" + DateTime.Now.Ticks);
                location.ToleranceClassAngle = clas;
            }
            else
            {
                location.ToleranceClassAngle = angle;
            }

            if (torque == null)
            {
                var clas = CreateToleranceClass(testSetup, "tol_" + DateTime.Now.Ticks);
                location.ToleranceClassTorque = clas;
            }
            else
            {
                location.ToleranceClassTorque = torque;
            }


            var addedLocation = dataAccess.AddLocation(location, testSetup.TestUser);
            location.Id = addedLocation.Id;
            return location;
        }

        public static LocationDirectory CreateLocationDirectory(TestSetup testSetup, string name)
        {
            var dataAccess = new LocationDataAccess(testSetup.ClientFactory, new PictureFromZipLoader());

            var locationDirectory = TestHelper.Factories.CreateLocationDirectory.Parameterized(0, name, 11);
            var addedLocationDirectory = dataAccess.AddLocationDirectory(locationDirectory.ParentId, locationDirectory.Name.ToDefaultString(), testSetup.TestUser);
            return addedLocationDirectory;
        }

        public static ToleranceClass CreateToleranceClass(TestSetup testSetup, string name)
        {
            var dataAccess = new ToleranceClassDataAccess(testSetup.ClientFactory, null, new TimeDataAccessMock());

            var tolClass = TestHelper.Factories.CreateToleranceClass.Parametrized(0, name, true, 1.0, 11.1);

            var addedTolClass = dataAccess.AddToleranceClass(tolClass, testSetup.TestUser);
            tolClass.Id = addedTolClass.Id;
            return tolClass;
        }


        public static Tool CreateTool(TestSetup testSetup, string serialNumber, CostCenter costCenter = null, ConfigurableField configurableField = null, Status status = null, ToolModel toolModel = null)
        {
            var dataAccess = new ToolDataAccess(testSetup.ClientFactory, new DefaultFormatter(), new PictureFromZipLoader());

            var newTool = TestHelper.Factories.CreateTool.WithIdInventoryAndSerialNumber(0, "I", serialNumber);
            newTool.Comment = null;
            newTool.CostCenter = costCenter;
            newTool.ConfigurableField = configurableField;
            newTool.Status = status;

            newTool.ToolModel = toolModel;
            if (toolModel == null)
            {
                newTool.ToolModel = CreateToolModel(testSetup, "m_" + DateTime.Now.Ticks);
            }

            var addedTool = dataAccess.AddTool(newTool, testSetup.TestUser);
            newTool.Id = addedTool.Id;
            return newTool;
        }

        public static ToolModel CreateToolModel(TestSetup testSetup, string description)
        {
            var dataAccess = new ToolModelDataAccess(testSetup.ClientFactory, new MockToolDisplayFormatter());

            var newToolModel = TestHelper.Factories.CreateToolModel.Anonymous();
            newToolModel.Id = new ToolModelId(0);
            newToolModel.Description = new ToolModelDescription(description);
            newToolModel.Manufacturer = CreateManufacturer(testSetup, "m" + DateTime.Now.Ticks);

            var addToolModel = dataAccess.AddToolModel(newToolModel, testSetup.TestUser);
            newToolModel.Id = addToolModel.Id;
            return newToolModel;
        }

        public static Manufacturer CreateManufacturer(TestSetup testSetup, string name)
        {
            var dataAccess = new ManufacturerDataAccess(testSetup.ClientFactory);

            var manufacturer = global::TestHelper.Factories.CreateManufacturer.Parametrized(0, name, "Deggendorf",
                "Deutschland", "1234", "14", "Hans", "32143564", "94469",
                "Straße 1", null);

            var addedManufacturer = dataAccess.AddManufacturer(manufacturer, testSetup.TestUser);
            manufacturer.Id = addedManufacturer.Id;
            return manufacturer;
        }

        public static Extension CreateExtension(TestSetup testSetup, string inv)
        {
            var dataAccess = new ExtensionDataAccess(testSetup.ClientFactory, new DefaultFormatter(), new TimeDataAccessMock());

            var extension = Client.TestHelper.Factories.CreateExtension.Parametrized(0, inv, inv, 40, 1, 3);

            var addedExtension = dataAccess.AddExtension(extension, testSetup.TestUser);
            extension.Id = addedExtension.Id;
            return extension;
        }

        public static TestEquipment CreateTestEquipment(TestSetup testSetup, string inventoryAndSerialNumber, Status status = null)
        {
            var dataAccess = new TestEquipmentDataAccess(testSetup.ClientFactory);

            var testEquipment = global::TestHelper.Factories.CreateTestEquipment.RandomizedWithId(214234,0);
            testEquipment.SerialNumber = new TestEquipmentSerialNumber(inventoryAndSerialNumber);
            testEquipment.InventoryNumber = new TestEquipmentInventoryNumber(inventoryAndSerialNumber);
            testEquipment.Status = status;

            // TODO Replace with new TestEquipmentModel when is implemented
            testEquipment.TestEquipmentModel = dataAccess.LoadTestEquipmentModels().FirstOrDefault();

            var addedTestEquipment = dataAccess.AddTestEquipment(testEquipment, testSetup.TestUser);
            testEquipment.Id = addedTestEquipment.Id;
            return testEquipment;
        }

        public static ToolUsage CreateToolUsage(TestSetup testSetup, string name)
        {
            var dataAccess = new ToolUsageDataAccess(testSetup.ClientFactory);
            var toolUsage = new ToolUsage()
            {
                ListId = new HelperTableEntityId(0),
                Value = new ToolUsageDescription(name)
            };

            var addedToolUsageId = dataAccess.AddItem(toolUsage, testSetup.TestUser);
            toolUsage.ListId = addedToolUsageId;
            return toolUsage;
        }

        public static Status CreateStatus(TestSetup testSetup, string name)
        {
            var dataAccess = new StatusDataAccess(testSetup.ClientFactory, null);
            var status = new Status()
            {
                ListId = new HelperTableEntityId(0),
                Value = new StatusDescription(name)
            };

            var addedStatusId = dataAccess.AddItem(status, testSetup.TestUser);
            status.ListId = addedStatusId;
            return status;
        }

        public static LocationToolAssignment CreateLocationToolAssignment(TestSetup testSetup, bool withCond, Location location = null, Tool tool = null, ToleranceClass toleranceClassTorque = null, ToleranceClass toleranceClassAngle = null)
        {
            var locationToolAssignment = TestHelper.Factories.CreateLocationToolAssignment.IdOnly(0);
            locationToolAssignment.AssignedLocation = location ?? CreateLocation(testSetup, "loc_" + DateTime.Now.Ticks, toleranceClassAngle, toleranceClassTorque);
            locationToolAssignment.AssignedTool = tool ?? CreateTool(testSetup, "t_" + DateTime.Now.Ticks);
            locationToolAssignment.ToolUsage = CreateToolUsage(testSetup, "tu" + DateTime.Now.Ticks);
            locationToolAssignment.TestParameters.ToleranceClassAngle = locationToolAssignment.AssignedLocation.ToleranceClassAngle;
            locationToolAssignment.TestParameters.ToleranceClassTorque = locationToolAssignment.AssignedLocation.ToleranceClassTorque;

            if (!withCond)
            {
                locationToolAssignment.TestParameters = null;
            }
            
            var dataAccess = new LocationToolAssignmentDataAccess(testSetup.ClientFactory, new MockLocationDisplayFormatter(), new TimeDataAccessMock());
            
            var testLevelSetAssignmentDataAccess = new TestLevelSetAssignmentDataAccess(testSetup.ClientFactory);

            dataAccess.AssignToolToLocation(locationToolAssignment, testSetup.TestUser);

            var testLevelSetChk = CreateTestLevelSetChk(testSetup);
            var testLevelSetMfu = CreateTestLevelSetMfu(testSetup);
            testLevelSetAssignmentDataAccess.AssignTestLevelSetToLocationToolAssignments(testLevelSetMfu.Id, new List<(LocationToolAssignmentId, TestType)>()
            {
                (locationToolAssignment.Id, TestType.Mfu)
            }, testSetup.TestUser);
                testLevelSetAssignmentDataAccess.AssignTestLevelSetToLocationToolAssignments(testLevelSetChk.Id, new List<(LocationToolAssignmentId, TestType)>()
            {
                (locationToolAssignment.Id, TestType.Chk)
            }, testSetup.TestUser);
            locationToolAssignment.TestLevelSetMfu = testLevelSetMfu;
            locationToolAssignment.TestLevelSetChk = testLevelSetChk;

            var oldLocationToolAssignment = locationToolAssignment.CopyDeep();
            locationToolAssignment.TestLevelNumberMfu = 2;
            locationToolAssignment.TestLevelNumberChk = 3;
            dataAccess.UpdateLocationToolAssignment(new List<Core.Diffs.LocationToolAssignmentDiff>()
            {
                new Core.Diffs.LocationToolAssignmentDiff()
                {
                    OldLocationToolAssignment = oldLocationToolAssignment,
                    NewLocationToolAssignment = locationToolAssignment,
                    User = testSetup.TestUser
                }
            });

            return locationToolAssignment;
        }


        public static TestLevelSet CreateTestLevelSetChk(TestSetup testSetup)
        {
            var testLevelSetDataAccess = new TestLevelSetDataAccess(testSetup.ClientFactory);
            var testLevelSetChk = new TestLevelSet()
            {
                Id = new TestLevelSetId(DateTime.Now.Ticks * 10 + 4),
                Name = new TestLevelSetName("tls_chk_" + DateTime.Now.Ticks),
                TestLevel1 = new TestLevel() { Id = new TestLevelId(DateTime.Now.Ticks * 10 + 5), IsActive = true },
                TestLevel2 = new TestLevel() { Id = new TestLevelId(DateTime.Now.Ticks * 10 + 6), IsActive = true },
                TestLevel3 = new TestLevel() { Id = new TestLevelId(DateTime.Now.Ticks * 10 + 7), SampleNumber = 500, TestInterval = new Interval() { IntervalValue = 6, Type = IntervalType.XTimesAShift }, IsActive = true }
            };
            return testLevelSetDataAccess.AddTestLevelSet(new Client.Core.Diffs.TestLevelSetDiff() { New = testLevelSetChk, User = new User() { UserId = new UserId(0) } });
        }

        public static TestLevelSet CreateTestLevelSetMfu(TestSetup testSetup)
        {
            var testLevelSetDataAccess = new TestLevelSetDataAccess(testSetup.ClientFactory);
            var testLevelSetMfu = new TestLevelSet()
            {
                Id = new TestLevelSetId(DateTime.Now.Ticks * 10),
                Name = new TestLevelSetName("tls_mfu_" + DateTime.Now.Ticks),
                TestLevel1 = new TestLevel() { Id = new TestLevelId(DateTime.Now.Ticks * 10 + 1), IsActive = true },
                TestLevel2 = new TestLevel() { Id = new TestLevelId(DateTime.Now.Ticks * 10 + 2), SampleNumber = 14, TestInterval = new Interval() { IntervalValue = 6, Type = IntervalType.XTimesAShift }, IsActive = true },
                TestLevel3 = new TestLevel() { Id = new TestLevelId(DateTime.Now.Ticks * 10 + 3), IsActive = true }
            };

            return testLevelSetDataAccess.AddTestLevelSet(new Client.Core.Diffs.TestLevelSetDiff() { New = testLevelSetMfu, User = new User() { UserId = new UserId(0) } });
        }

        public static (TestEquipment, List<TestEquipmentTestResult>, LocationToolAssignment) CreateTestEquipmentChkTests(TestSetup testSetup, LocationToolAssignment locationToolAssignment = null)
        {
            var locationToolAssignmentDataAccess = new LocationToolAssignmentDataAccess(testSetup.ClientFactory,
                new MockLocationDisplayFormatter(), new TimeDataAccessMock());

            var dataAccess = new TransferToTestEquipmentDataAccess(testSetup.ClientFactory, new TimeDataAccessMock(),
                locationToolAssignmentDataAccess);

            var newLocationToolAssignment = locationToolAssignment ?? CreateLocationToolAssignment(testSetup, true);
            var testEquipmentTestResults = new List<TestEquipmentTestResult>();

            var date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour,
                DateTime.Now.Minute, DateTime.Now.Second);

            var testEquipmentTestResult = new TestEquipmentTestResult
            {
                LocationToolAssignment = newLocationToolAssignment,
                ResultFromDataGate = new DataGateResult
                {
                    Min1 = 12.5,
                    Max1 = 13.5,
                    Nom1 = 13,
                    Min2 = 122.5,
                    Max2 = 132.5,
                    Nom2 = 132,
                    LocationToolAssignmentId = newLocationToolAssignment.Id.ToLong(),
                    Unit1Id = (long)MeaUnit.Nm,
                    Unit2Id = (long)MeaUnit.Deg,
                    Values = new List<DataGateResultValue>
                    {
                        new DataGateResultValue {Value1 = 4.2, Value2 = 12.2, Timestamp = date.AddSeconds(1)},
                        new DataGateResultValue {Value1 = 4.5, Value2 = 213.5, Timestamp = date.AddSeconds(2)},
                        new DataGateResultValue {Value1 = 5.9, Value2 = 23.7, Timestamp = date.AddSeconds(3)},
                        new DataGateResultValue {Value1 = 6.0, Value2 = 2.8, Timestamp = date.AddSeconds(1)},
                        new DataGateResultValue {Value1 = 7.2, Value2 = 34.2, Timestamp = date.AddSeconds(3)},
                        new DataGateResultValue {Value1 = 7.8, Value2 = 44.2, Timestamp = date.AddSeconds(1)},
                    }
                },
                TestResult = new Core.Entities.TestResult(0),
                LocationTreePath = "Ordner/Halle"
            };
            testEquipmentTestResults.Add(testEquipmentTestResult);

            var testEquipmentTestResult2 = new TestEquipmentTestResult
            {
                LocationToolAssignment = newLocationToolAssignment,
                ResultFromDataGate = new DataGateResult
                {
                    Min1 = 15.5,
                    Max1 = 15.5,
                    Nom1 = 15,
                    Min2 = 125.5,
                    Max2 = 125.5,
                    Nom2 = 125,
                    LocationToolAssignmentId = newLocationToolAssignment.Id.ToLong(),
                    Unit1Id = (long)MeaUnit.Nm,
                    Unit2Id = (long)MeaUnit.Deg,
                    Values = new List<DataGateResultValue>
                    {
                        new DataGateResultValue {Value1 = 5.2, Value2 = 25.2, Timestamp = date.AddSeconds(3)},
                        new DataGateResultValue {Value1 = 15.5, Value2 = 25.5, Timestamp = date.AddSeconds(3)},
                        new DataGateResultValue {Value1 = 25.9, Value2 = 25.7, Timestamp = date.AddSeconds(2)},
                        new DataGateResultValue {Value1 = 35.0, Value2 = 25.8, Timestamp = date.AddSeconds(1)},
                        new DataGateResultValue {Value1 = 35.2, Value2 = 35.2, Timestamp = date.AddSeconds(1)},
                    }
                },
                TestResult = new Core.Entities.TestResult(1),
                LocationTreePath = "Werk/Ordner/Halle"
            };
            testEquipmentTestResults.Add(testEquipmentTestResult2);


            var testEquipment = TestDataCreator.CreateTestEquipment(testSetup, "t" + DateTime.Now.Ticks);

            dataAccess.SaveTestEquipmentTestResult(testEquipment, testEquipmentTestResults, (1.67, 1.66), testSetup.TestUser);

            return (testEquipment, testEquipmentTestResults, newLocationToolAssignment);
        }

        public static (TestEquipment, List<TestEquipmentTestResult>, LocationToolAssignment) CreateTestEquipmentMfuTests(TestSetup testSetup, (double, double) cmCmk, LocationToolAssignment locationToolAssignment = null)
        {
            var locationToolAssignmentDataAccess = new LocationToolAssignmentDataAccess(testSetup.ClientFactory,
                new MockLocationDisplayFormatter(), new TimeDataAccessMock());

            var dataAccess = new TransferToTestEquipmentDataAccess(testSetup.ClientFactory, new TimeDataAccessMock(),
                locationToolAssignmentDataAccess);

            var newLocationToolAssignment = locationToolAssignment ?? CreateLocationToolAssignment(testSetup, true);
            var testEquipmentTestResults = new List<TestEquipmentTestResult>();

            var testEquipmentTestResult = new TestEquipmentTestResult
            {
                LocationToolAssignment = newLocationToolAssignment,
                ResultFromDataGate = new DataGateResult
                {
                    Min1 = 2.5,
                    Max1 = 3.5,
                    Nom1 = 3,
                    Min2 = 22.5,
                    Max2 = 32.5,
                    Nom2 = 32,
                    LocationToolAssignmentId = newLocationToolAssignment.Id.ToLong(),
                    Unit1Id = (long)MeaUnit.Nm,
                    Unit2Id = (long)MeaUnit.Deg,
                    Values = new List<DataGateResultValue>
                    {
                        new DataGateResultValue {Value1 = 1.2, Value2 = 5.2, Timestamp = new DateTime(2021,1,1,2,3,4)},
                        new DataGateResultValue {Value1 = 1.5, Value2 = 6.5, Timestamp = new DateTime(2020,1,1,2,3,4)},
                        new DataGateResultValue {Value1 = 2.9, Value2 = 7.7, Timestamp = new DateTime(2019,1,1,2,3,4)},
                        new DataGateResultValue {Value1 = 3.0, Value2 = 8.8, Timestamp = new DateTime(2018,1,1,2,3,4)},
                        new DataGateResultValue {Value1 = 3.2, Value2 = 9.2, Timestamp = new DateTime(2017,1,1,2,3,4)},
                        new DataGateResultValue {Value1 = 3.8, Value2 = 19.2, Timestamp = new DateTime(2016,1,1,2,3,4)},
                        new DataGateResultValue {Value1 = 4.2, Value2 = 12.2, Timestamp = new DateTime(2015,1,1,2,3,4)},
                        new DataGateResultValue {Value1 = 4.5, Value2 = 213.5, Timestamp = new DateTime(2014,1,1,2,3,4)},
                        new DataGateResultValue {Value1 = 5.9, Value2 = 23.7, Timestamp = new DateTime(2013,1,1,2,3,4)},
                        new DataGateResultValue {Value1 = 6.0, Value2 = 2.8, Timestamp = new DateTime(2012,1,1,2,3,4)},
                        new DataGateResultValue {Value1 = 7.2, Value2 = 34.2, Timestamp = new DateTime(2011,1,1,2,3,4)},
                        new DataGateResultValue {Value1 = 7.8, Value2 = 44.2, Timestamp = new DateTime(2010,1,1,2,3,4)},
                    }
                },
                TestResult = new Core.Entities.TestResult(1),
                LocationTreePath = "Ordner/Halle"
            };
            testEquipmentTestResults.Add(testEquipmentTestResult);

            var testEquipmentTestResult2 = new TestEquipmentTestResult
            {
                LocationToolAssignment = newLocationToolAssignment,
                ResultFromDataGate = new DataGateResult
                {
                    Min1 = 25.5,
                    Max1 = 35.5,
                    Nom1 = 35,
                    Min2 = 225.5,
                    Max2 = 325.5,
                    Nom2 = 325,
                    LocationToolAssignmentId = newLocationToolAssignment.Id.ToLong(),
                    Unit1Id = (long)MeaUnit.Nm,
                    Unit2Id = (long)MeaUnit.Deg,
                    Values = new List<DataGateResultValue>
                    {
                        new DataGateResultValue {Value1 = 5.2, Value2 = 25.2, Timestamp = new DateTime(2021,1,1,2,3,4)},
                        new DataGateResultValue {Value1 = 15.5, Value2 = 25.5, Timestamp = new DateTime(2020,2,1,2,3,4)},
                        new DataGateResultValue {Value1 = 25.9, Value2 = 25.7, Timestamp = new DateTime(2019,3,1,2,3,4)},
                        new DataGateResultValue {Value1 = 35.0, Value2 = 25.8, Timestamp = new DateTime(2018,4,1,2,3,4)},
                        new DataGateResultValue {Value1 = 35.2, Value2 = 35.2, Timestamp = new DateTime(2017,5,1,2,3,4)},
                        new DataGateResultValue {Value1 = 35.8, Value2 = 45.2, Timestamp = new DateTime(2016,6,1,2,3,4)},
                        new DataGateResultValue {Value1 = 55.2, Value2 = 25.2, Timestamp = new DateTime(2001,1,1,2,3,4)},
                        new DataGateResultValue {Value1 = 165.5, Value2 = 254.5, Timestamp = new DateTime(2000,2,1,2,3,4)},
                        new DataGateResultValue {Value1 = 255.9, Value2 = 255.7, Timestamp = new DateTime(2009,3,1,2,3,4)},
                        new DataGateResultValue {Value1 = 365.0, Value2 = 257.8, Timestamp = new DateTime(2008,4,1,2,3,4)},
                        new DataGateResultValue {Value1 = 345.2, Value2 = 35.2, Timestamp = new DateTime(2007,5,1,2,3,4)},
                        new DataGateResultValue {Value1 = 345.8, Value2 = 245.2, Timestamp = new DateTime(2006,6,1,2,3,4)},
                    }
                },
                TestResult = new Core.Entities.TestResult(1),
                LocationTreePath = "Ordner/Halle"
            };
            testEquipmentTestResults.Add(testEquipmentTestResult2);

            var testEquipment = TestDataCreator.CreateTestEquipment(testSetup, "t" + DateTime.Now.Ticks);

            dataAccess.SaveTestEquipmentTestResult(testEquipment, testEquipmentTestResults, cmCmk, testSetup.TestUser);

            return (testEquipment, testEquipmentTestResults, newLocationToolAssignment);
        }


        public static ProcessControlCondition CreateProcessControlCondition(TestSetup testSetup, Location location)
        {
            var dataAccess = new ProcessControlDataAccess(testSetup.ClientFactory);

            var processControlCondition = Client.TestHelper.Factories.CreateProcessControlCondition.Randomized(23435);
            processControlCondition.Id = new ProcessControlConditionId(0);
            processControlCondition.ProcessControlTech.Id = new ProcessControlTechId(0);
            processControlCondition.ProcessControlTech.Extension = null;
            processControlCondition.ProcessControlTech.ProcessControlConditionId = new ProcessControlConditionId(0);
            processControlCondition.Location = location;
            processControlCondition.TestLevelSet = CreateTestLevelSetChk(testSetup);
            processControlCondition.TestLevelNumber = 3;

            var addedProcessControl = dataAccess.AddProcessControlCondition(processControlCondition, testSetup.TestUser);

            processControlCondition.Id = addedProcessControl.Id;
            processControlCondition.ProcessControlTech.Id = addedProcessControl.ProcessControlTech.Id;
            processControlCondition.ProcessControlTech.ProcessControlConditionId = addedProcessControl.Id;

            return processControlCondition;

        }
    }
}
