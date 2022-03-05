using System;
using System.Collections.Generic;
using System.Windows;
using FrameworksAndDrivers.Gui.Wpf.EventArgs;
using FrameworksAndDrivers.Gui.Wpf.Interfaces;
using FrameworksAndDrivers.Gui.Wpf.ViewModel;
using System.Windows.Controls;
using FrameworksAndDrivers.Gui.Wpf.View.Dialogs;
using FrameworksAndDrivers.Localization;
using InterfaceAdapters.Models;
using Syncfusion.Windows.Shared;

namespace FrameworksAndDrivers.Gui.Wpf.View
{
    /// <summary>
    /// Interaction logic for ToleranceClassView.xaml
    /// </summary>
    public partial class ToleranceClassView : UserControl, ICanClose, IDisposable
    {
		public ToleranceClassView(ToleranceClassViewModel viewModel, LocalizationWrapper localization)
		{
			InitializeComponent();
            _viewModel = viewModel;
			DataContext = viewModel;
			viewModel.RegisterEventHandler(ToleranceClass_MessageBoxRequest);
			viewModel.SetDispatcher(Dispatcher);
            DataGrid.LocalizationWrapper = localization;
            localization.Subscribe(DataGrid);
            viewModel.ReferencesDialogRequest += ViewModelOnReferencesDialogRequest;
        }

        private void ViewModelOnReferencesDialogRequest(object sender, List<ReferenceList> e)
        {
            var dialog = new ViewReferencesDialog();
            dialog.ShowDialog(Window.GetWindow(this), e);
        }


        private void ToleranceClass_MessageBoxRequest(object sender, MessageBoxEventArgs e)
        {
            e.Show();
        }

        public bool CanClose()
        {
            if (DataContext is ICanClose cc)
            {
                return cc.CanClose();
            }

            return true;
        }

        private void RelativeRadioButton_OnChecked(object sender, RoutedEventArgs e)
        {
            if (!(DataContext is ToleranceClassViewModel vm))
            {
                return;
            }

            ErrorTextSymmetricLimits.Visibility = Visibility.Hidden;
            ErrorTextLowerLimit.Visibility = Visibility.Hidden;
            ErrorTextUpperLimit.Visibility = Visibility.Hidden;
            //Checks if Limits are lower than 100 for relative limits
            if (vm.SelectedToleranceClassModel == _lastToleranceClassModel)
            {
                if (vm.SelectedToleranceClassModel.SymmetricalLimits)
                {
                    if (vm.SelectedToleranceClassModel.SymmetricLimitsValue > 100)
                    {
                        ErrorTextSymmetricLimits.Visibility = Visibility.Visible;
                        vm.SelectedToleranceClassModel.Relative = false;
                    }
                }
                else
                {
                    if (vm.SelectedToleranceClassModel.LowerLimit > 100)
                    {
                        ErrorTextLowerLimit.Visibility = Visibility.Visible;
                        vm.SelectedToleranceClassModel.Relative = false;
                    }
                    if (vm.SelectedToleranceClassModel.UpperLimit > 100)
                    {
                        ErrorTextUpperLimit.Visibility = Visibility.Visible;
                        vm.SelectedToleranceClassModel.Relative = false;
                    }
                }
            }
            _lastToleranceClassModel = vm.SelectedToleranceClassModel;
        }

        private ToleranceClassModel _lastToleranceClassModel;
        private ToleranceClassViewModel _viewModel;

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 1)
            {
                return;
            }
            _lastToleranceClassModel = e.AddedItems[0] as ToleranceClassModel;
            ErrorTextSymmetricLimits.Visibility = Visibility.Hidden;
            ErrorTextLowerLimit.Visibility = Visibility.Hidden;
            ErrorTextUpperLimit.Visibility = Visibility.Hidden;
        }

        private void UpDownOnValueChanging(object sender, ValueChangingEventArgs e)
        {
            if (!(DataContext is ToleranceClassViewModel vm))
            {
                return;
            }
            if (!vm.SelectedToleranceClassModel.Relative)
            {
                return;
            }
            if ((double) e.NewValue > 100)
            {
                e.Cancel = true;
            }
        }

        public void Dispose()
        {
            SymmetricUpDown?.Dispose();
            LowerUpDown?.Dispose();
            UpperUpDown?.Dispose();
            DataGrid?.Dispose();
            _viewModel.ReferencesDialogRequest -= ViewModelOnReferencesDialogRequest;
            _viewModel.MessageBoxRequest -= ToleranceClass_MessageBoxRequest;
            _viewModel = null;
            DataContext = null;
        }
    }
}
