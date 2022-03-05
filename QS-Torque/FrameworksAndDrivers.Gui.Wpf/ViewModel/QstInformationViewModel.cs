using Core.UseCases;
using FrameworksAndDrivers.Gui.Wpf.EventArgs;
using FrameworksAndDrivers.Localization;
using System;
using System.Windows;
using InterfaceAdapters;

namespace FrameworksAndDrivers.Gui.Wpf.ViewModel
{
    public class QstInformationViewModel :  BindableBase, IQstInformationGui
    {
        #region Properties
        private QstInformationUseCase _useCase;
		private LocalizationWrapper _localization;

        private string _qstVersion;
        public string QstVersion
        {
            get
            {
                return _qstVersion;
            }
            set
            {
                _qstVersion = value;
                RaisePropertyChanged(nameof(QstVersion));
            }
        }

        private string _serverVersion;

        public string ServerVersion
        {
            get { return _serverVersion; }
            set
            {
                _serverVersion = value;
                RaisePropertyChanged(nameof(ServerVersion));
            }
        }

        private string _computerName;
        public string ComputerName
        {
            get { return _computerName; }
            set
            {
                _computerName = value;
                RaisePropertyChanged(nameof(ComputerName));
            }
        }
        #endregion


        #region Event
        public event EventHandler<MessageBoxEventArgs> MessageBoxRequest;
        #endregion


        #region Methods
        public void CreateLogPackage(string fileName)
        {
            _useCase.CreateLogPackage(fileName);
        }

        public void Startup()
        {
            _useCase.LoadQstVersion();
            _useCase.LoadServerVersion();
            _useCase.LoadComputerName();
        }
        #endregion


        #region Interface
        public void ShowQstVersion(string qstVersion)
        {
            QstVersion = qstVersion;
        }

        public void ShowServerVersion(string serverVersion)
        {
            ServerVersion = serverVersion;
        }

        public void ShowComputerName(string computerName)
        {
            ComputerName = computerName;
        }

        public void ShowLogPackageSuccessMessage()
        {
			// Define EventArgs
			var args = new MessageBoxEventArgs((r) => { },
												_localization.Strings.GetString("Successfully created Protocol Package!"),
                                               "",
                                               MessageBoxButton.OK,
                                               MessageBoxImage.Information);

            // Request MessageBox
            MessageBoxRequest?.Invoke(this, args);
        }
		#endregion

		public RelayCommand LoadedCommand { get; private set; }

		private bool CanExecuteLoaded(object arg)
		{
			return true;
		}

		private void ExecuteLoaded(object arg)
		{
			Startup();
		}

		public QstInformationViewModel(QstInformationUseCase useCase, LocalizationWrapper localization)
		{
			_useCase = useCase;
			_localization = localization;
			LoadedCommand = new RelayCommand(ExecuteLoaded, CanExecuteLoaded);
		}
    }
}
