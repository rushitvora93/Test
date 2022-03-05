namespace Core.Entities.ReferenceLink
{
    public class ToolReferenceLink : ReferenceLink
    {
        public string InventoryNumber { get; private set; }
        public string SerialNumber { get; private set; }
        private IToolDisplayFormatter _formatter;

        public override string DisplayName => _formatter.Format(this);

        public ToolReferenceLink(QstIdentifier id, string inventoryNumber, string serialNumber, IToolDisplayFormatter formatter)
        {
            Id = id;
            InventoryNumber = inventoryNumber;
            SerialNumber = serialNumber;
            _formatter = formatter;
        }
    }
}
