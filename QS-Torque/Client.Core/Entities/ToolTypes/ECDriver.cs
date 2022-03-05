namespace Core.Entities.ToolTypes
{
    /// <summary>
    /// Database number=4
    /// </summary>
    public class ECDriver : AbstractToolType
    {
        public override string Name => nameof(ECDriver);
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

            return false;
        }

        public override void Accept(IAbstractToolTypeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}