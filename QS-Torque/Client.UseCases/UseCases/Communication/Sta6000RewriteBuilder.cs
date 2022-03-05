using Core.Entities;

namespace Core.UseCases.Communication.DataGate
{
    public class Sta6000RewriteBuilder: ISemanticModelRewriterBuilder
    {
        private class Sta6000Rewriter: ISemanticModelRewriter
        {
            public void Apply(ref SemanticModel dataGateSemanticModel)
            {
                new ControlledByRewriter().Apply(ref dataGateSemanticModel);
                new Sta6000FieldRemoveRewriter().Apply(ref dataGateSemanticModel);
            }
        }

        public ISemanticModelRewriter Build(TestEquipment testEquipment)
        {
            return new Sta6000Rewriter();
        }
    }
}