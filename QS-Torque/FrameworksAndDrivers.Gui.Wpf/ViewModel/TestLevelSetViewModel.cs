using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Client.Core.Diffs;
using Client.UseCases.UseCases;
using Core.Diffs;
using Core.Entities;
using Core.UseCases;
using FrameworksAndDrivers.Gui.Wpf.EventArgs;
using FrameworksAndDrivers.Gui.Wpf.Interfaces;
using FrameworksAndDrivers.Gui.Wpf.Model;
using FrameworksAndDrivers.Gui.Wpf.View;
using InterfaceAdapters;
using InterfaceAdapters.Localization;
using InterfaceAdapters.Models;
using Syncfusion.Data.Extensions;
using Syncfusion.Windows.Controls;
using WorkingCalendarEntryModel = InterfaceAdapters.Models.WorkingCalendarEntryModel;

namespace FrameworksAndDrivers.Gui.Wpf.ViewModel
{
    public class TestLevelSetViewModel 
        : BindableBase, 
        ICanClose, 
        INotifyPropertyChanged, 
        IShiftManagementErrorHandler, 
        IWorkingCalendarErrorHandler, 
        ITestLevelSetErrorHandler,
        IProcessControlErrorGui,
        IGetUpdatedByLanguageChanges, 
        IShiftManagementDiffShower,
        ITestLevelSetDiffShower,
        IDisposable
    {
        private ITestLevelSetUseCase _testLevelSetUseCase;
        private ITestLevelSetInterface _testLevelSetInterface;
        private IWorkingCalendarUseCase _workingCalendarUseCase;
        private IWorkingCalendarInterface _workingCalendarInterface;
        private IShiftManagementUseCase _shiftManagementUseCase;
        private IShiftManagementInterface _shiftManagementInterface;
        private ILocalizationWrapper _localization;
        private IStartUp _startUp;
        private Dispatcher _guiDispatcher;

        public Color HolidayColor { get => Colors.LightSkyBlue; }
        public Color ExtraShiftColor { get => Colors.LimeGreen; }
        public Color WeekendColor { get => Colors.LightGray; }

        
        public ObservableCollection<TestLevelSetModel> TestLevelSets
        {
            get => _testLevelSetInterface.TestLevelSets;
        }

        public TestLevelSetModel SelectedTestLevelSet
        {
            get => _testLevelSetInterface.SelectedTestLevelSet;
            set
            {
                if(SelectedTestLevelSet?.Entity.EqualsById(value?.Entity) == false && SelectedTestLevelSet != null && !SelectedTestLevelSet.Entity.EqualsByContent(TestLevelSetWithoutChanges.Entity))
                {
                    MessageBoxResult testLevelSetResult = MessageBoxResult.None;

                    var testLevelSetArgs = new MessageBoxEventArgs(r => testLevelSetResult = r,
                        _localization.Strings.GetParticularString("TestLevelSetView", "There are unsaved changes. Do you want to save them?"),
                        _localization.Strings.GetParticularString("TestLevelSetView", "Warning"),
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Warning);
                    MessageBoxRequest?.Invoke(this, testLevelSetArgs);

                    if (testLevelSetResult == MessageBoxResult.Yes)
                    {
                        SaveTestLevelSetCommand.Invoke(null);
                    }
                    else if (testLevelSetResult == MessageBoxResult.No)
                    {
                        SelectedTestLevelSet.UpdateWith(TestLevelSetWithoutChanges.Entity);
                        return;
                    }
                }

                _testLevelSetInterface.SelectedTestLevelSet = value;
            }
        }

        public TestLevelSetModel TestLevelSetWithoutChanges
        {
            get => _testLevelSetInterface.TestLevelSetWithoutChanges;
        }

        private IFilteredObservableCollectionExtension<WorkingCalendarEntryModel> _singleWorkingCalendarEntries;
        public IFilteredObservableCollectionExtension<WorkingCalendarEntryModel> SingleWorkingCalendarEntries
        {
            get => _singleWorkingCalendarEntries;
            set
            {
                _singleWorkingCalendarEntries = value;
                RaisePropertyChanged();
            }
        }

        private IFilteredObservableCollectionExtension<WorkingCalendarEntryModel> _yearlyWorkingCalendarEntries;
        public IFilteredObservableCollectionExtension<WorkingCalendarEntryModel> YearlyWorkingCalendarEntries
        {
            get => _yearlyWorkingCalendarEntries;
            set
            {
                _yearlyWorkingCalendarEntries = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<WorkingCalendarEntryModel> WorkingCalendarEntries
        {
            get { return _workingCalendarInterface.WorkingCalendarEntries; }
        }

        public WorkingCalendarEntryModel SelectedEntryInList
        {
            get { return _workingCalendarInterface.SelectedEntryInList; }
            set { _workingCalendarInterface.SelectedEntryInList = value; }
        }

        public DateTime? SelectedDateInCalendar
        {
            get { return _workingCalendarInterface.SelectedDateInCalendar; }
            set
            {
                _workingCalendarInterface.SelectedDateInCalendar = value;
            }
        }

        public bool AreSaturdaysFree
        {
            get { return _workingCalendarInterface.AreSaturdaysFree; }
            set
            {
                var valueChanged = _workingCalendarInterface.AreSaturdaysFree != value;
                _workingCalendarInterface.AreSaturdaysFree = value;

                if (valueChanged)
                {
                    _singleWorkingCalendarEntries.RefilterCollection();
                    _yearlyWorkingCalendarEntries.RefilterCollection();
                    _workingCalendarUseCase.SetWeekendSettings(new WorkingCalendarDiff(_workingCalendarInterface.WorkingCalendarWithoutChanges,
                        new WorkingCalendar()
                        {
                            AreSaturdaysFree = _workingCalendarInterface.AreSaturdaysFree,
                            AreSundaysFree = _workingCalendarInterface.AreSundaysFree
                        }), this, this);
                    RaisePropertyChanged();
                }
            }
        }

        public bool AreSundaysFree
        {
            get { return _workingCalendarInterface.AreSundaysFree; }
            set
            {
                var valueChanged = _workingCalendarInterface.AreSundaysFree != value;
                _workingCalendarInterface.AreSundaysFree = value;

                if (valueChanged)
                {
                    _singleWorkingCalendarEntries.RefilterCollection();
                    _yearlyWorkingCalendarEntries.RefilterCollection();
                    _workingCalendarUseCase.SetWeekendSettings(new WorkingCalendarDiff(_workingCalendarInterface.WorkingCalendarWithoutChanges,
                        new WorkingCalendar()
                        {
                            AreSaturdaysFree = _workingCalendarInterface.AreSaturdaysFree,
                            AreSundaysFree = _workingCalendarInterface.AreSundaysFree
                        }), this, this);
                    RaisePropertyChanged();
                }
            }
        }

        public ShiftManagementModel CurrentShiftManagement
        {
            get => _shiftManagementInterface.CurrentShiftManagement;
            set => _shiftManagementInterface.CurrentShiftManagement = value;
        }

        public ShiftManagementModel ShiftManagementWithoutChanges => _shiftManagementInterface.ShiftManagementWithoutChanges;

        public RelayCommand LoadedCommand { get; private set; }
        public RelayCommand AddTestLevelSetCommand { get; private set; }
        public RelayCommand RemoveTestLevelSetCommand { get; private set; }
        public RelayCommand SaveTestLevelSetCommand { get; private set; }
        public RelayCommand AddWorkingCalendarEntryCommand { get; private set; }
        public RelayCommand RemoveWorkingCalendarEntryCommand { get; private set; }
        public RelayCommand SaveShiftManagementCommand { get; private set; }

        private bool _isShiftManagementCurrentlySaving = false;


        public event EventHandler<MessageBoxEventArgs> MessageBoxRequest;
        public event EventHandler<ICanShowDialog> ShowDialogRequest;
        public event EventHandler<WorkingCalendarEntryModel> WorkingCalendarEntrySelectionRequest;
        public event EventHandler<VerifyChangesEventArgs> RequestVerifyChangesView;


        public TestLevelSetViewModel(ITestLevelSetUseCase testLevelSetUseCase, 
                                 ITestLevelSetInterface testLevelSetInterface,
                                 IWorkingCalendarUseCase workingCalendarUseCase,
                                 IWorkingCalendarInterface workingCalendarInterface,
                                 IShiftManagementUseCase shiftManagementUseCase,
                                 IShiftManagementInterface shiftManagementInterface,
                                 ILocalizationWrapper localization,
                                 IStartUp startUp,
                                 IFilteredObservableCollectionExtension<WorkingCalendarEntryModel> singleWorkingCalendarEntries = null,
                                 IFilteredObservableCollectionExtension<WorkingCalendarEntryModel> yearlyWorkingCalendarEntries = null)
        {
            _testLevelSetUseCase = testLevelSetUseCase;
            _testLevelSetInterface = testLevelSetInterface;
            _workingCalendarUseCase = workingCalendarUseCase;
            _workingCalendarInterface = workingCalendarInterface;
            _shiftManagementUseCase = shiftManagementUseCase;
            _shiftManagementInterface = shiftManagementInterface;
            _localization = localization;
            _startUp = startUp;

            _localization.Subscribe(this);
            WireViewModelToTestLevelSetInterface();
            WireViewModelToWorkingCalendarInterface();
            WireViewModelToShiftManagementInterface();

            _testLevelSetInterface.ShowLoadingControlRequest += InterfaceAdapter_ShowLoadingControlRequest;
            _workingCalendarInterface.ShowLoadingControlRequest += InterfaceAdapter_ShowLoadingControlRequest;
            _shiftManagementInterface.ShowLoadingControlRequest += InterfaceAdapter_ShowLoadingControlRequest;

            SingleWorkingCalendarEntries = singleWorkingCalendarEntries ?? new FilteredObservableCollectionExtension<WorkingCalendarEntryModel>(_workingCalendarInterface.WorkingCalendarEntries);
            YearlyWorkingCalendarEntries = yearlyWorkingCalendarEntries ?? new FilteredObservableCollectionExtension<WorkingCalendarEntryModel>(_workingCalendarInterface.WorkingCalendarEntries);
            SingleWorkingCalendarEntries.Filter = x =>
            {
                var model = (WorkingCalendarEntryModel)x;
                return model.Repetition == WorkingCalendarEntryRepetition.Once && WorkingCalendarEntryValidator.IsWorkingCalendarEntryValidAtDate(model.Entity, AreSaturdaysFree, AreSundaysFree);
            };
            YearlyWorkingCalendarEntries.Filter = x => (x as WorkingCalendarEntryModel).Repetition == WorkingCalendarEntryRepetition.Yearly;

            LoadedCommand = new RelayCommand(LoadedExecute, LoadedCanExecute);
            AddTestLevelSetCommand = new RelayCommand(AddTestLevelSetExecute, AddTestLevelSetCanExecute);
            RemoveTestLevelSetCommand = new RelayCommand(RemoveTestLevelSetExecute, RemoveTestLevelSetCanExecute);
            SaveTestLevelSetCommand = new RelayCommand(SaveTestLevelSetExecute, SaveTestLevelSetCanExecute);
            AddWorkingCalendarEntryCommand = new RelayCommand(AddWorkingCalendarEntryExecute, AddWorkingCalendarEntryCanExecute);
            RemoveWorkingCalendarEntryCommand = new RelayCommand(RemoveWorkingCalendarEntryExecute, RemoveWorkingCalendarEntryCanExecute);
            SaveShiftManagementCommand = new RelayCommand(SaveShiftManagementExecute, SaveShiftManagementCanExecute);
        }

        private void InterfaceAdapter_ShowLoadingControlRequest(object sender, bool e)
        {
            _startUp.RaiseShowLoadingControl(e);
        }

        public bool HasTestLevelSetChanged()
        {
            return !(SelectedTestLevelSet?.Entity.EqualsByContent(TestLevelSetWithoutChanges.Entity) ?? TestLevelSetWithoutChanges == null);
        }

        public void ResetTestLevelSet()
        {
            TestLevelSets.First(x => x.Entity.EqualsById(TestLevelSetWithoutChanges.Entity)).UpdateWith(TestLevelSetWithoutChanges.Entity);
            SelectedTestLevelSet = new TestLevelSetModel(TestLevelSetWithoutChanges.Entity.CopyDeep());
        }

        public bool HasShiftManagementChanged()
        {
            return !_shiftManagementInterface.CurrentShiftManagement?.EqualsByContent(_shiftManagementInterface.ShiftManagementWithoutChanges.Entity) ?? ShiftManagementWithoutChanges == null;
        }

        public void ResetShiftManagement()
        {
            _shiftManagementInterface.CurrentShiftManagement = new ShiftManagementModel(_shiftManagementInterface.ShiftManagementWithoutChanges.Entity.CopyDeep(), _localization);
        }

        public void SetGuiDispatcher(Dispatcher guiDispatcher)
        {
            _testLevelSetInterface.SetGuiDispatcher(guiDispatcher);
            _workingCalendarInterface.SetGuiDispatcher(guiDispatcher);
            _guiDispatcher = guiDispatcher;
        }

        private void WireViewModelToTestLevelSetInterface()
        {
            PropertyChangedEventManager.AddHandler(
                _testLevelSetInterface,
                (s, e) => RaisePropertyChanged(nameof(TestLevelSets)),
                nameof(TestLevelSetInterfaceAdapter.TestLevelSets));
            PropertyChangedEventManager.AddHandler(
                _testLevelSetInterface,
                (s, e) =>
                {
                    RaisePropertyChanged(nameof(SelectedTestLevelSet));
                },
                nameof(TestLevelSetInterfaceAdapter.SelectedTestLevelSet));
            PropertyChangedEventManager.AddHandler(
                _testLevelSetInterface,
                (s, e) =>
                {
                    RaisePropertyChanged(nameof(TestLevelSetWithoutChanges));
                },
                nameof(TestLevelSetInterfaceAdapter.TestLevelSetWithoutChanges));
        }

        ObservableCollection<WorkingCalendarEntryModel> previousWorkingCalendarEntries;
        private void WireViewModelToWorkingCalendarInterface()
        {
            PropertyChangedEventManager.AddHandler(
                _workingCalendarInterface,
                WorkingCalendarEntries_Changed,
                nameof(WorkingCalendarInterfaceAdapter.WorkingCalendarEntries));
            PropertyChangedEventManager.AddHandler(
                _workingCalendarInterface,
                (s, e) => RaisePropertyChanged(nameof(SelectedEntryInList)),
                nameof(WorkingCalendarInterfaceAdapter.SelectedEntryInList));
            PropertyChangedEventManager.AddHandler(
                _workingCalendarInterface,
                (s, e) =>
                {
                    if (SelectedEntryInList != null)
                    {
                        WorkingCalendarEntrySelectionRequest?.Invoke(this, SelectedEntryInList); 
                    }
                },
                nameof(WorkingCalendarInterfaceAdapter.SelectedEntryInList));
            PropertyChangedEventManager.AddHandler(
                _workingCalendarInterface,
                (s, e) => RaisePropertyChanged(nameof(SelectedDateInCalendar)),
                nameof(WorkingCalendarInterfaceAdapter.SelectedDateInCalendar));
            PropertyChangedEventManager.AddHandler(
                _workingCalendarInterface,
                (s, e) => RaisePropertyChanged(nameof(AreSaturdaysFree)),
                nameof(WorkingCalendarInterfaceAdapter.AreSaturdaysFree));
            PropertyChangedEventManager.AddHandler(
                _workingCalendarInterface,
                (s, e) => RaisePropertyChanged(nameof(AreSundaysFree)),
                nameof(WorkingCalendarInterfaceAdapter.AreSundaysFree));
        }

        private void WorkingCalendarEntries_Changed(object sender, PropertyChangedEventArgs e)
        {
            RaisePropertyChanged(nameof(WorkingCalendarEntries));
            SingleWorkingCalendarEntries.SetNewSourceCollection(_workingCalendarInterface.WorkingCalendarEntries);
            YearlyWorkingCalendarEntries.SetNewSourceCollection(_workingCalendarInterface.WorkingCalendarEntries);
            _workingCalendarInterface.WorkingCalendarEntries.CollectionChanged += WorkingCalendarEntries_CollectionChanged;
            if (previousWorkingCalendarEntries != null) { previousWorkingCalendarEntries.CollectionChanged -= WorkingCalendarEntries_CollectionChanged; }
            previousWorkingCalendarEntries = _workingCalendarInterface.WorkingCalendarEntries;
        }

        private void WorkingCalendarEntries_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                SingleWorkingCalendarEntries.RefilterCollection();
                YearlyWorkingCalendarEntries.RefilterCollection();
            }
        }

        public void ShowTestLevelSetError()
        {
            var args = new MessageBoxEventArgs(r => { },
                _localization.Strings.GetParticularString("Test level sets", "Some errors occurred for the test level sets"),
                _localization.Strings.GetParticularString("Test level sets", "Warning"),
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
            MessageBoxRequest?.Invoke(this, args);
        }

        private void WireViewModelToShiftManagementInterface()
        {
            PropertyChangedEventManager.AddHandler(
                _shiftManagementInterface,
                (s, e) => RaisePropertyChanged(nameof(CurrentShiftManagement)),
                nameof(ShiftManagementInterfaceAdapter.CurrentShiftManagement));
            PropertyChangedEventManager.AddHandler(
                _shiftManagementInterface,
                (s, e) =>
                {
                    _isShiftManagementCurrentlySaving = false;
                    RaisePropertyChanged(nameof(ShiftManagementWithoutChanges));
                },
                nameof(ShiftManagementInterfaceAdapter.ShiftManagementWithoutChanges));
        }

        public void ShiftManagementError()
        {
            var args = new MessageBoxEventArgs(r => { },
                _localization.Strings.GetParticularString("Shift management", "Some errors occurred for the shift management"),
                _localization.Strings.GetParticularString("Shift management message box caption", "Warning"),
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
            MessageBoxRequest?.Invoke(this, args);
        }
        
        public void WorkingCalendarError(WorkingCalendarEntry problematicEntry)
        {
            var args = new MessageBoxEventArgs(r => { },
                _localization.Strings.GetParticularString("Working calendar", "Some errors occurred for the working calendar"),
                _localization.Strings.GetParticularString("Working calendar message box caption", "Warning"),
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
            MessageBoxRequest?.Invoke(this, args);
        }


        private bool LoadedCanExecute(object arg) { return true; }
        private void LoadedExecute(object obj)
        {
            _testLevelSetInterface.SelectedTestLevelSet = null;
            _startUp.RaiseShowLoadingControl(true, 2);
            _testLevelSetUseCase.LoadTestLevelSets(this);
            _shiftManagementUseCase.LoadShiftManagement(this);
            _workingCalendarInterface.SelectedDateInCalendar = DateTime.Today;
        }

        private bool _isWorkingCalendarLoadedYet = false;
        public void LoadWorkingCalendarIfNotLoadedYet()
        {
            if(_isWorkingCalendarLoadedYet) { return; }

            _startUp.RaiseShowLoadingControl(true);
            _workingCalendarUseCase.LoadWeekendSettings(this);
            _workingCalendarUseCase.LoadCalendarEntries(this);
            _isWorkingCalendarLoadedYet = true;
        }

        private bool AddTestLevelSetCanExecute(object arg) { return true; }
        private void AddTestLevelSetExecute(object obj)
        {
            if (!CanClose())
            {
                return;
            }

            // Get unique name as default
            int counter = 0;
            string name;
            while (true)
            {
                name = $"{_localization.Strings.GetParticularString("TestLevelSet", "Test level set")} {TestLevelSets.Count + counter + 1}";
                if (TestLevelSets.FirstOrDefault(x => x.Name == name) == null)
                {
                    // Leave loop if Value is unique
                    break;
                }
                counter++;
            }

            _testLevelSetUseCase.AddTestLevelSet(new TestLevelSet()
            {
                Id = new TestLevelSetId(0),
                Name = new TestLevelSetName(name),
                TestLevel1 = new TestLevel()
                {
                    Id = new TestLevelId(0),
                    TestInterval = new Interval()
                    {
                        IntervalValue = 1,
                        Type = IntervalType.XTimesADay
                    },
                    SampleNumber = 1,
                    ConsiderWorkingCalendar = true,
                    IsActive = true
                },
                TestLevel2 = new TestLevel()
                {
                    Id = new TestLevelId(0),
                    TestInterval = new Interval()
                    {
                        IntervalValue = 1,
                        Type = IntervalType.XTimesADay
                    },
                    SampleNumber = 1,
                    ConsiderWorkingCalendar = true,
                    IsActive = false
                },
                TestLevel3 = new TestLevel()
                {
                    Id = new TestLevelId(0),
                    TestInterval = new Interval()
                    {
                        IntervalValue = 1,
                        Type = IntervalType.XTimesADay
                    },
                    SampleNumber = 1,
                    ConsiderWorkingCalendar = true,
                    IsActive = false
                }
            }, this);
        }

        private bool RemoveTestLevelSetCanExecute(object arg) { return SelectedTestLevelSet != null; }
        private void RemoveTestLevelSetExecute(object obj)
        {
            if (_testLevelSetUseCase.DoesTestLevelSetHaveReferences(SelectedTestLevelSet.Entity))
            {
                var referencesArgs = new MessageBoxEventArgs(r => { },
                    _localization.Strings.GetParticularString("TestLevelSet", "The test level set cannot be deleted. It has still references."),
                    _localization.Strings.GetParticularString("TestLevelSet", "Warning"),
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                MessageBoxRequest?.Invoke(this, referencesArgs);
                return;
            } 

            var args = new MessageBoxEventArgs(r => { if (r == MessageBoxResult.Yes) _testLevelSetUseCase.RemoveTestLevelSet(SelectedTestLevelSet?.Entity, this); },
                _localization.Strings.GetParticularString("TestLevelSet", "Do you really want to remove the selected test level set?"),
                _localization.Strings.GetParticularString("TestLevelSet", "Warning"),
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);
            MessageBoxRequest?.Invoke(this, args);
        }

        private bool SaveTestLevelSetCanExecute(object arg) { return HasTestLevelSetChanged(); }
        public void SaveTestLevelSetExecute(object obj)
        {
            if (!SelectedTestLevelSet.Entity.Name.Equals(TestLevelSetWithoutChanges.Entity.Name))
            {
                if (!_testLevelSetUseCase.IsTestLevelSetNameUnique(SelectedTestLevelSet.Name))
                {
                    var args = new MessageBoxEventArgs(r => { },
                        _localization.Strings.GetParticularString("TestLevelSet", "You cannot save the test level set - the name is not unique."),
                        _localization.Strings.GetParticularString("TestLevelSet", "Warning"),
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                    MessageBoxRequest?.Invoke(this, args);
                    return;
                }
            }

            _testLevelSetUseCase.UpdateTestLevelSet(new TestLevelSetDiff() { New = SelectedTestLevelSet.Entity, Old = TestLevelSetWithoutChanges.Entity }, this, this, this);
        }

        private bool AddWorkingCalendarEntryCanExecute(object arg) { return true; }
        private void AddWorkingCalendarEntryExecute(object obj)
        {
            WorkingCalendarEntryType suggestedType;
            if(AreSaturdaysFree && SelectedDateInCalendar.ToDateTime().DayOfWeek == DayOfWeek.Saturday || AreSundaysFree && SelectedDateInCalendar.ToDateTime().DayOfWeek == DayOfWeek.Sunday)
            {
                suggestedType = WorkingCalendarEntryType.ExtraShift;
            }
            else
            {
                suggestedType = WorkingCalendarEntryType.Holiday;
            }

            AssistentView assistant = _startUp.OpenAddWorkingCalendarEntryAssistant(AreSaturdaysFree, 
                AreSundaysFree, 
                date => WorkingCalendarEntries.FirstOrDefault(x => x.Date.Date == date.Date)?.Entity,
                SelectedDateInCalendar, 
                suggestedType);

            assistant.EndOfAssistent += (s, e) =>
            {
                var entry = (WorkingCalendarEntry)(assistant.DataContext as AssistentViewModel).FillResultObject(new WorkingCalendarEntry());
                entry.Date = entry.Date.Date;

                var preexisting = WorkingCalendarEntries.FirstOrDefault(x => x.Date.Date == entry.Date.Date);
                _workingCalendarUseCase.AddWorkingCalendarEntry(entry, this, this, preexisting?.Entity);
            };
            assistant.Closed += (s, e) =>
            {
                _startUp.RaiseShowLoadingControl(false);
            };

            _startUp.RaiseShowLoadingControl(true);
            ShowDialogRequest?.Invoke(this, assistant);
        }

        private bool RemoveWorkingCalendarEntryCanExecute(object arg)
        {
            return SelectedEntryInList != null;
        }
        private void RemoveWorkingCalendarEntryExecute(object obj)
        {
            Action<MessageBoxResult> resultAction = r =>
            {
                if (r == MessageBoxResult.Yes)
                {
                    _workingCalendarUseCase.RemoveWorkingCalendarEntry(SelectedEntryInList.Entity, this, this);
                }
            };

            var args = new MessageBoxEventArgs(resultAction,
                SelectedEntryInList.Repetition == WorkingCalendarEntryRepetition.Yearly ?
                    _localization.Strings.GetParticularString("Working calendar", "Do you really want to delete the calendar entry? The repeated entries will also be deleted") :
                    _localization.Strings.GetParticularString("Working calendar", "Do you really want to delete the calendar entry?"),
                _localization.Strings.GetParticularString("Working calendar", "Warning"),
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);
            MessageBoxRequest?.Invoke(this, args);
        }

        private bool SaveShiftManagementCanExecute(object arg)
        {
            return !_isShiftManagementCurrentlySaving && 
                (!CurrentShiftManagement?.EqualsByContent(_shiftManagementInterface.ShiftManagementWithoutChanges?.Entity) ?? false) &&
                CurrentShiftManagement?.Entity.Validate().Any() == false;
        }
        private void SaveShiftManagementExecute(object obj)
        {
            _shiftManagementUseCase.SaveShiftManagement(new ShiftManagementDiff(_shiftManagementInterface.ShiftManagementWithoutChanges.Entity, _shiftManagementInterface.CurrentShiftManagement.Entity), this, this, this);
            _isShiftManagementCurrentlySaving = true;
        }


        public bool CanClose()
        {
            if (CurrentShiftManagement?.Entity.Validate().Any() == true)
            {
                // If there are invalid inputs
                var args = new MessageBoxEventArgs(r => { },
                    _localization.Strings.GetParticularString("Shift management", "Your inputs are invalid. You have to enter valid values to leave this view."),
                    _localization.Strings.GetParticularString("Shift management", "Error"),
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);

                MessageBoxRequest?.Invoke(this, args);
                return false;
            }

            if (!(_shiftManagementInterface.CurrentShiftManagement?.EqualsByContent(_shiftManagementInterface.ShiftManagementWithoutChanges.Entity) ?? _shiftManagementInterface.ShiftManagementWithoutChanges == null))
            {
                MessageBoxResult shiftManagementResult = MessageBoxResult.None;

                var shiftManagementArgs = new MessageBoxEventArgs(r => shiftManagementResult = r,
                    _localization.Strings.GetParticularString("ShiftManagement", "There are unsaved changes. Do you want to save them?"),
                    _localization.Strings.GetParticularString("ShiftManagement", "Warning"),
                    MessageBoxButton.YesNoCancel,
                    MessageBoxImage.Warning);
                MessageBoxRequest?.Invoke(this, shiftManagementArgs);

                if (shiftManagementResult == MessageBoxResult.Yes)
                {
                    SaveShiftManagementCommand.Invoke(null);
                    return true;
                }
                else if (shiftManagementResult == MessageBoxResult.No)
                {
                    ResetShiftManagement();
                    return true;
                }
                else
                {
                    return false;
                }
            }

            if (!(SelectedTestLevelSet?.Entity.EqualsByContent(TestLevelSetWithoutChanges.Entity) ?? TestLevelSetWithoutChanges == null))
            {
                MessageBoxResult testLevelSetResult = MessageBoxResult.None;

                var testLevelSetArgs = Task.Run(() => new MessageBoxEventArgs(r => testLevelSetResult = r,
                    _localization.Strings.GetParticularString("TestLevelSetView", "There are unsaved changes. Do you want to save them?"),
                    _localization.Strings.GetParticularString("TestLevelSetView", "Warning"),
                    MessageBoxButton.YesNoCancel,
                    MessageBoxImage.Warning)).Result;
                MessageBoxRequest?.Invoke(this, testLevelSetArgs);

                if(testLevelSetResult == MessageBoxResult.Yes)
                {
                    SaveTestLevelSetCommand.Invoke(null);
                    return true;
                }
                else if(testLevelSetResult == MessageBoxResult.No)
                {
                    ResetTestLevelSet();
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        public void LanguageUpdate()
        {
            WorkingCalendarEntries.ForEach(x => x.RaisePropertyChanged());
            RaisePropertyChanged(nameof(CurrentShiftManagement));
        }

        public void ShowDiffDialog(ShiftManagementDiff diff, Action saveAction)
        {
            var changes = GetChangesFromShiftManagementDiff(diff).ToList();

            if (changes.Count == 0)
            {
                return;
            }

            var args = new VerifyChangesEventArgs(changes);
            RequestVerifyChangesView?.Invoke(this, args);
            diff.Comment = new HistoryComment(args.Comment);

            _guiDispatcher.Invoke(() =>
            {
                if (args.Result == MessageBoxResult.No)
                {
                    _shiftManagementInterface.CurrentShiftManagement.UpdateWith(_shiftManagementInterface.ShiftManagementWithoutChanges?.Entity);
                    _startUp.RaiseShowLoadingControl(false);
                }

                if (args.Result == MessageBoxResult.Yes)
                {
                    saveAction();
                }

                if (args.Result == MessageBoxResult.Cancel)
                {
                    _startUp.RaiseShowLoadingControl(false);
                }
            });
        }

        private IEnumerable<SingleValueChangeModel> GetChangesFromShiftManagementDiff(ShiftManagementDiff diff)
        {
            var entity = _localization.Strings.GetParticularString("Shift management", "Shift management");

            if (diff.Old.FirstShiftStart != diff.New.FirstShiftStart)
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("Shift management diff", "First shift start"),
                    OldValue = diff.Old.FirstShiftStart.ToString(),
                    NewValue = diff.New.FirstShiftStart.ToString()
                };
            }

            if (diff.Old.FirstShiftEnd != diff.New.FirstShiftEnd)
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("Shift management diff", "First shift end"),
                    OldValue = diff.Old.FirstShiftEnd.ToString(),
                    NewValue = diff.New.FirstShiftEnd.ToString()
                };
            }

            if (diff.Old.SecondShiftStart != diff.New.SecondShiftStart)
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("Shift management diff", "Second shift start"),
                    OldValue = diff.Old.SecondShiftStart.ToString(),
                    NewValue = diff.New.SecondShiftStart.ToString()
                };
            }

            if (diff.Old.SecondShiftEnd != diff.New.SecondShiftEnd)
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("Shift management diff", "Second shift end"),
                    OldValue = diff.Old.SecondShiftEnd.ToString(),
                    NewValue = diff.New.SecondShiftEnd.ToString()
                };
            }

            if (diff.Old.ThirdShiftStart != diff.New.ThirdShiftStart)
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("Shift management diff", "Third shift start"),
                    OldValue = diff.Old.ThirdShiftStart.ToString(),
                    NewValue = diff.New.ThirdShiftStart.ToString()
                };
            }

