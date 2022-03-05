using Core.Entities;
using Core.UseCases;
using FrameworksAndDrivers.Gui.Wpf.EventArgs;
using FrameworksAndDrivers.Gui.Wpf.Interfaces;
using FrameworksAndDrivers.Gui.Wpf.Model;
using FrameworksAndDrivers.Gui.Wpf.View;
using InterfaceAdapters;
using InterfaceAdapters.Localization;
using InterfaceAdapters.Models;
using log4net;
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
using Client.UseCases.UseCases;
using Core.PhysicalValueTypes;

namespace FrameworksAndDrivers.Gui.Wpf.ViewModel
{
    public class ProcessControlViewModel : 
        BindableBase, 
        ILocationGui, 
        ICanClose, 
        IProcessControlErrorGui, 
        ITestLevelSetErrorHandler, 
        IProcessControlSaveGuiShower,
        IExtensionErrorGui,
        IDisposable
    {
        public ProcessControlViewModel(IProcessControlUseCase useCase, ILocationUseCase locationUseCase,
            IStartUp startUp, ILocalizationWrapper localization, IProcessControlInterface processControlInterface,
            ITestLevelSetUseCase testLevelSetUseCase, ITestLevelSetInterface testLevelSetInterface, 
            IProcessControlConditionValidator processControlConditionValidator,
            IExtensionInterface extensionInterface, IExtensionUseCase extensionUseCase)
        {
            _processControlUseCase = useCase;
            _locationUseCase = locationUseCase;
            _startUp = startUp;
            _localization = localization;
            _processControlInterface = processControlInterface;
            _testLevelSetUseCase = testLevelSetUseCase;
            _testLevelSetInterface = testLevelSetInterface;
            _processControlConditionValidator = processControlConditionValidator;
            _extensionInterface = extensionInterface;
            _extensionUseCase = extensionUseCase;
            _processControlInterface.ShowLoadingControlRequest += _processControlInterface_ShowLoadingControlRequest;
            WireViewModelToProcessControlInterface();

            LoadCommand = new RelayCommand(LoadExecute, LoadCanExecute);
            CreateProcessControlForLocationCommand = new RelayCommand(CreateProcessControlForLocationExecute, CreateProcessControlForLocationCanExecute);
            RemoveProcessControlForLocationCommand = new RelayCommand(RemoveProcessControlForLocationExecute, RemoveProcessControlForLocationCanExecute);

            SaveProcessControlCommand = new RelayCommand(SaveProcessControlExecute, SaveProcessControlCanExecute);
            LocationTree = new LocationTreeModel();
        }

        private void _processControlInterface_ShowLoadingControlRequest(object sender, bool e)
        {
            _startUp.RaiseShowLoadingControl(e);
        }

        private void WireViewModelToProcessControlInterface()
        {
            PropertyChangedEventManager.AddHandler(
                _processControlInterface,
                (s, e) =>
                {
                    RaisePropertyChanged(nameof(IsProcessControlConditionEnabled));
                    RaisePropertyChanged(nameof(SelectedProcessControl));
                    RaisePropertyChanged(nameof(SelectedProcessControlWithoutChanges));
                    RaisePropertyChanged(nameof(IsLocationParamEnabled));
                    RaisePropertyChanged(nameof(IsQstStandardMethodExpanded));
                    RaisePropertyChanged(nameof(TestLevelSet));
                    RaisePropertyChanged(nameof(SelectedExtension));
                    RaisePropertyChanged(nameof(AvailableTestLevelSetNumbers));
                    CommandManager.InvalidateRequerySuggested();
                },
                nameof(ProcessControlInterfaceAdapter.SelectedProcessControl));

            PropertyChangedEventManager.AddHandler(
                _processControlInterface,
                (s, e) =>
                {
                    RaisePropertyChanged(nameof(SelectedProcessControlWithoutChanges));
                },
                nameof(ProcessControlInterfaceAdapter.SelectedProcessControlWithoutChanges));

            PropertyChangedEventManager.AddHandler(_testLevelSetInterface,
                (s, e) => RaisePropertyChanged(nameof(AvailableTestLevelSets)),
                nameof(TestLevelSetInterfaceAdapter.TestLevelSets));

            PropertyChangedEventManager.AddHandler(_extensionInterface,
                (s, e) => RaisePropertyChanged(nameof(AvailableExtensions)),
                nameof(ExtensionInterfaceAdapter.Extensions));
        }

        #region Properties
        private IStartUp _startUp;
        private readonly ILocationUseCase _locationUseCase;
        private readonly IProcessControlUseCase _processControlUseCase;
        private ILocalizationWrapper _localization;
        private Dispatcher _guiDispatcher;

        private LocationTreeModel _locationTree;
        public LocationTreeModel LocationTree
        {
            get => _locationTree;
            set => Set(ref _locationTree, value);
        }

        public RelayCommand LoadCommand { get; private set; }
        public RelayCommand CreateProcessControlForLocationCommand { get; private set; }
        public RelayCommand RemoveProcessControlForLocationCommand { get; private set; }
        public RelayCommand SaveProcessControlCommand { get; private set; }
        public event EventHandler<VerifyChangesEventArgs> RequestVerifyChangesView;
        public event EventHandler<LocationModel> SelectionRequestLocation;

        private LocationModel _selectedLocation;
        public LocationModel SelectedLocation
        {
            get => _selectedLocation; 
            set
            {
                if (_selectedLocation?.EqualsById(value) ?? false)
                {
                    Set(ref _selectedLocation, value);
                    return;
                }

                if (!_processControlConditionValidator.Validate(SelectedProcessControl?.Entity))
                {
                    var continueEditing = true;
                    MessageBoxRequest?.Invoke(this, new MessageBoxEventArgs(r =>
                    {
                        if (r != MessageBoxResult.No)
                            return;

                        SelectedProcessControl.UpdateWith(SelectedProcessControlWithoutChanges?.Entity);
                        continueEditing = false;
                    },
                        _localization.Strings.GetParticularString("ProcessControlViewModel",
                            "The process control condition is not valid, do you want to continue editing? (If not, the process control condition is reseted to the last saved value)"),
                        _localization.Strings.GetParticularString("ProcessControlViewModel", "Process control condition not valid"),
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Error));

                    if (continueEditing)
                    {
                        SelectionRequestLocation?.Invoke(this, SelectedLocation);
                        return;
                    }
                }

                if (_processControlInterface.SelectedProcessControl != null &&
                    _processControlInterface.SelectedProcessControlWithoutChanges != null &&
                    !_processControlInterface.SelectedProcessControl.EqualsByContent(_processControlInterface.SelectedProcessControlWithoutChanges))
                {
                    var diff = new ProcessControlConditionDiff(null, null,
                        _processControlInterface.SelectedProcessControlWithoutChanges?.Entity,
                        SelectedProcessControl?.Entity);

                    var result = ShowProcessControlChangesDialog(new List<ProcessControlConditionDiff>() { diff });
                    if (result != null)
                    {
                        switch (result)
                        {
                            case MessageBoxResult.Yes:
                                _startUp.RaiseShowLoadingControl(true);
                                _processControlUseCase.UpdateProcessControlCondition(new List<ProcessControlConditionDiff>() { diff }, this);
                                break;
                            case MessageBoxResult.No:
                                SelectedProcessControl.UpdateWith(SelectedProcessControlWithoutChanges.Entity);
                                break;
                            case MessageBoxResult.Cancel:
                                SelectionRequestLocation?.Invoke(this, SelectedLocation);
                                return;
                        }
                    }
                }

                Set(ref _selectedLocation, value);
                if (value != null)
                {
                    _processControlUseCase.LoadProcessControlConditionForLocation(_selectedLocation.Entity, this);
                }

                CommandManager.InvalidateRequerySuggested();
                RaisePropertyChanged();
            }
        }


        public ProcessControlConditionHumbleModel SelectedProcessControl
        {
            get => _processControlInterface.SelectedProcessControl;
            set
            {
                _processControlInterface.SelectedProcessControl = value;
                RaisePropertyChanged();
            }
        }


        public ExtensionModel SelectedExtension
        {
            
            get => SelectedProcessControl?.Entity?.ProcessControlTech?.Extension == null
                ? null
                : ExtensionModel.GetModelFor(SelectedProcessControl.Entity.ProcessControlTech.Extension, _localization);
           
            set
            {
                if (SelectedProcessControl.Entity?.ProcessControlTech != null)
                {
                    SelectedProcessControl.Entity.ProcessControlTech.Extension = value?.Entity;
                }
                    
                RaisePropertyChanged();
            }
        }

        public ProcessControlConditionHumbleModel SelectedProcessControlWithoutChanges =>
            _processControlInterface.SelectedProcessControlWithoutChanges;

        public TestLevelSetModel TestLevelSet
        {
            get => SelectedProcessControl?.Entity?.TestLevelSet == null
                ? null
                : TestLevelSetModel.GetModelFor(SelectedProcessControl.Entity.TestLevelSet);

            set
            {
                if (SelectedProcessControl.Entity != null)
                {
                    SelectedProcessControl.Entity.TestLevelSet = value?.Entity;
                    SelectedProcessControl.Entity.TestLevelNumber = 1;
                }
              
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(AvailableTestLevelSetNumbers));
            }
        }

