namespace Core.Entities.ReferenceLink
{

    public class LocationReferenceLink : ReferenceLink
    {
        public LocationNumber Number { get; private set; }
        public LocationDescription Description { get; private set; }
        private ILocationDisplayFormatter _formatter;

        public override string DisplayName => _formatter.Format(this);

        public LocationReferenceLink(QstIdentifier id, LocationNumber number, LocationDescription description, ILocationDisplayFormatter formatter)
        {
            Id = id;
            Number = number;
            Description = description;
            _formatter = formatter;
        }
    }
}
