using FrameworksAndDrivers.Gui.Wpf.ViewModel;
using FrameworksAndDrivers.Localization;
using System.Windows;
using System.Windows.Controls;

namespace FrameworksAndDrivers.Gui.Wpf.View.Controls
{
    /// <summary>
    /// Interaction logic for GlobalToolBar.xaml
    /// </summary>
    public partial class GlobalToolBar : UserControl
    {
		private LocalizationWrapper _localizationWrapper;
		private IStartUp _startUp;

        public GlobalToolBarViewModel ViewModel;
        public event RoutedEventHandler LogoutClick;


        #region Methods
        internal void SetLocalizationWrapper(LocalizationWrapper localizationWrapper)
        {
            if (_languageSelector.Visibility == Visibility.Collapsed)
            {
                return;
            }

            _localizationWrapper = localizationWrapper;        
        }

        internal void SetViewModel(GlobalToolBarViewModel viewModel)
        {
            ViewModel = viewModel;
            this.DataContext = viewModel;
        }
        #endregion


        #region Event-Handler
        private void ButtonAboutQst_Click(object sender, RoutedEventArgs e)
        {
            var window = _startUp.OpenQstInformation();
            window.ShowDialog();
        }

		private void ButtonOpenSettings_Click(object sender, RoutedEventArgs e)
        {
			// Open Settings only if Windows is not opened yet
			// Once a UseCase is needed here, please use IStartUp
			if (SettingsViewModel.IsWindowOpened)
                return;

            var window = new SettingsView();
            window.ShowDialog();
        }

        private void ButtonOpenUserSettings_Click(object sender, RoutedEventArgs e)
        {
			// Open UserSettings only if Windows is not opened yet
			// Once a UseCase is needed here, please use IStartUp
			if (UserSettingsViewModel.IsWindowOpened)
                return;

			var window = new UserSettingsView();
            window.ShowDialog();
        }

        private void ButtonLogout_Click(object sender, RoutedEventArgs e)
        {
            LogoutClick?.Invoke(this, e);
        }

		internal void SetStartUp(IStartUp startUp)
		{
			_startUp = startUp;
		}

        
        #endregion


        public GlobalToolBar()
        {
            InitializeComponent();
        }
    }
}
