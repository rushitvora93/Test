using FrameworksAndDrivers.Gui.Wpf.View.Themes;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace FrameworksAndDrivers.Gui.Wpf.Test
{
    class ThemeDictionaryTest
    {
        private ThemeDictionary _themeDictionary;
        private bool _wasThemeChangedCalled;

        const string normalUri = "/FrameworksAndDrivers.Gui.Wpf;component/View/Themes/QstColors.xaml";
        const string darkUri = "/FrameworksAndDrivers.Gui.Wpf;component/View/Themes/DarkModeColors.xaml";

        [SetUp]
        public void ThemeDictionarySetUp()
        {
            var themeUris = new Dictionary<Theme, Uri>
            {
                { Theme.Normal, new Uri(normalUri, UriKind.RelativeOrAbsolute) },
                { Theme.Dark, new Uri(darkUri, UriKind.RelativeOrAbsolute) }
            };

            _wasThemeChangedCalled = false;
            _themeDictionary = new ThemeDictionary(themeUris);
            _themeDictionary.ThemeChanged += (s, e) => _wasThemeChangedCalled = true;
        }


        [Test]
        public void OnCreateTest()
        {
            Assert.AreEqual(Theme.Normal, _themeDictionary.CurrentTheme);
            Assert.AreEqual(normalUri, _themeDictionary.Source.OriginalString.TrimStart('<').TrimEnd('>'));
        }

        [Test]
        public void ChangeThemeTest()
        {
            Theme eventArgTheme = Theme.Normal;
            _themeDictionary.ThemeChanged += (s, e) => eventArgTheme = e;

            _themeDictionary.ChangeTheme(Theme.Dark);

            Assert.AreEqual(Theme.Dark, _themeDictionary.CurrentTheme);
            Assert.AreEqual(darkUri, _themeDictionary.Source.OriginalString.TrimStart('<').TrimEnd('>'));
            Assert.IsTrue(_wasThemeChangedCalled);
            Assert.AreEqual(Theme.Dark, eventArgTheme);
        }

        [Test]
        public void ChangeToSameTheme()
        {
            _themeDictionary.ChangeTheme(Theme.Normal);

            Assert.AreEqual(Theme.Normal, _themeDictionary.CurrentTheme);
            Assert.AreEqual(normalUri, _themeDictionary.Source.OriginalString.TrimStart('<').TrimEnd('>'));
            Assert.IsFalse(_wasThemeChangedCalled);
        }
    }
}
