using Core.Entities;
using Core.UseCases;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Security;
using System.Threading.Tasks;
using TestHelper.Factories;

namespace Core.Test.UseCases
{
    class LoginUseCaseTest
    {
        #region Interface implementions

        private class ServerConnectionStorage : IServerConnectionStorage
        {
            public List<ServerConnection> ServerConnections { get; set; } 
            public List<string> SuggestedUserNames { get; set; } = new List<string>();
            public bool ServerConnectionShowError { get; set; }

            public bool LastServerConnectionCalled = false;
            public bool AddUserNameToSuggestionsCalled = false;
            public int LoadLastServerConnectionCallCount = 0;

            public List<ServerConnection> LoadServerConnections()
            {
                if (ServerConnectionShowError)
                {
                    throw new Exception();
                }

                return ServerConnections;
            }

            public List<string> LoadSuggestionUserNames()
            {
                return SuggestedUserNames;
            }

            public void AddUserNameToSuggestions(string userName)
            {
                AddUserNameToSuggestionsCalled = true;
            }

            public void AddLastServerConnection(ServerConnection serverConnection)
            {
                LastServerConnectionCalled = true;
            }

            public string LoadLastServerConnectionName()
            {
                LoadLastServerConnectionCallCount++;
                return "test";
            }
        }

        private class LoginData : ILoginData
        {
            public Func<Task<List<Group>>> loadGroupReplacement = null;

            public bool ReturnLoginError { get; set; }

            public Exception ReturnException { get; set; }

            public bool ReturnSetupLoginError { get; set; }
            

            public List<Group> Groups { get; set; }

            public async Task<List<Group>> LoadGroupsForUserNameAsync(string username)
            {
                if (loadGroupReplacement == null)
                {
                    return username == "TestUserName" ? Groups : new List<Group>();
                }
                else
                {
                    return await loadGroupReplacement();
                }
            }

            public Task SetupServerConnectionAsync(ServerConnection serverConnection)
            {
                if (!ReturnSetupLoginError)
                {
                    return Task.CompletedTask;
                }

                throw new Exception("Fehler beim Setup");
            }

            public Task LoginAsync(string userName, SecureString password, Group @group)
            {
                if (!ReturnLoginError)
                {
                    return Task.CompletedTask;
                }

                throw ReturnException;
            }
        }

        private class LoginGui : ILoginGui
        {
            public int closeLoginCounter = 0;

            public ServerConnection ServerConnection { get; set; }
            public string UserName { get; set; }
            public SecureString Password { get; set; }
            public Group Group { get; set; }

            public List<ServerConnection> ServerConnections { get; private set; }
            public List<string> SuggestedUserNames { get; private set; }
            public List<Group> Groups { get; private set; }

            public bool ShowLoginNotSuccessfullMessageCalled = false;
            public bool ShowTimeoutMessageCalled = false;
            public bool ShowGenericErrorMessageCalled = false;
            public bool ShowLicenseErrorMessageCalled = false;

            public bool ShowServerConnectionErrorCalled { get; private set; }

            public bool ShowConnectServerConnectionErrorCalled { get; private set; }

            public bool ControlsEnabled { get; set; } = true;

            public ServerConnection GetServerConnection()
            {
                return ServerConnection;
            }

            public string GetUserName()
            {
                return UserName;
            }

            public SecureString GetPassword()
            {
                return Password;
            }

            public Group GetGroup()
            {
                return Group;
            }

            public void ShowServerConnections(List<ServerConnection> serverConnections, string lastServerConnectionName)
            {
                ServerConnections = serverConnections;
            }

            public void ShowSuggestionUserNames(List<string> suggestionUserNames)
            {
                SuggestedUserNames = suggestionUserNames;
            }

            public void ShowGroups(List<Group> groups)
            {
                Groups = groups;
            }

            public void CloseLogin()
            {
                closeLoginCounter++;
            }

            public void ShowLoginNotSuccessfullMessage()
            {
                ShowLoginNotSuccessfullMessageCalled = true;
            }

