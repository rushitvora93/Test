using Core.UseCases;
using FrameworksAndDrivers.Gui.Wpf.View;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Data;
using FrameworksAndDrivers.Localization;
using InterfaceAdapters;
using InterfaceAdapters.Localization;
using FrameworksAndDrivers.Gui.Wpf.EventArgs;
using System.Windows;

namespace FrameworksAndDrivers.Gui.Wpf.ViewModel.Controls
{
	public class GlobalTreeViewModel : BindableBase, ISessionInformationGui, IGetUpdatedByLanguageChanges, ISessionInformationErrorHandler
    {
		private ILocalizationWrapper _localization;
		private IStartUp _startUp;

		public class HelperTableActionItem
		{
			public string Name { get; set; }
			public RelayCommand Action { get; set; }
		}

		#region Properties
		ISessionInformationUseCase _useCase;

        private bool _isTreeExpanded;
        public bool IsTreeExpanded
        {
            get => _isTreeExpanded;
            set
            {
                if (IsPinned && !value)
                {
                    throw new InvalidOperationException("The Tree cannot be collapsed if it is pinned");
                }
                else
                {
                    Set(ref _isTreeExpanded, value);
                }
            }
        }

        private bool _isPinned;
        public bool IsPinned
        {
            get => _isPinned;
            set
            {
                _useCase.SetMegaMainMenuIsPinned(this, value);
                Set(ref _isPinned, value);
            }
        }

        private string _userName;
        public string UserName
        {
            get => _userName;
            set => Set(ref _userName, value);
        }

        private string _serverName;
        public string ServerName
        {
            get => _serverName;
            set => Set(ref _serverName, value);
        }

        private string _groupName;
        public string GroupName
        {
            get => _groupName;
            set => Set(ref _groupName, value);
        }

        private string _areaName;
        public string AreaName
        {
            get => _areaName;
            set => Set(ref _areaName, value);
        }

        private string _qstVersion;
        public string QstVersion
        {
            get => _qstVersion;
            set => Set(ref _qstVersion, value);
        }

        private bool _isLoadingControlVisible;

        public bool IsLoadingControlVisible
        {
            get => _isLoadingControlVisible;
            set => Set(ref _isLoadingControlVisible, value);
        }

        private ObservableCollection<HelperTableActionItem> HelperTableCollectionProperty { get; set; }
		public ListCollectionView HelperTableCollection { get; private set; }
		#endregion


        #region Interface
        public void ShowUserName(string userName)
        {
            UserName = userName;
        }

        public void ShowServerName(string serverName)
        {
            ServerName = serverName;
        }

        public void ShowGroupName(string groupName)
        {
            GroupName = groupName;
        }

        public void ShowAreaName(string areaName)
        {
            AreaName = areaName;
        }

        public void ShowQstVersion(string qstVersion)
        {
            QstVersion = qstVersion;
        }

        public void LanguageUpdate()
        {
            FillHelperTableCollection();
        }

        public void LoadMegaMainMenuIsPinned(bool isPinned)
        {
            _isPinned = isPinned;
            RaisePropertyChanged(nameof(IsPinned));
            IsTreeExpanded = true;
        }
        #endregion


        #region Commands
        public RelayCommand TogglePinCommand { get; private set; }
        public RelayCommand LoadedCommand { get; private set; }
        private bool LoadedCanExecute(object arg) { return true; }
        private bool TogglePinCanExecute(object arg) { return true; }
        private void TogglePinExecute(object obj) { IsPinned = !IsPinned; }

        public RelayCommand ExpandTreeCommand { get; private set; }
        private bool ExpandTreeCanExecute(object arg) { return !IsTreeExpanded && !IsPinned; }
        private void ExpandTreeExecute(object obj) { IsTreeExpanded = true; }

        public RelayCommand CollapseTreeCommand { get; private set; }
        private bool CollapseTreeCanExecute(object arg) { return IsTreeExpanded && !IsPinned; }
        private void CollapseTreeExecute(object obj) { IsTreeExpanded = false; }

        public RelayCommand HomeCommand { get; private set; }
        private bool HomeCanExecute(object arg) { return true; }
        private void HomeExecute(object obj)
        {
            RaiseTreeWindowSelectionChanged(new StartView());

            if (!IsPinned)
            {
                IsTreeExpanded = false; 
            }
        }

        public RelayCommand OpenLocationViewCommand { get; private set; }
        private bool OpenLocationViewCanExecute(object arg) { return true; }
        private void OpenLocationViewExecute(object obj)
        {
            RaiseTreeWindowSelectionChanged(_startUp.OpenLocation());
        }

