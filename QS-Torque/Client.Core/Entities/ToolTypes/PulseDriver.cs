namespace Core.Entities.ToolTypes
{

    /// <summary>
    /// Database number=1
    /// </summary>
    public class PulseDriver : AbstractToolType
    {
        public override string Name => nameof(PulseDriver);
        public override bool DoesToolTypeHasProperty(string propertyName)
        {
            if (propertyName.Equals(nameof(ToolModel.AirPressure)))
            {
                return true;
            }
            else if(propertyName.Equals(nameof(ToolModel.AirConsumption)))
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