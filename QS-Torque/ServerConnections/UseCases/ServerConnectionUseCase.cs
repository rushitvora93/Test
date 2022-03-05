using System.Collections.Generic;
using Core.UseCases;

namespace ServerConnections.UseCases
{
    public interface IServerConnectionStorage
    {
        List<ServerConnection> GetServerConnections();
        void SaveServerConnections(List<ServerConnection> connections);
    }

    public interface IServerConnectionGui
    {
        void ShowServerConnections(List<ServerConnection> connections);
        List<ServerConnection> GetUpdatedServerConnections();
        void ShowServiceConnectionReachableResult(bool serverConnectionReachable);
    }

    public interface IServerConnectionChecker
    {
        bool ServerConnectionReachable(Core.UseCases.ServerConnection connection);
    }

    public class ServerConnectionUseCase
    {
        private readonly IServerConnectionStorage _localServerConnectionStorage;
        private readonly IServerConnectionChecker _connectionChecker;
        private readonly IServerConnectionGui _guiInterface;

        public ServerConnectionUseCase(IServerConnectionStorage localServerConnectionData, IServerConnectionChecker connectionChecker, IServerConnectionGui guiInterface)
        {
            this._localServerConnectionStorage = localServerConnectionData;
            this._guiInterface = guiInterface;
            this._connectionChecker = connectionChecker;
        }

        public void GetConnectionList()
        {
             _guiInterface.ShowServerConnections(_localServerConnectionStorage.GetServerConnections());
        }

        public void SaveServerConnections()
        {
            _localServerConnectionStorage.SaveServerConnections(_guiInterface.GetUpdatedServerConnections());
        }

        public void IsServiceConnectionReachable(Core.UseCases.ServerConnection serverConnection)
        {
            _guiInterface.ShowServiceConnectionReachableResult(_connectionChecker.ServerConnectionReachable(serverConnection));
        }
    }
}