        public RelayCommand OpenToolModelViewCommand { get; private set; }
        private bool OpenToolModelViewCanExecute(object arg) { return true; }
        private void OpenToolModelViewExecute(object obj)
        {
            RaiseTreeWindowSelectionChanged(_startUp.OpenToolModel());
        }

        public RelayCommand OpenToolViewCommand { get; private set; }
        private bool OpenToolViewCanExecute(object arg) { return true; }
        private void OpenToolViewExecute(object obj)
        {
            RaiseTreeWindowSelectionChanged(_startUp.OpenTool());
        }

        public RelayCommand OpenTestEquipmentViewCommand { get; private set; }
        private bool OpenTestEquipmentViewCanExecute(object arg) { return true; }
        private void OpenTestEquipmentViewExecute(object obj)
        {
            RaiseTreeWindowSelectionChanged(_startUp.OpenTestEquipment());
        }

        public RelayCommand OpenTestLevelSetViewCommand { get; private set; }
        private bool OpenTestPlanViewCanExecute(object arg) { return true; }
        private void OpenTestPlanViewExecute(object obj)
        {
            RaiseTreeWindowSelectionChanged(_startUp.OpenTestLevelSet());
        }

        public RelayCommand OpenTransfertToTestEquipmentView { get; private set; }
        private bool OpenTransfertToTestEquipmentViewCanExecute(object arg){ return true; }
        private void OpenTransfertToTestEquipmentViewExectue(object obj) { RaiseTreeWindowSelectionChanged(_startUp.OpenTransferToTestEquipment());}

        public RelayCommand OpenLocationToolAssignmentCommand { get; private set; }
        private bool OpenLocationToolAssignmentCanExecute(object arg) { return true; }

        private void OpenLocationToolAssignmentExecute(object obj)
        {
            RaiseTreeWindowSelectionChanged(_startUp.OpenLocationToolAssignment());
        }

        public RelayCommand OpenProcessControlCommand { get; private set; }
        private bool OpenProcessControlCanExecute(object arg) { return true; }

        private void OpenProcessControlExecute(object obj)
        {
            RaiseTreeWindowSelectionChanged(_startUp.OpenProcessControl());
        }

        public RelayCommand OpenTrashCommand { get; private set; }
        private bool OpenTrashCanExecute(object arg) { return true; }

        private void OpenTrashExecute(object obj)
        {
            RaiseTreeWindowSelectionChanged(_startUp.OpenTrash());
        }

        public RelayCommand OpenClassicTestViewCommand { get; private set; }
        private bool OpenClassicTestCanExecute(object arg) { return true; }
        private void OpenClassicTestExecute(object obj)
        {
            RaiseTreeWindowSelectionChanged(_startUp.OpenClassicTest());
        }


        public RelayCommand OpenToolTestPlanningViewCommand { get; private set; }
        private bool OpenToolTestPlanningViewCanExecute(object arg) { return true; }
        private void OpenToolTestPlanningViewExecute(object obj)
        {
            RaiseTreeWindowSelectionChanged(_startUp.OpenToolTestPlanning());
        }
        
        public RelayCommand OpenProcessControlPlanningViewCommand { get; private set; }
        private bool OpenProcessControlPlanningViewCanExecute(object arg) { return true; }
        private void OpenProcessControlPlanningViewExecute(object obj)
        {
            RaiseTreeWindowSelectionChanged(_startUp.OpenProcessControlPlanning());
        }

        public RelayCommand OpenLocationHistoryViewCommand { get; private set; }
        private bool OpenLocationHistoryViewCanExecute(object arg) { return FeatureToggles.FeatureToggles.ShowToolTestPlanning; }
        private void OpenLocationHistoryViewExecute(object obj)
        {
            RaiseTreeWindowSelectionChanged(_startUp.OpenLocationHistoryView());
        }
        #endregion


        #region Methods
        public void SetLocalizationWrapper(LocalizationWrapper localization)
		{
			_localization = localization;
			FillHelperTableCollection();
		}

		public void SetStartUp(IStartUp startUp)
		{
			_startUp = startUp;
		}

        internal void LoadSessionInformation()
        {
            _useCase.LoadUserData();
        }

		private void OpenShutOffHelperTable()
		{
			RaiseTreeWindowSelectionChanged(_startUp.OpenShutOffHelperTable());
		}

		private void OpenSwitchOffHelperTable()
		{
			RaiseTreeWindowSelectionChanged(_startUp.OpenSwitchOffHelperTable());
		}

		private void OpenDriveSizeHelperTable()
		{
			RaiseTreeWindowSelectionChanged(_startUp.OpenDriveSizeHelperTable());
		}