        private readonly ObservableCollection<int> _availableTestLevelSetNumbers = new ObservableCollection<int>();
        public ObservableCollection<int> AvailableTestLevelSetNumbers
        {
            get
            {
                var oldTestLevelNumber = SelectedProcessControl?.TestLevelNumber;
                _availableTestLevelSetNumbers.Clear();
                if (SelectedProcessControl?.TestLevelSet != null)
                {
                    _availableTestLevelSetNumbers.Add(1);
                    if (SelectedProcessControl.TestLevelSet.IsActive2)
                    {
                        _availableTestLevelSetNumbers.Add(2);
                    }
                    if (SelectedProcessControl.TestLevelSet.IsActive3)
                    {
                        _availableTestLevelSetNumbers.Add(3);
                    }
                }

                if (oldTestLevelNumber != null)
                {
                    SelectedProcessControl.TestLevelNumber = oldTestLevelNumber.Value;
                }
                
                return _availableTestLevelSetNumbers;
            }
        }

        public bool IsProcessControlConditionEnabled => SelectedProcessControl != null;
        public bool IsLocationParamEnabled => SelectedLocation != null;

        private bool _isQstStandardMethodExpanded = true;
        public bool IsQstStandardMethodExpanded
        {
            get
            {
                if (SelectedProcessControl?.QstProcessControlTechHumbleModel != null)
                {
                    return _isQstStandardMethodExpanded;
                }

                return false;
            } 
            set
            {
                if (SelectedProcessControl?.QstProcessControlTechHumbleModel != null)
                {
                    _isQstStandardMethodExpanded = value;
                }
                else
                {
                    _isQstStandardMethodExpanded = false;
                }
                RaisePropertyChanged();
            }
        } 

