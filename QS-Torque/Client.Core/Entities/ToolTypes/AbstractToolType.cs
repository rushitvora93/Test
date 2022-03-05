namespace Core.Entities.ToolTypes
{
    public interface IAbstractToolTypeVisitor
    {
        void Visit(ClickWrench toolType);
        void Visit(ECDriver toolType);
        void Visit(General toolType);
        void Visit(MDWrench toolType);
        void Visit(ProductionWrench toolType);
        void Visit(PulseDriver toolType);
        void Visit(PulseDriverShutOff toolType);
    }

    public abstract class AbstractToolType
    {
        public abstract string Name { get; }
        public abstract bool DoesToolTypeHasProperty(string propertyName);

        public abstract void Accept(IAbstractToolTypeVisitor visitor);

        public bool EqualsByType(AbstractToolType other)
        {
            return this.GetType() == other?.GetType();
        }
    }
}