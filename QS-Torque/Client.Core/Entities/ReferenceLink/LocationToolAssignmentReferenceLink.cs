namespace Core.Entities.ReferenceLink
{
    public class LocationToolAssignmentReferenceLink : ReferenceLink
    {
        public LocationId LocationId { get; set; }
        public LocationDescription LocationName { get; set; }
        public LocationNumber LocationNumber { get; set; }
        public ToolId ToolId { get; set; }
        public string ToolSerialNumber { get; set; }
        public string ToolInventoryNumber { get; set; }
        private ILocationToolAssignmentDisplayFormatter _formatter;

        public override string DisplayName => _formatter.Format(this);

        public LocationToolAssignmentReferenceLink(
            QstIdentifier id,
            LocationDescription locationName,
            LocationNumber locationNumber,
            string toolSerialNumber,
            string toolInventoryNumber,
            ILocationToolAssignmentDisplayFormatter formatter,
            LocationId locationId,
            ToolId toolId)
        {
            Id = id;
            LocationId = locationId;
            LocationName = locationName;
            LocationNumber = locationNumber;
            ToolId = toolId;
            ToolSerialNumber = toolSerialNumber;
            ToolInventoryNumber = toolInventoryNumber;
            _formatter = formatter;
        }
    }
}