        public ObservableCollection<TestLevelSetModel> AvailableTestLevelSets => _testLevelSetInterface.TestLevelSets;
        public ObservableCollection<ExtensionModel> AvailableExtensions => _extensionInterface.Extensions;


        private static readonly ILog Log = LogManager.GetLogger(typeof(ProcessControlUseCase));

        private readonly IProcessControlInterface _processControlInterface;
        private readonly ITestLevelSetUseCase _testLevelSetUseCase;
        private readonly ITestLevelSetInterface _testLevelSetInterface;
        private readonly IProcessControlConditionValidator _processControlConditionValidator;
        private readonly IExtensionInterface _extensionInterface;
        private readonly IExtensionUseCase _extensionUseCase;

        #endregion

        #region Events
        public event EventHandler<LocationTreeModel> InitializeLocationTreeRequest;
        public event EventHandler<IAssistentView> ShowDialogRequest;
        public event EventHandler<MessageBoxEventArgs> MessageBoxRequest;
        public event EventHandler<VerifyChangesEventArgs> RequestChangesVerification;
        #endregion

        public void SetDispatcher(Dispatcher dispatcher)
        {
            _guiDispatcher = dispatcher;
            _extensionInterface.SetDispatcher(dispatcher);
        }

        #region Interface-Implementations

        public bool CanClose()
        {
            if (SelectedProcessControl != null)
            {
                if (!_processControlConditionValidator.Validate(SelectedProcessControl?.Entity))
                {
                    var continueEditing = true;
                    MessageBoxRequest?.Invoke(this, new MessageBoxEventArgs(r =>
                    {
                        if (r != MessageBoxResult.No)
                            return;

                        _processControlInterface.SelectedProcessControl = null;
                        continueEditing = false;
                    },
                        _localization.Strings.GetParticularString("ProcessControlViewModel",
                            "The process control condition is not valid, do you want to continue editing? (If not, the process control condition is reseted to the last saved value)"),
                        _localization.Strings.GetParticularString("ProcessControlViewModel", "Process control not valid"),
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Error));
                    return !continueEditing;
                }

                if (_processControlInterface.SelectedProcessControl != null &&
                    _processControlInterface.SelectedProcessControlWithoutChanges != null &&
                    !_processControlInterface.SelectedProcessControl.EqualsByContent(_processControlInterface.SelectedProcessControlWithoutChanges))
                {

                    var diff = new ProcessControlConditionDiff(null, null,
                        _processControlInterface.SelectedProcessControlWithoutChanges?.Entity,
                        SelectedProcessControl?.Entity);

                    var result = ShowProcessControlChangesDialog(new List<ProcessControlConditionDiff>() { diff });
                    if (result == null)
                    {
                        return true;
                    }

                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            _startUp.RaiseShowLoadingControl(true);
                            _processControlUseCase.UpdateProcessControlCondition(new List<ProcessControlConditionDiff>() { diff }, this);
                            _processControlInterface.SelectedProcessControl = null;
                            return true;
                        case MessageBoxResult.No:
                            _processControlInterface.SelectedProcessControl = null;
                            return true;
                        case MessageBoxResult.Cancel:
                            return false;
                    }
                }

                _processControlInterface.SelectedProcessControl = null;
                return true;
            }

