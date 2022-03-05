using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Core.Entities;
using Core.Entities.ReferenceLink;
using Core.UseCases;
using FrameworksAndDrivers.Gui.Wpf.Model;
using FrameworksAndDrivers.Localization;

namespace FrameworksAndDrivers.Gui.Wpf.Assistent
{
    public class UnusedToolUsageAssistentPlan : HelperTableItemAssistentPlan<ToolUsage>, ILocationToolAssignmentGui
    {
        private ILocationToolAssignmentUseCase _locationToolAssignmentUseCase;
        private LocalizationWrapper _localization;
        private readonly LocationId _locationId;


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
            // Do nothing
        }

        public void AssignToolToLocationError()
        {
            // Do nothing
        }

        public void ShowAssignedToolsForLocation(List<LocationToolAssignment> assignments)
        {
            // Do nothing
        }

        public void LoadAssignedToolsForLocationError()
        {
            // Do nothing
        }

        public void AddTestConditions(LocationToolAssignment assignment)
        {
            var items = AssistentItem.GetCurrentListItems();
            var usage = items.FirstOrDefault(x => x.EqualsById(assignment.ToolUsage));

            if (usage != null)
            {
                items.Remove(usage);
                AssistentItem.RefillListItems(items);
            }
        }

        public void AddTestConditionsError()
        {
            // Do nothing
        }

        public void ShowUnusedToolUsagesForLocation(List<ToolUsage> toolUsages, LocationId locationId)
        {
            AssistentItem.RefillListItems(toolUsages);
        }

        public void LoadUnusedToolUsagesForLocationError()
        {
            MessageBox.Show(
                _localization.Strings.GetParticularString("Unused tool usage assistent plan", "An error occured while loading tool usages"),
                _localization.Strings.GetParticularString("Unused tool usage assistent plan", "Error"),
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }

        public void RemoveLocationToolAssignment(LocationToolAssignment assignment)
        {
            AssistentItem.AddListItem(assignment.ToolUsage);
        }

        public void RemoveLocationToolAssignmentError()
        {
            // Do nothing
        }

        public void ShowLocationReferenceLinksForTool(List<LocationReferenceLink> locationReferenceLinks)
        {
            //Do nothing
        }

        public void UpdateLocationToolAssignment(List<LocationToolAssignment> updatedLocationToolAssignment)
        {
            //Do nothing
        }

        public void UpdateLocationToolAssignmentError()
        {
            //Do nothing
        }

        #endregion


        public override void ShowItems(List<ToolUsage> items)
        {
            // Do nothing (We don't want to show all ToolUsages, just the unused ones -> they are loaded at the beginning, adds/removes/updates are handled)
        }

        public override void Initialize()
        {
            _locationToolAssignmentUseCase.LoadUnusedToolUsagesForLocation(_locationId);

            base.Initialize();
        }


        public UnusedToolUsageAssistentPlan(ILocationToolAssignmentUseCase locationToolAssignmentUseCase, IHelperTableUseCase<ToolUsage> toolUsageUseCase, LocationId locationId, LocalizationWrapper localization, ListAssistentItemModel<ToolUsage> item) : base(toolUsageUseCase, item)
        {
            _locationToolAssignmentUseCase = locationToolAssignmentUseCase;
            _locationId = locationId;
            _localization = localization;
            base.DisableInitialization = true;
        }

        public UnusedToolUsageAssistentPlan(ILocationToolAssignmentUseCase locationToolAssignmentUseCase, IHelperTableUseCase<ToolUsage> toolUsageUseCase, LocationId locationId, LocalizationWrapper localization, List<AssistentPlan> subPlans, ListAssistentItemModel<ToolUsage> item) : base(toolUsageUseCase, subPlans, item)
        {
            _locationToolAssignmentUseCase = locationToolAssignmentUseCase;
            _locationId = locationId;
            _localization = localization;
            base.DisableInitialization = true;
        }
    }
}
