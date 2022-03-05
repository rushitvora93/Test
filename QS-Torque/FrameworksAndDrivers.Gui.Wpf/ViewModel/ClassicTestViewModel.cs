using Core.Entities;
using Core.UseCases;
using FrameworksAndDrivers.Gui.Wpf.EventArgs;
using FrameworksAndDrivers.Gui.Wpf.Interfaces;
using FrameworksAndDrivers.Gui.Wpf.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using Common.Types.Enums;
using Core.Enums;
using InterfaceAdapters;
using InterfaceAdapters.Localization;
using InterfaceAdapters.Models;
using Syncfusion.Data.Extensions;
using System.ComponentModel;
using Client.Core.Entities;

namespace FrameworksAndDrivers.Gui.Wpf.ViewModel
{
    public class ClassicTestViewModel : BindableBase, ICanClose, ILocationGui, IShowEvaluation, IClassicTestDataErrorShower
    {
        public ClassicTestViewModel(IClassicTestUseCase classicTestUseCase,
            ILocationUseCase locationUseCase, ILocalizationWrapper localization, 
            IStartUp startUp, IClassicTestInterface classicTestInterface)
        {
            _classicTestUseCase = classicTestUseCase;
            _locationUseCase = locationUseCase;
            _startUp = startUp;
            _localization = localization;
            _classicTestInterface = classicTestInterface;
            LocationTree = new LocationTreeModel();

            LoadCommand = new RelayCommand(LoadExecute, LoadCanExecute);
            EvaluateDataComand = new RelayCommand(EvaluateDataExecute, EvaluateDataCheck);
            ChkControlledByTorqueCommand = new RelayCommand((obj) => RaisePropertyChanged(nameof(ChkHeaderClassicTests)));
            ChkControlledByAngleCommand = new RelayCommand((obj) => RaisePropertyChanged(nameof(ChkHeaderClassicTests)));
            MfuControlledByTorqueCommand = new RelayCommand((obj) => RaisePropertyChanged(nameof(MfuHeaderClassicTests)));
            MfuControlledByAngleCommand = new RelayCommand((obj) => RaisePropertyChanged(nameof(MfuHeaderClassicTests)));
            ToolTestingCommand = new RelayCommand(ToolTestingExecute, (obj) => true);
            ProcessTestingCommand = new RelayCommand(ProcessTestingExecute, (obj) => true);
            WireViewModelToInterfaceAdapters();
        }

        private void WireViewModelToInterfaceAdapters()
        {
            PropertyChangedEventManager.AddHandler(
                _classicTestInterface,
                (s, e) =>
                {
                    RaisePropertyChanged(nameof(ShowToolAssignmentLegend));
                    RaisePropertyChanged(nameof(PowToolClassicTests));
                },
                nameof(ClassicTestInterfaceAdapter.PowToolClassicTests));

            PropertyChangedEventManager.AddHandler(
                _classicTestInterface,
                (s, e) =>
                {
                    RaisePropertyChanged(nameof(IsMfuByControlVisible));
                    RaisePropertyChanged(nameof(IsMfuControlledByTorqueChecked));
                    RaisePropertyChanged(nameof(MfuHeaderClassicTests));
                },
                nameof(ClassicTestInterfaceAdapter.MfuHeaderClassicTests));

            PropertyChangedEventManager.AddHandler(
                _classicTestInterface,
                (s, e) =>
                {
                    RaisePropertyChanged(nameof(IsChkByControlVisible));
                    RaisePropertyChanged(nameof(IsChkControlledByTorqueChecked));
                    RaisePropertyChanged(nameof(ChkHeaderClassicTests));
                },
                nameof(ClassicTestInterfaceAdapter.MfuHeaderClassicTests));

            PropertyChangedEventManager.AddHandler(
                _classicTestInterface,
                (s, e) =>
                {
                    RaisePropertyChanged(nameof(ProcessHeaderClassicTests));
                },
                nameof(ClassicTestInterfaceAdapter.ProcessHeaderClassicTests));
        }

        #region Properties
        private readonly IStartUp _startUp;
        private readonly IClassicTestInterface _classicTestInterface;
        private readonly ILocationUseCase _locationUseCase;
        private readonly IClassicTestUseCase _classicTestUseCase;
        private ILocalizationWrapper _localization;
        private Dispatcher _guiDispatcher;
        public ObservableCollection<LocationModel> LocationModels = new ObservableCollection<LocationModel>();

