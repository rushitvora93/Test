using Client.Core;
using InterfaceAdapters.Localization;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceAdapters.Test
{
    public class LocalizationWrapperMock : ILocalizationWrapper
    {
        public ICatalogProxy Strings { get; set; }

        public bool SetLanguageCalled { get; set; }

        public string DefaultLanguageReturnValue = "en";

        public string GetLanguage()
        {
            return DefaultLanguageReturnValue;
        }

        public void SetLanguage(string language)
        {
            SetLanguageCalled = true;
        }

        public void Subscribe(IGetUpdatedByLanguageChanges subscriber)
        {
            throw new NotImplementedException();
        }
    }

    public class LanguageInterfaceAdapterTest
    {
        [TestCase("en")]
        [TestCase("DE-de")]
        public void SetLanguageFromWrapperGetsCalledWhenGettingLanguageThroughInterfaceAdapter(string languageString)
        {
            var env = CreateInterfaceAdapterEnviroment();
            env.IfaceAdapter.GetLastLanguage(languageString);
            Assert.IsTrue(env.Localization.SetLanguageCalled);
            Assert.AreEqual(env.IfaceAdapter.Language, languageString);
        }

        [TestCase("en")]
        [TestCase("DE-de")]
        public void SetLanguageFromWrapperGetsCalledWhenSettingLanguageThroughInterfaceAdapter(string languageString)
        {
            var env = CreateInterfaceAdapterEnviroment();
            env.IfaceAdapter.SetDefaultLanguage(languageString);
            Assert.IsTrue(env.Localization.SetLanguageCalled);
            Assert.AreEqual(env.IfaceAdapter.Language, languageString);
        }

        private static Enviroment CreateInterfaceAdapterEnviroment()
        {
            var env = new Enviroment();
            env.Localization = new LocalizationWrapperMock();
            env.IfaceAdapter = new LanguageInterfaceAdapter(env.Localization);
            return env;
        }

        class Enviroment
        {
            public LanguageInterfaceAdapter IfaceAdapter;
            public LocalizationWrapperMock Localization;
        }

    }
}
