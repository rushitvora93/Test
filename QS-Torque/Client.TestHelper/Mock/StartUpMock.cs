using Core.Entities;
using FrameworksAndDrivers.Gui.Wpf;
using FrameworksAndDrivers.Gui.Wpf.View;
using FrameworksAndDrivers.Gui.Wpf.View.Themes;
using FrameworksAndDrivers.Gui.Wpf.ViewModel.Controls;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Threading;
using Client.Core.Entities;
using Common.Types.Enums;
using Core.UseCases.Communication;
using FrameworksAndDrivers.Gui.Wpf.Assistent;
using FrameworksAndDrivers.Gui.Wpf.Model;
using ToolModel = Core.Entities.ToolModel;

namespace TestHelper.Mock
{
    public class StartUpMock : IStartUp
    {
        public ThemeDictionary ThemeDictionary => throw new System.NotImplementedException();
        public int OpenLocationToolAssignmentToolDetailDialogCallCount { get; set; }
        public List<ToolModelWithToolUsage> OpenLocationToolAssignmentToolDetailDialogParameter { get; set; }
        public int OpenChangeToolStatusAssistantCallCount { get; set; }
        public IChangeToolStateView OpenChangeToolStatusAssistantReturn { get; set; }
        public IAssistentView GetAssistentViewReturn { get; set; }
        public bool? RaiseShowLoadingControlShowLoadingControl { get; set; }
        public int RaiseShowLoadingControlCount { get; set; }

        public HelperTableView OpenShutOffHelperTable()
        {
            throw new System.NotImplementedException();
        }

        public void OpenShutOffHelperTableDialog(Window owner = null)
        {
            throw new NotImplementedException();
        }

        public HelperTableView OpenSwitchOffHelperTable()
        {
            throw new System.NotImplementedException();
        }

        public void OpenSwitchOffHelperTableDialog(Window owner = null)
        {
            throw new NotImplementedException();
        }

        public HelperTableView OpenDriveSizeHelperTable()
        {
            throw new System.NotImplementedException();
        }

        public void OpenDriveSizeHelperTableDialog(Window owner = null)
        {
            throw new NotImplementedException();
        }

        public HelperTableView OpenDriveTypeHelperTable()
        {
            throw new System.NotImplementedException();
        }

        public void OpenDriveTypeHelperTableDialog(Window owner = null)
        {
            throw new NotImplementedException();
        }

        public HelperTableView OpenToolTypeHelperTable()
        {
            throw new System.NotImplementedException();
        }

        public void OpenToolTypeHelperTableDialog(Window owner = null)
        {
            throw new NotImplementedException();
        }

        public HelperTableView OpenStatusHelperTable()
        {
            throw new System.NotImplementedException();
        }

        public void OpenStatusHelperTableDialog(Window owner = null)
        {
            throw new NotImplementedException();
        }

        public HelperTableView OpenReasonForToolChangeHelperTable()
        {
            throw new System.NotImplementedException();
        }

        public void OpenReasonForToolChangeHelperTableDialog(Window owner = null)
        {
            throw new NotImplementedException();
        }

        public HelperTableView OpenConstructionTypeHelperTable()
        {
            throw new System.NotImplementedException();
        }

        public void OpenConstructionTypeHelperTableDialog(Window owner = null)
        {
            throw new NotImplementedException();
        }

        public HelperTableView OpenConfigurableFieldHelperTable()
        {
            throw new System.NotImplementedException();
        }

        public void OpenConfigurableFieldHelperTableDialog(Window owner = null)
        {
            throw new NotImplementedException();
        }

        public HelperTableView OpenCostCenterHelperTable()
        {
            throw new System.NotImplementedException();
        }

        public void OpenCostCenterHelperTableDialog(Window owner = null)
        {
            throw new NotImplementedException();
        }

        public ManufacturerView OpenManufacturer()
        {
            throw new System.NotImplementedException();
        }

        public void OpenManufacturerDialog(Window owner = null)
        {
            throw new NotImplementedException();
        }

        public ToleranceClassView OpenToleranceClass()
        {
            throw new System.NotImplementedException();
        }

        public void OpenToleranceClassDialog(Window owner = null)
        {
            throw new NotImplementedException();
        }

        public HelperTableView OpenToolUsage()
        {
            throw new NotImplementedException();
        }

        public void OpenExtensionDialog(Window owner = null)
        {
            throw new NotImplementedException();
        }

        public ExtensionView OpenExtension()
        {
            throw new NotImplementedException();
        }

        public void OpenToolUsageDialog(Window owner = null)
        {
            throw new NotImplementedException();
        }

        public QstInformationView OpenQstInformation()
        {
            throw new System.NotImplementedException();
        }

        public LoginView OpenLogin()
        {
            throw new System.NotImplementedException();
        }

        public GlobalTreeViewModel OpenGlobalTree()
        {
            throw new System.NotImplementedException();
        }

        public ToolModelView OpenToolModel()
        {
            throw new System.NotImplementedException();
        }

