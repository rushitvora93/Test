using FrameworksAndDrivers.Gui.Wpf.ViewModel;
using FrameworksAndDrivers.Localization;
using Microsoft.Win32;
using System.Windows;

namespace FrameworksAndDrivers.Gui.Wpf.View
{
    /// <summary>
    /// Interaction logic for QstInformationView.xaml
    /// </summary>
    public partial class QstInformationView : Window
    {
		private LocalizationWrapper _localization;

        #region Event-Handler
        private void ButtonCreteLogPackage_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog();
            dialog.Filter = string.Format("{0}|*.zip", _localization.Strings.GetParticularString("File Selection Dialog - File Type", "ZIP Archives"));
            dialog.DefaultExt = ".zip";
            dialog.AddExtension = true;

            if (dialog.ShowDialog() == true)
            {
                (this.DataContext as QstInformationViewModel).CreateLogPackage(dialog.FileName);
            }
        }

        private void ViewModel_MessageBoxRequest(object sender, EventArgs.MessageBoxEventArgs e)
        {
            e.Show(this);
        }
        #endregion

		public QstInformationView(QstInformationViewModel viewModel, LocalizationWrapper localization)
		{
			InitializeComponent();
			_localization = localization;
			DataContext = viewModel;
			viewModel.MessageBoxRequest += ViewModel_MessageBoxRequest;
		}
    }
}
