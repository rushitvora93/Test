using Core.Entities.ToolTypes;
using InterfaceAdapters.Localization;

namespace InterfaceAdapters.Models
{
    public class ECDriverModel : AbstractToolTypeModel
    {
        public ECDriverModel(ILocalizationWrapper localizationWrapper, AbstractToolType toolType = null) : base(localizationWrapper,toolType)
        {
        }

        public override string TranslatedName => _localizationWrapper.Strings.GetParticularString("ToolModelType", "ECDriver");
    }
}
