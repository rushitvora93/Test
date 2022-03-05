using FrameworksAndDrivers.Gui.Wpf.EventArgs;
using FrameworksAndDrivers.Gui.Wpf.ViewModel;
using ServerConnections.Model;
using ServerConnections.UseCases;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using Core.UseCases;
using FrameworksAndDrivers.Localization;

namespace ServerConnections.Gui
{
	class ServerConnectionViewModel : DependencyObject, IServerConnectionGui
    {
        #region Properties
        private ServerConnectionUseCase _useCase;

        private ObservableCollection<ServerConnectionModel> _connectionList;
        public ListCollectionView ConnectionListView { get; private set; }

        private bool _isSaved = true;
		private LocalizationWrapper _localizer;

        // SelectedConnection
        private static readonly DependencyProperty SelectedConnectionProperty =
            DependencyProperty.Register("SelectedConnection", typeof(ServerConnectionModel), typeof(ServerConnectionViewModel));
        public ServerConnectionModel SelectedConnection
        {
            get { return (ServerConnectionModel)GetValue(SelectedConnectionProperty); }
            set { SetValue(SelectedConnectionProperty, value); }
        }

        public static readonly DependencyProperty IsBusyProperty = DependencyProperty.Register(
            "IsBusy", typeof(bool), typeof(ServerConnectionViewModel), new PropertyMetadata(default(bool)));

        public bool IsBusy
        {
            get { return (bool) GetValue(IsBusyProperty); }
            set { SetValue(IsBusyProperty, value); }
        }

        #endregion


        #region Events
        public event EventHandler<MessageBoxEventArgs> MessageBoxRequest;
        #endregion


        #region Commands
        // Commands
        public RelayCommand AddConnectionCommand      { get; private set; }
        public RelayCommand RemoveConnectionCommand   { get; private set; }
        public RelayCommand TestConnectionCommand     { get; private set; }
        public RelayCommand MoveConnectionUpCommand   { get; private set; }
        public RelayCommand MoveConnectionDownCommand { get; private set; }
        public RelayCommand SaveConnectionsCommand    { get; private set; }
        public RelayCommand AskForSavingCommand       { get; private set; }


        // CanExecutes
        private bool AddConnectionCanExecute(object arg)      { return true; }
        private bool RemoveConnectionCanExecute(object arg)   { return SelectedConnection != null; }
        private bool TestConnectionCanExecute(object arg)     { return SelectedConnection != null; }
        private bool MoveConnectionUpCanExecute(object arg)   { return SelectedConnection != null && ConnectionListView.IndexOf(SelectedConnection) > 0; }
        private bool MoveConnectionDownCanExecute(object arg) { return SelectedConnection != null && ConnectionListView.IndexOf(SelectedConnection) < ConnectionListView.Count - 1; }
        private bool SaveConnectionsCanExecute(object arg)    { return true; }
        private bool AskForSavingCanExecute(object arg)       { return !_isSaved; }


        // Executes
        private void AddConnectionExecute(object obj)
        {
            var con = new ServerConnectionModel();

            // Get unique ServerName
            int counter = 0;
            string name;
            while (true)
            {
                name = $"Server {ConnectionListView.Count + counter + 1}";
                if(_connectionList.FirstOrDefault(c => c.ServerName == name) == null)
                {
                    // Leave loop if ServerName is unique
                    break;
                }
                else
                {
                    counter++;
                }
            }

            con.ServerName = name;
            ConnectionListView.AddNewItem(con);
            SelectedConnection = con;
        }

        private void RemoveConnectionExecute(object obj)
        {
            // Define Action
            Action<MessageBoxResult> resultAction = (r) =>
            {
                if(r == MessageBoxResult.Yes)
                {
                    _connectionList.Remove(SelectedConnection);
                }
            };

			// Define MessageBoxEventArgs
			var args = new MessageBoxEventArgs(resultAction,
											   _localizer.Strings.GetString("Do you really want to remove the server connection?"),
											   _localizer.Strings.GetParticularString("Window Title", "Warning"),
                                               MessageBoxButton.YesNo,
                                               MessageBoxImage.Exclamation);

            // Request MessageBox
            MessageBoxRequest?.Invoke(this, args);
        }

        private async void TestConnectionExecute(object obj)
        {
            IsBusy = true;
            var serverConnectionEntity = SelectedConnection.MapToEntity();
            await  Task.Run(() =>  _useCase.IsServiceConnectionReachable(serverConnectionEntity));
        }

