namespace ServerConnections.UseCases
{
    public class ServerConnectionUseCaseFactory
    {
        private readonly IServerConnectionStorage _localServerConnectionStorage;
        private readonly IServerConnectionChecker _serverConnectionChecker;

        public ServerConnectionUseCase GetUseCase(IServerConnectionGui serverConnectionGui)
        {
            return new ServerConnectionUseCase(_localServerConnectionStorage, _serverConnectionChecker, serverConnectionGui);
        }
        
        public ServerConnectionUseCaseFactory(IServerConnectionStorage serverConnectionStorage, IServerConnectionChecker serverConnectionChecker)
        {
            _localServerConnectionStorage = serverConnectionStorage;
            _serverConnectionChecker = serverConnectionChecker;
        }
    }
}
