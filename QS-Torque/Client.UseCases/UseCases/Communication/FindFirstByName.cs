namespace Core.UseCases.Communication.DataGate
{
    public class FindFirstByName : IElementVisitor
    {
        public FindFirstByName(ElementName name)
        {
            _name = name;
        }

        public void Visit(Content element)
        {
            if (element.GetName().Equals(_name))
            {
                Result = element;
            }
        }

        public void Visit(Container element)
        {
            if (element.GetName().Equals(_name))
            {
                Result = element;
                return;
            }

            foreach (var item in element)
            {
                item.Accept(this);
                if (!ReferenceEquals(null, Result))
                {
                    return;
                }
            }
        }

        public void Visit(HiddenContent element)
        {
            if (element.GetName().Equals(_name))
            {
                Result = element;
            }
        }

        public IElement Result;
        private ElementName _name;
    }
}