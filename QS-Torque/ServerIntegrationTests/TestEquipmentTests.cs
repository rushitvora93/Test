using System.Collections.Generic;
using System.Linq;
using Core.Diffs;
using Core.Entities;
using Core.PhysicalValueTypes;
using FrameworksAndDrivers.RemoteData.GRPC.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using CollectionAssert = Microsoft.VisualStudio.TestTools.UnitTesting.CollectionAssert;

namespace ServerIntegrationTests
{
    [TestClass]
    public class TestEquipmentTests
    {
        private readonly TestSetup _testSetup;

        public TestEquipmentTests()
        {
            _testSetup = new TestSetup();
        }

        [TestMethod]
        public void AddTestEquipment()
        {
            var dataAccess = new TestEquipmentDataAccess(_testSetup.ClientFactory);
            var testEquipment = AddNewTestEquipmentForTest(dataAccess);

            var result = GetTestEquipmentFromModels(dataAccess, testEquipment.Id.ToLong());

            Assert.IsTrue(testEquipment.Id.ToLong() != 0);
            Assert.IsTrue(testEquipment.EqualsByContent(result));
        }

        [TestMethod]
        public void SaveTestEquipment()
        {
            var dataAccess = new TestEquipmentDataAccess(_testSetup.ClientFactory);
            var testEquipment = AddNewTestEquipmentForTest(dataAccess);

            var diff = new TestEquipmentDiff(testEquipment.CopyDeep(), testEquipment.CopyDeep(), _testSetup.TestUser);
            diff.NewTestEquipment.CanDeleteMeasurements = !testEquipment.CanDeleteMeasurements;
            diff.NewTestEquipment.CapacityMax = Torque.FromNm(testEquipment.CapacityMax.Nm + 1.0);
            diff.NewTestEquipment.SerialNumber = new TestEquipmentSerialNumber("NEW SerialNumber");
            diff.NewTestEquipment.InventoryNumber = new TestEquipmentInventoryNumber("NEW InventoryNumber");
            dataAccess.SaveTestEquipment(diff);

            var result = GetTestEquipmentFromModels(dataAccess, testEquipment.Id.ToLong());
  
            Assert.IsTrue(testEquipment.Id.ToLong() != 0);
            Assert.IsTrue(diff.NewTestEquipment.EqualsByContent(result));
        }

        [TestMethod]
        public void RemoveTestEquipment()
        {
            var dataAccess = new TestEquipmentDataAccess(_testSetup.ClientFactory);
            var newTestEquipment = AddNewTestEquipmentForTest(dataAccess);

            var testEquipment = GetTestEquipmentFromModels(dataAccess, newTestEquipment.Id.ToLong());
            Assert.IsNotNull(testEquipment);

            dataAccess.RemoveTestEquipment(newTestEquipment, _testSetup.TestUser);
            testEquipment = GetTestEquipmentFromModels(dataAccess, newTestEquipment.Id.ToLong());
            Assert.IsNull(testEquipment);
        }

        [TestMethod]
        public void IsSerialNumberUnique()
        {
            var dataAccess = new TestEquipmentDataAccess(_testSetup.ClientFactory);
            var newTestEquipment = AddNewTestEquipmentForTest(dataAccess);

            Assert.IsFalse(dataAccess.IsSerialNumberUnique(newTestEquipment.SerialNumber));
            Assert.IsTrue(dataAccess.IsSerialNumberUnique(new TestEquipmentSerialNumber(newTestEquipment.SerialNumber.ToDefaultString() + "X")));
        }

        [TestMethod]
        public void IsInventoryNumberUnique()
        {
            var dataAccess = new TestEquipmentDataAccess(_testSetup.ClientFactory);
            var newTestEquipment = AddNewTestEquipmentForTest(dataAccess);

            Assert.IsFalse(dataAccess.IsInventoryNumberUnique(newTestEquipment.InventoryNumber));
            Assert.IsTrue(dataAccess.IsInventoryNumberUnique(new TestEquipmentInventoryNumber(newTestEquipment.InventoryNumber.ToDefaultString() + "X")));
        }

        [TestMethod]
        public void IsTestEquipmentModelNameUnique()
        {
            //TODO implement when InsertTestEquipmentModel is implemented
            Assert.IsTrue(true);
        }

        private TestEquipment AddNewTestEquipmentForTest(TestEquipmentDataAccess dataAccess)
        {
            var name = "t" + System.DateTime.Now.Ticks;
            var result = GetTestEquipmentFromModels(dataAccess, name);
            Assert.IsNull(result);

            return TestDataCreator.CreateTestEquipment(_testSetup, name);
        }

        [TestMethod]
        public void LoadAvailableTestEquipmentTypes()
        {
            var dataAccess = new TestEquipmentDataAccess(_testSetup.ClientFactory);
            var availAbleTypes = dataAccess.LoadAvailableTestEquipmentTypes().OrderBy(x => x).ToList();
            var testEquipmentModels = dataAccess.LoadTestEquipmentModels().GroupBy(x => x.Type).Select(x => x.Key).OrderBy(x => x).ToList();

            CollectionAssert.AreEqual(testEquipmentModels, availAbleTypes);
        }

        private TestEquipment GetTestEquipmentFromModels(TestEquipmentDataAccess dataAccess, string name)
        {
            var testEquipmentModels = dataAccess.LoadTestEquipmentModels().ToList();
            var testEquipments = new List<TestEquipment>();
            testEquipmentModels.ForEach(x => testEquipments.AddRange(x.TestEquipments));
            var result = testEquipments.Find(x => x.SerialNumber.ToDefaultString() == name);
            return result;
        }

        private TestEquipment GetTestEquipmentFromModels(TestEquipmentDataAccess dataAccess, long id)
        {
            var testEquipmentModels = dataAccess.LoadTestEquipmentModels().ToList();
            var testEquipments = new List<TestEquipment>();
            testEquipmentModels.ForEach(x => testEquipments.AddRange(x.TestEquipments));
            var result = testEquipments.Find(x => x.Id.ToLong() == id);
            return result;
        }

    }
}