            return true;
        }
        private bool LoadCanExecute(object arg) { return true; }

        private void LoadExecute(object obj)
        {
            _locationUseCase.LoadTree(this);
            _extensionUseCase.ShowExtensions(this);
            _testLevelSetUseCase.LoadTestLevelSets(this);
        }

        private bool CreateProcessControlForLocationCanExecute(object arg)
        {
            return SelectedLocation != null && SelectedProcessControl == null;
        }

        private void CreateProcessControlForLocationExecute(object obj)
        {
            var assistant = _startUp.OpenAddProcessControlAssistant(SelectedLocation.Entity);
            assistant.EndOfAssistent += (s, e) =>
            {
                var processControlCondition =
                    (ProcessControlCondition) (assistant.DataContext as AssistentViewModel)?.FillResultObject(
                        new ProcessControlCondition()
                        {
                            ProcessControlTech = new QstProcessControlTech()
                            {
                                Id = new ProcessControlTechId(0), 
                                ProcessControlConditionId = new ProcessControlConditionId(0),
                                MinimumTorqueMt = Torque.FromNm(0.5),
                                StartAngleMt = Torque.FromNm(0.5),
                                StartMeasurementMt = Torque.FromNm(0.5),
                                AlarmTorqueMt = Torque.FromNm(0),
                                StartAngleCountingPa = Torque.FromNm(0.5),
                                AlarmTorquePa = Torque.FromNm(0),
                                StartMeasurementPa = Torque.FromNm(0.5),
                                AngleForFurtherTurningPa = Angle.FromDegree(0),
                                TargetAnglePa = Angle.FromDegree(0),
                                AlarmAnglePa = Angle.FromDegree(0),
                                AlarmAngleMt = Angle.FromDegree(0),
                                AngleLimitMt = Angle.FromDegree(1),
                                StartMeasurementPeak = Torque.FromNm(0.5)
                            }
                        });
                if (processControlCondition == null)
                    return;
                processControlCondition.Location = SelectedLocation.Entity;
                processControlCondition.Id = new ProcessControlConditionId(0);
                _processControlUseCase.AddProcessControlCondition(processControlCondition, this);

            };
            assistant.Closed += (s, e) =>
            {
                _startUp.RaiseShowLoadingControl(false);
            };

            _startUp.RaiseShowLoadingControl(true);
            ShowDialogRequest?.Invoke(this, assistant);
        }

        private bool RemoveProcessControlForLocationCanExecute(object arg)
        {
            return SelectedProcessControl != null;
        }

        private void RemoveProcessControlForLocationExecute(object obj)
        {
            if (SelectedProcessControl == null)
            {
                return;
            }

            Action<MessageBoxResult> resultAction = (r) =>
            {
                if (r != MessageBoxResult.Yes)
                {
                    _startUp.RaiseShowLoadingControl(false);
                    return;
                }

                SelectedProcessControl.UpdateWith(_processControlInterface.SelectedProcessControlWithoutChanges?.Entity);
                _processControlUseCase.RemoveProcessControlCondition(SelectedProcessControl.Entity, this);
            };

            var args = new MessageBoxEventArgs(resultAction,
                _localization.Strings.GetString("Do you really want to remove this item?"),
                _localization.Strings.GetParticularString("Window Title", "Warning"),
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            _startUp.RaiseShowLoadingControl(true);
            MessageBoxRequest?.Invoke(this, args);
        }

        private bool SaveProcessControlCanExecute(object arg)
        {
            if (_processControlInterface.SelectedProcessControl == null)
            {
                return false;
            }
            
            if (!_processControlInterface.SelectedProcessControl.EqualsByContent(_processControlInterface.SelectedProcessControlWithoutChanges))
            {
                return _processControlConditionValidator.Validate(_processControlInterface.SelectedProcessControl?.Entity);
            }
           
            return false;
        }

        private void SaveProcessControlExecute(object obj)
        {
            if (_processControlInterface.SelectedProcessControl != null)
            {
                _startUp.RaiseShowLoadingControl(true);

                var diff = new ProcessControlConditionDiff(null,null,
                    _processControlInterface.SelectedProcessControlWithoutChanges.Entity,
                    _processControlInterface.SelectedProcessControl.Entity);

                _processControlUseCase.SaveProcessControlCondition(new List<ProcessControlConditionDiff>() { diff }, this, this);
            }
        }

        public void ShowLoadingLocationTreeFinished()
        {
            _startUp.RaiseShowLoadingControl(false);
        }

        public void ShowLocationTree(List<LocationDirectory> directories)
        {
            _guiDispatcher.Invoke(() =>
            {
                LocationTree = new LocationTreeModel();
                directories.ForEach(x => LocationTree.LocationDirectoryModels.Add(LocationDirectoryHumbleModel.GetModelFor(x, _locationUseCase)));
                InitializeLocationTreeRequest?.Invoke(this, null);
            });
        }

        public void ShowLocationTreeError()
        {
            _startUp.RaiseShowLoadingControl(false);
        }
        public void ShowLocation(Location location)
        {
            _guiDispatcher.Invoke(() =>
            {
                var locationModel = LocationModel.GetModelFor(location, _localization, _locationUseCase);
                LocationTree.LocationModels.Add(locationModel);
            });
        }


        #region not_used_yet
        public void AddLocation(Location location)
        {
            //Intentionally empty
        }

        public void AddLocationError()
        {
            //Intentionally empty
        }

        public void RemoveLocation(Location location)
        {
            //Intentionally empty
        }

        public void ShowRemoveLocationError()
        {
            //Intentionally empty
        }

        public void ShowPictureForLocation(Picture picture, LocationId locationId)
        {
            //Intentionally empty
        }

        public void ShowCommentForLocation(string comment, LocationId locationId)
        {
            //Intentionally empty
        }

        public void UpdateLocation(Location location)
        {
            //Intentionally empty
        }

        public void UpdateLocationError()
        {
            //Intentionally empty
        }

        public void LocationAlreadyExists()
        {
            //Intentionally empty
        }

        public void AddLocationDirectory(LocationDirectory locationDirectory)
        {
            //Intentionally empty
        }

        public void ShowAddLocationDirectoryError(string name)
        {
            //Intentionally empty
        }

        public void RemoveDirectory(LocationDirectoryId selectedDirectoryId)
        {
            //Intentionally empty
        }

        public void ShowRemoveDirectoryError()
        {
            //Intentionally empty
        }

        public void ChangeLocationParent(Location location, LocationDirectoryId newParentId)
        {
            //Intentionally empty
        }

        public void ChangeLocationParentError()
        {
            //Intentionally empty
        }

        public void ChangeLocationDirectoryParent(LocationDirectory directory, LocationDirectoryId newParentId)
        {
            //Intentionally empty
        }

        public void ChangeLocationDirectoryParentError()
        {
            //Intentionally empty
        }

        public void ShowChangeLocationToolAssignmentNotice()
        {
            //Intentionally empty
        }

        public void ShowChangeToolStatusDialog(Action onSuccess, List<LocationToolAssignment> locationToolAssignments)
        {
            //Intentionally empty
        }
        #endregion
        #endregion

        public void ShowProblemLoadingLocationProcessControlCondition()
        {
            var args = new MessageBoxEventArgs(r => { },
                _localization.Strings.GetParticularString("ProcessControlViewModel", "Some errors occurred while loading location process control condition"),
                _localization.Strings.GetString("Unknown Error!"),
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            MessageBoxRequest?.Invoke(this, args);
        }

        public void ShowTestLevelSetError()
        {
            var args = new MessageBoxEventArgs(r => { },
                _localization.Strings.GetParticularString("ProcessControlViewModel", "Some errors occurred while loading the test level sets"),
                _localization.Strings.GetParticularString("ProcessControlViewModel", "Error"),
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            MessageBoxRequest?.Invoke(this, args);
        }

        public void ShowProblemRemoveProcessControlCondition()
        {
            var args = new MessageBoxEventArgs(r => { },
                _localization.Strings.GetParticularString("ProcessControlViewModel", "Some errors occurred while removing location process control condition"),
                _localization.Strings.GetString("Unknown Error!"),
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            MessageBoxRequest?.Invoke(this, args);
        }

        public void ShowProblemSavingProcessControlCondition()
        {
            var args = new MessageBoxEventArgs(r => { },
                _localization.Strings.GetParticularString("ProcessControlViewModel", "Some errors occurred while saving location process control condition"),
                _localization.Strings.GetString("Unknown Error!"),
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            MessageBoxRequest?.Invoke(this, args);
        }

        public void SaveProcessControl(List<ProcessControlConditionDiff> diffs, Action saveAction)
        {
            var result = ShowProcessControlChangesDialog(diffs);
            if (result == null)
            {
                return;
            }

            _guiDispatcher.Invoke(() =>
            {
                if (result == MessageBoxResult.No)
                {
                    _processControlInterface.SelectedProcessControl = _processControlInterface.SelectedProcessControlWithoutChanges?.CopyDeep();
                    RaisePropertyChanged(null);
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

        private MessageBoxResult? ShowProcessControlChangesDialog(List<ProcessControlConditionDiff> diffs)
        {
            var changes = GetChangesFromProcessControlConditionDiff(diffs[0]).ToList();

            if (changes.Count == 0)
            {
                return null;
            }

            var args = new VerifyChangesEventArgs(changes);
            RequestVerifyChangesView?.Invoke(this, args);
            diffs.ForEach(x => x.Comment = new HistoryComment(args.Comment));
            return args.Result;
        }

        private IEnumerable<SingleValueChangeModel> GetChangesFromProcessControlConditionDiff(ProcessControlConditionDiff diff)
        {
            var entity = SelectedLocation?.Number + " - " + SelectedLocation?.Description;

            if (!diff.GetOldProcessControlCondition().LowerInterventionLimit.Equals(diff.GetNewProcessControlCondition().LowerInterventionLimit))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("ProcessControlConditionAttribute", "Lower intervention limit"),
                    OldValue = diff.GetOldProcessControlCondition().LowerInterventionLimit.Nm.ToString(),
                    NewValue = diff.GetNewProcessControlCondition().LowerInterventionLimit.Nm.ToString()
                };
            }

            if (!diff.GetOldProcessControlCondition().UpperInterventionLimit.Equals(diff.GetNewProcessControlCondition().UpperInterventionLimit))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("ProcessControlConditionAttribute", "Upper intervention limit"),
                    OldValue = diff.GetOldProcessControlCondition().UpperInterventionLimit.Nm.ToString(),
                    NewValue = diff.GetNewProcessControlCondition().UpperInterventionLimit.Nm.ToString()
                };
            }

            if (!diff.GetOldProcessControlCondition().LowerMeasuringLimit.Equals(diff.GetNewProcessControlCondition().LowerMeasuringLimit))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("ProcessControlConditionAttribute", "Lower measuring limit"),
                    OldValue = diff.GetOldProcessControlCondition().LowerMeasuringLimit.Nm.ToString(),
                    NewValue = diff.GetNewProcessControlCondition().LowerMeasuringLimit.Nm.ToString()
                };
            }

            if (!diff.GetOldProcessControlCondition().UpperMeasuringLimit.Equals(diff.GetNewProcessControlCondition().UpperMeasuringLimit))
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("ProcessControlConditionAttribute", "Upper measuring limit"),
                    OldValue = diff.GetOldProcessControlCondition().UpperMeasuringLimit.Nm.ToString(),
                    NewValue = diff.GetNewProcessControlCondition().UpperMeasuringLimit.Nm.ToString()
                };
            }

            if (diff.GetOldProcessControlCondition().TestLevelSet != null && diff.GetNewProcessControlCondition().TestLevelSet != null)
            {
                if (!diff.GetOldProcessControlCondition().TestLevelSet.Id.Equals(diff.GetNewProcessControlCondition().TestLevelSet.Id))
                {
                    yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                    {
                        AffectedEntity = entity,
                        ChangedAttribute = _localization.Strings.GetParticularString("ProcessControlConditionAttribute", "Test level set"),
                        OldValue = diff.GetOldProcessControlCondition().TestLevelSet.Name.ToDefaultString(),
                        NewValue = diff.GetNewProcessControlCondition().TestLevelSet.Name.ToDefaultString()
                    };
                }
            }

            if (diff.GetOldProcessControlCondition().TestLevelNumber != diff.GetNewProcessControlCondition().TestLevelNumber)
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("ProcessControlConditionAttribute", "Test level number"),
                    OldValue = diff.GetOldProcessControlCondition().TestLevelNumber.ToString(),
                    NewValue = diff.GetNewProcessControlCondition().TestLevelNumber.ToString()
                };
            }

            if (diff.GetOldProcessControlCondition().StartDate != diff.GetNewProcessControlCondition().StartDate)
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("ProcessControlConditionAttribute", "Start date"),
                    OldValue = diff.GetOldProcessControlCondition().StartDate == null ? "" : diff.GetOldProcessControlCondition().StartDate.Value.ToShortDateString(),
                    NewValue = diff.GetNewProcessControlCondition().StartDate == null ? "" : diff.GetNewProcessControlCondition().StartDate.Value.ToShortDateString()
                };
            }

            if (diff.GetOldProcessControlCondition().TestOperationActive != diff.GetNewProcessControlCondition().TestOperationActive)
            {
                yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("ProcessControlConditionAttribute", "Test operation active"),
                    OldValue = _localization.Strings.GetParticularString("ProcessControlConditionAttribute", diff.GetOldProcessControlCondition().TestOperationActive ? "Active" : "Not active"),
                    NewValue = _localization.Strings.GetParticularString("ProcessControlConditionAttribute", diff.GetNewProcessControlCondition().TestOperationActive ? "Active" : "Not active")
                };
            }


            var oldQstStandardTech = diff.GetOldProcessControlCondition()?.ProcessControlTech as QstProcessControlTech;
            var newQstStandardTech = diff.GetNewProcessControlCondition()?.ProcessControlTech as QstProcessControlTech;
            if (oldQstStandardTech != null && newQstStandardTech != null)
            {
                var qstStandardMethodsText = _localization.Strings.GetParticularString("ProcessControlTechnique", "QST standard methods");
                var testMethodMinimumTorque =
                    _localization.Strings.GetParticularString("ProcessControl test method", "QST_MT");
                var testMethodPeak =
                    _localization.Strings.GetParticularString("ProcessControl test method", "QST_PEAK");
                var testMethodPrevail =
                    _localization.Strings.GetParticularString("ProcessControl test method", "QST_PA");

                if (!oldQstStandardTech.TestMethod.Equals(newQstStandardTech.TestMethod))
                {
                    yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                    {
                        AffectedEntity = entity,
                        ChangedAttribute = _localization.Strings.GetParticularString("ProcessControl test method", "Method"),
                        OldValue = _localization.Strings.GetParticularString("ProcessControl test method", oldQstStandardTech.TestMethod.ToString()),
                        NewValue = _localization.Strings.GetParticularString("ProcessControl test method", newQstStandardTech.TestMethod.ToString()),
                    };
                }

                
                if (!oldQstStandardTech.MinimumTorqueMt?.Equals(newQstStandardTech.MinimumTorqueMt) ?? newQstStandardTech.MinimumTorqueMt == null)
                {
                    yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                    {
                        AffectedEntity = entity,
                        ChangedAttribute = qstStandardMethodsText + " - " + testMethodMinimumTorque + " - " +
                                           _localization.Strings.GetParticularString("QstProcessControlTechAttribute", "Minimum torque (Mmin)"),
                        OldValue = oldQstStandardTech.MinimumTorqueMt?.Nm.ToString(),
                        NewValue = newQstStandardTech.MinimumTorqueMt?.Nm.ToString()
                    };
                }

                if (!oldQstStandardTech.StartAngleMt?.Equals(newQstStandardTech.StartAngleMt) ?? newQstStandardTech.StartAngleMt == null)
                {
                    yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                    {
                        AffectedEntity = entity,
                        ChangedAttribute = qstStandardMethodsText + " - " + testMethodMinimumTorque + " - " +
                                           _localization.Strings.GetParticularString("QstProcessControlTechAttribute", "Start angle count (Ms)"),
                        OldValue = oldQstStandardTech.StartAngleMt?.Nm.ToString(),
                        NewValue = newQstStandardTech.StartAngleMt?.Nm.ToString()
                    };
                }

                if (!oldQstStandardTech.AngleLimitMt?.Equals(newQstStandardTech.AngleLimitMt) ?? newQstStandardTech.AngleLimitMt == null)
                {
                    yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                    {
                        AffectedEntity = entity,
                        ChangedAttribute = qstStandardMethodsText + " - " + testMethodMinimumTorque + " - " +
                                           _localization.Strings.GetParticularString("QstProcessControlTechAttribute", "Angle limit (Alim)"),
                        OldValue = oldQstStandardTech.AngleLimitMt?.Degree.ToString(),
                        NewValue = newQstStandardTech.AngleLimitMt?.Degree.ToString()
                    };
                }

                if (!oldQstStandardTech.StartMeasurementMt?.Equals(newQstStandardTech.StartMeasurementMt) ?? newQstStandardTech.StartMeasurementMt == null)
                {
                    yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                    {
                        AffectedEntity = entity,
                        ChangedAttribute = qstStandardMethodsText + " - " + testMethodMinimumTorque + " - " +
                                           _localization.Strings.GetParticularString("QstProcessControlTechAttribute", "Start measurement (Mstart)"),
                        OldValue = oldQstStandardTech.StartMeasurementMt?.Nm.ToString(),
                        NewValue = newQstStandardTech.StartMeasurementMt?.Nm.ToString()
                    };
                }

                if (!oldQstStandardTech.AlarmTorqueMt?.Equals(newQstStandardTech.AlarmTorqueMt) ?? newQstStandardTech.AlarmTorqueMt == null)
                {
                    yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                    {
                        AffectedEntity = entity,
                        ChangedAttribute = qstStandardMethodsText + " - " + testMethodMinimumTorque + " - " +
                                           _localization.Strings.GetParticularString("QstProcessControlTechAttribute", "Alarm limit - torque"),
                        OldValue = oldQstStandardTech.AlarmTorqueMt?.Nm.ToString(),
                        NewValue = newQstStandardTech.AlarmTorqueMt?.Nm.ToString()
                    };
                }

                if (!oldQstStandardTech.AlarmAngleMt?.Equals(newQstStandardTech.AlarmAngleMt) ?? newQstStandardTech.AlarmAngleMt == null)
                {
                    yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                    {
                        AffectedEntity = entity,
                        ChangedAttribute = qstStandardMethodsText + " - " + testMethodMinimumTorque + " - " +
                                           _localization.Strings.GetParticularString("QstProcessControlTechAttribute", "Alarm limit - angle"),
                        OldValue = oldQstStandardTech.AlarmAngleMt?.Degree.ToString(),
                        NewValue = newQstStandardTech.AlarmAngleMt?.Degree.ToString()
                    };
                }

                if (!oldQstStandardTech.StartMeasurementPeak?.Equals(newQstStandardTech.StartMeasurementPeak) ?? newQstStandardTech.StartMeasurementPeak == null)
                {
                    yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                    {
                        AffectedEntity = entity,
                        ChangedAttribute = qstStandardMethodsText + " - " + testMethodPeak + " - " +
                                           _localization.Strings.GetParticularString("QstProcessControlTechAttribute", "Start measurement (Mstart)"),
                        OldValue = oldQstStandardTech.StartMeasurementPeak?.Nm.ToString(),
                        NewValue = newQstStandardTech.StartMeasurementPeak?.Nm.ToString()
                    };
                }

                if (!oldQstStandardTech.StartAngleCountingPa?.Equals(newQstStandardTech.StartAngleCountingPa) ?? newQstStandardTech.StartAngleCountingPa == null)
                {
                    yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                    {
                        AffectedEntity = entity,
                        ChangedAttribute = qstStandardMethodsText + " - " + testMethodPrevail + " - " +
                                           _localization.Strings.GetParticularString("QstProcessControlTechAttribute", "Start angle count (Ms)"),
                        OldValue = oldQstStandardTech.StartAngleCountingPa?.Nm.ToString(),
                        NewValue = newQstStandardTech.StartAngleCountingPa?.Nm.ToString()
                    };
                }

                if (!oldQstStandardTech.AngleForFurtherTurningPa?.Equals(newQstStandardTech.AngleForFurtherTurningPa) ?? newQstStandardTech.AngleForFurtherTurningPa == null)
                {
                    yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                    {
                        AffectedEntity = entity,
                        ChangedAttribute = qstStandardMethodsText + " - " + testMethodPrevail + " - " +
                                           _localization.Strings.GetParticularString("QstProcessControlTechAttribute", "Angle for further turning (A1)"),
                        OldValue = oldQstStandardTech.AngleForFurtherTurningPa?.Degree.ToString(),
                        NewValue = newQstStandardTech.AngleForFurtherTurningPa?.Degree.ToString()
                    };
                }

                if (!oldQstStandardTech.TargetAnglePa?.Equals(newQstStandardTech.TargetAnglePa) ?? newQstStandardTech.TargetAnglePa == null)
                {
                    yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                    {
                        AffectedEntity = entity,
                        ChangedAttribute = qstStandardMethodsText + " - " + testMethodPrevail + " - " +
                                           _localization.Strings.GetParticularString("QstProcessControlTechAttribute", "Target angle (A2)"),
                        OldValue = oldQstStandardTech.TargetAnglePa?.Degree.ToString(),
                        NewValue = newQstStandardTech.TargetAnglePa?.Degree.ToString()
                    };
                }

                if (!oldQstStandardTech.StartMeasurementPa?.Equals(newQstStandardTech.StartMeasurementPa) ?? newQstStandardTech.StartMeasurementPa == null)
                {
                    yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                    {
                        AffectedEntity = entity,
                        ChangedAttribute = qstStandardMethodsText + " - " + testMethodPrevail + " - " +
                                           _localization.Strings.GetParticularString("QstProcessControlTechAttribute", "Start measurement (Mstart)"),
                        OldValue = oldQstStandardTech.StartMeasurementPa?.Nm.ToString(),
                        NewValue = newQstStandardTech.StartMeasurementPa?.Nm.ToString()
                    };
                }

                if (!oldQstStandardTech.AlarmTorquePa?.Equals(newQstStandardTech.AlarmTorquePa) ?? newQstStandardTech.AlarmTorquePa == null)
                {
                    yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                    {
                        AffectedEntity = entity,
                        ChangedAttribute = qstStandardMethodsText + " - " + testMethodPrevail + " - " +
                                           _localization.Strings.GetParticularString("QstProcessControlTechAttribute", "Alarm limit - torque"),
                        OldValue = oldQstStandardTech.AlarmTorquePa?.Nm.ToString(),
                        NewValue = newQstStandardTech.AlarmTorquePa?.Nm.ToString()
                    };
                }

                if (!oldQstStandardTech.AlarmAnglePa?.Equals(newQstStandardTech.AlarmAnglePa) ?? newQstStandardTech.AlarmAnglePa == null)
                {
                    yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                    {
                        AffectedEntity = entity,
                        ChangedAttribute = qstStandardMethodsText + " - " + testMethodPrevail + " - " +
                                           _localization.Strings.GetParticularString("QstProcessControlTechAttribute", "Alarm limit - angle"),
                        OldValue = oldQstStandardTech.AlarmAnglePa?.Degree.ToString(),
                        NewValue = newQstStandardTech.AlarmAnglePa?.Degree.ToString()
                    };
                }

                if (diff.GetOldProcessControlCondition().ProcessControlTech?.Extension != null && diff.GetNewProcessControlCondition().ProcessControlTech?.Extension != null)
                {
                    if (!diff.GetOldProcessControlCondition().ProcessControlTech.Extension.Id.Equals(diff.GetNewProcessControlCondition().ProcessControlTech.Extension.Id))
                    {
                        yield return new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                        {
                            AffectedEntity = entity,
                            ChangedAttribute = qstStandardMethodsText + " - " + _localization.Strings.GetParticularString("ProcessControlConditionAttribute", "Extension"),
                            OldValue = diff.GetOldProcessControlCondition().ProcessControlTech.Extension.Description,
                            NewValue = diff.GetNewProcessControlCondition().ProcessControlTech.Extension.Description
                        };
                    }
                }
            }
        }

        public void Dispose()
        {
            _processControlInterface.ShowLoadingControlRequest -= _processControlInterface_ShowLoadingControlRequest;
        }

        public void ShowErrorMessageLoadingExentsions()
        {
            var args = new MessageBoxEventArgs(r => { },
                _localization.Strings.GetParticularString("ExtensionViewModel", "Some errors occurred while loading extensions"),
                _localization.Strings.GetString("Unknown Error!"),
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            MessageBoxRequest?.Invoke(this, args);
        }

        public void ShowErrorMessageLoadingReferencedLocations()
        {
            //Intentiaonaly empty
        }

        public void ShowProblemSavingExtension()
        {
            //Intentiaonaly empty
        }

        public void ExtensionAlreadyExists()
        {
            //Intentiaonaly empty
        }

        public void ShowProblemRemoveExtension()
        {
            //Intentiaonaly empty
        }
    }
}
