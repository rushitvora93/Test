using System.Collections;
using System.Collections.Generic;

namespace Core.UseCases.Communication.DataGate
{
    public class Container: IElement, IEnumerable<IElement>
    {
        public Container(ElementName name)
        {
            _name = name;
            _subElements = new List<IElement>();
        }

        public void Accept(IElementVisitor visitor)
        {
            visitor.Visit(this);
        }

        public ElementName GetName()
        {
            return _name;
        }

        public IEnumerator<IElement> GetEnumerator()
        {
            return _subElements.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(IElement content)
        {
            _subElements.Add(content);
        }

        public void Remove(IElement content)
        {
            _subElements.Remove(content);
        }

        private ElementName _name;
        private List<IElement> _subElements;
    }
}