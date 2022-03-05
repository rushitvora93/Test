using Core.Entities;
using FrameworksAndDrivers.Gui.Wpf.ViewModel;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Threading;
using FrameworksAndDrivers.Gui.Wpf.EventArgs;
using FrameworksAndDrivers.Gui.Wpf.Interfaces;
using InterfaceAdapters;
using InterfaceAdapters.Models;
using Syncfusion.UI.Xaml.Grid;
using SelectionChangedEventArgs = System.Windows.Controls.SelectionChangedEventArgs;
using InterfaceAdapters.Localization;

namespace FrameworksAndDrivers.Gui.Wpf.View
{
    /// <summary>
    /// Interaction logic for TestLevelSetView.xaml
    /// </summary>
    public partial class TestLevelSetView : UserControl, ICanClose, IDisposable
    {
        private TestLevelSetViewModel _viewModel;
        private ILocalizationWrapper _localization;
        private int _currentMonth;
        private int _currentYear;


        public TestLevelSetView(TestLevelSetViewModel viewModel, ILocalizationWrapper localization)
        {
            InitializeComponent();
            this.DataContext = _viewModel = viewModel;
            WorkingCalendarEntryDataGridOnce.LocalizationWrapper = localization;
            WorkingCalendarEntryDataGridYearly.LocalizationWrapper = localization;
            _localization = localization;
            localization.Subscribe(WorkingCalendarEntryDataGridOnce);
            localization.Subscribe(WorkingCalendarEntryDataGridYearly);
            _currentMonth = DateTime.Today.Month;
            _currentYear = DateTime.Today.Year;
            _viewModel.SetGuiDispatcher(this.Dispatcher);

            WorkingCalendar.Date = DateTime.Today;

            PropertyChangedEventManager.AddHandler(
                _viewModel,
                (s, e) => RefillShiftManagementTimePicker(),
                nameof(ShiftManagementInterfaceAdapter.CurrentShiftManagement));
            PropertyChangedEventManager.AddHandler(
                _viewModel,
                SelectedDateInCalendar_PropertyChanged,
                nameof(TestLevelSetViewModel.SelectedDateInCalendar));
            previousWorkingCalendarEntries = _viewModel.WorkingCalendarEntries;
            _viewModel.PropertyChanged += ViewModel_PropertyChanged;
            _viewModel.WorkingCalendarEntries.CollectionChanged += WorkingCalendarEntries_CollectionChanged;
            _viewModel.MessageBoxRequest += ViewModel_MessageBoxRequest;
            _viewModel.ShowDialogRequest += ViewModel_ShowDialogRequest;
            _viewModel.WorkingCalendarEntrySelectionRequest += ViewModel_WorkingCalendarEntrySelectionRequest;
            _viewModel.RequestVerifyChangesView += ViewModel_RequestVerifyChangesView;
            _viewModel.SingleWorkingCalendarEntries.Refiltered += SingleWorkingCalendarEntries_Refiltered;
            _viewModel.YearlyWorkingCalendarEntries.Refiltered += YearlyWorkingCalendarEntries_Refiltered;
        }

        private void SelectedDateInCalendar_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (_viewModel.SelectedDateInCalendar != null)
            {
                WorkingCalendar.Date = _viewModel.SelectedDateInCalendar.Value;
            }
        }

        private void YearlyWorkingCalendarEntries_Refiltered(object sender, System.EventArgs e)
        {
            // Reset sort descriptions
            var sorts = WorkingCalendarEntryDataGridYearly.SortColumnDescriptions.ToList();
            WorkingCalendarEntryDataGridYearly.SortColumnDescriptions.Clear();
            foreach (var sort in sorts)
            {
                WorkingCalendarEntryDataGridYearly.SortColumnDescriptions.Add(sort);
            }
        }

