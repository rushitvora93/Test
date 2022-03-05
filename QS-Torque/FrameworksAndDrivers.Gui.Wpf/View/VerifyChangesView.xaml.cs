using FrameworksAndDrivers.Gui.Wpf.Model;
using FrameworksAndDrivers.Gui.Wpf.ViewModel;
using InterfaceAdapters.Models;
using System.Collections.Generic;
using System.Windows;

namespace FrameworksAndDrivers.Gui.Wpf.View
{
    /// <summary>
    /// Interaction logic for VerifyChangesView.xaml
    /// </summary>
    public partial class VerifyChangesView : Window
    {
        public MessageBoxResult Result { get; private set; }

        public string Comment
        {
            get => (this.DataContext as VerifyChangesViewModel).Comment;
        }


        #region Event-Handler
        private void ButtonApply_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.Yes;
            this.DialogResult = true;
        }

        private void ButtonReset_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.No;
            this.DialogResult = true;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.Cancel;
            this.DialogResult = false;
        }
        #endregion

        
        public VerifyChangesView(IEnumerable<SingleValueChangeModel> changes)
        {
            InitializeComponent();
            this.DataContext = new VerifyChangesViewModel(changes);
        }

        private void VerifyChangesView_OnClosed(object sender, System.EventArgs e)
        {
            Result = Result == MessageBoxResult.None ? MessageBoxResult.Cancel : Result;
        }
    }
}