		private void OpenDriveTypeHelperTable()
		{
			RaiseTreeWindowSelectionChanged(_startUp.OpenDriveTypeHelperTable());
		}

		private void OpenToolTypeHelperTable()
		{
			RaiseTreeWindowSelectionChanged(_startUp.OpenToolTypeHelperTable());
		}

		private void OpenConstructionTypeHelperTable()
		{
			RaiseTreeWindowSelectionChanged(_startUp.OpenConstructionTypeHelperTable());
		}

		private void OpenStatusHelperTable()
		{
			RaiseTreeWindowSelectionChanged(_startUp.OpenStatusHelperTable());
		}

        public void OpenManufacturerTable()
        {
            RaiseTreeWindowSelectionChanged(_startUp.OpenManufacturer());
        }

        private void OpenToleranceClassTable()
        {
            RaiseTreeWindowSelectionChanged(_startUp.OpenToleranceClass());
        }

        private void OpenReasonForToolChangeHelperTable()
        {
			RaiseTreeWindowSelectionChanged(_startUp.OpenReasonForToolChangeHelperTable());
        }

        private void OpenConfigurableFieldHelperTable()
        {
			RaiseTreeWindowSelectionChanged(_startUp.OpenConfigurableFieldHelperTable());
        }

        private void OpenCostCenterTable()
        {
            RaiseTreeWindowSelectionChanged(_startUp.OpenCostCenterHelperTable());
        }

        private void OpenToolUsageHelperTable()
        {
            RaiseTreeWindowSelectionChanged(_startUp.OpenToolUsage());
        }

        private void OpenExtensionHelperTable()
        {
            RaiseTreeWindowSelectionChanged(_startUp.OpenExtension());
        }

        protected virtual void RaiseTreeWindowSelectionChanged(UserControl e)
		{
			TreeWindowSelectionChanged?.Invoke(this, e);
        }

        private void FillHelperTableCollection()
        {
            if (_localization == null)
            {
                return;
            }

            HelperTableCollectionProperty.Clear();
            var items = new ObservableCollection<HelperTableActionItem>
            {
                new HelperTableActionItem
                {
                    Name = _localization.Strings.GetParticularString("Auxiliary Master Data", "Shut Off"),
                    Action = new RelayCommand((obj) => { OpenShutOffHelperTable(); })
                },
                new HelperTableActionItem
                {
                    Name = _localization.Strings.GetParticularString("Auxiliary Master Data", "Switch Off"),
                    Action = new RelayCommand((obj) => { OpenSwitchOffHelperTable(); })
                },
                new HelperTableActionItem
                {
                    Name = _localization.Strings.GetParticularString("Auxiliary Master Data", "Drive Size"),
                    Action = new RelayCommand((obj) => { OpenDriveSizeHelperTable(); })
                },
                new HelperTableActionItem
                {
                    Name = _localization.Strings.GetParticularString("Auxiliary Master Data", "Drive Type"),
                    Action = new RelayCommand((obj) => { OpenDriveTypeHelperTable(); })
                },
                new HelperTableActionItem
                {
                    Name = _localization.Strings.GetParticularString("Auxiliary Master Data", "Tool Type"),
                    Action = new RelayCommand((obj) => { OpenToolTypeHelperTable(); })
                },
                new HelperTableActionItem
                {
                    Name = _localization.Strings.GetParticularString("Auxiliary Master Data", "Construction Type"),
                    Action = new RelayCommand((obj) => { OpenConstructionTypeHelperTable(); })
                },
                new HelperTableActionItem
                {
                    Name = _localization.Strings.GetParticularString("Auxiliary Master Data", "Status"),
                    Action = new RelayCommand((obj) => { OpenStatusHelperTable(); })
                },
                new HelperTableActionItem
                {
                    Name = _localization.Strings.GetParticularString("Auxiliary Master Data", "Reason for tool change"),
                    Action = new RelayCommand((obj) => { OpenReasonForToolChangeHelperTable(); })
                },
                new HelperTableActionItem
                {
                    Name = _localization.Strings.GetParticularString("Auxiliary Master Data", "Configurable field tool"),
                    Action = new RelayCommand((obj) => { OpenConfigurableFieldHelperTable(); })
                },
            };

            foreach (var item in items)
            {
                HelperTableCollectionProperty.Add(item);
            }

            HelperTableCollectionProperty.Add(new HelperTableActionItem
            {
                Name = _localization.Strings.GetParticularString("Auxiliary Master Data", "Manufacturer"),
                Action = new RelayCommand((obj) => { OpenManufacturerTable(); })
            });

            HelperTableCollectionProperty.Add(new HelperTableActionItem
            {
                Name = _localization.Strings.GetString("Tolerance class"),
                Action = new RelayCommand((obj) => OpenToleranceClassTable())
            });
            HelperTableCollectionProperty.Add(new HelperTableActionItem
            {
                Name = _localization.Strings.GetParticularString("Auxiliary Master Data", "Cost center"),
                Action = new RelayCommand((obj) => OpenCostCenterTable())
            });
            HelperTableCollectionProperty.Add(new HelperTableActionItem
            {
                Name = _localization.Strings.GetParticularString("Auxiliary Master Data", "Tool usage"),
                Action = new RelayCommand((obj) => { OpenToolUsageHelperTable(); })
            });
            
            HelperTableCollectionProperty.Add(new HelperTableActionItem
            {
                Name = _localization.Strings.GetParticularString("Auxiliary Master Data", "Extension"),
                Action = new RelayCommand((obj) => { OpenExtensionHelperTable(); })
            });

            HelperTableCollection = new ListCollectionView(HelperTableCollectionProperty);
        }