        public void OpenToolModelDialog(Window owner = null)
        {
            throw new NotImplementedException();
        }

        public ToolView OpenTool()
        {
            throw new System.NotImplementedException();
        }

        public void OpenToolDialog(Window owner = null)
        {
            throw new NotImplementedException();
        }

        public TestEquipmentView OpenTestEquipment()
        {
            throw new NotImplementedException();
        }

        public void OpenTestEquipmentDialog(Window owner = null)
        {
            throw new NotImplementedException();
        }

        public LocationView OpenLocation()
        {
            throw new NotImplementedException();
        }

        public void OpenLocationDialog(Window owner = null)
        {
            throw new NotImplementedException();
        }

        public void OpenToolTestPlanningDialog(Window owner = null)
        {
            throw new NotImplementedException();
        }

        public void OpenLocationToolAssignmentToolDetailDialog(List<ToolModelWithToolUsage> toolModels)
        {
            OpenLocationToolAssignmentToolDetailDialogParameter = toolModels;
            OpenLocationToolAssignmentToolDetailDialogCallCount++;
        }

        public TransferToTestEquipmentView OpenTransferToTestEquipment()
        {
            throw new NotImplementedException();
        }

        public LocationToolAssignmentView OpenLocationToolAssignment()
        {
            throw new NotImplementedException();
        }

        public ProcessControlView OpenProcessControl()
        {
            throw new NotImplementedException();
        }

        public ClassicTestHtmlView OpenClassicProcessMonitoringTestHtmlViewReturnValue { get; set; }
        public List<ClassicProcessTest> OpenClassicProcessMonitoringTestHtmlViewTests { get; set; }
        public Location OpenClassicProcessMonitoringTestHtmlViewLocation { get; set; }
        public ClassicTestHtmlView OpenClassicProcessMonitoringTestHtmlView(Location location, List<ClassicProcessTest> tests)
        {
            OpenClassicProcessMonitoringTestHtmlViewLocation = location;
            OpenClassicProcessMonitoringTestHtmlViewTests = tests;
            return OpenClassicProcessMonitoringTestHtmlViewReturnValue;
        }

        public ClassicTestHtmlView OpenClassicProcessPfuTestHtmlViewReturnValue { get; set; }
        public List<ClassicProcessTest> OpenClassicProcessPfuTestHtmlViewTests { get; set; }
        public Location OpenClassicProcessPfuTestHtmlViewLocation { get; set; }
        public ClassicTestHtmlView OpenClassicProcessPfuTestHtmlView(Location location, List<ClassicProcessTest> tests)
        {
            OpenClassicProcessPfuTestHtmlViewLocation = location;
            OpenClassicProcessPfuTestHtmlViewTests = tests;
            return OpenClassicProcessPfuTestHtmlViewReturnValue;
        }


        public AssistentView OpenAddToolModelAssistent(ToolModel defaultToolModel = null)
        {
            throw new System.NotImplementedException();
        }

        public AssistentView OpenAddTestConditionsAssistent(LocationToolAssignment defaultAssignment)
        {
            throw new NotImplementedException();
        }

        public AssistentView OpenAddProcessControlAssistantReturnValue { get; set; }
        public Location OpenAddProcessControlAssistantParameter { get; set; }

        public AssistentView OpenAddProcessControlAssistant(Location location)
        {
            OpenAddProcessControlAssistantParameter = location;
            return OpenAddProcessControlAssistantReturnValue;
        }

        public AssistentView OpenAddWorkingCalendarEntryAssistant(bool areSaturdaysFree, bool areSundaysFree, Func<DateTime, WorkingCalendarEntry> getExistingEntryForDate, DateTime? defaultDate = null, WorkingCalendarEntryType? defaultType = null)
        {
            return new AssistentView("");
        }

        public AssistentView OpenAddTestPlanAssistant(TestPlan defaultTestPlan = null)
        {
            return new AssistentView("");
        }

        public HelperTableItemAssistentPlan<Status> GetStatusAssistentPlan(IAssistentView assistantView, HelperTableEntityId defaultId)
        {
            return new HelperTableItemAssistentPlan<Status>(null, new ListAssistentItemModel<Status>(Dispatcher.CurrentDispatcher,
                "",
                "",
                null,
                (o, i) => { },
                null,
                x => "",
                () => { }));
        }

        public IChangeToolStateView OpenChangeToolStatusAssistant(List<LocationToolAssignment> assignments)
        {
            OpenChangeToolStatusAssistantCallCount++;
            return OpenChangeToolStatusAssistantReturn;
        }

        public MainView OpenMainView()
        {
            throw new NotImplementedException();
        }

        public TestEquipmentTestResultView OpenTestEquipmentTestResultView(List<TestEquipmentTestResult> testEquipmentTestResult)
        {
            throw new NotImplementedException();
        }

        public void RaiseShowLoadingControl(bool showLoadingControl, int count = 1)
        {
            RaiseShowLoadingControlShowLoadingControl = showLoadingControl;
            RaiseShowLoadingControlCount = count;
        }

