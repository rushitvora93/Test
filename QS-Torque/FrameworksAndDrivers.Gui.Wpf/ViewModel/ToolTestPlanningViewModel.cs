using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Threading;
using Client.UseCases.UseCases;
using Core;
using Core.Diffs;
using Core.Entities;
using Core.Enums;
using Core.UseCases;
using FrameworksAndDrivers.Gui.Wpf.EventArgs;
using FrameworksAndDrivers.Gui.Wpf.Interfaces;
using FrameworksAndDrivers.Gui.Wpf.Model;
using InterfaceAdapters;
using InterfaceAdapters.Localization;
using InterfaceAdapters.Models;

namespace FrameworksAndDrivers.Gui.Wpf.ViewModel
{
    public class ToolTestPlanningViewModel : BindableBase, ITestLevelSetErrorHandler, ITestLevelSetAssignmentErrorHandler, IDisposable, ICanClose, ILocationToolAssignmentDiffShower, ILocationToolAssignmentErrorHandler, IShiftManagementErrorHandler
    {
        private ILocalizationWrapper _localization;
        private ITestLevelSetUseCase _testLevelSetUseCase;
        private ITestLevelSetInterface _testLevelSetInterface;
        private ITestLevelSetAssignmentUseCase _testLevelSetAssignmentUseCase;
        private ITestLevelSetAssignmentInterface _testLevelSetAssignmentInterface;
        private ILocationToolAssignmentUseCase _locationToolAssignmentUseCase;
        private ILocationToolAssignmentInterface _locationToolAssignmentInterface;
        private IShiftManagementUseCase _shiftManagementUseCase;
        private IShiftManagementInterface _shiftManagementInterface;
        private ILocationToolAssignmentDisplayFormatter _locationToolAssignmentDisplayFormatter;
        private IStartUp _startUp;

        private Dispatcher _guiDispatcher;

        public ShiftManagementModel CurrentShiftManagement => _shiftManagementInterface.CurrentShiftManagement;

        public ObservableCollection<TestLevelSetModel> TestLevelSets
        {
            get => _testLevelSetInterface.TestLevelSets;
        }

        public ObservableCollection<LocationToolAssignmentModelWithTestType> LocationToolAssignmentsForTestLevelAssignment
        {
            get => _testLevelSetAssignmentInterface.LocationToolAssignments;
        }

        /// <summary>
        /// LocationToolAssignmentModelWithTestType - original test level number
        /// </summary>
        public Dictionary<LocationToolAssignmentModelWithTestType, int> ChangedTestLevelNumbersAssignments { get; private set; } = new Dictionary<LocationToolAssignmentModelWithTestType, int>();

        public ObservableCollection<LocationToolAssignmentModelWithTestType> SelectedLocationToolAssignmentsForTestLevelAssignment
        {
            get => _testLevelSetAssignmentInterface.SelectedLocationToolAssignments;
            set
            {
                _testLevelSetAssignmentInterface.SelectedLocationToolAssignments = value;
                RaisePropertyChanged();
            }
        }

        public TestLevelSetModel SelectedTestLevelSet
        {
            get => _testLevelSetAssignmentInterface.SelectedTestLevelSet;
            set
            {
                _testLevelSetAssignmentInterface.SelectedTestLevelSet = value;
                RaisePropertyChanged();
            }
        }

        private FilteredObservableCollectionExtension<LocationToolAssignmentModel> _mcaLocationToolAssignmentsForOverview;
        public FilteredObservableCollectionExtension<LocationToolAssignmentModel> McaLocationToolAssignmentsForOverview
        {
            get => _mcaLocationToolAssignmentsForOverview;
            private set
            {
                _mcaLocationToolAssignmentsForOverview = value;
                RaisePropertyChanged();
            }
        }

        private FilteredObservableCollectionExtension<LocationToolAssignmentModel> _chkLocationToolAssignmentsForOverview;
        public FilteredObservableCollectionExtension<LocationToolAssignmentModel> ChkLocationToolAssignmentsForOverview
        {
            get => _chkLocationToolAssignmentsForOverview;
            private set
            {
                _chkLocationToolAssignmentsForOverview = value;
                RaisePropertyChanged();
            }
        }


