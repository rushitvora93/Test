using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Core.Entities;
using Core.UseCases;
using FrameworksAndDrivers.RemoteData.GRPC.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestHelper.Mock;

namespace ServerIntegrationTests
{
    [TestClass]
    public class ToleranceClassTest
    {
        private readonly TestSetup _testSetup;

        public ToleranceClassTest()
        {
            _testSetup = new TestSetup();
        }

        [TestMethod]
        public void AddToleranceClass()
        {
            var dataAccess = new ToleranceClassDataAccess(_testSetup.ClientFactory, null, new TimeDataAccessMock());
            var tolClass = AddNewToleranceClassForTest(dataAccess);

            var classes = dataAccess.LoadToleranceClasses();
            var result = classes.Find(x => x.Id.ToLong() == tolClass.Id.ToLong());

            Assert.IsTrue(tolClass.Id.ToLong() != 0);
            Assert.IsTrue(tolClass.EqualsByContent(result));
        }

        [TestMethod]
        public void SaveToleranceClass()
        {
            var dataAccess = new ToleranceClassDataAccess(_testSetup.ClientFactory, null, new TimeDataAccessMock());
            var oldTolClass = AddNewToleranceClassForTest(dataAccess);

            var updatedTolClass = global::TestHelper.Factories.CreateToleranceClass.Parametrized(oldTolClass.Id.ToLong(),
                "Neue Klasse Z", false, 5.0, 51.1);

            var tolClassDiff =
                new ToleranceClassDiff(_testSetup.TestUser, new HistoryComment(""), oldTolClass, updatedTolClass);
            dataAccess.SaveToleranceClass(tolClassDiff);

            var classes = dataAccess.LoadToleranceClasses();
            var result = classes.Find(x => x.Id.ToLong() == updatedTolClass.Id.ToLong());

            Assert.IsTrue(updatedTolClass.EqualsByContent(result));
        }

        [TestMethod]
        public void RemoveToleranceClass()
        {
            var dataAccess = new ToleranceClassDataAccess(_testSetup.ClientFactory, null, new TimeDataAccessMock());
            var tolClass = AddNewToleranceClassForTest(dataAccess);

            var tolClasses = dataAccess.LoadToleranceClasses();
            var result = tolClasses.Find(x => x.Id.ToLong() == tolClass.Id.ToLong());
            Assert.IsNotNull(result);

            var tolClassDiff =
               new ToleranceClassDiff(_testSetup.TestUser, new HistoryComment(""), tolClass, tolClass);

            dataAccess.RemoveToleranceClass(tolClassDiff);

            tolClasses = dataAccess.LoadToleranceClasses();
            result = tolClasses.Find(x => x.Id.ToLong() == tolClass.Id.ToLong());
            Assert.IsNull(result);
        }

        [TestMethod]
        public void LoadToleranceClasses()
        {
            var dataAccess = new ToleranceClassDataAccess(_testSetup.ClientFactory, null, new TimeDataAccessMock());
            var tolClass = AddNewToleranceClassForTest(dataAccess);

            var classes = dataAccess.LoadToleranceClasses();
            var result = classes.Find(x => x.Id.ToLong() == tolClass.Id.ToLong());

            Assert.IsTrue(tolClass.EqualsByContent(result));
        }

        [TestMethod]
        public void GetToleranceClassFromHistoryForIds()
        {
            var dataAccess = new ToleranceClassDataAccess(_testSetup.ClientFactory, null, new TimeDataAccessMock());
            var newToleranceClass = AddNewToleranceClassForTest(dataAccess);
            Thread.Sleep(4000);

            var newLocationToolAssignment = TestDataCreator.CreateLocationToolAssignment(_testSetup, true, null, null, newToleranceClass, newToleranceClass);
            var tests = TestDataCreator.CreateTestEquipmentChkTests(_testSetup, newLocationToolAssignment);

            var classicTestDataAccess = new ClassicTestDataAccess(_testSetup.ClientFactory, new TimeDataAccessMock());
            var chkTestsFromTool = classicTestDataAccess.GetClassicChkHeaderFromTool(tests.Item3.AssignedTool.Id, tests.Item3.AssignedLocation.Id);

            var toleranceClassHistoryData = dataAccess.GetToleranceClassFromHistoryForIds(
                new List<Tuple<long, long, DateTime>>()
                {
                    new Tuple<long, long, DateTime>(chkTestsFromTool.First().Id.ToLong(), newToleranceClass.Id.ToLong(),
                        chkTestsFromTool.First().Timestamp)
                });

            var toleranceClassHistory = toleranceClassHistoryData[chkTestsFromTool.First().Id.ToLong()];
            Assert.IsTrue(newToleranceClass.EqualsByContent(toleranceClassHistory));

            var updatedTolClass = TestHelper.Factories.CreateToleranceClass.Parametrized(newToleranceClass.Id.ToLong(),
                newToleranceClass.Name + "X", !newToleranceClass.Relative, newToleranceClass.LowerLimit + 1, newToleranceClass.UpperLimit + 1);

            var tolClassDiff =
                new ToleranceClassDiff(_testSetup.TestUser, new HistoryComment(""), newToleranceClass, updatedTolClass);
            dataAccess.SaveToleranceClass(tolClassDiff);

            var toleranceClassHistoryAfterChangeOriginal = toleranceClassHistoryData[chkTestsFromTool.First().Id.ToLong()];
            Assert.IsTrue(toleranceClassHistoryAfterChangeOriginal.EqualsByContent(toleranceClassHistory));
        }

        [TestMethod]
        public void LoadReferencedLocationToolAssignments()
        {
            var dataAccess = new ToleranceClassDataAccess(_testSetup.ClientFactory, null, new TimeDataAccessMock());
            var toleranceClass = AddNewToleranceClassForTest(dataAccess);

            var locationToolAssignment = TestDataCreator.CreateLocationToolAssignment(_testSetup, true, null, null, toleranceClass, toleranceClass);
            var ids = dataAccess.LoadReferencedLocationToolAssignments(toleranceClass.Id);

            Assert.AreEqual(1, ids.Count);
            Assert.AreEqual(locationToolAssignment.Id.ToLong(), ids.First().ToLong());
        }

        [TestMethod]
        public void LoadReferencedLocations()
        {
            var dataAccess = new ToleranceClassDataAccess(_testSetup.ClientFactory, null, new TimeDataAccessMock());
            var toleranceClass = AddNewToleranceClassForTest(dataAccess);

            var location = TestDataCreator.CreateLocation(_testSetup, "l_" + DateTime.Now.Ticks, toleranceClass, toleranceClass);
            var locationReferences = dataAccess.LoadReferencedLocations(toleranceClass.Id);

            LocationTests.CheckLocationReferenceLink(locationReferences, location);
        }

        private ToleranceClass AddNewToleranceClassForTest(ToleranceClassDataAccess dataAccess)
        {
            var name = "tol_" + System.DateTime.Now.Ticks;

            var classes = dataAccess.LoadToleranceClasses();
            var result = classes.Find(x => x.Name == name);
            Assert.IsNull(result);
           
            return TestDataCreator.CreateToleranceClass(_testSetup, name);
        }
    }
}
