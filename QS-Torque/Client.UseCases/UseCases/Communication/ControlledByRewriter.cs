using System.Linq;

namespace Core.UseCases.Communication.DataGate
{
    public class ControlledByRewriter : ISemanticModelRewriter, IElementVisitor
    {
        public void Apply(ref SemanticModel dataGateSemanticModel)
        {
            dataGateSemanticModel.Accept(this);
        }

        public void Visit(Content element)
        {
        }

        public void Visit(Container element)
        {
            foreach (var subelement in element)
            {
                if (subelement.GetName().ToDefaultString() == "TestItem")
                {
                    ApplyRewrite(subelement as Container);
                }
                else
                {
                    subelement.Accept(this);
                }
            }
        }

        public void Visit(HiddenContent element)
        {
        }

        private void ApplyRewrite(Container testItem)
        {
            var torqueNom = (testItem.FirstOrDefault(element => element.GetName().ToDefaultString() == "DimensionTorqueNominal") as HiddenContent).GetValue();
            var torqueMin = (testItem.FirstOrDefault(element => element.GetName().ToDefaultString() == "DimensionTorqueMin") as HiddenContent).GetValue();
            var torqueMax = (testItem.FirstOrDefault(element => element.GetName().ToDefaultString() == "DimensionTorqueMax") as HiddenContent).GetValue();
            var angleNom = (testItem.FirstOrDefault(element => element.GetName().ToDefaultString() == "DimensionAngleNominal") as HiddenContent).GetValue();
            var angleMin = (testItem.FirstOrDefault(element => element.GetName().ToDefaultString() == "DimensionAngleMin") as HiddenContent).GetValue();
            var angleMax = (testItem.FirstOrDefault(element => element.GetName().ToDefaultString() == "DimensionAngleMax") as HiddenContent).GetValue();

            if ((testItem.FirstOrDefault(element => element.GetName().ToDefaultString() == "ControlDimension") as HiddenContent).GetValue() == "0")
            {
                (testItem.FirstOrDefault(element => element.GetName().ToDefaultString() == "Unit1Id") as Content).SetValue("10");
                (testItem.FirstOrDefault(element => element.GetName().ToDefaultString() == "Unit1") as Content).SetValue("Deg");
                (testItem.FirstOrDefault(element => element.GetName().ToDefaultString() == "Nom1") as Content).SetValue(angleNom);
                (testItem.FirstOrDefault(element => element.GetName().ToDefaultString() == "Min1") as Content).SetValue(angleMin);
                (testItem.FirstOrDefault(element => element.GetName().ToDefaultString() == "Max1") as Content).SetValue(angleMax);

                (testItem.FirstOrDefault(element => element.GetName().ToDefaultString() == "Unit2Id") as Content).SetValue("0");
                (testItem.FirstOrDefault(element => element.GetName().ToDefaultString() == "Unit2") as Content).SetValue("Nm");
                (testItem.FirstOrDefault(element => element.GetName().ToDefaultString() == "Nom2") as Content).SetValue(torqueNom);
                (testItem.FirstOrDefault(element => element.GetName().ToDefaultString() == "Min2") as Content).SetValue(torqueMin);
                (testItem.FirstOrDefault(element => element.GetName().ToDefaultString() == "Max2") as Content).SetValue(torqueMax);
                return;
            }
            (testItem.FirstOrDefault(element => element.GetName().ToDefaultString() == "Unit1Id") as Content).SetValue("0");
            (testItem.FirstOrDefault(element => element.GetName().ToDefaultString() == "Unit1") as Content).SetValue("Nm");
            (testItem.FirstOrDefault(element => element.GetName().ToDefaultString() == "Nom1") as Content).SetValue(torqueNom);
            (testItem.FirstOrDefault(element => element.GetName().ToDefaultString() == "Min1") as Content).SetValue(torqueMin);
            (testItem.FirstOrDefault(element => element.GetName().ToDefaultString() == "Max1") as Content).SetValue(torqueMax);

            (testItem.FirstOrDefault(element => element.GetName().ToDefaultString() == "Unit2Id") as Content).SetValue("10");
            (testItem.FirstOrDefault(element => element.GetName().ToDefaultString() == "Unit2") as Content).SetValue("Deg");
            (testItem.FirstOrDefault(element => element.GetName().ToDefaultString() == "Nom2") as Content).SetValue(angleNom);
            (testItem.FirstOrDefault(element => element.GetName().ToDefaultString() == "Min2") as Content).SetValue(angleMin);
            (testItem.FirstOrDefault(element => element.GetName().ToDefaultString() == "Max2") as Content).SetValue(angleMax);
        }
    }
}