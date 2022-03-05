using Core.Entities;
using Core.UseCases;
using FrameworksAndDrivers.Gui.Wpf.EventArgs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using InterfaceAdapters;
using InterfaceAdapters.Localization;
using Client.UseCases.UseCases;
using System.ComponentModel;

namespace FrameworksAndDrivers.Gui.Wpf.ViewModel
{
    public class LoginViewModel : BindableBase, ILoginGui, ILanguageErrorHandler
    {
        #region Properties
        private LoginUseCase _useCase;
        private ILanguageUseCase _languageUseCase;
        private ILanguageInterface _languageInterface;
		private ILocalizationWrapper _localization;
        

        //ServerNames
        private ObservableCollection<ServerConnection> _serverNames;
        public ListCollectionView ServerNameCollectionView { get; private set; }

        // SuggestedUserNames
        private ObservableCollection<string> _suggestedUserNames;
        public ListCollectionView SuggestedUserNameCollectionView { get; private set; }

        // Groups
        private ObservableCollection<Group> _groups;
        public ListCollectionView GroupCollectionView { get; private set; }


        private ServerConnection _selectedServerConnection;

        public ServerConnection SelectedServerConnection
        {
            get => _selectedServerConnection;
            set
            {
                _selectedServerConnection = value;
                RaisePropertyChanged(nameof(SelectedServerConnection));
            }
        }

        // EnteredUserName
        private string _enteredUserName = "";
        public string EnteredUserName
        {
            get => _enteredUserName;
            set
            {
                _enteredUserName = value;
                if (!string.IsNullOrWhiteSpace(value))
                {
                    RaisePropertyChanged(nameof(EnteredUserName));
                    EnteredUserNameChanged();
                }
            }
        }
        private void EnteredUserNameChanged()
        {
            LoginEnabled = false;
            _useCase.LoadGroupsForUserName();
        }

        // EnteredPassword
        public SecureString EnteredPassword { get; set; }

        // SelectedGroup
        private Group _selectedGroup;
        public Group SelectedGroup
        {
            get => _selectedGroup;
            set
            {
                _selectedGroup = value;
                RaisePropertyChanged(nameof(SelectedGroup));
            }
        }

        // IsCapsLockToggled
        private bool _isCapsLockToggled;
        public bool IsCapsLockToggled
        {
            get => _isCapsLockToggled;
            set
            {
                _isCapsLockToggled = value;
                RaisePropertyChanged(nameof(IsCapsLockToggled));
            }
        }

        private bool _loginEnabled;

        public bool LoginEnabled
        {
            get => _loginEnabled;
            set
            {
                _loginEnabled = value;
                RaisePropertyChanged();
                CommandManager.InvalidateRequerySuggested();
            }
        }

        private bool _controlsEnabled;
        public bool ControlsEnabled
        {
            get => _controlsEnabled;
            set
            {
                _controlsEnabled = value;
                RaisePropertyChanged(nameof(ControlsEnabled));
            }
        }

        //ServerConnectionEnabled
        private bool _serverConnectionEnabled;

        public bool ServerConnectionEnabled
        {
            get => _serverConnectionEnabled;
            set
            {
                _serverConnectionEnabled = value;
                RaisePropertyChanged(nameof(ServerConnectionEnabled));
            }
        }

        public Visibility GroupVisibility
        {
            get => FeatureToggles.FeatureToggles.HideUnusedParts ? Visibility.Collapsed : Visibility.Visible;
        }

        public string Language
        {
            get => _languageInterface.Language;
            set
            {             
                _languageUseCase.SetDefaultLanguage(value,this);
            }
        }
        #endregion


        #region Events
        public event EventHandler<MessageBoxEventArgs> MessageBoxRequest;
        public event EventHandler CloseRequest;
        #endregion


        #region Commands
        public RelayCommand SetSuggestedUserNameCommand { get; private set; }
        public RelayCommand CheckForCapsLockCommand { get; private set; }
        public RelayCommand LoginCommand { get; private set; }
        public RelayCommand LoadedCommand { get; private set; }
        

        private bool SetSuggestedUserNameCanExecute(object arg) { return true; }
        private bool CheckForCapsLockCanExecute(object arg) { return true; }
        private bool LoginCanExecute(object arg) { return LoginEnabled; }
        private bool LoadedCanExecute(object arg) { return true; }


        private void SetSuggestedUserNameExecute(object obj)
        {
            EnteredUserName = (string)obj;
        }

        private void CheckForCapsLockExecute(object obj)
        {
            IsCapsLockToggled = (Keyboard.GetKeyStates(Key.CapsLock) & KeyStates.Toggled) == KeyStates.Toggled;
        }

        private void LoginExecute(object obj)
        {
            _useCase.Login();
        }

        private void LoadedExecute(object obj)
        {
            _languageUseCase.GetLastLanguage(this);
        }
        #endregion


        #region Interface
        public ServerConnection GetServerConnection()
        {
            return SelectedServerConnection;
        }

        public string GetUserName()
        {
            
            return EnteredUserName;
        }

        public SecureString GetPassword()
        {
            return EnteredPassword;
        }

        public Group GetGroup()
        {
            return SelectedGroup;
        }

        public void ShowServerConnections(List<ServerConnection> serverConnections, string lastServerConnectionName)
        {
            _serverNames = new ObservableCollection<ServerConnection>(serverConnections);
            ServerNameCollectionView = new ListCollectionView(_serverNames);
            RaisePropertyChanged(nameof(ServerNameCollectionView));
            var lastServerNames = _serverNames.FirstOrDefault(x => x.ServerName == lastServerConnectionName);
            if (lastServerNames != null)
            {
                SelectedServerConnection = lastServerNames;
            }
            _useCase.SetupServerConnection();
            
        }

