using Core.Entities;
using Core.UseCases;
using Core.UseCases.Communication;
using FrameworksAndDrivers.Gui.Wpf;
using FrameworksAndDrivers.Gui.Wpf.Assistent;
using FrameworksAndDrivers.Gui.Wpf.CefUtils;
using FrameworksAndDrivers.Gui.Wpf.Model;
using FrameworksAndDrivers.Gui.Wpf.Validator;
using FrameworksAndDrivers.Gui.Wpf.View;
using FrameworksAndDrivers.Gui.Wpf.View.Dialogs;
using FrameworksAndDrivers.Gui.Wpf.View.Themes;
using FrameworksAndDrivers.Gui.Wpf.ViewModel;
using FrameworksAndDrivers.Gui.Wpf.ViewModel.Controls;
using FrameworksAndDrivers.Localization;
using FrameworksAndDrivers.Threads;
using InterfaceAdapters;
using StartUp.AssistentCreator;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Client.Core;
using Client.Core.Entities;
using Client.Core.Validator;
using Client.UseCases.UseCases;
using Common.Types.Enums;
using Core;
using FrameworksAndDrivers.Gui.Wpf.ViewModel.HtmlViewModel;
using InterfaceAdapters.Communication;
using InterfaceAdapters.Models;

namespace StartUp
{
    public class UseCaseCollection
    {
        public IHelperTableUseCase<ShutOff> shutOff;
        public HelperTableInterfaceAdapter<ShutOff> shutOffGuiAdapter;

        public IHelperTableUseCase<SwitchOff> switchOff;
        public HelperTableInterfaceAdapter<SwitchOff> switchOffGuiAdapter;

        public IHelperTableUseCase<DriveSize> driveSize;
        public HelperTableInterfaceAdapter<DriveSize> driveSizeGuiAdapter;

        public IHelperTableUseCase<DriveType> driveType;
        public HelperTableInterfaceAdapter<DriveType> driveTypeGuiAdapter;

        public IHelperTableUseCase<ToolType> toolType;
        public HelperTableInterfaceAdapter<ToolType> toolTypeGuiAdapter;

        public IHelperTableUseCase<ReasonForToolChange> reasonForToolChange;
        public HelperTableInterfaceAdapter<ReasonForToolChange> reasonForToolChangeGuiAdapter;

        public IHelperTableUseCase<Status> status;
        public HelperTableInterfaceAdapter<Status> statusGuiAdapter; // remove with HelperTablesWithInterfaceAdapter
        public HelperTableInterface<Status, string> statusGuiAdapterNew;

        public IHelperTableUseCase<ConstructionType> constructionType;
        public HelperTableInterfaceAdapter<ConstructionType> constructionTypeGuiAdapter;

        public IHelperTableUseCase<ConfigurableField> configurableField;
        public HelperTableInterfaceAdapter<ConfigurableField> configurableFieldGuiAdapter;

        public IHelperTableUseCase<CostCenter> costCenter;
        public HelperTableInterfaceAdapter<CostCenter> costCenterGuiAdapter;

        public IHelperTableUseCase<ToolUsage> toolUsage;
        public HelperTableInterfaceAdapter<ToolUsage> toolUsageGuiAdapter;

        public IManufacturerUseCase manufacturer;
        public ManufacturerInterfaceAdapter manufacturerGuiAdapter;

        public ISaveColumnsUseCase saveColumns;
        public SaveColumnsInterfaceAdapter saveColumnsGuiAdapter;

        public IToleranceClassUseCase toleranceClass;
        public ToleranceClassInterfaceAdapter toleranceClassGuiAdapter;

        public QstInformationUseCase qstInformation;
        public QstInformationGuiProxy qstInformationGuiAdapter;

        public LoginUseCase login;
        public LoginGuiProxy loginGuiAdapter;

        public ILanguageUseCase language;
        public LanguageInterfaceAdapter LanguageInterface;

        public ISessionInformationUseCase sessionInformation;
        public SessionInformationGuiProxy sessionInformationGuiAdapter;

        public IToolModelUseCase toolModel;
        public ToolModelInterfaceAdapter toolModelGuiAdapter;

        public IToolUseCase tool;
        public ToolInterfaceAdapter toolGuiAdapter;

        public ILocationUseCase location;
        public LocationInterfaceAdapter locationGuiAdapter;

        public ITrashUseCase trash;
        public TrashInterfaceAdapter trashGuiAdapter;

        public IWorkingCalendarUseCase workingCalendar;
        public WorkingCalendarInterfaceAdapter workingCalendarInterface;

        public IShiftManagementUseCase shiftManagement;
        public ShiftManagementInterfaceAdapter shiftManagementInterface;

        public ITestLevelSetUseCase TestLevelSet;
        public TestLevelSetInterfaceAdapter TestLevelSetInterface;

        public ITestLevelSetAssignmentUseCase TestLevelSetAssignment;
        public TestLevelSetAssignmentInterfaceAdapter TestLevelSetAssignmentInterface;

        public ITestDateCalculationUseCase TestDateCalculation;

        public ILocationToolAssignmentUseCase locationToolAssignment;
        public LocationToolAssignmentInterfaceAdapter locationToolAssignmentGuiAdapter;

        public TestEquipmentInterfaceAdapter testEquipmentInterface;
        public ITestEquipmentUseCase testEquipment;

        public TransferToTestEquipmentGuiProxy transferToTestEquipmentGuiAdapter;
        public ITransferToTestEquipmentUseCase TransferToTestEquipment;

