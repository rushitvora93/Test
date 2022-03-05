using NGettext;
using System;
using System.Collections.Generic;
using System.Globalization;
using InterfaceAdapters.Localization;
using Client.Core;

namespace FrameworksAndDrivers.Localization
{
    public class LocalizationWrapper : ILocalizationWrapper
    {
        public class CatalogProxy : ICatalogProxy
        {
			public void SetCat(ICatalog newcat)
			{
				cat = newcat;
			}

			public string GetString(string text)
			{
				return cat.GetString(text);
			}

			public string GetParticularString(string context, string text)
			{
				return cat.GetParticularString(context, text);
			}

			private ICatalog cat;
		}

		private readonly string Domain = "Messages";
		private string _language;
		private List<WeakReference<IGetUpdatedByLanguageChanges>> _languageUpdateSubscribers;

		public ICatalogProxy Strings { get; protected set; }

		public LocalizationWrapper(string domain)
		{
            if (domain == "")
            {
                _languageUpdateSubscribers = new List<WeakReference<IGetUpdatedByLanguageChanges>>();
				return;
            }
			Domain = domain;
			NGettext.Wpf.CompositionRoot.Compose(Domain);
			Strings = new CatalogProxy();
			_languageUpdateSubscribers = new List<WeakReference<IGetUpdatedByLanguageChanges>>();
		}

        public void SetLanguage(string language)
		{
			_language = language;
			new NGettext.Wpf.ChangeCultureCommand().Execute(language);
			CultureInfo.CurrentCulture = CultureInfo.GetCultureInfoByIetfLanguageTag(language);
            if (Strings is CatalogProxy catalogProxy)
            {
                catalogProxy.SetCat(new Catalog(Domain, "./Locale", CultureInfo.CurrentCulture));
			}
            UpdateSubscribers();
		}

		public string GetLanguage()
		{
			return _language;
		}

		public void Subscribe(IGetUpdatedByLanguageChanges subscriber)
		{
			_languageUpdateSubscribers.Add(new WeakReference<IGetUpdatedByLanguageChanges>(subscriber));
		}

		private void UpdateSubscribers()
		{
			List<WeakReference<IGetUpdatedByLanguageChanges>> toRemove = new List<WeakReference<IGetUpdatedByLanguageChanges>>();
			foreach (var subscriber in _languageUpdateSubscribers)
			{
				IGetUpdatedByLanguageChanges target;
				if (!subscriber.TryGetTarget(out target))
				{
					toRemove.Add(subscriber);
					continue;
				}
				target.LanguageUpdate();
			}

			foreach (var killedSubscriber in toRemove)
			{
				_languageUpdateSubscribers.Remove(killedSubscriber);
			}
		}
	}
}