        private void SingleWorkingCalendarEntries_Refiltered(object sender, System.EventArgs e)
        {
            // Reset sort descriptions
            var sorts = WorkingCalendarEntryDataGridOnce.SortColumnDescriptions.ToList();
            WorkingCalendarEntryDataGridOnce.SortColumnDescriptions.Clear();
            foreach (var sort in sorts)
            {
                WorkingCalendarEntryDataGridOnce.SortColumnDescriptions.Add(sort);
            }
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

        private void RefillSpecialDates()
        {
            Dispatcher.InvokeAsync(() =>
            {
                WorkingCalendar.SpecialDates.Clear();

                for (int i = 0; i < DateTime.DaysInMonth(_currentYear, _currentMonth); i++)
                {
                    WorkingCalendar.SetToolTip(new Date(_currentYear, _currentMonth, i), null);
                }

                foreach (var dayInMonth in Enumerable.Range(1, DateTime.DaysInMonth(_currentYear, _currentMonth)).Select(n => new DateTime(_currentYear, _currentMonth, n)))
                {
                    if (dayInMonth.DayOfWeek == DayOfWeek.Saturday && _viewModel.AreSaturdaysFree ||
                        dayInMonth.DayOfWeek == DayOfWeek.Sunday && _viewModel.AreSundaysFree)
                    {
                        string xamlTemplate = "<DataTemplate>" +
                                                  "<Border BorderThickness=\"0\" " +
                                                          "Background=\"" + _viewModel.WeekendColor.ToString() + "\" " +
                                                          "Width=\"43\" " +
                                                          "Height=\"43\"> " +
                                                      "<TextBlock VerticalAlignment=\"Center\" " +
                                                                 "HorizontalAlignment=\"Center\" " +
                                                                 "TextAlignment=\"Center\" " +
                                                                 "Text=\"" + dayInMonth.Date.Day.ToString() + "\"/> " +
                                                  "</Border>" +
                                              "</DataTemplate>";

                        var context = new ParserContext();
                        context.XamlTypeMapper = new XamlTypeMapper(new string[0]);
                        context.XmlnsDictionary.Add("",
                            "http://schemas.microsoft.com/winfx/2006/xaml/presentation");
                        var template = (DataTemplate)XamlReader.Parse(xamlTemplate, context);

                        WorkingCalendar.SpecialDates.Add(
                            new SpecialDate()
                            {
                                Date = new DateTime(_currentYear, _currentMonth, dayInMonth.Date.Day),
                                CellTemplate = template
                            });
                    }
                }

                foreach (var entry in _viewModel.WorkingCalendarEntries)
                {
                    if (entry.Date.Month == _currentMonth && entry.Repetition == WorkingCalendarEntryRepetition.Yearly ||
                        entry.Date.Month == _currentMonth && entry.Date.Year == _currentYear)
                    {
                        var specialDate = new DateTime(_currentYear, _currentMonth, entry.Date.Day);

                        // Continue just if the entry is valid (e. g. no holidays at the weekend, no extra shift during the week)
                        if (_viewModel.AreSaturdaysFree && specialDate.DayOfWeek == DayOfWeek.Saturday && entry.Type == WorkingCalendarEntryType.Holiday)
                        {
                            continue;
                        }
                        if (_viewModel.AreSundaysFree && specialDate.DayOfWeek == DayOfWeek.Sunday && entry.Type == WorkingCalendarEntryType.Holiday)
                        {
                            continue;
                        }
                        if (specialDate.DayOfWeek != DayOfWeek.Saturday && specialDate.DayOfWeek != DayOfWeek.Sunday && entry.Type == WorkingCalendarEntryType.ExtraShift)
                        {
                            continue;
                        }
                        if (!_viewModel.AreSaturdaysFree && specialDate.DayOfWeek == DayOfWeek.Saturday && entry.Type == WorkingCalendarEntryType.ExtraShift)
                        {
                            continue;
                        }
                        if (!_viewModel.AreSundaysFree && specialDate.DayOfWeek == DayOfWeek.Sunday && entry.Type == WorkingCalendarEntryType.ExtraShift)
                        {
                            continue;
                        }

                        string xamlTemplate = "<DataTemplate>" +
                                                "<Border BorderThickness=\"0\" " +
                                                        "Background=\"" + (entry.Type == WorkingCalendarEntryType.Holiday ? _viewModel.HolidayColor.ToString() : _viewModel.ExtraShiftColor.ToString()) + "\" " +
                                                        "Width=\"43\" " +
                                                        "Height=\"43\"> " +
                                                    "<TextBlock VerticalAlignment=\"Center\" " +
                                                               "HorizontalAlignment=\"Center\" " +
                                                               "TextAlignment=\"Center\" " +
                                                               "Text=\"" + entry.Date.Day.ToString() + "\"/> " +
                                                "</Border>" +
                                              "</DataTemplate>";

                        var context = new ParserContext();
                        context.XamlTypeMapper = new XamlTypeMapper(new string[0]);
                        context.XmlnsDictionary.Add("", "http://schemas.microsoft.com/winfx/2006/xaml/presentation");
                        var template = (DataTemplate)XamlReader.Parse(xamlTemplate, context);

                        WorkingCalendar.SpecialDates.Add(
                            new SpecialDate()
                            {
                                Date = specialDate,
                                CellTemplate = template
                            });
                        WorkingCalendar.SetToolTip(new Date(_currentYear, _currentMonth, entry.Date.Day),
                            new ToolTip()
                            {
                                Content = entry.Description +
                                          " (" +
                                          (entry.Repetition == WorkingCalendarEntryRepetition.Once
                                              ? _localization.Strings.GetParticularString("WorkingCalendarRepetition", "Once")
                                              : _localization.Strings.GetParticularString("WorkingCalendarRepetition", "Yearly")) +
                                          ")"
                            });
                    }
                }
            });
        }

        private void WorkingCalendar_OnMonthChanged(object sender, MonthChangedEventArgs args)
        {
            _currentMonth = args.NewMonth;
            _currentYear = WorkingCalendar.Date.Year;
            RefillSpecialDates();
        }

        private void RefillShiftManagementTimePicker()
        {
            Dispatcher.Invoke(() =>
            {
                FirstShiftStartTimePicker.Value = _viewModel.CurrentShiftManagement.FirstShiftStart;
                FirstShiftEndTimePicker.Value = _viewModel.CurrentShiftManagement.FirstShiftEnd;
                SecondShiftStartTimePicker.Value = _viewModel.CurrentShiftManagement.SecondShiftStart;
                SecondShiftEndTimePicker.Value = _viewModel.CurrentShiftManagement.SecondShiftEnd;
                ThirdShiftStartTimePicker.Value = _viewModel.CurrentShiftManagement.ThirdShiftStart;
                ThirdShiftEndTimePicker.Value = _viewModel.CurrentShiftManagement.ThirdShiftEnd;
                ChangeOfDayTimePicker.Value = _viewModel.CurrentShiftManagement.ChangeOfDay;
            });
        }

        private void WorkingCalendar_DateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            _viewModel.SelectedDateInCalendar = (DateTime?)e.NewValue;
        }

