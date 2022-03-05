using Client.Core;

namespace InterfaceAdapters.Localization
{
    public interface IGetUpdatedByLanguageChanges
    {
        void LanguageUpdate();
    }

    public interface ILocalizationWrapper
    {
        ICatalogProxy Strings { get; }
        void SetLanguage(string language);
        string GetLanguage();
        void Subscribe(IGetUpdatedByLanguageChanges subscriber);
    }
}
