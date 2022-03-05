namespace Core.Entities.ToolTypes
{
    /// <summary>
    /// Database number=7
    /// </summary>
    public class MDWrench : AbstractToolType
    {
        public override string Name => nameof(MDWrench);
        public override bool DoesToolTypeHasProperty(string propertyName)
        {
            if (propertyName.Equals(nameof(ToolModel.Class)))
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