        public Func<long, object, object, bool> CompareIfTestLevelNumberHasChanged { get; private set; }


        public RelayCommand LoadedCommand { get; private set; }
        public RelayCommand AssignTestLevelSetCommand { get; private set; }
        public RelayCommand ResetTestLevelSetSelectionCommand { get; private set; }
        public RelayCommand RemoveTestLevelSetAssignmentsCommand { get; private set; }
        public RelayCommand SaveTestLevelNumbersCommand { get; private set; }


        public event EventHandler<MessageBoxEventArgs> MessageBoxRequest;
        public event EventHandler<VerifyChangesEventArgs> RequestVerifyChangesView;

        public ToolTestPlanningViewModel(ITestLevelSetAssignmentUseCase testLevelSetAssignmentUseCase,
            ITestLevelSetAssignmentInterface testLevelSetAssignmentInterface,
            ITestLevelSetUseCase testLevelSetUseCase,
            ITestLevelSetInterface testLevelSetInterface,
            ILocationToolAssignmentUseCase locationToolAssignmentUseCase,
            ILocationToolAssignmentInterface locationToolAssignmentInterface,
            ILocalizationWrapper localization,
            ILocationToolAssignmentDisplayFormatter locationToolAssignmentDisplayFormatter,
            IShiftManagementUseCase shiftManagementUseCase,
            IShiftManagementInterface shiftManagementInterface,
            IStartUp startUp)
        {
            _testLevelSetAssignmentUseCase = testLevelSetAssignmentUseCase;
            _testLevelSetAssignmentInterface = testLevelSetAssignmentInterface;
            _testLevelSetUseCase = testLevelSetUseCase;
            _testLevelSetInterface = testLevelSetInterface;
            _locationToolAssignmentUseCase = locationToolAssignmentUseCase;
            _locationToolAssignmentInterface = locationToolAssignmentInterface;
            _shiftManagementUseCase = shiftManagementUseCase;
            _shiftManagementInterface = shiftManagementInterface;
            _localization = localization;
            _startUp = startUp;
            _locationToolAssignmentDisplayFormatter = locationToolAssignmentDisplayFormatter;
            McaLocationToolAssignmentsForOverview = new FilteredObservableCollectionExtension<LocationToolAssignmentModel>(_locationToolAssignmentInterface.LocationToolAssignments);
            McaLocationToolAssignmentsForOverview.Filter = x => x.TestLevelSetMfu != null && x.TestOperationActiveMfu && x.TestParameters != null && x.TestTechnique != null;
            ChkLocationToolAssignmentsForOverview = new FilteredObservableCollectionExtension<LocationToolAssignmentModel>(_locationToolAssignmentInterface.LocationToolAssignments);
            ChkLocationToolAssignmentsForOverview.Filter = x => x.TestLevelSetChk != null && x.TestOperationActiveChk && x.TestParameters != null && x.TestTechnique != null;

            LoadedCommand = new RelayCommand(LoadedExecute, LoadedCanExecute);
            AssignTestLevelSetCommand = new RelayCommand(AssignTestLevelSetExecute, AssignTestLevelSetCanExecute);
            ResetTestLevelSetSelectionCommand = new RelayCommand(ResetTestLevelSetSelectionExecute, ResetTestLevelSetSelectionCanExecute);
            RemoveTestLevelSetAssignmentsCommand = new RelayCommand(RemoveTestLevelSetAssignmentsExecute, RemoveTestLevelSetAssignmentsCanExecute);
            SaveTestLevelNumbersCommand = new RelayCommand(SaveTestLevelNumbersExecute, SaveTestLevelNumbersCanExecute);

            WireViewModelToInterfaceAdapters();

            CompareIfTestLevelNumberHasChanged = (id, val, param) =>
            {
                return ChangedTestLevelNumbersAssignments.Keys.Any(x => x.Id == id && x.TestType == (TestType)param);
            };
        }

