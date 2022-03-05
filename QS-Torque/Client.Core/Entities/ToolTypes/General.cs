namespace Core.Entities.ToolTypes
{
    /// <summary>
    /// Database number=3
    /// </summary>
    public class General : AbstractToolType
    {
        public override string Name => nameof(General);
        public override bool DoesToolTypeHasProperty(string propertyName)
        {
            if (propertyName.Equals(nameof(ToolModel.BatteryVoltage)))
            {
                return true;
            }
            else if (propertyName.Equals(nameof(ToolModel.MaxRotationSpeed)))
            {
                return true;
            }
            else if (propertyName.Equals(nameof(ToolModel.AirPressure)))
            {
                return true;
            }
            else if (propertyName.Equals(nameof(ToolModel.AirConsumption)))
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