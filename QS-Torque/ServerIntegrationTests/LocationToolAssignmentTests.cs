using System;
using System.Collections.Generic;
using System.Linq;
using Core.Diffs;
using Core.Entities;
using Core.Entities.ReferenceLink;
using Core.Enums;
using Core.PhysicalValueTypes;
using FrameworksAndDrivers.RemoteData.GRPC.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestHelper.Mock;

namespace ServerIntegrationTests
{
    [TestClass]
    public class LocationToolAssignmentTests
    {
        private readonly TestSetup _testSetup;

        public LocationToolAssignmentTests()
        {
            _testSetup = new TestSetup();
        }

        [TestMethod]
        public void AssignToolToLocation()
        {
            var dataAccess = new LocationToolAssignmentDataAccess(_testSetup.ClientFactory, new MockLocationDisplayFormatter(), new TimeDataAccessMock());

            var newLocationToolAssignment = AddNewLocationToolAssignmentForTest(dataAccess,true);
            var locationToolAssignments = dataAccess.GetLocationToolAssignmentsByIds(new List<LocationToolAssignmentId>(){newLocationToolAssignment.Id});
            var result = locationToolAssignments.Find(x => x.Id.ToLong() == newLocationToolAssignment.Id.ToLong());

            Assert.IsNotNull(result);
            Assert.IsTrue(newLocationToolAssignment.EqualsByContent(result));
        }

        [TestMethod]
        public void RemoveLocationToolAssignment()
        {
            var dataAccess = new LocationToolAssignmentDataAccess(_testSetup.ClientFactory, new MockLocationDisplayFormatter(), new TimeDataAccessMock());
            var newLocationToolAssignment = AddNewLocationToolAssignmentForTest(dataAccess, true);

            var locationToolAssignmentsIds = dataAccess.LoadLocationToolAssignments().Select(x => x.Id.ToLong()).ToList();
            Assert.IsTrue(locationToolAssignmentsIds.Contains(newLocationToolAssignment.Id.ToLong()));

            dataAccess.RemoveLocationToolAssignment(newLocationToolAssignment, _testSetup.TestUser);

            locationToolAssignmentsIds = dataAccess.LoadLocationToolAssignments().Select(x => x.Id.ToLong()).ToList();
            Assert.IsFalse(locationToolAssignmentsIds.Contains(newLocationToolAssignment.Id.ToLong()));
        }

        [TestMethod]
        public void GetLocationToolAssignmentsByIds()
        {
            var dataAccess = new LocationToolAssignmentDataAccess(_testSetup.ClientFactory, new MockLocationDisplayFormatter(), new TimeDataAccessMock());

            var newLocationToolAssignment = AddNewLocationToolAssignmentForTest(dataAccess, true);

            var locationToolAssignments = dataAccess.GetLocationToolAssignmentsByIds(new List<LocationToolAssignmentId>() { newLocationToolAssignment.Id });
            var result =
                locationToolAssignments.SingleOrDefault(x => x.Id.ToLong() == newLocationToolAssignment.Id.ToLong());

            Assert.IsNotNull(result);
            Assert.IsTrue(newLocationToolAssignment.EqualsByContent(result));
        }

        [TestMethod]
        public void LoadLocationToolAssignments()
        {
            var dataAccess = new LocationToolAssignmentDataAccess(_testSetup.ClientFactory, new MockLocationDisplayFormatter(), new TimeDataAccessMock());

            var newLocationToolAssignment = AddNewLocationToolAssignmentForTest(dataAccess, true);

            var locationToolAssignmentsIds = dataAccess.LoadLocationToolAssignments();

            var result =
                locationToolAssignmentsIds.SingleOrDefault(x => x.Id.ToLong() == newLocationToolAssignment.Id.ToLong());

            Assert.IsNotNull(result);
            Assert.IsTrue(newLocationToolAssignment.EqualsByContent(result));
        }

        [TestMethod]
        public void LoadUnusedToolUsagesForLocation()
        {
            var dataAccess = new LocationToolAssignmentDataAccess(_testSetup.ClientFactory, new MockLocationDisplayFormatter(), new TimeDataAccessMock());

            var newLocationToolAssignment = AddNewLocationToolAssignmentForTest(dataAccess);
            var toolUsage = TestDataCreator.CreateToolUsage(_testSetup, "tu_" + DateTime.Now.Ticks);

            var unusedToolUsages = dataAccess.LoadUnusedToolUsagesForLocation(newLocationToolAssignment.AssignedLocation.Id);

            Assert.IsNull(unusedToolUsages.SingleOrDefault(x => x.ListId.ToLong() == newLocationToolAssignment.ToolUsage.ListId.ToLong()));
            Assert.IsNotNull(unusedToolUsages.SingleOrDefault(x => x.ListId.ToLong() == toolUsage.ListId.ToLong()));
        }

