namespace Core.Entities.ToolTypes
{
    /// <summary>
    /// Database number=2
    /// </summary>
    public class PulseDriverShutOff : AbstractToolType
    {
        public override string Name => nameof(PulseDriverShutOff);
        public override bool DoesToolTypeHasProperty(string propertyName)
        {
            if (propertyName.Equals(nameof(ToolModel.AirPressure)))
            {
                return true;
            }
            else if (propertyName.Equals(nameof(ToolModel.AirConsumption)))
            {
                return true;
            }
            else if (propertyName.Equals(nameof(ToolModel.MaxRotationSpeed)))
            {
                return true;
            }
            return false;
        }

        public override void Accept(IAbstractToolTypeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}