        public void SetGuiDispatcher(Dispatcher dispatcher)
        {
            _guiDispatcher = dispatcher;
        }

        private void WireViewModelToInterfaceAdapters()
        {
            PropertyChangedEventManager.AddHandler(
                _testLevelSetInterface,
                (s, e) => 
                {
                    RaisePropertyChanged(nameof(TestLevelSets));
                    _startUp.RaiseShowLoadingControl(false);
                },
                nameof(ToolTestPlanningViewModel.TestLevelSets));
            PropertyChangedEventManager.AddHandler(
                _testLevelSetAssignmentInterface,
                (s, e) => RaisePropertyChanged(nameof(SelectedTestLevelSet)),
                nameof(ToolTestPlanningViewModel.SelectedTestLevelSet));
            PropertyChangedEventManager.AddHandler(
                _testLevelSetAssignmentInterface,
                (s, e) => RaisePropertyChanged(nameof(SelectedLocationToolAssignmentsForTestLevelAssignment)),
                nameof(ToolTestPlanningViewModel.SelectedLocationToolAssignmentsForTestLevelAssignment));
            PropertyChangedEventManager.AddHandler(
                _testLevelSetAssignmentInterface,
                (s, e) =>
                {
                    RaisePropertyChanged(nameof(LocationToolAssignmentsForTestLevelAssignment));
                    _startUp.RaiseShowLoadingControl(false);
                },
                nameof(ITestLevelSetAssignmentInterface.LocationToolAssignments));
            PropertyChangedEventManager.AddHandler(
                _locationToolAssignmentInterface,
                (s, e) =>
                {
                    McaLocationToolAssignmentsForOverview.SetNewSourceCollection(_locationToolAssignmentInterface.LocationToolAssignments);
                    ChkLocationToolAssignmentsForOverview.SetNewSourceCollection(_locationToolAssignmentInterface.LocationToolAssignments);
                    _startUp.RaiseShowLoadingControl(false);
                },
                nameof(ILocationToolAssignmentInterface.LocationToolAssignments));
            PropertyChangedEventManager.AddHandler(
                _shiftManagementInterface,
                (s, e) =>
                {
                    RaisePropertyChanged(nameof(CurrentShiftManagement));
                    _startUp.RaiseShowLoadingControl(false);
                },
                nameof(IShiftManagementInterface.CurrentShiftManagement));

            _locationToolAssignmentInterface.LocationToolAssignmentErrorRequest += InterfaceAdapter_LocationToolAssignmentErrorRequest;
        }

