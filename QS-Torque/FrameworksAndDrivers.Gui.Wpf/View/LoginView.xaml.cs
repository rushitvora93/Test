using FrameworksAndDrivers.Gui.Wpf.ViewModel;
using FrameworksAndDrivers.Localization;
using System.Windows;
using System.Windows.Controls;

namespace FrameworksAndDrivers.Gui.Wpf.View
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {

        private LocalizationWrapper _localization;
        private LoginViewModel _viewModel;
        #region Event-Handler
        private void LoginView_OnLoaded(object sender, RoutedEventArgs e)
        {
            _viewModel?.SetupViewModel();

        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            _viewModel.EnteredPassword = (sender as PasswordBox).SecurePassword;
        }

        private void ViewModel_MessageBoxRequest(object sender, EventArgs.MessageBoxEventArgs e)
        {
            e.Show(this);
        }

        private void ViewModel_CloseRequest(object sender, System.EventArgs e)
        {
            DialogResult = true;
            this.Close();
        }

        private void SelectedServerConnection_DropDownClosed(object sender, System.EventArgs e)
        {
            _viewModel?.SetupServerConnection();       
        }
        #endregion

        public LoginView(LoginViewModel loginViewModel, LocalizationWrapper localization)
		{
			InitializeComponent();
            _localization = localization;
			DataContext = loginViewModel;
            _viewModel = loginViewModel;
			loginViewModel.MessageBoxRequest += ViewModel_MessageBoxRequest;
			loginViewModel.CloseRequest += ViewModel_CloseRequest;
		}
    }
}