        public void ShowSuggestionUserNames(List<string> suggestionUserNames)
        {
            _suggestedUserNames.Clear();
            suggestionUserNames.ForEach(x => _suggestedUserNames.Add(x));
        }

        public void ShowGroups(List<Group> groups)
        {
            _groups.Clear();
            _groups = new ObservableCollection<Group>(groups);
            GroupCollectionView = new ListCollectionView(_groups);
            RaisePropertyChanged(nameof(GroupCollectionView));
            LoginEnabled = true;
        }

        public void CloseLogin()
        {
            // Close LoginView
            CloseRequest?.Invoke(this, null);
        }

        public void ShowLoginNotSuccessfullMessage()
        {
            var args = new MessageBoxEventArgs((r) => { },
				_localization.Strings.GetString("Log in failed!\nPlease check your user name and password."),
				_localization.Strings.GetString("Check login data"),
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
            //Request MessageBox
            MessageBoxRequest?.Invoke(this, args);
        }

        public void ShowTimeoutMessage()
        {
            var args = new MessageBoxEventArgs((r) => { },
				_localization.Strings.GetString("The connection to the server could not be established.\nPlease check the server connection settings"),
				_localization.Strings.GetString("Server Connection lost"),
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            MessageBoxRequest?.Invoke(this, args);
        }

        public void ShowGenericErrorMessage()
        {
            var args = new MessageBoxEventArgs((r) => { },
                caption: _localization.Strings.GetString("Unknown Error!"),
                text: _localization.Strings.GetString("An unknown error occured.\nPlease contact our Support Team"),

				messageBoxButton: MessageBoxButton.OK,
                messageBoxImage: MessageBoxImage.Error);
            MessageBoxRequest?.Invoke(this, args);
        }

        public void ShowReadServerConnectionsError()
        {
            var args = new MessageBoxEventArgs((r) => { },
                caption: _localization.Strings.GetString("Input Error!"),
                text: _localization.Strings.GetString("Error in server configuration.\nPlease check the server configuration."),
				messageBoxButton: MessageBoxButton.OK,
                messageBoxImage: MessageBoxImage.Error);
            MessageBoxRequest?.Invoke(this, args);
        }

        public void ShowConnectServerConnectionError()
        {
            var args = new MessageBoxEventArgs((r) => { },
                caption: _localization.Strings.GetString("Connection Error"),
                text: _localization.Strings.GetString("Connection to the Server could not be established."),
                messageBoxButton:MessageBoxButton.OK,
                messageBoxImage: MessageBoxImage.Error);
            MessageBoxRequest?.Invoke(this, args);
        }

        public void ShowLicenseErrorMessage(string msg)
        {
            var args = new MessageBoxEventArgs((r) => { },
                caption: _localization.Strings.GetString("License Error"),
                text: _localization.Strings.GetString("Could not load a valid QST-License"),
                messageBoxButton: MessageBoxButton.OK,
                messageBoxImage: MessageBoxImage.Error);
            MessageBoxRequest?.Invoke(this, args);
        }

        public void ShowLanguageError()
        {
            var args = new MessageBoxEventArgs(r => { },
                _localization.Strings.GetParticularString("LoginView", "Error occured while loading language"),
                _localization.Strings.GetParticularString("LoginView", "Error"),
                MessageBoxButton.OK, MessageBoxImage.Error);
            MessageBoxRequest?.Invoke(this, args);
        }

        public void EnableLogin(bool status)
        {
            ControlsEnabled = status;
            LoginEnabled = status;
        }

        public void EnableServerConnectionSelection(bool enabled)
        {
            ServerConnectionEnabled = enabled;
        }

        #endregion


        #region Methods

        public void SetupViewModel()
        {
            _useCase.LoadServerConnections();
            _useCase.LoadSuggestionUserNames();
        }

        public void SetupServerConnection()
        {
            _useCase.SetupServerConnection();
        }

        private void WireViewModelToLanguageInterface()
        {
            PropertyChangedEventManager.AddHandler(
              _languageInterface,
              (s, e) => RaisePropertyChanged(nameof(Language)),
              nameof(LanguageInterfaceAdapter.Language));
        }
        #endregion

        public LoginViewModel(LoginUseCase loginUseCase, ILocalizationWrapper localization, ILanguageUseCase languageUseCase, ILanguageInterface languageInterface)
		{
			_useCase = loginUseCase;
            _languageUseCase = languageUseCase;
            _languageInterface = languageInterface;
			_localization = localization;

            WireViewModelToLanguageInterface();

			// Initialize lists
			_serverNames = new ObservableCollection<ServerConnection>();
			ServerNameCollectionView = new ListCollectionView(_serverNames);

			_suggestedUserNames = new ObservableCollection<string>();
			SuggestedUserNameCollectionView = new ListCollectionView(_suggestedUserNames);

			_groups = new ObservableCollection<Group>();
			GroupCollectionView = new ListCollectionView(_groups);

			// Initialize commands
			SetSuggestedUserNameCommand = new RelayCommand(SetSuggestedUserNameExecute, SetSuggestedUserNameCanExecute);
			CheckForCapsLockCommand = new RelayCommand(CheckForCapsLockExecute, CheckForCapsLockCanExecute);
			LoginCommand = new RelayCommand(LoginExecute, LoginCanExecute);
            LoadedCommand = new RelayCommand(LoadedExecute, LoadedCanExecute);
		}
	}
}
