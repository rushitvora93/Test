using Core;
using Core.Entities;
using Core.Entities.ReferenceLink;
using Core.UseCases;
using FrameworksAndDrivers.RemoteData.GRPC.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerIntegrationTests
{
    [TestClass]
    public class ToolModelTest
    {
        public ToolModelTest()
        {
            _testSetup = new TestSetup();
        }

        [TestMethod]
        public void AddToolModel()
        {
            var displayFormatter = new TestFormatter();
            var dataAccess = new ToolModelDataAccess(_testSetup.ClientFactory, displayFormatter);
            var toolModel = AddNewToolModelForTest(dataAccess);

            var toolModels = dataAccess.LoadToolModels();
            var result = toolModels.Find(resultToolModel => resultToolModel.Id.ToLong() == toolModel.Id.ToLong());

            Assert.IsTrue(toolModel.Id.ToLong() != 0);
            Assert.IsTrue(toolModel.EqualsByContent(result));
        }

        [TestMethod]
        public void SaveToolModel()
        {
            var displayFormatter = new TestFormatter();
            var dataAccess = new ToolModelDataAccess(_testSetup.ClientFactory, displayFormatter);
            var oldToolModel = AddNewToolModelForTest(dataAccess);

            var newToolModel = oldToolModel.CopyDeep();
            newToolModel.Description = new ToolModelDescription("changed");
            newToolModel.Weight = 2.6;

            var toolModelDiff = new ToolModelDiff(_testSetup.TestUser, new HistoryComment(""), oldToolModel, newToolModel);
            dataAccess.UpdateToolModel(toolModelDiff);

            var toolModels = dataAccess.LoadToolModels();
            var result = toolModels.Find(resultToolModel => resultToolModel.Id.ToLong() == newToolModel.Id.ToLong());

            Assert.IsTrue(newToolModel.EqualsByContent(result));
        }

        [TestMethod]
        public void RemoveToolModel()
        {
            var displayFormatter = new TestFormatter();
            var dataAccess = new ToolModelDataAccess(_testSetup.ClientFactory, displayFormatter);
            var toolModel = AddNewToolModelForTest(dataAccess);

            var toolModels = dataAccess.LoadToolModels();
            var result = toolModels.Find(x => x.Id.ToLong() == toolModel.Id.ToLong());
            Assert.IsNotNull(result);

            dataAccess.RemoveToolModels(new List<ToolModel> { toolModel }, _testSetup.TestUser);
            toolModels = dataAccess.LoadToolModels();
            result = toolModels.Find(x => x.Id.ToLong() == toolModel.Id.ToLong());
            Assert.IsNull(result);
        }

        [TestMethod]
        public void LoadToolModels()
        {
            var displayFormatter = new TestFormatter();
            var dataAccess = new ToolModelDataAccess(_testSetup.ClientFactory, displayFormatter);
            var toolModel = AddNewToolModelForTest(dataAccess);

            var toolModels = dataAccess.LoadToolModels();
            var result = toolModels.Find(x => x.Id.ToLong() == toolModel.Id.ToLong());

            Assert.IsTrue(toolModel.EqualsByContent(result));
        }

        [TestMethod]
        public void LoadReferencedToolsForToolModel()
        {
            var displayFormatter = new TestFormatter();
            var dataAccess = new ToolModelDataAccess(_testSetup.ClientFactory, displayFormatter);
            var toolModel = AddNewToolModelForTest(dataAccess);
            var tool = TestDataCreator.CreateTool(_testSetup, "test", null, null, null, toolModel);

            var results = dataAccess.LoadReferencedTools(toolModel.Id.ToLong());
            ToolTest.CheckToolReferenceLink(results, tool);
        }

        public class TestFormatter : IToolDisplayFormatter
        {
            public string Format(ToolReferenceLink link)
            {
                throw new NotImplementedException();
            }

            public string Format(Tool tool)
            {
                throw new NotImplementedException();
            }
        }

        private ToolModel AddNewToolModelForTest(ToolModelDataAccess dataAccess)
        {
            var description = "m_" + DateTime.Now.Ticks;
            var toolModels = dataAccess.LoadToolModels();
            var result = toolModels.Find(toolModel => toolModel.Description.ToDefaultString() == description);
            Assert.IsNull(result);
            return TestDataCreator.CreateToolModel(_testSetup, description);
        }

        private readonly TestSetup _testSetup;
    }
}
