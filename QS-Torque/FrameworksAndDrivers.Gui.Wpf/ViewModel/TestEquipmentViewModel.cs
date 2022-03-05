using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Client.Core.Diffs;
using Client.Core.Entities;
using Client.Core.Validator;
using Common.Types.Enums;
using Core.Diffs;
using Core.Entities;
using Core.UseCases;
using Core.UseCases.Communication;
using FrameworksAndDrivers.Gui.Wpf.EventArgs;
using FrameworksAndDrivers.Gui.Wpf.Interfaces;
using FrameworksAndDrivers.Gui.Wpf.Model;
using FrameworksAndDrivers.Gui.Wpf.View;
using InterfaceAdapters;
using InterfaceAdapters.Communication;
using InterfaceAdapters.Localization;
using InterfaceAdapters.Models;

namespace FrameworksAndDrivers.Gui.Wpf.ViewModel
{
    public class TestEquipmentViewModel : BindableBase, 
        ITestEquipmentErrorGui, 
        ITestEquipmentSaveGuiShower, 
        IClearShownChanges,
        ICanClose
    {
        private readonly ITestEquipmentUseCase _testEquipmentUseCase;
        private readonly ILocalizationWrapper _localization;
        private readonly IStartUp _startUp;
        private readonly ITestEquipmentInterface _testEquipmentInterface;
        private readonly ISessionInformationUseCase _sessionInformation;
        private readonly ITestEquipmentValidator _testEquipmentValidator;
        private Dispatcher _guiDispatcher;
        public event EventHandler<VerifyChangesEventArgs> RequestVerifyChangesView;
        public event EventHandler<TestEquipmentHumbleModel> SelectionRequestTestEquipment;
        public event EventHandler<TestEquipmentModelHumbleModel> SelectionRequestTestEquipmentModel;
        public event EventHandler<MessageBoxEventArgs> MessageBoxRequest;
        public event EventHandler<ICanShowDialog> ShowDialogRequest;
        public event EventHandler ClearShownChanges;

        public TestEquipmentViewModel(ITestEquipmentUseCase testEquipmentUseCase, ILocalizationWrapper localization, 
            IStartUp startUp, ITestEquipmentInterface testEquipmentInterface, ISessionInformationUseCase sessionInformation,
            ITestEquipmentValidator testEquipmentValidator)
        {
            _testEquipmentUseCase = testEquipmentUseCase;
            _localization = localization;
            _startUp = startUp;
            _testEquipmentInterface = testEquipmentInterface;
            _sessionInformation = sessionInformation;
            _testEquipmentValidator = testEquipmentValidator;

            WireViewModelToTestEquipmentInterface();

            _testEquipmentInterface.ShowLoadingControlRequest += TestEquipmentInterface_ShowLoadingControlRequest;
            _testEquipmentInterface.SelectionRequestTestEquipment += (s, e) => SelectionRequestTestEquipment?.Invoke(s, e);
            LoadedCommand = new RelayCommand(LoadedCommandExecute, LoadedCommandCanExecute);
            AddTestEquipmentCommand = new RelayCommand(AddTestEquipmentExecute, AddTestEquipmentCanExecute);
            RemoveTestEquipmentCommand = new RelayCommand(RemoveTestEquipmentExecute, RemoveTestEquipmentCanExecute);
            SaveTestEquipmentCommand = new RelayCommand(SaveTestEquipmentExecute, SaveTestEquipmentCanExecute);
            SelectDriverPathCommand = new RelayCommand(SelectDriverPathExecute, SelectDriverPathCanExecute);
            SelectCommunicationPathCommand = new RelayCommand(SelectCommunicationPathExecute, SelectCommunicationPathCanExecute);
            SelectResultPathCommand = new RelayCommand(SelectResultPathExecute, SelectResultPathCanExecute);
            SelectStatusPathCommand = new RelayCommand(SelectStatusPathExecute, SelectStatusPathCanExecute);
        }

        private void WireViewModelToTestEquipmentInterface()
        {
            PropertyChangedEventManager.AddHandler(
                _testEquipmentInterface,
                (s, e) =>
                {
                    RaisePropertyChanged(nameof(SelectedTestEquipment));
                    RaisePropertyChanged(nameof(SelectedTestEquipmentModel));
                    RaisePropertyChanged(nameof(IsTestEquipmentVisible));
                    RaisePropertyChanged(nameof(IsAdditionalSettingVisible));
                    CommandManager.InvalidateRequerySuggested();
                },
                nameof(TestEquipmentInterfaceAdapter.SelectedTestEquipment));

            PropertyChangedEventManager.AddHandler(
                _testEquipmentInterface,
                (s, e) => RaisePropertyChanged(nameof(SelectedTestEquipmentWithoutChanges)),
                nameof(TestEquipmentInterfaceAdapter.SelectedTestEquipmentWithoutChanges));

            PropertyChangedEventManager.AddHandler(
                _testEquipmentInterface,
                (s, e) =>
                {
                    RaisePropertyChanged(nameof(SelectedTestEquipmentModel));
                    RaisePropertyChanged(nameof(SelectedTestEquipment));
                    RaisePropertyChanged(nameof(IsTestEquipmentModelVisible));
                },
                nameof(TestEquipmentInterfaceAdapter.SelectedTestEquipmentModel));

            PropertyChangedEventManager.AddHandler(
                _testEquipmentInterface,
                (s, e) => RaisePropertyChanged(nameof(SelectedTestEquipmentModelWithoutChanges)),
                nameof(TestEquipmentInterfaceAdapter.SelectedTestEquipmentModelWithoutChanges));
        }

        public void SetDispatcher(Dispatcher dispatcher)
        {
            _guiDispatcher = dispatcher;
            _testEquipmentInterface.SetDispatcher(dispatcher);
        }

        private void TestEquipmentInterface_ShowLoadingControlRequest(object sender, bool e)
        {
            _startUp.RaiseShowLoadingControl(e);
        }
        
        public RelayCommand LoadedCommand { get; set; }

        private bool LoadedCommandCanExecute(object arg)
        {
            return true;
        }

        private void LoadedCommandExecute(object obj)
        {
            _startUp.RaiseShowLoadingControl(true);
            _testEquipmentUseCase.ShowTestEquipments(this);
        }


        public RelayCommand SaveTestEquipmentCommand { get; set; }

        private bool SaveTestEquipmentCanExecute(object arg)
        {
            if (_testEquipmentInterface.SelectedTestEquipment == null && _testEquipmentInterface.SelectedTestEquipmentModel == null)
            {
                return false;
            }

            if (_testEquipmentInterface.SelectedTestEquipmentModel != null)
            {
               if(!_testEquipmentInterface.SelectedTestEquipmentModel.EqualsByContent(_testEquipmentInterface.SelectedTestEquipmentModelWithoutChanges))
               {
                    return _testEquipmentValidator.Validate(SelectedTestEquipmentModel?.Entity);
               }
            }

            if (_testEquipmentInterface.SelectedTestEquipment != null)
            {
                if(!_testEquipmentInterface.SelectedTestEquipment.EqualsByContent(_testEquipmentInterface.SelectedTestEquipmentWithoutChanges))
                {
                    return _testEquipmentValidator.Validate(SelectedTestEquipment?.Entity);
                }
            }

            return false;
        }

        private void SaveTestEquipmentExecute(object obj)
        {
            if (_testEquipmentInterface.SelectedTestEquipmentModel != null)
            {
                _startUp.RaiseShowLoadingControl(true);

                var diff = new TestEquipmentModelDiff(
                    _testEquipmentInterface.SelectedTestEquipmentModelWithoutChanges.Entity,
                    SelectedTestEquipmentModel.Entity, null);

                _testEquipmentUseCase.SaveTestEquipmentModel(diff, this, this);
            }
            else if (_testEquipmentInterface.SelectedTestEquipment != null)
            {
                _startUp.RaiseShowLoadingControl(true);

                var diff = new TestEquipmentDiff(
                    _testEquipmentInterface.SelectedTestEquipmentWithoutChanges.Entity,
                    SelectedTestEquipment.Entity, null);

                _testEquipmentUseCase.SaveTestEquipment(diff, this, this);
            }
        }

        public void SaveTestEquipmentModel(TestEquipmentModelDiff diff, Action saveAction)
        {
            var result = ShowTestEquipmentModelChangesDialog(diff);
            if (result == null)
            {
                return;
            }

            _guiDispatcher.Invoke(() =>
            {
                if (result == MessageBoxResult.No)
                {
                    _testEquipmentInterface.SelectedTestEquipmentModel.UpdateWith(_testEquipmentInterface.SelectedTestEquipmentModelWithoutChanges?.Entity);
                    _startUp.RaiseShowLoadingControl(false);
                }

                if (result == MessageBoxResult.Yes)
                {
                    saveAction();
                }

                if (result == MessageBoxResult.Cancel)
                {
                    _startUp.RaiseShowLoadingControl(false);
                }
            });
        }

        public void SaveTestEquipment(TestEquipmentDiff diff, Action saveAction)
        {
            var result = ShowTestEquipmentChangesDialog(diff);
            if (result == null)
            {
                return;
            }

            _guiDispatcher.Invoke(() =>
            {
                if (result == MessageBoxResult.No)
                {
                    _testEquipmentInterface.SelectedTestEquipment.UpdateWith(_testEquipmentInterface.SelectedTestEquipmentWithoutChanges?.Entity);
                    _startUp.RaiseShowLoadingControl(false);
                }

                if (result == MessageBoxResult.Yes)
                {
                    saveAction();
                }

                if (result == MessageBoxResult.Cancel)
                {
                    _startUp.RaiseShowLoadingControl(false);
                }
            });
        }

        public RelayCommand RemoveTestEquipmentCommand { get; set; }
        private bool RemoveTestEquipmentCanExecute(object arg)
        {
            return SelectedTestEquipment != null;
        }

        private void RemoveTestEquipmentExecute(object obj)
        {
            Action<MessageBoxResult> resultAction = (r) =>
            {
                if (r != MessageBoxResult.Yes)
                {
                    _startUp.RaiseShowLoadingControl(false);
                    return;
                }

                SelectedTestEquipment.UpdateWith(_testEquipmentInterface.SelectedTestEquipmentWithoutChanges?.Entity);
                _testEquipmentUseCase.RemoveTestEquipment(SelectedTestEquipment.Entity, this);
            };

            var args = new MessageBoxEventArgs(resultAction,
                _localization.Strings.GetString("Do you really want to remove this item?"),
                _localization.Strings.GetParticularString("Window Title", "Warning"),
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            _startUp.RaiseShowLoadingControl(true);
            MessageBoxRequest?.Invoke(this, args);
        }

        public RelayCommand AddTestEquipmentCommand { get; set; }
        private bool AddTestEquipmentCanExecute(object arg)
        {
            return true;
        }

        private void AddTestEquipmentExecute(object obj)
        {
            if (SelectedTestEquipmentModel != null)
            {
                var previousSelectedTestEquipmentModel = SelectedTestEquipmentModel;
                SelectedTestEquipmentModel = null;
                if (SelectedTestEquipmentModel != null)
                {
                    return;
                }

                SelectedTestEquipmentModel = previousSelectedTestEquipmentModel;
            }

            var previousSelectedTestEquipment = SelectedTestEquipment;
            SelectedTestEquipment = null;
            if (SelectedTestEquipment != null)
            {
                return;
            }
            SelectedTestEquipment = previousSelectedTestEquipment;

            AssistentView assistant = null;
            if (previousSelectedTestEquipment != null)
            {
                assistant =_startUp.OpenAddTestEquipmentAssistant(_testEquipmentUseCase.LoadAvailableTestEquipmentTypes(), previousSelectedTestEquipment.Entity);
            }
            else if (SelectedTestEquipmentModel != null)
            {
                assistant = _startUp.OpenAddTestEquipmentAssistant(_testEquipmentUseCase.LoadAvailableTestEquipmentTypes(), null, SelectedTestEquipmentModel.Entity);
            }
            else
            {
                assistant = _startUp.OpenAddTestEquipmentAssistant(_testEquipmentUseCase.LoadAvailableTestEquipmentTypes(), null, null, SelectedTestEquipmentType?.Type);
            }
            
            assistant.EndOfAssistent += (s, e) =>
            {
                var testEquipment = (TestEquipment)(assistant.DataContext as AssistentViewModel)?.FillResultObject(new TestEquipment());
                if (testEquipment == null)
                    return;

                testEquipment.Id = new TestEquipmentId(0);
                _testEquipmentUseCase.AddTestEquipment(testEquipment, this);

            };
            assistant.Closed += (s, e) =>
            {
                _startUp.RaiseShowLoadingControl(false);
            };

            _startUp.RaiseShowLoadingControl(true);
            ShowDialogRequest?.Invoke(this, assistant);
        }
   

        public event EventHandler<FileDialogEventArgs> FileDialogRequest;

        public RelayCommand SelectDriverPathCommand { get; set; }
        private bool SelectDriverPathCanExecute(object arg) {return true;}

        private void SelectDriverPathExecute(object obj)
        {
            FileDialogRequest?.Invoke(this, new FileDialogEventArgs((path) =>
            {
                SelectedTestEquipmentModel.DriverProgramPath = path;
            }));
        }

        public RelayCommand SelectCommunicationPathCommand { get; set; }
        private bool SelectCommunicationPathCanExecute(object arg) { return true; }
        private void SelectCommunicationPathExecute(object obj)
        {
            FileDialogRequest?.Invoke(this, new FileDialogEventArgs((path) =>
            {
                SelectedTestEquipmentModel.CommunicationFilePath = path;
            }));
        }

        public RelayCommand SelectResultPathCommand { get; set; }
        private bool SelectResultPathCanExecute(object arg) { return true; }
        private void SelectResultPathExecute(object obj)
        {
            FileDialogRequest?.Invoke(this, new FileDialogEventArgs((path) =>
            {
                SelectedTestEquipmentModel.ResultFilePath = path;
            }));
        }

        public RelayCommand SelectStatusPathCommand { get; set; }
        private bool SelectStatusPathCanExecute(object arg) { return true; }
        private void SelectStatusPathExecute(object obj)
        {
            FileDialogRequest?.Invoke(this, new FileDialogEventArgs((path) =>
            {
                SelectedTestEquipmentModel.StatusFilePath = path;
            }));
        }

        public ObservableCollection<TestEquipmentModelHumbleModel> TestEquipmentModels => _testEquipmentInterface.TestEquipmentModels;
        public ObservableCollection<TestEquipmentHumbleModel> TestEquipments => _testEquipmentInterface.TestEquipments;
        public ObservableCollection<DataGateVersion> DataGateVersions => _testEquipmentInterface.DataGateVersions;

        public void ShowProblemSavingTestEquipment()
        {
            var args = new MessageBoxEventArgs(r => { },
                _localization.Strings.GetParticularString("TestEquipmentViewModel", "Some errors occurred while saving test equipments"),
                _localization.Strings.GetString("Unknown Error!"),
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            MessageBoxRequest?.Invoke(this, args);
        }

        public void ShowProblemRemoveTestEquipment()
        {
            _guiDispatcher.Invoke(() =>
            {
                var args = new MessageBoxEventArgs((r) => { },
                    _localization.Strings.GetParticularString("TestEquipmentViewModel", "Some errors occurred while removing test equipment"),
                    _localization.Strings.GetString("Unknown Error!"),
                    messageBoxButton: MessageBoxButton.OK,
                    messageBoxImage: MessageBoxImage.Error);
                MessageBoxRequest?.Invoke(this, args);
                _startUp.RaiseShowLoadingControl(false);
            });
        }

        public void ShowErrorMessageLoadingTestEquipments()
        {
            var args = new MessageBoxEventArgs(r => { },
                _localization.Strings.GetParticularString("TestEquipmentViewModel", "Some errors occurred while loading test equipments"),
                _localization.Strings.GetString("Unknown Error!"),
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
            var args = new MessageBoxEventArgs(r => { },
                _localization.Strings.GetParticularString("TestEquipmentViewModel", "A test equipment with this serial number or inventory number already exists"),
                messageBoxImage: MessageBoxImage.Warning);
            MessageBoxRequest?.Invoke(this, args);
        }

        public void TestEquipmentModelAlreadyExists()
        {
            var args = new MessageBoxEventArgs(r => { },
                _localization.Strings.GetParticularString("TestEquipmentViewModel", "A test equipment model with this name already exists"),
                messageBoxImage: MessageBoxImage.Warning);
            MessageBoxRequest?.Invoke(this, args);
        }

        private TestEquipmentTypeModel _selectedTestEquipmentType;

        public TestEquipmentTypeModel SelectedTestEquipmentType
        {
            get => _selectedTestEquipmentType;
            set
            {
                _selectedTestEquipmentType = value;
                RaisePropertyChanged();
            }
        }


        public TestEquipmentHumbleModel SelectedTestEquipmentWithoutChanges =>
            _testEquipmentInterface.SelectedTestEquipmentWithoutChanges;

        public TestEquipmentHumbleModel SelectedTestEquipment
        {
            get => _testEquipmentInterface.SelectedTestEquipment;
            set
            {
                if (_testEquipmentInterface.SelectedTestEquipment == value)
                {
                    return;
                }

                if (!_testEquipmentValidator.Validate(SelectedTestEquipment?.Entity))
                {
                    var continueEditing = true;
                    MessageBoxRequest?.Invoke(this, new MessageBoxEventArgs(r =>
                    {
                        if (r != MessageBoxResult.No)
                            return;

                        SelectedTestEquipment.UpdateWith(SelectedTestEquipmentWithoutChanges?.Entity);
                        continueEditing = false;
                    },
                        _localization.Strings.GetParticularString("TestEquipmentViewModel",
                            "The test equipment is not valid, do you want to continue editing? (If not, the test equipment is reseted to the last saved value)"),
                        _localization.Strings.GetParticularString("TestEquipmentViewModel", "Test equipment not valid"),
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Error));

                    if (continueEditing)
                    {
                        SelectionRequestTestEquipment?.Invoke(this, SelectedTestEquipment);
                        return;
                    }
                }

                if (_testEquipmentInterface.SelectedTestEquipment != null &&
                    _testEquipmentInterface.SelectedTestEquipmentWithoutChanges != null &&
                    !_testEquipmentInterface.SelectedTestEquipment.EqualsByContent(_testEquipmentInterface.SelectedTestEquipmentWithoutChanges))
                {
                    var diff = new TestEquipmentDiff(
                        _testEquipmentInterface.SelectedTestEquipmentWithoutChanges?.Entity,
                        SelectedTestEquipment?.Entity, null);

                    var result = ShowTestEquipmentChangesDialog(diff);
                    if (result != null)
                    {
                        switch (result)
                        {
                            case MessageBoxResult.Yes:
                                _startUp.RaiseShowLoadingControl(true);
                                _testEquipmentUseCase.UpdateTestEquipment(diff, this);
                                break;
                            case MessageBoxResult.No:
                                SelectedTestEquipment.UpdateWith(SelectedTestEquipmentWithoutChanges.Entity);
                                break;
                            case MessageBoxResult.Cancel:
                                SelectionRequestTestEquipment?.Invoke(this, _testEquipmentInterface.SelectedTestEquipment);
                                return;
                        }
                    }
                }

                _testEquipmentInterface.SelectedTestEquipment = value;
                RaisePropertyChanged();
            }
        }

        public TestEquipmentModelHumbleModel SelectedTestEquipmentModelWithoutChanges =>
            _testEquipmentInterface.SelectedTestEquipmentModelWithoutChanges;

        public TestEquipmentModelHumbleModel SelectedTestEquipmentModel
        {
            get => _testEquipmentInterface.SelectedTestEquipmentModel;
            set
            {
                if (_testEquipmentInterface.SelectedTestEquipmentModel != null &&
                    _testEquipmentInterface.SelectedTestEquipmentModel.Equals(value))
                {
                    return;
                }

                if (!_testEquipmentValidator.Validate(SelectedTestEquipmentModel?.Entity))
                {
                    var continueEditing = true;
                    MessageBoxRequest?.Invoke(this, new MessageBoxEventArgs(r =>
                        {
                            if (r != MessageBoxResult.No)
                                return;

                            SelectedTestEquipmentModel.UpdateWith(SelectedTestEquipmentModelWithoutChanges?.Entity);
                            continueEditing = false;
                        },
                        _localization.Strings.GetParticularString("TestEquipmentViewModel",
                            "The test equipment model is not valid, do you want to continue editing? (If not, the test equipment model is reseted to the last saved value)"),
                        _localization.Strings.GetParticularString("TestEquipmentViewModel", "Test equipment model not valid"),
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Error));

                    if (continueEditing)
                    {
                        SelectionRequestTestEquipmentModel?.Invoke(this, SelectedTestEquipmentModel);
                        return;
                    }
                }

                if (_testEquipmentInterface.SelectedTestEquipmentModel != null &&
                    _testEquipmentInterface.SelectedTestEquipmentModelWithoutChanges != null &&
                    !_testEquipmentInterface.SelectedTestEquipmentModel.EqualsByContent(_testEquipmentInterface.SelectedTestEquipmentModelWithoutChanges))
                {
                    var diff = new TestEquipmentModelDiff(
                        _testEquipmentInterface.SelectedTestEquipmentModelWithoutChanges?.Entity,
                        SelectedTestEquipmentModel?.Entity, null);

                    var result = ShowTestEquipmentModelChangesDialog(diff);
                    if (result != null)
                    {
                        switch (result)
                        {
                            case MessageBoxResult.Yes:
                                _startUp.RaiseShowLoadingControl(true);
                                _testEquipmentUseCase.UpdateTestEquipmentModel(diff, this);
                                break;
                            case MessageBoxResult.No:
                                SelectedTestEquipmentModel.UpdateWith(SelectedTestEquipmentModelWithoutChanges.Entity);
                                break;
                            case MessageBoxResult.Cancel:
                                SelectionRequestTestEquipmentModel?.Invoke(this, _testEquipmentInterface.SelectedTestEquipmentModel);
                                return;
                        }
                    }
                }
                
                _testEquipmentInterface.SelectedTestEquipmentModel = value;

                if (value?.Entity != null)
                {
                    ShowTransferAttributes = value.Entity.HasTransferAttributes();
                    ShowTestBehavior = value.Entity.HasTestBehavior();
                }
                RaisePropertyChanged();
            }
        }


        public bool IsTestEquipmentVisible => SelectedTestEquipment != null;
        public bool IsAdditionalSettingVisible
        {
            get
            {
                if (SelectedTestEquipment?.Entity?.TestEquipmentModel == null)
                {
                    return false;
                }

                return SelectedTestEquipment.Entity.TestEquipmentModel.Type == TestEquipmentType.Wrench;
            }
        }

        private bool _showTestBehavior;

        public bool ShowTestBehavior
        {
            get => _showTestBehavior;
            set
            {
                _showTestBehavior = value;
                RaisePropertyChanged();
            }
        }

        private bool _showTransferAttributes;

        public bool ShowTransferAttributes
        {
            get => _showTransferAttributes;
            set
            {
                _showTransferAttributes = value;
                RaisePropertyChanged();
            }
        }

        public bool IsTestEquipmentModelVisible => SelectedTestEquipmentModel != null;

        private MessageBoxResult? ShowTestEquipmentChangesDialog(TestEquipmentDiff diff)
        {
            var changes = GetChangesFromTestEquipmentDiff(diff).ToList();

            if (changes.Count == 0)
            {
                return null;
            }

            var args = new VerifyChangesEventArgs(changes);
            RequestVerifyChangesView?.Invoke(this, args);
            diff.Comment = new HistoryComment(args.Comment);
            return args.Result;
        }

        private IEnumerable<SingleValueChangeModel> GetChangesFromTestEquipmentDiff(TestEquipmentDiff diff)
        {
            var entity = diff.NewTestEquipment.SerialNumber.ToDefaultString();

            if (!diff.OldTestEquipment.SerialNumber.Equals(diff.NewTestEquipment.SerialNumber))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("TestEquipmentAttribute", "Serial number"),
                    OldValue = diff.OldTestEquipment.SerialNumber.ToDefaultString(),
                    NewValue = diff.NewTestEquipment.SerialNumber.ToDefaultString()
                };
            }

            if (!diff.OldTestEquipment.InventoryNumber.Equals(diff.NewTestEquipment.InventoryNumber))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("TestEquipmentAttribute", "Inventory number"),
                    OldValue = diff.OldTestEquipment.InventoryNumber.ToDefaultString(),
                    NewValue = diff.NewTestEquipment.InventoryNumber.ToDefaultString()
                };
            }

            if (!diff.OldTestEquipment.TestEquipmentModel.EqualsById(diff.NewTestEquipment?.TestEquipmentModel))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("TestEquipmentAttribute", "Test equipment model"),
                    OldValue = diff.OldTestEquipment?.TestEquipmentModel?.TestEquipmentModelName?.ToDefaultString(),
                    NewValue = diff.NewTestEquipment?.TestEquipmentModel?.TestEquipmentModelName?.ToDefaultString()
                };
            }

            if ((diff.OldTestEquipment.Status == null && diff.NewTestEquipment.Status != null)
                || (!diff.OldTestEquipment.Status?.EqualsById(diff.NewTestEquipment.Status) ?? false))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("TestEquipmentAttribute", "Status"),
                    OldValue = diff.OldTestEquipment?.Status?.Value?.ToDefaultString(),
                    NewValue = diff.NewTestEquipment?.Status?.Value?.ToDefaultString()
                };
            }

            if ((diff.OldTestEquipment.Version == null && diff.NewTestEquipment.Version != null)
                || (!diff.OldTestEquipment.Version?.Equals(diff.NewTestEquipment.Version) ?? false))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("TestEquipmentAttribute", "Firmware version"),
                    OldValue = diff.OldTestEquipment.Version?.ToDefaultString(),
                    NewValue = diff.NewTestEquipment.Version?.ToDefaultString()
                };
            }

            if (!diff.OldTestEquipment.CapacityMin.Equals(diff.NewTestEquipment.CapacityMin))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("TestEquipmentAttribute", "Capacity minimum"),
                    OldValue = diff.OldTestEquipment.CapacityMin.Nm.ToString(),
                    NewValue = diff.NewTestEquipment.CapacityMin.Nm.ToString()
                };
            }

            if (!diff.OldTestEquipment.CapacityMax.Equals(diff.NewTestEquipment.CapacityMax))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("TestEquipmentAttribute", "Capacity maximum"),
                    OldValue = diff.OldTestEquipment.CapacityMax.Nm.ToString(),
                    NewValue = diff.NewTestEquipment.CapacityMax.Nm.ToString()
                };
            }


            if (!diff.OldTestEquipment.LastCalibration.Equals(diff.NewTestEquipment.LastCalibration))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("TestEquipmentAttribute", "Last calibration"),
                    OldValue = diff.OldTestEquipment.LastCalibration == null ? "" : diff.OldTestEquipment.LastCalibration.Value.ToShortDateString(),
                    NewValue = diff.NewTestEquipment.LastCalibration == null ? "" : diff.NewTestEquipment.LastCalibration.Value.ToShortDateString()
                };
            }

            if (!diff.OldTestEquipment.CalibrationInterval.EqualsByContent(diff.NewTestEquipment.CalibrationInterval))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("TestEquipmentAttribute", "Calibration interval"),
                    OldValue = diff.OldTestEquipment.CalibrationInterval.IntervalValue.ToString(),
                    NewValue = diff.NewTestEquipment.CalibrationInterval.IntervalValue.ToString()
                };
            }

            if ((diff.OldTestEquipment.CalibrationNorm == null && diff.NewTestEquipment.CalibrationNorm != null)
                || (!diff.OldTestEquipment.CalibrationNorm?.Equals(diff.NewTestEquipment.CalibrationNorm) ?? false))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("TestEquipmentAttribute", "Calibration norm"),
                    OldValue = diff.OldTestEquipment.CalibrationNorm?.ToDefaultString(),
                    NewValue = diff.NewTestEquipment.CalibrationNorm?.ToDefaultString()
                };
            }

            if (!diff.OldTestEquipment.UseForCtl.Equals(diff.NewTestEquipment.UseForCtl))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("TestEquipmentAttribute", "UseForCtl"),
                    OldValue = GetTranslationForBool(diff.OldTestEquipment.UseForCtl),
                    NewValue = GetTranslationForBool(diff.NewTestEquipment.UseForCtl)
                };
            }

            if (!diff.OldTestEquipment.UseForRot.Equals(diff.NewTestEquipment.UseForRot))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("TestEquipmentAttribute", "UseForRot"),
                    OldValue = GetTranslationForBool(diff.OldTestEquipment.UseForRot),
                    NewValue = GetTranslationForBool(diff.NewTestEquipment.UseForRot)
                };
            }

            if (!diff.OldTestEquipment.TransferUser.Equals(diff.NewTestEquipment.TransferUser))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("TestEquipmentAttribute", "TransferUser"),
                    OldValue = GetTranslationForBool(diff.OldTestEquipment.TransferUser),
                    NewValue = GetTranslationForBool(diff.NewTestEquipment.TransferUser)
                };
            }

            if (!diff.OldTestEquipment.TransferAdapter.Equals(diff.NewTestEquipment.TransferAdapter))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("TestEquipmentAttribute", "TransferAdapter"),
                    OldValue = GetTranslationForBool(diff.OldTestEquipment.TransferAdapter),
                    NewValue = GetTranslationForBool(diff.NewTestEquipment.TransferAdapter)
                };
            }

            if (!diff.OldTestEquipment.TransferTransducer.Equals(diff.NewTestEquipment.TransferTransducer))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("TestEquipmentAttribute", "TransferTransducer"),
                    OldValue = GetTranslationForBool(diff.OldTestEquipment.TransferTransducer),
                    NewValue = GetTranslationForBool(diff.NewTestEquipment.TransferTransducer)
                };
            }

            if (!diff.OldTestEquipment.TransferAttributes.Equals(diff.NewTestEquipment.TransferAttributes))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("TestEquipmentAttribute", "TransferAttributes"),
                    OldValue = GetTranslationForBool(diff.OldTestEquipment.TransferAttributes),
                    NewValue = GetTranslationForBool(diff.NewTestEquipment.TransferAttributes)
                };
            }

            if (!diff.OldTestEquipment.TransferLocationPictures.Equals(diff.NewTestEquipment.TransferLocationPictures))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("TestEquipmentAttribute", "TransferLocationPictures"),
                    OldValue = GetTranslationForBool(diff.OldTestEquipment.TransferLocationPictures),
                    NewValue = GetTranslationForBool(diff.NewTestEquipment.TransferLocationPictures)
                };
            }

            if (!diff.OldTestEquipment.TransferNewLimits.Equals(diff.NewTestEquipment.TransferNewLimits))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("TestEquipmentAttribute", "TransferNewLimits"),
                    OldValue = GetTranslationForBool(diff.OldTestEquipment.TransferNewLimits),
                    NewValue = GetTranslationForBool(diff.NewTestEquipment.TransferNewLimits)
                };
            }

            if (!diff.OldTestEquipment.TransferCurves.Equals(diff.NewTestEquipment.TransferCurves))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("TestEquipmentAttribute", "TransferCurves"),
                    OldValue = _localization.Strings.GetParticularString("TestEquipmentView", "TransferCurves " + diff.OldTestEquipment.TransferCurves),
                    NewValue = _localization.Strings.GetParticularString("TestEquipmentView", "TransferCurves " + diff.NewTestEquipment.TransferCurves)
                };
            }

            if (!diff.OldTestEquipment.AskForIdent.Equals(diff.NewTestEquipment.AskForIdent))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("TestEquipmentAttribute", "AskForIdent"),
                    OldValue = _localization.Strings.GetParticularString("TestEquipmentView", "AskForIdent " + diff.OldTestEquipment.AskForIdent),
                    NewValue = _localization.Strings.GetParticularString("TestEquipmentView", "AskForIdent " + diff.NewTestEquipment.AskForIdent)
                };
            }

            if (!diff.OldTestEquipment.AskForSign.Equals(diff.NewTestEquipment.AskForSign))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("TestEquipmentAttribute", "AskForSign"),
                    OldValue = GetTranslationForBool(diff.OldTestEquipment.AskForSign),
                    NewValue = GetTranslationForBool(diff.NewTestEquipment.AskForSign)
                };
            }

            if (!diff.OldTestEquipment.UseErrorCodes.Equals(diff.NewTestEquipment.UseErrorCodes))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("TestEquipmentAttribute", "UseErrorCodes"),
                    OldValue = GetTranslationForBool(diff.OldTestEquipment.UseErrorCodes),
                    NewValue = GetTranslationForBool(diff.NewTestEquipment.UseErrorCodes)
                };
            }

            if (!diff.OldTestEquipment.DoLoseCheck.Equals(diff.NewTestEquipment.DoLoseCheck))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("TestEquipmentAttribute", "DoLoseCheck"),
                    OldValue = GetTranslationForBool(diff.OldTestEquipment.DoLoseCheck),
                    NewValue = GetTranslationForBool(diff.NewTestEquipment.DoLoseCheck)
                };
            }

            if (!diff.OldTestEquipment.CanDeleteMeasurements.Equals(diff.NewTestEquipment.CanDeleteMeasurements))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("TestEquipmentAttribute", "CanDeleteMeasurements"),
                    OldValue = GetTranslationForBool(diff.OldTestEquipment.CanDeleteMeasurements),
                    NewValue = GetTranslationForBool(diff.NewTestEquipment.CanDeleteMeasurements)
                };
            }

            if (!diff.OldTestEquipment.ConfirmMeasurements.Equals(diff.NewTestEquipment.ConfirmMeasurements))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("TestEquipmentAttribute", "ConfirmMeasurements"),
                    OldValue = _localization.Strings.GetParticularString("TestEquipmentView", "ConfirmMeasurements " + diff.OldTestEquipment.ConfirmMeasurements),
                    NewValue = _localization.Strings.GetParticularString("TestEquipmentView", "ConfirmMeasurements " + diff.NewTestEquipment.ConfirmMeasurements)
                };
            }

            if (!diff.OldTestEquipment.CanUseQstStandard.Equals(diff.NewTestEquipment.CanUseQstStandard))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("TestEquipmentAttribute", "CanUseQstStandard"),
                    OldValue = GetTranslationForBool(diff.OldTestEquipment.CanUseQstStandard),
                    NewValue = GetTranslationForBool(diff.NewTestEquipment.CanUseQstStandard)
                };
            }
        }

        private MessageBoxResult? ShowTestEquipmentModelChangesDialog(TestEquipmentModelDiff diff)
        {
            var changes = GetChangesFromTestEquipmentModelDiff(diff).ToList();

            if (changes.Count == 0)
            {
                return null;
            }

            var args = new VerifyChangesEventArgs(changes);
            RequestVerifyChangesView?.Invoke(this, args);
            diff.Comment = new HistoryComment(args.Comment);
            return args.Result;
        }

        private IEnumerable<SingleValueChangeModel> GetChangesFromTestEquipmentModelDiff(TestEquipmentModelDiff diff)
        {
            var entity = diff.NewTestEquipmentModel.TestEquipmentModelName.ToDefaultString();

            if (!diff.OldTestEquipmentModel.TestEquipmentModelName.Equals(diff.NewTestEquipmentModel.TestEquipmentModelName))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("TestEquipmentModelAttribute", "Name"),
                    OldValue = diff.OldTestEquipmentModel.TestEquipmentModelName.ToDefaultString(),
                    NewValue = diff.NewTestEquipmentModel.TestEquipmentModelName.ToDefaultString()
                };
            }

            if (!diff.OldTestEquipmentModel.DataGateVersion.Equals(diff.NewTestEquipmentModel.DataGateVersion))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("TestEquipmentModelAttribute", "DataGate Version"),
                    OldValue = diff.OldTestEquipmentModel.DataGateVersion.DataGateVersionsString,
                    NewValue = diff.NewTestEquipmentModel.DataGateVersion.DataGateVersionsString
                };
            }

            if (!diff.OldTestEquipmentModel.UseForCtl.Equals(diff.NewTestEquipmentModel.UseForCtl))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("TestEquipmentModelAttribute", "Use for process"),
                    OldValue = GetTranslationForBool(diff.OldTestEquipmentModel.UseForCtl),
                    NewValue = GetTranslationForBool(diff.NewTestEquipmentModel.UseForCtl)
                };
            }

            if (!diff.OldTestEquipmentModel.UseForRot.Equals(diff.NewTestEquipmentModel.UseForRot))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("TestEquipmentModelAttribute", "Use for rotating"),
                    OldValue = GetTranslationForBool(diff.OldTestEquipmentModel.UseForRot),
                    NewValue = GetTranslationForBool(diff.NewTestEquipmentModel.UseForRot)
                };
            }

            if (!diff.OldTestEquipmentModel.Type.Equals(diff.NewTestEquipmentModel.Type))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("TestEquipmentModelAttribute", "TestEquipmentType"),
                    OldValue = TestEquipmentTypeModel.GetTranslationForTestEquipmentType(diff.OldTestEquipmentModel.Type, _localization),
                    NewValue = TestEquipmentTypeModel.GetTranslationForTestEquipmentType(diff.NewTestEquipmentModel.Type, _localization)
                };
            }

            if (!diff.OldTestEquipmentModel.TransferUser.Equals(diff.NewTestEquipmentModel.TransferUser))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("TestEquipmentModelAttribute", "TransferUser"),
                    OldValue = GetTranslationForBool(diff.OldTestEquipmentModel.TransferUser),
                    NewValue = GetTranslationForBool(diff.NewTestEquipmentModel.TransferUser)
                };
            }

            if (!diff.OldTestEquipmentModel.TransferAdapter.Equals(diff.NewTestEquipmentModel.TransferAdapter))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("TestEquipmentModelAttribute", "TransferAdapter"),
                    OldValue = GetTranslationForBool(diff.OldTestEquipmentModel.TransferAdapter),
                    NewValue = GetTranslationForBool(diff.NewTestEquipmentModel.TransferAdapter)
                };
            }

            if (!diff.OldTestEquipmentModel.TransferTransducer.Equals(diff.NewTestEquipmentModel.TransferTransducer))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("TestEquipmentModelAttribute", "TransferTransducer"),
                    OldValue = GetTranslationForBool(diff.OldTestEquipmentModel.TransferTransducer),
                    NewValue = GetTranslationForBool(diff.NewTestEquipmentModel.TransferTransducer)
                };
            }

            if (!diff.OldTestEquipmentModel.TransferAttributes.Equals(diff.NewTestEquipmentModel.TransferAttributes))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("TestEquipmentModelAttribute", "TransferAttributes"),
                    OldValue = GetTranslationForBool(diff.OldTestEquipmentModel.TransferAttributes),
                    NewValue = GetTranslationForBool(diff.NewTestEquipmentModel.TransferAttributes)
                };
            }

            if (!diff.OldTestEquipmentModel.TransferLocationPictures.Equals(diff.NewTestEquipmentModel.TransferLocationPictures))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("TestEquipmentModelAttribute", "TransferLocationPictures"),
                    OldValue = GetTranslationForBool(diff.OldTestEquipmentModel.TransferLocationPictures),
                    NewValue = GetTranslationForBool(diff.NewTestEquipmentModel.TransferLocationPictures)
                };
            }

            if (!diff.OldTestEquipmentModel.TransferNewLimits.Equals(diff.NewTestEquipmentModel.TransferNewLimits))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("TestEquipmentModelAttribute", "TransferNewLimits"),
                    OldValue = GetTranslationForBool(diff.OldTestEquipmentModel.TransferNewLimits),
                    NewValue = GetTranslationForBool(diff.NewTestEquipmentModel.TransferNewLimits)
                };
            }

            if (!diff.OldTestEquipmentModel.TransferCurves.Equals(diff.NewTestEquipmentModel.TransferCurves))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("TestEquipmentModelAttribute", "TransferCurves"),
                    OldValue = GetTranslationForBool(diff.OldTestEquipmentModel.TransferCurves),
                    NewValue = GetTranslationForBool(diff.NewTestEquipmentModel.TransferCurves)
                };
            }

            if (!diff.OldTestEquipmentModel.AskForIdent.Equals(diff.NewTestEquipmentModel.AskForIdent))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("TestEquipmentModelAttribute", "AskForIdent"),
                    OldValue = GetTranslationForBool(diff.OldTestEquipmentModel.AskForIdent),
                    NewValue = GetTranslationForBool(diff.NewTestEquipmentModel.AskForIdent)
                };
            }

            if (!diff.OldTestEquipmentModel.AskForSign.Equals(diff.NewTestEquipmentModel.AskForSign))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("TestEquipmentModelAttribute", "AskForSign"),
                    OldValue = GetTranslationForBool(diff.OldTestEquipmentModel.AskForSign),
                    NewValue = GetTranslationForBool(diff.NewTestEquipmentModel.AskForSign)
                };
            }

            if (!diff.OldTestEquipmentModel.UseErrorCodes.Equals(diff.NewTestEquipmentModel.UseErrorCodes))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("TestEquipmentModelAttribute", "UseErrorCodes"),
                    OldValue = GetTranslationForBool(diff.OldTestEquipmentModel.UseErrorCodes),
                    NewValue = GetTranslationForBool(diff.NewTestEquipmentModel.UseErrorCodes)
                };
            }

            if (!diff.OldTestEquipmentModel.DoLoseCheck.Equals(diff.NewTestEquipmentModel.DoLoseCheck))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("TestEquipmentModelAttribute", "DoLoseCheck"),
                    OldValue = GetTranslationForBool(diff.OldTestEquipmentModel.DoLoseCheck),
                    NewValue = GetTranslationForBool(diff.NewTestEquipmentModel.DoLoseCheck)
                };
            }

            if (!diff.OldTestEquipmentModel.CanDeleteMeasurements.Equals(diff.NewTestEquipmentModel.CanDeleteMeasurements))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("TestEquipmentModelAttribute", "CanDeleteMeasurements"),
                    OldValue = GetTranslationForBool(diff.OldTestEquipmentModel.CanDeleteMeasurements),
                    NewValue = GetTranslationForBool(diff.NewTestEquipmentModel.CanDeleteMeasurements)
                };
            }

            if (!diff.OldTestEquipmentModel.ConfirmMeasurements.Equals(diff.NewTestEquipmentModel.ConfirmMeasurements))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("TestEquipmentModelAttribute", "ConfirmMeasurements"),
                    OldValue = GetTranslationForBool(diff.OldTestEquipmentModel.ConfirmMeasurements),
                    NewValue = GetTranslationForBool(diff.NewTestEquipmentModel.ConfirmMeasurements)
                };
            }

            if (!diff.OldTestEquipmentModel.CanUseQstStandard.Equals(diff.NewTestEquipmentModel.CanUseQstStandard))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("TestEquipmentModelAttribute", "CanUseQstStandard"),
                    OldValue = GetTranslationForBool(diff.OldTestEquipmentModel.CanUseQstStandard),
                    NewValue = GetTranslationForBool(diff.NewTestEquipmentModel.CanUseQstStandard)
                };
            }

            if (!diff.OldTestEquipmentModel.DriverProgramPath.Equals(diff.NewTestEquipmentModel.DriverProgramPath))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("TestEquipmentModelAttribute", "Path to test equipment driver"),
                    OldValue = diff.OldTestEquipmentModel.DriverProgramPath.ToDefaultString(),
                    NewValue = diff.NewTestEquipmentModel.DriverProgramPath.ToDefaultString()
                };
            }
            if (!diff.OldTestEquipmentModel.StatusFilePath.Equals(diff.NewTestEquipmentModel.StatusFilePath))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("TestEquipmentModelAttribute", "Path to status file"),
                    OldValue = diff.OldTestEquipmentModel.StatusFilePath.ToDefaultString(),
                    NewValue = diff.NewTestEquipmentModel.StatusFilePath.ToDefaultString()
                };
            }

            if (!diff.OldTestEquipmentModel.CommunicationFilePath.Equals(diff.NewTestEquipmentModel.CommunicationFilePath))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("TestEquipmentModelAttribute", "Path to qst to test equipment file"),
                    OldValue = diff.OldTestEquipmentModel.CommunicationFilePath.ToDefaultString(),
                    NewValue = diff.NewTestEquipmentModel.CommunicationFilePath.ToDefaultString()
                };
            }

            if (!diff.OldTestEquipmentModel.ResultFilePath.Equals(diff.NewTestEquipmentModel.ResultFilePath))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("TestEquipmentModelAttribute", "Path to test equipment to qst file"),
                    OldValue = diff.OldTestEquipmentModel.ResultFilePath.ToDefaultString(),
                    NewValue = diff.NewTestEquipmentModel.ResultFilePath.ToDefaultString()
                };
            }
        }

        private string GetTranslationForBool(bool value)
        {
            return _localization.Strings.GetString(value ? "Yes" : "No");
        }

        public bool IsTestEquipmentModelEnabled => _sessionInformation.IsCspUser();

        public bool CanClose()
        {
            if (SelectedTestEquipment != null)
            {
                if (!_testEquipmentValidator.Validate(SelectedTestEquipment?.Entity))
                {
                    var continueEditing = true;
                    MessageBoxRequest?.Invoke(this, new MessageBoxEventArgs(r =>
                        {
                            if (r != MessageBoxResult.No)
                                return;

                            _testEquipmentInterface.SelectedTestEquipment = null;
                            continueEditing = false;
                        },
                        _localization.Strings.GetParticularString("TestEquipmentViewModel",
                            "The test equipment is not valid, do you want to continue editing? (If not, the test equipment is reseted to the last saved value)"),
                        _localization.Strings.GetParticularString("TestEquipmentViewModel", "Test equipment not valid"),
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Error));
                    return !continueEditing;
                }

                if (_testEquipmentInterface.SelectedTestEquipment != null &&
                    _testEquipmentInterface.SelectedTestEquipmentWithoutChanges != null &&
                    !_testEquipmentInterface.SelectedTestEquipment.EqualsByContent(_testEquipmentInterface.SelectedTestEquipmentWithoutChanges))
                {

                    var diff = new TestEquipmentDiff(
                        _testEquipmentInterface.SelectedTestEquipmentWithoutChanges?.Entity,
                        SelectedTestEquipment?.Entity, null);
                    
                    var result = ShowTestEquipmentChangesDialog(diff);
                    if (result == null)
                    {
                        return true;
                    }

                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            _startUp.RaiseShowLoadingControl(true);
                            _testEquipmentUseCase.UpdateTestEquipment(diff, this);
                            _testEquipmentInterface.SelectedTestEquipment = null;
                            return true;
                        case MessageBoxResult.No:
                            _testEquipmentInterface.SelectedTestEquipment = null;
                            return true;
                        case MessageBoxResult.Cancel:
                            return false;
                    }
                }

                _testEquipmentInterface.SelectedTestEquipment = null;
                return true;
            }

            if (SelectedTestEquipmentModel != null)
            {
                if (!_testEquipmentValidator.Validate(SelectedTestEquipmentModel?.Entity))
                {
                    var continueEditing = true;
                    MessageBoxRequest?.Invoke(this, new MessageBoxEventArgs(r =>
                    {
                        if (r != MessageBoxResult.No)
                            return;

                        _testEquipmentInterface.SelectedTestEquipmentModel = null;
                        continueEditing = false;
                    },
                        _localization.Strings.GetParticularString("TestEquipmentViewModel",
                            "The test equipment model is not valid, do you want to continue editing? (If not, the test equipment model is reseted to the last saved value)"),
                        _localization.Strings.GetParticularString("TestEquipmentViewModel", "Test equipment model not valid"),
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Error));
                    return !continueEditing;
                }

                if (_testEquipmentInterface.SelectedTestEquipmentModel != null &&
                    _testEquipmentInterface.SelectedTestEquipmentModelWithoutChanges != null &&
                    !_testEquipmentInterface.SelectedTestEquipmentModel.EqualsByContent(_testEquipmentInterface.SelectedTestEquipmentModelWithoutChanges))
                {
                    var diff = new TestEquipmentModelDiff(
                        _testEquipmentInterface.SelectedTestEquipmentModelWithoutChanges?.Entity,
                        SelectedTestEquipmentModel?.Entity, null);

                    var result = ShowTestEquipmentModelChangesDialog(diff);
                    if (result == null)
                    {
                        return true;
                    }

                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            _startUp.RaiseShowLoadingControl(true);
                            _testEquipmentUseCase.UpdateTestEquipmentModel(diff, this);
                            _testEquipmentInterface.SelectedTestEquipmentModel = null;
                            return true;
                        case MessageBoxResult.No:
                            _testEquipmentInterface.SelectedTestEquipmentModel = null;
                            return true;
                        case MessageBoxResult.Cancel:
                            return false;
                    }
                }

                _testEquipmentInterface.SelectedTestEquipmentModel = null;
                return true;
            }

            return true;
        }
    }
}
