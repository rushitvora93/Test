using Client.UseCases.UseCases;
using Core.UseCases;
using FrameworksAndDrivers.Gui.Wpf.ViewModel;
using InterfaceAdapters;
using NUnit.Framework;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security;
using TestHelper.Factories;

namespace FrameworksAndDrivers.Gui.Wpf.Test.ViewModels
{
    public class LoginUseCaseMock : LoginUseCase
    {
        public bool SetupServerConnectionCalled;
        public bool LoadServerConnectionsCalled;
        public bool LoadSuggestionUserNamesCalled;
        public LoginUseCaseMock(IServerConnectionStorage xmlDataInterface, ILoginData dataInterface, ILoginGui guiInterface) : base(xmlDataInterface, dataInterface, guiInterface)
        {
        }

        public override void SetupServerConnection()
        {
            SetupServerConnectionCalled = true;
        }

        public override void LoadServerConnections()
        {
            LoadServerConnectionsCalled = true;
        }

        public override void LoadSuggestionUserNames()
        {
            LoadSuggestionUserNamesCalled = true;
        }
    }

    public class LanguageUseCaseMock : ILanguageUseCase
    {
        public string Language { get; set; }
       
        public ILanguageErrorHandler GetLastLanguageErrorHandler { get; set; }
        public ILanguageErrorHandler SetDefaultLanguageErrorHandler { get; set; }
        public void GetLastLanguage(ILanguageErrorHandler errorHandler)
        {
            Language = "en";
            GetLastLanguageErrorHandler = errorHandler;
        }

        public void SetDefaultLanguage(string language, ILanguageErrorHandler errorHandler)
        {
            Language = language;
            SetDefaultLanguageErrorHandler = errorHandler;
        }
    }

    public class LanguageInterfaceMock : ILanguageInterface
    {
        private string _language;
        public string Language {
            get => _language;
            set
            {
                _language = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Language)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }


    public class LoginViewModelTest
    {
        private LoginViewModel _loginViewModel;
        private LoginUseCaseMock _useCaseMock;


        [SetUp]
        public void Setup()
        {
            _useCaseMock = new LoginUseCaseMock(null, null, null);
            _loginViewModel = new LoginViewModel(_useCaseMock, new NullLocalizationWrapper(), new LanguageUseCaseMock(), new LanguageInterfaceMock());
        }

        [TestCase("Group1", 1)]
        [TestCase("Group2", 2)]
        public void GetGroupTest(string groupName, int groupId)
        {
			var group = CreateGroup.Parametrized(groupId, groupName);
            _loginViewModel.SelectedGroup = group;
            var selectedGroup = _loginViewModel.GetGroup();
            Assert.AreSame(group, selectedGroup);
        }

        [TestCase("Benutzer")]
        [TestCase("Benutzer2")]
        public void GetUserNameTest(string testUserName)
        {
            var userName = testUserName;
            _loginViewModel.EnteredUserName = userName;
            var getUserName = _loginViewModel.GetUserName();
            Assert.AreSame(userName, getUserName);
        }

        [TestCase("Password1")]
        [TestCase("Password2")]
        public void GetPasswordTest(string password)
        {
            var secureString = new SecureString();
            foreach (var singleChar in password)
            {
                secureString.AppendChar(singleChar);
            }

            var getEnderEnteredPassword = _loginViewModel.EnteredPassword = secureString;
            Assert.AreSame(secureString, getEnderEnteredPassword);
        }

        [TestCase("Test",(ushort)11111,"Test","Test")]
        [TestCase("Test2",(ushort)22222,"Test","Test")]
        public void GetServerConnectionTest(string hostName, ushort port, string principalName, string serverName)
        {
            var serverConnection = new ServerConnection
                {HostName = hostName, Port = port, PrincipalName = principalName, ServerName = serverName};
            var selectedServerConnection = _loginViewModel.SelectedServerConnection = serverConnection;
            Assert.AreSame(serverConnection, selectedServerConnection);
        }

        [Test]
        public void SetupServerConnectionTest()
        {
            _loginViewModel.SetupServerConnection();
            Assert.IsTrue(_useCaseMock.SetupServerConnectionCalled);
        }

        [Test]
        public void SetupViewModelTest()
        {
            _loginViewModel.SetupViewModel();
            Assert.IsTrue(_useCaseMock.LoadServerConnectionsCalled);
            Assert.IsTrue(_useCaseMock.LoadSuggestionUserNamesCalled);
        }