        public bool WasOpenAddToolAssistent = false;
        public Tool OpenAddToolAssistenttParameter;
        public AssistentView OpenAddToolAssistentAssistentReturnValue;
        public AssistentView OpenAddToolAssistent(Tool defaultTool = null)
        {
            OpenAddToolAssistenttParameter = defaultTool;
            WasOpenAddToolAssistent = true;
            return OpenAddToolAssistentAssistentReturnValue;
        }

        public List<TestEquipmentType> OpenAddTestEquipmentAssistantParameterAvailableTypes { get; set; }
        public AssistentView OpenAddTestEquipmentAssistantReturnValue { get; set; }
        public TestEquipment OpenAddTestEquipmentAssistantDefaultTestEquipment { get; set; }
        public TestEquipmentModel OpenAddTestEquipmentAssistantDefaultTestEquipmentModel { get; set; }
        public TestEquipmentType? OpenAddTestEquipmentAssistantDefaultTestEquipmentType { get; set; }
        public AssistentView OpenAddTestEquipmentAssistant(List<TestEquipmentType> availableTestEquipmentTypes, TestEquipment testEquipmentDefault = null, TestEquipmentModel testEquipmentModelDefault = null, TestEquipmentType? testEquipmentTypeDefault = null)
        {
            OpenAddTestEquipmentAssistantParameterAvailableTypes = availableTestEquipmentTypes;
            OpenAddTestEquipmentAssistantDefaultTestEquipment = testEquipmentDefault;
            OpenAddTestEquipmentAssistantDefaultTestEquipmentModel = testEquipmentModelDefault;
            OpenAddTestEquipmentAssistantDefaultTestEquipmentType = testEquipmentTypeDefault;
            return OpenAddTestEquipmentAssistantReturnValue;
        }

        public bool WasOpenAddLocationAssistent = false;
        public Location OpenAddLocationAssistentParameter;
        public AssistentView OpenAddLocationAssistentReturnValue;
        public AssistentView OpenAddLocationAssistent(Location defaultLocation = null)
        {
            OpenAddLocationAssistentParameter = defaultLocation;
            WasOpenAddLocationAssistent = true;
            return OpenAddLocationAssistentReturnValue;
        }

        public ClassicTestView OpenClassicTest()
        {
            throw new NotImplementedException();
        }

        public AssistentView OpenAssignToolToLocationAssistent(LocationToolAssignment assignment)
        {
            throw new NotImplementedException();
        }

        public List<ClassicChkTest> OpenClassicChkTestHtmlViewTests { get; set; }
        public Tool OpenClassicChkTestHtmlViewTool { get; set; }
        public Location OpenClassicChkTestHtmlViewLocation { get; set; }
        public ClassicTestHtmlView OpenClassicChkTestHtmlView(Location location, Tool tool, List<ClassicChkTest> tests)
        {
            OpenClassicChkTestHtmlViewLocation = location;
            OpenClassicChkTestHtmlViewTool = tool;
            OpenClassicChkTestHtmlViewTests = tests;
            return null;
        }

        public List<ClassicMfuTest> OpenClassicMfuTestHtmlViewTests { get; set; }
        public Tool OpenClassicMfuTestHtmlViewTool { get; set; }
        public Location OpenClassicMfuTestHtmlViewLocation { get; set; }
        public ClassicTestHtmlView OpenClassicMfuTestHtmlView(Location location, Tool tool, List<ClassicMfuTest> tests)
        {
            OpenClassicMfuTestHtmlViewLocation = location;
            OpenClassicMfuTestHtmlViewTool = tool;
            OpenClassicMfuTestHtmlViewTests = tests;
            return null;
        }

        public IAssistentView GetAssistentView(string header)
        {
            return GetAssistentViewReturn;
        }

        public AssistentView OpenAddExtensionAssistantReturnValue { get; set; }
        public Extension OpenAddExtensionAssistantExtension { get; set; }
        public AssistentView OpenAddExtensionAssistant(Extension entity)
        {
            OpenAddExtensionAssistantExtension = entity;
            return OpenAddExtensionAssistantReturnValue;
        }

        public TestLevelSetView OpenTestLevelSet()
        {
            throw new NotImplementedException();
        }

        public void OpenTestLevelSetDialog(Window owner = null)
        {
            throw new NotImplementedException();
        }

        public ToolTestPlanningView OpenToolTestPlanning()
        {
            throw new NotImplementedException();
        }

        public ProcessControlPlanningView OpenProcessControlPlanning()
        {
            throw new NotImplementedException();
        }

        public void OpenProcessControlPlanningDialog(Window owner = null)
        {
            throw new NotImplementedException();
        }

        public LocationHistoryView OpenLocationHistoryView()
        {
            throw new NotImplementedException();
        }

        public void OpenLocationHistoryDialog(Window owner = null)
        {
            throw new NotImplementedException();
        }

        public TrashView OpenTrash()
        {
            throw new NotImplementedException();
        }
    }
}