using FrameworksAndDrivers.Gui.Wpf.ViewModel;
using System.Windows;

namespace FrameworksAndDrivers.Gui.Wpf.View
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : Window
    {
        public SettingsView()
        {
            InitializeComponent();
            this.DataContext = new SettingsViewModel();

            SettingsViewModel.IsWindowOpened = true;
            (this.DataContext as SettingsViewModel).CloseRequest += (s, e) => this.Close();

            this.Closing += (s, e) => SettingsViewModel.IsWindowOpened = false;
        }
    }
}
