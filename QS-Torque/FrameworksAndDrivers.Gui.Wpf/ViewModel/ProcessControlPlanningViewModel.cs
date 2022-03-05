using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Threading;
using Client.Core.Diffs;
using Client.Core.Entities;
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
    public class ProcessControlPlanningViewModel : BindableBase, ITestLevelSetErrorHandler, ITestLevelSetAssignmentErrorHandler, ICanClose, IProcessControlSaveGuiShower, IProcessControlErrorGui, IShiftManagementErrorHandler, IDisposable
    {
        private ILocalizationWrapper _localization;
        private ITestLevelSetUseCase _testLevelSetUseCase;
        private ITestLevelSetInterface _testLevelSetInterface;
        private ITestLevelSetAssignmentUseCase _testLevelSetAssignmentUseCase;
        private ITestLevelSetAssignmentInterface _testLevelSetAssignmentInterface;
        private IShiftManagementUseCase _shiftManagementUseCase;
        private IShiftManagementInterface _shiftManagementInterface;
        private IProcessControlInterface _processControlInterface;
        private IProcessControlUseCase _processControlUseCase;
        private ILocationDisplayFormatter _locationDisplayFormatter;
        private IStartUp _startUp;

        private Dispatcher _guiDispatcher;

        public ShiftManagementModel CurrentShiftManagement => _shiftManagementInterface.CurrentShiftManagement;

        public ObservableCollection<TestLevelSetModel> TestLevelSets
        {
            get => _testLevelSetInterface.TestLevelSets;
        }

        private FilteredObservableCollectionExtension<ProcessControlConditionHumbleModel> _processControlConditionsForOverview;
        public FilteredObservableCollectionExtension<ProcessControlConditionHumbleModel> ProcessControlConditionsForOverview
        {
            get => _processControlConditionsForOverview;
            private set
            {
                _processControlConditionsForOverview = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<ProcessControlConditionHumbleModel> ProcessControlConditions
        {
            get => _processControlInterface.ProcessControlConditions;
        }

        /// <summary>
        /// ProcessControlConditionHumbleModel - original test level number
        /// </summary>
        public Dictionary<ProcessControlConditionHumbleModel, int> ChangedTestLevelNumbersAssignments { get; private set; } = new Dictionary<ProcessControlConditionHumbleModel, int>();

        public ObservableCollection<ProcessControlConditionHumbleModel> SelectedProcessControlsForTestLevelAssignment
        {
            get => _processControlInterface.SelectedProcessControlConditions;
            set
            {
                _processControlInterface.SelectedProcessControlConditions = value;
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


        public Func<long, object, object, bool> CompareIfTestLevelNumberHasChanged { get; private set; }


        public RelayCommand LoadedCommand { get; private set; }
        public RelayCommand AssignTestLevelSetCommand { get; private set; }
        public RelayCommand ResetTestLevelSetSelectionCommand { get; private set; }
        public RelayCommand RemoveTestLevelSetAssignmentsCommand { get; private set; }
        public RelayCommand SaveTestLevelNumbersCommand { get; private set; }


        public event EventHandler<MessageBoxEventArgs> MessageBoxRequest;
        public event EventHandler<VerifyChangesEventArgs> RequestVerifyChangesView;

        public ProcessControlPlanningViewModel(ITestLevelSetAssignmentUseCase testLevelSetAssignmentUseCase,
            ITestLevelSetAssignmentInterface testLevelSetAssignmentInterface,
            ITestLevelSetUseCase testLevelSetUseCase,
            ITestLevelSetInterface testLevelSetInterface,
            IShiftManagementUseCase shiftManagementUseCase,
            IShiftManagementInterface shiftManagementInterface,
            IProcessControlUseCase processControlUseCase,
            IProcessControlInterface processControlInterface,
            ILocalizationWrapper localization,
            ILocationDisplayFormatter locationDisplayFormatter,
            IStartUp startUp)
        {
            _testLevelSetAssignmentUseCase = testLevelSetAssignmentUseCase;
            _testLevelSetAssignmentInterface = testLevelSetAssignmentInterface;
            _testLevelSetUseCase = testLevelSetUseCase;
            _testLevelSetInterface = testLevelSetInterface;
            _shiftManagementUseCase = shiftManagementUseCase;
            _shiftManagementInterface = shiftManagementInterface;
            _processControlUseCase = processControlUseCase;
            _processControlInterface = processControlInterface;
            _localization = localization;
            _locationDisplayFormatter = locationDisplayFormatter;
            _startUp = startUp;

            LoadedCommand = new RelayCommand(LoadedExecute, LoadedCanExecute);
            AssignTestLevelSetCommand = new RelayCommand(AssignTestLevelSetExecute, AssignTestLevelSetCanExecute);
            ResetTestLevelSetSelectionCommand = new RelayCommand(ResetTestLevelSetSelectionExecute, ResetTestLevelSetSelectionCanExecute);
            RemoveTestLevelSetAssignmentsCommand = new RelayCommand(RemoveTestLevelSetAssignmentsExecute, RemoveTestLevelSetAssignmentsCanExecute);
            SaveTestLevelNumbersCommand = new RelayCommand(SaveTestLevelNumbersExecute, SaveTestLevelNumbersCanExecute);
            SelectedProcessControlsForTestLevelAssignment = new ObservableCollection<ProcessControlConditionHumbleModel>();
            ProcessControlConditionsForOverview = new FilteredObservableCollectionExtension<ProcessControlConditionHumbleModel>(_processControlInterface.ProcessControlConditions);
            ProcessControlConditionsForOverview.Filter = x => x.TestLevelSet != null && x.TestOperationActive;

            WireViewModelToInterfaceAdapters();

            CompareIfTestLevelNumberHasChanged = (id, val, param) =>
            {
                return ChangedTestLevelNumbersAssignments.Keys.Any(x => x.Id == id);
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
                nameof(TestLevelSetInterfaceAdapter.TestLevelSets));
            PropertyChangedEventManager.AddHandler(
                _testLevelSetAssignmentInterface,
                (s, e) => RaisePropertyChanged(nameof(SelectedTestLevelSet)),
                nameof(ToolTestPlanningViewModel.SelectedTestLevelSet));
            PropertyChangedEventManager.AddHandler(
                _processControlInterface,
                (s, e) =>
                {
                    ProcessControlConditionsForOverview.SetNewSourceCollection(_processControlInterface.ProcessControlConditions);
                    RaisePropertyChanged(nameof(ProcessControlConditionsForOverview));
                    RaisePropertyChanged(nameof(ProcessControlConditions));
                    _startUp.RaiseShowLoadingControl(false);
                },
                nameof(ProcessControlInterfaceAdapter.ProcessControlConditions));
            PropertyChangedEventManager.AddHandler(
                _processControlInterface,
                (s, e) => RaisePropertyChanged(nameof(SelectedProcessControlsForTestLevelAssignment)),
                nameof(ProcessControlInterfaceAdapter.SelectedProcessControlConditions));
            PropertyChangedEventManager.AddHandler(
                _shiftManagementInterface,
                (s, e) =>
                {
                    RaisePropertyChanged(nameof(CurrentShiftManagement));
                    _startUp.RaiseShowLoadingControl(false);
                },
                nameof(IShiftManagementInterface.CurrentShiftManagement));
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
            _startUp.RaiseShowLoadingControl(true, 3);
            SelectedProcessControlsForTestLevelAssignment?.Clear();
            _testLevelSetUseCase.LoadTestLevelSets(this);
            _processControlUseCase.LoadProcessControlConditions(this);
            _shiftManagementUseCase.LoadShiftManagement(this);
        }

        private bool AssignTestLevelSetCanExecute(object arg)
        {
            return SelectedTestLevelSet != null && SelectedProcessControlsForTestLevelAssignment?.Any() == true &&
                   SelectedProcessControlsForTestLevelAssignment?.ToList().TrueForAll(x => x.TestLevelSet?.Entity.EqualsById(SelectedTestLevelSet.Entity) == true) != true;
        }
        private void AssignTestLevelSetExecute(object obj)
        {
            _testLevelSetAssignmentUseCase.AssignTestLevelSetToProcessControlConditions(SelectedTestLevelSet.Entity,
                SelectedProcessControlsForTestLevelAssignment.Select(x => x.Entity.Id).ToList(),
                this, this);
        }

        private bool ResetTestLevelSetSelectionCanExecute(object arg) { return SelectedTestLevelSet != null; }
        private void ResetTestLevelSetSelectionExecute(object obj)
        {
            SelectedTestLevelSet = null;
        }

        private bool RemoveTestLevelSetAssignmentsCanExecute(object arg) { return SelectedProcessControlsForTestLevelAssignment?.Any() == true && SelectedProcessControlsForTestLevelAssignment?.ToList().TrueForAll(x => x.TestLevelSet == null) != true; }
        private void RemoveTestLevelSetAssignmentsExecute(object obj)
        {
            _testLevelSetAssignmentUseCase.RemoveTestLevelSetAssignmentFor(
                SelectedProcessControlsForTestLevelAssignment.Select(x => x.Entity.Id)
                    .ToList(),
                this);
        }

        private bool SaveTestLevelNumbersCanExecute(object arg) { return ChangedTestLevelNumbersAssignments.Count > 0; }
        private void SaveTestLevelNumbersExecute(object obj)
        {
            var diffs = new List<ProcessControlConditionDiff>();
            foreach (var pair in ChangedTestLevelNumbersAssignments)
            {
                var old = pair.Key.Entity.CopyDeep();
                old.TestLevelNumber = pair.Value;
                diffs.Add(new ProcessControlConditionDiff(null, null, old, pair.Key.Entity));
            }
            _processControlUseCase.SaveProcessControlCondition(diffs, this, this);
            ChangedTestLevelNumbersAssignments.Clear();
        }

        private void ResetChangedTestLevelNumbers()
        {
            foreach (var pair in ChangedTestLevelNumbersAssignments)
            {
                pair.Key.TestLevelNumber = pair.Value;
            }
            ChangedTestLevelNumbersAssignments.Clear();
            NotifyChangedTestLevelNumbers();
        }

        public void NotifyChangedTestLevelNumbers()
        {
            RaisePropertyChanged(nameof(ChangedTestLevelNumbersAssignments));
        }

        public bool AskForTestLevelNumbersSaving()
        {
            if (ChangedTestLevelNumbersAssignments.Count > 0)
            {
                MessageBoxResult result = MessageBoxResult.None;
                var args = new MessageBoxEventArgs(r => result = r,
                    _localization.Strings.GetParticularString("ProcessControlPlanning", "Do you want to save your changes?"),
                    _localization.Strings.GetParticularString("ProcessControlPlanning", "Warning"),
                    MessageBoxButton.YesNoCancel,
                    MessageBoxImage.Warning,
                    messageBoxOptions: MessageBoxOptions.DefaultDesktopOnly,
                    executionMode: ExecutionMode.Async);

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

        public void SaveProcessControl(List<ProcessControlConditionDiff> diffs, Action saveAction)
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

                if(args.Result == MessageBoxResult.Cancel)
                {
                    ChangedTestLevelNumbersAssignments.Clear();
                    diffs.ForEach(x =>
                    {
                        ChangedTestLevelNumbersAssignments.Add(ProcessControlConditionHumbleModel.GetModelFor(x.GetNewProcessControlCondition(), _localization), x.GetOldProcessControlCondition().TestLevelNumber);
                    });
                }
            });
        }

        private IEnumerable<SingleValueChangeModel> GetChangesFromLocationToolAssignmentDiffs(List<ProcessControlConditionDiff> diffs)
        {
            foreach (var diff in diffs)
            {
                if (diff.GetOldProcessControlCondition().TestLevelNumber != diff.GetNewProcessControlCondition().TestLevelNumber)
                {
                    yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                    {
                        AffectedEntity = _locationDisplayFormatter.Format(diff.GetNewProcessControlCondition().Location),
                        ChangedAttribute = _localization.Strings.GetParticularString("LocationToolAssignmentDiff", "Test level set number chk"),
                        OldValue = diff.GetOldProcessControlCondition().TestLevelNumber.ToString(),
                        NewValue = diff.GetNewProcessControlCondition().TestLevelNumber.ToString()
                    };
                }
            }
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

        public void ShowProblemLoadingLocationProcessControlCondition()
        {
            var args = new MessageBoxEventArgs(r => { },
                 _localization.Strings.GetParticularString("Test Planning", "Some errors occurred while loading the process controls"),
                 _localization.Strings.GetParticularString("Test Planning message box caption", "Warning"),
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
            var args = new MessageBoxEventArgs(r => { },
                 _localization.Strings.GetParticularString("Test Planning", "Some errors occurred while saving the process controls"),
                 _localization.Strings.GetParticularString("Test Planning message box caption", "Warning"),
                 MessageBoxButton.OK,
                 MessageBoxImage.Warning);
            MessageBoxRequest?.Invoke(this, args);
        }

        public void Dispose()
        {
            ProcessControlConditionsForOverview.Dispose();
        }
    }
}
