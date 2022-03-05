namespace Core.UseCases.Communication.DataGate
{
    public class HiddenContent : IElement
    {
        public HiddenContent(ElementName name, string value)
        {
            _name = name;
            _value = value;
        }

        public ElementName GetName()
        {
            return _name;
        }

        public void Accept(IElementVisitor visitor)
        {
            visitor.Visit(this);
        }

        public string GetValue()
        {
            return _value;
        }

        private ElementName _name;
        private string _value;
    }
}