        private void MoveConnectionUpExecute(object obj)
        {
            var index = ConnectionListView.IndexOf(SelectedConnection);
            _connectionList.Move(index, index - 1);
        }

        private void MoveConnectionDownExecute(object obj)
        {
            var index = ConnectionListView.IndexOf(SelectedConnection);
            _connectionList.Move(index, index + 1);
        }

        private void SaveConnectionsExecute(object obj)
        {
            _useCase.SaveServerConnections();
            _isSaved = true;

            // Display MessageBox

            var args = new MessageBoxEventArgs((r) => { },
											   _localizer.Strings.GetString("Entries saved"),
                                               "",
                                               MessageBoxButton.OK,
                                               MessageBoxImage.Information);
            MessageBoxRequest?.Invoke(this, args);
        }

        private void AskForSavingExecute(object obj)
        {
            // Define Action
            Action<MessageBoxResult> resultAction = (r) =>
            {
                if (r == MessageBoxResult.Yes)
                {
                    // Save ServerConnections
                    _useCase.SaveServerConnections();
                }
                else if (r == MessageBoxResult.Cancel)
                {
                    // Do not close window
                    ((CancelEventArgs)obj).Cancel = true;
                }
            };

            // Define MessageBoxEventArgs
            var args = new MessageBoxEventArgs(resultAction,
											   _localizer.Strings.GetString("Do you want to save your changes?"),
											   _localizer.Strings.GetParticularString("Window Title", "Warning"),
                                               MessageBoxButton.YesNoCancel,
                                               MessageBoxImage.Question);

            // Request MessageBox
            MessageBoxRequest?.Invoke(this, args);
        }
        #endregion


        #region Interface
        public List<ServerConnection> GetUpdatedServerConnections()
        {
            var list = new List<ServerConnection>();

            // Fill ServerConnectionList
            foreach (var model in _connectionList)
            {
                list.Add(model.MapToEntity());
            }
            return list;
        }

        public void ShowServiceConnectionReachableResult(bool serverConnectionReachable)
        {
            MessageBoxEventArgs args;
            
            // Check if Server is reachable
            if (serverConnectionReachable)
            {
                args = new MessageBoxEventArgs((r) => { },
					_localizer.Strings.GetString("The connection could be established"),
                    "",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            else
            {
                args = new MessageBoxEventArgs((r) => { },
					_localizer.Strings.GetString("The connection could not be established"),
                    "",
                    MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);
            }
            Dispatcher?.Invoke(() =>
            {
                // Request MessageBox
                MessageBoxRequest?.Invoke(this, args);
                IsBusy = false;
            });

        }

        public void ShowServerConnections(List<ServerConnection> connections)
        {
            _connectionList.Clear();

            // Fill _connectionList and ConnectionListView
            foreach (var con in connections)
            {
                _connectionList.Add(ServerConnectionModel.FromEntity(con));
            }
        }
        #endregion
        
        

        // Cstor
        public ServerConnectionViewModel(ServerConnectionUseCaseFactory useCaseFactory, LocalizationWrapper localizer)
        {
			_localizer = localizer;
            _connectionList = new ObservableCollection<ServerConnectionModel>();
            ConnectionListView = new ListCollectionView(_connectionList);

            // Save parameter
            _useCase = useCaseFactory.GetUseCase(this);

            // Load ServerConnections
            _useCase.GetConnectionList();

            // Add Event-Handler
            ServerConnectionModel.ServerConnectionChanged += (s, e) =>
            {
                _isSaved = false;
            };
            
            // Initialize Commands
            AddConnectionCommand = new RelayCommand(AddConnectionExecute, AddConnectionCanExecute);
            RemoveConnectionCommand = new RelayCommand(RemoveConnectionExecute, RemoveConnectionCanExecute);
            TestConnectionCommand = new RelayCommand(TestConnectionExecute, TestConnectionCanExecute);
            MoveConnectionUpCommand = new RelayCommand(MoveConnectionUpExecute, MoveConnectionUpCanExecute);
            MoveConnectionDownCommand = new RelayCommand(MoveConnectionDownExecute, MoveConnectionDownCanExecute);
            SaveConnectionsCommand = new RelayCommand(SaveConnectionsExecute, SaveConnectionsCanExecute);
            AskForSavingCommand = new RelayCommand(AskForSavingExecute, AskForSavingCanExecute);
        }
    }
}