        [TestMethod]
        public void LoadAssignedToolsForLocation()
        {
            var dataAccess = new LocationToolAssignmentDataAccess(_testSetup.ClientFactory, new MockLocationDisplayFormatter(), new TimeDataAccessMock());
            
            var newLocationToolAssignment = AddNewLocationToolAssignmentForTest(dataAccess, true);

            var locationToolAssignments = dataAccess.LoadAssignedToolsForLocation(newLocationToolAssignment.AssignedLocation.Id);

            Assert.AreEqual(1, locationToolAssignments.Count);
            var result =
                locationToolAssignments.SingleOrDefault(x => x.Id.ToLong() == newLocationToolAssignment.Id.ToLong());

            Assert.IsNotNull(result);
            Assert.IsTrue(newLocationToolAssignment.EqualsByContent(result));
        }

        [TestMethod]
        public void AddTestConditions()
        {
            var dataAccess = new LocationToolAssignmentDataAccess(_testSetup.ClientFactory, new MockLocationDisplayFormatter(), new TimeDataAccessMock());
            var newLocationToolAssignment = AddNewLocationToolAssignmentForTest(dataAccess, false);
            var locationToolAssignment = dataAccess.GetLocationToolAssignmentsByIds(new List<LocationToolAssignmentId>() { newLocationToolAssignment.Id }).First();

            Assert.IsNull(locationToolAssignment.TestParameters);

            var toleranceClass = TestDataCreator.CreateToleranceClass(_testSetup, "T_" + DateTime.Now.Ticks);
            locationToolAssignment.TestParameters = new TestParameters
            {
                ToleranceClassAngle = toleranceClass, 
                ToleranceClassTorque = toleranceClass
            };
            locationToolAssignment.TestTechnique = new TestTechnique();

            dataAccess.AddTestConditions(locationToolAssignment, _testSetup.TestUser);

            var updatedLocationToolAssignment = dataAccess.GetLocationToolAssignmentsByIds(new List<LocationToolAssignmentId>() { newLocationToolAssignment.Id }).First();
            Assert.IsNotNull(updatedLocationToolAssignment.TestParameters);
            Assert.IsTrue(locationToolAssignment.TestParameters.EqualsByContent(updatedLocationToolAssignment.TestParameters));
            Assert.IsTrue(locationToolAssignment.EqualsByContent(updatedLocationToolAssignment));
        }

        [TestMethod]
        public void LoadLocationReferenceLinksForTool()
        {
            var dataAccess = new LocationToolAssignmentDataAccess(_testSetup.ClientFactory, new MockLocationDisplayFormatter(), new TimeDataAccessMock());
           
            var newLocationToolAssignment = AddNewLocationToolAssignmentForTest(dataAccess, false);

            var referenceLinks = dataAccess.LoadLocationReferenceLinksForTool(newLocationToolAssignment.AssignedTool.Id);
            Assert.AreEqual(1, referenceLinks.Count);
            LocationTests.CheckLocationReferenceLink(referenceLinks, newLocationToolAssignment.AssignedLocation);
        }

