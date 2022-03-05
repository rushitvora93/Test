using System;
using Core.Entities;
using FrameworksAndDrivers.RemoteData.GRPC.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ServerIntegrationTests
{
    [TestClass]
    public class StatusTest
    {
        private readonly TestSetup _testSetup;

        public StatusTest()
        {
            _testSetup = new TestSetup();
        }

        [TestMethod]
        public void AddItem()
        {
            var dataAccess = new StatusDataAccess(_testSetup.ClientFactory, null);
            var status = AddNewStatusToDataAccess(dataAccess);

            var statusList = dataAccess.LoadItems();
            var result = statusList.Find(x => x.ListId.ToLong() == status.ListId.ToLong());

            Assert.IsTrue(status.ListId.ToLong() != 0);
            Assert.IsTrue(status.EqualsByContent(result));
        }

        [TestMethod]
        public void SaveItem()
        {
            var dataAccess = new StatusDataAccess(_testSetup.ClientFactory, null);
            var oldStatus = AddNewStatusToDataAccess(dataAccess);

            var newStatus = new Status()
            {
                ListId = oldStatus.ListId,
                Value = new StatusDescription("Neuer Status Y")
            };

            dataAccess.SaveItem(oldStatus, newStatus, _testSetup.TestUser);

            var statusList = dataAccess.LoadItems();
            var result = statusList.Find(x => x.ListId.ToLong() == newStatus.ListId.ToLong());

            Assert.IsTrue(newStatus.EqualsByContent(result));
        }

        [TestMethod]
        public void RemoveItem()
        {
            var dataAccess = new StatusDataAccess(_testSetup.ClientFactory, null);
            var newStatus = AddNewStatusToDataAccess(dataAccess);

            var statusList = dataAccess.LoadItems();
            var result = statusList.Find(x => x.ListId.ToLong() == newStatus.ListId.ToLong());
            Assert.IsNotNull(result);

            dataAccess.RemoveItem(newStatus, _testSetup.TestUser);
            statusList = dataAccess.LoadItems();
            result = statusList.Find(x => x.ListId.ToLong() == newStatus.ListId.ToLong());
            Assert.IsNull(result);
        }

        [TestMethod]
        public void LoadItems()
        {
            var dataAccess = new StatusDataAccess(_testSetup.ClientFactory, null);
            var status = AddNewStatusToDataAccess(dataAccess);

            var statusList = dataAccess.LoadItems();
            var result = statusList.Find(x => x.ListId.ToLong() == status.ListId.ToLong());

            Assert.IsTrue(status.EqualsByContent(result));
        }

        [TestMethod]
        public void LoadToolReferenceLinks()
        {
            var dataAccess = new StatusDataAccess(_testSetup.ClientFactory, null);
            var status = AddNewStatusToDataAccess(dataAccess);

            var tool = TestDataCreator.CreateTool(_testSetup, "t_" + DateTime.Now.Ticks, null, null, status);
            var toolReferences = dataAccess.LoadToolReferenceLinks(status.ListId);

            ToolTest.CheckToolReferenceLink(toolReferences, tool);
        }

        private Status AddNewStatusToDataAccess(StatusDataAccess dataAccess)
        {
            var name = "status_" + System.DateTime.Now.Ticks;

            var statusItems = dataAccess.LoadItems();
            var result = statusItems.Find(x => x.Value.ToDefaultString() == name);
            Assert.IsNull(result);

            return TestDataCreator.CreateStatus(_testSetup, name);
        }
    }
}
