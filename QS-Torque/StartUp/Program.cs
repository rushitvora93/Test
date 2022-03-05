
using FrameworksAndDrivers.Gui.Wpf.View;
using FrameworksAndDrivers.Localization;
using log4net;
using State;
using System;
using System.Windows;
using Core.UseCases;
using InterfaceAdapters;
using FrameworksAndDrivers.Gui.Wpf;
using FrameworksAndDrivers.Data.Xml.DataAccess;
using FrameworksAndDrivers.Gui.Wpf.View.Themes;
using System.Collections.Generic;
using Client.UseCases.UseCases;
using Core.Entities;
using Core.UseCases.Communication;
using Core.UseCases.Communication.DataGate;
using FrameworksAndDrivers.DataGate;
using FrameworksAndDrivers.Gui.Wpf.Formatter;
using FrameworksAndDrivers.Process;
using FrameworksAndDrivers.Threads;
using FrameworksAndDrivers.Time;
using FrameworksAndDrivers.ToastNotification;
using InterfaceAdapters.Communication;
using log4net.Config;
using ClientFactory = FrameworksAndDrivers.RemoteData.GRPC.ClientFactory;
using LoginDataAccess = FrameworksAndDrivers.RemoteData.GRPC.DataAccess.LoginDataAccess;
using FrameworksAndDrivers.RemoteData.GRPC;
using Syncfusion.UI.Xaml.Grid;
using InterfaceAdapters.Models;
using System.Windows.Threading;
using FrameworksAndDrivers.Data.Registry;
using FrameworksAndDrivers.RemoteData.GRPC.DataAccess;

/*

   |\---/|
   | ,_, |
    \_`_/-..----.
 ___/ `   ' ,""+ \
(__...'   __\    |`.___.';
  (_,...'(_,.`__)/'.....+

Das ist Location Cat.

Location Cat ist müde, weil sie so lange Programmierer in Location2 in
QS-Torque V7 aufheitern musste.

Bitte halte den Code sauber, dass sie sich ausruhen kann.

*/

namespace StartUp
{
    internal static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
#if DEBUG
            //Aktivieren von Syncfusion Grid for CodedUI-Tests
            //Dadurch wird dass Grid anders aufgebaut sodass man auf die Elemente in Tests leichter zugreifen kann
            AutomationPeerHelper.EnableCodedUI = true;
#endif

            XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo("ClientLogConfig.xml"));
            ILog log = LogManager.GetLogger(LoggerName.DefaultLogger);
            log.Info("Client Starting...");
            try
			{
				var localizationWrapper = new LocalizationWrapper("Messages");
				localizationWrapper.SetLanguage("en-US");
                LocalizationUtil.Localization = new LocalizationUtil(localizationWrapper);
				var application = new Application { ShutdownMode = ShutdownMode.OnExplicitShutdown };
                
				var startUp = CreateStartUp(localizationWrapper, application.Dispatcher);

                application.Resources.Add(ResourceKeys.ApplicationThemeDictionaryKey, startUp.ThemeDictionary);
                application.Resources.MergedDictionaries.Add(startUp.ThemeDictionary);
				application.Startup += (sender, eventArgs) => ApplicationOnStartup(sender, eventArgs, localizationWrapper, startUp);
				application.Exit += Application_Exit;
				application.DispatcherUnhandledException += Application_DispatcherUnhandledException;
				application.Run();
			}
			catch (Exception e)
            {
                log.Error("Unhandled Exception in Main", e);
            }
        }

		private static LoginXmlDataAccess MakeLoginXmlDataAccess()
		{
			var stream = new CreateFileStream();
			var serverConnectionsConfigPath = FilePaths.ServerConnectionsConfigPath;
			return new LoginXmlDataAccess(stream, serverConnectionsConfigPath);
		}

		private static StartUpImpl CreateStartUp(LocalizationWrapper localizationWrapper, Dispatcher guiDispatcher)
		{
			var useCases = new UseCaseCollection();

			var sessionInformationLocal = new SessionInformationLocalDataAccess();
            var timeDataAccess = new HumbleTimeDataAccess();
            var notificationManager = new ToastNotificationManager(localizationWrapper);
            var channelCreator = new ChannelCreator();
            var clientFactory = new ClientFactory();

            useCases.treePathBuilder = new TreePathBuilder();
            
            var defaultFormatter = new DefaultFormatter();
            useCases.toolDisplayFormatter = defaultFormatter;
            useCases.locationToolAssignmentDisplayFormatter = defaultFormatter;
            useCases.LocationDisplayFormatter = defaultFormatter;
            useCases.testEquipmentDisplayFormatter = defaultFormatter;
            useCases.TestDateCalculation = new TestDateCalculationHumbleAsyncRunner(
                new TestDateCalculationUseCase(new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.TestDateCalculationDataAccess(clientFactory), notificationManager));

            useCases.saveColumnsGuiAdapter = new SaveColumnsInterfaceAdapter();
			ISaveColumnsData saveColumnsDataAccess = 
                new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.SaveColumnsDataAccess(clientFactory);

            useCases.saveColumns = new SaveColumnsHumbleAsyncRunner(new SaveColumnsUseCase(useCases.saveColumnsGuiAdapter, saveColumnsDataAccess));

			useCases.shutOffGuiAdapter = new HelperTableInterfaceAdapter<Core.Entities.ShutOff>();
            IHelperTableData<ShutOff> shutOffDataAccess = new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.HelperTableDataAccess<ShutOff>(
                    NodeId.ShutOff,
                    new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.LambdaHelperTableEntitySupport<Core.Entities.ShutOff>(
                        () => new Core.Entities.ShutOff(),
                        (i, m) => m.DirectPropertyMapping(i),
                        (dto, i, m) => m.DirectPropertyMapping(dto, i)),
                    true,
                    false,
                    new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.HelperTableToToolModelReferenceLoader(clientFactory, NodeId.ShutOff),
                    clientFactory);

            useCases.shutOff = new HelperTableUseCaseHumbleAsyncRunner<ShutOff>(
                new HelperTableUseCase<Core.Entities.ShutOff>(
                    shutOffDataAccess, 
                    useCases.shutOffGuiAdapter,
                    sessionInformationLocal,
                    notificationManager));

			useCases.switchOffGuiAdapter = new HelperTableInterfaceAdapter<Core.Entities.SwitchOff>();
            IHelperTableData<SwitchOff> switchOffDataAccess = new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.HelperTableDataAccess<SwitchOff>(
                        NodeId.SwitchOff,
                        new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.LambdaHelperTableEntitySupport<Core.Entities.SwitchOff>(
                            () => new Core.Entities.SwitchOff(),
                            (i, m) => m.DirectPropertyMapping(i),
                            (dto, i, m) => m.DirectPropertyMapping(dto, i)),
                        true,
                        false,
                        new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.HelperTableToToolModelReferenceLoader(clientFactory, NodeId.SwitchOff),
                        clientFactory);

            useCases.switchOff = new HelperTableUseCaseHumbleAsyncRunner<SwitchOff>(
                new HelperTableUseCase<Core.Entities.SwitchOff>(
                    switchOffDataAccess,
                    useCases.switchOffGuiAdapter,
                    sessionInformationLocal,
                    notificationManager));

			useCases.driveSizeGuiAdapter = new HelperTableInterfaceAdapter<Core.Entities.DriveSize>();
            IHelperTableData<DriveSize> driveSizeDataAccess = new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.HelperTableDataAccess<DriveSize>(
                        NodeId.DriveSize,
                        new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.LambdaHelperTableEntitySupport<Core.Entities.DriveSize>(
                            () => new Core.Entities.DriveSize(),
                            (i, m) => m.DirectPropertyMapping(i),
                            (dto, i, m) => m.DirectPropertyMapping(dto, i)),
                        true,
                        false,
                        new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.HelperTableToToolModelReferenceLoader(clientFactory, NodeId.DriveSize),
                        clientFactory);

            useCases.driveSize = new HelperTableUseCaseHumbleAsyncRunner<DriveSize>(
                new HelperTableUseCase<Core.Entities.DriveSize>(
                    driveSizeDataAccess,
                    useCases.driveSizeGuiAdapter,
                    sessionInformationLocal,
                    notificationManager));

			useCases.driveTypeGuiAdapter = new HelperTableInterfaceAdapter<Core.Entities.DriveType>();
            IHelperTableData<DriveType> driveTypeDataAccess= new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.HelperTableDataAccess<DriveType>(
                        NodeId.DriveType,
                        new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.LambdaHelperTableEntitySupport<Core.Entities.DriveType>(
                            () => new Core.Entities.DriveType(),
                            (i, m) => m.DirectPropertyMapping(i),
                            (dto, i, m) => m.DirectPropertyMapping(dto, i)),
                        true,
                        false,
                        new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.HelperTableToToolModelReferenceLoader(clientFactory, NodeId.DriveType),
                        clientFactory);

            useCases.driveType = new HelperTableUseCaseHumbleAsyncRunner<DriveType>(
                new HelperTableUseCase<Core.Entities.DriveType>(
                    driveTypeDataAccess,
                    useCases.driveTypeGuiAdapter,
                    sessionInformationLocal,
                    notificationManager));

            useCases.toolTypeGuiAdapter = new HelperTableInterfaceAdapter<Core.Entities.ToolType>();
            IHelperTableData<ToolType> toolTypeDataAccess = new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.HelperTableDataAccess<ToolType>(
                    NodeId.ToolType,
                    new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.LambdaHelperTableEntitySupport<Core.Entities.ToolType>(
                        () => new Core.Entities.ToolType(),
                        (i, m) => m.DirectPropertyMapping(i),
                        (dto, i, m) => m.DirectPropertyMapping(dto, i)),
                    true,
                    false,
                    new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.HelperTableToToolModelReferenceLoader(clientFactory, NodeId.ToolType),
                    clientFactory);

            useCases.toolType = new HelperTableUseCaseHumbleAsyncRunner<ToolType>(
                new HelperTableUseCase<Core.Entities.ToolType>(
                    toolTypeDataAccess,
                    useCases.toolTypeGuiAdapter,
                    sessionInformationLocal, notificationManager));

			useCases.reasonForToolChangeGuiAdapter = new HelperTableInterfaceAdapter<Core.Entities.ReasonForToolChange>();
            IHelperTableData<ReasonForToolChange> reasonForToolChangeDataAccess = new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.HelperTableDataAccess<ReasonForToolChange>(
                    NodeId.ReasonForToolChange,
                    new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.LambdaHelperTableEntitySupport<Core.Entities.ReasonForToolChange>(
                        () => new Core.Entities.ReasonForToolChange(),
                        (i, m) => m.DirectPropertyMapping(i),
                        (dto, i, m) => m.DirectPropertyMapping(dto, i)),
                    false,
                    false,
                    new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.HelperTableToToolModelReferenceDoesNotExist(),
                    clientFactory);

            useCases.reasonForToolChange = new HelperTableUseCaseHumbleAsyncRunner<ReasonForToolChange>(
                new HelperTableUseCase<Core.Entities.ReasonForToolChange>(
                    reasonForToolChangeDataAccess, 
                    useCases.reasonForToolChangeGuiAdapter, 
                    sessionInformationLocal, notificationManager));

			IHelperTableData<Status> statusDataAccess =
				new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.StatusDataAccess(
					clientFactory,
					useCases.toolDisplayFormatter);
			useCases.statusGuiAdapterNew =
				new HelperTableInterface<Core.Entities.Status, string>(
					e => HelperTableItemModel.GetModelForStatus(e),
					guiDispatcher);
			useCases.statusGuiAdapter = new HelperTableInterfaceAdapter<Core.Entities.Status>();


            useCases.status = new HelperTableUseCaseHumbleAsyncRunner<Status>(
                new HelperTableUseCase<Core.Entities.Status>(
                    statusDataAccess,
                    useCases.statusGuiAdapter, 
                    sessionInformationLocal,
                    notificationManager));

			useCases.constructionTypeGuiAdapter = new HelperTableInterfaceAdapter<Core.Entities.ConstructionType>();
            IHelperTableData<ConstructionType> constructionTypeDataAccess = new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.HelperTableDataAccess<ConstructionType>(
                    NodeId.ConstructionType,
                    new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.LambdaHelperTableEntitySupport<Core.Entities.ConstructionType>(
                        () => new Core.Entities.ConstructionType(),
                        (i, m) => m.DirectPropertyMapping(i),
                        (dto, i, m) => m.DirectPropertyMapping(dto, i)),
                    true,
                    false,
                    new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.HelperTableToToolModelReferenceLoader(clientFactory, NodeId.ConstructionType),
                    clientFactory);

            useCases.constructionType = new HelperTableUseCaseHumbleAsyncRunner<ConstructionType>(
                new HelperTableUseCase<Core.Entities.ConstructionType>(
                    constructionTypeDataAccess, 
                    useCases.constructionTypeGuiAdapter, 
                    sessionInformationLocal, 
                    notificationManager));

			useCases.configurableFieldGuiAdapter = new HelperTableInterfaceAdapter<Core.Entities.ConfigurableField>();
            IHelperTableData<ConfigurableField> configurableFieldDataAccess = new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.HelperTableDataAccess<ConfigurableField>(
                    NodeId.ConfigurableField,
                    new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.LambdaHelperTableEntitySupport<Core.Entities.ConfigurableField>(
                        () => new Core.Entities.ConfigurableField(),
                        (i, m) => m.DirectPropertyMapping(i),
                        (dto, i, m) => m.DirectPropertyMapping(dto, i)),
                    false,
                    true,
                    new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.HelperTableToToolReferenceLoader(clientFactory, NodeId.ConfigurableField, useCases.toolDisplayFormatter),
                    clientFactory);

            useCases.configurableField = new HelperTableUseCaseHumbleAsyncRunner<ConfigurableField>(
                new HelperTableUseCase<Core.Entities.ConfigurableField>(
                    configurableFieldDataAccess,
                    useCases.configurableFieldGuiAdapter, 
                    sessionInformationLocal,
                    notificationManager));

            useCases.costCenterGuiAdapter = new HelperTableInterfaceAdapter<Core.Entities.CostCenter>();
            IHelperTableData<CostCenter> costCenterDataAccess = new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.HelperTableDataAccess<CostCenter>(
                    NodeId.CostCenter,
                    new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.LambdaHelperTableEntitySupport<Core.Entities.CostCenter>(
                        () => new Core.Entities.CostCenter(),
                        (i, m) => m.DirectPropertyMapping(i),
                        (dto, i, m) => m.DirectPropertyMapping(dto, i)),
                    false,
                    true,
                    new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.HelperTableToToolReferenceLoader(clientFactory, NodeId.CostCenter, useCases.toolDisplayFormatter),
                    clientFactory);

            useCases.costCenter = new HelperTableUseCaseHumbleAsyncRunner<CostCenter>(
                new HelperTableUseCase<Core.Entities.CostCenter>(
                    costCenterDataAccess,
                    useCases.costCenterGuiAdapter, 
                    sessionInformationLocal,
                    notificationManager));

            useCases.manufacturerGuiAdapter = new ManufacturerInterfaceAdapter();
            IManufacturerData manufacturerDataAccess = new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.ManufacturerDataAccess(clientFactory);

            useCases.manufacturer =
                new ManufacturerHumbleAsyncRunner(
                    new ManufacturerUseCase(
                        useCases.manufacturerGuiAdapter,
                        manufacturerDataAccess,
                        sessionInformationLocal,
                        notificationManager));
            
			useCases.qstInformationGuiAdapter = new QstInformationGuiProxy();
			var qstInformationDataAccessLocal = new QstInformationLocalDataAccess();
			IQstInformationData qstInformationDataAccessRemote = new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.QstInformationDataAccess(clientFactory);

            useCases.qstInformation = new QstInformationUseCase(qstInformationDataAccessLocal, qstInformationDataAccessRemote, useCases.qstInformationGuiAdapter);

			useCases.loginGuiAdapter = new LoginGuiProxy();
			var loginDataAccessLocal = MakeLoginXmlDataAccess();
            ILoginData loginDataAccessRemote = new LoginDataAccess(clientFactory, channelCreator);
          
			useCases.login = new LoginUseCase(loginDataAccessLocal, loginDataAccessRemote, useCases.loginGuiAdapter);

            ILanguageData languageAccess = new RegistryDataAccess();
            useCases.LanguageInterface = new LanguageInterfaceAdapter(localizationWrapper);
            useCases.language =  new LanguageUseCase(useCases.LanguageInterface,languageAccess);

            ISessionInformationRegistryDataAccess registryAccess = new RegistryDataAccess();

            useCases.sessionInformationGuiAdapter = new SessionInformationGuiProxy();
			useCases.sessionInformation = new SessionInformationUseCase(sessionInformationLocal, sessionInformationLocal, useCases.sessionInformationGuiAdapter,registryAccess);

			useCases.toolModelGuiAdapter = new ToolModelInterfaceAdapter();

            ICmCmkDataAccess cmCmkDataAccess = new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.CmCmkDataAccess(clientFactory);

            var toolModelPictureAndDataAccess =
                new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.ToolModelDataAccess(
                    clientFactory,
                    defaultFormatter);

            useCases.toolModel = new ToolModelUseCase(
                toolModelPictureAndDataAccess,
                cmCmkDataAccess, 
				useCases.toolModelGuiAdapter,
				useCases.manufacturer,
				useCases.toolType,
				useCases.driveSize,
				useCases.driveType,
				useCases.switchOff,
				useCases.shutOff,
				useCases.constructionType,
                sessionInformationLocal,
                notificationManager);

            useCases.locationToolAssignmentGuiAdapter = new LocationToolAssignmentInterfaceAdapter(localizationWrapper);
            ILocationToolAssignmentData locationToolAssignmentDataAccess =
                new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.LocationToolAssignmentDataAccess(clientFactory,
                    useCases.LocationDisplayFormatter, timeDataAccess);

            useCases.locationToolAssignment = new LocationToolAssignmentUseCaseHumbleAsyncRunner(new LocationToolAssignmentUseCase(locationToolAssignmentDataAccess, useCases.locationToolAssignmentGuiAdapter, sessionInformationLocal, notificationManager, useCases.TestDateCalculation));


            useCases.toleranceClassGuiAdapter = new ToleranceClassInterfaceAdapter();
            IToleranceClassData toleranceClassDataAccess =
                new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.ToleranceClassDataAccess(clientFactory, useCases.LocationDisplayFormatter, timeDataAccess);


            useCases.toleranceClass = new ToleranceClassHumbleAsyncRunner(
                new ToleranceClassUseCase(useCases.toleranceClassGuiAdapter, 
                toleranceClassDataAccess, 
                sessionInformationLocal, 
                locationToolAssignmentDataAccess, 
                notificationManager));

            useCases.toolGuiAdapter = new ToolInterfaceAdapter();
            IToolData toolDataAccess =
                new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.ToolDataAccess(
                    clientFactory,
                    useCases.locationToolAssignmentDisplayFormatter,
                    new FrameworksAndDrivers.RemoteData.GRPC.Utils.PictureFromZipLoader());

            useCases.tool = new ToolUseCase(toolDataAccess, useCases.toolGuiAdapter, toolModelPictureAndDataAccess, useCases.toolModel, useCases.toolType, useCases.status, useCases.costCenter, useCases.configurableField, sessionInformationLocal, notificationManager);

            useCases.locationGuiAdapter = new LocationInterfaceAdapter(localizationWrapper);
            ILocationData locationDataAccess = new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.LocationDataAccess(clientFactory, new FrameworksAndDrivers.RemoteData.GRPC.Utils.PictureFromZipLoader());
            IProcessControlData processControlDataAccess = new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.ProcessControlDataAccess(clientFactory);

            useCases.location = new LocationUseCase(useCases.locationGuiAdapter, locationDataAccess, locationToolAssignmentDataAccess, toolDataAccess, sessionInformationLocal, useCases.toleranceClass, notificationManager, processControlDataAccess);

            ITrashData trashDataAccess = new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.TrashDataAccess(clientFactory);
            useCases.trashGuiAdapter = new TrashInterfaceAdapter(localizationWrapper);
            useCases.trash = new TrashUseCase(useCases.trashGuiAdapter, trashDataAccess, locationDataAccess, sessionInformationLocal, notificationManager, processControlDataAccess, locationToolAssignmentDataAccess);
            
            useCases.toolUsageGuiAdapter = new HelperTableInterfaceAdapter<ToolUsage>();
            IHelperTableData<ToolUsage> toolUsageDataAccess =
                new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.ToolUsageDataAccess(clientFactory);


            useCases.toolUsage = new HelperTableUseCaseHumbleAsyncRunner<ToolUsage>(
                new HelperTableUseCase<ToolUsage>(
                    toolUsageDataAccess,
                    useCases.toolUsageGuiAdapter, 
                    sessionInformationLocal,
                    notificationManager,
                    locationToolAssignmentDataAccess));

            useCases.testEquipmentInterface = new TestEquipmentInterfaceAdapter(localizationWrapper);
            ITestEquipmentDataAccess testEquipmentDataAccess = null;
            testEquipmentDataAccess = new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.TestEquipmentDataAccess(clientFactory);

            useCases.testEquipment =
                    new TestEquipmentUseCaseHumbleAsyncRunner(
                        new TestEquipmentUseCase(
                            testEquipmentDataAccess,
                            useCases.testEquipmentInterface,
                            sessionInformationLocal,
                            notificationManager));

                useCases.transferToTestEquipmentGuiAdapter = new TransferToTestEquipmentGuiProxy();
            ITransferToTestEquipmentDataAccess transferToTestEquipmentDataAccess =
                new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.TransferToTestEquipmentDataAccess(clientFactory,
                    timeDataAccess, locationToolAssignmentDataAccess);

            useCases.TransferToTestEquipment =
                new TransferToTestEquipmentUseCaseHumbleAsyncRunner(
                    new TransferToTestEquipmentUseCase(
                        transferToTestEquipmentDataAccess,
                        locationToolAssignmentDataAccess,
                        new DataGateDataAccess(
                            new DataGateFileSystemHumbleObject(),
                            new DataGateFileGenerator(),
                            new StatusFileParserHumbleObject(),
                            new ResultFileParser()),
                        cmCmkDataAccess,
                        timeDataAccess,
                        useCases.transferToTestEquipmentGuiAdapter,
                        new SemanticModelFactory(useCases.treePathBuilder),
                        new Sta6000RewriteBuilder(),
                        new CommunicationProgramController(new HumbleDefaultProcessController()),
                        sessionInformationLocal,
                        useCases.TestDateCalculation,
                        notificationManager,
                        useCases.location,
                        useCases.treePathBuilder,
                        null,
                        null));

            useCases.processControlInterfaceAdapter = new ProcessControlInterfaceAdapter(localizationWrapper);
            useCases.classicTestInterfaceAdapter = new ClassicTestInterfaceAdapter();

            IClassicTestData classicTestDataAccess =
                new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.ClassicTestDataAccess(clientFactory, timeDataAccess);

            useCases.classicTest = new ClassicTestUseCase(useCases.classicTestInterfaceAdapter, classicTestDataAccess, useCases.location, toleranceClassDataAccess);

            useCases.changeToolStateForLocationGuiProxy = new ChangeToolStateForLocationGuiProxy();
            useCases.changeToolStateForLocationUseCase = new ChangeToolStateForLocationUseCaseHumbleAsyncRunner(
                new ChangeToolStateForLocationUseCase(locationToolAssignmentDataAccess, toolDataAccess, useCases.changeToolStateForLocationGuiProxy, sessionInformationLocal));

            useCases.processControlInterfaceAdapter = new ProcessControlInterfaceAdapter(localizationWrapper);

            useCases.processControl = new ProcessControlUseCase(useCases.processControlInterfaceAdapter, processControlDataAccess, sessionInformationLocal, notificationManager, useCases.TestDateCalculation);

            useCases.workingCalendarInterface = new WorkingCalendarInterfaceAdapter();
            IWorkingCalendarData workingCalendarDataAccess =
                new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.WorkingCalendarDataAccess(clientFactory);

            useCases.workingCalendar = new WorkingCalendarHumbleAsyncRunner(
                new WorkingCalendarUseCase(useCases.workingCalendarInterface, workingCalendarDataAccess, useCases.TestDateCalculation, useCases.locationToolAssignment, useCases.processControl, sessionInformationLocal));

            IShiftManagementData shiftManagementData =
                new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.ShiftManagementDataAccess(clientFactory);

            useCases.shiftManagementInterface = new ShiftManagementInterfaceAdapter(localizationWrapper);
            useCases.shiftManagement = new ShiftManagementHumbleAsyncRunner(
                new ShiftManagementUseCase(useCases.shiftManagementInterface, shiftManagementData, notificationManager, useCases.TestDateCalculation, useCases.locationToolAssignment, useCases.processControl, sessionInformationLocal));

            ITestLevelSetData testLevelSetDataAccess = new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.TestLevelSetDataAccess(clientFactory);

            useCases.TestLevelSetInterface = new TestLevelSetInterfaceAdapter(localizationWrapper);
            useCases.TestLevelSet = new TestLevelSetHumbleAsyncRunner(
                new TestLevelSetUseCase(useCases.TestLevelSetInterface, testLevelSetDataAccess, notificationManager, useCases.TestDateCalculation, useCases.locationToolAssignment, useCases.processControl, sessionInformationLocal));

            useCases.extensionInterface = new ExtensionInterfaceAdapter(localizationWrapper);
            IExtensionDataAccess extensionDataAccess =
                new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.ExtensionDataAccess(clientFactory, useCases.LocationDisplayFormatter, timeDataAccess);


            useCases.extension = new ExtensionHumbleAsyncRunner(
                new ExtensionUseCase(useCases.extensionInterface,
                extensionDataAccess,
                sessionInformationLocal,
                notificationManager));

            useCases.TestLevelSetAssignmentInterface = new TestLevelSetAssignmentInterfaceAdapter(localizationWrapper);
            useCases.TestLevelSetAssignment = new TestLevelSetAssignmentHumbleAsyncRunner(
                new TestLevelSetAssignmentUseCase(useCases.TestLevelSetAssignmentInterface, useCases.processControlInterfaceAdapter, new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.TestLevelSetAssignmentDataAccess(clientFactory), notificationManager, useCases.TestDateCalculation, useCases.locationToolAssignment, useCases.processControl, sessionInformationLocal));

            useCases.historyInterfaceAdapter = new HistoryInterfaceAdapter();
            useCases.history = new HistoryUseCaseHumbleAsyncRunner(new HistoryUseCase(new HistoryDataAccess(clientFactory), useCases.historyInterfaceAdapter, useCases.LocationDisplayFormatter));

            ILogoutData logoutDataAccess = new FrameworksAndDrivers.RemoteData.GRPC.DataAccess.LogoutDataAccess(clientFactory);
            
            useCases.logoutGuiProxy = new LogoutGuiProxy();
            useCases.logout = new LogoutUseCase(logoutDataAccess, useCases.logoutGuiProxy);
            var themeUris = new Dictionary<Theme, Uri>
            {
                { Theme.Normal, new Uri("/FrameworksAndDrivers.Gui.Wpf;component/View/Themes/QstColors.xaml", UriKind.RelativeOrAbsolute) },
                { Theme.Dark, new Uri("/FrameworksAndDrivers.Gui.Wpf;component/View/Themes/DarkModeColors.xaml", UriKind.RelativeOrAbsolute) }
            };

            var startUp = new StartUpImpl(useCases, new ThemeDictionary(themeUris), localizationWrapper);
			return startUp;
		}

		private static void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            //TODO: Logging unhandled Exception
        }

        private static void ApplicationOnStartup(object sender, StartupEventArgs e, LocalizationWrapper localizationWrapper, IStartUp startUp)
        {
            SetupApplication(localizationWrapper, startUp);
        }

		private static void SetupApplication(LocalizationWrapper localizationWrapper, IStartUp startUp)
		{
			var result = MainView.ResultCode.LOGIN;
			while (result != MainView.ResultCode.EXIT)
			{
				switch (result)
				{
					case MainView.ResultCode.EXIT:
						break;

					case MainView.ResultCode.RELOAD:
						result = ShowMainWindow(localizationWrapper, startUp);
						break;

					case MainView.ResultCode.LOGIN:
						result = ShowLoginWindow(startUp)
							? MainView.ResultCode.RELOAD
							: MainView.ResultCode.EXIT;
						break;
				}
			}
			Application.Current.Shutdown();
		}

		private static MainView.ResultCode ShowMainWindow(LocalizationWrapper localizationWrapper, IStartUp startUp)
        {
            var mainView = startUp.OpenMainView();
            mainView.ShowDialog();
            return mainView.Result;
        }

        private static bool ShowLoginWindow(IStartUp startUp)
        {
			var loginView = startUp.OpenLogin();
            return loginView.ShowDialog().GetValueOrDefault(false);
        }

        private static void Application_Exit(object sender, ExitEventArgs e)
        {
        }
    }
}
