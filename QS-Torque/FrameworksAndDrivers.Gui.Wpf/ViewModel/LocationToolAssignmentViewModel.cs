using Core.Diffs;
using Core.Entities;
using Core.Entities.ReferenceLink;
using Core.Enums;
using Core.PhysicalValueTypes;
using Core.UseCases;
using FrameworksAndDrivers.Gui.Wpf.EventArgs;
using FrameworksAndDrivers.Gui.Wpf.Interfaces;
using FrameworksAndDrivers.Gui.Wpf.Model;
using FrameworksAndDrivers.Threads;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using FrameworksAndDrivers.Gui.Wpf.Validator;
using ToolModel = Core.Entities.ToolModel;
using FrameworksAndDrivers.Gui.Wpf.View;
using System.ComponentModel;
using Core;
using InterfaceAdapters;
using InterfaceAdapters.Localization;
using InterfaceAdapters.Models;

namespace FrameworksAndDrivers.Gui.Wpf.ViewModel
{
    public class LocationToolAssignmentViewModel : BindableBase, ILocationToolAssignmentGui, ILocationGui, IToolGui, IToleranceClassGui, ICanClose, ITestLevelSetErrorHandler
    {
        private ILocationUseCase _locationUseCase;
        private IToolUseCase _toolUseCase;
        private ILocationToolAssignmentUseCase _locationToolAssignmentUseCase;
        private IToleranceClassUseCase _toleranceClassUseCase;
        private ITestLevelSetUseCase _testLevelSetUseCase;
        private ITestLevelSetInterface _testLevelSetInterface;
        private Dispatcher _guiDispatcher;
        private IStartUp _startUp;
        private ILocalizationWrapper _localization;
        private ILocationToolAssignmentDisplayFormatter _locationToolAssignmentDisplayFormatter;
        private ILocationDisplayFormatter _locationDisplayFormatter;
        private IToolDisplayFormatter _toolDisplayFormatter;
        private IThreadCreator _threadCreator;
        private ILocationToolAssignmentValidtor _locationToolAssignmentValidator;
        private int _loadToolAssignmentsForLocationCallCounter = 0;
        private bool _setFromLocationToolAssignment;

        public LocationTreeModel LocationTree { get; private set; }

        public event EventHandler InitializeLocationTreeRequest;
        public event EventHandler<IAssistentView> ShowDialogRequest;
        public event EventHandler<MessageBoxEventArgs> MessageBoxRequest;
        public event EventHandler<VerifyChangesEventArgs> RequestChangesVerification;
        public event EventHandler<LocationModel> LocationSelectionRequest;
        public event EventHandler<InterfaceAdapters.Models.ToolModel> ToolSelectionRequest; 

        public ObservableCollection<ToolModelModel> AllToolModelModels { get; private set; }
        public ObservableCollection<InterfaceAdapters.Models.ToolModel> AllToolModels { get; private set; }
        public ObservableCollection<LocationToolAssignmentModel> AssignmentsForSelectedLocation { get; private set; }
        public ObservableCollection<LocationControlledBy> LocationControlledBys { get; private set; }
        public ObservableCollection<ToleranceClassModel> ToleranceClassModels { get; private set; }
        public ObservableCollection<HelperTableItemModel<ToolUsage, string>> AvailableToolUsages { get; set; }

        public string SelectedLocationFormattedName => SelectedLocation?.Entity != null ? _locationDisplayFormatter.Format(SelectedLocation?.Entity) : "";

        public ObservableCollection<TestLevelSetModel> AvailableTestLevelSets => _testLevelSetInterface.TestLevelSets;

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
                if (!_locationToolAssignmentValidator.Validate(_selectedLocationToolAssignmentModel))
                {
                    bool continueEditing = ShowUnvalidLocationToolAssignmentMessage();
                    if (continueEditing)
                    {
                        LocationSelectionRequest?.Invoke(this, LocationTree.LocationModels.FirstOrDefault(x => x.EqualsById(_selectedLocationToolAssignmentModel.AssignedLocation)));
                        return;
                    }
                }
                else
                {
                    if (!_locationToolAssignmentWithoutChanges?.EqualsByContent(
                        _selectedLocationToolAssignmentModel) ?? false)
                    {
                        var diff = new LocationToolAssignmentDiff
                        {
                            OldLocationToolAssignment = _locationToolAssignmentWithoutChanges.Entity,
                            NewLocationToolAssignment = _selectedLocationToolAssignmentModel.Entity
                        };
                        var isNotCanceled = VerifyLocationToolAssignmentsDiff(diff);
                        if (!isNotCanceled)
                        {
                            LocationSelectionRequest?.Invoke(this, LocationTree.LocationModels.FirstOrDefault(x => x.EqualsById(_selectedLocationToolAssignmentModel.AssignedLocation)));
                            return;
                        }
                    }
                }
                Set(ref _selectedLocation, value);
                CommandManager.InvalidateRequerySuggested();

