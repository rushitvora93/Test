using Core.Entities;
using FrameworksAndDrivers.Gui.Wpf;
using FrameworksAndDrivers.Gui.Wpf.Model;
using FrameworksAndDrivers.Gui.Wpf.View;
using FrameworksAndDrivers.Localization;
using System.Collections.Generic;
using System.Linq;
using Core;

namespace StartUp.AssistentCreator
{
    public class ChangeToolStatusAssistentCreator 
    {
        private LocalizationWrapper _localization;
        private UseCaseCollection _useCases;
        private IStartUp _startUp;
        private List<LocationToolAssignment> _assignments;
        private IToolDisplayFormatter _toolDisplayFormatter;
        private ILocationDisplayFormatter _locationDisplayFormatter;

        public ChangeToolStatusAssistentCreator(LocalizationWrapper localization, UseCaseCollection useCases, IStartUp startUpImpl, 
            List<LocationToolAssignment> assignments, IToolDisplayFormatter toolDisplayFormatter, ILocationDisplayFormatter locationDisplayFormatter)
        {
            _localization = localization;
            _useCases = useCases;
            _toolDisplayFormatter = toolDisplayFormatter;
            _locationDisplayFormatter = locationDisplayFormatter;
            _startUp = startUpImpl;
            _assignments = assignments;
        }

        public ChangeToolStateView CreateChangeToolStatusAssistant()
        {
            var changeToolStateView = new ChangeToolStateView(_localization.Strings.GetParticularString("ChangeToolStatusAssistant", "Change Status of tools"), _startUp, _localization, _useCases.changeToolStateForLocationUseCase);
            var groupedAssignments = _assignments.GroupBy(x => x.AssignedLocation?.Id.ToLong());

            var tools = _assignments.Select(x => x.AssignedTool).ToList();
            
            var locationToolAssignmentList = new List<LocationToolAssignmentChangeStateModel>();
            foreach (var groupedAssignment in groupedAssignments)
            {
                foreach (var locationToolAssignment in groupedAssignment)
                {
                    var statusAssistantPlan = CreateLocationToolAssignmentStateChange(changeToolStateView, locationToolAssignment, locationToolAssignmentList);
                    
                    locationToolAssignmentList.Add(statusAssistantPlan);                    
                }
            }
            changeToolStateView.SetAssignedTools(locationToolAssignmentList);

            _useCases.changeToolStateForLocationGuiProxy.Real = changeToolStateView.ViewModel;
            _useCases.changeToolStateForLocationUseCase.LoadLocationsForTools(tools);

			if(!FeatureToggles.FeatureToggles.HelperTablesWithInterfaceAdapter)
			{
            	_useCases.statusGuiAdapter.RegisterGuiInterface(changeToolStateView.ViewModel);
			}
            _useCases.status.LoadItems(changeToolStateView.ViewModel);
            return changeToolStateView;
        }

        private LocationToolAssignmentChangeStateModel CreateLocationToolAssignmentStateChange(ChangeToolStateView changeToolStateView, LocationToolAssignment assignment, List<LocationToolAssignmentChangeStateModel> locationToolAssignmentStateModels)
        {
            var changeModel = new LocationToolAssignmentChangeStateModel(changeToolStateView.Dispatcher, assignment,
                _locationDisplayFormatter.Format(assignment.AssignedLocation),
                _localization.Strings.GetParticularString("Assign tool to location assistent", "Status for ") +
                _toolDisplayFormatter.Format(assignment.AssignedTool), assignment.AssignedTool.Status,  locationToolAssignmentStateModels);
            return changeModel;
        }
    }
}