            if (diff.Old.ThirdShiftEnd != diff.New.ThirdShiftEnd)
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("Shift management diff", "Third shift end"),
                    OldValue = diff.Old.ThirdShiftEnd.ToString(),
                    NewValue = diff.New.ThirdShiftEnd.ToString()
                };
            }

            if (diff.Old.IsSecondShiftActive != diff.New.IsSecondShiftActive)
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("Shift management diff", "Is second shift active"),
                    OldValue = diff.Old.IsSecondShiftActive ? "✓" : "✗",
                    NewValue = diff.New.IsSecondShiftActive ? "✓" : "✗"
                };
            }

            if (diff.Old.IsThirdShiftActive != diff.New.IsThirdShiftActive)
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("Shift management diff", "Is third shift active"),
                    OldValue = diff.Old.IsThirdShiftActive ? "✓" : "✗",
                    NewValue = diff.New.IsThirdShiftActive ? "✓" : "✗"
                };
            }

            if (diff.Old.ChangeOfDay != diff.New.ChangeOfDay)
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("Shift management diff", "Change of day"),
                    OldValue = diff.Old.ChangeOfDay.ToString(),
                    NewValue = diff.New.ChangeOfDay.ToString()
                };
            }

            if (diff.Old.FirstDayOfWeek != diff.New.FirstDayOfWeek)
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("Shift management diff", "First day of week"),
                    OldValue = _localization.Strings.GetParticularString("Shift management diff", diff.Old.FirstDayOfWeek.ToString()),
                    NewValue = _localization.Strings.GetParticularString("Shift management diff", diff.New.FirstDayOfWeek.ToString())
                };
            }
        }

        public void ShowDiffDialog(TestLevelSetDiff diff, Action saveAction)
        {
            var changes = GetChangesFromTestLevelSetDiff(diff).ToList();

            if (changes.Count == 0)
            {
                return;
            }

            var args = new VerifyChangesEventArgs(changes);
            RequestVerifyChangesView?.Invoke(this, args);
            diff.Comment = new HistoryComment(args.Comment);

            _guiDispatcher.Invoke(() =>
            {
                if (args.Result == MessageBoxResult.No)
                {
                    var set = TestLevelSets.FirstOrDefault(x => x.Entity.EqualsById(diff.New));
                    if(set != null)
                    {
                        set.UpdateWith(diff.Old);
                    }
                    _startUp.RaiseShowLoadingControl(false);
                }

                if (args.Result == MessageBoxResult.Yes)
                {
                    saveAction();
                }

                if (args.Result == MessageBoxResult.Cancel)
                {
                    _startUp.RaiseShowLoadingControl(false);
                }
            });
        }

        private IEnumerable<SingleValueChangeModel> GetChangesFromTestLevelSetDiff(TestLevelSetDiff diff)
        {
            var testLevelSet = diff.New.Name.ToDefaultString();
            var testLevel1 = diff.New.Name.ToDefaultString() + " - " + _localization.Strings.GetParticularString("Test level set diff", "Test Level 1");
            var testLevel2 = diff.New.Name.ToDefaultString() + " - " + _localization.Strings.GetParticularString("Test level set diff", "Test Level 2");
            var testLevel3 = diff.New.Name.ToDefaultString() + " - " + _localization.Strings.GetParticularString("Test level set diff", "Test Level 3");

            if (diff.Old.Name.ToDefaultString() != diff.New.Name.ToDefaultString())
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = testLevelSet,
                    ChangedAttribute = _localization.Strings.GetParticularString("Test level set diff", "Name"),
                    OldValue = diff.Old.Name.ToDefaultString(),
                    NewValue = diff.New.Name.ToDefaultString()
                };
            }

            if (diff.Old.TestLevel1.TestInterval.IntervalValue != diff.New.TestLevel1.TestInterval.IntervalValue)
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = testLevel1,
                    ChangedAttribute = _localization.Strings.GetParticularString("Test level set diff", "Interval value"),
                    OldValue = diff.Old.TestLevel1.TestInterval.IntervalValue.ToString(),
                    NewValue = diff.New.TestLevel1.TestInterval.IntervalValue.ToString()
                };
            }

            if (diff.Old.TestLevel1.TestInterval.Type != diff.New.TestLevel1.TestInterval.Type)
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = testLevel1,
                    ChangedAttribute = _localization.Strings.GetParticularString("Test level set diff", "Interval type"),
                    OldValue = IntervalModel.GetIntervalTypeTranslation(diff.Old.TestLevel1.TestInterval.Type, _localization),
                    NewValue = IntervalModel.GetIntervalTypeTranslation(diff.New.TestLevel1.TestInterval.Type, _localization)
                };
            }

            if (diff.Old.TestLevel1.SampleNumber != diff.New.TestLevel1.SampleNumber)
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = testLevel1,
                    ChangedAttribute = _localization.Strings.GetParticularString("Test level set diff", "Sample number"),
                    OldValue = diff.Old.TestLevel1.SampleNumber.ToString(),
                    NewValue = diff.New.TestLevel1.SampleNumber.ToString()
                };
            }

            if (diff.Old.TestLevel1.ConsiderWorkingCalendar != diff.New.TestLevel1.ConsiderWorkingCalendar)
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = testLevel1,
                    ChangedAttribute = _localization.Strings.GetParticularString("Test level set diff", "Consider working calendar"),
                    OldValue = diff.Old.TestLevel1.ConsiderWorkingCalendar ? "✓" : "✗",
                    NewValue = diff.New.TestLevel1.ConsiderWorkingCalendar ? "✓" : "✗"
                };
            }

            if (diff.Old.TestLevel2.TestInterval.IntervalValue != diff.New.TestLevel2.TestInterval.IntervalValue)
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = testLevel2,
                    ChangedAttribute = _localization.Strings.GetParticularString("Test level set diff", "Interval value"),
                    OldValue = diff.Old.TestLevel2.TestInterval.IntervalValue.ToString(),
                    NewValue = diff.New.TestLevel2.TestInterval.IntervalValue.ToString()
                };
            }

            if (diff.Old.TestLevel2.TestInterval.Type != diff.New.TestLevel2.TestInterval.Type)
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = testLevel2,
                    ChangedAttribute = _localization.Strings.GetParticularString("Test level set diff", "Interval type"),
                    OldValue = IntervalModel.GetIntervalTypeTranslation(diff.Old.TestLevel2.TestInterval.Type, _localization),
                    NewValue = IntervalModel.GetIntervalTypeTranslation(diff.New.TestLevel2.TestInterval.Type, _localization)
                };
            }

            if (diff.Old.TestLevel2.SampleNumber != diff.New.TestLevel2.SampleNumber)
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = testLevel2,
                    ChangedAttribute = _localization.Strings.GetParticularString("Test level set diff", "Sample number"),
                    OldValue = diff.Old.TestLevel2.SampleNumber.ToString(),
                    NewValue = diff.New.TestLevel2.SampleNumber.ToString()
                };
            }

            if (diff.Old.TestLevel2.ConsiderWorkingCalendar != diff.New.TestLevel2.ConsiderWorkingCalendar)
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = testLevel2,
                    ChangedAttribute = _localization.Strings.GetParticularString("Test level set diff", "Consider working calendar"),
                    OldValue = diff.Old.TestLevel2.ConsiderWorkingCalendar ? "✓" : "✗",
                    NewValue = diff.New.TestLevel2.ConsiderWorkingCalendar ? "✓" : "✗"
                };
            }

            if (diff.Old.TestLevel2.IsActive != diff.New.TestLevel2.IsActive)
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = testLevel2,
                    ChangedAttribute = _localization.Strings.GetParticularString("Test level set diff", "Is active"),
                    OldValue = diff.Old.TestLevel2.IsActive ? "✓" : "✗",
                    NewValue = diff.New.TestLevel2.IsActive ? "✓" : "✗"
                };
            }

            if (diff.Old.TestLevel3.TestInterval.IntervalValue != diff.New.TestLevel3.TestInterval.IntervalValue)
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = testLevel3,
                    ChangedAttribute = _localization.Strings.GetParticularString("Test level set diff", "Interval value"),
                    OldValue = diff.Old.TestLevel3.TestInterval.IntervalValue.ToString(),
                    NewValue = diff.New.TestLevel3.TestInterval.IntervalValue.ToString()
                };
            }

            if (diff.Old.TestLevel3.TestInterval.Type != diff.New.TestLevel3.TestInterval.Type)
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = testLevel3,
                    ChangedAttribute = _localization.Strings.GetParticularString("Test level set diff", "Interval type"),
                    OldValue = IntervalModel.GetIntervalTypeTranslation(diff.Old.TestLevel3.TestInterval.Type, _localization),
                    NewValue = IntervalModel.GetIntervalTypeTranslation(diff.New.TestLevel3.TestInterval.Type, _localization)
                };
            }

            if (diff.Old.TestLevel3.SampleNumber != diff.New.TestLevel3.SampleNumber)
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = testLevel3,
                    ChangedAttribute = _localization.Strings.GetParticularString("Test level set diff", "Sample number"),
                    OldValue = diff.Old.TestLevel3.SampleNumber.ToString(),
                    NewValue = diff.New.TestLevel3.SampleNumber.ToString()
                };
            }

            if (diff.Old.TestLevel3.ConsiderWorkingCalendar != diff.New.TestLevel3.ConsiderWorkingCalendar)
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = testLevel3,
                    ChangedAttribute = _localization.Strings.GetParticularString("Test level set diff", "Consider working calendar"),
                    OldValue = diff.Old.TestLevel3.ConsiderWorkingCalendar ? "✓" : "✗",
                    NewValue = diff.New.TestLevel3.ConsiderWorkingCalendar ? "✓" : "✗"
                };
            }

            if (diff.Old.TestLevel3.IsActive != diff.New.TestLevel3.IsActive)
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = testLevel3,
                    ChangedAttribute = _localization.Strings.GetParticularString("Test level set diff", "Is active"),
                    OldValue = diff.Old.TestLevel3.IsActive ? "✓" : "✗",
                    NewValue = diff.New.TestLevel3.IsActive ? "✓" : "✗"
                };
            }
        }

        public void Dispose()
        {
            _workingCalendarInterface.WorkingCalendarEntries.CollectionChanged -= WorkingCalendarEntries_CollectionChanged;
            _testLevelSetInterface.ShowLoadingControlRequest -= InterfaceAdapter_ShowLoadingControlRequest;
            _workingCalendarInterface.ShowLoadingControlRequest -= InterfaceAdapter_ShowLoadingControlRequest;
            _shiftManagementInterface.ShowLoadingControlRequest -= InterfaceAdapter_ShowLoadingControlRequest;
            SingleWorkingCalendarEntries.Dispose();
            YearlyWorkingCalendarEntries.Dispose();
            PropertyChangedEventManager.RemoveHandler(_workingCalendarInterface, WorkingCalendarEntries_Changed, nameof(WorkingCalendarInterfaceAdapter.WorkingCalendarEntries));
        }

        public void ShowProblemLoadingLocationProcessControlCondition()
        {
            var args = new MessageBoxEventArgs(r => { },
                _localization.Strings.GetParticularString("Test level set", "Some errors occurred for process control conditions"),
                _localization.Strings.GetParticularString("Test level set message box caption", "Warning"),
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
            MessageBoxRequest?.Invoke(this, args);
        }

        public void ShowProblemRemoveProcessControlCondition()
        {
            throw new NotImplementedException();
        }

        public void ShowProblemSavingProcessControlCondition()
        {
            throw new NotImplementedException();
        }
    }
}
