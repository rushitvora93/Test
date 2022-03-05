using Client.Core;
using InterfaceAdapters.Localization;

namespace FrameworksAndDrivers.Gui.Wpf.Test
{
    class NullCatalogProxy : ICatalogProxy
	{
		public string GetParticularString(string context, string text)
		{
			return "";
		}

		public string GetString(string text)
		{
			return "";
		}
	}

	class NullLocalizationWrapper : ILocalizationWrapper
	{
		public NullLocalizationWrapper()
		{
			Strings = new NullCatalogProxy();
		}

        public ICatalogProxy Strings { get; }

        public void SetLanguage(string language)
        {
        }

        public string GetLanguage()
        {
            return "";
        }

        public void Subscribe(IGetUpdatedByLanguageChanges subscriber)
        {
        }
    }
}
