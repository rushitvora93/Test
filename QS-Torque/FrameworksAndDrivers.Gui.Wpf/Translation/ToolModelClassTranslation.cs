using Core.Enums;
using InterfaceAdapters.Localization;

namespace FrameworksAndDrivers.Gui.Wpf.Translation
{
    public static class ToolModelClassTranslation
    {
        public static string GetTranlationForToolModelClass(ToolModelClass toolModelClass, ILocalizationWrapper localization)
        {
            switch (toolModelClass)
            {
                case ToolModelClass.WrenchScale: return localization.Strings.GetParticularString("ToolModelClass", "Wrench configurable/scale");
                case ToolModelClass.WrenchFixSet: return localization.Strings.GetParticularString("ToolModelClass", "Wrench fix set");
                case ToolModelClass.WrenchWithoutScale: return localization.Strings.GetParticularString("ToolModelClass", "Wrench without scale");
                case ToolModelClass.DriverScale: return localization.Strings.GetParticularString("ToolModelClass", "Driver scale");
                case ToolModelClass.DriverFixSet: return localization.Strings.GetParticularString("ToolModelClass", "Driver fix set");
                case ToolModelClass.DriverWithoutScale: return localization.Strings.GetParticularString("ToolModelClass", "Driver without scale");
                case ToolModelClass.WrenchWithBendingSteelLever: return localization.Strings.GetParticularString("ToolModelClass", "Beam-type torque wrench, adjustable, with scale");
                case ToolModelClass.WrenchBendingSteelLever: return localization.Strings.GetParticularString("ToolModelClass", "Beam-type torque wrench");
                case ToolModelClass.WrenchWithDialIndicator: return localization.Strings.GetParticularString("ToolModelClass", "Wrench with dial indicator");
                case ToolModelClass.WrenchElectronic: return localization.Strings.GetParticularString("ToolModelClass", "Wrench electronic");
                case ToolModelClass.DriverWithDialIndicator: return localization.Strings.GetParticularString("ToolModelClass", "Driver with dial indicator");
                case ToolModelClass.DriverElectronic: return localization.Strings.GetParticularString("ToolModelClass", "Driver electronic");
                default: return "";
            }
        }
    }
}
