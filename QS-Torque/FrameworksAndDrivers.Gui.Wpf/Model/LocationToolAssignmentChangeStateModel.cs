using Core.Entities;
using Core.Entities.ReferenceLink;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using InterfaceAdapters;
using InterfaceAdapters.Models;

namespace FrameworksAndDrivers.Gui.Wpf.Model
{
    public class LocationToolAssignmentChangeStateModel : BindableBase
    {
        private Dispatcher _guiDispatcher;
        private readonly List<LocationToolAssignmentChangeStateModel> _locationToolAssignmentChangeStateModels;

        private LocationToolAssignment _locationToolAssignment;
        public LocationToolAssignment LocationToolAssignment
        {
            get => _locationToolAssignment;
            set => Set(ref _locationToolAssignment, value);
        }


        private string _location;
        public string Location
        {
            get => _location;
            set => Set(ref _location, value);
        }

        private string _powTool;
        public string PowTool
        {
            get => _powTool;
            set => Set(ref _powTool, value);
        }

        private Status _stateId;
        public Status StateId
        {
            get => _stateId;
            set => Set(ref _stateId, value);
        }

        public HelperTableItemModel<Status, string> Status
        {
            get => StateId != null ? HelperTableItemModel.GetModelForStatus(StateId) : null;
            set
            {
                StateId = value?.Entity;

                var locationToolAssigmentModelsWithSameTool = _locationToolAssignmentChangeStateModels.Where(x =>
                    !x.LocationToolAssignment.Id.Equals(LocationToolAssignment.Id) &&
                    x.LocationToolAssignment.AssignedTool.Id.Equals(LocationToolAssignment.AssignedTool.Id) &&
                    !x.StateId.EqualsById(StateId));

                foreach (var locationToolAssignment in locationToolAssigmentModelsWithSameTool)
                {
                    locationToolAssignment.Status = Status;
                }

                RaisePropertyChanged();
            }
        }

        public bool HasOtherConnectedLocations
        {
            get => (_otherConnectedLocations.Length > 0);
        }

        private string _otherConnectedLocations;
        public string OtherConnectedLocations
        {
            get => _otherConnectedLocations;
            set => Set(ref _otherConnectedLocations, value);
        }

        #region Methods

        public void SetAttributeValueToResultObject(List<LocationToolAssignment> resultLocationToolAssignments)
        {
            var foundLocationToolAssignment = resultLocationToolAssignments.FirstOrDefault(x => x == _locationToolAssignment);
            if (foundLocationToolAssignment != null)
            {
                foundLocationToolAssignment.AssignedTool.Status = StateId;
            }
        }


        public void ShowLocationReferenceLinksForTool(List<LocationReferenceLink> locationReferenceLinks)
        {
            StringBuilder buildOtherLocations = new StringBuilder();
            foreach (var i in locationReferenceLinks)
            {
                if (i.Id.ToLong() != LocationToolAssignment.AssignedLocation.Id.ToLong())
                {
                    buildOtherLocations.AppendLine(i.DisplayName);
                }
            }

           OtherConnectedLocations = buildOtherLocations.ToString();
        }
        #endregion


        public LocationToolAssignmentChangeStateModel(Dispatcher guiDispatcher, LocationToolAssignment assignment, string location, string powTool, Status stateId, List<LocationToolAssignmentChangeStateModel> locationToolAssignmentChangeStateModels) 
        {
            _guiDispatcher = guiDispatcher;
            _locationToolAssignmentChangeStateModels = locationToolAssignmentChangeStateModels;
            LocationToolAssignment = assignment;
            Location = location;
            PowTool = powTool;
            StateId = stateId;
            OtherConnectedLocations = "";
        }

    }
}
