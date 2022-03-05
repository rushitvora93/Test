using Core.Entities;
using FrameworksAndDrivers.Gui.Wpf.Interfaces;
using FrameworksAndDrivers.Gui.Wpf.ViewModel;
using FrameworksAndDrivers.Localization;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using FrameworksAndDrivers.Gui.Wpf.View.Controls;
using FrameworksAndDrivers.Gui.Wpf.View.Dialogs;
using System;

namespace FrameworksAndDrivers.Gui.Wpf.View
{
    /// <summary>
    /// Interaction logic for HelperTableView.xaml
    /// </summary>
    /// <typeparam name="T">Entity that should be shown with the HelperTable (e. g. ToolType, Status, ...)</typeparam>
    /// <typeparam name="U">Type of the representing Value in the Entity (e. g. Value of ToolType)</typeparam>
    public partial class HelperTableView : UserControl, ICanClose
    {
        private LocalizationWrapper _localization;
        private IHelperTableViewModel _viewModel;

        #region Interface-Implementations
        public bool CanClose()
        {
            if (DataContext is ICanClose canClose)
            {
                return canClose.CanClose();
            }
            else
            {
                return true;
            }
        }
        #endregion


        #region Event-Handler
        private void HelperTableView_MessageBoxRequest(object sender, EventArgs.MessageBoxEventArgs e)
        {
            e.Show();
        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(HelperTableViewModel<ToolType, string>.AreReferencesShown))
            {
                if (_viewModel.AreReferencesShown)
                {
                    if (ReferencesExpander.Content is ExtendedSfDataGrid dataGrid)
                    {
                        dataGrid.LocalizationWrapper = _localization;
                        _localization.Subscribe(dataGrid);
                    }
                }
            }
        }
        #endregion


        // Cstor
        public HelperTableView(IHelperTableViewModel viewModel, LocalizationWrapper localization)
        {
            InitializeComponent();
			DataContext = viewModel;
            _viewModel = viewModel;
            _localization = localization;
            viewModel.PropertyChanged += ViewModel_PropertyChanged;
            viewModel.RegisterSelectAndFocusInputField(HelperTableView_SelectAndFocusInputField);
            viewModel.RegisterMessageBoxEventHandler(HelperTableView_MessageBoxRequest);
            viewModel.RegisterReferencesDialogRequest(HelperTableView_RegisterReferences);
            viewModel.SetGuiDispatcher(Dispatcher);
        }

        private void HelperTableView_SelectAndFocusInputField(object sender, System.EventArgs e)
        {
            ValueInputTextBox.SelectAll();
            ValueInputTextBox.Focus();
        }

        private void HelperTableView_RegisterReferences(object sender, List<ReferenceList> e)
        {
            var dialog = new ViewReferencesDialog();
            dialog.ShowDialog(Window.GetWindow(this), e);
        }

        public HelperTableView()
		{
			InitializeComponent();
		}
    }
}
