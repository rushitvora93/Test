using FrameworksAndDrivers.RemoteData.GRPC;

namespace TestHelper.Mock
{
    public class ClientFactoryMock : FrameworksAndDrivers.RemoteData.GRPC.IClientFactory
    {
        public IChannelWrapper AnonymousChannel { get; set; }
        public IChannelWrapper AuthenticationChannel { get; set; }
    }
}