        private LocationTreeModel _locationTree;
        public LocationTreeModel LocationTree
        {
            get => _locationTree;
            set => Set(ref _locationTree, value);
        }
        public RelayCommand LoadCommand { get; private set; }
        public RelayCommand EvaluateDataComand { get; private set; }
        public RelayCommand ChkControlledByTorqueCommand { get; }
        public RelayCommand ChkControlledByAngleCommand { get; }
        public RelayCommand MfuControlledByTorqueCommand { get; }
        public RelayCommand MfuControlledByAngleCommand { get; }
        public RelayCommand ToolTestingCommand { get; }
        public RelayCommand ProcessTestingCommand { get; }

        private LocationModel _selectedLocation;
        public LocationModel SelectedLocation
        {
            get => _selectedLocation; 
            set
            {
                _selectedLocation = value;

                if (ToolTestingChecked)
                {
                    IsMfuControlledByTorqueChecked = _selectedLocation == null || _selectedLocation.ControlledBy == LocationControlledBy.Torque;
                    IsChkControlledByTorqueChecked = _selectedLocation == null || _selectedLocation.ControlledBy == LocationControlledBy.Torque;
                    _classicTestUseCase.LoadToolsFromLocationTests(_selectedLocation?.Entity, this);
                }
                else
                {
                    _classicTestUseCase.LoadProcessHeaderFromLocation(_selectedLocation?.Entity, this);
                }
            }
        }

        private PowToolClassicTestHumbleModel _selectedTool;
        public PowToolClassicTestHumbleModel SelectedTool
        {
            get => _selectedTool; set
            {
                if (_selectedTool == value)
                {
                    return;
                }
                _selectedTool = value;

                _classicTestUseCase.LoadChkHeaderFromTool(_selectedTool?.Entity, this, _selectedLocation?.Entity);
                _classicTestUseCase.LoadMfuHeaderFromTool(_selectedTool?.Entity, this, _selectedLocation?.Entity);
            }
        }

        public bool CanEvaluateData
        {
            get
            {
                if (ToolTestingChecked)
                {
                    return selectedMfus.Count + selectedChks.Count > 0 && SelectedLocation != null && SelectedTool != null;
                }

                return selectedPfus.Count + selectedCtls.Count > 0 && SelectedLocation != null;
            }
        }

        public List<MfuHeaderClassicTestHumbleModel> selectedMfus = new List<MfuHeaderClassicTestHumbleModel>();
        public List<ChkHeaderClassicTestHumbleModel> selectedChks = new List<ChkHeaderClassicTestHumbleModel>();
        public List<ProcessHeaderClassicTestHumbleModel> selectedPfus = new List<ProcessHeaderClassicTestHumbleModel>();
        public List<ProcessHeaderClassicTestHumbleModel> selectedCtls = new List<ProcessHeaderClassicTestHumbleModel>();

        public bool IsChkByControlVisible =>
            _classicTestInterface.ChkHeaderClassicTests != null &&
            _classicTestInterface.ChkHeaderClassicTests
                .Count(x => x.Entity.ControlledByUnitId == MeaUnit.Nm) > 0 &&
            _classicTestInterface.ChkHeaderClassicTests
                .Count(x => x.Entity.ControlledByUnitId == MeaUnit.Deg) > 0;

        public bool IsMfuByControlVisible =>
            _classicTestInterface.MfuHeaderClassicTests != null &&
            _classicTestInterface.MfuHeaderClassicTests
                .Count(x => x.Entity.ControlledByUnitId == MeaUnit.Nm) > 0 && 
            _classicTestInterface.MfuHeaderClassicTests
                .Count(x => x.Entity.ControlledByUnitId == MeaUnit.Deg) > 0;

        private bool _isChkControlledByTorqueChecked; 
        public bool IsChkControlledByTorqueChecked
        {
            get => _isChkControlledByTorqueChecked;
            set => Set(ref _isChkControlledByTorqueChecked, value);
        }

        private bool _isMfuControlledByTorqueChecked;
        public bool IsMfuControlledByTorqueChecked 
        {
            get => _isMfuControlledByTorqueChecked;
            set => Set(ref _isMfuControlledByTorqueChecked, value);
        }

        public bool ShowToolAssignmentLegend
        {
            get  
            {
                if (_classicTestInterface.PowToolClassicTests == null)
                {
                    return false;
                }

                var oneToolIsAssigned = false;
                foreach (var tool in _classicTestInterface.PowToolClassicTests)
                {
                    oneToolIsAssigned = oneToolIsAssigned || tool.IsToolAssignmentActive;
                }

                return oneToolIsAssigned;
            }
        }