            public void ShowTimeoutMessage()
            {
                ShowTimeoutMessageCalled = true;
            }

            public void ShowGenericErrorMessage()
            {
                ShowGenericErrorMessageCalled = true;
            }

            public void ShowReadServerConnectionsError()
            {
                ShowServerConnectionErrorCalled = true;
            }

            public void ShowConnectServerConnectionError()
            {
                ShowConnectServerConnectionErrorCalled = true;
            }

            public readonly List<bool> ControlsEnabledList = new List<bool>();
            public void EnableLogin(bool status)
            {
                ControlsEnabled = status;
                ControlsEnabledList.Add(status);
            }

            public readonly List<bool> ServerConnectionSelectionEnabledList = new List<bool>();
            public void EnableServerConnectionSelection(bool enabled)
            {
                ServerConnectionSelectionEnabledList.Add(enabled);
            }

            public void ShowLicenseErrorMessage(string msg)
            {
                ShowLicenseErrorMessageCalled = true;
            }
        }

        #endregion

        ServerConnectionStorage _xmlData;
        LoginData _data;
        LoginGui _gui;
        LoginUseCase _useCase;

        [SetUp]
        public void LoginSetup()
        {
            _xmlData = new ServerConnectionStorage();
            _gui = new LoginGui();
            _data = new LoginData();

            _useCase = new LoginUseCase(_xmlData, _data, _gui);
        }

        [Test]
        public void LoadServerConnections()
        {
            _xmlData.ServerConnections = new List<ServerConnection>()
            {
                new ServerConnection()
                {
                    HostName = "TestConnection"
                },
                new ServerConnection()
                {
                    HostName = "TestConnection2"
                }
            };

            _useCase.LoadServerConnections();

            Assert.AreEqual(_xmlData.ServerConnections, _gui.ServerConnections);
        }

        [Test]
        public void LoadUserSuggestion()
        {
            _xmlData.SuggestedUserNames = new List<string>() {"User 1", "User 2"};

            _useCase.LoadSuggestionUserNames();

            Assert.AreEqual(_xmlData.SuggestedUserNames, _gui.SuggestedUserNames);
        }

        [Test]
        public void LoadGroupsForUserName()
        {
            _gui.UserName = "TestUserName";
            _data.Groups = new List<Group>()
                {CreateGroup.WithName("Gruppe 1"), CreateGroup.WithName("Gruppe 2")};

            _useCase.LoadGroupsForUserName();

            Assert.AreEqual(_data.Groups, _gui.Groups);
        }

        class VolatileWorkaround
        {
            public string groupName;
            public volatile bool blockLoadGroup = true;
            public volatile bool threadStarted = false;

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
			// Methode ist async damit die signatur mit ILoginData zusammenpasst
			public async Task<List<Group>> BlockingLoadGroup()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
			{
                threadStarted = true;
                while (blockLoadGroup)
                {
                }
                return new List<Group>() { CreateGroup.WithName(groupName) };
            }
        }

        [Test]
        public void LoadGroupsRefiredWithBadTiming()
        {
            var load1 = new VolatileWorkaround() { groupName = "Group1" };
            var load2 = new VolatileWorkaround() { groupName = "Group2" };

            _gui.UserName = "TestUserName";

            var t1 = Task.Run(() =>
            {
                _data.loadGroupReplacement = load1.BlockingLoadGroup;
                _useCase.LoadGroupsForUserName();
            });

            while (!load1.threadStarted)
            {
            }

            var t2 = Task.Run(() =>
            {
                _data.loadGroupReplacement = load2.BlockingLoadGroup;
                _useCase.LoadGroupsForUserName();
            });

            load2.blockLoadGroup = false;
            t2.Wait();

            load1.blockLoadGroup = false;
            t1.Wait();

            Assert.AreEqual("Group2", _gui.Groups[0].GroupName);
        }

        [Test]
        public void LoginNotSuccessfullExceptionTest()
        {
            _data.ReturnLoginError = true;
            _data.ReturnException = new LoginFailedException(null);
            _useCase.Login();
            Assert.IsTrue(_gui.ShowLoginNotSuccessfullMessageCalled);
        }

