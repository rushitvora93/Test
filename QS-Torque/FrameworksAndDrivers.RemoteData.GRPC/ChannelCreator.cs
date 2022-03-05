using System.Threading.Tasks;
using Core.UseCases;
using Grpc.Core;
using Grpc.Net.Client;

namespace FrameworksAndDrivers.RemoteData.GRPC
{
    public interface IChannelCreator
    {
        IChannelWrapper CreateChannel(ServerConnection serverConnection);
        IChannelWrapper CreateChannel(ServerConnection serverConnection, string base64Token);
    }

    public class ChannelCreator : IChannelCreator
    {
        public IChannelWrapper CreateChannel(ServerConnection serverConnection)
        {
            return new ChannelWrapper()
            {
                GrpcChannel = GrpcChannel.ForAddress(ServerConnectionToUrl(serverConnection),
                    new GrpcChannelOptions {MaxSendMessageSize = null, MaxReceiveMessageSize = null})
            };
        }

        private static string ServerConnectionToUrl(ServerConnection serverConnection)
        {
            return $"https://{serverConnection.HostName}:{serverConnection.Port}";
        }

        public IChannelWrapper CreateChannel(ServerConnection serverConnection, string base64Token)
        {
            var credentials = CallCredentials.FromInterceptor((context, metadata) =>
            {
                if (!string.IsNullOrEmpty(base64Token))
                {
                    metadata.Add("Authorization", $"Bearer {base64Token}");
                }
                return Task.CompletedTask;
            });

            return new ChannelWrapper()
            {
                GrpcChannel = GrpcChannel.ForAddress(ServerConnectionToUrl(serverConnection), new GrpcChannelOptions
                {
                    Credentials = ChannelCredentials.Create(new SslCredentials(), credentials),
                    MaxSendMessageSize = null,
                    MaxReceiveMessageSize = null
                })
            };
        }
    }
}