                if (value != null)
                {
                    _startUp.RaiseShowLoadingControl(true);
                    _loadToolAssignmentsForLocationCallCounter++;
                    _locationToolAssignmentUseCase.LoadUnusedToolUsagesForLocation(value.Entity.Id);
                    _locationToolAssignmentUseCase.LoadToolAssignmentsForLocation(value?.Entity);
                     SelectedLocationToolAssignmentModel = AssignmentsForSelectedLocation.FirstOrDefault(x =>
                        SelectedTool != null && SelectedLocation != null && x.AssignedTool.EqualsById(SelectedTool) &&
                        x.AssignedLocation.EqualsById(SelectedLocation));
                    RaisePropertyChanged(nameof(SelectedLocationFormattedName));
                }
                else
                {
                    AssignmentsForSelectedLocation.Clear();
                }
            }
        }

        public string SelectedToolFormattedName => SelectedTool?.Entity != null ? _toolDisplayFormatter.Format(SelectedTool?.Entity) : "";

        private InterfaceAdapters.Models.ToolModel _selectedTool;
        public InterfaceAdapters.Models.ToolModel SelectedTool
        {
            get => _selectedTool;
            set
            {
                if (_selectedTool?.EqualsById(value) ?? false)
                {
                    Set(ref _selectedTool, value);
                    return;
                }
                if (!_locationToolAssignmentValidator.Validate(_selectedLocationToolAssignmentModel))
                {
                    bool continueEditing = ShowUnvalidLocationToolAssignmentMessage();
                    if (continueEditing)
                    {
                        _threadCreator.Run(() =>
                        {
                            ToolSelectionRequest?.Invoke(this, AllToolModels.FirstOrDefault(x => x.EqualsById(_selectedLocationToolAssignmentModel?.AssignedTool)));
                        });
                        return;
                    }

                    if (_locationToolAssignmentWithoutChanges?.EqualsByContent(_selectedLocationToolAssignmentModel) ?? false)
                    {
                        var diff = new LocationToolAssignmentDiff
                        {
                            OldLocationToolAssignment = _locationToolAssignmentWithoutChanges.Entity,
                            NewLocationToolAssignment = _selectedLocationToolAssignmentModel.Entity
                        };
                        var isNotCanceled = VerifyLocationToolAssignmentsDiff(diff);
                        if (!isNotCanceled)
                        {
                            _threadCreator.Run(() =>
                            {
                                ToolSelectionRequest?.Invoke(this, AllToolModels.FirstOrDefault(x => x.EqualsById(_selectedLocationToolAssignmentModel?.AssignedTool)));
                            });
                            return;
                        }
                    }
                }
                
                Set(ref _selectedTool, value);

                if (value != null)
                {
                    _toolUseCase?.LoadCommentForTool(value.Entity);
                }

                CommandManager.InvalidateRequerySuggested();
                if (!_setFromLocationToolAssignment)
                {
                    SelectedLocationToolAssignmentModel = AssignmentsForSelectedLocation.FirstOrDefault(x =>
                        SelectedTool != null && SelectedLocation != null && x.AssignedTool.EqualsById(SelectedTool) &&
                        x.AssignedLocation.EqualsById(SelectedLocation));
                }
                RaisePropertyChanged(nameof(SelectedToolFormattedName));
                _setFromLocationToolAssignment = false;
            }
        }

        private LocationToolAssignmentModel _selectedLocationToolAssignmentModel;
        private LocationToolAssignmentModel _locationToolAssignmentWithoutChanges;

        private bool _allreadyChanging = false;
        public LocationToolAssignmentModel SelectedLocationToolAssignmentModel
        {
            get => _selectedLocationToolAssignmentModel;
            set
            {
                if (_selectedLocationToolAssignmentModel?.EqualsByContent(value) ?? false)
                {
                    return;
                }

                if (_allreadyChanging)
                {
                    return;
                }
                if (!_locationToolAssignmentValidator.Validate(_selectedLocationToolAssignmentModel))
                {
                    bool continueEditing = ShowUnvalidLocationToolAssignmentMessage();
                    if (continueEditing)
                    {
                        _threadCreator.Run(() =>
                        {
                            var lastSelectedLocationToolAssignment = _selectedLocationToolAssignmentModel;
                            _guiDispatcher.Invoke(() =>
                            {
                                Set(ref _selectedLocationToolAssignmentModel, value);
                                Set(ref _selectedLocationToolAssignmentModel, lastSelectedLocationToolAssignment);
                            });
                        });
                        return;
                    }
                }
                else
                {
                    if (!_locationToolAssignmentWithoutChanges?.EqualsByContent(_selectedLocationToolAssignmentModel) ??
                        false)
                    {
                        _allreadyChanging = true;
                        var diff = new LocationToolAssignmentDiff
                        {
                            OldLocationToolAssignment = _locationToolAssignmentWithoutChanges.Entity,
                            NewLocationToolAssignment = _selectedLocationToolAssignmentModel.Entity
                        };
                        var isNotCanceled = VerifyLocationToolAssignmentsDiff(diff);
                        _allreadyChanging = false;
                        if (!isNotCanceled)
                        {
                            _threadCreator.Run(() =>
                            {
                                var lastSelectedLocationToolAssignment = _selectedLocationToolAssignmentModel;
                                _guiDispatcher.Invoke(() =>
                                {
                                    Set(ref _selectedLocationToolAssignmentModel, value);
                                    Set(ref _selectedLocationToolAssignmentModel, lastSelectedLocationToolAssignment);
                                });
                            });
                            return;
                        }
                    }
                }

                var oldToolUsage = _selectedLocationToolAssignmentModel?.ToolUsage;
                if (value != null)
                {
                    if (!AvailableToolUsages.Any(x => x.EqualsById(value.ToolUsage)))
                    {
                        AvailableToolUsages.Add(value.ToolUsage);
                    }
                }
                Set(ref _selectedLocationToolAssignmentModel, value);
                if (oldToolUsage != null)
                {
                    AvailableToolUsages.Remove(oldToolUsage);
                }
                _locationToolAssignmentWithoutChanges = value?.CopyDeep();
                if (_selectedLocationToolAssignmentModel?.TestParameters != null)
                {
                    _selectedLocationToolAssignmentModel.TestParameters.PropertyChanged += TestParameterPropertyChanged;
                }
                var toolModel = AllToolModels.FirstOrDefault(x => x.EqualsById(value?.AssignedTool));
                if (toolModel is null)
                {
                    if (_selectedLocationToolAssignmentModel is null)
                    {
                        return;
                    }
                    _setFromLocationToolAssignment = true;
                    SelectedTool = _selectedLocationToolAssignmentModel?.AssignedTool;
                }
                else
                {
                    ToolSelectionRequest?.Invoke(this, toolModel);
                }
            }
        }

        private bool _allreadyChangingTestParameter;
        private void TestParameterPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(TestParametersModel.ControlledBy) 
                || _allreadyChangingTestParameter)
            {
                return;
            }

            if (!_locationToolAssignmentValidator.Validate(_selectedLocationToolAssignmentModel))
            {
                _allreadyChangingTestParameter = true;
                if (ShowUnvalidLocationToolAssignmentMessage())
                {
                    _guiDispatcher.BeginInvoke(DispatcherPriority.Normal,new Action(() => 
                    {
                        _allreadyChangingTestParameter = true;
                        SelectedLocationToolAssignmentModel.TestParameters.ControlledBy =
                            _locationToolAssignmentWithoutChanges.TestParameters.ControlledBy;
                        _allreadyChangingTestParameter = false;
                    }));
                }
                else
                {
                    _guiDispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
                    {
                        _allreadyChangingTestParameter = true;
                        SelectedLocationToolAssignmentModel.TestParameters.ControlledBy =
                            _locationToolAssignmentWithoutChanges.TestParameters.ControlledBy;
                        _allreadyChangingTestParameter = false;
                    }));
                }
            }
            else
            {
                if (!_locationToolAssignmentWithoutChanges?.EqualsByContent(_selectedLocationToolAssignmentModel) ??
                    false)
                {
                    _allreadyChangingTestParameter = true;
                    var diff = new LocationToolAssignmentDiff
                    {
                        OldLocationToolAssignment = _locationToolAssignmentWithoutChanges.Entity,
                        NewLocationToolAssignment = _selectedLocationToolAssignmentModel.Entity
                    };
                    _guiDispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
                    {
                        var isNotCanceled = VerifyLocationToolAssignmentsDiff(diff);
                        _allreadyChangingTestParameter = false;
                        if (!isNotCanceled)
                        {
                            _allreadyChangingTestParameter = true;
                            SelectedLocationToolAssignmentModel.TestParameters.ControlledBy =
                                _locationToolAssignmentWithoutChanges.TestParameters.ControlledBy;
                            _allreadyChangingTestParameter = false;
                        }
                    }));
                }
            }
        }

        private bool ShowUnvalidLocationToolAssignmentMessage()
        {
            bool continueEditing = true;
            MessageBoxRequest?.Invoke(this, new MessageBoxEventArgs(r =>
                {
                    if (r == MessageBoxResult.No)
                    {
                        // Reset selected location if user does not want to continue editing
                        SelectedLocationToolAssignmentModel.UpdateWith(_locationToolAssignmentWithoutChanges.Entity);
                        continueEditing = false;
                    }
                },
                _localization.Strings.GetParticularString("LocationToolAssignment", "The location tool assignment is not valid, do you want to continue editing? (If not, the location tool assignment is reseted to the last saved value)"),
                _localization.Strings.GetParticularString("LocationToolAssignment", "Location tool assignment not valid"),
                MessageBoxButton.YesNo,
                MessageBoxImage.Error));
            return continueEditing;
        }

        public RelayCommand LoadCommand { get; private set; }
        public RelayCommand AssignToolToLocationCommand { get; private set; }
        public RelayCommand AddTestConditionsCommand { get; private set; }
        public RelayCommand RemoveLocationToolAssignmentCommand { get; private set; }
        public RelayCommand ShowToolDetailsDialog { get; private set; }
        public RelayCommand SaveLocationToolAssignmentCommand { get; private set; }
        
        public LocationToolAssignmentViewModel(ILocationToolAssignmentUseCase locationToolAssignmentUseCase, ILocationUseCase locationUseCase, 
            IToolUseCase toolUseCase, IToleranceClassUseCase toleranceClassUseCase, ITestLevelSetUseCase testLevelSetUseCase, ITestLevelSetInterface testLevelSetInterface, IStartUp startUp, ILocalizationWrapper localization, 
            ILocationToolAssignmentDisplayFormatter locationToolAssignmentDisplayFormatter, IThreadCreator threadCreator, ILocationToolAssignmentValidtor locationToolAssignmentValidator,
            ILocationDisplayFormatter locationDisplayFormatter, IToolDisplayFormatter toolDisplayFormatter)
        {
            AllToolModelModels = new ObservableCollection<ToolModelModel>();
            AllToolModels = new ObservableCollection<InterfaceAdapters.Models.ToolModel>();
            AssignmentsForSelectedLocation = new ObservableCollection<LocationToolAssignmentModel>();

            _locationToolAssignmentUseCase = locationToolAssignmentUseCase;
            _locationUseCase = locationUseCase;
            _toolUseCase = toolUseCase;
            _toleranceClassUseCase = toleranceClassUseCase;
            _startUp = startUp;
            _localization = localization;
            _locationToolAssignmentDisplayFormatter = locationToolAssignmentDisplayFormatter;
            _locationDisplayFormatter = locationDisplayFormatter;
            _toolDisplayFormatter = toolDisplayFormatter;
            _threadCreator = threadCreator;
            _locationToolAssignmentValidator = locationToolAssignmentValidator;
            _testLevelSetUseCase = testLevelSetUseCase;
            _testLevelSetInterface = testLevelSetInterface;
            LocationTree = new LocationTreeModel();
            LoadCommand = new RelayCommand(LoadExecute, LoadCanExecute);
            AssignToolToLocationCommand = new RelayCommand(AssignToolToLocationExecute, AssignToolToLocationCanExecute);
            AddTestConditionsCommand = new RelayCommand(AddTestConditionsExecute, AddTestConditionsCanExecute);
            RemoveLocationToolAssignmentCommand = new RelayCommand(RemoveLocationToolAssignmentExecute, RemoveLocationToolAssignmentCanExecute);
            ShowToolDetailsDialog = new RelayCommand(ShowToolDetailsDialogExecute, ShowToolDetailsDialogCanExecute);
            SaveLocationToolAssignmentCommand = new RelayCommand(SaveLocationToolAssignmentExecute, SaveLocationToolAssignmentCanExecute);
            LocationControlledBys = new ObservableCollection<LocationControlledBy> { LocationControlledBy.Angle, LocationControlledBy.Torque };
            ToleranceClassModels = new ObservableCollection<ToleranceClassModel>();
            AvailableToolUsages = new ObservableCollection<HelperTableItemModel<ToolUsage, string>>();

            PropertyChangedEventManager.AddHandler(_testLevelSetInterface,
                (s, e) => RaisePropertyChanged(nameof(AvailableTestLevelSets)),
                nameof(TestLevelSetInterfaceAdapter.TestLevelSets));
        }

        private bool SaveLocationToolAssignmentCanExecute(object arg)
        {
            return (!_locationToolAssignmentWithoutChanges?.EqualsByContent(_selectedLocationToolAssignmentModel) ?? false) && 
                   _locationToolAssignmentValidator.Validate(_selectedLocationToolAssignmentModel);
        }

        private void SaveLocationToolAssignmentExecute(object obj)
        {
            var diff = new LocationToolAssignmentDiff()
            {
                OldLocationToolAssignment = _locationToolAssignmentWithoutChanges?.Entity,
                NewLocationToolAssignment = _selectedLocationToolAssignmentModel?.Entity
            };
            VerifyLocationToolAssignmentsDiff(diff);
        }

        private bool VerifyLocationToolAssignmentsDiff(LocationToolAssignmentDiff diff)
        {
            if (diff is null)
            {
                return true;
            }

            var changes = GetChangesFromDiff(diff).ToList();
            if (changes.Count == 0)
            {
                return true;
            }
            var args = new VerifyChangesEventArgs(changes);
            RequestChangesVerification?.Invoke(this, args);
            diff.Comment = new HistoryComment(args.Comment);
            switch (args.Result)
            {
                case MessageBoxResult.Yes:
                    _startUp.RaiseShowLoadingControl(true);
                    _locationToolAssignmentUseCase.UpdateLocationToolAssignment(new List<LocationToolAssignmentDiff>() { diff });
                    _locationToolAssignmentWithoutChanges = _selectedLocationToolAssignmentModel;
                    break;
                case MessageBoxResult.No:
                    _selectedLocationToolAssignmentModel.UpdateWith(_locationToolAssignmentWithoutChanges.Entity);
                    _locationToolAssignmentWithoutChanges = _selectedLocationToolAssignmentModel.CopyDeep();
                    RaisePropertyChanged(nameof(SelectedLocationToolAssignmentModel));
                    break;
                case MessageBoxResult.Cancel:
                    return false;
            }
            return true;
        }

        private IEnumerable<SingleValueChangeModel> GetChangesFromDiff(LocationToolAssignmentDiff diff)
        {
            List<SingleValueChangeModel> valueChanges = new List<SingleValueChangeModel>();
            if (diff is null)
            {
                return valueChanges;
            }

            if (diff.OldLocationToolAssignment is null && diff.NewLocationToolAssignment is null)
            {
                return valueChanges;
            }

            if (diff.OldLocationToolAssignment != null && diff.NewLocationToolAssignment is null)
            {
                throw new ArgumentNullException(nameof(diff.NewLocationToolAssignment), "NewLocationToolAssignment cant be null if oldLocationAssignment is not null");
            }

            string entity = _locationToolAssignmentDisplayFormatter.Format(diff.NewLocationToolAssignment);

            if (!diff.OldLocationToolAssignment?.ToolUsage?.EqualsByContent(diff.NewLocationToolAssignment.ToolUsage) ?? false)
            {
                valueChanges.Add(new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute =
                        _localization.Strings.GetParticularString("LocationToolAssignmentAttribute", "Tool usage"),
                    OldValue = diff.OldLocationToolAssignment.ToolUsage.Value.ToDefaultString(),
                    NewValue = diff.NewLocationToolAssignment.ToolUsage.Value.ToDefaultString()
                });
            }

            if (diff.OldLocationToolAssignment.TestParameters != null && diff.NewLocationToolAssignment.TestParameters != null)
            {
                if (diff.OldLocationToolAssignment.TestParameters.ControlledBy != diff.NewLocationToolAssignment.TestParameters.ControlledBy)
                {
                    valueChanges.Add(new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                    {
                        AffectedEntity = entity,
                        ChangedAttribute = _localization.Strings.GetParticularString("LocationToolAssignmentAttribute", "Controlled by"),
                        OldValue = _localization.Strings.GetParticularString("LocationToolAssignmentAttribute", diff.OldLocationToolAssignment.TestParameters.ControlledBy.ToString()),
                        NewValue = _localization.Strings.GetParticularString("LocationToolAssignmentAttribute", diff.NewLocationToolAssignment.TestParameters.ControlledBy.ToString())
                    });
                }

                FillTestParameterAngleValueChanges(diff, entity, valueChanges);
                FillTestParameterTorqueValueChanges(diff, entity, valueChanges);
            }
            
            if (diff.OldLocationToolAssignment.StartDateMfu != diff.NewLocationToolAssignment.StartDateMfu)
            {
                valueChanges.Add(new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("LocationToolAssignmentAttribute", "Start date mfu"),
                    OldValue = diff.OldLocationToolAssignment.StartDateMfu.ToShortDateString(),
                    NewValue = diff.NewLocationToolAssignment.StartDateMfu.ToShortDateString()
                });
            }

            if (diff.OldLocationToolAssignment.StartDateChk != diff.NewLocationToolAssignment.StartDateChk)
            {
                valueChanges.Add(new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("LocationToolAssignmentAttribute", "Start date chk"),
                    OldValue = diff.OldLocationToolAssignment.StartDateChk.ToShortDateString(),
                    NewValue = diff.NewLocationToolAssignment.StartDateChk.ToShortDateString()
                });
            }

            if (diff.OldLocationToolAssignment.TestOperationActiveMfu != diff.NewLocationToolAssignment.TestOperationActiveMfu)
            {
                valueChanges.Add(new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("LocationToolAssignmentAttribute", "Test operation active mfu"),
                    OldValue = _localization.Strings.GetParticularString("LocationToolAssignmentAttribute", diff.NewLocationToolAssignment.TestOperationActiveMfu ? "Active" : "Not active"),
                    NewValue = _localization.Strings.GetParticularString("LocationToolAssignmentAttribute", diff.NewLocationToolAssignment.TestOperationActiveMfu ? "Active" : "Not active")
                });
            }

            if (diff.OldLocationToolAssignment.TestOperationActiveChk != diff.NewLocationToolAssignment.TestOperationActiveChk)
            {
                valueChanges.Add(new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("LocationToolAssignmentAttribute", "Test operation active chk"),
                    OldValue = _localization.Strings.GetParticularString("LocationToolAssignmentAttribute", diff.OldLocationToolAssignment.TestOperationActiveChk ? "Active" : "Not active"),
                    NewValue = _localization.Strings.GetParticularString("LocationToolAssignmentAttribute", diff.NewLocationToolAssignment.TestOperationActiveChk ? "Active" : "Not active")
                });
            }

            if (diff.OldLocationToolAssignment.TestLevelSetMfu != null && diff.NewLocationToolAssignment.TestLevelSetMfu != null)
            {
                if (!diff.OldLocationToolAssignment.TestLevelSetMfu.Id.Equals(diff.NewLocationToolAssignment.TestLevelSetMfu.Id))
                {
                    valueChanges.Add(new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                    {
                        AffectedEntity = entity,
                        ChangedAttribute = _localization.Strings.GetParticularString("LocationToolAssignmentAttribute", "Test level set mfu"),
                        OldValue = diff.OldLocationToolAssignment.TestLevelSetMfu.Name.ToDefaultString(),
                        NewValue = diff.NewLocationToolAssignment.TestLevelSetMfu.Name.ToDefaultString()
                    });
                }
            }
            
            if (diff.OldLocationToolAssignment.TestLevelNumberMfu != diff.NewLocationToolAssignment.TestLevelNumberMfu)
            {
                valueChanges.Add(new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("LocationToolAssignmentAttribute", "Test level number mfu"),
                    OldValue = diff.OldLocationToolAssignment.TestLevelNumberMfu.ToString(),
                    NewValue = diff.NewLocationToolAssignment.TestLevelNumberMfu.ToString()
                });
            }

            if (diff.OldLocationToolAssignment.TestLevelSetChk != null && diff.NewLocationToolAssignment.TestLevelSetChk != null)
            {
                if (!diff.OldLocationToolAssignment.TestLevelSetChk.Id.Equals(diff.NewLocationToolAssignment.TestLevelSetChk.Id))
                {
                    valueChanges.Add(new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                    {
                        AffectedEntity = entity,
                        ChangedAttribute = _localization.Strings.GetParticularString("LocationToolAssignmentAttribute", "Test level set monitoring"),
                        OldValue = diff.OldLocationToolAssignment.TestLevelSetChk.Name.ToDefaultString(),
                        NewValue = diff.NewLocationToolAssignment.TestLevelSetChk.Name.ToDefaultString()
                    });
                }
            }

            if (diff.OldLocationToolAssignment.TestLevelNumberChk != diff.NewLocationToolAssignment.TestLevelNumberChk)
            {
                valueChanges.Add(new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange()
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("LocationToolAssignmentAttribute", "Test level number monitoring"),
                    OldValue = diff.OldLocationToolAssignment.TestLevelNumberChk.ToString(),
                    NewValue = diff.NewLocationToolAssignment.TestLevelNumberChk.ToString()
                }));
            }

            if (diff.OldLocationToolAssignment.TestTechnique != null)
            {
                FillTestTechniqueChangeValueModels(diff, entity, valueChanges);
            }
            return valueChanges;
        }

        private void FillTestTechniqueChangeValueModels(LocationToolAssignmentDiff diff, string entity, List<SingleValueChangeModel> valueChanges)
        {
            if (diff.OldLocationToolAssignment.TestTechnique.EndCycleTime !=
                diff.NewLocationToolAssignment.TestTechnique.EndCycleTime)
            {
                valueChanges.Add(new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute =
                        _localization.Strings.GetParticularString("LocationToolAssignmentAttribute",
                            "End cycle time"),
                    OldValue = diff.OldLocationToolAssignment.TestTechnique.EndCycleTime.ToString(),
                    NewValue = diff.NewLocationToolAssignment.TestTechnique.EndCycleTime.ToString()
                });
            }

            if (diff.OldLocationToolAssignment.TestTechnique.FilterFrequency !=
                diff.NewLocationToolAssignment.TestTechnique.FilterFrequency)
            {
                valueChanges.Add(new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute =
                        _localization.Strings.GetParticularString("LocationToolAssignmentAttribute",
                            "Filter frequency"),
                    OldValue = diff.OldLocationToolAssignment.TestTechnique.FilterFrequency.ToString(),
                    NewValue = diff.NewLocationToolAssignment.TestTechnique.FilterFrequency.ToString()
                });
            }

            if (diff.OldLocationToolAssignment.TestTechnique.CycleComplete !=
                diff.NewLocationToolAssignment.TestTechnique.CycleComplete)
            {
                valueChanges.Add(new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute =
                        _localization.Strings.GetParticularString("LocationToolAssignmentAttribute",
                            "Cycle complete"),
                    OldValue = diff.OldLocationToolAssignment.TestTechnique.CycleComplete.ToString(),
                    NewValue = diff.NewLocationToolAssignment.TestTechnique.CycleComplete.ToString()
                });
            }

            if (diff.OldLocationToolAssignment.TestTechnique.MeasureDelayTime !=
                diff.NewLocationToolAssignment.TestTechnique.MeasureDelayTime)
            {
                valueChanges.Add(new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute =
                        _localization.Strings.GetParticularString("LocationToolAssignmentAttribute",
                            "Measure delay time"),
                    OldValue = diff.OldLocationToolAssignment.TestTechnique.MeasureDelayTime.ToString(),
                    NewValue = diff.NewLocationToolAssignment.TestTechnique.MeasureDelayTime.ToString()
                });
            }

            if (diff.OldLocationToolAssignment.TestTechnique.ResetTime !=
                diff.NewLocationToolAssignment.TestTechnique.ResetTime)
            {
                valueChanges.Add(new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute =
                        _localization.Strings.GetParticularString("LocationToolAssignmentAttribute",
                            "Reset time"),
                    OldValue = diff.OldLocationToolAssignment.TestTechnique.ResetTime.ToString(),
                    NewValue = diff.NewLocationToolAssignment.TestTechnique.ResetTime.ToString()
                });
            }

            if (diff.OldLocationToolAssignment.TestTechnique.MustTorqueAndAngleBeInLimits !=
                diff.NewLocationToolAssignment.TestTechnique.MustTorqueAndAngleBeInLimits)
            {
                valueChanges.Add(new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute =
                        _localization.Strings.GetParticularString("LocationToolAssignmentAttribute",
                            "Mest torque and angle be in limits"),
                    OldValue = _localization.Strings.GetParticularString("LocationToolAssignmentAttribute", "Yes"),
                    NewValue = _localization.Strings.GetParticularString("LocationToolAssignmentAttribute", "No")
                });
            }

            if (diff.OldLocationToolAssignment.TestTechnique.CycleStart !=
                diff.NewLocationToolAssignment.TestTechnique.CycleStart)
            {
                valueChanges.Add(new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute =
                        _localization.Strings.GetParticularString("LocationToolAssignmentAttribute",
                            "Cycle start"),
                    OldValue = diff.OldLocationToolAssignment.TestTechnique.CycleStart.ToString(),
                    NewValue = diff.NewLocationToolAssignment.TestTechnique.CycleStart.ToString()
                });
            }

            if (diff.OldLocationToolAssignment.TestTechnique.StartFinalAngle !=
                diff.NewLocationToolAssignment.TestTechnique.StartFinalAngle)
            {
                valueChanges.Add(new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute =
                        _localization.Strings.GetParticularString("LocationToolAssignmentAttribute",
                            "Start final angle"),
                    OldValue = diff.OldLocationToolAssignment.TestTechnique.StartFinalAngle.ToString(),
                    NewValue = diff.NewLocationToolAssignment.TestTechnique.StartFinalAngle.ToString()
                });
            }

            if (diff.OldLocationToolAssignment.TestTechnique.SlipTorque !=
                diff.NewLocationToolAssignment.TestTechnique.SlipTorque)
            {
                valueChanges.Add(new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute =
                        _localization.Strings.GetParticularString("LocationToolAssignmentAttribute",
                            "Slip torque"),
                    OldValue = diff.OldLocationToolAssignment.TestTechnique.SlipTorque.ToString(),
                    NewValue = diff.NewLocationToolAssignment.TestTechnique.SlipTorque.ToString()
                });
            }

            if (diff.OldLocationToolAssignment.TestTechnique.TorqueCoefficient !=
                diff.NewLocationToolAssignment.TestTechnique.TorqueCoefficient)
            {
                valueChanges.Add(new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute =
                        _localization.Strings.GetParticularString("LocationToolAssignmentAttribute",
                            "Torque Coefficient"),
                    OldValue = diff.OldLocationToolAssignment.TestTechnique.TorqueCoefficient.ToString(),
                    NewValue = diff.NewLocationToolAssignment.TestTechnique.TorqueCoefficient.ToString()
                });
            }

            if (diff.OldLocationToolAssignment.TestTechnique.MinimumPulse !=
                diff.NewLocationToolAssignment.TestTechnique.MinimumPulse)
            {
                valueChanges.Add(new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute =
                        _localization.Strings.GetParticularString("LocationToolAssignmentAttribute",
                            "Minimum pulse"),
                    OldValue = diff.OldLocationToolAssignment.TestTechnique.MinimumPulse.ToString(),
                    NewValue = diff.NewLocationToolAssignment.TestTechnique.MinimumPulse.ToString()
                });
            }

            if (diff.OldLocationToolAssignment.TestTechnique.MaximumPulse !=
                diff.NewLocationToolAssignment.TestTechnique.MaximumPulse)
            {
                valueChanges.Add(new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute =
                        _localization.Strings.GetParticularString("LocationToolAssignmentAttribute",
                            "Maximum pulse"),
                    OldValue = diff.OldLocationToolAssignment.TestTechnique.MaximumPulse.ToString(),
                    NewValue = diff.NewLocationToolAssignment.TestTechnique.MaximumPulse.ToString()
                });
            }

            if (diff.OldLocationToolAssignment.TestTechnique.Threshold !=
                diff.NewLocationToolAssignment.TestTechnique.Threshold)
            {
                valueChanges.Add(new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute =
                        _localization.Strings.GetParticularString("LocationToolAssignmentAttribute",
                            "Threshold"),
                    OldValue = diff.OldLocationToolAssignment.TestTechnique.Threshold.ToString(),
                    NewValue = diff.NewLocationToolAssignment.TestTechnique.Threshold.ToString()
                });
            }
        }

        private string GetStringForTestOperationActive(bool testOperationActive)
        {
            if (testOperationActive)
            {
                return _localization.Strings.GetParticularString("LocationToolAssignmentAttribute", "Active");
            }
            else
            {
                return _localization.Strings.GetParticularString("LocationToolAssignmentAttribute", "Inactive");
            }
        }

        private void FillTestParameterAngleValueChanges(LocationToolAssignmentDiff diff, string entity, List<SingleValueChangeModel> changedValueModels)
        {
            if (diff.OldLocationToolAssignment.TestParameters.ThresholdTorque.Degree !=
                diff.NewLocationToolAssignment.TestParameters.ThresholdTorque.Degree)
            {
                changedValueModels.Add(new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute =
                        _localization.Strings.GetParticularString("LocationToolAssignmentAttribute",
                            "Threshold torque"),
                    OldValue = diff.OldLocationToolAssignment.TestParameters.ThresholdTorque.Degree.ToString(),
                    NewValue = diff.NewLocationToolAssignment.TestParameters.ThresholdTorque.Degree.ToString()
                });
            }

            if (diff.OldLocationToolAssignment.TestParameters.SetPointAngle.Degree !=
                diff.NewLocationToolAssignment.TestParameters.SetPointAngle.Degree)
            {
                changedValueModels.Add(new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute =
                        _localization.Strings.GetParticularString("LocationToolAssignmentAttribute",
                            "Angle"),
                    OldValue = diff.OldLocationToolAssignment.TestParameters.SetPointAngle.Degree.ToString(),
                    NewValue = diff.NewLocationToolAssignment.TestParameters.SetPointAngle.Degree.ToString()
                });
            }

            if (!diff.OldLocationToolAssignment.TestParameters.ToleranceClassAngle?.EqualsById(
                diff.NewLocationToolAssignment.TestParameters.ToleranceClassAngle) ?? false)
            {
                changedValueModels.Add(new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute =
                        _localization.Strings.GetParticularString("LocationToolAssignmentAttribute",
                            "Tolerance class angle"),
                    OldValue = diff.OldLocationToolAssignment.TestParameters.ToleranceClassAngle.Name,
                    NewValue = diff.NewLocationToolAssignment.TestParameters.ToleranceClassAngle.Name
                });
            }

            if (diff.OldLocationToolAssignment.TestParameters.MinimumAngle.Degree !=
                diff.NewLocationToolAssignment.TestParameters.MinimumAngle.Degree)
            {
                changedValueModels.Add(new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute =
                        _localization.Strings.GetParticularString("LocationToolAssignmentAttribute",
                            "Minimum Angle"),
                    OldValue = diff.OldLocationToolAssignment.TestParameters.MinimumAngle.Degree.ToString(),
                    NewValue = diff.NewLocationToolAssignment.TestParameters.MinimumAngle.Degree.ToString()
                });
            }

            if (diff.OldLocationToolAssignment.TestParameters.MaximumAngle.Degree !=
                diff.NewLocationToolAssignment.TestParameters.MaximumAngle.Degree)
            {
                changedValueModels.Add(new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute =
                        _localization.Strings.GetParticularString("LocationToolAssignmentAttribute",
                            "Maximum Angle"),
                    OldValue = diff.OldLocationToolAssignment.TestParameters.MaximumAngle.Degree.ToString(),
                    NewValue = diff.NewLocationToolAssignment.TestParameters.MaximumAngle.Degree.ToString()
                });
            }
        }

        private void FillTestParameterTorqueValueChanges(LocationToolAssignmentDiff diff, string entity, List<SingleValueChangeModel> changedValueModels)
        {
            if (diff.OldLocationToolAssignment.TestParameters.SetPointTorque.Nm !=
                diff.NewLocationToolAssignment.TestParameters.SetPointTorque.Nm)
            {
                changedValueModels.Add(new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute =
                        _localization.Strings.GetParticularString("LocationToolAssignmentAttribute",
                            "Setpoint torque"),
                    OldValue = diff.OldLocationToolAssignment.TestParameters.SetPointTorque.Nm.ToString(),
                    NewValue = diff.NewLocationToolAssignment.TestParameters.SetPointTorque.Nm.ToString()
                });
            }

            if (!diff.OldLocationToolAssignment.TestParameters.ToleranceClassTorque?.EqualsById(
                diff.NewLocationToolAssignment.TestParameters.ToleranceClassTorque) ?? false)
            {
                changedValueModels.Add(new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute = _localization.Strings.GetParticularString("LocationToolAssignmentAttribute",
                        "Tolerance class torque"),
                    OldValue = diff.OldLocationToolAssignment.TestParameters.ToleranceClassTorque.Name,
                    NewValue = diff.NewLocationToolAssignment.TestParameters.ToleranceClassTorque.Name
                });
            }

            if (diff.OldLocationToolAssignment.TestParameters.MinimumTorque.Nm !=
                diff.NewLocationToolAssignment.TestParameters.MinimumTorque.Nm)
            {
                changedValueModels.Add(new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute =
                        _localization.Strings.GetParticularString("LocationToolAssignmentAttribute",
                            "Minimum torque"),
                    OldValue = diff.OldLocationToolAssignment.TestParameters.MinimumTorque.Nm.ToString(CultureInfo.CurrentCulture),
                    NewValue = diff.NewLocationToolAssignment.TestParameters.MinimumTorque.Nm.ToString(CultureInfo.CurrentCulture)
                });
            }

            if (diff.OldLocationToolAssignment.TestParameters.MaximumTorque.Nm !=
                diff.NewLocationToolAssignment.TestParameters.MaximumTorque.Nm)
            {
                changedValueModels.Add(new SingleValueChangeModel(new Client.Core.ChangesGenerators.SingleValueChange())
                {
                    AffectedEntity = entity,
                    ChangedAttribute =
                        _localization.Strings.GetParticularString("LocationToolAssignmentAttribute",
                            "Maximum torque"),
                    OldValue = diff.OldLocationToolAssignment.TestParameters.MaximumTorque.Nm.ToString(CultureInfo.CurrentCulture),
                    NewValue = diff.NewLocationToolAssignment.TestParameters.MaximumTorque.Nm.ToString(CultureInfo.CurrentCulture)
                });
            }
        }

        private bool ShowToolDetailsDialogCanExecute(object arg)
        {
            return SelectedTool != null || SelectedLocationToolAssignmentModel?.AssignedTool != null;
        }

        private void ShowToolDetailsDialogExecute(object obj)
        {
            _startUp.OpenLocationToolAssignmentToolDetailDialog(AssignmentsForSelectedLocation?.Select(x => new ToolModelWithToolUsage(x.AssignedTool, x.ToolUsage)).ToList());
        }


        private bool LoadCanExecute(object arg) { return true; }

        private void LoadExecute(object obj)
        {
            _startUp.RaiseShowLoadingControl(true, 3);
            _locationUseCase.LoadTree(this);

            _toolUseCase.LoadModelsWithAtLeastOneTool();
            _toleranceClassUseCase.LoadToleranceClasses();
            _testLevelSetUseCase.LoadTestLevelSets(this);
        }

        private bool AssignToolToLocationCanExecute(object arg)
        {
            return SelectedLocation != null &&
                   SelectedTool != null &&
                   _loadToolAssignmentsForLocationCallCounter == 0 &&
                   AssignmentsForSelectedLocation.Count(x =>
                       x.AssignedLocation.EqualsById(SelectedLocation) && x.AssignedTool.EqualsById(SelectedTool)) == 0;
        }

        private void AssignToolToLocationExecute(object obj)
        {
            var assignment = new LocationToolAssignment()
            {
                AssignedLocation = SelectedLocation?.Entity,
                AssignedTool = SelectedTool.Entity
            };

            var assistent = _startUp.OpenAssignToolToLocationAssistent(assignment);

            assistent.EndOfAssistent += (s, e) =>
            {
                _startUp.RaiseShowLoadingControl(true);
                var filledAssignment = (LocationToolAssignment)(assistent.DataContext as AssistentViewModel).FillResultObject(assignment);

                if (SelectedTool.Status.Entity.EqualsById(filledAssignment.AssignedTool.Status))
                {
                    _toolUseCase.UpdateTool(new ToolDiff(null, SelectedTool.Entity, filledAssignment.AssignedTool));
                }

                _locationToolAssignmentUseCase.AssignToolToLocation(filledAssignment);
            };

            ShowDialogRequest?.Invoke(this, assistent);
        }

        private bool AddTestConditionsCanExecute(object arg)
        {
            return SelectedLocationToolAssignmentModel != null &&
                   SelectedLocationToolAssignmentModel.Entity.TestParameters == null &&
                   SelectedLocationToolAssignmentModel.Entity.TestTechnique == null;
        }

        private void AddTestConditionsExecute(object obj)
        {
            var assistent = _startUp.OpenAddTestConditionsAssistent(new LocationToolAssignment()
            {
                AssignedTool = SelectedLocationToolAssignmentModel?.AssignedTool?.Entity,
                AssignedLocation = SelectedLocation?.Entity,
                TestParameters = new TestParameters()
                {
                    SetPointTorque = Torque.FromNm(SelectedLocation.SetPointTorque),
                    ToleranceClassTorque = SelectedLocation.ToleranceClassTorque?.Entity,
                    MinimumTorque = Torque.FromNm(SelectedLocation.MinimumTorque),
                    MaximumTorque = Torque.FromNm(SelectedLocation.MaximumTorque),
                    SetPointAngle = Angle.FromDegree(SelectedLocation.SetPointAngle),
                    ThresholdTorque = Angle.FromDegree(SelectedLocation.ThresholdTorque),
                    ToleranceClassAngle = SelectedLocation.ToleranceClassAngle?.Entity,
                    MinimumAngle = Angle.FromDegree(SelectedLocation.MinimumAngle),
                    MaximumAngle = Angle.FromDegree(SelectedLocation.MaximumAngle),
                    ControlledBy = SelectedLocation.ControlledBy,
                },
                StartDateMfu = DateTime.Today,
                StartDateChk = DateTime.Today
            });

            assistent.EndOfAssistent += (s, e) =>
            {
                SelectedLocationToolAssignmentModel.Entity.TestParameters = new TestParameters();
                SelectedLocationToolAssignmentModel.Entity.TestTechnique = new TestTechnique();

                var filledAssignment = (LocationToolAssignment)(assistent.DataContext as AssistentViewModel).FillResultObject(SelectedLocationToolAssignmentModel.Entity);

                switch (filledAssignment.TestParameters.ControlledBy)
                {
                    case LocationControlledBy.Torque:
                        filledAssignment.TestParameters.SetPointAngle =
                            Angle.FromDegree(SelectedLocation.SetPointAngle);
                        filledAssignment.TestParameters.ThresholdTorque =
                            Angle.FromDegree(SelectedLocation.ThresholdTorque);
                        filledAssignment.TestParameters.ToleranceClassAngle =
                            SelectedLocation.ToleranceClassAngle?.Entity;
                        filledAssignment.TestParameters.MinimumAngle = Angle.FromDegree(SelectedLocation.MinimumAngle);
                        filledAssignment.TestParameters.MaximumAngle = Angle.FromDegree(SelectedLocation.MaximumAngle);
                        break;
                    case LocationControlledBy.Angle:
                        filledAssignment.TestParameters.SetPointTorque = Torque.FromNm(SelectedLocation.SetPointTorque);
                        filledAssignment.TestParameters.ToleranceClassTorque = SelectedLocation.ToleranceClassTorque?.Entity;
                        filledAssignment.TestParameters.MinimumTorque = Torque.FromNm(SelectedLocation.MinimumTorque);
                        filledAssignment.TestParameters.MaximumTorque = Torque.FromNm(SelectedLocation.MaximumTorque);
                        break;
                }
                filledAssignment.TestParameters.UpdateToleranceLimits();
                _locationToolAssignmentUseCase.AddTestConditions(filledAssignment);
            };

            ShowDialogRequest?.Invoke(this, assistent);
        }

        private bool RemoveLocationToolAssignmentCanExecute(object arg)
        {
            return SelectedLocationToolAssignmentModel != null;
        }

        private void RemoveLocationToolAssignmentExecute(object obj)
        {
            var args = new MessageBoxEventArgs(result =>
                {
                    if (result == MessageBoxResult.Yes)
                    {
                        if (!AskForNewStatusForTool())
                        {
                            return;
                        }
                        SelectedLocationToolAssignmentModel?.UpdateWith(_locationToolAssignmentWithoutChanges?.Entity);

                        _locationToolAssignmentUseCase.RemoveLocationToolAssignment(SelectedLocationToolAssignmentModel?.Entity);
                    }
                },
                _localization.Strings.GetParticularString("Location tool assignment", "Do you really want to remove the selected location tool assignment (only the assignment will be deleted, not the location or tool)"),
                _localization.Strings.GetParticularString("Location tool assignment", "Remove location tool assignment"),
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);
            MessageBoxRequest?.Invoke(this, args);
        }

        private bool AskForNewStatusForTool()
        {
            var assignedToolModel = SelectedLocationToolAssignmentModel.AssignedTool;
            var assignedToolEntity = SelectedLocationToolAssignmentModel.Entity.AssignedTool;
            var assistant = _startUp.GetAssistentView(_localization.Strings.GetParticularString("Location tool assignment", "New status for tool"));
            var statusPlan = _startUp.GetStatusAssistentPlan(assistant, SelectedLocationToolAssignmentModel?.Entity?.AssignedTool?.Status?.ListId);

            assistant.SetParentPlan(statusPlan);
            bool result = false;
            assistant.EndOfAssistent += (s, e) =>
            {
                var oldTool = assignedToolModel.CopyDeep().Entity;

                var filledTool = (Tool)assistant.ViewModel.FillResultObject(assignedToolEntity);

                if (!oldTool.Status.EqualsById(filledTool.Status))
                {
                    _toolUseCase.UpdateTool(new ToolDiff(null, oldTool, filledTool), false);
                }
                result = true;
            };

            ShowDialogRequest?.Invoke(this, assistant);
            return result;
        }

        public void SetGuiDispatcher(Dispatcher guiDispatcher)
        {
            _guiDispatcher = guiDispatcher;
        }

        public void ToolModelExpanded(ToolModelModel toolmodel)
        {
            _startUp.RaiseShowLoadingControl(true);
            _toolUseCase.LoadToolsForModel(toolmodel?.Entity);
        }


        #region Interface

        public void LoadLocationToolAssignments(List<LocationToolAssignment> locationToolAssignments)
        {
            // Do nothing
        }

        public void ShowLocationToolAssignmentError()
        {
            // Do nothing
        }

        public void AssignToolToLocation(LocationToolAssignment assignment)
        {
            if (assignment is null)
            {
                return;
            }

            _guiDispatcher.Invoke(() =>
            {
                var locationToolAssignmentModel = LocationToolAssignmentModel.GetModelFor(assignment, _localization);
                AssignmentsForSelectedLocation.Add(locationToolAssignmentModel);
                SelectedLocationToolAssignmentModel = locationToolAssignmentModel;
                RaisePropertyChanged();
                _startUp.RaiseShowLoadingControl(false);
                CommandManager.InvalidateRequerySuggested();
            });
        }

        public void AssignToolToLocationError()
        {
            var args = new MessageBoxEventArgs(r => { },
                _localization.Strings.GetParticularString("Location tool assignment", "An error occured while assigning a tool to a location"),
                messageBoxImage: MessageBoxImage.Error);
            MessageBoxRequest?.Invoke(this, args);
            _startUp.RaiseShowLoadingControl(false);
        }

        public void ShowAssignedToolsForLocation(List<LocationToolAssignment> assignments)
        {
            _guiDispatcher.Invoke(() =>
            {
                AssignmentsForSelectedLocation.Clear();
                assignments.ForEach(x => AssignmentsForSelectedLocation.Add(LocationToolAssignmentModel.GetModelFor(x, _localization)));
                RaisePropertyChanged(nameof(SelectedLocation));
                _loadToolAssignmentsForLocationCallCounter--;
                CommandManager.InvalidateRequerySuggested();
                _startUp.RaiseShowLoadingControl(false);
            });
        }

        public void LoadAssignedToolsForLocationError()
        {
            var args = new MessageBoxEventArgs(r => { },
                _localization.Strings.GetParticularString("Location tool assignment", "An error occured while loading all assigned tools for the location"),
                messageBoxImage: MessageBoxImage.Error);
            MessageBoxRequest?.Invoke(this, args);
            _loadToolAssignmentsForLocationCallCounter--;
            CommandManager.InvalidateRequerySuggested();
        }

        public void AddTestConditions(LocationToolAssignment assignment)
        {
            _guiDispatcher.Invoke(() =>
            {
                SelectedLocationToolAssignmentModel.Entity.TestTechnique = assignment.TestTechnique;
                SelectedLocationToolAssignmentModel.Entity.TestParameters = assignment.TestParameters;
                if (SelectedLocationToolAssignmentModel?.Entity?.TestParameters != null)
                {
                    SelectedLocationToolAssignmentModel.TestParameters.PropertyChanged += TestParameterPropertyChanged;
                }
                _locationToolAssignmentWithoutChanges = SelectedLocationToolAssignmentModel.CopyDeep();
                RaisePropertyChanged(nameof(SelectedLocationToolAssignmentModel));
            });
        }

        public void AddTestConditionsError()
        {
            var args = new MessageBoxEventArgs(r => { },
                _localization.Strings.GetParticularString("Location tool assignment", "An error occured while adding test conditions"),
                messageBoxImage: MessageBoxImage.Error);
            MessageBoxRequest?.Invoke(this, args);
        }

        public void ShowUnusedToolUsagesForLocation(List<ToolUsage> toolUsages, LocationId locationId)
        {
            _guiDispatcher.Invoke(() =>
            {
                if (SelectedLocation == null || SelectedLocation.Id != locationId.ToLong())
                {
                    return;
                }

                var previousToolUsage = SelectedLocationToolAssignmentModel?.ToolUsage;
                AvailableToolUsages.Clear();
                toolUsages.ForEach(x => AvailableToolUsages.Add(HelperTableItemModel.GetModelForToolUsage(x)));

                if (previousToolUsage != null)
                {
                    AvailableToolUsages.Add(previousToolUsage);
                    SelectedLocationToolAssignmentModel.ToolUsage = previousToolUsage;
                }
            });
        }

        public void LoadUnusedToolUsagesForLocationError()
        {
            // Do nothing
        }

        public void RemoveLocationToolAssignment(LocationToolAssignment assignment)
        {
            _guiDispatcher.Invoke(() =>
            {

                var model = AssignmentsForSelectedLocation.FirstOrDefault(x => x.Entity.EqualsById(assignment));

                if (SelectedLocationToolAssignmentModel?.Entity.EqualsById(assignment) ?? assignment == null)
                {
                    _selectedLocationToolAssignmentModel = null;
                    _locationToolAssignmentWithoutChanges = null;
                    SelectedLocationToolAssignmentModel = null;
                }

                if (model != null)
                {
                    AssignmentsForSelectedLocation.Remove(model);
                }

                CommandManager.InvalidateRequerySuggested();
            });
        }

        public void RemoveLocationToolAssignmentError()
        {
            var args = new MessageBoxEventArgs(r => { },
                _localization.Strings.GetParticularString("Location tool assignment", "An error occured while removing a location tool assignment"),
                messageBoxImage: MessageBoxImage.Error);
            MessageBoxRequest?.Invoke(this, args);
        }

        public void ShowLocationReferenceLinksForTool(List<LocationReferenceLink> locationReferenceLinks)
        {
            // Do nothing
        }

        public void UpdateLocationToolAssignment(List<LocationToolAssignment> updatedLocationToolAssignments)
        {
            _guiDispatcher.Invoke(() =>
            {
                foreach (var updatedLocationToolAssignment in updatedLocationToolAssignments)
                {
                    var locationToolAssignment =
                                AssignmentsForSelectedLocation.FirstOrDefault(
                                    x => x.Id == updatedLocationToolAssignment.Id.ToLong());
                    locationToolAssignment?.UpdateWith(updatedLocationToolAssignment);
                    if (SelectedLocationToolAssignmentModel?.EqualsById(locationToolAssignment) ?? false)
                    {
                        _locationToolAssignmentWithoutChanges = locationToolAssignment.CopyDeep();
                        _selectedLocationToolAssignmentModel = locationToolAssignment;
                    } 
                }

                _locationToolAssignmentUseCase.LoadUnusedToolUsagesForLocation(_selectedLocation?.Entity?.Id);
                _startUp.RaiseShowLoadingControl(false);
            });

        }

        public void UpdateLocationToolAssignmentError()
        {
            var args = new MessageBoxEventArgs(r => { },
                _localization.Strings.GetParticularString("LocationToolAssignmentViewModel",
                    "An error occured while updating test conditions"),
                caption: _localization.Strings.GetParticularString("LocationToolAssignmentViewModel",
                    "Error while updating"),
                messageBoxImage: MessageBoxImage.Error);
            MessageBoxRequest?.Invoke(this, args);
            _startUp.RaiseShowLoadingControl(false);
        }

        public void ShowLocationTree(List<LocationDirectory> directories)
        {
            _guiDispatcher.Invoke(() =>
            {
                LocationTree = new LocationTreeModel();
                directories.ForEach(x => LocationTree.LocationDirectoryModels.Add(LocationDirectoryHumbleModel.GetModelFor(x, _locationUseCase)));
                InitializeLocationTreeRequest?.Invoke(this, System.EventArgs.Empty);
            });
        }

        public void ShowLocationTreeError()
        {
            // Do nothing
        }

        public void ShowLoadingLocationTreeFinished()
        {
            _startUp.RaiseShowLoadingControl(false);
        }

        public void AddLocation(Location location)
        {
            _guiDispatcher.Invoke(() =>
            {
                var locationModel = LocationModel.GetModelFor(location, _localization, _locationUseCase);
                LocationTree.LocationModels.Add(locationModel);
                _startUp.RaiseShowLoadingControl(false);
            });
        }

        public void AddLocationError()
        {
            // Do nothing
        }

        public void RemoveLocation(Location location)
        {
            // TODO: Has to be implemented if Locations can be edited parallel
            // Do nothing
        }

        public void ShowRemoveLocationError()
        {
            // Do nothing
        }

        public void ShowPictureForLocation(Picture picture, LocationId locationId)
        {
            // TODO: Has to be implemented if Locations can be edited parallel
            // Do nothing
        }

        public void ShowCommentForLocation(string comment, LocationId locationId)
        {
            // TODO: Has to be implemented if Locations can be edited parallel
            // Do nothing
        }

        public void UpdateLocation(Location location)
        {
            // TODO: Has to be implemented if Locations can be edited parallel
            // Do nothing
        }

        public void UpdateLocationError()
        {
            // Do nothing
        }

        public void LocationAlreadyExists()
        {
            // Do nothing
        }

        public void AddLocationDirectory(LocationDirectory locationDirectory)
        {
            // TODO: Has to be implemented if Locations can be edited parallel
            // Do nothing
        }

        public void ShowAddLocationDirectoryError(string name)
        {
            // Do nothing
        }

        public void RemoveDirectory(LocationDirectoryId selectedDirectoryId)
        {
            // TODO: Has to be implemented if Locations can be edited parallel
            // Do nothing
        }

        public void ShowRemoveDirectoryError()
        {
            // Do nothing
        }

        public void ChangeLocationParent(Location location, LocationDirectoryId newParentId)
        {
            throw new NotImplementedException();
        }

        public void ChangeLocationParentError()
        {
            throw new NotImplementedException();
        }

        public void ShowLocation(Location location)
        {
            _guiDispatcher.Invoke(() =>
            {
                var locationModel = LocationModel.GetModelFor(location, _localization, _locationUseCase);
                LocationTree.LocationModels.Add(locationModel);
            });
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

        public void ChangeToolStatusAssistant(List<LocationToolAssignment> locationToolAssignments)
        {
            //Intentionally empty
        }

        public void ShowChangeToolStatusDialog(Action onSuccess, List<LocationToolAssignment> locationToolAssignments)
        {
            //Intentionally empty
        }

        public void AddTool(Tool newTool)
        {
            // TODO: Has to be implemented if Tools can be edited parallel
            // Do nothing
        }

        public void ShowLoadingErrorMessage()
        {
            // Do nothing
        }

        public void ShowTools(List<Tool> loadTools)
        {
            _guiDispatcher.Invoke(() =>
            {
                loadTools.ForEach(x => AllToolModels.Add(InterfaceAdapters.Models.ToolModel.GetModelFor(x, _localization)));
                _startUp.RaiseShowLoadingControl(false);
            });
        }

        public void ShowCommentForTool(Tool tool, string comment)
        {
            _guiDispatcher.Invoke(() =>
            {
                var toolModel = AllToolModels.FirstOrDefault(x => x.Entity.EqualsById(tool));

                if (!(toolModel is null))
                {
                    toolModel.Comment = comment;
                }
                _startUp.RaiseShowLoadingControl(false);
            });
        }

        public void ShowCommentForToolError()
        {
            // Do nothing
        }

        public void ShowPictureForTool(long toolId, Picture picture)
        {
            // TODO: Has to be implemented if Tools can be edited parallel
            // Do nothing
        }

        public void ShowToolErrorMessage()
        {
            // Do nothing
        }

        public void ShowModelsWithAtLeastOneTool(List<ToolModel> models)
        {
            _guiDispatcher.Invoke(() =>
            {
                AllToolModelModels.Clear();
                models.ForEach(x => AllToolModelModels.Add(ToolModelModel.GetModelFor(x, _localization)));
                _startUp.RaiseShowLoadingControl(false);
            });
        }

        public void ShowRemoveToolErrorMessage()
        {
            // Do nothing
        }

        public void RemoveTool(Tool tool)
        {
            // TODO: Has to be implemented if Tools can be edited parallel
            // Do nothing
        }

        public void UpdateTool(Tool updateTool)
        {
            var toolModel = AllToolModels.FirstOrDefault(x => x.Entity.EqualsById(updateTool));
            toolModel?.UpdateWith(updateTool);

            if (SelectedTool?.Entity.EqualsById(updateTool) == true)
            {
                SelectedTool.UpdateWith(updateTool);
            }
        }

        public void ShowEntryAlreadyExistsMessage(Tool diffNewTool)
        {
            // Do nothing
        }

        public void ToolAlreadyExists()
        {
            // Do nothing
        }

        public void ShowRemoveToolPreventingReferences(List<LocationToolAssignmentReferenceLink> references)
        {
            throw new NotImplementedException();
        }

        #endregion

        public void ShowToleranceClasses(List<ToleranceClass> toleranceClasses)
        {
            _guiDispatcher.Invoke(() =>
            {
                ToleranceClassModels.Clear();
                toleranceClasses.ForEach(x => ToleranceClassModels.Add(ToleranceClassModel.GetModelFor(x)));
                _startUp.RaiseShowLoadingControl(false);
            });
        }

        public void ShowToleranceClassesError()
        {
            //Intentiaonaly empty
        }

        public void RemoveToleranceClass(ToleranceClass toleranceClass)
        {
            //Intentiaonaly empty
        }

        public void RemoveToleranceClassError()
        {
            //Intentiaonaly empty
        }

        public void AddToleranceClass(ToleranceClass toleranceClass)
        {
            //Intentiaonaly empty
        }

        public void AddToleranceClassError()
        {
            //intentionally empty
        }

        public void UpdateToleranceClass(ToleranceClass toleranceClass)
        {
            //Intentiaonaly empty
        }

        public void SaveToleranceClassError()
        {
            //Intentiaonaly empty
        }

        public void ShowReferencedLocations(List<LocationReferenceLink> locationReferenceLinks)
        {
            //Intentiaonaly empty
        }

        public void ShowReferencesError()
        {
            //Intentiaonaly empty
        }

        public void ShowReferencedLocationToolAssignments(List<LocationToolAssignment> assignments)
        {
            // Do nothing
        }

        public void ShowRemoveToleranceClassPreventingReferences(List<LocationReferenceLink> referencedLocations, List<LocationToolAssignment> referencedLocationToolAssignments)
        {
            //Intentiaonaly empty
        }

        public bool CanClose()
        {
            if (!_locationToolAssignmentValidator.Validate(_selectedLocationToolAssignmentModel))
            {
                var continueEditing = ShowUnvalidLocationToolAssignmentMessage();
                if (continueEditing)
                {
                    return false;
                }
            }

            if (!_selectedLocationToolAssignmentModel?.EqualsByContent(_locationToolAssignmentWithoutChanges) ?? false)
            {
                var diff = new LocationToolAssignmentDiff
                {
                    OldLocationToolAssignment = _locationToolAssignmentWithoutChanges.Entity,
                    NewLocationToolAssignment = _selectedLocationToolAssignmentModel.Entity
                };
                var isNotCanceled = VerifyLocationToolAssignmentsDiff(diff);
                return isNotCanceled;
            }
            return true;
        }

        public void ShowTestLevelSetError()
        {
            var args = new MessageBoxEventArgs(r => { },
                _localization.Strings.GetParticularString("LocationToolAssignment", "Some errors occurred while loading the test level sets"),
                _localization.Strings.GetParticularString("LocationToolAssignment", "Error"),
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            MessageBoxRequest?.Invoke(this, args);
        }
    }
}
