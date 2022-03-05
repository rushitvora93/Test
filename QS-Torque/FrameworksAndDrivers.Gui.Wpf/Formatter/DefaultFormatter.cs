using System;
using Client.Core;
using Core;
using Core.Entities;
using Core.Entities.ReferenceLink;

namespace FrameworksAndDrivers.Gui.Wpf.Formatter
{
    public class DefaultFormatter : ILocationDisplayFormatter, IToolDisplayFormatter, ILocationToolAssignmentDisplayFormatter, ITestEquipmentDisplayFormatter
    {

        #region LocationDisplayFormatter

        public string Format(LocationReferenceLink link)
        {
            if (link is null)
            {
                throw new ArgumentNullException(nameof(link), "Link cant be null");
            }
            return GetLocationDisplayString(link.Description.ToDefaultString(), link.Number.ToDefaultString());
        }

        public string Format(Location location)
        {
            if (location is null)
            {
                throw new ArgumentNullException(nameof(location), "Location cant be null");
            }

            return GetLocationDisplayString(location.Number?.ToDefaultString(), location.Description?.ToDefaultString());
        }

        private string GetLocationDisplayString(string name, string userId)
        {
            return $"{name} - {userId}";
        }

        #endregion

        #region ToolDisplayFormatter

        public string Format(ToolReferenceLink link)
        {
            if (link is null)
            {
                throw new ArgumentNullException(nameof(link), "Link cant be null");
            }

            return GetToolDisplayString(link.SerialNumber, link.InventoryNumber);
        }

        public string Format(Tool tool)
        {
            if (tool is null)
            {
                throw new ArgumentNullException(nameof(tool), "Tool cant be null");
            }

            return GetToolDisplayString(tool.SerialNumber?.ToDefaultString(), tool.InventoryNumber?.ToDefaultString());
        }

        private string GetToolDisplayString(string serialNumber, string inventoryNumber)
        {
            return $"{serialNumber} - {inventoryNumber}";
        }

        #endregion

        #region LocationToolAssignmentDisplayFormatter

        public string Format(LocationToolAssignmentReferenceLink link)
        {
            if (link is null)
            {
                throw new ArgumentNullException(nameof(link), "Link cant be null");
            }

            return GetLocationToolAssignmentDiplayString(link.LocationNumber, link.LocationName, link.ToolSerialNumber, link.ToolInventoryNumber);
        }

        public string Format(LocationToolAssignment locationToolAssignment)
        {
            if (locationToolAssignment is null)
            {
                throw new ArgumentNullException(nameof(locationToolAssignment), "Tool cant be null");
            }

            return GetLocationToolAssignmentDiplayString(locationToolAssignment.AssignedLocation.Number,
                locationToolAssignment.AssignedLocation.Description, locationToolAssignment.AssignedTool.SerialNumber?.ToDefaultString(),
                locationToolAssignment.AssignedTool.InventoryNumber?.ToDefaultString());
        }

        private string GetLocationToolAssignmentDiplayString(LocationNumber linkLocationNumber, LocationDescription linkLocationName, string linkToolSerialNumber, string linkToolInventoryNumber)
        {
            return
                $"{GetLocationDisplayString(linkLocationNumber.ToDefaultString(), linkLocationName.ToDefaultString())} / {GetToolDisplayString(linkToolSerialNumber, linkToolInventoryNumber)}";
        }
        #endregion

        #region TestEquipmentDisplayFormatter

        public string Format(TestEquipment testEquipment)
        {
            if (testEquipment is null)
            {
                throw new ArgumentNullException(nameof(testEquipment), "TestEquipment cant be null");
            }

            return GetTestEquipmentDisplayString(testEquipment.SerialNumber?.ToDefaultString(), testEquipment.InventoryNumber?.ToDefaultString());
        }

        private string GetTestEquipmentDisplayString(string serialNumber, string inventoryNumber)
        {
            return $"{serialNumber} - {inventoryNumber}";
        }
        #endregion
    }
}