        private void WorkingCalendarEntries_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RefillSpecialDates();
        }

        ObservableCollection<WorkingCalendarEntryModel> previousWorkingCalendarEntries;
        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(TestLevelSetViewModel.WorkingCalendarEntries) && _viewModel.WorkingCalendarEntries != null)
            {
                if (previousWorkingCalendarEntries != null) { previousWorkingCalendarEntries.CollectionChanged -= WorkingCalendarEntries_CollectionChanged; }
                previousWorkingCalendarEntries = _viewModel.WorkingCalendarEntries;
                _viewModel.WorkingCalendarEntries.CollectionChanged += WorkingCalendarEntries_CollectionChanged;
                RefillSpecialDates();
            }

            if (e.PropertyName == nameof(TestLevelSetViewModel.SelectedDateInCalendar))
            {
                if (_viewModel.SelectedDateInCalendar != null)
                {
                    Dispatcher.Invoke(() => WorkingCalendar.Date = _viewModel.SelectedDateInCalendar.Value);
                }
            }

            if (e.PropertyName == nameof(TestLevelSetViewModel.AreSaturdaysFree) ||
                e.PropertyName == nameof(TestLevelSetViewModel.AreSundaysFree))
            {
                RefillSpecialDates();
            }
        }

        private void ViewModel_ShowDialogRequest(object sender, ICanShowDialog e)
        {
            e.ShowDialog();
        }

        private void ViewModel_MessageBoxRequest(object sender, EventArgs.MessageBoxEventArgs e)
        {
            e.Show();
        }

        private void ViewModel_WorkingCalendarEntrySelectionRequest(object sender, WorkingCalendarEntryModel e)
        {
            Dispatcher.Invoke(() =>
            {
                if (e.Repetition == WorkingCalendarEntryRepetition.Once && WorkingCalendarEntryDataGridOnce.SelectedItem == e ||
                    e.Repetition == WorkingCalendarEntryRepetition.Yearly && WorkingCalendarEntryDataGridYearly.SelectedItem == e)
                {
                    return;
                }

                WorkingCalendarEntryDataGridOnce.ClearSelections(false);
                WorkingCalendarEntryDataGridYearly.ClearSelections(false);

                if (e.Repetition == WorkingCalendarEntryRepetition.Once)
                {
                    WorkingCalendarEntryDataGridOnce.SelectedItem = e;
                }
                else if (e.Repetition == WorkingCalendarEntryRepetition.Yearly)
                {
                    WorkingCalendarEntryDataGridYearly.SelectedItem = e;
                }
            });
        }
        
        private void ShiftManagementTimePicker_OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (_viewModel == null || _viewModel.CurrentShiftManagement == null || e.NewValue == null)
            {
                return;
            }

            TimeSpan time;

            if (e.NewValue is TimeSpan timeSpan)
            {
                time = timeSpan;
            }
            else if (e.NewValue is DateTime dateTime)
            {
                time = dateTime.TimeOfDay;
            }
            else if (e.NewValue is string s)
            {
                time = TimeSpan.Parse(s);
            }
            else
            {
                return;
            }

            if (sender == FirstShiftStartTimePicker)
            {
                _viewModel.CurrentShiftManagement.FirstShiftStart = time;
            }
            else if (sender == FirstShiftEndTimePicker)
            {
                _viewModel.CurrentShiftManagement.FirstShiftEnd = time;
            }
            else if (sender == SecondShiftStartTimePicker)
            {
                _viewModel.CurrentShiftManagement.SecondShiftStart = time;
            }
            else if (sender == SecondShiftEndTimePicker)
            {
                _viewModel.CurrentShiftManagement.SecondShiftEnd = time;
            }
            else if (sender == ThirdShiftStartTimePicker)
            {
                _viewModel.CurrentShiftManagement.ThirdShiftStart = time;
            }
            else if (sender == ThirdShiftEndTimePicker)
            {
                _viewModel.CurrentShiftManagement.ThirdShiftEnd = time;
            }
            else if (sender == ChangeOfDayTimePicker)
            {
                _viewModel.CurrentShiftManagement.ChangeOfDay = time;
            }

            ValidateShiftManagement();
        }

        private void ValidateShiftManagement()
        {
            var invalidShiftTimes = _viewModel.CurrentShiftManagement.Entity.Validate();

            FirstShiftStartTimePickerBorder.BorderThickness = new Thickness(0);
            FirstShiftEndTimePickerBorder.BorderThickness = new Thickness(0);
            SecondShiftStartTimePickerBorder.BorderThickness = new Thickness(0);
            SecondShiftEndTimePickerBorder.BorderThickness = new Thickness(0);
            ThirdShiftStartTimePickerBorder.BorderThickness = new Thickness(0);
            ThirdShiftEndTimePickerBorder.BorderThickness = new Thickness(0);
            FirstShiftStartTimePicker.ToolTip = null;
            FirstShiftEndTimePicker.ToolTip = null;
            SecondShiftStartTimePicker.ToolTip = null;
            SecondShiftEndTimePicker.ToolTip = null;
            ThirdShiftStartTimePicker.ToolTip = null;
            ThirdShiftEndTimePicker.ToolTip = null;

            if (invalidShiftTimes.Contains(nameof(ShiftManagement.FirstShiftStart)))
            {
                FirstShiftStartTimePickerBorder.BorderThickness = new Thickness(2);
                FirstShiftStartTimePicker.ToolTip = new ToolTip()
                {
                    Content = new TextBlock() { Text = _localization.Strings.GetParticularString("ShiftManagementValidationError", "The shifts must not overlap") }
                };
            }

            if (invalidShiftTimes.Contains(nameof(ShiftManagement.FirstShiftEnd)))
            {
                FirstShiftEndTimePickerBorder.BorderThickness = new Thickness(2);
                FirstShiftEndTimePicker.ToolTip = new ToolTip()
                {
                    Content = new TextBlock() { Text = _localization.Strings.GetParticularString("ShiftManagementValidationError", "The shifts must not overlap") }
                };
            }

            if (invalidShiftTimes.Contains(nameof(ShiftManagement.SecondShiftStart)))
            {
                SecondShiftStartTimePickerBorder.BorderThickness = new Thickness(2);
                SecondShiftStartTimePicker.ToolTip = new ToolTip()
                {
                    Content = new TextBlock() { Text = _localization.Strings.GetParticularString("ShiftManagementValidationError", "The shifts must not overlap") }
                };
            }

            if (invalidShiftTimes.Contains(nameof(ShiftManagement.SecondShiftEnd)))
            {
                SecondShiftEndTimePickerBorder.BorderThickness = new Thickness(2);
                SecondShiftEndTimePicker.ToolTip = new ToolTip()
                {
                    Content = new TextBlock() { Text = _localization.Strings.GetParticularString("ShiftManagementValidationError", "The shifts must not overlap") }
                };
            }

            if (invalidShiftTimes.Contains(nameof(ShiftManagement.ThirdShiftStart)))
            {
                ThirdShiftStartTimePickerBorder.BorderThickness = new Thickness(2);
                ThirdShiftStartTimePicker.ToolTip = new ToolTip()
                {
                    Content = new TextBlock() { Text = _localization.Strings.GetParticularString("ShiftManagementValidationError", "The shifts must not overlap") }
                };
            }

            if (invalidShiftTimes.Contains(nameof(ShiftManagement.ThirdShiftEnd)))
            {
                ThirdShiftEndTimePickerBorder.BorderThickness = new Thickness(2);
                ThirdShiftEndTimePicker.ToolTip = new ToolTip()
                {
                    Content = new TextBlock() { Text = _localization.Strings.GetParticularString("ShiftManagementValidationError", "The shifts must not overlap") }
                };
            }
        }

        private void WorkingCalendarEntryDataGridOnce_OnSelectionChanged(object sender, GridSelectionChangedEventArgs e)
        {
            WorkingCalendarEntryDataGridYearly.ClearSelections(false);
            var selectedEntry = (e.AddedItems.FirstOrDefault() as GridRowInfo)?.RowData as WorkingCalendarEntryModel;

            if (_viewModel.SelectedEntryInList != selectedEntry)
            {
                _viewModel.SelectedEntryInList = selectedEntry;
            }
        }

        private void WorkingCalendarEntryDataGridYearly_OnSelectionChanged(object sender, GridSelectionChangedEventArgs e)
        {
            WorkingCalendarEntryDataGridOnce.ClearSelections(false);
            var selectedEntry = (e.AddedItems.FirstOrDefault() as GridRowInfo)?.RowData as WorkingCalendarEntryModel;

            if (_viewModel.SelectedEntryInList != selectedEntry)
            {
                _viewModel.SelectedEntryInList = selectedEntry;
            }
        }
        
        public bool CanClose()
        {
            return _viewModel.CanClose();
        }

        private void IsShiftActiveToggleButton_Changed(object sender, RoutedEventArgs e)
        {
            ValidateShiftManagement();
        }

        private void WorkingCalendarTab_Selected(object sender, RoutedEventArgs e)
        {
            _viewModel.LoadWorkingCalendarIfNotLoadedYet();
        }
        
        /// <returns>Selection change successful (returns false = cancel selection)</returns>
        private bool UnselectTestLevelSet()
        {
            if (_viewModel.HasTestLevelSetChanged())
            {
                var result = Task.Run(() => MessageBox.Show(_localization.Strings.GetParticularString("TestLevelSets", "The selected test level set has unsaved changes. Du you want to save it?"),
                    _localization.Strings.GetParticularString("TestLevelSets", "Warning"),
                    MessageBoxButton.YesNoCancel,
                    MessageBoxImage.Warning)).Result;

                if (result == MessageBoxResult.Yes)
                {
                    _viewModel.SaveTestLevelSetCommand.Invoke(null);
                    return true;
                }
                else if (result == MessageBoxResult.No)
                {
                    _viewModel.ResetTestLevelSet();
                    return true;
                }
                else if (result == MessageBoxResult.Cancel)
                {
                    return false;
                }
            }
            return true;
        }

        /// <returns>Selection change successful (returns false = cancel selection)</returns>
        private bool UnselectShiftManagement()
        {
            if (_viewModel.HasShiftManagementChanged())
            {
                if (_viewModel.CurrentShiftManagement.Entity.Validate().Any())
                {
                    // If there are invalid inputs
                    ShiftManagementTab.IsSelected = true;

                    MessageBox.Show(_localization.Strings.GetParticularString("Shift management", "Your inputs are invalid. You have to enter valid values to change the tab."),
                        _localization.Strings.GetParticularString("Shift management", "Error"),
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                    return false;
                }
                else
                {
                    var result = Task.Run(() => MessageBox.Show(_localization.Strings.GetParticularString("Shift management", "The shift management has changed. Du you want to save it?"),
                        _localization.Strings.GetParticularString("Shift management", "Warning"),
                        MessageBoxButton.YesNoCancel,
                        MessageBoxImage.Warning)).Result;

                    if (result == MessageBoxResult.Yes)
                    {
                        _viewModel.SaveShiftManagementCommand.Invoke(null);
                        return true;
                    }
                    else if (result == MessageBoxResult.No)
                    {
                        _viewModel.ResetShiftManagement();
                        return true;
                    }
                    else if (result == MessageBoxResult.Cancel)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void TestLevelSetTab_OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(e.Source is not TabItem)
            {
                return;
            }

            if (ShiftManagementTab.IsSelected)
            {
                if (!UnselectShiftManagement())
                {
                    e.Handled = true;
                } 
            }
        }

        private void WorkingCalendarTab_OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.Source is not TabItem)
            {
                return;
            }

            if (ShiftManagementTab.IsSelected)
            {
                if (!UnselectShiftManagement())
                {
                    e.Handled = true;
                }
            }
            if (TestLevelSetTab.IsSelected)
            {
                if (!UnselectTestLevelSet())
                {
                    e.Handled = true;
                }
            }
        }

        private void ShiftManagementTab_OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.Source is not TabItem)
            {
                return;
            }

            if (TestLevelSetTab.IsSelected)
            {
                if (!UnselectTestLevelSet())
                {
                    e.Handled = true;
                } 
            }
        }

        private void TabItem_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Source is not TabItem)
            {
                return;
            }
            if (e.Key != Key.Tab)
            {
                e.Handled = true; 
            }
        }

        public void Dispose()
        {
            PropertyChangedEventManager.RemoveHandler(
                _viewModel,
                SelectedDateInCalendar_PropertyChanged,
                nameof(TestLevelSetViewModel.SelectedDateInCalendar));

            _viewModel.WorkingCalendarEntries.CollectionChanged -= WorkingCalendarEntries_CollectionChanged;
            _viewModel.PropertyChanged -= ViewModel_PropertyChanged;
            _viewModel.MessageBoxRequest -= ViewModel_MessageBoxRequest;
            _viewModel.ShowDialogRequest -= ViewModel_ShowDialogRequest;
            _viewModel.WorkingCalendarEntrySelectionRequest -= ViewModel_WorkingCalendarEntrySelectionRequest;
            _viewModel.RequestVerifyChangesView -= ViewModel_RequestVerifyChangesView;
            _viewModel.SingleWorkingCalendarEntries.Refiltered -= SingleWorkingCalendarEntries_Refiltered;
            _viewModel.YearlyWorkingCalendarEntries.Refiltered -= YearlyWorkingCalendarEntries_Refiltered;
            _viewModel.Dispose();
            WorkingCalendar.Dispose();
            WorkingCalendarEntryDataGridOnce.Dispose();
            WorkingCalendarEntryDataGridYearly.Dispose();
            FirstShiftStartTimePicker.Dispose();
            FirstShiftEndTimePicker.Dispose();
            SecondShiftStartTimePicker.Dispose();
            SecondShiftEndTimePicker.Dispose();
            ThirdShiftStartTimePicker.Dispose();
            ThirdShiftEndTimePicker.Dispose();
            ChangeOfDayTimePicker.Dispose();
        }
    }
}
