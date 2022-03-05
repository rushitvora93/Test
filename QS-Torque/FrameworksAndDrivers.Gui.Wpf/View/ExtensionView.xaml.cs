using FrameworksAndDrivers.Gui.Wpf.EventArgs;
using FrameworksAndDrivers.Gui.Wpf.Interfaces;
using FrameworksAndDrivers.Gui.Wpf.View.Dialogs;
using FrameworksAndDrivers.Gui.Wpf.ViewModel;
using FrameworksAndDrivers.Localization;
using InterfaceAdapters.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FrameworksAndDrivers.Gui.Wpf.View
{
    /// <summary>
    /// Interaktionslogik für ExtensionView.xaml
    /// </summary>
    public partial class ExtensionView : UserControl, ICanClose, IDisposable
    {
        private readonly ExtensionViewModel _viewModel;
        private readonly LocalizationWrapper _localization;

        public ExtensionView(ExtensionViewModel viewModel, LocalizationWrapper localization)
        {
            InitializeComponent();
            _viewModel = viewModel;
            viewModel.SetDispatcher(Dispatcher);
            _localization = localization;
            this.DataContext = viewModel;
            _viewModel.MessageBoxRequest += _viewModel_MessageBoxRequest;
            _viewModel.ShowDialogRequest += _viewModel_ShowDialogRequest;
            _viewModel.RequestVerifyChangesView += _viewModel_RequestVerifyChangesView;
            _viewModel.ReferencesDialogRequest += _viewModel_ReferencesDialogRequest;
        }

        private void _viewModel_ReferencesDialogRequest(object sender, ReferenceList e)
        {
            var dialog = new ViewReferencesDialog();
            dialog.ShowDialog(Window.GetWindow(this), new List<ReferenceList> { e });
        }

        private void _viewModel_RequestVerifyChangesView(object sender, VerifyChangesEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                var view = new VerifyChangesView(e.ChangedValues);
                view.ShowDialog();

                // Set Comment
                e.Comment = (view.DataContext as VerifyChangesViewModel).Comment;

                // Set Result
                e.Result = view.Result;
            });
        }

        private void _viewModel_ShowDialogRequest(object sender, ICanShowDialog e)
        {
            e.ShowDialog();
        }

        private void _viewModel_MessageBoxRequest(object sender, EventArgs.MessageBoxEventArgs e)
        {
            e.Show();
        }

        public bool CanClose()
        {
            return _viewModel.CanClose();
        }

        public void Dispose()
        {
            _viewModel.MessageBoxRequest -= _viewModel_MessageBoxRequest;
            _viewModel.ShowDialogRequest -= _viewModel_ShowDialogRequest;
            _viewModel.RequestVerifyChangesView -= _viewModel_RequestVerifyChangesView;
            _viewModel.ReferencesDialogRequest -= _viewModel_ReferencesDialogRequest;
            _viewModel.Dispose();
            DataContext = null;
        }
    }
}
