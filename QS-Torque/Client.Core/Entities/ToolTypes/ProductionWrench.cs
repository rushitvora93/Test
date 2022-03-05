namespace Core.Entities.ToolTypes
{
    /// <summary>
    /// Databse number=8
    /// </summary>
    public class ProductionWrench : AbstractToolType
    {
        public override string Name => nameof(ProductionWrench);
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