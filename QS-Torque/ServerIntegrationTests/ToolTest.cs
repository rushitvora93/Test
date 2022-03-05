using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Entities.ReferenceLink;
using Core.UseCases;
using FrameworksAndDrivers.Gui.Wpf.Formatter;
using FrameworksAndDrivers.RemoteData.GRPC.DataAccess;
using FrameworksAndDrivers.RemoteData.GRPC.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestHelper.Mock;

namespace ServerIntegrationTests
{
    [TestClass]
    public class ToolTest
    {
        private readonly TestSetup _testSetup;

        public ToolTest()
        {
            _testSetup = new TestSetup();
        }

        [TestMethod]
        public void AddTool()
        {
            var dataAccess = new ToolDataAccess(_testSetup.ClientFactory, new DefaultFormatter(), new PictureFromZipLoader());
            var newTool = AddNewToolForTest(dataAccess);

            var allTools = GetAllTools(dataAccess);

            var result = allTools.Find(x => x.Id.ToLong() == newTool.Id.ToLong());

            Assert.IsTrue(newTool.Id.ToLong() != 0);
            Assert.IsNotNull(result);

            Assert.IsTrue(newTool.EqualsByContent(result));
        }

        [TestMethod]
        public void UpdateTool()
        {
            var dataAccess = new ToolDataAccess(_testSetup.ClientFactory, new DefaultFormatter(), new PictureFromZipLoader());
            var oldTool = AddNewToolForTest(dataAccess);

            var updatedTool = oldTool.CopyDeep();
            updatedTool.InventoryNumber = new ToolInventoryNumber("UpdatedInventoryNumber");
            updatedTool.SerialNumber = new ToolSerialNumber("UpdatedSerialNumber");

            var toolDiff =
                new ToolDiff(_testSetup.TestUser, new HistoryComment(""), oldTool, updatedTool);
            dataAccess.UpdateTool(toolDiff);

            var allTools = GetAllTools(dataAccess);
            var result = allTools.Find(x => x.Id.ToLong() == updatedTool.Id.ToLong());

            Assert.IsTrue(updatedTool.EqualsByContent(result));
        }

        [TestMethod]
        public void RemoveTool()
        {
            var dataAccess = new ToolDataAccess(_testSetup.ClientFactory, new DefaultFormatter(), new PictureFromZipLoader());
            var tool = AddNewToolForTest(dataAccess);

            var allTools = GetAllTools(dataAccess);
            var result = allTools.Find(x => x.Id.ToLong() == tool.Id.ToLong());
            Assert.IsNotNull(result);

            dataAccess.RemoveTool(tool, _testSetup.TestUser);
            allTools = GetAllTools(dataAccess);
            result = allTools.Find(x => x.Id.ToLong() == tool.Id.ToLong());
            Assert.IsNull(result);
        }

        [TestMethod]
        public void LoadModelsWithAtLeasOneTool()
        {
            var dataAccess = new ToolDataAccess(_testSetup.ClientFactory, new DefaultFormatter(), new PictureFromZipLoader());
            var toolModel = TestDataCreator.CreateToolModel(_testSetup, "t_" + DateTime.Now.Ticks);
            var models = dataAccess.LoadModelsWithAtLeasOneTool();
            var result = models.SingleOrDefault(x => x.Id.ToLong() == toolModel.Id.ToLong());
            Assert.IsNull(result);
           
            var tool = AddNewToolForTest(dataAccess, toolModel);

            models = dataAccess.LoadModelsWithAtLeasOneTool();
            result = models.Find(x => x.Id.ToLong() == tool.ToolModel.Id.ToLong());

            Assert.IsNotNull(result);
            Assert.IsTrue(toolModel.EqualsByContent(result));
        }

        [TestMethod]
        public void LoadToolsForModel()
        {
            var dataAccess = new ToolDataAccess(_testSetup.ClientFactory, new DefaultFormatter(), new PictureFromZipLoader());
            var tool = AddNewToolForTest(dataAccess);

            var tools = dataAccess.LoadToolsForModel(tool.ToolModel);
            var result = tools.Find(x => x.Id.ToLong() == tool.Id.ToLong());

            Assert.IsNotNull(result);
            Assert.IsTrue(tool.EqualsByContent(result));
        }

