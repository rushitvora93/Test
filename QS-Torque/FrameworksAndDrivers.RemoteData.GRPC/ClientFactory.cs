namespace FrameworksAndDrivers.RemoteData.GRPC
{
    public interface IClientFactory
    {
        IChannelWrapper AnonymousChannel { get; set; }
        IChannelWrapper AuthenticationChannel { get; set; }
    }
    public class ClientFactory : IClientFactory
    {
        public IChannelWrapper AnonymousChannel { get; set; }
        public IChannelWrapper AuthenticationChannel { get; set; }
    }
}