        [Test]
        public void EnableLoginTrueTest()
        {
            _loginViewModel.EnableLogin(true);
            Assert.IsTrue(_loginViewModel.ControlsEnabled);
        }

        [Test]
        public void EnableLoginFalseTest()
        {
            _loginViewModel.EnableLogin(false);
            Assert.IsFalse(_loginViewModel.ControlsEnabled);
        }

        [Test]
        public void EnableServerConnectionSelectionTrueTest()
        {
            _loginViewModel.EnableServerConnectionSelection(true);
            Assert.IsTrue(_loginViewModel.ServerConnectionEnabled);
        }

        [Test]
        public void EnableServerConnectionSelectionFalseTest()
        {
            _loginViewModel.EnableServerConnectionSelection(false);
            Assert.IsFalse(_loginViewModel.ServerConnectionEnabled);
        }

        [Test]
        public void CloseLoginTest()
        {
            bool closeRequestCalled = false;
            _loginViewModel.CloseRequest += (sender, args) => closeRequestCalled = true;
            _loginViewModel.CloseLogin();
            Assert.IsTrue(closeRequestCalled);
        }

        [Test]
        public void ShowServerConnectionsTest()
        {
            var serverConnectionList = new List<ServerConnection>
            {
                new ServerConnection {HostName = "Test", ServerName = "Test", PrincipalName = "Test", Port = 11111},
                new ServerConnection {HostName = "Test2", ServerName = "Test2", PrincipalName = "Test2", Port = 22222}
            };
            _loginViewModel.ShowServerConnections(serverConnectionList, "test");
            foreach (var serverConnection in _loginViewModel.ServerNameCollectionView.SourceCollection as IList<ServerConnection>)
            {
                var singleOrDefault = serverConnectionList.SingleOrDefault(x => x.Equals(serverConnection));
                if (singleOrDefault == null)
                {
                    Assert.Fail("Lists are not equal");
                }
            }
            Assert.Pass();
        }

        [Test]
        public void ShowTimeoutMessageTest()
        {
            _loginViewModel.MessageBoxRequest += (sender, args) => { Assert.Pass(); };
            _loginViewModel.ShowTimeoutMessage();
            Assert.Fail();
        }

        [Test]
        public void ShowGenericErrorMessageTest()
        {
            _loginViewModel.MessageBoxRequest += (sender, args) => { Assert.Pass(); };
            _loginViewModel.ShowGenericErrorMessage();
            Assert.Fail();
        }

        [Test]
        public void ShowTimeoutMessage()
        {
            _loginViewModel.MessageBoxRequest += (sender, args) => { Assert.Pass(); };
            _loginViewModel.ShowTimeoutMessage();
            Assert.Fail();
        }

        [Test]
        public void LoadedCommandCallsLanguageUseCase()
        {
            var env = CreateUseCaseEnviroment();
            env.viewModel.LoadedCommand.Invoke(null);           
            Assert.AreEqual(env.useCase.Language,"en");
            Assert.AreSame(env.viewModel, env.useCase.GetLastLanguageErrorHandler);
        }

        [Test]
        public void UseCaseLanguageGetsSetThroughViewModel()
        {
            var env = CreateUseCaseEnviroment();
            var languageString = "en";
            env.viewModel.Language = languageString;
            Assert.AreEqual(env.useCase.Language, languageString);
            Assert.AreSame(env.viewModel, env.useCase.SetDefaultLanguageErrorHandler);
        }

        [Test]
        public void LanguageErrorInvokesMessageRequest()
        {
            var env = CreateUseCaseEnviroment();
            bool requestInvoked = false;
            env.viewModel.MessageBoxRequest += (s, e) => requestInvoked = true;
            env.viewModel.ShowLanguageError();
            Assert.IsTrue(requestInvoked);
        }


        private static Enviroment CreateUseCaseEnviroment()
        {
            var env = new Enviroment();
            env.useCase = new LanguageUseCaseMock();
            env.languageInterface = new LanguageInterfaceMock();
            env.viewModel = new LoginViewModel(null,new NullLocalizationWrapper(), env.useCase, env.languageInterface);
            return env;
        }

        class Enviroment
        {
            public LoginViewModel viewModel;
            public LanguageUseCaseMock useCase;
            public LanguageInterfaceMock languageInterface;
           
        }
    }
}