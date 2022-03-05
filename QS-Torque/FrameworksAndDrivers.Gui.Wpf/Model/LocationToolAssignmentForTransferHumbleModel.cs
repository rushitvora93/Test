using System;
using Core.Entities;
using Core.Enums;
using Core.UseCases.Communication;
using InterfaceAdapters;
using InterfaceAdapters.Models;

namespace FrameworksAndDrivers.Gui.Wpf.Model
{
    public class LocationToolAssignmentForTransferHumbleModel: BindableBase
    {
        public LocationToolAssignmentForTransferHumbleModel(LocationToolAssignmentForTransfer locationToolAssignmentForTransfer)
        {
            _locationToolAssignmentForTransfer = locationToolAssignmentForTransfer;
        }

        public bool Selected
        {
            get => _selected;
            set => Set(ref _selected, value);
        }

        public LocationToolAssignmentForTransfer GetEntity()
        {
            return _locationToolAssignmentForTransfer;
        }

        public string LocationNumber
        {
            get=> _locationToolAssignmentForTransfer.LocationNumber.ToDefaultString();
            set => Set(ref _locationToolAssignmentForTransfer.LocationNumber, new LocationNumber(value));
        }

        public long LocationId
        {
            get => _locationToolAssignmentForTransfer.LocationId.ToLong();
            set => Set(ref _locationToolAssignmentForTransfer.LocationId, new LocationId(value));
        }

        public string LocationDescription
        {
            get => _locationToolAssignmentForTransfer.LocationDescription.ToDefaultString();
            set => Set(ref _locationToolAssignmentForTransfer.LocationDescription, new LocationDescription(value));
        }

        public long ToolUsageId
        {
            get => _locationToolAssignmentForTransfer.ToolUsageId.ToLong();
            set => Set(ref _locationToolAssignmentForTransfer.ToolUsageId, new HelperTableEntityId(value));
        }

        public string ToolUsageDescription
        {
            get => _locationToolAssignmentForTransfer.ToolUsageDescription.ToDefaultString();
            set => Set(ref _locationToolAssignmentForTransfer.ToolUsageDescription, new ToolUsageDescription(value));
        }

        public long ToolId
        {
            get => _locationToolAssignmentForTransfer.ToolId.ToLong();
            set => Set(ref _locationToolAssignmentForTransfer.ToolId, new ToolId(value));
        }

        public string ToolSerialNumber
        {
            get => _locationToolAssignmentForTransfer.ToolSerialNumber;
            set => Set(ref _locationToolAssignmentForTransfer.ToolSerialNumber, value);
        }

        public string ToolInventoryNumber
        {
            get => _locationToolAssignmentForTransfer.ToolInventoryNumber;
            set => Set(ref _locationToolAssignmentForTransfer.ToolInventoryNumber, value);
        }

        public long LocationToolAssignmentId
        {
            get => _locationToolAssignmentForTransfer.LocationToolAssignmentId.ToLong();
            set => Set(ref _locationToolAssignmentForTransfer.LocationToolAssignmentId, new LocationToolAssignmentId(value));
        }

        public string LocationFreeFieldCategory
        {
            get => _locationToolAssignmentForTransfer.LocationFreeFieldCategory;
            set => Set(ref _locationToolAssignmentForTransfer.LocationFreeFieldCategory, value);
        }

        public bool LocationFreeFieldDocumentation
        {
            get => _locationToolAssignmentForTransfer.LocationFreeFieldDocumentation;
            set => Set(ref _locationToolAssignmentForTransfer.LocationFreeFieldDocumentation, value);
        }

        public int SampleNumber => _locationToolAssignmentForTransfer.SampleNumber;
        public IntervalModel TestInterval => IntervalModel.GetModelFor(_locationToolAssignmentForTransfer.TestInterval);
        public DateTime? NextTestDate => _locationToolAssignmentForTransfer.NextTestDate;
        public DateTime? LastTestDate => _locationToolAssignmentForTransfer.LastTestDate;
        public Shift? NextTestDateShift => _locationToolAssignmentForTransfer.NextTestDateShift;
        private readonly LocationToolAssignmentForTransfer _locationToolAssignmentForTransfer;
        private bool _selected;
    }
}