        [Test]
        public void LoginTimeoutExceptionTest()
        {
            _data.ReturnLoginError = true;
            _data.ReturnException = new TimeoutException();
            _useCase.Login();
            Assert.IsTrue(_gui.ShowTimeoutMessageCalled);
        }

        [Test]
        public void LicenseFailedExceptionTest()
        {
            _data.ReturnLoginError = true;
            _data.ReturnException = new LicenseFailedException(null,"");
            _useCase.Login();
            Assert.IsTrue(_gui.ShowLicenseErrorMessageCalled);
        }

        [Test]
        public void LoginGenericExceptionTest()
        {
            _data.ReturnLoginError = true;
            _data.ReturnException = new Exception();
            _useCase.Login();
            Assert.IsTrue(_gui.ShowGenericErrorMessageCalled);
        }

        [Test]
        public void LoginServerConnectionErrorTest()
        {
            _xmlData.ServerConnectionShowError = true;
            _useCase.LoadServerConnections();
            Assert.IsTrue(_gui.ShowServerConnectionErrorCalled);
        }

        [Test]
        public void SetupServerConnectionErrorTest()
        {
            _data.ReturnSetupLoginError = true;
            _useCase.SetupServerConnection();
            Assert.IsTrue(_gui.ShowConnectServerConnectionErrorCalled);
        }

        [Test]
        public void SetupServerConnectionLicenseErrorTest()
        {
            _data.ReturnException = new LoginFailedException(null);
            _data.ReturnSetupLoginError = true;
            _useCase.SetupServerConnection();
            Assert.IsTrue(_gui.ShowConnectServerConnectionErrorCalled);
        }

        [Test]
        public void DisableControlsTest()
        {
            _data.ReturnSetupLoginError = true;
            _useCase.SetupServerConnection();
            Assert.IsFalse(_gui.ControlsEnabled);
        }

        [Test]
        public void LoginTest()
        {
            _gui.ServerConnection = new ServerConnection()
            {
                HostName = "TestConnection",
                Port = 11111,
                PrincipalName = "Hans",
                ServerName = "testServerConnection"
            };
            _gui.UserName = "TestUserName";
            _gui.Password = new SecureString();
            _gui.Password.AppendChar('1');
            _gui.Password.AppendChar('2');
            _gui.Password.AppendChar('3');
            _gui.Group = CreateGroup.WithName("TestGroup");
            _gui.closeLoginCounter = 0;

            _useCase.Login();
            Assert.IsTrue(_xmlData.LastServerConnectionCalled,"AddLastServerConnection not called");
            Assert.IsTrue(_xmlData.AddUserNameToSuggestionsCalled, "AddUserNameToSuggestions not called");
            Assert.AreEqual(_gui.closeLoginCounter, 1);
        }

        [Test]
        public void LoginControlsAreDisabledWhenLogin()
        {
            var gui = new LoginGui();
            var useCase = new LoginUseCase(new ServerConnectionStorage(), new LoginData(), gui);
            useCase.Login();

            Assert.AreEqual(2, gui.ControlsEnabledList.Count);
            Assert.IsFalse(gui.ControlsEnabledList[0]);

            Assert.AreEqual(2, gui.ServerConnectionSelectionEnabledList.Count);
            Assert.IsFalse(gui.ServerConnectionSelectionEnabledList[0]);
        }

        [Test]
        public void LoginControlsAreEnabledAfterLogin()
        {
            var gui = new LoginGui();
            var useCase = new LoginUseCase(new ServerConnectionStorage(), new LoginData(), gui);
            useCase.Login();

            Assert.AreEqual(2, gui.ControlsEnabledList.Count);
            Assert.IsTrue(gui.ControlsEnabledList[1]);

            Assert.AreEqual(2, gui.ServerConnectionSelectionEnabledList.Count);
            Assert.IsTrue(gui.ServerConnectionSelectionEnabledList[1]);
        }
    }
}
