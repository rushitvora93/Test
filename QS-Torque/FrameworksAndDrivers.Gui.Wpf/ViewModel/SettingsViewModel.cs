using System;

namespace FrameworksAndDrivers.Gui.Wpf.ViewModel
{
    class SettingsViewModel
    {
        public static bool IsWindowOpened = false;

        public event EventHandler CloseRequest;


        #region Commands
        public RelayCommand CloseCommand { get; private set; }

        private bool CloseCanExecute(object arg) { return true; }

        private void CloseExecute(object obj)
        {
            CloseRequest?.Invoke(this, null);
        }
        #endregion


        public SettingsViewModel()
        {
            // Initialize Commands
            CloseCommand = new RelayCommand(CloseExecute, CloseCanExecute);
        }
    }
}