        public ObservableCollection<MfuHeaderClassicTestHumbleModel> MfuHeaderClassicTests
        {
            get
            {                
                if (!IsMfuByControlVisible)
                {
                    return _classicTestInterface.MfuHeaderClassicTests;
                }

                var allClassicMfuHeaderByControlled = new Dictionary<MeaUnit, ObservableCollection<MfuHeaderClassicTestHumbleModel>>
                {
                    [MeaUnit.Nm] = _classicTestInterface.MfuHeaderClassicTests.Where(x => x.Entity.ControlledByUnitId == MeaUnit.Nm).ToObservableCollection(),
                    [MeaUnit.Deg] = _classicTestInterface.MfuHeaderClassicTests.Where(x => x.Entity.ControlledByUnitId == MeaUnit.Deg).ToObservableCollection()
                };

                if (IsMfuControlledByTorqueChecked)
                {
                    return allClassicMfuHeaderByControlled[MeaUnit.Nm];
                }

                return allClassicMfuHeaderByControlled[MeaUnit.Deg];
            } 
        }

        public ObservableCollection<ChkHeaderClassicTestHumbleModel> ChkHeaderClassicTests
        {
            get
            {
                if (!IsChkByControlVisible)
                {
                    return _classicTestInterface.ChkHeaderClassicTests;
                }

                var allClassicChkHeaderByControlled = new Dictionary<MeaUnit, ObservableCollection<ChkHeaderClassicTestHumbleModel>>
                {
                    [MeaUnit.Nm] = _classicTestInterface.ChkHeaderClassicTests.Where(x => x.Entity.ControlledByUnitId == MeaUnit.Nm).ToObservableCollection(),
                    [MeaUnit.Deg] = _classicTestInterface.ChkHeaderClassicTests.Where(x => x.Entity.ControlledByUnitId == MeaUnit.Deg).ToObservableCollection()
                };

                if (IsChkControlledByTorqueChecked)
                {
                    return allClassicChkHeaderByControlled[MeaUnit.Nm];
                }

                return allClassicChkHeaderByControlled[MeaUnit.Deg];
            }
        }

        public ObservableCollection<PowToolClassicTestHumbleModel> PowToolClassicTests =>
            _classicTestInterface.PowToolClassicTests;

        public ObservableCollection<ProcessHeaderClassicTestHumbleModel> ProcessHeaderClassicTests =>
            _classicTestInterface.ProcessHeaderClassicTests;


        private bool _toolTestingChecked = true;
        public bool ToolTestingChecked
        {
            get => _toolTestingChecked;
            set
            {
                _toolTestingChecked = value;
                RaisePropertyChanged(nameof(ToolTestingChecked));
                RaisePropertyChanged(nameof(ProcessTestingChecked));
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
            }
        }

        public bool ShowCtlStatistics => FeatureToggles.FeatureToggles.ShowCtlStatistics;
        #endregion


        #region Events
        public event EventHandler<LocationTreeModel> InitializeLocationTreeRequest;
        public event EventHandler<MessageBoxEventArgs> MessageBoxRequest;
        #endregion

        #region Interface-Implementations
        #region not_used_yet
        public void AddLocation(Location location){}
        public void AddLocationDirectory(LocationDirectory locationDirectory){}
        public void AddLocationError(){}
        public void RemoveLocation(Location location) {}
        public void ShowAddLocationDirectoryError(string name) {}
        public void ShowCommentForLocation(string comment, LocationId locationId) {}
        public void LocationAlreadyExists() {}
        public void ShowPictureForLocation(Picture picture, LocationId locationId) {}
        public void ShowRemoveDirectoryError() {}
        public void ShowRemoveLocationError() {}
        public void UpdateLocation(Location location) {}
        public void UpdateLocationError() {}
        public void RemoveDirectory(LocationDirectoryId selectedDirectoryId) {}
        public void ChangeLocationParent(Location location, long newParentId) { }
        public void ChangeLocationParentError() { }
        public void ChangeLocationParent(Location location, LocationDirectoryId newParentId) { }

        public void ChangeLocationDirectoryParent(LocationDirectory directory, LocationDirectoryId newParentId) { }

        public void ChangeLocationDirectoryParentError() { }
        public void ShowChangeLocationToolAssignmentNotice(){}

        public void ChangeToolStatusAssistant(List<LocationToolAssignment> locationToolAssignments)
        {
            //Intentionally empty
        }

        public void ShowChangeToolStatusDialog(Action onSuccess, List<LocationToolAssignment> locationToolAssignments)
        {
            //Intentionally empty
        }

