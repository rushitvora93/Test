using FrameworksAndDrivers.Localization;
using ServerConnections.UseCases;
using System.Windows;

namespace ServerConnections.Gui
{
    /// <summary>
    /// Interaction logic for ServerConnectionView.xaml
    /// </summary>
    public partial class ServerConnectionView : Window
    {
        #region Event-Handler
        private void ServerConnectionView_MessageBoxRequest(object sender, FrameworksAndDrivers.Gui.Wpf.EventArgs.MessageBoxEventArgs e)
        {
            e.Show(this);
        }
        #endregion


        public ServerConnectionView(ServerConnectionUseCaseFactory useCaseFactory, LocalizationWrapper localizer)
        {
            InitializeComponent();
            this.DataContext = new ServerConnectionViewModel(useCaseFactory, localizer);

            // Add Event-Handler
            (this.DataContext as ServerConnectionViewModel).MessageBoxRequest += ServerConnectionView_MessageBoxRequest;
        }
    }
}