        public ClassicTestInterfaceAdapter classicTestInterfaceAdapter;
        public IClassicTestUseCase classicTest;

        public HistoryInterfaceAdapter historyInterfaceAdapter;
        public IHistoryUseCase history;

        public ILogoutUseCase logout;
        public LogoutGuiProxy logoutGuiProxy;

        public IToolDisplayFormatter toolDisplayFormatter;
        public ILocationDisplayFormatter LocationDisplayFormatter;
        public ILocationToolAssignmentDisplayFormatter locationToolAssignmentDisplayFormatter;
        public ITestEquipmentDisplayFormatter testEquipmentDisplayFormatter;

        public IChangeToolStateForLocationUseCase changeToolStateForLocationUseCase;
        public ChangeToolStateForLocationGuiProxy changeToolStateForLocationGuiProxy;

        public ITreePathBuilder treePathBuilder;

        public IProcessControlUseCase processControl;

        public ProcessControlInterfaceAdapter processControlInterfaceAdapter;

        public IExtensionUseCase extension;
        public ExtensionInterfaceAdapter extensionInterface;

        public ITrashUseCase trashUseCase;
        public TrashInterfaceAdapter trashInterface;
    }

    public class StartUpImpl : IStartUp
    {
        static StartUpImpl()
        {
            AppDomain.CurrentDomain.AssemblyResolve += CefHumbleInitializer.Resolver;
            CefHumbleInitializer.InitCef();
            SyncfusionHumbleInitializer.InitSyncfusion();
        }

        public ThemeDictionary ThemeDictionary { get; private set; }

        public int ShowLoadingControlRefernceCounter = 0;
        public event EventHandler<bool> ShowLoadingControlEvent;

        public StartUpImpl(UseCaseCollection useCases, ThemeDictionary themeDictionary, LocalizationWrapper localization)
        {
            _useCases = useCases;
            _localization = localization;
            ThemeDictionary = themeDictionary;
        }

        public HelperTableView OpenShutOffHelperTable()
        {
            Func<string> getHelperTableName = () => _localization.Strings.GetParticularString("Auxiliary Master Data", "Shut Off");
            var viewModel = new HelperTableViewModel<ShutOff, string>(this,
                                                                      _useCases.shutOff,
                                                                      null,
                                                                      getHelperTableName,
                                                                      e => HelperTableItemModel.GetModelForShutOff(e),
                                                                      m => m.Entity,
                                                                      (m, e) => m.Value = e.Value.ToDefaultString(),
                                                                      () => new ShutOff(),
                                                                      _localization);
            viewModel.HasReferencedToolModels = true;
            _useCases.shutOffGuiAdapter.RegisterGuiInterface(viewModel);
            _localization.Subscribe(viewModel);
            var userControl = new HelperTableView(viewModel, _localization);
            return userControl;
        }

        public void OpenShutOffHelperTableDialog(Window owner = null)
        {
            var view = OpenShutOffHelperTable();
            OpenDialog(view, owner, _useCases.shutOffGuiAdapter, (IHelperTableGui<ShutOff>)view.DataContext);
        }

