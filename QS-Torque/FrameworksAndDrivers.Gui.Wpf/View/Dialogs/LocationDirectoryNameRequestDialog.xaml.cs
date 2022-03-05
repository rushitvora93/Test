using System;
using System.Windows;

namespace FrameworksAndDrivers.Gui.Wpf.View.Dialogs
{
    /// <summary>
    /// Interaction logic for LocationDirectoryNameRequestDialog.xaml
    /// </summary>
    public partial class LocationDirectoryNameRequestDialog : Window
    {

        private Action<MessageBoxResult, string> _resultAction;
        private Predicate<string> _isDirectoryNameUnique;
        private string _messageBoxMessage;
        private string _messageBoxTitle;

        public LocationDirectoryNameRequestDialog()
        {
            InitializeComponent();
            DirectoryName.Focus();
        }

        public LocationDirectoryNameRequestDialog(Action<MessageBoxResult, string> result,  Predicate<string> isDirectoryNameUnique, string messageBoxMessage, string messageBoxTitle) : this()
        {
            _resultAction = result;
            _isDirectoryNameUnique = isDirectoryNameUnique;
            _messageBoxMessage = messageBoxMessage;
            _messageBoxTitle = messageBoxTitle;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if (_isDirectoryNameUnique.Invoke(DirectoryName.Text))
            {
                _resultAction?.Invoke(MessageBoxResult.OK, DirectoryName.Text);
                DialogResult = true;
            }
            else
            {
                MessageBox.Show(this,
                    _messageBoxMessage,
                    _messageBoxTitle,
                    MessageBoxButton.OK, MessageBoxImage.Error);
                DirectoryName.Focus();
            }
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            _resultAction?.Invoke(MessageBoxResult.Cancel, String.Empty);
            DialogResult = false;
        }
    }
}
