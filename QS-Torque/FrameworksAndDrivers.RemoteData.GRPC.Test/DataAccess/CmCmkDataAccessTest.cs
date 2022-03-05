using System.Collections.Generic;
using Client.TestHelper.Mock;
using DtoTypes;
using FrameworksAndDrivers.RemoteData.GRPC.DataAccess;
using NUnit.Framework;
using SetupService;
using State;
using TestHelper.Factories;
using TestHelper.Mock;

namespace FrameworksAndDrivers.RemoteData.GRPC.Test.DataAccess
{
    public class CmCmkDataAccessTest
    {
        [Test]
        public void LoadCmCmkCallsClient()
        {
            var environment = new Environment();
            SessionInformation.CurrentUser = CreateUser.IdOnly(1);
            environment.mocks.setupClient.GetQstSetupsByUserIdAndNameReturnValues = new List<ListOfQstSetup>()
            {
                new ListOfQstSetup(),
                new ListOfQstSetup()
            };
            environment.dataAccess.LoadCmCmk();

            var cmRequest = new GetQstSetupsByUserIdAndNameRequest()
            {
                UserId = 0,
                Name = "Cm"
            };

            var cmkRequest = new GetQstSetupsByUserIdAndNameRequest()
            {
                UserId = 0,
                Name = "Cmk"
            };

            Assert.IsTrue(environment.mocks.setupClient.GetQstSetupsByUserIdAndNameParameters.Contains(cmRequest));
            Assert.IsTrue(environment.mocks.setupClient.GetQstSetupsByUserIdAndNameParameters.Contains(cmkRequest));
        }

        [TestCase(12.0, 11.1, "12.0", "11.1")]
        [TestCase(1.50, 1.91, "1.50", "1.91")]
        public void LoadCmCmkReturnsCorrectValue(double expectedCmk, double expectedCm, string setupCmk, string setupCm)
        {
            var environment = new Environment();
            SessionInformation.CurrentUser = CreateUser.Anonymous();

            environment.mocks.setupClient.GetQstSetupsByUserIdAndNameReturnValues = new List<ListOfQstSetup>()
            {
                new ListOfQstSetup()
                {
                    SetupList = { new QstSetup(){ Value = setupCm} }
                },
                new ListOfQstSetup()
                {
                    SetupList = { new QstSetup(){ Value = setupCmk } }
                }
            };

            var (cm, cmk) = environment.dataAccess.LoadCmCmk();

            Assert.AreEqual(expectedCmk, cmk);
            Assert.AreEqual(expectedCm, cm);
        }

        [Test]
        public void LoadCmCmkReturnsDefaultValueWithoutSetup()
        {
            const double defaultCm = 1.67;
            const double defaultCmk = 1.33;
            var environment = new Environment();
            SessionInformation.CurrentUser = CreateUser.IdOnly(1);
            environment.mocks.setupClient.GetQstSetupsByUserIdAndNameReturnValues = new List<ListOfQstSetup>()
            {
                new ListOfQstSetup(),
                new ListOfQstSetup()
            };
            var (cm, cmk) = environment.dataAccess.LoadCmCmk();

            Assert.AreEqual(defaultCm, cm);
            Assert.AreEqual(defaultCmk, cmk);
        }

        private class Environment
        {
            public class Mocks
            {
                public Mocks()
                {
                    clientFactory = new ClientFactoryMock();
                    channelWrapper = new ChannelWrapperMock();
                    setupClient = new SetupClientMock();
                    channelWrapper.GetSetupClientReturnValue = setupClient;
                    clientFactory.AuthenticationChannel = channelWrapper;
                }
                public ClientFactoryMock clientFactory;
                public ChannelWrapperMock channelWrapper;
                public SetupClientMock setupClient;
            }

            public Environment()
            {
                mocks = new Mocks();
                dataAccess = new CmCmkDataAccess(mocks.clientFactory);
            }

            public readonly Mocks mocks;
            public readonly CmCmkDataAccess dataAccess;
        }
    }
}
