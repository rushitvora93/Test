using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Core.Diffs;
using Core.Entities;
using FrameworksAndDrivers.Gui.Wpf.Formatter;
using FrameworksAndDrivers.RemoteData.GRPC.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestHelper.Mock;

namespace ServerIntegrationTests
{
    [TestClass]
    public class ExtensionTests
    {

        private readonly TestSetup _testSetup;

        public ExtensionTests()
        {
            _testSetup = new TestSetup();
        }

        [TestMethod]
        public void AddExtension()
        {
            var dataAccess = new ExtensionDataAccess(_testSetup.ClientFactory,new DefaultFormatter(), new TimeDataAccessMock());
            var extension = AddNewExtensionForTest(dataAccess);

            var extensions = dataAccess.LoadExtensions();
            var result = extensions.Find(x => x.Id.ToLong() == extension.Id.ToLong());
            Assert.IsTrue(extension.EqualsByContent(result));
        }

        [TestMethod]
        public void SaveExtension()
        {
            var dataAccess = new ExtensionDataAccess(_testSetup.ClientFactory, new DefaultFormatter(), new TimeDataAccessMock());
            var oldExtension = AddNewExtensionForTest(dataAccess);

            var updatedExtension = oldExtension.CopyDeep();

            var diff =
                new ExtensionDiff(_testSetup.TestUser, new HistoryComment(""), oldExtension, updatedExtension);
            dataAccess.SaveExtension(diff);

            var extensions = dataAccess.LoadExtensions();
            var result = extensions.Find(x => x.Id.ToLong() == updatedExtension.Id.ToLong());

            Assert.IsTrue(updatedExtension.EqualsByContent(result));
        }

        [TestMethod]
        public void RemoveExtension()
        {
            var dataAccess = new ExtensionDataAccess(_testSetup.ClientFactory, new DefaultFormatter(), new TimeDataAccessMock());
            var newExtension = AddNewExtensionForTest(dataAccess);

            var extensions = dataAccess.LoadExtensions();
            var extension = extensions.Find(x => x.Id.ToLong() == newExtension.Id.ToLong());
            Assert.IsNotNull(extension);

            dataAccess.RemoveExtension(newExtension, _testSetup.TestUser);
            extensions = dataAccess.LoadExtensions();
            extension = extensions.Find(x => x.Id.ToLong() == newExtension.Id.ToLong());
            Assert.IsNull(extension);
        }

        [TestMethod]
        public void IsInventoryNumberUnique()
        {
            var dataAccess = new ExtensionDataAccess(_testSetup.ClientFactory, new DefaultFormatter(), new TimeDataAccessMock());
            var extension = AddNewExtensionForTest(dataAccess);

            Assert.IsFalse(dataAccess.IsInventoryNumberUnique(extension.InventoryNumber));
            Assert.IsTrue(dataAccess.IsInventoryNumberUnique(new ExtensionInventoryNumber(extension.InventoryNumber.ToDefaultString() + "X")));
        }

        private Extension AddNewExtensionForTest(ExtensionDataAccess dataAccess)
        {
            var inv = "ext_" + System.DateTime.Now.Ticks;

            var extensions = dataAccess.LoadExtensions();
            var result = extensions.Find(x => x.InventoryNumber.ToDefaultString() == inv);
            Assert.IsNull(result);

            return TestDataCreator.CreateExtension(_testSetup, inv);
        }
    }
}
