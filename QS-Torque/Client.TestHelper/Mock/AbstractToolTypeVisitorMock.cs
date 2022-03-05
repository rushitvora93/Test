using Core.Entities.ToolTypes;

namespace TestHelper.Mock
{
    public class AbstractToolTypeVisitorMock : IAbstractToolTypeVisitor
    {
        public void Visit(ClickWrench toolType)
        {
            VisitClickWrenchWasCalled = true;
        }

        public void Visit(ECDriver toolType)
        {
            VisitECDriverWasCalled = true;
        }

        public void Visit(General toolType)
        {
            VisitGeneralWasCalled = true;
        }

        public void Visit(MDWrench toolType)
        {
            VisitMDWrenchWasCalled = true;
        }

        public void Visit(ProductionWrench toolType)
        {
            VisitProductionWrenchWasCalled = true;
        }

        public void Visit(PulseDriver toolType)
        {
            VisitPulseDriverWasCalled = true;
        }

        public void Visit(PulseDriverShutOff toolType)
        {
            VisitPulseDriverShutOffWasCalled = true;
        }

        public bool VisitClickWrenchWasCalled = false;
        public bool VisitECDriverWasCalled = false;
        public bool VisitGeneralWasCalled = false;
        public bool VisitMDWrenchWasCalled = false;
        public bool VisitProductionWrenchWasCalled = false;
        public bool VisitPulseDriverWasCalled = false;
        public bool VisitPulseDriverShutOffWasCalled = false;
    }
}