        [TestMethod]
        public void UpdateLocationToolAssignment()
        {
            var dataAccess = new LocationToolAssignmentDataAccess(_testSetup.ClientFactory, new MockLocationDisplayFormatter(), new TimeDataAccessMock());

            var newLocationToolAssignment = AddNewLocationToolAssignmentForTest(dataAccess, true);

            var changedToolAssignment = newLocationToolAssignment.CopyDeep();
            changedToolAssignment.TestParameters.MaximumAngle = Angle.FromDegree(newLocationToolAssignment.TestParameters.MaximumAngle.Degree);
            changedToolAssignment.TestParameters.MinimumAngle = Angle.FromDegree(newLocationToolAssignment.TestParameters.MinimumAngle.Degree);
            changedToolAssignment.TestParameters.SetPointAngle = Angle.FromDegree(newLocationToolAssignment.TestParameters.SetPointAngle.Degree);
            changedToolAssignment.TestParameters.MaximumTorque = Torque.FromNm(newLocationToolAssignment.TestParameters.MaximumTorque.Nm);
            changedToolAssignment.TestParameters.MinimumTorque = Torque.FromNm(newLocationToolAssignment.TestParameters.MinimumTorque.Nm);
            changedToolAssignment.TestParameters.MaximumTorque = Torque.FromNm(newLocationToolAssignment.TestParameters.MaximumTorque.Nm);
            changedToolAssignment.TestLevelSetMfu = TestDataCreator.CreateTestLevelSetMfu(_testSetup);
            changedToolAssignment.TestLevelSetChk = TestDataCreator.CreateTestLevelSetChk(_testSetup);
            changedToolAssignment.TestLevelNumberMfu = 15;
            changedToolAssignment.TestLevelNumberChk = 19;
            changedToolAssignment.StartDateMfu = DateTime.Today;
            changedToolAssignment.StartDateChk = DateTime.Today.AddDays(2);
            changedToolAssignment.TestOperationActiveMfu = true;
            changedToolAssignment.TestOperationActiveChk = true;
            

            changedToolAssignment.AssignedTool = TestDataCreator.CreateTool(_testSetup, "t_" + DateTime.Now.Ticks);

            var diff = new LocationToolAssignmentDiff
            {
                OldLocationToolAssignment = newLocationToolAssignment,
                NewLocationToolAssignment = changedToolAssignment,
                User = _testSetup.TestUser
            };
            dataAccess.UpdateLocationToolAssignment(new List<LocationToolAssignmentDiff>() { diff });

            var locationToolAssignment = dataAccess.LoadAssignedToolsForLocation(newLocationToolAssignment.AssignedLocation.Id).First();

            Assert.AreEqual(changedToolAssignment.TestParameters.MaximumAngle.Degree, locationToolAssignment.TestParameters.MaximumAngle.Degree);
            Assert.AreEqual(changedToolAssignment.TestParameters.MinimumAngle.Degree, locationToolAssignment.TestParameters.MinimumAngle.Degree);
            Assert.AreEqual(changedToolAssignment.TestParameters.SetPointAngle.Degree, locationToolAssignment.TestParameters.SetPointAngle.Degree);
            Assert.AreEqual(changedToolAssignment.TestParameters.MinimumTorque.Nm, locationToolAssignment.TestParameters.MinimumTorque.Nm);
            Assert.AreEqual(changedToolAssignment.TestParameters.MaximumTorque.Nm, locationToolAssignment.TestParameters.MaximumTorque.Nm);
            Assert.AreEqual(changedToolAssignment.TestParameters.SetPointTorque.Nm, locationToolAssignment.TestParameters.SetPointTorque.Nm);
            Assert.AreEqual(changedToolAssignment.TestLevelSetMfu.Id.ToLong(), locationToolAssignment.TestLevelSetMfu.Id.ToLong());
            Assert.AreEqual(changedToolAssignment.TestLevelSetChk.Id.ToLong(), locationToolAssignment.TestLevelSetChk.Id.ToLong());
            Assert.AreEqual(changedToolAssignment.TestLevelNumberMfu, locationToolAssignment.TestLevelNumberMfu);
            Assert.AreEqual(changedToolAssignment.TestLevelNumberChk, locationToolAssignment.TestLevelNumberChk);
            Assert.AreEqual(changedToolAssignment.StartDateMfu, locationToolAssignment.StartDateMfu);
            Assert.AreEqual(changedToolAssignment.StartDateChk, locationToolAssignment.StartDateChk);
            Assert.AreEqual(changedToolAssignment.TestOperationActiveMfu, locationToolAssignment.TestOperationActiveMfu);
            Assert.AreEqual(changedToolAssignment.TestOperationActiveChk, locationToolAssignment.TestOperationActiveChk);
            Assert.AreEqual(changedToolAssignment.AssignedTool.Id.ToLong(), locationToolAssignment.AssignedTool.Id.ToLong());
        }

        private LocationToolAssignment AddNewLocationToolAssignmentForTest(LocationToolAssignmentDataAccess dataAccess, bool withCond = false)
        {
            var locationToolAssignmentsBefore = dataAccess.LoadLocationToolAssignments();
            
            var newLocationToolAssignment = TestDataCreator.CreateLocationToolAssignment(_testSetup, withCond);

            Assert.IsNull(locationToolAssignmentsBefore.SingleOrDefault(x => x.Id.ToLong() == newLocationToolAssignment.Id.ToLong()));

            return newLocationToolAssignment;
        }

        public static void CheckLocationReferenceLink(LocationToolAssignmentReferenceLink locationReference, Tool tool, Location location)
        {
            Assert.IsNotNull(locationReference);
            Assert.AreEqual(location.Id.ToLong(), locationReference.LocationId.ToLong());
            Assert.AreEqual(location.Description.ToDefaultString(), locationReference.LocationName.ToDefaultString());
            Assert.AreEqual(location.Number.ToDefaultString(), locationReference.LocationNumber.ToDefaultString());
            Assert.AreEqual(tool.Id.ToLong(), locationReference.ToolId.ToLong());
            Assert.AreEqual(tool.SerialNumber.ToDefaultString(), locationReference.ToolSerialNumber);
            Assert.AreEqual(tool.InventoryNumber.ToDefaultString(), locationReference.ToolInventoryNumber);
        }
    }
}
