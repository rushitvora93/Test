using System.Text;
using Core.UseCases.Communication.DataGate;

namespace FrameworksAndDrivers.DataGate
{
    public class DataGateFileGenerator: IDataGateFileGenerator, IElementVisitor
    {
        public void Accept(SemanticModel semanticModel)
        {
            _builder = new StringBuilder();
            _builder.AppendLine("<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>");
            semanticModel.Accept(this);
        }

        public string DataGateFileContent()
        {
            return _builder.ToString();
        }

        public void Visit(Content element)
        {
            _builder.AppendLine($"<{element.GetName().ToDefaultString()}>{element.GetValue()}</{element.GetName().ToDefaultString()}>");
        }

        public void Visit(Container element)
        {
            _builder.AppendLine($"<{element.GetName().ToDefaultString()}>");
            foreach (var item in element)
            {
                item.Accept(this);
            }
            _builder.AppendLine($"</{element.GetName().ToDefaultString()}>");
        }

        public void Visit(HiddenContent element)
        {
            // HiddenContent does not get written to a file
        }

        private StringBuilder _builder;
    }
}
