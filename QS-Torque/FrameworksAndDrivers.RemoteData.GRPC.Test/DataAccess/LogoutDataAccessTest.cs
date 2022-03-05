using Client.TestHelper.Mock;
using FrameworksAndDrivers.RemoteData.GRPC.DataAccess;
using NUnit.Framework;
using State;
using TestHelper.Factories;
using TestHelper.Mock;

namespace FrameworksAndDrivers.RemoteData.GRPC.Test.DataAccess
{
    class LogoutDataAccessTest
    {
        [Test]
        public void LoggingOutDisposesAuthenticatedChannel()
        {
            var environment = new Environment();
            environment.DataAccess.Logout();
            Assert.IsTrue(environment.Mock.AuthenticationChannel.DisposeWasCalled);
            Assert.IsNull(environment.Mock.ClientFactory.AuthenticationChannel);
        }

        [Test]
        public void LoggingOutDisposesAnonymousChannel()
        {
            var environment = new Environment();
            environment.DataAccess.Logout();
            Assert.IsTrue(environment.Mock.AnonymousChannel.DisposeWasCalled);
            Assert.IsNull(environment.Mock.ClientFactory.AnonymousChannel);
        }

        [Test]
        public void LoggingOutClearsSessionInformationUser()
        {
            var environment = new Environment();
            SessionInformation.CurrentUser = CreateUser.Anonymous();
            environment.DataAccess.Logout();
            Assert.IsNull(SessionInformation.CurrentUser); // not able to do concurrency, because it's static! arrrgh! -_-
        }

        private class Environment
        {
            public class Mocks
            {
                public Mocks()
                {
                    ClientFactory = new ClientFactoryMock();
                    AuthenticationChannel = new ChannelWrapperMock();
                    ClientFactory.AuthenticationChannel = AuthenticationChannel;
                    AnonymousChannel = new ChannelWrapperMock();
                    ClientFactory.AnonymousChannel = AnonymousChannel;
                }

                public readonly ClientFactoryMock ClientFactory;
                public readonly ChannelWrapperMock AuthenticationChannel;
                public readonly ChannelWrapperMock AnonymousChannel;
            }

            public Environment()
            {
                Mock = new Mocks();
                DataAccess = new LogoutDataAccess(Mock.ClientFactory);
            }

            public readonly Mocks Mock;
            public readonly LogoutDataAccess DataAccess;
        }
    }
}
