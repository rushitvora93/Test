using Client.UseCases.UseCases;
using InterfaceAdapters.Localization;
using System.ComponentModel;


namespace InterfaceAdapters
{
    public interface ILanguageInterface: INotifyPropertyChanged
    {
        string Language { get; set; }
    }

    public class LanguageInterfaceAdapter: BindableBase, ILanguageInterface, ILanguageGui
    {
        private string _language;
        private ILocalizationWrapper _localization;

        public string Language
        {
            get => _language;
            set
            {
                _language = value;
                RaisePropertyChanged();
            }
        }

        public void GetLastLanguage(string language)
        {
            _localization.SetLanguage(language);
            Language = language;
        }

        public void SetDefaultLanguage(string language)
        {
            _localization.SetLanguage(language);
            Language = language;
        }

        public LanguageInterfaceAdapter(ILocalizationWrapper localization)
        {
            _localization = localization;
        }
    }
}
