using Core.Entities;
using FrameworksAndDrivers.Gui.Wpf.View;
using FrameworksAndDrivers.Gui.Wpf.View.Themes;
using FrameworksAndDrivers.Gui.Wpf.ViewModel.Controls;
using System;
using System.Collections.Generic;
using System.Windows;
using Client.Core.Entities;
using Common.Types.Enums;
using Core.UseCases.Communication;
using FrameworksAndDrivers.Gui.Wpf.Assistent;
using FrameworksAndDrivers.Gui.Wpf.Model;
using ToolModel = Core.Entities.ToolModel;

namespace FrameworksAndDrivers.Gui.Wpf
{
    public interface IStartUp
	{
        ThemeDictionary ThemeDictionary { get; }
        HelperTableView OpenShutOffHelperTable();
        void OpenShutOffHelperTableDialog(Window owner = null);
        HelperTableView OpenSwitchOffHelperTable();
        void OpenSwitchOffHelperTableDialog(Window owner = null);
		HelperTableView OpenDriveSizeHelperTable();
        void OpenDriveSizeHelperTableDialog(Window owner = null);
		HelperTableView OpenDriveTypeHelperTable();
        void OpenDriveTypeHelperTableDialog(Window owner = null);
		HelperTableView OpenToolTypeHelperTable();
        void OpenToolTypeHelperTableDialog(Window owner = null);
		HelperTableView OpenStatusHelperTable();
        void OpenStatusHelperTableDialog(Window owner = null);
		HelperTableView OpenReasonForToolChangeHelperTable();
        void OpenReasonForToolChangeHelperTableDialog(Window owner = null);
		HelperTableView OpenConstructionTypeHelperTable();
        void OpenConstructionTypeHelperTableDialog(Window owner = null);
		HelperTableView OpenConfigurableFieldHelperTable();
        void OpenConfigurableFieldHelperTableDialog(Window owner = null);
        HelperTableView OpenCostCenterHelperTable();
        void OpenCostCenterHelperTableDialog(Window owner = null);
        ManufacturerView OpenManufacturer();
        void OpenManufacturerDialog(Window owner = null);
		ToleranceClassView OpenToleranceClass();
        void OpenToleranceClassDialog(Window owner = null);
        HelperTableView OpenToolUsage();
        void OpenToolUsageDialog(Window owner = null);
        ExtensionView OpenExtension();  
        void OpenExtensionDialog(Window owner = null);
        QstInformationView OpenQstInformation();
		LoginView OpenLogin();
		GlobalTreeViewModel OpenGlobalTree();
		ToolModelView OpenToolModel();
        void OpenToolModelDialog(Window owner = null);
        ToolView OpenTool();
        void OpenToolDialog(Window owner = null);
        TestEquipmentView OpenTestEquipment();
        void OpenTestEquipmentDialog(Window owner = null);
        LocationView OpenLocation();
        void OpenLocationDialog(Window owner = null);

        TestLevelSetView OpenTestLevelSet();
        void OpenTestLevelSetDialog(Window owner = null);

        ToolTestPlanningView OpenToolTestPlanning();
        void OpenToolTestPlanningDialog(Window owner = null);
        ProcessControlPlanningView OpenProcessControlPlanning();
        void OpenProcessControlPlanningDialog(Window owner = null);

        void OpenLocationToolAssignmentToolDetailDialog(List<ToolModelWithToolUsage> toolModels);
        TransferToTestEquipmentView OpenTransferToTestEquipment();
        LocationToolAssignmentView OpenLocationToolAssignment();
        ProcessControlView OpenProcessControl();

        TrashView OpenTrash();
        ClassicTestView OpenClassicTest();

        ClassicTestHtmlView OpenClassicChkTestHtmlView(Location location, Tool tool, List<ClassicChkTest> tests);
        ClassicTestHtmlView OpenClassicMfuTestHtmlView(Location location, Tool tool, List<ClassicMfuTest> tests);
        ClassicTestHtmlView OpenClassicProcessMonitoringTestHtmlView(Location location, List<ClassicProcessTest> tests);
        ClassicTestHtmlView OpenClassicProcessPfuTestHtmlView(Location location, List<ClassicProcessTest> tests);

        LocationHistoryView OpenLocationHistoryView();
        void OpenLocationHistoryDialog(Window owner = null);

        AssistentView OpenAddToolModelAssistent(ToolModel defaultToolModel = null);
        AssistentView OpenAddToolAssistent(Tool defaultTool = null);
        AssistentView OpenAddTestEquipmentAssistant(List<TestEquipmentType> availableTestEquipmentTypes, TestEquipment testEquipmentDefault = null, TestEquipmentModel testEquipmentModelDefault = null, TestEquipmentType? testEquipmentTypeDefault = null);
        AssistentView OpenAddLocationAssistent(Location defaultLocation = null);
        AssistentView OpenAssignToolToLocationAssistent(LocationToolAssignment assignment);
        AssistentView OpenAddTestConditionsAssistent(LocationToolAssignment defaultAssignment);
        AssistentView OpenAddProcessControlAssistant(Location location);
        AssistentView OpenAddWorkingCalendarEntryAssistant(bool areSaturdaysFree, bool areSundaysFree, Func<DateTime, WorkingCalendarEntry> getExistingEntryForDate, DateTime? defaultDate = null, WorkingCalendarEntryType? defaultType = null);
        AssistentView OpenAddTestPlanAssistant(TestPlan defaultTestPlan = null);
        HelperTableItemAssistentPlan<Status> GetStatusAssistentPlan(IAssistentView assistantView, HelperTableEntityId defaultId);
        IChangeToolStateView OpenChangeToolStatusAssistant(List<LocationToolAssignment> assignments);

        MainView OpenMainView();

        TestEquipmentTestResultView OpenTestEquipmentTestResultView(List<TestEquipmentTestResult> testEquipmentTestResult);

        void RaiseShowLoadingControl(bool showLoadingControl, int count = 1);
        IAssistentView GetAssistentView(string header);
        AssistentView OpenAddExtensionAssistant(Extension entity);
    }
}