        private void LoadedExecute(object obj)
        {
            _useCase.LoadMegaMainMenuIsPinned(this);           
        }

        #endregion


        #region Events
        public event EventHandler<UserControl> TreeWindowSelectionChanged;

        public event EventHandler<MessageBoxEventArgs> MessageBoxRequest;
        #endregion

        public GlobalTreeViewModel(ISessionInformationUseCase sessionInformationUseCase, ILocalizationWrapper localization, IStartUp startUp)
		{
			_useCase = sessionInformationUseCase;
			_localization = localization;
			_startUp = startUp;

			HelperTableCollectionProperty = new ObservableCollection<HelperTableActionItem>();
			HelperTableCollection = new ListCollectionView(HelperTableCollectionProperty);

            TogglePinCommand = new RelayCommand(TogglePinExecute, TogglePinCanExecute);
            ExpandTreeCommand = new RelayCommand(ExpandTreeExecute, ExpandTreeCanExecute);
            CollapseTreeCommand = new RelayCommand(CollapseTreeExecute, CollapseTreeCanExecute);
            HomeCommand = new RelayCommand(HomeExecute, HomeCanExecute);
			OpenLocationViewCommand = new RelayCommand(OpenLocationViewExecute, OpenLocationViewCanExecute);
			OpenToolModelViewCommand = new RelayCommand(OpenToolModelViewExecute, OpenToolModelViewCanExecute);
            OpenToolViewCommand = new RelayCommand(OpenToolViewExecute, OpenToolViewCanExecute);
            OpenTestEquipmentViewCommand = new RelayCommand(OpenTestEquipmentViewExecute, OpenTestEquipmentViewCanExecute);
            OpenTestLevelSetViewCommand = new RelayCommand(OpenTestPlanViewExecute, OpenTestPlanViewCanExecute);
            OpenToolTestPlanningViewCommand = new RelayCommand(OpenToolTestPlanningViewExecute, OpenToolTestPlanningViewCanExecute);
            OpenTransfertToTestEquipmentView = new RelayCommand(OpenTransfertToTestEquipmentViewExectue, OpenTransfertToTestEquipmentViewCanExecute);
            OpenLocationToolAssignmentCommand = new RelayCommand(OpenLocationToolAssignmentExecute, OpenLocationToolAssignmentCanExecute);
            OpenProcessControlCommand = new RelayCommand(OpenProcessControlExecute, OpenProcessControlCanExecute);
            OpenClassicTestViewCommand = new RelayCommand(OpenClassicTestExecute, OpenClassicTestCanExecute);
            OpenProcessControlPlanningViewCommand = new RelayCommand(OpenProcessControlPlanningViewExecute, OpenProcessControlPlanningViewCanExecute);
            OpenLocationHistoryViewCommand = new RelayCommand(OpenLocationHistoryViewExecute, OpenLocationHistoryViewCanExecute);
            LoadedCommand = new RelayCommand(LoadedExecute, LoadedCanExecute);
            OpenTrashCommand = new RelayCommand(OpenTrashExecute, OpenTrashCanExecute);
            FillHelperTableCollection();
		}

        public void ShowLoadingControl(object sender, bool showLoadingControl)
        {
            IsLoadingControlVisible = showLoadingControl;
        }

        public void ShowMegaMenuLockingError()
        {
            var args = new MessageBoxEventArgs(r => { },
                _localization.Strings.GetParticularString("MegaMenuLocking", "Error occured while loading Pinned status"),
                _localization.Strings.GetParticularString("MegaMenuLocking", "Error"),
                MessageBoxButton.OK, MessageBoxImage.Error);
            MessageBoxRequest?.Invoke(this, args);
        }
    }
}
