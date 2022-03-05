using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using Core.Enums;
using Core.UseCases.Communication;
using FrameworksAndDrivers.Gui.Wpf.EventArgs;
using FrameworksAndDrivers.Gui.Wpf.Model;
using FrameworksAndDrivers.Gui.Wpf.View;
using InterfaceAdapters;
using InterfaceAdapters.Communication;
using InterfaceAdapters.Localization;
using InterfaceAdapters.Models;

namespace FrameworksAndDrivers.Gui.Wpf.ViewModel
{
    public class TransferToTestEquipmentViewModel:
        BindableBase,
        ITransferToTestEquipmentGui,
        IGetUpdatedByLanguageChanges,
        ITestEquipmentErrorGui
    {
        public TransferToTestEquipmentViewModel(
            ITestEquipmentUseCase testEquipmentUseCase,
            ITransferToTestEquipmentUseCase transferToTestEquipmentUseCase,
            ILocalizationWrapper localization,
            ITestEquipmentInterface interfaceAdapter,
            IStartUp startUp)
        {
            _testEquipmentUseCase = testEquipmentUseCase;
            _transferToTestEquipmentUseCase = transferToTestEquipmentUseCase;
            _localization = localization;
            _localization.Subscribe(this);
            _testEquipmentInterface = interfaceAdapter;
            _startUp = startUp;
            _testEquipmentInterface.ShowLoadingControlRequest += _testEquipmentInterface_ShowLoadingControlRequest;

            PropertyChangedEventManager.AddHandler(
                _testEquipmentInterface,
                (s, e) =>
                {
                    RaisePropertyChanged(nameof(SelectedTestEquipment));
                },
                nameof(TestEquipmentInterfaceAdapter.SelectedTestEquipment));

            LocationToolAssignments = new ObservableCollection<LocationToolAssignmentForTransferHumbleModel>();
            ProcessControlForTransferData = new ObservableCollection<ProcessControlForTransferHumbleModel>();

            Load = new RelayCommand(LoadExecute, _ => true);

            ChkTestType4Transfer =
                new RelayCommand(
                    param =>
                    {
                        _startUp.RaiseShowLoadingControl(true);
                        _transferToTestEquipmentUseCase.ShowLocationToolAssignments(TestType.Chk);
                    },
                    _ => true);
            MfuTestType4Transfer =
                new RelayCommand(
                    param =>
                    {
                        _startUp.RaiseShowLoadingControl(true);
                        _transferToTestEquipmentUseCase.ShowLocationToolAssignments(TestType.Mfu);
                    },
                    _ => true);

            ShowRotatingTestCommand = new RelayCommand(
                param =>
                {
                    _startUp.RaiseShowLoadingControl(true, 2);
                    _testEquipmentUseCase.ShowTestEquipmentsForProcessControlAndRotatingTests(this, ProcessTestingChecked,
                        ToolTestingChecked);
                    _transferToTestEquipmentUseCase.ShowLocationToolAssignments(IsMcaTestTypeChecked
                        ? TestType.Mfu
                        : TestType.Chk);
                    SelectedTestEquipment = null;
                },
                _ => true);

            ShowProcessControlCommand =
                new RelayCommand(
                    param =>
                    {
                        _startUp.RaiseShowLoadingControl(true, 2);
                        _testEquipmentUseCase.ShowTestEquipmentsForProcessControlAndRotatingTests(this, ProcessTestingChecked,
                            ToolTestingChecked);
                        _transferToTestEquipmentUseCase.ShowProcessControlData();
                        SelectedTestEquipment = null;
                    },
                    _ => true);

            SubmitDataToSelectedTestEquipment =
                new RelayCommand(SubmitDataToSelectedTestEquipmentExecute, _ => true);

            ReadDataToSelectedTestEquipment =
                new RelayCommand(ReadDataToSelectedTestEquipmentExecute, _ => true);

            SelectCommand = new RelayCommand(SelectRoute(null, true), _ => true);
            DeselectCommand = new RelayCommand(SelectRoute(null, false), _ => true);

            IsChkTestTypeChecked = true;
            UpdateTranslatedStrings();
        }

        private Action<object> SelectRoute(object obj, bool selected)
        {
            return (o) =>
            {
                if (ToolTestingChecked)
                {
                    foreach (var item in LocationToolAssignments)
                    {
                        item.Selected = selected;
                    }
                }
                else
                {
                    foreach (var item in ProcessControlForTransferData)
                    {
                        item.Selected = selected;
                    }
                }
            };
        }

        private void _testEquipmentInterface_ShowLoadingControlRequest(object sender, bool e)
        {
            _startUp.RaiseShowLoadingControl(e);
        }

        public void SetDispatcher(Dispatcher dispatcher)
        {
            _dispatcher = dispatcher;
            _testEquipmentInterface.SetDispatcher(dispatcher);
        }

        public void ShowProcessControlForTransferList(List<ProcessControlForTransfer> processData)
        {
            ProcessControlForTransferData.Clear();
            foreach (var data in processData)
            {
                ProcessControlForTransferData.Add(new ProcessControlForTransferHumbleModel(data, _localization));
            }
            CheckTestEquipmentCapacity();
            _startUp.RaiseShowLoadingControl(false);
        }

        public void ShowLoadProcessControlDataError()
        {
            var args = new MessageBoxEventArgs(r => { },
                _localization.Strings.GetParticularString("TransferToTestEquipmentViewModel", "Some errors occurred while loading process control data for transfer!"),
                _localization.Strings.GetString("Unknown Error!"),
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            MessageBoxRequest?.Invoke(this, args);
        }

        public void ShowLocationToolAssignmentForTransferList(List<LocationToolAssignmentForTransfer> locationToolAssignments, 
            TestType testType)
        {
            LocationToolAssignments.Clear();
            foreach (var assignment in locationToolAssignments)
            {
                LocationToolAssignments.Add(new LocationToolAssignmentForTransferHumbleModel(assignment));
            }

            if (testType == TestType.Chk)
            {
                SampleNumberName = _translation4MonitoringSamples;
                TestIntervalValueName = _translation4MonitoringPeriod;
                NextTestDateName = _translation4MonitoringNextCheck;
            }
            else
            {
                SampleNumberName = _translation4MfuSamples;
                TestIntervalValueName = _translation4MfuPeriod;
                NextTestDateName = _translation4MfuNextCheck;
            }
            _startUp.RaiseShowLoadingControl(false);
        }

        public void ShowCommunicationProgramNotFoundError()
        {
            var args = new MessageBoxEventArgs(
                _ => { },
                _localization.Strings.GetParticularString(
                    "TransferToTestEquipment",
                    "The communication program could not be found."),
                _localization.Strings.GetString("Error!"),
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            MessageBoxRequest?.Invoke(this, args);
        }

        public void AskToCancelLastTransfer(Action onCancelLastTransfer)
        {
            var args = new MessageBoxEventArgs(
                result =>
                {
                    if (result == MessageBoxResult.Yes)
                    {
                        onCancelLastTransfer();
                    }
                },
                _localization.Strings.GetParticularString(
                    "TransferToTestEquipment",
                    "Another transfer is currently in progress. do you want to cancel the running transfer?"),
                _localization.Strings.GetString("Warning"),
                MessageBoxButton.YesNo,
                MessageBoxImage.Exclamation);
            MessageBoxRequest?.Invoke(this, args);
        }

        public void ShowMismatchingSerialNumber()
        {
            var args = new MessageBoxEventArgs(
                _ => { },
                _localization.Strings.GetParticularString(
                    "TransferToTestEquipment",
                    "The serial number of the device does not match the expected serial number"),
                _localization.Strings.GetString("Error!"),
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            MessageBoxRequest?.Invoke(this, args);
        }

        public void ShowTransmissionError(string message)
        {
            var args = new MessageBoxEventArgs(
                _ => { },
                message,
                _localization.Strings.GetString("Error!"),
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            MessageBoxRequest?.Invoke(this, args);
        }

        public void ShowReadResults(List<TestEquipmentTestResult> results)
        {
            _dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(
                () =>
                {
                    var vm = new TestEquipmentTestResultViewModel(results, _localization);
                    var view = new TestEquipmentTestResultView(vm, _localization);
                    var window = new Window { Content = view };
                    window.Show();
                }));
        }

        public void LanguageUpdate()
        {
            UpdateTranslatedStrings();
        }

        public void ShowProblemSavingTestEquipment()
        {
            var args = new MessageBoxEventArgs(
                _ => { },
                _localization.Strings.GetParticularString(
                    "TransferToTestEquipment",
                    "A problem occured while saving the test equipment"),
                _localization.Strings.GetString("Error!"),
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            MessageBoxRequest?.Invoke(this, args);
        }

        public void ShowProblemRemoveTestEquipment()
        {
            //intentionally empty
        }

        public void ShowErrorMessageLoadingTestEquipments()
        {
            var args = new MessageBoxEventArgs(r => { },
                _localization.Strings.GetParticularString("TransferToTestEquipmentViewModel", "Some errors occurred while loading test equipments"),
                _localization.Strings.GetString("Unknown Error!"),
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            MessageBoxRequest?.Invoke(this, args);
        }

        public void ShowNoTestEquipmentSelectedError()
        {
            var args = new MessageBoxEventArgs(r => { },
                _localization.Strings.GetParticularString("TransferToTestEquipmentViewModel", "No test equipment selected"),
                _localization.Strings.GetString("Error!"),
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            MessageBoxRequest?.Invoke(this, args);
        }

        public void ShowNoRouteSelectedError()
        {
            var args = new MessageBoxEventArgs(r => { },
                _localization.Strings.GetParticularString("TransferToTestEquipmentViewModel", "No valid test route selected"),
                _localization.Strings.GetString("Error!"),
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            MessageBoxRequest?.Invoke(this, args);
        }

        public void ShowErrorMessageLoadingTestEquipmentModels()
        {
            //intentionally empty
        }

        public void TestEquipmentAlreadyExists()
        {
            //intentionally empty
        }

        public void TestEquipmentModelAlreadyExists()
        {
            //intentionally empty
        }

        private List<LocationToolAssignmentForTransfer> GetLocationToolAssignmentForTransfer()
        {
            var result = new List<LocationToolAssignmentForTransfer>();
            foreach (var item in LocationToolAssignments)
            {
                if (item.Selected)
                {
                    result.Add(item.GetEntity());
                }
            }
            return result;
        }

        private void CheckTestEquipmentCapacity()
        {
            foreach (var item in ProcessControlForTransferData)
            {
                item.HasCapacityError = false;
                if (!ToolTestingChecked && SelectedTestEquipment != null && SelectedTestEquipment.Entity.HasCapacity())
                {
                    item.HasCapacityError = !(SelectedTestEquipment.CapacityMin <= item.MinimumTorque &&
                                            SelectedTestEquipment.CapacityMin <= item.SetPointTorque &&
                                            SelectedTestEquipment.CapacityMin <= item.MaximumTorque &&
                                            SelectedTestEquipment.CapacityMax >= item.MinimumTorque &&
                                            SelectedTestEquipment.CapacityMax >= item.SetPointTorque &&
                                            SelectedTestEquipment.CapacityMax >= item.MaximumTorque);
                }
            }
        }

        private List<ProcessControlForTransfer> GetProcessControlForTransfer()
        {
            var result = new List<ProcessControlForTransfer>();
            foreach (var item in ProcessControlForTransferData)
            {
                if (item.Selected && !item.HasCapacityError)
                {
                    result.Add(item.GetEntity());
                }
            }
            return result;
        }

        private void LoadExecute(object _)
        {
            _startUp.RaiseShowLoadingControl(true, 2);
            _transferToTestEquipmentUseCase.ShowLocationToolAssignments(TestType.Chk);
            _testEquipmentUseCase.ShowTestEquipmentsForProcessControlAndRotatingTests(this, ProcessTestingChecked,
                ToolTestingChecked);
        }

        private void SubmitDataToSelectedTestEquipmentExecute(object _)
        {
            if (ToolTestingChecked)
            {
                _transferToTestEquipmentUseCase.SubmitToTestEquipment(
                    SelectedTestEquipment?.Entity,
                    GetLocationToolAssignmentForTransfer(), IsChkTestTypeChecked ? TestType.Chk : TestType.Mfu);
            }
            else
            {
                _transferToTestEquipmentUseCase.SubmitToTestEquipment(SelectedTestEquipment?.Entity,
                    GetProcessControlForTransfer());
            }
        }

        private void ReadDataToSelectedTestEquipmentExecute(object _)
        {
            _transferToTestEquipmentUseCase.ReadFromTestEquipment(SelectedTestEquipment?.Entity);
        }

        private void UpdateTranslatedStrings()
        {
            const string context = "Transfer To Test Equipment";
            _translation4MonitoringSamples = _localization.Strings.GetParticularString(context, "Monitoring samples");
            _translation4MonitoringPeriod = _localization.Strings.GetParticularString(context, "Monitoring period");
            _translation4MonitoringNextCheck = _localization.Strings.GetParticularString(context, "Monitoring next check");
            _translation4MfuSamples = _localization.Strings.GetParticularString(context, "Mfu samples");
            _translation4MfuPeriod = _localization.Strings.GetParticularString(context, "Mfu period");
            _translation4MfuNextCheck = _localization.Strings.GetParticularString(context, "Mfu next check");
        }

        public RelayCommand Load { get; private set; }

        private bool _toolTestingChecked = true;
        public bool ToolTestingChecked
        {
            get => _toolTestingChecked;
            set
            {
                _toolTestingChecked = value;
                RaisePropertyChanged(nameof(ToolTestingChecked));
                RaisePropertyChanged(nameof(ProcessTestingChecked));
                RaisePropertyChanged(nameof(LocationToolAssignmentWithTestLevelSetVisible));
            } 
        }

        public bool ProcessTestingChecked
        {
            get => !_toolTestingChecked;
            set
            {
                _toolTestingChecked = !value;
                RaisePropertyChanged(nameof(ProcessTestingChecked));
                RaisePropertyChanged(nameof(ToolTestingChecked));
                RaisePropertyChanged(nameof(LocationToolAssignmentWithTestLevelSetVisible));
            }
        }

        private bool _isChkTestTypeChecked;
        public bool IsChkTestTypeChecked
        {
            get => _isChkTestTypeChecked;
            set
            {
                _isChkTestTypeChecked = value;
                RaisePropertyChanged(nameof(IsChkTestTypeChecked));
                RaisePropertyChanged(nameof(IsMcaTestTypeChecked));
            }
        }

        public bool IsMcaTestTypeChecked
        {
            get => !_isChkTestTypeChecked;
            set
            {
                _isChkTestTypeChecked = !value;
                RaisePropertyChanged(nameof(IsMcaTestTypeChecked));
                RaisePropertyChanged(nameof(IsChkTestTypeChecked));
            }
        }

        public bool HasCapacityErrorLegendVisible => ProcessControlForTransferData.Any(x => x.HasCapacityError);

        public bool LocationToolAssignmentWithTestLevelSetVisible => ToolTestingChecked;


        public RelayCommand SubmitDataToSelectedTestEquipment { get; private set; }
        public RelayCommand ReadDataToSelectedTestEquipment { get; private set; }
        public RelayCommand ChkTestType4Transfer { get; }
        public RelayCommand MfuTestType4Transfer { get; }
        public RelayCommand ShowProcessControlCommand { get; }
        public RelayCommand ShowRotatingTestCommand { get; }
        public RelayCommand SelectCommand { get; }
        public RelayCommand DeselectCommand { get; }


        private string _sampleNumberName;
        public string SampleNumberName
        {
            get => _sampleNumberName;
            set => Set(ref _sampleNumberName, value);
        }

        private string _testIntervalValueName;
        public string TestIntervalValueName
        {
            get => _testIntervalValueName;
            set => Set(ref _testIntervalValueName, value);
        }

        private string _nextTestDateName;
        public string NextTestDateName
        {
            get => _nextTestDateName;
            set => Set(ref _nextTestDateName, value);
        }

        public ObservableCollection<LocationToolAssignmentForTransferHumbleModel> LocationToolAssignments { get; set; }
        public ObservableCollection<ProcessControlForTransferHumbleModel> ProcessControlForTransferData { get; set; }

        public ObservableCollection<TestEquipmentModelHumbleModel> TestEquipmentModels => _testEquipmentInterface.TestEquipmentModels;
        public ObservableCollection<TestEquipmentHumbleModel> TestEquipments => _testEquipmentInterface.TestEquipments;

        public TestEquipmentHumbleModel SelectedTestEquipment
        {
            get => _testEquipmentInterface.SelectedTestEquipment;
            set
            {
                _testEquipmentInterface.SelectedTestEquipment = value;
                RaisePropertyChanged();
                CheckTestEquipmentCapacity();
                RaisePropertyChanged(nameof(HasCapacityErrorLegendVisible));
            }
        }

        public event EventHandler<MessageBoxEventArgs> MessageBoxRequest;

        private readonly ITestEquipmentUseCase _testEquipmentUseCase;
        private readonly ITransferToTestEquipmentUseCase _transferToTestEquipmentUseCase;
        private ILocalizationWrapper _localization;
        private string _translation4MonitoringSamples;
        private string _translation4MonitoringPeriod;
        private string _translation4MonitoringNextCheck;
        private string _translation4MfuSamples;
        private string _translation4MfuNextCheck;
        private string _translation4MfuPeriod;
        private Dispatcher _dispatcher;
        private ITestEquipmentInterface _testEquipmentInterface;
        private readonly IStartUp _startUp;
    }
}
