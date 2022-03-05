using Core.Entities.ToolTypes;
using InterfaceAdapters.Localization;

namespace InterfaceAdapters.Models
{
    public class ClickWrenchModel : AbstractToolTypeModel
    {
        
        public ClickWrenchModel(ILocalizationWrapper localizationWrapper, AbstractToolType toolType = null) : base(localizationWrapper,toolType)
        {
            LanguageUpdate();
        }

        public override string TranslatedName => _localizationWrapper.Strings.GetParticularString("ToolModelType", "ClickWrench");
    }
}