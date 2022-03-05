using Core.Entities.ToolTypes;
using InterfaceAdapters.Localization;

namespace InterfaceAdapters.Models
{
    public class ProductionWrenchModel : AbstractToolTypeModel
    {
        public ProductionWrenchModel(ILocalizationWrapper localizationWrapper, AbstractToolType toolType=null) : base(localizationWrapper, toolType)
        {
        }

        public override string TranslatedName => _localizationWrapper.Strings.GetParticularString("ToolModelType", "ProductionWrench");
    }
}