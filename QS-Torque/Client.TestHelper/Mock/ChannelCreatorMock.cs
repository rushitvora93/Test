using Core.UseCases;
using FrameworksAndDrivers.RemoteData.GRPC;

namespace TestHelper.Mock
{
    public class ChannelCreatorMock : IChannelCreator
    {
        public IChannelWrapper CreateChannel(ServerConnection serverConnection)
        {
            CreateChannelServerConnectionParameter = serverConnection;
            return CreateChannelReturnValue;
        }

        public IChannelWrapper CreateChannel(ServerConnection serverConnection, string base64Token)
        {
            CreateChannelServerConnectionParameter = serverConnection;
            CreateChannelTokenParameter = base64Token;
            return CreateChannelReturnValue;
        }

        public string CreateChannelTokenParameter { get; set; }

        public ServerConnection CreateChannelServerConnectionParameter { get; set; }

        public IChannelWrapper CreateChannelReturnValue { get; set; }
    }
}