        [TestMethod]
        public void LoadComment()
        {
            var dataAccess = new ToolDataAccess(_testSetup.ClientFactory, new DefaultFormatter(), new PictureFromZipLoader());
            var oldtool = AddNewToolForTest(dataAccess);

            var updatedTool = oldtool.CopyDeep();
            updatedTool.Comment = "Kommentar XYZ";

            var toolDiff =
                new ToolDiff(_testSetup.TestUser, new HistoryComment(""), oldtool, updatedTool);
            dataAccess.UpdateTool(toolDiff);

            var result = dataAccess.LoadComment(updatedTool);
            Assert.AreEqual(updatedTool.Comment, result);
        }

        [TestMethod]
        public void IsSerialNumberUnique()
        {
            var dataAccess = new ToolDataAccess(_testSetup.ClientFactory, new DefaultFormatter(), new PictureFromZipLoader());
            var newTool = AddNewToolForTest(dataAccess);

            Assert.IsFalse(dataAccess.IsSerialNumberUnique(newTool.SerialNumber.ToDefaultString()));
            Assert.IsTrue(dataAccess.IsSerialNumberUnique(newTool.SerialNumber + "X"));
        }

        [TestMethod]
        public void IsInventoryNumberUnique()
        {
            var dataAccess = new ToolDataAccess(_testSetup.ClientFactory, new DefaultFormatter(), new PictureFromZipLoader());
            var newTool = AddNewToolForTest(dataAccess);

            Assert.IsFalse(dataAccess.IsInventoryNumberUnique(newTool.InventoryNumber.ToDefaultString()));
            Assert.IsTrue(dataAccess.IsInventoryNumberUnique(newTool.InventoryNumber + "X"));
        }


        [TestMethod]
        public void LoadLocationToolAssignmentLinksForToolId()
        {
            var dataAccess = new ToolDataAccess(_testSetup.ClientFactory, new DefaultFormatter(), new PictureFromZipLoader());
            var tool = AddNewToolForTest(dataAccess);

            var locationToolAssignment = TestDataCreator.CreateLocationToolAssignment(_testSetup, true, null, tool);
            var locationReferences = dataAccess.LoadLocationToolAssignmentLinksForToolId(tool.Id);
            Assert.AreEqual(1, locationReferences.Count);
            LocationToolAssignmentTests.CheckLocationReferenceLink(locationReferences.First(), tool, locationToolAssignment.AssignedLocation);
        }

        [TestMethod]
        public void LoadPictureForLocation()
        {
            //TODO: Complete the test, as soon AddPicture is implemented with grpc
            Assert.IsTrue(true);
        }

        private Tool AddNewToolForTest(ToolDataAccess dataAccess, ToolModel toolModel = null)
        {
            var serialNumber = "t_" + System.DateTime.Now.Ticks;

            var tools = GetAllTools(dataAccess);
            var result = tools.Find(x => x.SerialNumber.ToDefaultString() == serialNumber);
            Assert.IsNull(result);
            
            return TestDataCreator.CreateTool(_testSetup, serialNumber,null,null,null, toolModel);
        }

        private List<Tool> GetAllTools(ToolDataAccess dataAccess)
        {
            var tools = new List<Tool>();
            var toolModels = dataAccess.LoadModelsWithAtLeasOneTool();
            foreach (var model in toolModels)
            {
                tools.AddRange(dataAccess.LoadToolsForModel(model));
            }

            return tools;
        }

        public static void CheckToolReferenceLink(List<ToolReferenceLink> toolReferences, Tool tool)
        {
            Assert.AreEqual(1, toolReferences.Count);
            var toolReference = toolReferences.Single(x => x.Id.ToLong() == tool.Id.ToLong());
            Assert.IsNotNull(toolReference);
            Assert.AreEqual(tool.SerialNumber.ToDefaultString(), toolReference.SerialNumber);
            Assert.AreEqual(tool.InventoryNumber.ToDefaultString(), toolReference.InventoryNumber);
        }
    }
}
