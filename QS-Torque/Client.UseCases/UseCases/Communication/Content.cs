namespace Core.UseCases.Communication.DataGate
{
    public class Content : IElement
    {
        public Content(ElementName name, string value)
        {
            _name = name;
            _value = value;
        }

        public void Accept(IElementVisitor visitor)
        {
            visitor.Visit(this);
        }

        public ElementName GetName()
        {
            return _name;
        }

        public string GetValue()
        {
            return _value;
        }

        public void SetValue(string value)
        {
            _value = value;
        }

        private ElementName _name;
        private string _value;
    }
}