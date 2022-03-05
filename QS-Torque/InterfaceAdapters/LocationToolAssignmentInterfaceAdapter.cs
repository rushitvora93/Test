using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Core.Entities;
using Core.Entities.ReferenceLink;
using Core.UseCases;
using InterfaceAdapters.Localization;
using InterfaceAdapters.Models;

namespace InterfaceAdapters
{
    public interface ILocationToolAssignmentInterface : INotifyPropertyChanged
    {
        ObservableCollection<LocationToolAssignmentModel> LocationToolAssignments { get; }

        event EventHandler LocationToolAssignmentErrorRequest;
    }


    public class LocationToolAssignmentInterfaceAdapter : InterfaceAdapter<ILocationToolAssignmentGui>, ILocationToolAssignmentInterface, ILocationToolAssignmentGui
    {
        private ILocalizationWrapper _localization;
        
        private ObservableCollection<LocationToolAssignmentModel> _locationToolAssignments = new ObservableCollection<LocationToolAssignmentModel>();
        public ObservableCollection<LocationToolAssignmentModel> LocationToolAssignments
        {
            get => _locationToolAssignments;
            private set
            {
                _locationToolAssignments = value;
                RaisePropertyChanged();
            }
        }
        
        public event EventHandler LocationToolAssignmentErrorRequest;

        public LocationToolAssignmentInterfaceAdapter(ILocalizationWrapper localization)
        {
            _localization = localization;
        }



        public void LoadLocationToolAssignments(List<LocationToolAssignment> locationToolAssignments)
        {
            var assignments = new ObservableCollection<LocationToolAssignmentModel>();
            foreach (var assignment in locationToolAssignments)
            {
                assignments.Add(LocationToolAssignmentModel.GetModelFor(assignment, _localization));
            }
            LocationToolAssignments = assignments;
        }

        public void ShowLocationToolAssignmentError()
        {
            LocationToolAssignmentErrorRequest?.Invoke(this, null);
        }

        public void AssignToolToLocation(LocationToolAssignment assignment)
        {
            InvokeActionOnGuiInterfaces(gui => gui.AssignToolToLocation(assignment));
        }

        public void AssignToolToLocationError()
        {
            InvokeActionOnGuiInterfaces(gui => gui.AssignToolToLocationError());
        }

        public void ShowAssignedToolsForLocation(List<LocationToolAssignment> assignments)
        {
            InvokeActionOnGuiInterfaces(gui => gui.ShowAssignedToolsForLocation(assignments));
        }

        public void LoadAssignedToolsForLocationError()
        {
            InvokeActionOnGuiInterfaces(gui => gui.LoadAssignedToolsForLocationError());
        }

        public void AddTestConditions(LocationToolAssignment assignment)
        {
            InvokeActionOnGuiInterfaces(gui => gui.AddTestConditions(assignment));
        }

        public void AddTestConditionsError()
        {
            InvokeActionOnGuiInterfaces(gui => gui.AddTestConditionsError());
        }

        public void ShowUnusedToolUsagesForLocation(List<ToolUsage> toolUsages, LocationId locationId)
        {
            InvokeActionOnGuiInterfaces(gui => gui.ShowUnusedToolUsagesForLocation(toolUsages, locationId));
        }

        public void LoadUnusedToolUsagesForLocationError()
        {
            InvokeActionOnGuiInterfaces(gui => gui.LoadUnusedToolUsagesForLocationError());
        }

        public void RemoveLocationToolAssignment(LocationToolAssignment assignment)
        {
            InvokeActionOnGuiInterfaces(gui => gui.RemoveLocationToolAssignment(assignment));
        }

        public void RemoveLocationToolAssignmentError()
        {
            InvokeActionOnGuiInterfaces(gui => gui.RemoveLocationToolAssignmentError());
        }

        public void ShowLocationReferenceLinksForTool(List<LocationReferenceLink> locationReferenceLinks)
        {
            InvokeActionOnGuiInterfaces(gui => gui.ShowLocationReferenceLinksForTool(locationReferenceLinks));
        }

        public void UpdateLocationToolAssignment(List<LocationToolAssignment> updatedLocationToolAssignment)
        {
            InvokeActionOnGuiInterfaces(gui => gui.UpdateLocationToolAssignment(updatedLocationToolAssignment));
        }

        public void UpdateLocationToolAssignmentError()
        {
            InvokeActionOnGuiInterfaces(gui => gui.UpdateLocationToolAssignmentError());
        }
    }
}
