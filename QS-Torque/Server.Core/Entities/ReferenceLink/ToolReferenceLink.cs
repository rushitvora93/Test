using Core.Entities;

namespace Server.Core.Entities.ReferenceLink
{
    public class ToolReferenceLink : ReferenceLink
    {
        public string InventoryNumber { get; private set; }
        public string SerialNumber { get; private set; }

        public ToolReferenceLink(QstIdentifier id, string inventoryNumber, string serialNumber)
        {
            Id = id;
            InventoryNumber = inventoryNumber;
            SerialNumber = serialNumber;
        }
    }
}
