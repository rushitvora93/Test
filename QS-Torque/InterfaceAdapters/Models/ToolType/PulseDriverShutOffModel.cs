using Core.Entities.ToolTypes;
using InterfaceAdapters.Localization;

namespace InterfaceAdapters.Models
{
    public class PulseDriverShutOffModel : AbstractToolTypeModel
    {
        public PulseDriverShutOffModel(ILocalizationWrapper localizationWrapper, AbstractToolType toolType=null) : base(localizationWrapper, toolType)
        {
        }

        public override string TranslatedName => _localizationWrapper.Strings.GetParticularString("ToolModelType", "PulseDriverShutOff");
    }
}