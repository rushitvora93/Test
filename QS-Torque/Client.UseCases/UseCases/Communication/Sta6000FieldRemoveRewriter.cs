using System.Collections.Generic;
using System.Linq;
using Core.UseCases.Communication.DataGate;

namespace Core.UseCases.Communication
{
    public class Sta6000FieldRemoveRewriter: ISemanticModelRewriter
    {
        public void Apply(ref SemanticModel dataGateSemanticModel)
        {
            var routeFinder = new FindFirstByName(new ElementName("Route"));
            dataGateSemanticModel.Accept(routeFinder);
            foreach (var element in (routeFinder.Result as Container))
            {
                if (element.GetName().Equals(new ElementName("TestItem")))
                {
                    RemoveFromTestItem(element as Container);
                }
            }
        }

        private void RemoveFromTestItem(Container testItem)
        {
            var itemsToRemove = new List<IElement>();

            var testMethod = (testItem.First(element => element.GetName().Equals(new ElementName("TestMethod"))) as Content)
                .GetValue();

            switch (testMethod)
            {
                case "19": // general
                    itemsToRemove = testItem.Where(GeneralRemoveElementNameFilter).ToList();
                    break;
                case "18": // click
                    itemsToRemove = testItem.Where(ClickRemoveElementNameFilter).ToList();
                    break;
                case "13": // peak
                    itemsToRemove = testItem.Where(PeakRemoveElementNameFilter).ToList();
                    break;
                case "14": //pulse
                    itemsToRemove = testItem.Where(PulseRemoveElementNameFilter).ToList();
                    break;
            }

            foreach (var element in itemsToRemove)
            {
                testItem.Remove(element);
            }
        }

        private bool GeneralRemoveElementNameFilter(IElement element)
        {
            return
                element.GetName().Equals(new ElementName("AC_MinimumPulse"))
                || element.GetName().Equals(new ElementName("AC_MaximumPulse"))
                || element.GetName().Equals(new ElementName("AC_TorqueCoefficient"))
                || element.GetName().Equals(new ElementName("AC_SlipTorque"));
        }

        private bool ClickRemoveElementNameFilter(IElement element)
        {
            return
                element.GetName().Equals(new ElementName("AC_MeasureTorqueAt"))
                || element.GetName().Equals(new ElementName("AC_CmCmkSpcTestType"))
                || element.GetName().Equals(new ElementName("AC_MinimumPulse"))
                || element.GetName().Equals(new ElementName("AC_MaximumPulse"))
                || element.GetName().Equals(new ElementName("AC_TorqueCoefficient"));
        }

        private bool PeakRemoveElementNameFilter(IElement element)
        {
            return
                element.GetName().Equals(new ElementName("AC_CycleComplete"))
                || element.GetName().Equals(new ElementName("AC_MeasureDelayTime"))
                || element.GetName().Equals(new ElementName("AC_ResetTime"))
                || element.GetName().Equals(new ElementName("AC_SlipTorque"))
                || element.GetName().Equals(new ElementName("AC_TorqueCoefficient"))
                || element.GetName().Equals(new ElementName("AC_MeasureTorqueAt"))
                || element.GetName().Equals(new ElementName("AC_MinimumPulse"))
                || element.GetName().Equals(new ElementName("AC_MaximumPulse"));
        }

        private bool PulseRemoveElementNameFilter(IElement element)
        {
            return
                element.GetName().Equals(new ElementName("AC_MeasureDelayTime"))
                || element.GetName().Equals(new ElementName("AC_ResetTime"))
                || element.GetName().Equals(new ElementName("AC_SlipTorque"))
                || element.GetName().Equals(new ElementName("AC_MeasureTorqueAt"))
                || element.GetName().Equals(new ElementName("AC_CmCmkSpcTestType"))
                || element.GetName().Equals(new ElementName("AC_MeasureTorqueAt"))
                || element.GetName().Equals(new ElementName("AC_CycleComplete"));
        }
    }
}