        private void InterfaceAdapter_LocationToolAssignmentErrorRequest(object sender, System.EventArgs e)
        {
            var args = new MessageBoxEventArgs(r => { },
                _localization.Strings.GetParticularString("Test level set assignment", "Some errors occurred for location tool assignments"),
                _localization.Strings.GetParticularString("Test level set assignment", "Warning"),
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
            MessageBoxRequest?.Invoke(this, args);
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

        public void ShowTestLevelSetAssignmentError()
        {
            var args = new MessageBoxEventArgs(r => { },
                _localization.Strings.GetParticularString("Test level set assignment", "Some errors occurred for the test level set assignments"),
                _localization.Strings.GetParticularString("Test level set assignment", "Warning"),
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
            MessageBoxRequest?.Invoke(this, args);
        }

        private bool LoadedCanExecute(object arg) { return true; }
        private void LoadedExecute(object obj)
        {
            _startUp.RaiseShowLoadingControl(true, 4);
            SelectedLocationToolAssignmentsForTestLevelAssignment.Clear();
            _testLevelSetUseCase.LoadTestLevelSets(this);
            _testLevelSetAssignmentUseCase.LoadLocationToolAssignments(this);
            _locationToolAssignmentUseCase.LoadLocationToolAssignments();
            _shiftManagementUseCase.LoadShiftManagement(this);
        }

        private bool AssignTestLevelSetCanExecute(object arg) { return SelectedTestLevelSet != null && SelectedLocationToolAssignmentsForTestLevelAssignment?.Any() == true &&
                                                                       SelectedLocationToolAssignmentsForTestLevelAssignment?.ToList().TrueForAll(x => x.TestLevelSetForTestType?.Entity.EqualsById(SelectedTestLevelSet.Entity) == true) != true; }
        private void AssignTestLevelSetExecute(object obj)
        {
            _testLevelSetAssignmentUseCase.AssignTestLevelSetToLocationToolAssignments(SelectedTestLevelSet.Entity,
                SelectedLocationToolAssignmentsForTestLevelAssignment.Select(x => (x.Entity.Id, x.TestType))
                    .ToList(), 
                this);
        }

        private bool ResetTestLevelSetSelectionCanExecute(object arg) { return SelectedTestLevelSet != null; }
        private void ResetTestLevelSetSelectionExecute(object obj)
        {
            SelectedTestLevelSet = null;
        }

        private bool RemoveTestLevelSetAssignmentsCanExecute(object arg) { return SelectedLocationToolAssignmentsForTestLevelAssignment?.Any() == true && SelectedLocationToolAssignmentsForTestLevelAssignment?.ToList().TrueForAll(x => x.TestLevelSetForTestType == null) != true; }
        private void RemoveTestLevelSetAssignmentsExecute(object obj)
        {
            _testLevelSetAssignmentUseCase.RemoveTestLevelSetAssignmentFor(
                SelectedLocationToolAssignmentsForTestLevelAssignment.Select(x => (x.Entity.Id, x.TestType))
                    .ToList(),
                this);
        }

        private bool SaveTestLevelNumbersCanExecute(object arg) { return ChangedTestLevelNumbersAssignments.Count > 0; }
        private void SaveTestLevelNumbersExecute(object obj)
        {
            var diffs = new List<LocationToolAssignmentDiff>();
            foreach (var pair in ChangedTestLevelNumbersAssignments)
            {
                var old = pair.Key.Entity.CopyDeep();
                if(pair.Key.TestType == TestType.Mfu)
                {
                    old.TestLevelNumberMfu = pair.Value;
                }
                else if(pair.Key.TestType == TestType.Chk)
                {
                    old.TestLevelNumberChk = pair.Value;
                }
                diffs.Add(
                new LocationToolAssignmentDiff()
                    {
                        OldLocationToolAssignment = old,
                        NewLocationToolAssignment = pair.Key.Entity
                    });
            }
            _locationToolAssignmentUseCase.UpdateLocationToolAssignment(diffs, this, this);
            ChangedTestLevelNumbersAssignments.Clear();
        }

        private void ResetChangedTestLevelNumbers()
        {
            foreach (var pair in ChangedTestLevelNumbersAssignments)
            {
                if(pair.Key.TestType == TestType.Mfu)
                {
                    pair.Key.TestLevelNumberMfu = pair.Value;
                }
                else if(pair.Key.TestType == TestType.Chk)
                {
                    pair.Key.TestLevelNumberChk = pair.Value;
                }
            }
            ChangedTestLevelNumbersAssignments.Clear();
            NotifyChangedTestLevelNumbers();
        }

        public void NotifyChangedTestLevelNumbers()
        {
            RaisePropertyChanged(nameof(ChangedTestLevelNumbersAssignments));
        }

        public void Dispose()
        {
            McaLocationToolAssignmentsForOverview.Dispose();
            ChkLocationToolAssignmentsForOverview.Dispose();
            _locationToolAssignmentInterface.LocationToolAssignmentErrorRequest -= InterfaceAdapter_LocationToolAssignmentErrorRequest;
        }

        public bool AskForTestLevelNumbersSaving()
        {
            if (ChangedTestLevelNumbersAssignments.Count > 0)
            {
                MessageBoxResult result = MessageBoxResult.None;
                var args = new MessageBoxEventArgs(r => result = r,
                    _localization.Strings.GetParticularString("ToolTestPlanning", "Do you want to save your changes?"),
                    _localization.Strings.GetParticularString("ToolTestPlanning", "Warning"),
                    MessageBoxButton.YesNoCancel,
                    MessageBoxImage.Warning,
                    messageBoxOptions:MessageBoxOptions.DefaultDesktopOnly,
                    executionMode:ExecutionMode.Async);

                MessageBoxRequest?.Invoke(this, args);

                if (result == MessageBoxResult.Yes)
                {
                    SaveTestLevelNumbersExecute(null);
                    return true;
                }
                else if (result == MessageBoxResult.No)
                {
                    ResetChangedTestLevelNumbers();
                    return true;
                }
                else if (result == MessageBoxResult.Cancel)
                {
                    return false;
                }
            }

            return true;
        }

        public bool CanClose()
        {
            return AskForTestLevelNumbersSaving();
        }

        public void ShowDiffDialog(List<LocationToolAssignmentDiff> diffs, Action saveAction)
        {
            var changes = GetChangesFromLocationToolAssignmentDiffs(diffs).ToList();

            if (changes.Count == 0)
            {
                return;
            }

            var args = new VerifyChangesEventArgs(changes);
            RequestVerifyChangesView?.Invoke(this, args);
            diffs.ForEach(diff => diff.Comment = new HistoryComment(args.Comment));

            _guiDispatcher.Invoke(() =>
            {
                if (args.Result == MessageBoxResult.No)
                {
                    ResetChangedTestLevelNumbers();
                }

                if (args.Result == MessageBoxResult.Yes)
                {
                    saveAction();
                }
            });
        }

        private IEnumerable<SingleValueChangeModel> GetChangesFromLocationToolAssignmentDiffs(List<LocationToolAssignmentDiff> diffs)
        {
            foreach(var diff in diffs)
            {
                if (diff.OldLocationToolAssignment.TestLevelNumberChk != diff.NewLocationToolAssignment.TestLevelNumberChk)
                {
                    yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                    {
                        AffectedEntity = _locationToolAssignmentDisplayFormatter.Format(diff.NewLocationToolAssignment),
                        ChangedAttribute = _localization.Strings.GetParticularString("LocationToolAssignmentDiff", "Test level set number chk"),
                        OldValue = diff.OldLocationToolAssignment.TestLevelNumberChk.ToString(),
                        NewValue = diff.NewLocationToolAssignment.TestLevelNumberChk.ToString()
                    };
                }
                if (diff.OldLocationToolAssignment.TestLevelNumberMfu != diff.NewLocationToolAssignment.TestLevelNumberMfu)
                {
                    yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                    {
                        AffectedEntity = _locationToolAssignmentDisplayFormatter.Format(diff.NewLocationToolAssignment),
                        ChangedAttribute = _localization.Strings.GetParticularString("LocationToolAssignmentDiff", "Test level set number mfu"),
                        OldValue = diff.OldLocationToolAssignment.TestLevelNumberMfu.ToString(),
                        NewValue = diff.NewLocationToolAssignment.TestLevelNumberMfu.ToString()
                    };
                }
            }
        }

        public void ShowLocationToolAssignmentError()
        {
            var args = new MessageBoxEventArgs(r => { },
                _localization.Strings.GetParticularString("Test Planning", "Some errors occurred while saving the test level set numbers"),
                _localization.Strings.GetParticularString("Test Planning message box caption", "Warning"),
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
            MessageBoxRequest?.Invoke(this, args);
        }

        public void ShiftManagementError()
        {
            var args = new MessageBoxEventArgs(r => { },
                 _localization.Strings.GetParticularString("Test Planning", "Some errors occurred while loading the shift management"),
                 _localization.Strings.GetParticularString("Test Planning message box caption", "Warning"),
                 MessageBoxButton.OK,
                 MessageBoxImage.Warning);
            MessageBoxRequest?.Invoke(this, args);
        }
    }
}
