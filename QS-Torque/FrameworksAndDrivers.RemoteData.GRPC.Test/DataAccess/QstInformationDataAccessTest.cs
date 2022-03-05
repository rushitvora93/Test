using Client.TestHelper.Mock;
using FrameworksAndDrivers.RemoteData.GRPC.DataAccess;
using NUnit.Framework;
using TestHelper.Mock;
using String = BasicTypes.String;

namespace FrameworksAndDrivers.RemoteData.GRPC.Test.DataAccess
{
    public class QstInformationClientMock : IQstInformationClient
    {
        public String LoadServerVersionReturnValue { get; set; } = new String();
        public bool LoadServerVersionCalled { get; set; }

        public String LoadServerVersion()
        {
            LoadServerVersionCalled = true;
            return LoadServerVersionReturnValue;
        }
    }

    class QstInformationDataAccessTest
    {
        [Test]
        public void LoadServerVersionCallsClient()
        {
            var environment = new Environment();
            environment.dataAccess.LoadServerVersion();
            Assert.IsTrue(environment.mocks.qstInformationClient.LoadServerVersionCalled);
        }

        [TestCase("1.0.0.1")]
        [TestCase("1.0.1.5")]
        public void LoadServerVersionReturnsCorrectValue(string val)
        {
            var environment = new Environment();
            environment.mocks.qstInformationClient.LoadServerVersionReturnValue = new String() {Value = val};
            var result = environment.dataAccess.LoadServerVersion();
            Assert.AreEqual(val, result);
        }

        private class Environment
        {
            public class Mocks
            {
                public Mocks()
                {
                    clientFactory = new ClientFactoryMock();
                    channelWrapper = new ChannelWrapperMock();
                    qstInformationClient = new QstInformationClientMock();
                    channelWrapper.GetQstInformationClientReturnValue = qstInformationClient;
                    clientFactory.AuthenticationChannel = channelWrapper;
                }
                public ClientFactoryMock clientFactory;
                public ChannelWrapperMock channelWrapper;
                public QstInformationClientMock qstInformationClient;
            }

            public Environment()
            {
                mocks = new Mocks();
                dataAccess = new QstInformationDataAccess(mocks.clientFactory);
            }

            public readonly Mocks mocks;
            public readonly QstInformationDataAccess dataAccess;
        }
    }
}
