using FrameworksAndDrivers.Gui.Wpf.ViewModel;
using System.Windows;

namespace FrameworksAndDrivers.Gui.Wpf.View
{
    /// <summary>
    /// Interaction logic for UserSettingsView.xaml
    /// </summary>
    public partial class UserSettingsView : Window
    {
        public UserSettingsView()
        {
            InitializeComponent();
            this.DataContext = new UserSettingsViewModel();

            UserSettingsViewModel.IsWindowOpened = true;
            (this.DataContext as UserSettingsViewModel).CloseRequest += (s, e) => this.Close();

            this.Closing += (s, e) => UserSettingsViewModel.IsWindowOpened = false;
        }
    }
}
