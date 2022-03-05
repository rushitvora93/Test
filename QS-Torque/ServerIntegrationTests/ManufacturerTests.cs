using Core.Entities;
using Core.UseCases;
using FrameworksAndDrivers.RemoteData.GRPC.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ServerIntegrationTests
{
    [TestClass]
    public class ManufacturerTests
    {
        
        private readonly TestSetup _testSetup;

        public ManufacturerTests()
        {
            _testSetup = new TestSetup();
        }

        [TestMethod]
        public void AddManufacturer()
        {
            var dataAccess = new ManufacturerDataAccess(_testSetup.ClientFactory);
            var manufacturer = AddNewManufacturerForTest(dataAccess);

            var manufacturers = dataAccess.LoadManufacturer();
            var result = manufacturers.Find(x => x.Id.ToLong() == manufacturer.Id.ToLong());

            Assert.IsTrue(manufacturer.Id.ToLong() != 0);
            Assert.IsTrue(manufacturer.EqualsByContent(result));
        }

        [TestMethod]
        public void SaveManufacturer()
        {
            var dataAccess = new ManufacturerDataAccess(_testSetup.ClientFactory);
            var oldManufacturer = AddNewManufacturerForTest(dataAccess);

            var updatedManufacturer = global::TestHelper.Factories.CreateManufacturer.Parametrized(oldManufacturer.Id.ToLong(), "Neuer Name", "Plattling",
                "Deutschland", "75665", "11", "Müller", "2344", "94447",
                "Straße 13", null);

            var manufacturerDiff =
                new ManufacturerDiff(_testSetup.TestUser, new HistoryComment(""), oldManufacturer, updatedManufacturer);
            dataAccess.SaveManufacturer(manufacturerDiff);

            var manufacturers = dataAccess.LoadManufacturer();
            var result = manufacturers.Find(x => x.Id.ToLong() == updatedManufacturer.Id.ToLong());

            Assert.IsTrue(updatedManufacturer.EqualsByContent(result));
        }

        [TestMethod]
        public void RemoveManufacturer()
        {
            var dataAccess = new ManufacturerDataAccess(_testSetup.ClientFactory);
            var manufacturer = AddNewManufacturerForTest(dataAccess);

            var manufacturers = dataAccess.LoadManufacturer();
            var result = manufacturers.Find(x => x.Id.ToLong() == manufacturer.Id.ToLong());
            Assert.IsNotNull(result);

            dataAccess.RemoveManufacturer(manufacturer, _testSetup.TestUser);
            manufacturers = dataAccess.LoadManufacturer();
            result = manufacturers.Find(x => x.Id.ToLong() == manufacturer.Id.ToLong());
            Assert.IsNull(result);
        }

        [TestMethod]
        public void LoadManufacturer()
        {
            var dataAccess = new ManufacturerDataAccess(_testSetup.ClientFactory);
            var manufacturer = AddNewManufacturerForTest(dataAccess);

            var manufacturers = dataAccess.LoadManufacturer();
            var result = manufacturers.Find(x => x.Id.ToLong() == manufacturer.Id.ToLong());

            Assert.IsTrue(manufacturer.EqualsByContent(result));
        }

        [TestMethod]
        public void LoadManufacturerForComment()
        {
            var dataAccess = new ManufacturerDataAccess(_testSetup.ClientFactory);
            var oldManufacturer = AddNewManufacturerForTest(dataAccess);

            var updatedManufacturer = oldManufacturer.CopyDeep();
            updatedManufacturer.Comment = "Kommentar XYZ";

            var manufacturerDiff =
                new ManufacturerDiff(_testSetup.TestUser, new HistoryComment(""), oldManufacturer, updatedManufacturer);
            dataAccess.SaveManufacturer(manufacturerDiff);

            var result = dataAccess.LoadManufacturerForComment(updatedManufacturer);
            Assert.AreEqual(updatedManufacturer.Comment, result);
        }

        [TestMethod]
        public void LoadToolModelReferenceLinksForManufacturerId()
        {
            //TODO: Complete the test, as soon AddToolModel is implemented with grpc
            var dataAccess = new ManufacturerDataAccess(_testSetup.ClientFactory);
            dataAccess.LoadToolModelReferenceLinksForManufacturerId(1);
            Assert.IsTrue(true);
        }

        private Manufacturer AddNewManufacturerForTest(ManufacturerDataAccess dataAccess)
        {
            var name = "manu_" + System.DateTime.Now.Ticks;

            var manufacturers = dataAccess.LoadManufacturer();
            var result = manufacturers.Find(x => x.Name.ToDefaultString() == name);
            Assert.IsNull(result);

            return TestDataCreator.CreateManufacturer(_testSetup, name);
        }
    }
}
