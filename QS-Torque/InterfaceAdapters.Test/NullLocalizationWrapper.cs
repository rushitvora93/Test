using Client.Core;
using Client.TestHelper.Mock;
using InterfaceAdapters.Localization;

namespace InterfaceAdapters.Test
{
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
