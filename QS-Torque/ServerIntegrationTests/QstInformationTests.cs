using FrameworksAndDrivers.RemoteData.GRPC.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ServerIntegrationTests
{
    [TestClass]
    public class QstInformationTests
    {
        private readonly TestSetup _testSetup;

        public QstInformationTests()
        {
            _testSetup = new TestSetup();
        }

        [TestMethod]
        public void LoadServerVersion()
        {
            var dataAccess = new QstInformationDataAccess(_testSetup.ClientFactory);
            Assert.IsTrue(dataAccess.LoadServerVersion().Length > 0);
        }
    }
}