        /// <summary>
        /// Opens a dialog with a given UserControl
        /// </summary>
        /// <typeparam name="GuiInterface">Type of the gui Interface</typeparam>
        /// <param name="view">UserControl which should be displayed</param>
        /// <param name="owner">Window which the Dialog should block. Null if no blocking is needed</param>
        /// <param name="interfaceAdapter">InterfaceAdapter</param>
        /// <param name="guiInterface"></param>
        private static void OpenDialog<GuiInterface>(UserControl view, Window owner, IInterfaceAdapter<GuiInterface> interfaceAdapter, GuiInterface guiInterface)
        {
            var window = new Window()
            {
                Content = view,
                Owner = owner,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            window.Width = 1024;
            window.Closed += (sender, args) => interfaceAdapter.RemoveGuiInterface(guiInterface);
            window.ShowDialog();
        }

        private static void OpenDialog(UserControl view, Window owner)
        {
            var window = new Window()
            {
                Content = view,
                Owner = owner,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            window.ShowDialog();
        }

        public HelperTableView OpenSwitchOffHelperTable()
        {
            Func<string> getHelperTableName = () => _localization.Strings.GetParticularString("Auxiliary Master Data", "Switch Off");
            var viewModel = new HelperTableViewModel<SwitchOff, string>(this,
                                                                        _useCases.switchOff,
                                                                        null,
                                                                        getHelperTableName,
                                                                        e => HelperTableItemModel.GetModelForSwitchOff(e),
                                                                        m => m.Entity,
                                                                        (m, e) => m.Value = e.Value.ToDefaultString(),
                                                                        () => new SwitchOff(),
                                                                        _localization);
            viewModel.HasReferencedToolModels = true;
            _useCases.switchOffGuiAdapter.RegisterGuiInterface(viewModel);
            _localization.Subscribe(viewModel);
            var userControl = new HelperTableView(viewModel, _localization);
            return userControl;
        }

        public void OpenSwitchOffHelperTableDialog(Window owner = null)
        {
            var view = OpenSwitchOffHelperTable();
            OpenDialog(view, owner, _useCases.switchOffGuiAdapter, (IHelperTableGui<SwitchOff>)view.DataContext);
        }

        public HelperTableView OpenDriveSizeHelperTable()
        {
            Func<string> getHelperTableName = () => _localization.Strings.GetParticularString("Auxiliary Master Data", "Drive Size");
            var viewModel = new HelperTableViewModel<DriveSize, string>(this,
                                                                        _useCases.driveSize,
                                                                        null,
                                                                        getHelperTableName,
                                                                        e => HelperTableItemModel.GetModelForDriveSize(e),
                                                                        m => m.Entity,
                                                                        (m, e) => m.Value = e.Value.ToDefaultString(),
                                                                        () => new DriveSize(),
                                                                        _localization);
            viewModel.HasReferencedToolModels = true;
            _useCases.driveSizeGuiAdapter.RegisterGuiInterface(viewModel);
            _localization.Subscribe(viewModel);
            var userControl = new HelperTableView(viewModel, _localization);
            return userControl;
        }

        public void OpenDriveSizeHelperTableDialog(Window owner = null)
        {
            var view = OpenDriveSizeHelperTable();
            OpenDialog(view, owner, _useCases.driveSizeGuiAdapter, (IHelperTableGui<DriveSize>)view.DataContext);
        }

        public HelperTableView OpenDriveTypeHelperTable()
        {
            Func<string> getHelperTableName = () => _localization.Strings.GetParticularString("Auxiliary Master Data", "Drive Type");
            var viewModel = new HelperTableViewModel<DriveType, string>(this,
                                                                        _useCases.driveType,
                                                                        null,
                                                                        getHelperTableName,
                                                                        e => HelperTableItemModel.GetModelForDriveType(e),
                                                                        m => m.Entity,
                                                                        (m, e) => m.Value = e.Value.ToDefaultString(),
                                                                        () => new DriveType(),
                                                                        _localization);
            viewModel.HasReferencedToolModels = true;
            _useCases.driveTypeGuiAdapter.RegisterGuiInterface(viewModel);
            _localization.Subscribe(viewModel);
            var userControl = new HelperTableView(viewModel, _localization);
            return userControl;
        }

        public void OpenDriveTypeHelperTableDialog(Window owner = null)
        {
            var view = OpenDriveTypeHelperTable();
            OpenDialog(view, owner, _useCases.driveTypeGuiAdapter, (IHelperTableGui<DriveType>)view.DataContext);
        }

        public HelperTableView OpenToolTypeHelperTable()
        {
            Func<string> getHelperTableName = () => _localization.Strings.GetParticularString("Auxiliary Master Data", "Tool Type");
            var viewModel = new HelperTableViewModel<ToolType, string>(this,
                                                                       _useCases.toolType,
                                                                       null,
                                                                       getHelperTableName,
                                                                       e => HelperTableItemModel.GetModelForToolType(e),
                                                                       m => m.Entity,
                                                                       (m, e) => m.Value = e.Value.ToDefaultString(),
                                                                       () => new ToolType(),
                                                                       _localization);
            viewModel.HasReferencedToolModels = true;
            _useCases.toolTypeGuiAdapter.RegisterGuiInterface(viewModel);
            _localization.Subscribe(viewModel);
            var userControl = new HelperTableView(viewModel, _localization);
            return userControl;
        }

        public void OpenToolTypeHelperTableDialog(Window owner = null)
        {
            var view = OpenToolTypeHelperTable();
            OpenDialog(view, owner, _useCases.toolTypeGuiAdapter, (IHelperTableGui<ToolType>)view.DataContext);
        }

        public HelperTableView OpenStatusHelperTable()
        {
            Func<string> getHelperTableName = () => _localization.Strings.GetParticularString("Auxiliary Master Data", "Status");
            var viewModel = new HelperTableViewModel<Status, string>(this,
                _useCases.status,
                FeatureToggles.FeatureToggles.HelperTablesWithInterfaceAdapter
                    ? _useCases.statusGuiAdapterNew
                    : null,
                getHelperTableName,
                e => HelperTableItemModel.GetModelForStatus(e),
                m => m.Entity,
                (m, e) => m.Value = e.Value.ToDefaultString(),
                () => new Status(),
                _localization);
            viewModel.HasReferencedTools = true;
            if (!FeatureToggles.FeatureToggles.HelperTablesWithInterfaceAdapter)
            {
                _useCases.statusGuiAdapter.RegisterGuiInterface(viewModel);
            }
            _localization.Subscribe(viewModel);
            var userControl = new HelperTableView(viewModel, _localization);
            return userControl;
        }

        public void OpenStatusHelperTableDialog(Window owner = null)
        {
            var view = OpenStatusHelperTable();
            if (!FeatureToggles.FeatureToggles.HelperTablesWithInterfaceAdapter)
            {
                OpenDialog(view, owner, _useCases.statusGuiAdapter, (IHelperTableGui<Status>)view.DataContext);
            }
        }

        public HelperTableView OpenReasonForToolChangeHelperTable()
        {
            Func<string> getHelperTableName = () => _localization.Strings.GetParticularString("Auxiliary Master Data", "Reason for tool change");
            var viewModel = new HelperTableViewModel<ReasonForToolChange, string>(this,
                                                                                  _useCases.reasonForToolChange,
                                                                                  null,
                                                                                  getHelperTableName,
                                                                                  e => HelperTableItemModel.GetModelForReasonForToolChange(e),
                                                                                  m => m.Entity,
                                                                                  (m, e) => m.Value = e.Value.ToDefaultString(),
                                                                                  () => new ReasonForToolChange(),
                                                                                  _localization);
            _useCases.reasonForToolChangeGuiAdapter.RegisterGuiInterface(viewModel);
            _localization.Subscribe(viewModel);
            var userControl = new HelperTableView(viewModel, _localization);
            return userControl;
        }

        public void OpenReasonForToolChangeHelperTableDialog(Window owner = null)
        {
            var view = OpenReasonForToolChangeHelperTable();
            OpenDialog(view, owner, _useCases.reasonForToolChangeGuiAdapter, (IHelperTableGui<ReasonForToolChange>)view.DataContext);
        }

        public HelperTableView OpenConstructionTypeHelperTable()
        {
            Func<string> getHelperTableName = () => _localization.Strings.GetParticularString("Auxiliary Master Data", "Construction Type");
            var viewModel = new HelperTableViewModel<ConstructionType, string>(this,
                                                                               _useCases.constructionType,
                                                                               null,
                                                                               getHelperTableName,
                                                                               e => HelperTableItemModel.GetModelForConstructionType(e),
                                                                               m => m.Entity,
                                                                               (m, e) => m.Value = e.Value.ToDefaultString(),
                                                                               () => new ConstructionType(),
                                                                               _localization);
            viewModel.HasReferencedToolModels = true;
            _useCases.constructionTypeGuiAdapter.RegisterGuiInterface(viewModel);
            _localization.Subscribe(viewModel);
            var userControl = new HelperTableView(viewModel, _localization);
            return userControl;
        }

        public void OpenConstructionTypeHelperTableDialog(Window owner = null)
        {
            var view = OpenConstructionTypeHelperTable();
            OpenDialog(view, owner, _useCases.constructionTypeGuiAdapter, (IHelperTableGui<ConstructionType>)view.DataContext);
        }

        public HelperTableView OpenConfigurableFieldHelperTable()
        {
            Func<string> getHelperTableName = () => _localization.Strings.GetParticularString("Auxiliary Master Data", "Configurable field tool");
            var viewModel = new HelperTableViewModel<ConfigurableField, string>(this,
                                                                                _useCases.configurableField,
                                                                                null,
                                                                                getHelperTableName,
                                                                                e => HelperTableItemModel.GetModelForConfigurableField(e),
                                                                                m => m.Entity,
                                                                                (m, e) => m.Value = e.Value.ToDefaultString(),
                                                                                () => new ConfigurableField(),
                                                                                _localization);
            viewModel.HasReferencedTools = true;
            _useCases.configurableFieldGuiAdapter.RegisterGuiInterface(viewModel);
            _localization.Subscribe(viewModel);
            var userControl = new HelperTableView(viewModel, _localization);
            return userControl;
        }

        public void OpenConfigurableFieldHelperTableDialog(Window owner = null)
        {
            var view = OpenConfigurableFieldHelperTable();
            OpenDialog(view, owner, _useCases.configurableFieldGuiAdapter, (IHelperTableGui<ConfigurableField>)view.DataContext);

        }

        public HelperTableView OpenCostCenterHelperTable()
        {
            Func<string> getHelperTableName = () => _localization.Strings.GetParticularString("Auxiliary Master Data", "Cost center");
            var viewModel = new HelperTableViewModel<CostCenter, string>(this,
                                                                         _useCases.costCenter,
                                                                         null,
                                                                         getHelperTableName,
                                                                         e => HelperTableItemModel.GetModelForCostCenter(e),
                                                                         m => m.Entity,
                                                                         (m, e) => m.Value = e.Value.ToDefaultString(),
                                                                         () => new CostCenter(),
                                                                         _localization);
            viewModel.HasReferencedTools = true;
            _useCases.costCenterGuiAdapter.RegisterGuiInterface(viewModel);
            _localization.Subscribe(viewModel);
            var userControl = new HelperTableView(viewModel, _localization);
            return userControl;
        }

        public void OpenCostCenterHelperTableDialog(Window owner = null)
        {
            var view = OpenCostCenterHelperTable();
            OpenDialog(view, owner, _useCases.costCenterGuiAdapter, (IHelperTableGui<CostCenter>)view.DataContext);
        }

        public ManufacturerView OpenManufacturer()
        {
            var manufacturerViewModel = new ManufacturerViewModel(this, _useCases.manufacturer, _useCases.saveColumns, _localization);
            _useCases.manufacturerGuiAdapter.RegisterGuiInterface(manufacturerViewModel);
            _useCases.saveColumnsGuiAdapter.RegisterGuiInterface(manufacturerViewModel);
            _localization.Subscribe(manufacturerViewModel);
            var userControl = new ManufacturerView(manufacturerViewModel, _localization);
            userControl.Unloaded += (sender, args) =>
            {
                _useCases.manufacturerGuiAdapter.RemoveGuiInterface(manufacturerViewModel);
                _useCases.saveColumnsGuiAdapter.RemoveGuiInterface(manufacturerViewModel);
            };
            return userControl;
        }

        public void OpenManufacturerDialog(Window owner = null)
        {
            var view = OpenManufacturer();
            OpenDialog(view, owner, _useCases.manufacturerGuiAdapter, (IManufacturerGui)view.DataContext);
        }

        public ToleranceClassView OpenToleranceClass()
        {
            var toleranceClassViewModel = new ToleranceClassViewModel(this, _useCases.toleranceClass, _useCases.saveColumns, _localization);
            _useCases.saveColumnsGuiAdapter.RegisterGuiInterface(toleranceClassViewModel);
            _useCases.toleranceClassGuiAdapter.RegisterGuiInterface(toleranceClassViewModel);
            var userControl = new ToleranceClassView(toleranceClassViewModel, _localization);
            userControl.Unloaded += (sender, args) =>
            {
                _useCases.toleranceClassGuiAdapter.RemoveGuiInterface(toleranceClassViewModel);
                _useCases.saveColumnsGuiAdapter.RemoveGuiInterface(toleranceClassViewModel);
            };
            return userControl;
        }

        public void OpenToleranceClassDialog(Window owner = null)
        {
            var view = OpenToleranceClass();
            OpenDialog(view, owner, _useCases.toleranceClassGuiAdapter, (IToleranceClassGui)view.DataContext);
        }

        public HelperTableView OpenToolUsage()
        {
            Func<string> getHelperTableName = () => _localization.Strings.GetParticularString("Auxiliary Master Data", "ToolUsage");
            var viewModel = new HelperTableViewModel<ToolUsage, string>(this,
                _useCases.toolUsage,
                null,
                getHelperTableName,
                e => HelperTableItemModel.GetModelForToolUsage(e),
                m => m.Entity,
                (m, e) => m.Value = e.Value.ToDefaultString(),
                () => new ToolUsage(),
                _localization);
            viewModel.HasReferencedTools = true;
            viewModel.HasReferencedLocationToolAssignments = true;
            _useCases.toolUsageGuiAdapter.RegisterGuiInterface(viewModel);
            _localization.Subscribe(viewModel);
            var userControl = new HelperTableView(viewModel, _localization);
            return userControl;
        }

        public void OpenToolUsageDialog(Window owner = null)
        {
            var view = OpenToolUsage();
            OpenDialog(view, owner, _useCases.toolUsageGuiAdapter, (IHelperTableGui<ToolUsage>)view.DataContext);
        }

        public ExtensionView OpenExtension()
        {
            var extensionViewModel = new ExtensionViewModel(this, _useCases.extension, _useCases.extensionInterface, _localization, new ExtensionValidator());
            var userControl = new ExtensionView(extensionViewModel, _localization);
            return userControl;
        }

        public void OpenExtensionDialog(Window owner = null)
        {
            var view = OpenExtension();
            OpenDialog(view, owner);
        }

        public QstInformationView OpenQstInformation()
        {
            var qstInformationViewModel = new QstInformationViewModel(_useCases.qstInformation, _localization);
            _useCases.qstInformationGuiAdapter.real = qstInformationViewModel;
            var userControl = new QstInformationView(qstInformationViewModel, _localization);
            return userControl;
        }

        public LoginView OpenLogin()
        {
            var loginViewModel = new LoginViewModel(_useCases.login, _localization, _useCases.language, _useCases.LanguageInterface);
            _useCases.loginGuiAdapter.real = loginViewModel;
            var userControl = new LoginView(loginViewModel, _localization);
            return userControl;
        }

        public GlobalTreeViewModel OpenGlobalTree()
        {
            var globalTreeViewModel = new GlobalTreeViewModel(_useCases.sessionInformation, _localization, this);
            _useCases.sessionInformationGuiAdapter.real = globalTreeViewModel;
            ShowLoadingControlEvent += globalTreeViewModel.ShowLoadingControl;
            return globalTreeViewModel;
        }

        public ToolModelView OpenToolModel()
        {
            var toolModelViewModel = new ToolModelViewModel(this, _useCases.toolModel, _useCases.saveColumns, _localization, _useCases.toolDisplayFormatter);
            _useCases.saveColumnsGuiAdapter.RegisterGuiInterface(toolModelViewModel);
            _useCases.toolModelGuiAdapter.RegisterGuiInterface(toolModelViewModel);
            _useCases.manufacturerGuiAdapter.RegisterGuiInterface(toolModelViewModel);
            _useCases.toolTypeGuiAdapter.RegisterGuiInterface(toolModelViewModel);
            _useCases.driveSizeGuiAdapter.RegisterGuiInterface(toolModelViewModel);
            _useCases.driveTypeGuiAdapter.RegisterGuiInterface(toolModelViewModel);
            _useCases.switchOffGuiAdapter.RegisterGuiInterface(toolModelViewModel);
            _useCases.shutOffGuiAdapter.RegisterGuiInterface(toolModelViewModel);
            _useCases.constructionTypeGuiAdapter.RegisterGuiInterface(toolModelViewModel);
            var userControl = new ToolModelView(toolModelViewModel, _localization);
            return userControl;
        }

        public void OpenToolModelDialog(Window owner = null)
        {
            var view = OpenToolModel();
            OpenDialog(view, owner, _useCases.toolModelGuiAdapter, (IToolModelGui)view.DataContext);
        }

        public ToolView OpenTool()
        {
            var toolViewModel = new ToolViewModel(_useCases.tool, _localization, _useCases.toolDisplayFormatter, this);
            _useCases.toolGuiAdapter.RegisterGuiInterface(toolViewModel);
            _useCases.toolModelGuiAdapter.RegisterGuiInterface(toolViewModel);
            if (!FeatureToggles.FeatureToggles.HelperTablesWithInterfaceAdapter)
            {
                _useCases.statusGuiAdapter.RegisterGuiInterface(toolViewModel);
            }
            _useCases.costCenterGuiAdapter.RegisterGuiInterface(toolViewModel);
            _useCases.configurableFieldGuiAdapter.RegisterGuiInterface(toolViewModel);
            _useCases.toolTypeGuiAdapter.RegisterGuiInterface(toolViewModel);
            var userControl = new ToolView(toolViewModel, _localization, _useCases.toolDisplayFormatter);
            userControl.Unloaded += (sender, args) =>
            {
                _useCases.toolGuiAdapter.RemoveGuiInterface(toolViewModel);
                _useCases.toolModelGuiAdapter.RemoveGuiInterface(toolViewModel);
                if (!FeatureToggles.FeatureToggles.HelperTablesWithInterfaceAdapter)
                {
                    _useCases.statusGuiAdapter.RemoveGuiInterface(toolViewModel);
                }
                _useCases.costCenterGuiAdapter.RemoveGuiInterface(toolViewModel);
                _useCases.configurableFieldGuiAdapter.RemoveGuiInterface(toolViewModel);
                _useCases.toolTypeGuiAdapter.RemoveGuiInterface(toolViewModel);
            };
            return userControl;
        }

        public void OpenToolDialog(Window owner = null)
        {
            var view = OpenTool();
            OpenDialog(view, owner, _useCases.toolGuiAdapter, (IToolGui)view.DataContext);
        }

        public TestEquipmentView OpenTestEquipment()
        {
            var viewModel = new TestEquipmentViewModel(_useCases.testEquipment, _localization, this, _useCases.testEquipmentInterface, _useCases.sessionInformation, new TestEquipmentValidator());
            return new TestEquipmentView(viewModel, _localization, _useCases.testEquipmentDisplayFormatter);
        }

        public void OpenTestEquipmentDialog(Window owner = null)
        {
            throw new NotImplementedException();
        }

        public LocationView OpenLocation()
        {
            var viewModel = new LocationViewModel(new LocationUseCaseHumbleAsyncRunner(_useCases.location), this, new LocationValidator(), _localization, new HumbleTaskThreadCreator(), _useCases.LocationDisplayFormatter);
            _useCases.locationGuiAdapter.RegisterGuiInterface(viewModel);
            _useCases.toleranceClassGuiAdapter.RegisterGuiInterface(viewModel);
            var locView = new LocationView(viewModel, _localization, _useCases.LocationDisplayFormatter);

            locView.Unloaded += (sender, args) =>
            {
                _useCases.locationGuiAdapter.RemoveGuiInterface(viewModel);
                _useCases.toleranceClassGuiAdapter.RemoveGuiInterface(viewModel);
            };
            return locView;
        }

        public void OpenLocationDialog(Window owner = null)
        {
            var view = OpenLocation();
            OpenDialog(view, owner, _useCases.locationGuiAdapter, (ILocationGui)view.DataContext);
        }

        public TestLevelSetView OpenTestLevelSet()
        {
            var viewModel = new TestLevelSetViewModel(_useCases.TestLevelSet,
                                                  _useCases.TestLevelSetInterface,
                                                  _useCases.workingCalendar,
                                                  _useCases.workingCalendarInterface,
                                                  _useCases.shiftManagement,
                                                  _useCases.shiftManagementInterface,
                                                  _localization,
                                                  this);
            return new TestLevelSetView(viewModel, _localization);
        }

        public void OpenTestLevelSetDialog(Window owner = null)
        {
            var view = OpenTestLevelSet();
            OpenDialog(view, owner);
        }

        public ToolTestPlanningView OpenToolTestPlanning()
        {
            var viewModel = new ToolTestPlanningViewModel(_useCases.TestLevelSetAssignment,
                _useCases.TestLevelSetAssignmentInterface,
                _useCases.TestLevelSet,
                _useCases.TestLevelSetInterface,
                _useCases.locationToolAssignment,
                _useCases.locationToolAssignmentGuiAdapter,
                _localization,
                _useCases.locationToolAssignmentDisplayFormatter,
                _useCases.shiftManagement,
                _useCases.shiftManagementInterface,
                this);
            return new ToolTestPlanningView(viewModel, _localization);
        }

        public void OpenToolTestPlanningDialog(Window owner = null)
        {
            throw new NotImplementedException();
        }

        public void OpenLocationToolAssignmentToolDetailDialog(List<ToolModelWithToolUsage> toolModels)
        {
            var view = new LocationToolAssignmentToolDetailsView(_localization, toolModels);
            view.Owner = Application.Current.MainWindow;
            view.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            view.Show();
        }

        public TransferToTestEquipmentView OpenTransferToTestEquipment()
        {
            var viewModel =
                new TransferToTestEquipmentViewModel(
                    _useCases.testEquipment,
                    _useCases.TransferToTestEquipment,
                    _localization,
                    _useCases.testEquipmentInterface, this);
            _useCases.transferToTestEquipmentGuiAdapter._real = viewModel;
            return new TransferToTestEquipmentView(viewModel, _localization, _useCases.testEquipmentDisplayFormatter);
        }

        public ClassicTestView OpenClassicTest()
        {
            var viewModel = new ClassicTestViewModel(_useCases.classicTest, new LocationUseCaseHumbleAsyncRunner(_useCases.location), _localization, this, _useCases.classicTestInterfaceAdapter);
            return new ClassicTestView(viewModel, _useCases.LocationDisplayFormatter);
        }

        public LocationToolAssignmentView OpenLocationToolAssignment()
        {
            var viewModel = new LocationToolAssignmentViewModel(
                _useCases.locationToolAssignment, new LocationUseCaseHumbleAsyncRunner(_useCases.location),
                _useCases.tool,
                _useCases.toleranceClass,
                _useCases.TestLevelSet,
                _useCases.TestLevelSetInterface,
                this, _localization,
                _useCases.locationToolAssignmentDisplayFormatter, new HumbleTaskThreadCreator(), new LocationToolAssignmentValidator(),
                _useCases.LocationDisplayFormatter, _useCases.toolDisplayFormatter);

            _useCases.locationToolAssignmentGuiAdapter.RegisterGuiInterface(viewModel);
            _useCases.locationGuiAdapter.RegisterGuiInterface(viewModel);
            _useCases.toolGuiAdapter.RegisterGuiInterface(viewModel);
            _useCases.toleranceClassGuiAdapter.RegisterGuiInterface(viewModel);
            var userControl = new LocationToolAssignmentView(viewModel, _localization, _useCases.toolDisplayFormatter, _useCases.LocationDisplayFormatter);
            userControl.Unloaded += (sender, args) =>
            {
                _useCases.locationToolAssignmentGuiAdapter.RemoveGuiInterface(viewModel);
                _useCases.locationGuiAdapter.RemoveGuiInterface(viewModel);
                _useCases.toolGuiAdapter.RemoveGuiInterface(viewModel);
                _useCases.toleranceClassGuiAdapter.RemoveGuiInterface(viewModel);
            };
            return userControl;
        }

        public ProcessControlView OpenProcessControl()
        {
            var viewModel = new ProcessControlViewModel(new ProcessControlUseCaseHumbleAsyncRunner(_useCases.processControl),
                new LocationUseCaseHumbleAsyncRunner(_useCases.location), this, _localization, _useCases.processControlInterfaceAdapter,
                _useCases.TestLevelSet,
                _useCases.TestLevelSetInterface, new ProcessControlConditionValidator(),
                _useCases.extensionInterface, _useCases.extension);
            return new ProcessControlView(viewModel, _localization, _useCases.LocationDisplayFormatter);
        }

        public TrashView OpenTrash()
        {
            var viewModel = new TrashViewModel(new TrashUseCaseHumbleAsyncRunner(_useCases.trash), this,
                _localization, new HumbleTaskThreadCreator(), new LocationUseCaseHumbleAsyncRunner(_useCases.location),_useCases.trashGuiAdapter,
                _useCases.extension, _useCases.extensionInterface);
            _useCases.trashGuiAdapter.RegisterGuiInterface(viewModel);
            //_useCases.toleranceClassGuiAdapter.RegisterGuiInterface(viewModel);
            var locView = new TrashView(viewModel, _localization, _useCases.LocationDisplayFormatter);

            locView.Unloaded += (sender, args) =>
            {
                _useCases.trashGuiAdapter.RemoveGuiInterface(viewModel);
                //_useCases.toleranceClassGuiAdapter.RemoveGuiInterface(viewModel);
            };
            return locView;
        }

        public AssistentView OpenAddToolModelAssistent(Core.Entities.ToolModel defaultToolModel = null)
        {
            var creator = new AddToolModelAssistentCreator(defaultToolModel, this, _localization, _useCases);
            return creator.CreateAddToolModelAssistent();
        }

        public AssistentView OpenAddToolAssistent(Tool defaultTool = null)
        {
            var creator = new AddToolAssistentCreator(defaultTool, this, _localization, _useCases);
            return creator.CreateAddToolAssistent();
        }

        public AssistentView OpenAddTestEquipmentAssistant(List<TestEquipmentType> availableTestEquipmentTypes, TestEquipment testEquipmentDefault = null, TestEquipmentModel testEquipmentModelDefault = null, TestEquipmentType? testEquipmentTypeDefault = null)
        {
            var creator = new AddTestEquipmentAssistantCreator(availableTestEquipmentTypes, testEquipmentDefault, testEquipmentModelDefault, testEquipmentTypeDefault, _localization, _useCases);
            return creator.CreateAddTestEquipmentAssistant();
        }

        public AssistentView OpenAddLocationAssistent(Location defaultLocation = null)
        {
            var creator = new AddLocationAssistentCreator(defaultLocation, this, _localization, _useCases);
            return creator.CreateAddLocationAssistent();
        }

        public AssistentView OpenAddTestConditionsAssistent(LocationToolAssignment defaultAssignment)
        {
            var creator = new AddTestConditionsAssistentCreator(_localization, this, _useCases, defaultAssignment);
            return creator.CreateAddTestConditionsAssistent();
        }

        public AssistentView OpenAddProcessControlAssistant(Location location)
        {
            var creator = new AddProcessControlConditionCreator(location, _localization, this, _useCases);
            return creator.CreateAddProcessControlAssistant();
        }

        public AssistentView OpenAddWorkingCalendarEntryAssistant(bool areSaturdaysFree, bool areSundaysFree, Func<DateTime, WorkingCalendarEntry> getExistingEntryForDate, DateTime? defaultDate = null, WorkingCalendarEntryType? defaultType = null)
        {
            var creator = new AddWorkingCalendarEntryAssistantCreator(_localization, areSaturdaysFree, areSundaysFree, getExistingEntryForDate, defaultDate, defaultType);
            return creator.CreateAddWorkingCalendarEntryAssistant();
        }

        public AssistentView OpenAddTestPlanAssistant(TestPlan defaultTestPlan = null)
        {
            var creator = new AddTestPlanAssistantCreator(_localization, _useCases, defaultTestPlan);
            return creator.CreateAddTestPlanAssistant();
        }

        public AssistentView OpenAssignToolToLocationAssistent(LocationToolAssignment assignment)
        {
            var creator = new AssignToolToLocationAssistentCreator(_localization, _useCases, this, assignment);
            return creator.CreateAssignToolToLocationAssistent();
        }

        public ClassicTestHtmlView OpenClassicChkTestHtmlView(Location location, Tool tool, List<ClassicChkTest> tests)
        {
            var viewModel = new ClassicToolMonitoringHtmlViewModel(location, tool, tests, _localization, _useCases.treePathBuilder);
            viewModel.Initialize();
            return new ClassicTestHtmlView(viewModel);
        }

        public ClassicTestHtmlView OpenClassicMfuTestHtmlView(Location location, Tool tool, List<ClassicMfuTest> tests)
        {
            var viewModel = new ClassicToolMfuTestHtmlViewModel(location, tool, tests, _localization, _useCases.treePathBuilder);
            viewModel.Initialize();
            return new ClassicTestHtmlView(viewModel);
        }

        public ClassicTestHtmlView OpenClassicProcessMonitoringTestHtmlView(Location location, List<ClassicProcessTest> tests)
        {
            var viewModel = new ClassicProcessMonitoringTestHtmlViewModel(location, tests, _localization, _useCases.treePathBuilder);
            viewModel.Initialize();
            return new ClassicTestHtmlView(viewModel);
        }

        public ClassicTestHtmlView OpenClassicProcessPfuTestHtmlView(Location location, List<ClassicProcessTest> tests)
        {
            var viewModel = new ClassicProcessPfuHtmlViewModel(location, tests, _localization, _useCases.treePathBuilder);
            viewModel.Initialize();
            return new ClassicTestHtmlView(viewModel);
        }

        public IChangeToolStateView OpenChangeToolStatusAssistant(List<LocationToolAssignment> assignments)
        {
            var creator = new ChangeToolStatusAssistentCreator(_localization, _useCases, this, assignments, _useCases.toolDisplayFormatter, _useCases.LocationDisplayFormatter);
            return creator.CreateChangeToolStatusAssistant();
        }

        public MainView OpenMainView()
        {
            var viewModel = new MainViewViewModel(_useCases.logout);
            var toolbarViewModel = new GlobalToolBarViewModel(_localization, _useCases.language, _useCases.LanguageInterface);
            var view = new MainView(_localization, viewModel, this, toolbarViewModel);
            _useCases.logoutGuiProxy.Real = viewModel;
            return view;
        }

        public TestEquipmentTestResultView OpenTestEquipmentTestResultView(List<TestEquipmentTestResult> testEquipmentTestResult)
        {
            var viewModel = new TestEquipmentTestResultViewModel(testEquipmentTestResult, _localization);
            return new TestEquipmentTestResultView(viewModel, _localization);
        }

        public ProcessControlPlanningView OpenProcessControlPlanning()
        {
            return new ProcessControlPlanningView(new ProcessControlPlanningViewModel(
                _useCases.TestLevelSetAssignment,
                _useCases.TestLevelSetAssignmentInterface,
                _useCases.TestLevelSet,
                _useCases.TestLevelSetInterface,
                _useCases.shiftManagement,
                _useCases.shiftManagementInterface,
                _useCases.processControl,
                _useCases.processControlInterfaceAdapter,
                _localization,
                _useCases.LocationDisplayFormatter,
                this),
                _localization);
        }

        public void OpenProcessControlPlanningDialog(Window owner = null)
        {
            throw new NotImplementedException();
        }

        public void RaiseShowLoadingControl(bool showLoadingControl, int count = 1)
        {
            if (showLoadingControl)
            {
                if (ShowLoadingControlRefernceCounter == 0)
                {
                    ShowLoadingControlEvent?.Invoke(this, true);
                }

                ShowLoadingControlRefernceCounter += count;
            }
            else
            {
                if (ShowLoadingControlRefernceCounter - count >= 0)
                {
                    ShowLoadingControlRefernceCounter -= count;
                }
                else
                {
                    ShowLoadingControlRefernceCounter = 0;
                }

                if (ShowLoadingControlRefernceCounter == 0)
                {
                    ShowLoadingControlEvent?.Invoke(this, false);
                }
            }
        }

        public HelperTableItemAssistentPlan<Status> GetStatusAssistentPlan(IAssistentView assistantView, HelperTableEntityId defaultId)
        {
            var statusPlan = new HelperTableItemAssistentPlan<Status>(_useCases.status,
                new ListAssistentItemModel<Status>(assistantView.Dispatcher,
                    _localization.Strings.GetParticularString("Location tool assignment", "Enter the new status for the tool"),
                    _localization.Strings.GetParticularString("Location tool assignment", "Status"),
                    null,
                    (resultObject, item) => (resultObject as Tool).Status = (item as ListAssistentItemModel<Status>).EnteredValue,
                    _localization.Strings.GetParticularString("Location tool assignment", "Add status"),
                    status => status.Value.ToDefaultString(),
                    () =>
                    {
                        OpenStatusHelperTableDialog(assistantView as Window);
                    }),
                defaultId);
            if (!FeatureToggles.FeatureToggles.HelperTablesWithInterfaceAdapter)
            {
                _useCases.statusGuiAdapter.RegisterGuiInterface(statusPlan);
            }
            return statusPlan;
        }

        public IAssistentView GetAssistentView(string header)
        {
            return new AssistentView(header);
        }

        public AssistentView OpenAddExtensionAssistant(Extension defaultExtension)
        {
            var creator = new AddExtensionAssistantCreator(defaultExtension, this, _localization, _useCases);
            return creator.CreateAddExtensionAssistant();
        }

        public LocationHistoryView OpenLocationHistoryView()
        {
            return new LocationHistoryView(new LocationHistoryViewModel(_useCases.history, _useCases.historyInterfaceAdapter, _useCases.location, _useCases.locationGuiAdapter, _localization, this), _localization, _useCases.LocationDisplayFormatter);
        }

        public void OpenLocationHistoryDialog(Window owner = null)
        {
            throw new NotImplementedException();
        }

        private LocalizationWrapper _localization;
        private UseCaseCollection _useCases;
    }
}