        #endregion

        public bool CanClose()
        {
            return true;
        }

        private bool LoadCanExecute(object arg) { return true; }

        private void LoadExecute(object obj)
        {
            _locationUseCase.LoadTree(this);
        }

        private bool EvaluateDataCheck(object arg)
        {
            return CanEvaluateData;
        }

        private void ToolTestingExecute(object obj)
        {
            IsMfuControlledByTorqueChecked = _selectedLocation == null || _selectedLocation.ControlledBy == LocationControlledBy.Torque;
            IsChkControlledByTorqueChecked = _selectedLocation == null || _selectedLocation.ControlledBy == LocationControlledBy.Torque;
            _classicTestUseCase.LoadToolsFromLocationTests(SelectedLocation?.Entity, this);
        }

        private void ProcessTestingExecute(object obj)
        {
            _classicTestUseCase.LoadProcessHeaderFromLocation(_selectedLocation?.Entity, this);
        }

        private void EvaluateDataExecute(object obj)
        {
            if (ProcessTestingChecked)
            {
                if (selectedCtls.Count > 0)
                {
                    var tests = new List<ClassicProcessTest>();
                    foreach (var item in selectedCtls)
                    {
                        tests.Add(item.Entity);
                    }

                    _classicTestUseCase.LoadValuesForClassicProcessHeader(SelectedLocation?.Entity, tests, false, this, this);
                }

                if (selectedPfus.Count > 0)
                {
                    var tests = new List<ClassicProcessTest>();
                    foreach (var item in selectedPfus)
                    {
                        tests.Add(item.Entity);
                    }

                    _classicTestUseCase.LoadValuesForClassicProcessHeader(SelectedLocation?.Entity, tests, true, this, this);
                }

                return; 
            }

            if (selectedChks.Count > 0)
            {
                var tests = new List<ClassicChkTest>();
                foreach (var item in selectedChks)
                {
                    tests.Add(item.Entity);
                }
                _classicTestUseCase.LoadValuesForClassicChkHeader(SelectedLocation?.Entity, SelectedTool.Entity, tests, this, this);
                return;
            }

            if(selectedMfus.Count > 0)
            {
                var tests = new List<ClassicMfuTest>();
                foreach (var item in selectedMfus)
                {
                    tests.Add(item.Entity);
                }
                _classicTestUseCase.LoadValuesForClassicMfuHeader(SelectedLocation?.Entity, SelectedTool.Entity, tests, this, this);
            }
        }
        public void ShowErrorMessage()
        {
            _guiDispatcher.Invoke(() =>
            {
                var args = new MessageBoxEventArgs((r) => { },
                    caption: _localization.Strings.GetParticularString("ClassicTestViewModel","An error occured while loading test data"),
                    text: _localization.Strings.GetParticularString("ClassicTestViewModel","Error"),
                    messageBoxButton: MessageBoxButton.OK,
                    messageBoxImage: MessageBoxImage.Error);
                MessageBoxRequest?.Invoke(this, args);
                _startUp.RaiseShowLoadingControl(false);
            });
        }

        public void ShowLoadingLocationTreeFinished()
        {
            _startUp.RaiseShowLoadingControl(false);
        }

        public void SetGuiDispatcher(Dispatcher guiDispatcher)
        {
            _guiDispatcher = guiDispatcher;
        }

        public void ShowLocationTree(List<LocationDirectory> directories)
        {
            _guiDispatcher.Invoke(() =>
            {
                LocationTree = new LocationTreeModel();
                directories.ForEach(x => LocationTree.LocationDirectoryModels.Add(LocationDirectoryHumbleModel.GetModelFor(x,_locationUseCase)));
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

        public void ShowValuesForClassicChkHeader(Location location, Tool tool, List<ClassicChkTest> chktest)
        {
            _startUp.OpenClassicChkTestHtmlView(location, tool, chktest)?.Show();              
        }

        public void ShowValuesForClassicMfuHeader(Location location, Tool tool, List<ClassicMfuTest> mfutest)
        {
            _startUp.OpenClassicMfuTestHtmlView(location, tool, mfutest)?.Show();
        }

        public void ShowValuesForClassicProcessHeader(Location location, List<ClassicProcessTest> tests, bool isPfu)
        {
            if (!isPfu)
            {
                _startUp.OpenClassicProcessMonitoringTestHtmlView(location, tests)?.Show();
                return;
            }

            _startUp.OpenClassicProcessPfuTestHtmlView(location, tests)?.Show();

        }

        #endregion
    }

}