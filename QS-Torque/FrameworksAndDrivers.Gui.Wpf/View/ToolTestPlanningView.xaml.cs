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
    class TestLevelNumberComboBox : ComboBox
    {
        public TestLevelNumberComboBox()
        {
            this.DataContextChanged += This_DataContextChanged;
        }

        private void This_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(e.OldValue != null)
            {
                (e.OldValue as INotifyPropertyChanged).PropertyChanged -= DataContext_PropertyChanged;
            }
            
            if(e.NewValue != null)
            {
                (e.NewValue as INotifyPropertyChanged).PropertyChanged += DataContext_PropertyChanged;
            }
        }

        private void DataContext_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(LocationToolAssignmentModelWithTestType.TestLevelSetForTestType))
            {
                this.Dispatcher.Invoke(() =>
                {
                    if ((this.DataContext as LocationToolAssignmentModelWithTestType).TestLevelSetForTestType == null)
                    {
                        this.SelectedIndex = -1;
                    }
                    else
                    {
                        this.Items.Clear();
                        this.Items.Add((this.DataContext as LocationToolAssignmentModelWithTestType).TestLevelNumberForTestType);
                        this.SelectedItem = (this.DataContext as LocationToolAssignmentModelWithTestType).TestLevelNumberForTestType;
                    }
                });
            }

            if(e.PropertyName == nameof(ProcessControlConditionHumbleModel.TestLevelSet))
            {
                this.Dispatcher.Invoke(() =>
                {
                    if ((this.DataContext as ProcessControlConditionHumbleModel).TestLevelSet == null)
                    {
                        this.SelectedIndex = -1;
                    }
                    else
                    {
                        this.Items.Clear();
                        this.Items.Add((this.DataContext as ProcessControlConditionHumbleModel).TestLevelNumber);
                        this.SelectedItem = (this.DataContext as ProcessControlConditionHumbleModel).TestLevelNumber;
                    }
                });
            }
        }
    }


    /// <summary>
    /// Interaction logic for ToolTestPlanningView.xaml
    /// </summary>
    public partial class ToolTestPlanningView : UserControl, IDisposable, ICanClose
    {
        private ToolTestPlanningViewModel _viewModel;
        private LocalizationWrapper _localization;

        public ToolTestPlanningView(ToolTestPlanningViewModel viewModel, LocalizationWrapper localization)
        {
            InitializeComponent();
            this.DataContext = _viewModel = viewModel;
            _localization = localization;

            NextTestsOverviewDataGridMca.LocalizationWrapper = _localization;
            _localization.Subscribe(NextTestsOverviewDataGridMca);
            NextTestsOverviewDataGridChk.LocalizationWrapper = _localization;
            _localization.Subscribe(NextTestsOverviewDataGridChk);
            LocationToolAssignmentDataGrid.LocalizationWrapper = _localization;
            _localization.Subscribe(LocationToolAssignmentDataGrid);
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

        private void LocationToolAssignmentDataGrid_OnSelectionChanged(object sender, GridSelectionChangedEventArgs e)
        {
            foreach (GridRowInfo row in e.RemovedItems)
            {
                if (row.RowData is LocationToolAssignmentModelWithTestType removed)
                {
                    _viewModel.SelectedLocationToolAssignmentsForTestLevelAssignment.Remove(removed); 
                }
            }
            foreach (GridRowInfo row in e.AddedItems)
            {
                if (row.RowData is LocationToolAssignmentModelWithTestType added)
                {
                    _viewModel.SelectedLocationToolAssignmentsForTestLevelAssignment.Add(added); 
                }
            }
        }

        public void Dispose()
        {
            _viewModel.MessageBoxRequest -= ViewModel_MessageBoxRequest;
            NextTestsOverviewDataGridMca.Dispose();
            NextTestsOverviewDataGridChk.Dispose();
            LocationToolAssignmentDataGrid.Dispose();
            TestLevelSetDataGrid.Dispose();
            _viewModel.Dispose();
        }

        private void TestLevelNumberComboBox_OnDropDownOpened(object sender, System.EventArgs e)
        {
            if (sender is ComboBox comboBox && comboBox.DataContext is LocationToolAssignmentModelWithTestType dataContext)
            {
                var previousSelected = (int?)comboBox.SelectedItem;
                comboBox.Items.Clear();

                if(dataContext.TestLevelSetForTestType == null)
                {
                    return;
                }

                comboBox.Items.Add(1);
                
                if(dataContext.TestLevelSetMfu.IsActive2)
                {
                    comboBox.Items.Add(2);
                }
                if(dataContext.TestLevelSetMfu.IsActive3)
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
            if (sender is ComboBox comboBox && comboBox.DataContext is LocationToolAssignmentModelWithTestType dataContext)
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
                else if (dataContext.TestLevelNumberForTestType == _viewModel.ChangedTestLevelNumbersAssignments[alreadyChanged])
                {
                    _viewModel.ChangedTestLevelNumbersAssignments.Remove(alreadyChanged);
                    _viewModel.NotifyChangedTestLevelNumbers();
                }
            }
        }

        private void TestLevelNumberComboBox_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (sender is ComboBox comboBox)
            {
                if (comboBox.DataContext is LocationToolAssignmentModelWithTestType dataContext)
                {
                    comboBox.Items.Clear();
                    comboBox.Items.Add(dataContext.TestLevelNumberForTestType);
                    dataContext.RaiseTestLevelNumberForTestTypeChanged();
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
