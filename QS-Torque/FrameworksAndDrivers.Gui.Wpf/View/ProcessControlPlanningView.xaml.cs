using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Core.Enums;
using FrameworksAndDrivers.Gui.Wpf.EventArgs;
using FrameworksAndDrivers.Gui.Wpf.Interfaces;
using FrameworksAndDrivers.Gui.Wpf.ViewModel;
using FrameworksAndDrivers.Localization;
using InterfaceAdapters.Models;
using Syncfusion.Data.Extensions;
using Syncfusion.UI.Xaml.Grid;
using SelectionChangedEventArgs = System.Windows.Controls.SelectionChangedEventArgs;

namespace FrameworksAndDrivers.Gui.Wpf.View
{
    /// <summary>
    /// Interaction logic for ProcessControlPlanningView.xaml
    /// </summary>
    public partial class ProcessControlPlanningView : UserControl, IDisposable, ICanClose
    {
        private ProcessControlPlanningViewModel _viewModel;
        private LocalizationWrapper _localization;

        public ProcessControlPlanningView(ProcessControlPlanningViewModel viewModel, LocalizationWrapper localization)
        {
            InitializeComponent();
            this.DataContext = _viewModel = viewModel;
            _localization = localization;

            NextTestsOverviewDataGrid.LocalizationWrapper = _localization;
            _localization.Subscribe(NextTestsOverviewDataGrid);
            ProcessControlConditionsDataGrid.LocalizationWrapper = _localization;
            _localization.Subscribe(ProcessControlConditionsDataGrid);
            TestLevelSetDataGrid.LocalizationWrapper = _localization;
            _localization.Subscribe(TestLevelSetDataGrid);
            _viewModel.SetGuiDispatcher(this.Dispatcher);

            _viewModel.MessageBoxRequest += ViewModel_MessageBoxRequest;
            _viewModel.RequestVerifyChangesView += ViewModel_RequestVerifyChangesView;
        }

        private void ViewModel_RequestVerifyChangesView(object sender, VerifyChangesEventArgs e)
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

        private void ViewModel_MessageBoxRequest(object sender, MessageBoxEventArgs e)
        {
            e.Show();
        }

        private void ProcessControlConditionsDataGrid_OnSelectionChanged(object sender, GridSelectionChangedEventArgs e)
        {
            foreach (GridRowInfo row in e.RemovedItems)
            {
                if (row.RowData is ProcessControlConditionHumbleModel removed)
                {
                    _viewModel.SelectedProcessControlsForTestLevelAssignment.Remove(removed); 
                }
            }
            foreach (GridRowInfo row in e.AddedItems)
            {
                if (row.RowData is ProcessControlConditionHumbleModel added)
                {
                    _viewModel.SelectedProcessControlsForTestLevelAssignment.Add(added); 
                }
            }
        }

        public void Dispose()
        {
            _viewModel.MessageBoxRequest -= ViewModel_MessageBoxRequest;
            NextTestsOverviewDataGrid.Dispose();
            ProcessControlConditionsDataGrid.Dispose();
            TestLevelSetDataGrid.Dispose();
        }

        private void TestLevelNumberComboBox_OnDropDownOpened(object sender, System.EventArgs e)
        {
            if (sender is ComboBox comboBox && comboBox.DataContext is ProcessControlConditionHumbleModel dataContext)
            {
                var previousSelected = (int?)comboBox.SelectedItem;
                comboBox.Items.Clear();

                if(dataContext.TestLevelSet == null)
                {
                    return;
                }

                comboBox.Items.Add(1);
                
                if(dataContext.TestLevelSet.IsActive2)
                {
                    comboBox.Items.Add(2);
                }
                if(dataContext.TestLevelSet.IsActive3)
                {
                    comboBox.Items.Add(3);
                }

                if (previousSelected != null && comboBox.Items.Contains(previousSelected))
                {
                    comboBox.SelectedItem = previousSelected;
                }
                else
                {
                    comboBox.SelectedItem = 1;
                }
            }
        }

        private void TestLevelNumberComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox comboBox && comboBox.DataContext is ProcessControlConditionHumbleModel dataContext)
            {
                if (e.RemovedItems.Count == 0 || e.AddedItems.Count == 0)
                {
                    return;
                }

                var alreadyChanged = _viewModel.ChangedTestLevelNumbersAssignments.FirstOrDefault(x => x.Key.EqualsById(dataContext)).Key;
                var previousValue = e.RemovedItems.ToList<int?>().FirstOrDefault() ?? 1;

                if (alreadyChanged == null)
                {
                    _viewModel.ChangedTestLevelNumbersAssignments.Add(dataContext, previousValue);
                    _viewModel.NotifyChangedTestLevelNumbers();
                }
                else if (dataContext.TestLevelNumber == _viewModel.ChangedTestLevelNumbersAssignments[alreadyChanged])
                {
                    _viewModel.ChangedTestLevelNumbersAssignments.Remove(alreadyChanged);
                    _viewModel.NotifyChangedTestLevelNumbers();
                }
            }
        }

        private void TestLevelNumberComboBox_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (sender is ComboBox comboBox && comboBox.IsLoaded)
            {
                if (comboBox.DataContext is ProcessControlConditionHumbleModel dataContext)
                {
                    comboBox.Items.Clear();
                    comboBox.Items.Add(dataContext.TestLevelNumber);
                    comboBox.SelectedItem = dataContext.TestLevelNumber;
                    dataContext.RaiseTestLevelNumberChanged();
                }
                else if(comboBox.DataContext == null)
                {
                    comboBox.Items.Clear();
                }
            }
        }

        private void TestLevelNumberComboBox_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            TestLevelNumberComboBox_OnLoaded(sender, null);
        }

        public bool CanClose()
        {
            return _viewModel.CanClose();
        }

        private void TestOverviewTab_OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.Source is not TabItem)
            {
                return;
            }

            if (TestLevelSetAssignmentTab.IsSelected)
            {
                if (!_viewModel.AskForTestLevelNumbersSaving())
                {
                    e.Handled = true;
                }
            }
        }
    }
}
