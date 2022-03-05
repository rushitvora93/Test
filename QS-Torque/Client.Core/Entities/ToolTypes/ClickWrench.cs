namespace Core.Entities.ToolTypes
{
    /// <summary>
    /// Databse number=5
    /// </summary>
    public class ClickWrench : AbstractToolType
    {
        public override string Name => nameof(ClickWrench);
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