using Core.Entities;
using FrameworksAndDrivers.RemoteData.GRPC.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ServerIntegrationTests
{
    [TestClass]
    public class ToolUsageTests
    {
        private readonly TestSetup _testSetup;

        public ToolUsageTests()
        {
            _testSetup = new TestSetup();
        }

        [TestMethod]
        public void AddItem()
        {
            var dataAccess = new ToolUsageDataAccess(_testSetup.ClientFactory);
            var toolUsage = AddNewToolUsageForTest(dataAccess);

            var toolUsageList = dataAccess.LoadItems();
            var result = toolUsageList.Find(x => x.ListId.ToLong() == toolUsage.ListId.ToLong());

            Assert.IsTrue(toolUsage.ListId.ToLong() != 0);
            Assert.IsTrue(toolUsage.EqualsByContent(result));
        }

        [TestMethod]
        public void SaveItem()
        {
            var dataAccess = new ToolUsageDataAccess(_testSetup.ClientFactory);
            var oldToolUsage = AddNewToolUsageForTest(dataAccess);

            var newToolUsage = new ToolUsage()
            {
                ListId = oldToolUsage.ListId,
                Value = new ToolUsageDescription("Wkz Pos 1")
            };

            dataAccess.SaveItem(oldToolUsage, newToolUsage, _testSetup.TestUser);

            var toolUsageList = dataAccess.LoadItems();
            var result = toolUsageList.Find(x => x.ListId.ToLong() == newToolUsage.ListId.ToLong());

            Assert.IsTrue(newToolUsage.EqualsByContent(result));
        }

        [TestMethod]
        public void RemoveItem()
        {
            var dataAccess = new ToolUsageDataAccess(_testSetup.ClientFactory);
            var newToolUsage = AddNewToolUsageForTest(dataAccess);

            var toolUsageList = dataAccess.LoadItems();
            var result = toolUsageList.Find(x => x.ListId.ToLong() == newToolUsage.ListId.ToLong());
            Assert.IsNotNull(result);

            dataAccess.RemoveItem(newToolUsage, _testSetup.TestUser);
            toolUsageList = dataAccess.LoadItems();
            result = toolUsageList.Find(x => x.ListId.ToLong() == newToolUsage.ListId.ToLong());
            Assert.IsNull(result);
        }

        [TestMethod]
        public void LoadItems()
        {
            var dataAccess = new ToolUsageDataAccess(_testSetup.ClientFactory);
            var toolUsage = AddNewToolUsageForTest(dataAccess);

            var toolUsageList = dataAccess.LoadItems();
            var result = toolUsageList.Find(x => x.ListId.ToLong() == toolUsage.ListId.ToLong());

            Assert.IsTrue(toolUsage.EqualsByContent(result));
        }

        [TestMethod]
        public void LoadReferencedLocationToolAssignmentIds()
        {
            //TODO: Complete the test, as soon AddLocationToolAssignment is implemented with grpc
            var dataAccess = new ToolUsageDataAccess(_testSetup.ClientFactory);
            dataAccess.LoadReferencedLocationToolAssignmentIds(new HelperTableEntityId(1));
            Assert.IsTrue(true);
        }

        private ToolUsage AddNewToolUsageForTest(ToolUsageDataAccess dataAccess)
        {
            var name = "tu_" + System.DateTime.Now.Ticks;

            var toolUsageItems = dataAccess.LoadItems();
            var result = toolUsageItems.Find(x => x.Value.ToDefaultString() == name);
            Assert.IsNull(result);
            
            return TestDataCreator.CreateToolUsage(_testSetup, name);
        }
    }
}
