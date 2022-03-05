using System.Collections.Generic;
using Core.UseCases;
using NUnit.Framework;
using ServerConnections.UseCases;
using IServerConnectionStorage = ServerConnections.UseCases.IServerConnectionStorage;

namespace ServerConnections.Test.UseCaseTests
{
    class ServerConnectionStorageMock : IServerConnectionStorage
    {
        public List<ServerConnection> list = new List<ServerConnection>();
        public List<ServerConnection> newServerConnections;

        public List<ServerConnection> GetServerConnections()
        {
            return list;
        }

        public void SaveServerConnections(List<ServerConnection> connections)
        {
            newServerConnections = connections;
        }
    }

    class ServerConnectionGuiMock : IServerConnectionGui
    {
        public void ShowServerConnections(List<ServerConnection> connections)
        {
            serverConnections = connections;
        }

        public List<ServerConnection> GetUpdatedServerConnections()
        {
            return newServerConnections;
        }

        public void ShowServiceConnectionReachableResult(bool serverConnectionReachable)
        {
            isServerConnectionReachable = serverConnectionReachable;
        }

        public List<ServerConnection> serverConnections;
        public bool isServerConnectionReachable;
        public List<ServerConnection> newServerConnections;
    }

    class ServerConnectionCheckerMock : IServerConnectionChecker
    {
        public bool ServerConnectionReachable(Core.UseCases.ServerConnection connection)
        {
            return true;
        }
    }

    class ServerConnectionsUseCaseTest
    {
        private ServerConnectionStorageMock _localServerConnectionStorage;
        private ServerConnectionCheckerMock _connectionChecker;
        private ServerConnectionGuiMock _guiInterface;
        private ServerConnectionUseCase _useCase;

        [SetUp]
        public void Setup()
        {
            _localServerConnectionStorage = new ServerConnectionStorageMock();
            _guiInterface = new ServerConnectionGuiMock();
            _connectionChecker = new ServerConnectionCheckerMock();
            _useCase = new ServerConnectionUseCase(_localServerConnectionStorage, _connectionChecker, _guiInterface);
        }

        [TestCase("TestServer")]
        [TestCase("NotTestServer")]
        public void GetConnectionListReturnSingleObject(string testServerName)
        {
            _localServerConnectionStorage.list.Add(new Core.UseCases.ServerConnection { ServerName = testServerName });

            _useCase.GetConnectionList();

            Assert.AreEqual(_guiInterface.serverConnections[0], new Core.UseCases.ServerConnection { ServerName = testServerName });
        }

        [Test]
        public void SaveServerConnections()
        {
            _guiInterface.newServerConnections = new List<ServerConnection> { new Core.UseCases.ServerConnection { ServerName = "TestServer" } };

            _useCase.SaveServerConnections();

            Assert.AreEqual(_localServerConnectionStorage.newServerConnections[0], new Core.UseCases.ServerConnection { ServerName = "TestServer" });
        }

        [Test]
        public void TestServerConnectionReachable()
        {
            _useCase.IsServiceConnectionReachable(new Core.UseCases.ServerConnection()
            {
                ServerName = "TestServer"
            });
            Assert.IsTrue(_guiInterface.isServerConnectionReachable);
        }
    }
}
