using System.Collections.Generic;
using Core.Entities;
using FrameworksAndDrivers.Gui.Wpf;
using FrameworksAndDrivers.Gui.Wpf.Assistent;
using FrameworksAndDrivers.Gui.Wpf.Model;
using FrameworksAndDrivers.Gui.Wpf.View;
using FrameworksAndDrivers.Localization;

namespace StartUp.AssistentCreator
{
    class AssignToolToLocationAssistentCreator
    {
        private LocalizationWrapper _localization;
        private IStartUp _startUp;
        private UseCaseCollection _useCases;
        private LocationToolAssignment _defaultAssignment;

        public AssignToolToLocationAssistentCreator(LocalizationWrapper localization, UseCaseCollection useCases, StartUpImpl startUp, LocationToolAssignment defaultAssignment = null)
        {
            _localization = localization;
            _startUp = startUp;
            _useCases = useCases;
            _defaultAssignment = defaultAssignment;
        }


        public AssistentView CreateAssignToolToLocationAssistent()
        {
            AssistentView assistentView = new AssistentView(_localization.Strings.GetParticularString("Assign tool to location assistent", "Assign tool to location"));

            var toolUsagePlan = CreateToolUsageAssistentPlan(assistentView);
            var statusPlan = CreateStatusAssistentPlan(assistentView);

            _useCases.locationToolAssignmentGuiAdapter.RegisterGuiInterface(toolUsagePlan);
            _useCases.toolUsageGuiAdapter.RegisterGuiInterface(toolUsagePlan);
			if(!FeatureToggles.FeatureToggles.HelperTablesWithInterfaceAdapter)
			{
            	_useCases.statusGuiAdapter.RegisterGuiInterface(statusPlan);
			}

            var parentPlan = new ParentAssistentPlan(new List<ParentAssistentPlan>()
            {
                toolUsagePlan,
                statusPlan
            });

            assistentView.SetParentPlan(parentPlan);
            return assistentView;
        }


        #region Special AssistentPlanCreator

        private UnusedToolUsageAssistentPlan CreateToolUsageAssistentPlan(AssistentView assistentView)
        {
            return new UnusedToolUsageAssistentPlan(_useCases.locationToolAssignment,
                _useCases.toolUsage,
                _defaultAssignment.AssignedLocation.Id,
                _localization,
                new ListAssistentItemModel<ToolUsage>(assistentView.Dispatcher,
                    _localization.Strings.GetParticularString("Assign tool to location assistent", "Choose a tool usage"),
                    _localization.Strings.GetParticularString("Assign tool to location assistent", "Tool usage"),
                    null,
                    (o, i) => (o as LocationToolAssignment).ToolUsage = (i as ListAssistentItemModel<ToolUsage>).EnteredValue,
                    _localization.Strings.GetParticularString("Assign tool to location assistent", "Jump to tool usage"),
                    x => x.Value.ToDefaultString(),
                    () =>
                    {
                        _startUp.OpenToolUsageDialog(assistentView);
                    }));
        }

        private HelperTableItemAssistentPlan<Status> CreateStatusAssistentPlan(AssistentView assistentView)
        {
            return new HelperTableItemAssistentPlan<Status>(_useCases.status,
                new ListAssistentItemModel<Status>(assistentView.Dispatcher,
                    _localization.Strings.GetParticularString("Assign tool to location assistent", "Choose a status for the tool after the assignment"),
                    _localization.Strings.GetParticularString("Assign tool to location assistent", "Status for tool"),
                    null,
                    (o, i) => (o as LocationToolAssignment).AssignedTool.Status = (i as ListAssistentItemModel<Status>).EnteredValue,
                    _localization.Strings.GetParticularString("Assign tool to location assistent", "Jump to status"),
                    x => x.Value.ToDefaultString(),
                    () =>
                    {
                        _startUp.OpenStatusHelperTableDialog(assistentView);
                    }),
                _defaultAssignment.AssignedTool.Status.ListId);
        }

        #endregion
    }
}
